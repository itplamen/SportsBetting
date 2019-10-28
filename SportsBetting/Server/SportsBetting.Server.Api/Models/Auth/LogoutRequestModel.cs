namespace SportsBetting.Server.Api.Models.Auth
{
    using System.ComponentModel.DataAnnotations;

    public class LogoutRequestModel
    {
        [Required]
        public string LoginToken { get; set; }
    }
}