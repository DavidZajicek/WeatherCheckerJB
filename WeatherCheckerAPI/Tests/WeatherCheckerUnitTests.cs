using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.Net;
using WeatherCheckerApi.Services;

namespace WeatherCheckerApi.Tests
{
    [TestFixture]
    public class WeatherCheckerApiTests
    {
        [Test]
        public async Task GetWeatherAsync_ValidCity_ReturnsDescription()
        {
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("few clouds")
            });
            var weatherCheckerService = new WeatherCheckerService("API_KEY");

            var weatherData = await weatherCheckerService.GetWeather("Melbourne", "AU");

            Assert.That(weatherData, Is.Not.Null);
        }
    }
}