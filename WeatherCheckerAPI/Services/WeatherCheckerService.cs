namespace WeatherCheckerApi.Services
{
    public class WeatherCheckerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _openWeatherApiKey;

        public WeatherCheckerService(string openWeatherApiKey)
        {
            _httpClient = new HttpClient();
            _openWeatherApiKey = openWeatherApiKey;
        }

        public async Task<string> GetWeather(string cityName, string countryName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentException("City Name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(countryName))
            {
                throw new ArgumentException("Country Name cannot be empty");
            }

            var apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityName},{countryName}&appid={_openWeatherApiKey}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve data from Open Weather's Api.");
            }

            var weatherData = await response.Content.ReadAsStringAsync();
            return weatherData;
        }
    }
}