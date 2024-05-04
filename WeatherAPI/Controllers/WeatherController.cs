using GlobalVariables;
using Microsoft.AspNetCore.Mvc;
using RepositoreService;
using RepositoreService.Repositories;
using Weather.Dal.Models;
using WeatherCore.Core;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WeatherController : Controller
    {
        IWeatherRepository weatherRepository;
        public WeatherController(IWeatherRepository weatherRepository)
        {
            this.weatherRepository = weatherRepository;
        }

        [HttpGet]
        [Route(nameof(GetAllWeatherDailies))]
        public async Task<IActionResult> GetAllWeatherDailies()
        {
            var response = weatherRepository.GetAllWeatherDailies();
            return Ok(response);
        }
        [HttpGet]
        [Route(nameof(GetWetherByDay))]
        public async Task<IActionResult> GetWetherByDay(DateOnly date)
        {
            var response = weatherRepository.GetWetherByDay(date);
            return Ok(response);
        }
        [HttpPost]
        [Route(nameof(GetWetherByDays))]
        public async Task<IActionResult> GetWetherByDays(FilterDay filterDay)
        {
            var response = weatherRepository.GetWetherByDays(filterDay);
            return Ok(response);
        }
        [HttpGet]
        [Route(nameof(GetWeatherDetailsById))]
        public async Task<IActionResult> GetWeatherDetailsById(int dayId)
        {
            var response = weatherRepository.GetWeatherDetailsById(dayId);
            return Ok(response);
        }

        [HttpPost]
        [Route(nameof(AddWeatherDay))]
        public async Task<IActionResult> AddWeatherDay(WeatherDaily weatherDaily)
        {
            var response = weatherRepository.AddWeatherDay(weatherDaily);
            return Ok(response);
        }
        [HttpPost]
        [Route(nameof(EditWeatherDail))]
        public async Task<IActionResult> EditWeatherDail(WeatherDaily weatherDaily)
        {
            var response = weatherRepository.EditWeatherDay(weatherDaily);
            return Ok(response);
        }
    }
}
