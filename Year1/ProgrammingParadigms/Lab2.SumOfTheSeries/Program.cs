namespace Lab2.SumOfTheSeries
{
    using Common;
    using System;

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
                WriteLine("---------------------");
            }
        }

        private static int GetInput(string displayMessage, Func<int, bool> predicate = null)
        {
            IntegerValidationResult validationResult;
            do
            {
                Write(displayMessage);
                var input = ReadLine();
                WriteLine();
                validationResult = input.IsInteger(predicate);
                if (!validationResult.IsValid)
                {
                    var message = GetValidationMessage(validationResult.Type);
                    WriteLine(message);
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

        private static string GetValidationMessage(IntegerValidationResultType validationResultType)
        {
            if (validationResultType == IntegerValidationResultType.EmptyInput)
                return "Please, enter some value!";

            if (validationResultType == IntegerValidationResultType.NotNumber)
                return "It is not a number!";

            if (validationResultType == IntegerValidationResultType.NotInteger)
                return "The number is too big!";

            if (validationResultType == IntegerValidationResultType.NotValidRange)
                return "Number should be in range [1, 30].";

            throw new ArgumentOutOfRangeException(nameof(validationResultType));
        }
    }
}
