using GlobalVariables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Dal.Models;

namespace WeatherCore.Core
{
    public class WeatherInfo : BaseCore
    {
        public WeatherInfo(LigadatabaseContext db) : base(db)
        {
        }

        public List<WeatherDaily> GetAllWeatherDailies()
        {
            return DB.WeatherDailies.Include(x => x.WeatherDayDetails).ToList();
        }

        public WeatherDaily GetWetherByDay(DateOnly date)
        {
            var weatherDay = DB.WeatherDailies.Include(x => x.WeatherDayDetails)
                .Where(x => x.Day == date).FirstOrDefault();
            return weatherDay;
        }
        public List<WeatherDaily> GetWetherByDays(FilterDay filterDay)
        {
            var weatherDays = DB.WeatherDailies.Include(x => x.WeatherDayDetails)
                .Where(x => x.Day > filterDay.StartDate && x.Day < filterDay.EndDate).ToList();
            return weatherDays;
        }

        public List<WeatherDayDetail> GetWeatherDetailsById(int dayId)
        {
            var weatherDayDetails = DB.WeatherDayDetails
                .Where(x => x.DayId == dayId).ToList();
            return weatherDayDetails;
        }
    }
}
