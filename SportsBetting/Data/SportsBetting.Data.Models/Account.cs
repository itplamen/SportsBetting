namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Account : BaseModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }

        public bool IsVerified { get; set; }
    }
}