/*
 * Дан файл f, компоненты которого являются действительными
 * числами. Найти:
 * а) сумму компонент файла f;
 * б) произведение компонент файла f
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4.LoadFile
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = "data.dat";
            const string dirPath = @"..\Lab4.LoadFile\temp";
            var filePath = Path.Combine(dirPath, fileName);

            EnsureRandomValuesFile(dirPath, fileName);
            var numbers = ReadValuesFromFile(filePath);

            var numbersSum = GetSum(numbers);
            var numbersProduct = GetProduct(numbers);

            Console.WriteLine($"The sum of numbers in the file is: {numbersSum.ToString(10)}");
            Console.WriteLine();
            Console.WriteLine($"The product of numbers in the file is: {numbersProduct.ToString(10)}");
        }

        /// <summary>
        /// Creates 100 (by default) random floating point numbers from 0 to 10000
        /// </summary>
        private static void EnsureRandomValuesFile(string dirPath, string fileName, int length = 100)
        {
            var filePath = Path.Combine(dirPath, fileName);
            if (File.Exists(filePath)) return;

            Directory.CreateDirectory(dirPath);

            var rand = new Random();

            using (var streamWriter = File.CreateText(filePath))
            {
                for (var i = 0; i < length; i++)
                {
                    var randomNumber = rand.NextDouble() * (rand.Next(10000) + 1);
                    streamWriter.WriteLine(randomNumber);
                }
            }
        }

        private static List<BigDecimal> ReadValuesFromFile(string filePath, int length = 100)
        {
            var values = new List<BigDecimal>(length);

            try
            {
                BigDecimal currentValue;
                using (var sr = new StreamReader(filePath))
                {
                    var counter = 0;
                    while (!sr.EndOfStream && counter < length)
                    {
                        var line = sr.ReadLine();
                        var isSuccess = BigDecimal.TryParse(line, out currentValue);
                        if (isSuccess) values.Add(currentValue);
                        counter++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return values;
        }

        private static BigDecimal GetSum(List<BigDecimal> numbers)
        {

            BigDecimal result = new BigDecimal(0);
            foreach (var number in numbers)
            {
                result += number;
            }

            return result;
        }

        private static BigDecimal GetProduct(List<BigDecimal> numbers)
        {
            BigDecimal result = new BigDecimal(1);
            foreach (var number in numbers)
            {
                result *= number;
            }

            return result;
        }
    }
}
