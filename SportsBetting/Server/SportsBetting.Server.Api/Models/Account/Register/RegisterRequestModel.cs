namespace SportsBetting.Server.Api.Models.Account.Register
{
    using System.ComponentModel.DataAnnotations;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Commands.Accounts;

    public class RegisterRequestModel : AccountRequestModel, IMapTo<CreateAccountCommand>
    {

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