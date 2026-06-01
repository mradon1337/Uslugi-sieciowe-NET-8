namespace TravelQuotesApi.Models
{
    // Pojedynczy cytat podróżniczy
    public class Quote
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
    }
}
