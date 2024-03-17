using System.Threading.Tasks;

namespace WeatherCheckerApi.Services
{
    public interface IWeatherCheckerService
    {
        Task<string> GetWeather(string cityName, string countryName);
    }
}
