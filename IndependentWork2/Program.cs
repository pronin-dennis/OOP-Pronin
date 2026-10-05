using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

class Cart
{
    private List<Product> products = new List<Product>();
    private List<int> quantities = new List<int>();

    public void AddItem(Product product, int quantity)
    {
        products.Add(product);
        quantities.Add(quantity);
    }

    public double GetTotal()
    {
        double total = 0;

        for (int i = 0; i < products.Count; i++)
        {
            double sum = products[i].Price * quantities[i];

            if (sum > 500)
                sum = sum * 0.9;

            total += sum;
        }

        return total;
    }

    public void ApplyDiscount()
    {
        for (int i = 0; i < products.Count; i++)
        {
            double sum = products[i].Price * quantities[i];

            if (sum > 500)
                Console.WriteLine(products[i].Name + ": знижка 10%");
            else
                Console.WriteLine(products[i].Name + ": без знижки");
        }
    }
}

class Program
{
    // Процедурна версія
    static double CalculateItemSum(double price, int quantity)
    {
        return price * quantity;
    }

    static double ApplyProceduralDiscount(double sum)
    {
        if (sum > 500)
            return sum * 0.9;

        return sum;
    }

    static double CalculateProceduralTotal(double[] prices, int[] quantities)
    {
        double total = 0;

        for (int i = 0; i < prices.Length; i++)
        {
            double sum = CalculateItemSum(prices[i], quantities[i]);
            total += ApplyProceduralDiscount(sum);
        }

        return total;
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("        ПРОЦЕДУРНИЙ ПІДХІД");
        Console.WriteLine();

        string[] names = { "Навушники", "Клавіатура", "Мишка" };
        double[] prices = { 800, 600, 300 };
        int[] quantities = { 1, 2, 2 };

        for (int i = 0; i < names.Length; i++)
        {
            double sum = CalculateItemSum(prices[i], quantities[i]);
            double finalSum = ApplyProceduralDiscount(sum);

            Console.WriteLine("Товар: " + names[i]);
            Console.WriteLine("Сума: " + sum + " грн");

            if (sum > 500)
                Console.WriteLine("Знижка: 10%");
            else
                Console.WriteLine("Знижка: немає");

            Console.WriteLine("Після знижки: " + finalSum + " грн");
            Console.WriteLine();
        }

        double proceduralTotal =
            CalculateProceduralTotal(prices, quantities);

        Console.WriteLine("Підсумок процедурної версії: "+ proceduralTotal + " грн");


        Console.WriteLine();
        Console.WriteLine("             ООП ПІДХІД");
        Console.WriteLine();

        Product headphones = new Product("Навушники", 800);
        Product keyboard = new Product("Клавіатура", 600);
        Product mouse = new Product("Мишка", 300);

        Cart cart = new Cart();

        cart.AddItem(headphones, 1);
        cart.AddItem(keyboard, 2);
        cart.AddItem(mouse, 2);

        cart.ApplyDiscount();

        double oopTotal = cart.GetTotal();

        Console.WriteLine();
        Console.WriteLine("Підсумок ООП-версії: "+ oopTotal + " грн");


        Console.WriteLine();
        Console.WriteLine("           ПОРІВНЯННЯ РЕЗУЛЬТАТІВ");

        Console.WriteLine("Процедурна версія: " + proceduralTotal + " грн");
        Console.WriteLine("ООП-версія: " + oopTotal + " грн");

        if (proceduralTotal == oopTotal)
            Console.WriteLine("Результати однакові.");
        else
            Console.WriteLine("Результати відрізняються.");
    }
}