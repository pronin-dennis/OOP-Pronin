using System;

class Product
{
    private int _id;
    private string _name;
    private decimal _price;
    private string _category;
    private int _stockCount;

    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public string Category { get; }
    public int StockCount { get; }

    // Конструктор 1 — основний
    public Product(int id, string name, decimal price, string category, int stockCount)
    {
        Id = id;
        Name = name;
        Price = price;
        Category = category;
        StockCount = stockCount;
    }

    public Product(int id, string name, decimal price)
        : this(id, name, price, "Uncategorized", 0)
    {
    }

    // Конструктор 3 — копіювання
    public Product(Product product)
        : this(product.Id,product.Name,product.Price,product.Category,product.StockCount)
    {
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price:C}, " +
               $"Category: {Category}, Stock: {StockCount}";
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Product product1 = new Product(1,"Ноутбук",25000,"Електроніка",10);

        Console.WriteLine("Конструктор 1");
        Console.WriteLine(product1);
        Console.WriteLine();
        Product product2 = new Product(2,"Мишка",800);

        Console.WriteLine("Конструктор 2");
        Console.WriteLine(product2);
        Console.WriteLine();
        Product original = new Product(3,"Клавіатура",1500,"Аксесуари",20);
        Product product3 = new Product(original);

        Console.WriteLine("Конструктор 3 - копіювання");
        Console.WriteLine("Оригінальний об'єкт:");
        Console.WriteLine(original);
        Console.WriteLine("Копія об'єкта:");
        Console.WriteLine(product3);
    }
}