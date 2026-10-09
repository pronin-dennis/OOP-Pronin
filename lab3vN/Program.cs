
using System;

public class EventLogger : IDisposable
{
    private string _logName;
    private bool _isLogging;
    private bool _disposed;

    public string LogName
    {
        get { return _logName; }
    }

    public bool IsLogging
    {
        get { return _isLogging; }
    }

    public EventLogger(string logName)
    {
        _logName = logName;
        _isLogging = true;
        _disposed = false;

        Console.WriteLine("Логування розпочато: " + _logName);
    }

    public void LogEvent(string eventName)
    {
        if (_isLogging)
        {
            Console.WriteLine("Подія: " + eventName);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _isLogging = false;
            _disposed = true;

            Console.WriteLine("Логування зупинено.");
        }
    }

    ~EventLogger()
    {
        Dispose(false);
    }
}

public class MyResource : IDisposable
{
    private bool _disposed = false;
    private bool _isResourceAllocated;

    public MyResource()
    {
        _isResourceAllocated = true;
        Console.WriteLine("Resource allocated");
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Console.WriteLine("Disposing managed resources");
            }

            if (_isResourceAllocated)
            {
                Console.WriteLine("Releasing unmanaged resource");
                _isResourceAllocated = false;
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~MyResource()
    {
        Dispose(false);
    }
}

internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("EventLogger");
        using (EventLogger logger = new EventLogger("SystemLog"))
        {
            logger.LogEvent("Програма запущена");
            logger.LogEvent("Користувач увійшов у систему");
        }

        Console.WriteLine();
        Console.WriteLine("1. Використання using");

        using (MyResource resource1 = new MyResource())
        {
            Console.WriteLine("Using працює.");
        }
        Console.WriteLine();
        Console.WriteLine("2. Явний виклик Dispose()");
        MyResource resource2 = new MyResource();
        resource2.Dispose();
        Console.WriteLine();
        Console.WriteLine("3. Виклик деструктора через GC");
        CreateResource();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.WriteLine("Демонстрацію завершено.");
        Console.ReadKey();
    }

    static void CreateResource()
    {
        MyResource resource3 = new MyResource();
    }
}