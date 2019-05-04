namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Tournament : BaseModel
    {
        public string Name { get; set; }

        public string CategoryId { get; set; }
    }
}