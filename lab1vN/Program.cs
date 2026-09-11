class Weather
{
    private string city;
    private DateTime date;

    public double Temperature { get; set; }

    // Властивості для доступу до приватних полів
    public string City
    {
        get { return city; }
        set { city = value; }
    }

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public Weather(string city, DateTime date, double temperature)
    {
        this.city = city;
        this.date = date;
        Temperature = temperature;
    }
    public void PrintForecast()
    {
        Console.WriteLine($"Місто: {City}");
        Console.WriteLine($"Дата: {Date:dd.MM.yyyy}");
        Console.WriteLine($"Температура: {Temperature}°C");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Weather weather1 = new Weather(
            "Рівне",
            new DateTime(2026, 9, 11),
            18.5
        );

        Weather weather2 = new Weather(
            "Київ",
            new DateTime(2026, 9, 11),
            21.0
        );

        Weather weather3 = new Weather(
            "Львів",
            new DateTime(2026, 9, 11),
            16.2
        );

        weather1.PrintForecast();
        weather2.PrintForecast();
        weather3.PrintForecast();
    }
}
