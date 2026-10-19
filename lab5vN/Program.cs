
using System;
using System.Collections.Generic;

class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string Message { get; set; }

    public LogEntry(DateTime timestamp, string message)
    {
        Timestamp = timestamp;
        Message = message;
    }

    public virtual void Display()
    {
        Console.WriteLine(Timestamp + " | " + Message);
    }
}

class InfoLog : LogEntry
{
    public string Source { get; set; }

    public InfoLog(DateTime timestamp, string message, string source)
        : base(timestamp, message)
    {
        Source = source;
    }

    public override void Display()
    {
        Console.WriteLine("INFO " + Timestamp + " | " + Message + " | Джерело: " + Source);
    }
}

class WarningLog : LogEntry
{
    public string Severity { get; set; }

    public WarningLog(DateTime timestamp, string message, string severity)
        : base(timestamp, message)
    {
        Severity = severity;
    }

    public override void Display()
    {
        Console.WriteLine("WARNING " + Timestamp + " | " + Message + " | Рівень важливості: " + Severity);
    }
}

class ErrorLog : LogEntry
{
    public string ExceptionType { get; set; }

    public ErrorLog(DateTime timestamp, string message, string exceptionType)
        : base(timestamp, message)
    {
        ExceptionType = exceptionType;
    }

    public override void Display()
    {
        Console.WriteLine("ERROR " + Timestamp + " | " + Message + " | Тип помилки: " + ExceptionType);
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        List<LogEntry> logs = new List<LogEntry>();

        logs.Add(new InfoLog(DateTime.Now, "Програму запущено", "Main"));
        logs.Add(new WarningLog(DateTime.Now, "Мало вільної пам'яті", "Середній"));
        logs.Add(new ErrorLog(DateTime.Now, "Не вдалося відкрити файл", "IOException"));

        Console.WriteLine("Журнал подій");

        foreach (LogEntry log in logs)
        {
            log.Display();
        }

        Console.WriteLine();
        Console.WriteLine("Загальна кількість записів: " + logs.Count);
    }
}
