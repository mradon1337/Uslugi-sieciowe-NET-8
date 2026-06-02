namespace WeatherWorkerService.Models
{
    public static class WeatherMapper
    {
        public static Weather ToWeather(this WeatherData data, string queriedCity)
        {
            return new Weather
            {
                City = queriedCity,
                ResolvedName = data.Name,
                Country = data.Sys.Country,
                Temperature = data.Main.Temp,
                FeelsLike = data.Main.FeelsLike,
                Humidity = data.Main.Humidity,
                Pressure = data.Main.Pressure,
                WindSpeed = data.Wind.Speed,

                Description = data.Weather.FirstOrDefault()?.Description ?? string.Empty,

                MeasuredAtUtc = DateTimeOffset.FromUnixTimeSeconds(data.Dt).UtcDateTime,
                SavedAtUtc = DateTime.UtcNow
            };
        }
    }
}
