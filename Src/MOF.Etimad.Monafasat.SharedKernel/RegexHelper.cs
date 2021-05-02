namespace MOF.Etimad.Monafasat.SharedKernel
{
    public class RegexHelper
    {
        public const string ENGLISH_ONLY = @"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?\s]*$";

        public const string ARABIC_ONLY = @"^[\u0600-\u06FF\s\u0660-\u0669\s0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]*$";

        public const string POSITIVE_INT_ONLY = @"^[0-9]*[1-9][0-9]*$";

        public const string PERCENTAGE = @"(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9]{1,2})?$)";
        public const string NONZERO_DECIMAL = @"(^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$)";
    }
}
