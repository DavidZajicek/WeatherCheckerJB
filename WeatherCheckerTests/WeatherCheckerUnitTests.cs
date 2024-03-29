using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using WeatherCheckerApi.Controllers;
using WeatherCheckerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace WeatherCheckerApi.Tests
{
    [TestFixture]
    public class WeatherCheckerControllerTests
    {
        private WeatherCheckerController _controller;
        private WeatherCheckerServiceStub _weatherCheckerServiceStub;
        private ApiKeyTracker _apiKeyTracker;

        [SetUp]
        public void SetUp()
        {
            _weatherCheckerServiceStub = new WeatherCheckerServiceStub();
            _apiKeyTracker = new ApiKeyTracker(new List<string> { "valid-api-key" }, 5);
            _controller = new WeatherCheckerController(_weatherCheckerServiceStub, _apiKeyTracker);
        }

        [Test]
        public async Task GetWeather_WithEmptyApiKey_ReturnsBadRequest()
        {
            // Arrange
            var cityName = "Melbourne";
            var countryName = "AU";
            var apiKey = "";

            // Act
            var result = await _controller.GetWeather(cityName, countryName, apiKey);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.AreEqual("API Key is required.", badRequestResult.Value);
        }

        [Test]
        public async Task GetWeather_WithValidApiKey_ReturnsOk()
        {
            // Arrange
            var cityName = "Melbourne";
            var countryName = "AU";
            var apiKey = "valid-api-key";

            // Act
            var result = await _controller.GetWeather(cityName, countryName, apiKey);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.AreEqual("Mocked weather data", okResult.Value);
        }

        [Test]
        public async Task GetWeather_WithInvalidApiKey_ReturnsUnauthorized()
        {
            // Arrange
            var cityName = "Melbourne";
            var countryName = "AU";
            var apiKey = "invalid-api-key";

            // Act
            var result = await _controller.GetWeather(cityName, countryName, apiKey);

            // Assert
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result.Result);
            var unauthorizedResult = (UnauthorizedObjectResult)result.Result;
            Assert.AreEqual("Invalid API Key.", unauthorizedResult.Value);
        }

        [Test]
        public async Task GetWeather_WithApiKeyUsageLimitReached_ReturnsTooManyRequests()
        {
            // Arrange
            var cityName = "Melbourne";
            var countryName = "AU";
            var apiKey = "valid-api-key";
            _apiKeyTracker.SetApiKeyUsageLimit(apiKey, 0);

            // Act
            var result = await _controller.GetWeather(cityName, countryName, apiKey);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = (ObjectResult)result.Result;
            Assert.AreEqual((int)HttpStatusCode.TooManyRequests, objectResult.StatusCode);
            Assert.AreEqual("API Key usage limit reached. Please try again later.", objectResult.Value);
        }
    }

    // Stub class for WeatherCheckerService
    public class WeatherCheckerServiceStub : IWeatherCheckerService
    {
        public Task<string> GetWeather(string cityName, string countryName)
        {
            return Task.FromResult("Mocked weather data");
        }
    }
}
