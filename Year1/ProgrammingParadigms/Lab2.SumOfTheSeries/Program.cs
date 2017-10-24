﻿namespace Lab2.SumOfTheSeries
{
    using System;
    using System.Text.RegularExpressions;

    using static System.Console;
    using static System.Math;

    internal class Program
    {
        static Program()
        {
            WriteLine("Sum of the series from i to k: sqrt(m * (1 / i)) * sin(m * i) for k <= 30");
        }
        private static void Main()
        {
            const int maxK = 30;
            const int i = 1;
            bool KRangeValidator(int value) => value >= i && value <= maxK;
            double Formula(double mValue, int iValue) => Sqrt(mValue * (1.0 / iValue)) * Sin(mValue * iValue);
            while (true)
            {
                var k = GetInput("Enter the number k: ", KRangeValidator);
                var m = GetInput("Enter the number m: ");
                var result = CalculateSum(i, k, m, Formula);
                WriteLine($"The result of the sum is {result}");
                WriteLine();
            }
        }

        private static int GetInput(string displayMessage, Func<int, bool> predicate = null)
        {
            ValidationResult validationResult;
            do
            {
                Write(displayMessage);
                var input = ReadLine();
                WriteLine();
                validationResult = ValidateInput(input, predicate);
                if (!validationResult.IsValid)
                {
                    WriteLine(validationResult.Message);
                }
            } while (!validationResult.IsValid);
            return validationResult.Value;
        }

        private static double CalculateSum(int i, int k, int m, Func<double, int, double> formula)
        {
            var sum = 0d;
            for (var j = i; j <= k; j++)
            {
                sum += formula(m, j);
            }
            return sum;
        }

        private static ValidationResult ValidateInput(string input, Func<int, bool> predicate)
        {
            if (!Regex.IsMatch(input, @"^[-+]?\d+$"))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "It is not a number!"
                };
            }

            var isInteger = int.TryParse(input, out var number);
            if (!isInteger)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "The number is too big!"
                };
            }

            if (predicate != null && !predicate(number))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "Number should be in range [1, 30]."
                };
            }

            return new ValidationResult
            {
                IsValid = true,
                Value = number
            };
        }
    }
}