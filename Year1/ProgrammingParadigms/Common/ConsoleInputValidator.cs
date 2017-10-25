namespace Common
{
    using System;
    using System.Text.RegularExpressions;

    internal static class ConsoleInputValidator
    {
        internal static IntegerValidationResult IsInteger(this string input, Func<int, bool> rangePredicate)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new IntegerValidationResult { Type = IntegerValidationResultType.EmptyInput };

            if (!Regex.IsMatch(input, @"^[-+]?\d+$"))
                return new IntegerValidationResult { Type = IntegerValidationResultType.NotNumber };

            var isInteger = int.TryParse(input, out var number);
            if (!isInteger)
                return new IntegerValidationResult { Type = IntegerValidationResultType.NotInteger };

            if (!rangePredicate(number))
                return new IntegerValidationResult { Type = IntegerValidationResultType.NotValidRange };

            return new IntegerValidationResult
            {
                Type = IntegerValidationResultType.ValidInteger,
                Value = number
            };
        }

        internal static YesNoValidationResult IsYesNo(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new YesNoValidationResult { Type = YesNoValidationResultType.ValidationResultInvalid };

            if (input.ToLower() == "y" || input.ToLower() == "yes")
                return new YesNoValidationResult { Type = YesNoValidationResultType.Yes };

            if (input.ToLower() == "n" || input.ToLower() == "no")
                return new YesNoValidationResult { Type = YesNoValidationResultType.No };

            return new YesNoValidationResult { Type = YesNoValidationResultType.ValidationResultInvalid };

        }
    }
}
