namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Team : BaseModel
    {
        public string Name { get; set; }

        public string SportId { get; set; }
    }
}