namespace SportsBetting.Common.Validation
{
    public class ValidationResult
    {
        public ValidationResult(string errorKey, string errorMessage)
        {
            ErrorKey = errorKey;
            ErrorMessage = errorMessage;
        }

        public ValidationResult()
        {
        }

        public string ErrorKey { get; set; }

        public string ErrorMessage { get; set; }

        public bool HasErrors => !string.IsNullOrEmpty(ErrorKey) && !string.IsNullOrEmpty(ErrorMessage);
    }
}