////9.1
//using System;

//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("Задача 9.1: Проверка арифметической прогрессии");
//        Console.Write("Введите количество элементов массива: ");
//        int n = int.Parse(Console.ReadLine());

//        int[] array = new int[n];
//        Console.WriteLine("Введите элементы массива:");
//        for (int i = 0; i < n; i++)
//        {
//            array[i] = int.Parse(Console.ReadLine());
//        }

//        (bool isProgression, int difference) = IsArithmeticProgression(array);

//        if (isProgression)
//        {
//            Console.WriteLine($"Да, это арифметическая прогрессия с разностью d = {difference}");
//        }
//        else
//        {
//            Console.WriteLine("Нет, это не арифметическая прогрессия");
//        }

//        Console.WriteLine();
//    }

//    static (bool, int) IsArithmeticProgression(int[] arr)
//    {
//        if (arr.Length <= 1)
//            return (true, 0); // По определению один или ноль элементов — прогрессия

//        if (arr.Length == 2)
//            return (true, arr[1] - arr[0]);

//        // Вычисляем разность на первом шаге
//        int d = arr[1] - arr[0];

//        // Проверяем остальные элементы
//        for (int i = 2; i < arr.Length; i++)
//        {
//            if (arr[i] - arr[i - 1] != d)
//                return (false, 0);
//        }

//        return (true, d);
//    }
//}

////9.2
//using System;
//using System.Collections.Generic;

//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("Задача 9.2: Делители числа");
//        Console.Write("Введите целое положительное число: ");
//        int number = int.Parse(Console.ReadLine());

//        if (number <= 0)
//        {
//            Console.WriteLine("Пожалуйста, введите положительное число.");
//            return;
//        }

//        List<int> divisors = FindDivisors(number);

//        Console.WriteLine($"Делители числа {number}:");
//        foreach (int divisor in divisors)
//        {
//            Console.Write(divisor + " ");
//        }
//        Console.WriteLine();

//        Console.WriteLine($"Количество делителей: {divisors.Count}");
//    }

//    static List<int> FindDivisors(int n)
//    {
//        List<int> divisors = new List<int>();

//        // Перебираем от 1 до sqrt(n) — это эффективнее, чем до n
//        for (int i = 1; i * i <= n; i++)
//        {
//            if (n % i == 0)
//            {
//                divisors.Add(i);
//                if (i != n / i) // Чтобы не добавить корень дважды
//                    divisors.Add(n / i);
//            }
//        }

//        divisors.Sort(); // Сортируем по возрастанию
//        return divisors;
//    }
//}

////9.3
//using System;

//public class Time : IComparable<Time>
//{
//    public int Hours { get; }
//    public int Minutes { get; }
//    public int Seconds { get; }

//    public Time(int hours = 0, int minutes = 0, int seconds = 0)
//    {
//        if (hours < 0 || hours > 23) throw new ArgumentException("Часы должны быть от 0 до 23");
//        if (minutes < 0 || minutes > 59) throw new ArgumentException("Минуты должны быть от 0 до 59");
//        if (seconds < 0 || seconds > 59) throw new ArgumentException("Секунды должны быть от 0 до 59");

//        Hours = hours;
//        Minutes = minutes;
//        Seconds = seconds;
//    }

//    // Сложение времени
//    public Time Add(Time other)
//    {
//        int totalSeconds = ToSeconds() + other.ToSeconds();
//        return FromSeconds(totalSeconds % 86400); // 86400 секунд в сутках
//    }

//    // Вычитание времени
//    public Time Subtract(Time other)
//    {
//        int totalSeconds = ToSeconds() - other.ToSeconds();
//        if (totalSeconds < 0) totalSeconds += 86400;
//        return FromSeconds(totalSeconds);
//    }

//    // Сравнение
//    public int CompareTo(Time other)
//    {
//        return ToSeconds().CompareTo(other.ToSeconds());
//    }

//    public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
//    public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
//    public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;
//    public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
//    public static bool operator ==(Time a, Time b) => a.CompareTo(b) == 0;
//    public static bool operator !=(Time a, Time b) => a.CompareTo(b) != 0;

//    // Форматированный вывод
//    public override string ToString()
//    {
//        return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
//    }

