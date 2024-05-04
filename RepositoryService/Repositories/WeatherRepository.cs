using GlobalVariables;
using Weather.Dal.Models;
using WeatherCore.Core;

namespace RepositoreService.Repositories
{
    public class WeatherRepository : BaseRepository, IWeatherRepository
    {
        #region Props
        #endregion

        #region Constructors
        public WeatherRepository() : base()
        {
        }
        #endregion

        #region GetInfo
        public List<WeatherDailyModel> GetAllWeatherDailies()
        {
            using (var core = new WeatherInfo(DB))
            {
                var days = core.GetAllWeatherDailies();
                return Mapper<WeatherDaily, WeatherDailyModel>.MapCollection(days);
            }
        }

        public WeatherDailyModel GetWetherByDay(DateOnly date)
        {
            using (var core = new WeatherInfo(DB))
            {
                var day = core.GetWetherByDay(date);
                return Mapper<WeatherDaily, WeatherDailyModel>.Map(day);
            }
        }
        public List<WeatherDailyModel> GetWetherByDays(FilterDay filterDay)
        {
            using (var core = new WeatherInfo(DB))
            {
                var days = core.GetWetherByDays(filterDay);
                return Mapper<WeatherDaily, WeatherDailyModel>.MapCollection(days);
            }
        }

        public List<WeatherDayDetailModel> GetWeatherDetailsById(int dayId)
        {
            using (var core = new WeatherInfo(DB))
            {
                var days = core.GetWeatherDetailsById(dayId);
                return Mapper<WeatherDayDetail, WeatherDayDetailModel>.MapCollection(days);
            }
        }
        #endregion

        #region Create

        public WeatherDailyModel AddWeatherDay(WeatherDaily weatherDaily)
        {
            using (var core = new WeatherCreator(DB, weatherDaily))
            {
                var weather = core.AddWeather();
                return Mapper<WeatherDaily, WeatherDailyModel>.Map(weather.Result);
            }
        }
        #endregion

        #region Edit

        public WeatherDailyModel EditWeatherDay(WeatherDaily weatherDaily)
        {
            using (var core = new WeatherEditor(DB, weatherDaily))
            {
                var weather = core.EditWeatherDay();
                return Mapper<WeatherDaily, WeatherDailyModel>.Map(weather);
            }
        }
        #endregion

    }
}
