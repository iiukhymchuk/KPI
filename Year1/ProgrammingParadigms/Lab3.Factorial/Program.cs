using System.Diagnostics;
using System.Numerics;

namespace Lab3.Factorial
{
    using System;
    using Common;

    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                var number = ConsoleInputProvider.GetIntegerInput("Enter a number to count it`s factorial: ",
                    (i) => i >= 1, GetValidationMessage);

                var isYesInput = ConsoleInputProvider.GetYesNoInput("Enable recursive method? (y/n) ");

                if (number > 5000 && isYesInput)
                {
                    Console.WriteLine($"Recursive method may not handle calculating of {number}!, please, choose another method or smaller number!");
                    continue;
                }

                if (number > 50000 && !isYesInput)
                {
                    Console.WriteLine($"Iterative method may take too long to calculate {number}!, please, choose another method or smaller number!");
                    continue;
                }

                var factorial = isYesInput ? Recursive(number) : NonRecursive(number);

                Console.WriteLine($"Factorial of the number {number} is {factorial}.");
                Console.WriteLine("-------------------");
            }
        }

        private static BigInteger Recursive(int number)
        {
            if (number == 1)
                return 1;

            return Recursive(number - 1) * number;
        }

        private static BigInteger NonRecursive(int number)
        {
            var bigNumber = (BigInteger)number;
            var result = BigInteger.One;
            for (var i = bigNumber; i > 1; i--)
            {
                result *= i;
            }
            return result;
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
                return "Number should be a natural number!";

            throw new ArgumentOutOfRangeException(nameof(validationResultType));
        }
    }
}
