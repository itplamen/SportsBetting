namespace SportsBetting.Feeder.Models
{
    using System;

    using SportsBetting.Feeder.Models.Base;

    public class Tournament : BaseModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        protected override int GenerateId()
        {
            return Math.Abs(Name.GetHashCode() ^ Category.GetHashCode());
        }
    }
}