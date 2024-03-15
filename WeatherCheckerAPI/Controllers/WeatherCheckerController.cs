using System.Net;
using Microsoft.AspNetCore.Mvc;
using WeatherCheckerApi.Services;


namespace WeatherCheckerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherCheckerController : ControllerBase
    {
        private readonly WeatherCheckerService _weatherChecker;

        public WeatherCheckerController(WeatherCheckerService weatherChecker)
        {
            _weatherChecker = weatherChecker;
            //TODO: API Key Tracker for rate limiting
        }

        [HttpGet("{cityName}/{countryName}")]
        public async Task<ActionResult<string>> GetWeather(string cityName, string countryName)
        {
            //TODO: Rate Limit via API Keys
            try
            {
                var weatherData = await _weatherChecker.GetWeather(cityName, countryName);
                //TODO: Return only the Description from WeatherData
                return Ok(weatherData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}