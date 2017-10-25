using System;

namespace Common.Helpers
{
    internal static class YesNoValidationResultTypeHelper
    {
        internal static bool ToBool(this YesNoValidationResultType @enum)
        {
            if (@enum == YesNoValidationResultType.Yes) return true;
            if (@enum == YesNoValidationResultType.No) return false;

            throw new InvalidOperationException();
        }
    }
}
