using System.ComponentModel.DataAnnotations;

namespace WeatherWorkerService.Models
{
    public class Weather
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; } = string.Empty;

        public string ResolvedName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime MeasuredAtUtc { get; set; }
        public DateTime SavedAtUtc { get; set; }
    }
}
