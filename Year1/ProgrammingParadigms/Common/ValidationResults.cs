using System;

namespace Common
{
    public class IntegerValidationResult
    {
        public bool IsValid => Type == IntegerValidationResultType.ValidInteger;
        public IntegerValidationResultType Type { get; internal set; }

        private int value;
        public int Value
        {
            get
            {
                if (!IsValid) throw new InvalidOperationException("Check for validity first by 'IsValid' property.");
                return value;
            }
            internal set => this.value = value;
        }
    }

    public class YesNoValidationResult
    {
        public bool IsValid => Type == YesNoValidationResultType.Yes || Type == YesNoValidationResultType.No;
        public YesNoValidationResultType Type { get; internal set; }
    }
}
