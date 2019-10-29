namespace SportsBetting.Server.Api.Models.Common
{
    using System.ComponentModel.DataAnnotations;

    using SportsBetting.Common.Constants;

    public abstract class AccountRequestModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(
             AccountValidationConstants.USERNAME_MAX_LENGTH,
             MinimumLength = AccountValidationConstants.USERNAME_MIN_LENGTH,
             ErrorMessage = "{0} must be between {2} and {1} characters long")]
        [RegularExpression(
             AccountValidationConstants.USERNAME_REG_EX,
             ErrorMessage = "{0} is in invalid format")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(
            AccountValidationConstants.PASSWORD_MAX_LENGTH,
            MinimumLength = AccountValidationConstants.PASSWORD_MIN_LENGTH,
            ErrorMessage = "{0} must be between {2} and {1} characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}