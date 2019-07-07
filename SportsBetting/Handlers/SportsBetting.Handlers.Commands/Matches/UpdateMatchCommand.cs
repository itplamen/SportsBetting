namespace SportsBetting.Handlers.Commands.Matches
{
    using System;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class UpdateMatchCommand : ICommand
    {
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public MatchStatus Status { get; set; }

        public string Score { get; set; }

        public string StreamURL { get; set; }
    }
}