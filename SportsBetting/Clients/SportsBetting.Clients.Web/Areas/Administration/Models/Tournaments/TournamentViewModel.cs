namespace SportsBetting.Clients.Web.Areas.Administration.Models.Tournaments
{
    using System;

    public class TournamentViewModel
    {
        public string Id { get; set; }

        public int Key { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }
    }
}