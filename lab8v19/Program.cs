using System;
using System.Collections.Generic;

namespace lab17vN
{
    class Product
    {
        public string Name;
        public double Price;

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    static class CollectionHelper<T>
    {
        public static double Average(IEnumerable<T> collection, Func<T, double> selector)
        {
            double sum = 0;
            int count = 0;

            foreach (T item in collection)
            {
                sum += selector(item);
                count++;
            }

            if (count == 0)
            {
                return 0;
            }

            return sum / count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>()
            {
                10, 20, 30, 40, 50
            };

            double averageNumbers = CollectionHelper<int>.Average(
                numbers, x => x);

            Console.WriteLine("Середнє значення чисел: " + averageNumbers);

            List<string> words = new List<string>()
            {
                "Програмування",
                "CSharp",
                "Колекція",
                "Метод"
            };

            double averageLength = CollectionHelper<string>.Average(
                words, x => x.Length);

            Console.WriteLine("Середня довжина рядків: " + averageLength);

            List<Product> products = new List<Product>()
            {
                new Product("Ноутбук", 25000),
                new Product("Миша", 500),
                new Product("Клавіатура", 1500),
                new Product("Монітор", 8000)
            };

            double averagePrice = CollectionHelper<Product>.Average(
                products, x => x.Price);

            Console.WriteLine("Середня ціна продуктів: " + averagePrice);
        }
    }
}
