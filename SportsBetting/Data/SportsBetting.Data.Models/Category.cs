namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Category : BaseModel
    {
        public string Name { get; set; }

        public string SportId { get; set; }
    }
}