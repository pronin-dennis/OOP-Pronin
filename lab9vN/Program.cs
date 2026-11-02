using System;
using System.Collections.Generic;


internal class Program
{
    static void AddReportType(HashSet<string> types, string type)
    {
        if (types.Add(type))
        {
            Console.WriteLine("Додано тип звіту: " + type);
        }
        else
        {
            Console.WriteLine("Такий тип звіту вже існує: " + type);
        }
    }
    static void AddReport(Queue<string> queue, string report)
    {
        queue.Enqueue(report);
        Console.WriteLine("Звіт додано до черги: " + report);
    }


    static void GenerateReports(Queue<string> queue, Stack<string> history)
    {
        Console.WriteLine("\nГенерація звітів:");


        while (queue.Count > 0)
        {
            string report = queue.Dequeue();


            Console.WriteLine("Згенеровано: " + report);


            history.Push(report);
        }
    }


    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("HASHSET: Типи звітів");


        HashSet<string> reportTypes = new HashSet<string>();


        AddReportType(reportTypes, "Фінансовий");
        AddReportType(reportTypes, "Продажів");
        AddReportType(reportTypes, "Технічний");
        AddReportType(reportTypes, "Фінансовий");


        Console.WriteLine("\nКількість унікальних типів: " + reportTypes.Count);


        Console.WriteLine("Перевірка наявності типу 'Продажів': " + reportTypes.Contains("Продажів"));


        reportTypes.Remove("Технічний");


        Console.WriteLine("Після видалення технічного звіту:");


        foreach (string type in reportTypes)
        {
            Console.WriteLine(type);
        }


        Console.WriteLine("\nQUEUE: Черга звітів");


        Queue<string> reportQueue = new Queue<string>();


        AddReport(reportQueue, "Звіт за січень");
        AddReport(reportQueue, "Звіт за лютий");
        AddReport(reportQueue, "Звіт за березень");


        Console.WriteLine("\nПерший звіт у черзі: " + reportQueue.Peek());


        Console.WriteLine("\nSTACK: Історія звітів");


        Stack<string> reportHistory = new Stack<string>();


        GenerateReports(reportQueue, reportHistory);


        Console.WriteLine("\nОстанній згенерований звіт: " + reportHistory.Peek());
        Console.WriteLine("\nІсторія звітів (від найновішого до найстарішого):");


        foreach (string report in reportHistory)
        {
            Console.WriteLine(report);
        }


        Console.WriteLine("\nВидалення останнього звіту з історії:");


        string lastReport = reportHistory.Pop();


        Console.WriteLine("Видалено: " + lastReport);


        Console.WriteLine("Звіти, що залишилися в історії:");


        foreach (string report in reportHistory)
        {
            Console.WriteLine(report);
        }


        Console.WriteLine("\nРоботу програми завершено.");
    }
}
