using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Dal.Models;
using WeatherCore.Validation;

namespace WeatherCore.Core
{
    public class WeatherCreator : BaseCore
    {
        private WeatherCreateValidation weatherCreateValidation;
        private WeatherDaily weatherDaily;
        public WeatherCreator(LigadatabaseContext db, WeatherDaily weatherDaily) : base(db)
        {
            this.weatherDaily = weatherDaily;
            weatherCreateValidation = new WeatherCreateValidation(weatherDaily);
        }

        public async Task<WeatherDaily> AddWeather()
        {
            try
            {
                var weather = CreateWeatherDaily();
                DB.WeatherDailies.Add(weather);
                DB.SaveChanges();
                return weather;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private WeatherDaily CreateWeatherDaily()
        {
            var weather = new WeatherDaily()
            {
                Day = weatherDaily.Day,
                MaxTemp = weatherDaily.MaxTemp,
                MinTemp = weatherDaily.MinTemp,
                WeatherDayDetails = CreateWeatherDetail(weatherDaily.WeatherDayDetails)
            };
            return weather;
        }
        private ICollection<WeatherDayDetail> CreateWeatherDetail(ICollection<WeatherDayDetail> details)
        {
            if (details.IsNullOrEmpty())
            {
                weatherCreateValidation.Validate();
                return null;
            }
            var validate = new WeatherDetailsValidaion(weatherDaily);
            validate.Validate();
            foreach (var item in details)
                item.Time = new TimeOnly(item.Time.Hour, default);
            return details;
        }
    }
}
