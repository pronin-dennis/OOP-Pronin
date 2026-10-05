using System;

class Weather
{
    private string _city;
    private DateTime _date;
    private double _temperatureCelsius;

    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    public DateTime Date
    {
        get { return _date; }
        set { _date = value; }
    }

    public double TemperatureCelsius
    {
        get { return _temperatureCelsius; }
        set
        {
            if (value >= -100 && value <= 100)
                _temperatureCelsius = value;
            else
                _temperatureCelsius = 10.0;
        }
    }

    public Weather() : this("Kyiv", DateTime.Now, 10.0)
    {
    }

    public Weather(string city, DateTime date, double temperature)
    {
        City = city;
        Date = date;
        TemperatureCelsius = temperature;
    }

    public void PrintForecast()
    {
        Console.WriteLine("Прогноз погоди:");
        Console.WriteLine("Місто: " + City);
        Console.WriteLine("Дата: " + Date.ToShortDateString());
        Console.WriteLine("Температура: " + TemperatureCelsius + " °C");
    }

    ~Weather()
    {
        Console.WriteLine("Об'єкт Weather знищено.");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Створення об'єктів класу Weather.");

        Weather weather1 = new Weather();
        Weather weather2 = new Weather("Lviv", DateTime.Now, 15.5);
        Weather weather3 = new Weather("Odesa", DateTime.Now, 22.0);

        Console.WriteLine("Об'єкти успішно створені.");
        Console.WriteLine();

        Console.WriteLine("Демонстрація роботи першого об'єкта:");
        weather1.PrintForecast();

        Console.WriteLine();

        Console.WriteLine("Демонстрація роботи другого об'єкта:");
        weather2.PrintForecast();

        Console.WriteLine();

        Console.WriteLine("Демонстрація роботи третього об'єкта:");
        weather3.PrintForecast();

        Console.WriteLine();
        Console.WriteLine("Об'єкти створені та методи виконані.");
        Console.WriteLine("Завершення Main. Підготовка до запуску збирача сміття.");

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Завершення програми.");
    }
}