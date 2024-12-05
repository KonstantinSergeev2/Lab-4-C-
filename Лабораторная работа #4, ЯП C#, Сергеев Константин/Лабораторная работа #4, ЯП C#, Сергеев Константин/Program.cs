using System;

namespace LabNumber4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите задание от 1 до 5. Число 0 для выхода из программы:");
                Console.WriteLine("1. Задание 1 -> удаление повторяющихся элементов");
                Console.WriteLine("2. Задание 2 -> смена соседей");
                Console.WriteLine("3. Задание 3 -> Знание иностранных языков");
                Console.WriteLine("4. Задание 4 -> с каких букв начинаются слова в текстовом файле:");
                Console.WriteLine("5. Задание 5");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите задание: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Tasks.Task1();
                        break;
                    case "2":
                        Tasks.Task2(); 
                        break;
                    case "3":
                        Tasks.Task3();
                        break;
                    case "4":
                        Tasks.Task4();
                        break;
                    case "5":
                        Tasks.Task5();
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Сделайте выбор  ещё раз.");
                        break;
                }
            }
        }
    }
}