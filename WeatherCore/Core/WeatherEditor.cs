using Microsoft.EntityFrameworkCore;
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
    public class WeatherEditor : BaseCore
    {
        private WeatherCreateValidation weatherCreateValidation;
        private WeatherDaily weatherDaily;
        public WeatherEditor(LigadatabaseContext db, WeatherDaily weatherDaily) : base(db)
        {
            this.weatherDaily = weatherDaily;
            weatherCreateValidation = new WeatherCreateValidation(weatherDaily);
        }

        public WeatherDaily EditWeatherDay()
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    weatherCreateValidation.Validate();
                    var weather = DB.WeatherDailies.Include(x => x.WeatherDayDetails).Where(x => x.Id == weatherDaily.Id).FirstOrDefault();
                    EditDetails(weather);
                    EditDayWeather(weather);
                    DB.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return weatherDaily;
        }
        private void EditDayWeather(WeatherDaily weather)
        {
            weather.MaxTemp = weatherDaily.MaxTemp;
            weather.MinTemp = weatherDaily.MinTemp;
            weather.Day = weatherDaily.Day;
        }
        private void EditDetails(WeatherDaily weather)
        {
            try
            {
                if (!weather.WeatherDayDetails.IsNullOrEmpty())
                    DB.RemoveRange(weather.WeatherDayDetails);
                else
                {
                    var validate = new WeatherDetailsValidaion(weatherDaily);
                    validate.Validate();
                    foreach (var item in weather.WeatherDayDetails)
                        item.Time = new TimeOnly(item.Time.Hour, default);
                    DB.WeatherDayDetails.AddRange(weather.WeatherDayDetails);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