//    private int ToSeconds() => Hours * 3600 + Minutes * 60 + Seconds;

//    private static Time FromSeconds(int seconds)
//    {
//        int h = seconds / 3600;
//        seconds %= 3600;
//        int m = seconds / 60;
//        int s = seconds % 60;
//        return new Time(h, m, s);
//    }
//}

//// Пример использования
//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("Задача 9.3: Класс Time");
//        Time t1 = new Time(10, 30, 45);
//        Time t2 = new Time(2, 15, 20);

//        Console.WriteLine($"t1 = {t1}");
//        Console.WriteLine($"t2 = {t2}");
//        Console.WriteLine($"t1 + t2 = {t1.Add(t2)}");
//        Console.WriteLine($"t1 - t2 = {t1.Subtract(t2)}");
//        Console.WriteLine($"t1 > t2: {t1 > t2}");
//        Console.WriteLine();
//    }
//}

////9.4
//using System;

//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("Задача 9.4: Сортировка пузырьком с оптимизацией");

//        int[] array = { 64, 34, 25, 12, 22, 11, 90, 88, 45 };
//        Console.WriteLine("Исходный массив: " + string.Join(" ", array));

//        BubbleSortOptimized(array);

//        Console.WriteLine("Отсортированный массив: " + string.Join(" ", array));
//        Console.WriteLine();
//    }

//    static void BubbleSortOptimized(int[] arr)
//    {
//        int n = arr.Length;
//        bool swapped;

//        for (int i = 0; i < n - 1; i++)
//        {
//            swapped = false;
//            // Последние i элементов уже на месте
//            for (int j = 0; j < n - i - 1; j++)
//            {
//                if (arr[j] > arr[j + 1])
//                {
//                    // Обмен
//                    int temp = arr[j];
//                    arr[j] = arr[j + 1];
//                    arr[j + 1] = temp;
//                    swapped = true;
//                }
//            }

//            // Если не было перестановок — массив уже отсортирован
//            if (!swapped)
//                break;
//        }
//    }
//}

////9.5
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//public class Student
//{
//    public string Name { get; set; }
//    public int Age { get; set; }
//    public int Grade { get; set; }

//    public override string ToString()
//    {
//        return $"{Name,-10} {Age,3} {Grade,3}";
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("Задача 9.5: Обработка CSV");

//        string filePath = "students.csv"; // Файл должен быть в папке с exe

//        if (!File.Exists(filePath))
//        {
//            Console.WriteLine("Файл students.csv не найден. Создаю пример...");
//            CreateSampleCsv(filePath);
//        }

//        List<Student> students = ReadCsv(filePath);

//        Console.WriteLine("Исходные данные:");
//        PrintStudents(students);

//        // Сортировка по оценке по убыванию
//        students = students.OrderByDescending(s => s.Grade).ToList();

//        Console.WriteLine("\nОтсортировано по оценке (по убыванию):");
//        PrintStudents(students);
//    }

//    static List<Student> ReadCsv(string path)
//    {
//        var lines = File.ReadAllLines(path);
//        var students = new List<Student>();

//        // Пропускаем заголовок
//        for (int i = 1; i < lines.Length; i++)
//        {
//            var parts = lines[i].Split(',');
//            if (parts.Length == 3)
//            {
//                students.Add(new Student
//                {
//                    Name = parts[0].Trim(),
//                    Age = int.Parse(parts[1].Trim()),
//                    Grade = int.Parse(parts[2].Trim())
//                });
//            }
//        }

//        return students;
//    }

//    static void PrintStudents(List<Student> students)
//    {
//        Console.WriteLine($"{nameof(Student.Name),-10} {nameof(Student.Age),3} {nameof(Student.Grade),3}");
//        Console.WriteLine(new string('-', 20));
//        foreach (var s in students)
//        {
//            Console.WriteLine(s);
//        }
//    }

//    static void CreateSampleCsv(string path)
//    {
//        string[] content =
//        {
//            "Имя,Возраст,Оценка",
//            "Анна,20,85",
//            "Борис,19,92",
//            "Виктор,21,78",
//            "Галина,20,88"
//        };
//        File.WriteAllLines(path, content);
//    }
//}