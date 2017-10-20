namespace Lab1.CountSpaces
{
    using System;

    internal class Program
    {
        static Program()
        {
            Console.WriteLine("Enter line to count number of spaces.");
        }

        private static void Main()
        {
            while (true)
            {
                try
                {
                    MainProgram();
                }
                catch
                {
                    Console.WriteLine("Something went wrong. Please, try again.");
                }
            }
        }

        private static void MainProgram()
        {
            var input = GetUserInput();
            var inputString = input.InputString;
            var tabSize = input.TabSize;

            var count = CountSpaces(inputString, tabSize);
            PrintResults(count);
        }

        private static UserInput GetUserInput()
        {
            var result = new UserInput();
            bool isValid = false;
            do
            {
                var line = Console.ReadLine();
                if (line == string.Empty)
                {
                    Console.WriteLine("There are no spaces in empty line :) Please, enter a line.");
                    continue;
                }
                result.InputString = line;
                isValid = true;
            } while (!isValid);

            return result;
        }

        private static int CountSpaces(string inputString, int TabSize)
        {
            var count = 0;
            foreach (var c in inputString)
            {
                if (c == ' ') count++;
                else if (c == '\t') count += TabSize;
            }

            return count;
        }

        private static void PrintResults(int count)
        {
            Console.WriteLine();
            Console.WriteLine($"The number of spaces is {count}");
        }
    }
}
