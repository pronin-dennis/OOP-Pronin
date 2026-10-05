using System;

class EventLogger : IDisposable
{
    private string _logName;
    private bool _isLogging;
    private bool _disposed;

    public string LogName
    {
        get { return _logName; }
        set { _logName = value; }
    }

    public bool IsLogging
    {
        get { return _isLogging; }
    }

    public EventLogger(string logName)
    {
        LogName = logName;
        _isLogging = true;
        _disposed = false;

        Console.WriteLine("Логування запущено: " + LogName);
    }

    public void LogEvent(string eventName)
    {
        if (_disposed)
        {
            Console.WriteLine("Об'єкт вже звільнений.");
            return;
        }

        if (_isLogging)
        {
            Console.WriteLine("Подія записана: " + eventName);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _isLogging = false;
            Console.WriteLine("Логування зупинено.");
        }
        else
        {
            _isLogging = false;
            Console.WriteLine("Логування зупинено деструктором.");
        }

        _disposed = true;
    }

    ~EventLogger()
    {
        Dispose(false);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("1. USING");

        using (EventLogger logger1 = new EventLogger("LogUsing"))
        {
            logger1.LogEvent("Запуск програми");
        }
        Console.WriteLine();

        Console.WriteLine("2. ЯВНИЙ DISPOSE");
        EventLogger logger2 = new EventLogger("LogDispose");
        logger2.LogEvent("Користувач увійшов у систему");
        logger2.Dispose();
        logger2.Dispose(); 
        Console.WriteLine();

        Console.WriteLine("3. ДЕСТРУКТОР + GC");
        EventLogger logger3 = new EventLogger("LogDestructor");
        logger3.LogEvent("Створено об'єкт");
        logger3 = null;
        Console.WriteLine("До виклику GC.Collect()");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.WriteLine("Після виклику GC.Collect()");
    }
}