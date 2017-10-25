namespace Common
{
    using System;
    using Helpers;
    using static System.Console;

    public static class ConsoleInputProvider
    {
        public static int GetIntegerInput(string displayMessage, Func<int, bool> rangePredicate,
            Func<IntegerValidationResultType, string> getMessageOnFail)
        {
            IntegerValidationResult validationResult;
            do
            {
                Write(displayMessage);
                var input = ReadLine();
                WriteLine();
                validationResult = input.IsInteger(rangePredicate);
                if (!validationResult.IsValid)
                {
                    var message = getMessageOnFail(validationResult.Type);
                    WriteLine(message);
                }
            } while (!validationResult.IsValid);
            return validationResult.Value;
        }

        public static bool GetYesNoInput(string displayMessage)
        {
            YesNoValidationResult validationResult;
            do
            {
                Write(displayMessage);
                var input = ReadLine();
                WriteLine();
                validationResult = input.IsYesNo();
                if (!validationResult.IsValid)
                {
                    WriteLine("Please, enter 'yes' or 'no' ('y' or 'n').");
                }
            } while (!validationResult.IsValid);
            return validationResult.Type.ToBool();
        }
    }
}
