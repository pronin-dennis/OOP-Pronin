using System;
using System.Collections.Generic;

namespace lab20vN
{
    class UserProfile
    {
        public int Id;
        public string Name;
        public string Email;
        public int Age;

        public UserProfile(int id, string name, string email, int age)
        {
            Id = id;
            Name = name;
            Email = email;
            Age = age;
        }
    }

    class InvalidUserDataException : Exception
    {
        public string Field { get; }
        public string Value { get; }

        public InvalidUserDataException(string field, string value)
            : base("Некоректне значення поля: " + field)
        {
            Field = field;
            Value = value;
        }

        public InvalidUserDataException(string message)
            : base(message)
        {
        }

        public InvalidUserDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }

        private Result(T value, bool isSuccess, string error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value, true, null);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result<T> Failure(Exception ex)
        {
            return new Result<T>(default(T), false, ex.Message);
        }
    }

    class UserProfileService
    {
        private List<UserProfile> users = new List<UserProfile>();

        public UserProfileService()
        {
            users.Add(new UserProfile(1, "Олександр", "alex@gmail.com", 20));
            users.Add(new UserProfile(2, "Андрій", "andrii@gmail.com", 22));
            users.Add(new UserProfile(3, "Максим", "max@gmail.com", 19));
        }

        public void UpdateProfile(UserProfile profile)
        {
            if (profile == null)
            {
                throw new InvalidUserDataException("Profile", "null");
            }

            if (string.IsNullOrWhiteSpace(profile.Name))
            {
                throw new InvalidUserDataException("Name", profile.Name);
            }

            if (profile.Age < 14 || profile.Age > 100)
            {
                throw new InvalidUserDataException("Age", profile.Age.ToString());
            }

            if (string.IsNullOrWhiteSpace(profile.Email) || !profile.Email.Contains("@") || !profile.Email.Contains("."))
            {
                throw new InvalidUserDataException("Email", profile.Email);
            }

            Console.WriteLine("Профіль користувача успішно оновлено.");
        }

        public Result<bool> ChangeEmail(int userId, string newEmail)
        {
            UserProfile user = users.Find(u => u.Id == userId);

            if (user == null)
            {
                return Result<bool>.Failure("Користувача з таким ID не знайдено.");
            }

            if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@") || !newEmail.Contains("."))
            {
                return Result<bool>.Failure("Введено некоректну електронну пошту.");
            }

            foreach (UserProfile item in users)
            {
                if (item.Email == newEmail && item.Id != userId)
                {
                    return Result<bool>.Failure("Ця електронна пошта вже використовується.");
                }
            }

            user.Email = newEmail;

            return Result<bool>.Success(true);
        }

        public void ShowUsers()
        {
            Console.WriteLine("\nСписок користувачів:");

            foreach (UserProfile user in users)
            {
                Console.WriteLine(user.Id + ". " + user.Name + " | " + user.Email + " | Вік: " + user.Age);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            UserProfileService service = new UserProfileService();

            Console.WriteLine("СПИСОК КОРИСТУВАЧІВ");

            service.ShowUsers();

            Console.WriteLine("\nОНОВЛЕННЯ ПРОФІЛЮ");

            UserProfile validProfile = new UserProfile(4, "Дмитро", "dmytro@gmail.com", 21);

            try
            {
                service.UpdateProfile(validProfile);

                UserProfile invalidProfile = new UserProfile(5, "", "invalid@gmail.com", 20);

                service.UpdateProfile(invalidProfile);
            }
            catch (InvalidUserDataException ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
                Console.WriteLine("Поле: " + ex.Field);
                Console.WriteLine("Значення: " + ex.Value);
            }

            Console.WriteLine("\nЗМІНА EMAIL");

            Result<bool> result1 = service.ChangeEmail(1, "newalex@gmail.com");

            if (result1.IsSuccess)
            {
                Console.WriteLine("Email успішно змінено.");
            }
            else
            {
                Console.WriteLine("Помилка: " + result1.Error);
            }

            Console.WriteLine("\nПЕРЕВІРКА ЗАЙНЯТОЇ ПОШТИ");

            Result<bool> result2 = service.ChangeEmail(1, "andrii@gmail.com");

            if (result2.IsSuccess)
            {
                Console.WriteLine("Email успішно змінено.");
            }
            else
            {
                Console.WriteLine("Помилка: " + result2.Error);
            }

            Console.WriteLine("\nПЕРЕВІРКА ID КОРИСТУВАЧА");

            Result<bool> result3 = service.ChangeEmail(10, "test@gmail.com");

            if (result3.IsSuccess)
            {
                Console.WriteLine("Email успішно змінено.");
            }
            else
            {
                Console.WriteLine("Помилка: " + result3.Error);
            }

            Console.WriteLine("\nОНОВЛЕНІ ДАНІ");

            service.ShowUsers();
        }
    }
}