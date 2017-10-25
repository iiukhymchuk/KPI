namespace Common
{
    using System;
    using System.Text.RegularExpressions;

    public static class ConsoleInputValidator
    {
        public static IntegerValidationResult IsInteger(this string input, Func<int, bool> rangePredicate)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new IntegerValidationResult { Type = IntegerValidationResultType.EmptyInput };

            if (!Regex.IsMatch(input, @"^[-+]?\d+$"))
                return new IntegerValidationResult { Type = IntegerValidationResultType.NotNumber };

            var isInteger = int.TryParse(input, out var number);
            if (!isInteger)
                return new IntegerValidationResult { Type = IntegerValidationResultType.NotInteger };

            if (rangePredicate != null && !rangePredicate(number))
                return new IntegerValidationResult { Type = IntegerValidationResultType.NotValidRange };

            return new IntegerValidationResult
            {
                Type = IntegerValidationResultType.ValidInteger,
                Value = number
            };
        }
    }
}
