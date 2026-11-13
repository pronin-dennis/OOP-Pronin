
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace lab19vN
{
    class FileProcessor
    {
        private int attempts = 0;

        public List<string> ReadDatabaseDump(string path)
        {
            attempts++;

            if (attempts <= 2)
            {
                throw new FileNotFoundException("Файл дампу бази даних тимчасово недоступний.");
            }

            return new List<string>()
            {
                "ID: 1, Name: Олександр",
                "ID: 2, Name: Андрій",
                "ID: 3, Name: Максим"
            };
        }
    }

    class NetworkClient
    {
        private int attempts = 0;

        public List<string> QueryDatabase(string connectionString, string query)
        {
            attempts++;

            if (attempts <= 3)
            {
                throw new HttpRequestException("Помилка підключення до бази даних.");
            }

            return new List<string>()
            {
                "Результат 1: Дані отримано",
                "Результат 2: Запит виконано",
                "Результат 3: Підключення успішне"
            };
        }
    }

    public static class RetryHelper
    {
        public static T ExecuteWithRetry<T>(Func<T> operation,int retryCount = 3,TimeSpan initialDelay = default,Func<Exception, bool> shouldRetry = null)
        {
            int attempt = 0;

            while (true)
            {
                try
                {
                    attempt++;

                    Console.WriteLine("Спроба №" + attempt);

                    T result = operation();

                    Console.WriteLine("Операція виконана успішно!");

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);

                    if (attempt > retryCount || (shouldRetry != null && !shouldRetry(ex)))
                    {
                        Console.WriteLine("Повторні спроби припинено.");
                        throw;
                    }

                    double delay = initialDelay.TotalMilliseconds * Math.Pow(2, attempt - 1);

                    Console.WriteLine("Повторна спроба через " + delay + " мс.");

                    if (delay > 0)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(delay));
                    }
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створення об'єктів
            FileProcessor fileProcessor = new FileProcessor();
            NetworkClient networkClient = new NetworkClient();
            Func<Exception, bool> shouldRetry = ex => ex is FileNotFoundException || ex is HttpRequestException;

            Console.WriteLine("1. ЧИТАННЯ ДАМПУ БАЗИ ДАНИХ");

            try
            {
                List<string> fileData = RetryHelper.ExecuteWithRetry(() => fileProcessor.ReadDatabaseDump("database.txt"),3,TimeSpan.FromMilliseconds(500),shouldRetry);

                Console.WriteLine("\nОтримані дані:");

                foreach (string item in fileData)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не вдалося прочитати файл: " + ex.Message);
            }

            Console.WriteLine("\n2. ЗАПИТ ДО БАЗИ ДАНИХ");

            try
            {
                List<string> databaseData = RetryHelper.ExecuteWithRetry(() => networkClient.QueryDatabase("Server=localhost;Database=Test;","SELECT * FROM Users"),4,TimeSpan.FromMilliseconds(500),shouldRetry);

                Console.WriteLine("\nРезультати запиту:");

                foreach (string item in databaseData)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка запиту: " + ex.Message);
            }

            Console.WriteLine("\n3. ПЕРЕВІРКА SHOULDRETRY");

            try
            {
                RetryHelper.ExecuteWithRetry<int>(
                    () =>
                    {
                        throw new ArgumentException("Неправильний аргумент.");
                    },
                    3,
                    TimeSpan.FromMilliseconds(500),
                    shouldRetry
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Операцію завершено через виняток: " + ex.Message);
            }

            Console.WriteLine("\nПрограму завершено.");
        }
    }
}
