using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Dal.Models;

namespace WeatherCore.Validation
{
    public class WeatherDetailsValidaion : WeatherCreateValidation
    {
        public WeatherDetailsValidaion(WeatherDaily weatherDaily) : base(weatherDaily)
        {
        }

        public override void Validate()
        {
            base.Validate();
            if (!weatherDaily.WeatherDayDetails.IsNullOrEmpty())
            {
                var details = weatherDaily.WeatherDayDetails;
                if (details.Any(x => x.Temperature > weatherDaily.MaxTemp || x.Temperature < weatherDaily.MinTemp))
                   throw new Exception("Details temperature does not match to day");
            }
        }
    }
}
