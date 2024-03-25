using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

class Task1
{
    /*Объявить одномерный (5 элементов) массив с име-
нем A и двумерный массив (3 строки, 4 столбца) дроб-
ных чисел с именем B. Заполнить одномерный массив 
А числами, введенными с клавиатуры пользователем, а 
двумерный массив В случайными числами с помощью 
циклов. Вывести на экран значения массивов: массива 
А в одну строку, массива В — в виде матрицы. Найти в 
данных массивах общий максимальный элемент, мини-
мальный элемент, общую сумму всех элементов, общее 
произведение всех элементов, сумму четных элементов 
массива А, сумму нечетных столбцов массива В.*/
    static void Main()
    {
        double[] A = new double[5];
        Console.WriteLine("Введите 5 чисел для массива A:");
        for (int i = 0; i < 5; i++)
        {
            A[i] = Convert.ToDouble(Console.ReadLine());
        }

        Random random = new Random();
        double[,] B = new double[3, 4];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                B[i, j] = random.NextDouble() * 100;
            }
        }

        Console.WriteLine("Массив A:");
        foreach (var num in A)
        {
            Console.Write(num + " ");
        }

        Console.WriteLine("\nМассив B:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.Write(B[i, j] + "\t");
            }
            Console.WriteLine();
        }

        double maxA = A.Max();
        double minA = A.Min();
        double maxB = B.Cast<double>().Max();
        double minB = B.Cast<double>().Min();

        double sumA = A.Sum();
        double sumB = B.Cast<double>().Sum();

        double prodA = A.Aggregate((x, y) => x * y);
        double prodB = B.Cast<double>().Aggregate((x, y) => x * y);

        double sumEvenA = A.Where(x => x % 2 == 0).Sum();

        double sumOddColsB = 0;
        for (int j = 0; j < 4; j++)
        {
            double colSum = 0;
            for (int i = 0; i < 3; i++)
            {
                colSum += B[i, j];
            }
            if (j % 2 != 0)
            {
                sumOddColsB += colSum;
            }
        }

        Console.WriteLine($"Максимальный элемент в массиве A: {maxA}");
        Console.WriteLine($"Минимальный элемент в массиве A: {minA}");
        Console.WriteLine($"Максимальный элемент в массиве B: {maxB}");
        Console.WriteLine($"Минимальный элемент в массиве B: {minB}");
        Console.WriteLine($"Общая сумма всех элементов в массиве A: {sumA}");
        Console.WriteLine($"Общая сумма всех элементов в массиве B: {sumB}");
        Console.WriteLine($"Общее произведение всех элементов в массиве A: {prodA}");
        Console.WriteLine($"Общее произведение всех элементов в массиве B: {prodB}");
        Console.WriteLine($"Сумма четных элементов в массиве A: {sumEvenA}");
        Console.WriteLine($"Сумма нечетных столбцов в массиве B: {sumOddColsB}");

        Console.ReadKey();
    }

}

class Task2
{
    /*Дан двумерный массив размерностью 5×5, заполнен-
ный случайными числами из диапазона от –100 до 100. 
Определить сумму элементов массива, расположенных
между минимальным и максимальным элементами.*/
    static void Main()
    {
        int[,] array = new int[5, 5];
        Random random = new Random();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                array[i, j] = random.Next(-100, 100);
            }
        }

        Console.WriteLine("Исходный массив:");
        PrintArray(array);


        int min = array[0, 0];
        int max = array[0, 0];
        int minRow = 0, minCol = 0, maxRow = 0, maxCol = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (array[i, j] < min)
                {
                    min = array[i, j];
                    minRow = i;
                    minCol = j;
                }

                if (array[i, j] > max)
                {
                    max = array[i, j];
                    maxRow = i;
                    maxCol = j;
                }
            }
        }

        int sum = 0;

        int startRow = Math.Min(minRow, maxRow);
        int endRow = Math.Max(minRow, maxRow);
        int startCol = Math.Min(minCol, maxCol);
        int endCol = Math.Max(minCol, maxCol);

        for (int i = startRow; i <= endRow; i++)
        {
            for (int j = startCol; j <= endCol; j++)
            {
                if ((i == minRow && j >= minCol && j <= maxCol) || (i == maxRow && j >= minCol && j <= maxCol) || (i > minRow && i < maxRow))
                {
                    sum += array[i, j];
                }
            }
        }

        Console.WriteLine($"Минимальный элемент: {min}");
        Console.WriteLine($"Максимальный элемент: {max}");
        Console.WriteLine($"Сумма элементов между минимальным и максимальным элементами: {sum}");
        Console.ReadKey();
    }

    static void PrintArray(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}

