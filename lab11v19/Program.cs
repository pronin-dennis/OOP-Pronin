using System;
using System.Collections.Generic;
using System.Linq;

namespace lab18vN
{
    class Employee
    {
        public int Id;
        public string Name;
        public double Salary;

        public Employee(int id, string name, double salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }
    }

    class Department
    {
        public int Id;
        public string Name;
        public List<Employee> Employees;

        public Department(int id, string name, List<Employee> employees)
        {
            Id = id;
            Name = name;
            Employees = employees;
        }
    }

    class Company
    {
        public int Id;
        public string Name;
        public List<Department> Departments;

        public Company(int id, string name, List<Department> departments)
        {
            Id = id;
            Name = name;
            Departments = departments;
        }
    }

    static class DepartmentExtensions
    {
        public static double AverageDepartmentSalary(this Department department)
        {
            if (department.Employees.Count == 0)
            {
                return 0;
            }

            double sum = 0;

            foreach (Employee employee in department.Employees)
            {
                sum += employee.Salary;
            }

            return sum / department.Employees.Count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Company> companies = new List<Company>()
            {
                new Company(1, "TechSoft", new List<Department>()
                {
                    new Department(1, "Розробка", new List<Employee>()
                    {
                        new Employee(1, "Олександр", 30000),
                        new Employee(2, "Андрій", 28000),
                        new Employee(3, "Максим", 32000)
                    }),

                    new Department(2, "Тестування", new List<Employee>()
                    {
                        new Employee(4, "Іван", 22000),
                        new Employee(5, "Дмитро", 24000)
                    }),

                    new Department(3, "Дизайн", new List<Employee>()
                    {
                        new Employee(6, "Марія", 26000),
                        new Employee(7, "Олена", 27000)
                    })
                }),

                new Company(2, "IT Solutions", new List<Department>()
                {
                    new Department(4, "Маркетинг", new List<Employee>()
                    {
                        new Employee(8, "Анна", 20000),
                        new Employee(9, "Софія", 23000)
                    }),

                    new Department(5, "Фінанси", new List<Employee>()
                    {
                        new Employee(10, "Петро", 25000),
                        new Employee(11, "Сергій", 27000)
                    }),

                    new Department(6, "Підтримка", new List<Employee>()
                    {
                        new Employee(12, "Віктор", 18000),
                        new Employee(13, "Тарас", 19000)
                    })
                }),

                new Company(3, "Digital Group", new List<Department>()
                {
                    new Department(7, "Аналітика", new List<Employee>()
                    {
                        new Employee(14, "Наталія", 29000),
                        new Employee(15, "Юлія", 31000)
                    }),

                    new Department(8, "Продажі", new List<Employee>()
                    {
                        new Employee(16, "Роман", 21000),
                        new Employee(17, "Богдан", 22000)
                    }),

                    new Department(9, "HR", new List<Employee>()
                    {
                        new Employee(18, "Катерина", 24000),
                        new Employee(19, "Ірина", 26000)
                    })
                })
            };

            Console.WriteLine("1. ВСІ ПРАЦІВНИКИ КОМПАНІЙ");

            List<Employee> allEmployees = companies
                .SelectMany(company => company.Departments)
                .SelectMany(department => department.Employees)
                .ToList();

            foreach (Employee employee in allEmployees)
            {
                Console.WriteLine(employee.Id + ". " + employee.Name +" - зарплата: " + employee.Salary);
            }

            Console.WriteLine("Всього працівників: " + allEmployees.Count);

            Console.WriteLine("\n2. ПАГІНАЦІЯ ВІДДІЛІВ");

            List<Department> allDepartments = companies
                .SelectMany(company => company.Departments)
                .ToList();

            int pageSize = 3;

            int totalPages = (int)Math.Ceiling(
                (double)allDepartments.Count / pageSize);

            for (int page = 0; page < totalPages; page++)
            {
                Console.WriteLine("\nСторінка " + (page + 1) + ":");

                List<Department> currentPage = allDepartments
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();

                foreach (Department department in currentPage)
                {
                    Console.WriteLine(department.Id + ". " + department.Name);
                }
            }

            Console.WriteLine("\n3. СЕРЕДНЯ ЗАРПЛАТА У ВІДДІЛАХ");

            foreach (Department department in allDepartments)
            {
                Console.WriteLine(department.Name + ": " +department.AverageDepartmentSalary());
            }
        }
    }
}
