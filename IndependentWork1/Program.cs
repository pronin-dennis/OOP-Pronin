using System;

class Student
{
    private string _name;
    private int _age;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void PrintInfo()
    {
        Console.WriteLine("Ім'я студента: " + Name);
        Console.WriteLine("Вік студента: " + Age);
    }
}
class Book
{
    private string _title;
    private string _author;

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public string Author
    {
        get { return _author; }
        set { _author = value; }
    }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public void PrintInfo()
    {
        Console.WriteLine("Назва книги: " + Title);
        Console.WriteLine("Автор книги: " + Author);
    }
}
class Car
{
    private string _brand;
    private double _speed;

    public string Brand
    {
        get { return _brand; }
        set { _brand = value; }
    }

    public double Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public Car(string brand, double speed)
    {
        Brand = brand;
        Speed = speed;
    }

    public void CheckSpeed()
    {
        Console.WriteLine("Марка автомобіля: " + Brand);
        Console.WriteLine("Швидкість: " + Speed + " км/год");

        if (Speed > 100)
            Console.WriteLine("Автомобіль рухається швидко.");
        else
            Console.WriteLine("Автомобіль рухається повільно.");
    }
}
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Student student = new Student("Денис", 18);
        Console.WriteLine("Інформація про студента:");
        student.PrintInfo();

        Console.WriteLine();

        Book book = new Book("Кобзар", "Тарас Шевченко");
        Console.WriteLine("Інформація про книгу:");
        book.PrintInfo();

        Console.WriteLine();

        Car car = new Car("BMW", 120);
        Console.WriteLine("Інформація про автомобіль:");
        car.CheckSpeed();

        Console.WriteLine("END OF PROGRAM");
    }
}