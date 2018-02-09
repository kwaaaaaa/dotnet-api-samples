namespace APICollection.Models
{
    public enum BaseRates
    {
        AUD,
        CNY,
        EUR,
        GBP,
        JPY,
        THB,
        USD
    }

    public class Currency
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
