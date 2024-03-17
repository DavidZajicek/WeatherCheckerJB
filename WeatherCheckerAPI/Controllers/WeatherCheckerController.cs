using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using WeatherCheckerApi.Models;
using WeatherCheckerApi.Services;


namespace WeatherCheckerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherCheckerController : ControllerBase
    {
        private readonly IWeatherCheckerService _weatherChecker;
        private readonly ApiKeyTracker _apiKeyTracker;

        public WeatherCheckerController(IWeatherCheckerService weatherChecker, ApiKeyTracker apiKeyTracker)
        {
            _weatherChecker = weatherChecker;
            _apiKeyTracker = apiKeyTracker;
        }

        [HttpGet("{cityName}/{countryName}")]
        public async Task<ActionResult<string>> GetWeather(string cityName, string countryName, [FromQuery] string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return BadRequest("API Key is required.");
            }

            if (!_apiKeyTracker.IsValidApiKey(apiKey))
            {
                return Unauthorized("Invalid API Key.");
            }

            if (!_apiKeyTracker.CanUseApiKey(apiKey))
            {
                return StatusCode(429, "API Key usage limit reached. Please try again later.");
            }

            _apiKeyTracker.TrackApiKeyUsage(apiKey);

            try
            {
                var weatherData = await _weatherChecker.GetWeather(cityName, countryName);
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