namespace Common
{
    public enum IntegerValidationResultType
    {
        ValidationResultNotSet = 0,
        ValidInteger = 1,
        EmptyInput = 2,
        NotNumber = 3,
        NotInteger = 4,
        NotValidRange = 5
    }

    public enum YesNoValidationResultType
    {
        ValidationResultNotSet = 0,
        Yes = 1,
        No = 2,
        ValidationResultInvalid = 3
    }
}
