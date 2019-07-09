namespace SportsBetting.Feeder.Models
{
    using SportsBetting.Feeder.Models.Base;

    public class TeamFeedModel : BaseFeedModel
    {
        public string Name { get; set; }

        public int? Score { get; set; }

        protected override int GenerateKey()
        {
            return Name.GetHashCode();
        }
    }
}