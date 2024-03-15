using Moq;
using System.Net.Http;
using System.Net;
using WeatherCheckerApi;
using WeatherCheckerApi.Services;

namespace WeatherCheckerApiTests
{
    [TestFixture]
    public class WeatherCheckerApiIntegrationTests
    {
        [Test]
        public async Task GetWeatherAsync_ValidAPIKey_ReturnsDescription()
        {
            
            var weatherCheckerService = new WeatherCheckerService("8b7535b42fe1c551f18028f64e8688f7");

            var weatherData = await weatherCheckerService.GetWeather("Melbourne", "AU");

            Assert.That(weatherData, Is.Not.Null);
        }

        [Test]
        public async Task GetWeatherAsync_InvalidAPIKey_ReturnsDescription()
        {
            
            var weatherCheckerService = new WeatherCheckerService("Incorrect API Key String");

            Assert.ThrowsAsync<Exception>(async () => await weatherCheckerService.GetWeather("Melbourne", "AU"));
        }
    }
}