class Task3
{
    /*Пользователь вводит строку с клавиатуры.Необходи-
мо зашифровать данную строку используя шифр Цезаря.
Из Википедии:
1
Шифр Цезаря — это вид шифра подстановки, в ко-
тором каждый символ в открытом тексте заменяется
символом, находящимся на некотором постоянном числе
позиций левее или правее него в алфавите.Например, 
в шифре со сдвигом вправо на 3, A была бы заменена на
D, B станет E, и так далее.
Подробнее тут: https://en.wikipedia.org/wiki/Caesar_
cipher.
Кроме механизма шифровки, реализуйте механизм
расшифрования.*/
    static void Main()
    {
        Console.WriteLine("Введите строку для шифрования:");
        string input = Console.ReadLine();

        Console.WriteLine("Введите сдвиг (целое число):");
        int shift = int.Parse(Console.ReadLine());

        string encryptedText = Encrypt(input, shift);
        Console.WriteLine($"Зашифрованная строка: {encryptedText}");

        string decryptedText = Decrypt(encryptedText, shift);
        Console.WriteLine($"Расшифрованная строка: {decryptedText}");

        Console.ReadKey();
    }

    static string Encrypt(string input, int shift)
    {
        string result = "";

        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                char shiftedChar = (char)(c + shift);

                if ((char.IsLower(c) && shiftedChar > 'z') || (char.IsUpper(c) && shiftedChar > 'Z'))
                {
                    shiftedChar = (char)(c - (26 - shift));
                }

                result += shiftedChar;
            }
            else
            {
                result += c;
            }
        }

        return result;
    }

    static string Decrypt(string input, int shift)
    {
        return Encrypt(input, 26 - shift);
    }
}


  
    class Task4
/* Создайте приложение, которое производит операции
над матрицами:
■ Умножение матрицы на число;
■ Сложение матриц;
■ Произведение матриц.*/
{
    static void Main(string[] args)
    {
        int[,] arr1 = new int[5, 5];
        int[,] arr2 = new int[5, 5];

        Random rand = new Random();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
                arr1[i, j] = rand.Next(100);
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
                arr2[i, j] = rand.Next(100);
        }

        Console.WriteLine("Массив 1:");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(arr1[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nМассив 2:");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(arr2[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nУмножим первый массив на 10:");

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write((arr1[i, j] * 10) + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nСумма первого и второго массивов:");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write((arr1[i, j] + arr2[i, j]) + "\t");

            }
            Console.WriteLine();
        }


        Console.WriteLine("\nПроизведение первого и второго массивов:");
        int[,] arr3 = new int[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    arr3[i, j] += arr1[i, k] * arr2[k, j];
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(arr3[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.ReadKey();
    }
}


class Task5
{
    /*Пользователь с клавиатуры вводит в строку ариф-
метическое выражение.Приложение должно посчитать
его результат. Необходимо поддерживать только две
операции: + и –.*/
    static void Main()
    {
        Console.WriteLine("Введите арифметическое выражение :");
        string input = Console.ReadLine();

        input = input.Replace(" ", "");

        int result = CalculateExpression(input);

        Console.WriteLine("Результат: " + result);
        Console.ReadLine();
    }

    static int CalculateExpression(string expression)
    {
        int result = 0;
        char operation = '+';
        int num = 0;

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (char.IsDigit(c))
            {
                num = num * 10 + (c - '0');
            }
            if (!char.IsDigit(c) || i == expression.Length - 1)
            {
                if (operation == '+')
                {
                    result += num;
                }
                else if (operation == '-')
                {
                    result -= num;
                }

                if (c == '+' || c == '-')
                {
                    operation = c;
                    num = 0;
                }
            }
        }

        return result;
    }
}

class Task6
{
    /* Пользователь с клавиатуры вводит некоторый текст.
    Приложение должно изменять регистр первой буквы
    каждого предложения на букву в верхнем регистре.
    Например, если пользователь ввёл: «today is a good
    day for walking.i will try to walk near the sea».
    Результат работы приложения: «Today is a good day
    for walking.I will try to walk near the sea».*/
    static void Main()
    {
        Console.WriteLine("Введите текст:");
        string str = Console.ReadLine();

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(str[0].ToString().ToUpper());
        for (int i = 1; i < str.Length; i++)
        {
            if (char.IsLetter(str[i]) && char.IsWhiteSpace(str[i - 1]) && ".!?".IndexOf(str[i - 2]) != -1)
            {
                stringBuilder.Append(str[i].ToString().ToUpper());
            }
            else
            {
                stringBuilder.Append(str[i]);
            }
        }
        Console.WriteLine(stringBuilder.ToString());
        Console.ReadKey();
    }
}
  
    class Task7
{
    /*Создайте приложение, проверяющее текст на недо-
пустимые слова. Если недопустимое слово найдено, оно 
должно быть заменено на набор символов *.*/
    static void Main(string[] args)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string replacement1 = "***";
            string[] str = "to be, or not to be, that is the question, \nor to take arms against a sea of troubles, \nand by opposing end them? to die: to sleep;\nno more; and by a sleep to say we end \ndevoutly to be wish'd. to die, to sleep".Split(' ');
            for (int i = 0; i < str.Length; i++)
            {
                if (ValidCharsFound(str[i]))
                {
                    Console.WriteLine(str[i] + " - ok");
                    string rep = str[i].Replace(str[i], replacement1);
                    str[i] = rep;
                }

                else
                    Console.WriteLine(str[i] + " - no");

                stringBuilder.Append(" " + str[i]);
            }
            Console.WriteLine(stringBuilder.ToString().TrimStart());
            Console.ReadKey();

        }
        static bool ValidCharsFound(string str)
        {
            return Regex.IsMatch(str, "die");
        }
    }
