﻿namespace SportsBetting.Feeder.Models
{
    using System;

    using SportsBetting.Feeder.Models.Base;

    public class Team : BaseModel
    {
        public string Name { get; set; }

        public int? Score { get; set; }

        protected override int GenerateId()
        {
            return Math.Abs(Name.GetHashCode());
        }
    }
}