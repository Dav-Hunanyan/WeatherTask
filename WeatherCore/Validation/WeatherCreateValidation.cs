using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Dal.Models;

namespace WeatherCore.Validation
{
    public class WeatherCreateValidation
    {
        protected WeatherDaily weatherDaily;
        public WeatherCreateValidation(WeatherDaily weatherDaily)
        {
            this.weatherDaily = weatherDaily;
        }

        public virtual void Validate()
        {
            if (weatherDaily.MaxTemp < weatherDaily.MinTemp)
                throw new Exception("MinTemperature is higher then MaxTemperature");
            if (weatherDaily.MaxTemp > 100 || weatherDaily.MinTemp < -100)
                throw new Exception("Invalid Temperature");

        }
    }
}
