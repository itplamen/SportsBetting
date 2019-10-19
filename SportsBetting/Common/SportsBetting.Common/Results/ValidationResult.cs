namespace SportsBetting.Common.Results
{
    public class ValidationResult
    {
        public ValidationResult(string errorKey, string errorMessage)
        {
            ErrorKey = errorKey;
            ErrorMessage = errorMessage;
        }

        public string ErrorKey { get; set; }

        public string ErrorMessage { get; set; }
    }
}