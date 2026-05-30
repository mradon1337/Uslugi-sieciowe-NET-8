using System.Text.Json.Serialization;

public class WeatherResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("main")]
    public Main Main { get; set; }

    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; }
}

public class Main
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
}

public class Weather
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class Wind
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }
}
