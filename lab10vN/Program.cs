using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lab10vN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<Product> products = new List<Product>()
            {
                new Product(1, "Ноутбук", 25000),
                new Product(2, "Смартфон", 18000),
                new Product(3, "Навушники", 2500),
                new Product(4, "Монітор", 8000),
                new Product(5, "Клавіатура", 1500),
                new Product(6, "Миша", 700),
                new Product(7, "Планшет", 12000),
                new Product(8, "Принтер", 6500),
                new Product(9, "Колонки", 3000),
                new Product(10, "Мікрофон", 2200),
                new Product(11, "Вебкамера", 1800),
                new Product(12, "Флешка", 400),
                new Product(13, "Жорсткий диск", 3500),
                new Product(14, "SSD", 2800),
                new Product(15, "Роутер", 2000),
                new Product(16, "Геймпад", 1700),
                new Product(17, "Відеокарта", 30000),
                new Product(18, "Процесор", 14000),
                new Product(19, "Оперативна пам'ять", 2500),
                new Product(20, "Материнська плата", 7000)
            };

            List<Review> reviews = new List<Review>()
            {
                new Review(1, 1, 5, "Чудовий ноутбук"),
                new Review(2, 1, 4, "Гарна продуктивність"),
                new Review(3, 2, 5, "Все працює добре"),
                new Review(4, 2, 5, "Задоволений покупкою"),
                new Review(5, 3, 3, "Звичайні навушники"),
                new Review(6, 3, 4, "Непоганий звук"),
                new Review(7, 4, 5, "Якісне зображення"),
                new Review(8, 4, 4, "Хороший монітор"),
                new Review(9, 5, 2, "Не дуже зручна"),
                new Review(10, 5, 3, "Середня якість"),
                new Review(11, 6, 4, "Працює добре"),
                new Review(12, 6, 5, "Зручна миша"),
                new Review(13, 7, 5, "Чудовий планшет"),
                new Review(14, 7, 4, "Все сподобалось"),
                new Review(15, 8, 2, "Часто виникають проблеми"),
                new Review(16, 8, 1, "Не рекомендую"),
                new Review(17, 9, 4, "Хороший звук"),
                new Review(18, 9, 5, "Гучні колонки"),
                new Review(19, 10, 3, "Нормальний мікрофон"),
                new Review(20, 10, 4, "Виконує свою роботу"),
                new Review(21, 11, 5, "Чітке зображення"),
                new Review(22, 11, 5, "Гарна камера"),
                new Review(23, 12, 3, "Звичайна флешка"),
                new Review(24, 12, 4, "Працює нормально"),
                new Review(25, 13, 5, "Великий обсяг"),
                new Review(26, 13, 4, "Хороший диск"),
                new Review(27, 14, 5, "Швидкий SSD"),
                new Review(28, 14, 5, "Все чудово"),
                new Review(29, 15, 2, "Слабкий сигнал"),
                new Review(30, 15, 3, "Є проблеми зі зв'язком"),
                new Review(31, 16, 4, "Зручний геймпад"),
                new Review(32, 16, 5, "Гарна якість"),
                new Review(33, 17, 5, "Висока продуктивність"),
                new Review(34, 17, 5, "Чудова відеокарта"),
                new Review(35, 18, 4, "Потужний процесор"),
                new Review(36, 18, 5, "Задоволений покупкою"),
                new Review(37, 19, 3, "Звичайна пам'ять"),
                new Review(38, 19, 4, "Працює стабільно"),
                new Review(39, 20, 2, "Не найкраща якість"),
                new Review(40, 20, 3, "Очікував більшого")
            };

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("ЗАПИТ 1. Продукти з рейтингом більше 4.0");

            stopwatch.Start();

            List<Product> result1 =
                (from product in products
                 join review in reviews
                 on product.Id equals review.ProductId
                 into productReviews
                 where productReviews.Average(r => r.Rating) > 4.0
                 select product).ToList();

            stopwatch.Stop();

            foreach (Product product in result1)
            {
                Console.WriteLine(product.Name);
            }

            Console.WriteLine("Час виконання: " +
                stopwatch.Elapsed.TotalMilliseconds + " мс");

            stopwatch.Reset();

            stopwatch.Start();

            List<Product> result1Methods = products.Where(p => reviews.Where(r => r.ProductId == p.Id).Average(r => r.Rating) > 4.0).ToList();

            stopwatch.Stop();

            Console.WriteLine("Методи LINQ:");

            foreach (Product product in result1Methods)
            {
                Console.WriteLine(product.Name);
            }

            Console.WriteLine("Час виконання: " + stopwatch.Elapsed.TotalMilliseconds + " мс");

            Console.WriteLine("\nЗАПИТ 2. Продукти та їхні відгуки");

            stopwatch.Reset();
            stopwatch.Start();

            var result2 =
                (from product in products
                 join review in reviews
                 on product.Id equals review.ProductId
                 select new
                 {
                     ProductName = product.Name,
                     Comment = review.Comment
                 }).ToList();

            stopwatch.Stop();

            foreach (var item in result2)
            {
                Console.WriteLine(item.ProductName + " - " + item.Comment);
            }

            Console.WriteLine("Час виконання: " + stopwatch.Elapsed.TotalMilliseconds + " мс");

            stopwatch.Reset();
            stopwatch.Start();

            var result2Methods = products
                .Join(reviews,
                    p => p.Id,
                    r => r.ProductId,
                    (p, r) => new
                    {
                        ProductName = p.Name,
                        Comment = r.Comment
                    }).ToList();

            stopwatch.Stop();

            Console.WriteLine("Методи LINQ:");

            foreach (var item in result2Methods)
            {
                Console.WriteLine(item.ProductName + " - " + item.Comment);
            }

            Console.WriteLine("Час виконання: " + stopwatch.Elapsed.TotalMilliseconds + " мс");

            Console.WriteLine("\nЗАПИТ 3. Середній рейтинг продуктів");

            stopwatch.Reset();
            stopwatch.Start();

            var result3 =
                (from product in products
                 join review in reviews
                 on product.Id equals review.ProductId
                 into productReviews
                 select new
                 {
                     ProductName = product.Name,
                     AverageRating = productReviews.Average(r => r.Rating)
                 }).ToList();

            stopwatch.Stop();

            foreach (var item in result3)
            {
                Console.WriteLine(item.ProductName + " - " + item.AverageRating);
            }

            Console.WriteLine("Час виконання: " + stopwatch.Elapsed.TotalMilliseconds + " мс");

            stopwatch.Reset();
            stopwatch.Start();

            var result3Methods = products.GroupJoin(reviews,p => p.Id,r => r.ProductId,(p, productReviews) => new
                    {
                        ProductName = p.Name,
                        AverageRating = productReviews.Average(r => r.Rating)
                    }).ToList();

            stopwatch.Stop();

            Console.WriteLine("Методи LINQ:");

            foreach (var item in result3Methods)
            {
                Console.WriteLine(item.ProductName + " - " + item.AverageRating);
            }

            Console.WriteLine("Час виконання: " + stopwatch.Elapsed.TotalMilliseconds + " мс");


            Console.WriteLine("\nЗАПИТ 4. Продукт з найнижчим рейтингом");

            stopwatch.Reset();
            stopwatch.Start();

            var result4 =
                (from product in products
                 join review in reviews
                 on product.Id equals review.ProductId
                 into productReviews
                 let average = productReviews.Average(r => r.Rating)
                 orderby average ascending
                 select new
                 {
                     ProductName = product.Name,
                     AverageRating = average
                 }).First();

            stopwatch.Stop();

            Console.WriteLine(result4.ProductName + " - рейтинг: " + result4.AverageRating);

            Console.WriteLine("Час виконання: " +stopwatch.Elapsed.TotalMilliseconds + " мс");

            stopwatch.Reset();
            stopwatch.Start();

            var result4Methods = products.GroupJoin(reviews,p => p.Id,r => r.ProductId,(p, productReviews) => new
                    {
                        ProductName = p.Name,
                        AverageRating = productReviews.Average(r => r.Rating)
                    })
                .OrderBy(item => item.AverageRating)
                .First();

            stopwatch.Stop();

            Console.WriteLine("Методи LINQ:");

            Console.WriteLine(result4Methods.ProductName + " - рейтинг: " + result4Methods.AverageRating);

            Console.WriteLine("Час виконання: " + stopwatch.Elapsed.TotalMilliseconds + " мс");
        }
    }
}