namespace SportsBetting.Clients.Web.Areas.Administration.Models.Base
{
    using System;

    public abstract class BaseViewModel
    {
        public string Id { get; set; }

        public int Key { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}