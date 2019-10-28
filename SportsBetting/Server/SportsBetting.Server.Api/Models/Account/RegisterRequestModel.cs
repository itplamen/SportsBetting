namespace SportsBetting.Server.Api.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Commands.Accounts.Commands;

    public class RegisterRequestModel : AccountRequestModel, IMapTo<AccountCommand>
    {
        [Required(ErrorMessage = "Confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(
            nameof(Password),
            ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}