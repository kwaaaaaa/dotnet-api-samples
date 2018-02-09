namespace APICollection.Models
{
    public enum TemperatureScales
    {
        Celsius,
        Farenheit,
        Kelvin
    }

    public enum BeaufortScale
    {
        Calm,
        Light,
        Gentle,
        Moderate,
        Strong,
        Severe,
        Stormy,
        Violent,
        Devastating
    }

    public class Weather
    {
        public string City { get; set; }
        public string Description { get; set; }
        public BeaufortScale Wind { get; set; }
        public string Humidity { get; set; }
        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }
        public TemperatureScales Scale { get; set; }
    }
}
