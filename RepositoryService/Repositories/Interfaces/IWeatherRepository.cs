using GlobalVariables;
using Weather.Dal.Models;

namespace RepositoreService.Repositories
{
    public interface IWeatherRepository
    {
        List<WeatherDailyModel> GetAllWeatherDailies();
        WeatherDailyModel GetWetherByDay(DateOnly date);
        List<WeatherDailyModel> GetWetherByDays(FilterDay filterDay);
        List<WeatherDayDetailModel> GetWeatherDetailsById(int dayId);
        WeatherDailyModel AddWeatherDay(WeatherDaily weatherDaily);
        WeatherDailyModel EditWeatherDay(WeatherDaily weatherDaily);
    }
}
