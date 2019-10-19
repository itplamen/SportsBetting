namespace SportsBetting.Data.Models
{
    using System;

    using SportsBetting.Data.Models.Base;
    
    public class Authentication : BaseModel
    {
        public string AccountId { get; set; }

        public DateTime Expiration { get; set; }
    }
}