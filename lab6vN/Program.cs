using System;
using System.Collections.Generic;


namespace Lab19
{
    interface IAction
    {
        void Perform();
        string Description { get; }
    }


    class LogAction : IAction
    {
        public string Description { get; }


        public LogAction(string description)
        {
            Description = description;
        }


        public void Perform()
        {
            Console.WriteLine("Запис у журнал: " + Description);
        }
    }


    class NotifyAction : IAction
    {
        public string Description { get; }


        public NotifyAction(string description)
        {
            Description = description;
        }


        public void Perform()
        {
            Console.WriteLine("Повідомлення: " + Description);
        }
    }


    class BackupAction : IAction
    {
        public string Description { get; }


        public BackupAction(string description)
        {
            Description = description;
        }


        public void Perform()
        {
            Console.WriteLine("Створення резервної копії: " + Description);
        }
    }


    abstract class ActionScheduler
    {
        public abstract void AddAction(IAction action);


        public abstract void RunAllActions();


        public void LogActionExecution(IAction action)
        {
            Console.WriteLine("Виконується дія: " + action.Description);
        }
    }


    class SimpleActionScheduler : ActionScheduler
    {
        private List<IAction> actions = new List<IAction>();


        public override void AddAction(IAction action)
        {
            actions.Add(action);
        }


        public override void RunAllActions()
        {
            foreach (IAction action in actions)
            {
                LogActionExecution(action);
                action.Perform();
            }
        }
    }
    class ImmediateActionScheduler : ActionScheduler
    {
        public override void AddAction(IAction action)
        {
            LogActionExecution(action);
            action.Perform();
        }
        public override void RunAllActions()
        {
            Console.WriteLine("Усі дії виконуються одразу після додавання.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Реалізація інтерфейсу IAction");


            List<IAction> actions = new List<IAction>();


            actions.Add(new LogAction("Програма запущена"));
            actions.Add(new NotifyAction("Завдання виконано"));
            actions.Add(new BackupAction("Резервна копія створена"));


            foreach (IAction action in actions)
            {
                Console.WriteLine("Опис: " + action.Description);
                action.Perform();
            }


            Console.WriteLine("\nРеалізація ActionScheduler");


            List<ActionScheduler> schedulers =new List<ActionScheduler>();


            SimpleActionScheduler simpleScheduler =new SimpleActionScheduler();


            simpleScheduler.AddAction(new LogAction("Запуск системи"));


            simpleScheduler.AddAction(new NotifyAction("Нове повідомлення"));


            schedulers.Add(simpleScheduler);
            schedulers.Add(new ImmediateActionScheduler());


            foreach (ActionScheduler scheduler in schedulers)
            {
                scheduler.RunAllActions();
            }


            Console.WriteLine("\nВиконання дій одразу");


            ImmediateActionScheduler immediateScheduler = new ImmediateActionScheduler();


            immediateScheduler.AddAction(new BackupAction("Копіювання файлів"));


            Console.ReadKey();
        }
    }
}
