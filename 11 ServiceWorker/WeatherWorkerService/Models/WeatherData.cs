using Newtonsoft.Json;

namespace WeatherWorkerService.Models
{
    public class WeatherData
    {
        [JsonProperty("weather")]
        public List<WeatherDescription> Weather { get; set; } = new();

        [JsonProperty("main")]
        public MainInfo Main { get; set; } = new();

        [JsonProperty("wind")]
        public WindInfo Wind { get; set; } = new();

        [JsonProperty("sys")]
        public SysInfo Sys { get; set; } = new();

        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }
    }

    public class WeatherDescription
    {
        [JsonProperty("main")]
        public string Main { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
    }

    public class MainInfo
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class WindInfo
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }

    public class SysInfo
    {
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;
    }
}
