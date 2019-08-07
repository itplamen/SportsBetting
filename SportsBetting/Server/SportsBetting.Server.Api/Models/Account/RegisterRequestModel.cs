namespace SportsBetting.Server.Api.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using SportsBetting.Common.Account;
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Commands.Accounts;

    public class RegisterRequestModel : IMapTo<CreateAccountCommand>
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(
               AccountConstants.USERNAME_MAX_LENGTH,
               MinimumLength = AccountConstants.USERNAME_MIN_LENGTH,
               ErrorMessage = "{0} must be between {2} and {1} characters long")]
        [RegularExpression(
               AccountConstants.USERNAME_REG_EX,
               ErrorMessage = "{0} is in invalid format")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(
            AccountConstants.PASSWORD_MAX_LENGTH,
            MinimumLength = AccountConstants.PASSWORD_MIN_LENGTH,
            ErrorMessage = "{0} must be between {2} and {1} characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(
            nameof(Password),
            ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}