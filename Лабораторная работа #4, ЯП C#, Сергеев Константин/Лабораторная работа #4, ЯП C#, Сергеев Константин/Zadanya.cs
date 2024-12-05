using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LabNumber4
{
    public static class Tasks
    {
        // Вариант 14, задание 1. List ->  Оставить в списке только первые вхождения одинаковых элементов.
        public static void Task1()
        {
            Console.WriteLine("Задание 1 -> удаление повторяющихся элементов.");
            Console.WriteLine("Введите числа через пробел:");
            string input = Console.ReadLine();

            try
            {
                List<int> numbers = PreobrazovanieChisel(input);
                Console.WriteLine("Первоначальный список: " + string.Join(", ", numbers));
                List<int> result = RemovePovtori(numbers);
                Console.WriteLine("Новый список без повторов одинаковых элементов: " + string.Join(", ", result));
            }
            catch (Exception ex) {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        // метод для преобразования ввода пользователя в список чисел
        private static List<int> PreobrazovanieChisel(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                // если строка пустая, то выбрасываем исключение
                throw new ArgumentException("Ввод не должен быть пустым.");
            }
            string[] parts = input.Split(' ');
            // создаём список
            List<int> numbers = new List<int>();

            foreach (var part in parts)
            {
                try
                {
                    if (int.TryParse(part, out int num)) // преобразуем строку в число
                    {
                        numbers.Add(num); // добавляем число в список
                    }
                    else
                    {
                        // если не получилось преобразовать, то выбрасываем исключение
                        throw new ArgumentException($"Некорректное значение: {part}, пропускаем его");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return numbers;
        }
        // метод для удаления повторяющихся элементов из списка
        private static List<int> RemovePovtori(List<int> inputList)
        {
            if (inputList == null || inputList.Count == 0)
            {
                // если список пустой или отсутствует, то выбрасывается исключение
                throw new ArgumentException("Список не должен быть пустым.");
            }
            // создаём новый список для хранения уникальных элементов
            List<int> result = new List<int>();
            // перебирарем каждый элемент входного списка
            foreach (var item in inputList)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
        // Вариант 14, задание 2 -> Если у элемента со значением E "соседи" не равны, поменять их местами.
        public static void Task2()
        {
            Console.WriteLine("Задание 2 -> смена неравных соседей со значением E");
            Console.WriteLine("Введите элементы списка через пробел:");
            string input = Console.ReadLine();

            try
            {
                LinkedList<int> numbers = PreobrazovanieChisel2(input);
                Console.WriteLine("Первоначальный список: " + string.Join(",", numbers));
                Console.WriteLine("Введите значение E для сравнения соседей:");
                int E = int.Parse(Console.ReadLine());

                SmenaSosedey(numbers, E);
                Console.WriteLine("Новый список после необходимых изменений: " + string.Join(",", numbers));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        // метод для преобразования ввода пользователя
        private static LinkedList<int> PreobrazovanieChisel2(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Ввод не должен быть пустым.");
            }

            string[] parts = input.Split(' ');
            LinkedList<int> numbers = new LinkedList<int>();
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int num))
                {
                    numbers.AddLast(num); // если получилось преобразавать строку в число, то добавляем элемент в конце списка
                }
                else
                {
                    throw new ArgumentException($"Некорректное значение: {part}");
                }
            }
            return numbers;
        }
        // метод для замены соседей у элементов со значением E, если их значения не равны
        private static void SmenaSosedey(LinkedList<int> numbers, int E)
        {
            // проверим, есть ли вообще значение E в нашем списке
            bool found = false;
            foreach (var value in numbers)
            {
                if (value == E)
                {
                    found = true;
                    break;
                }
            }
            // Если значение не найдено, то выведем соответсвующее сообщение
            if (!found)
            {
                Console.WriteLine($"Значение {E} отсутствует в списке, пожалуйста, введите существующее значение.");
                return;
            }
            // если же значение найдено, то продолжаем выполнение задачи
            LinkedListNode<int> currentNode = numbers.First;
            // проходим по всему списку, пока текущий узел не null
            while (currentNode != null)
            {
                // проверяем равен ли текущий узел значению E
                if (currentNode.Value == E)
                {
                    // проверяем есть ли у него соседи
                    if (currentNode.Previous != null && currentNode.Next != null)
                    {
                        // проверяем являются ли соседи неравными, если да, то меняем местами
                        if (currentNode.Previous.Value != currentNode.Next.Value)
                        {
                            // сохраняем значение левого соседа в переменную Save
                            int Save = currentNode.Previous.Value;
                            // записываем значение правого соседа в левого
                            currentNode.Previous.Value = currentNode.Next.Value;
                            // значение правого соседа приравниваем к значению из переменной, где хранится значение левого соседа
                            currentNode.Next.Value = Save;
                        }
                    }
                }
                // Переходим к следующему узлу
                currentNode = currentNode.Next;
            }
        }
        /* Вариант 14, задание 3 -> Есть перечень иностранных языков. Работники фирмы могут знать некоторые из них. Для
        каждого работника известно, какие языки он знает.Определить для каждого языка, какие из них
        знает каждый из работников, какие — хотя бы один из работников, и какие — никто из
        работников. */
        public static void Task3()
        {
            Console.WriteLine("Задание 3 -> Знание иностранных языков.");
            // ввод всех доступных языков
            Console.WriteLine("Введите доступные языки (через запятую):");
            string[] LanguagesArray = Console.ReadLine().Split(',');
            HashSet<string> Languages = new HashSet<string>();
            // перебираем все введённые языки
            foreach (var language in LanguagesArray)
            {
                // убираем лишние пробелы
                string probelLanguage = language.Trim();
                if (NormalLanguage(probelLanguage))
                {
                    // добавляем язык в коллекцию
                    Languages.Add(probelLanguage.ToLower()); // игнорируем регистр
                }
                else
                {
                    // если язык некорректный, то пишем об этом
                    Console.WriteLine($"Некорректный язык: {language}. Пожалуйста, введите только буквы.");
                    return;
                }
            }
            // ввод количества работников
            Console.WriteLine("Введите количество работников:");
            int NumRabotnikov;
            while (!int.TryParse(Console.ReadLine(), out NumRabotnikov) || NumRabotnikov <= 0)
            {
                Console.WriteLine("Некорректное количество работников. Пожалуйста, введите положительное число.");
            }
            // список для хранения языков работников
            List<HashSet<string>> RabotLanguage = new List<HashSet<string>>();
            // перебираем каждого работника
            for (int i = 0; i < NumRabotnikov; i++)
            {
                Console.WriteLine($"Введите все языки, которые знает работник {i + 1} (через запятую):");
                string[] RabotLanguagesArray = Console.ReadLine().Split(',');
                HashSet<string> RabotLanguages = new HashSet<string>();
                foreach (var language in RabotLanguagesArray)
                {
                    string probelLanguage = language.Trim();
                    if (NormalLanguage(probelLanguage))
                    {
                        RabotLanguages.Add(probelLanguage.ToLower()); // игнорируем регистр
                    }
                    else
                    {
                        Console.WriteLine($"Некорректный язык: {language}. Пожалуйста, введите только буквы.");
                        return;
                    }
                }
                RabotLanguage.Add(RabotLanguages);
            }
            // Языки, которые знают все работники
            HashSet<string> ZnaytVse = new HashSet<string>(Languages);
            foreach (var RabotLanguages in RabotLanguage)
            {
                ZnaytVse.IntersectWith(RabotLanguages); // InserectWith - пересечение. Оставляем только те языки, которые есть у всех работников
            }
            Console.WriteLine("Языки, которые знают все работники:");
            Console.WriteLine(string.Join(",", ZnaytVse));

            // Языки, которые знает хотя бы один работник
            HashSet<string> ZnaetKtoTo = new HashSet<string>();
            foreach (var RabotLanguages in RabotLanguage)
            {
                ZnaetKtoTo.UnionWith(RabotLanguages); // UnionWith - объеденение
            }
            Console.WriteLine("Языки, которые знает хотя бы один работник: ");
            Console.WriteLine(string.Join(",", ZnaetKtoTo));

            // Языки, которые не знает ни один из работников
            HashSet<string> NiktoNeZnaet = new HashSet<string>(Languages);
            foreach (var RabotLanguages in RabotLanguage)
            {
                NiktoNeZnaet.ExceptWith(RabotLanguages); // ExceptWith - исключение, удаляем все элементы, которые есть в другом множетсве. 
            }
            Console.WriteLine("Языки, которые не знает ни один работник:");
            Console.WriteLine(string.Join(",", NiktoNeZnaet));
        }
        // метод для проверки, является ли строка языком. 
        private static bool NormalLanguage(string language)
        {
            return Regex.IsMatch(language, @"^[a-zA-Zа-яА-ЯёЁ]+$"); // проверка на наличие только БУКВ в языке. "^" - начало строки "$" - конец строки "+" - строка состоит из 1 более букв. 
        }
        // Вариант 14, задание 4 -> Файл содержит текст на русском языке. С каких букв начинаются слова?
        public static void Task4()
        {
            Console.WriteLine("Задание 4 -> с каких букв начинаются слова в текстовом файле:");
            try
            {
                // путь к файлу
                string filePath = "text.txt";
                // проверяем существует ли файл
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Файл не найден, попробуйте ещё раз.");
                    return;
                }
                // читаем текст из файла
                string text = File.ReadAllText(filePath);
                text = text.ToLower();

                string[] words = text.Split(new[] { ' ', '\n', '\t', '.', ',', ';', '!', '?', '-', '(', ')', '"' }, StringSplitOptions.RemoveEmptyEntries);

                // Создаём HashSet для хранения уникальных букв
                HashSet<char> NachalBuckv = new HashSet<char>();
                // Проходим по каждому слову и добавляем первую букву в HashSet
                foreach (string word in words)
                {
                    if (word.Length > 0 && RussianBuckva(word[0]))
                    {
                        NachalBuckv.Add(word[0]);
                    }
                }
                if (NachalBuckv.Count > 0)
                {
                    Console.WriteLine("Уникальные буквы, с которых начинаются слова:");
                    foreach (char buckva in NachalBuckv)
                    {
                        Console.WriteLine(buckva);
                    }
                }
                else
                {
                    Console.WriteLine("Извините, учитываются слова только на русском языке. Проверьте ваш текстовый файл и попробуйте ещё раз.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        private static bool RussianBuckva(char c)
        {
            return (c >= 'а' && c <= 'я') || c == 'ё';
        }
        public static void Task5()
        {
            Console.WriteLine("Задание 5: ");
        }
    }
}