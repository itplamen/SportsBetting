namespace SportsBetting.Feeder.Models
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum MatchFeedStatus
    {
        [EnumMember(Value = "NOT STARTED")]
        NotStarted,

        [EnumMember(Value = "LIVE")]
        InPlay,

        [EnumMember(Value = "ENDED")]
        Ended,

        [EnumMember(Value = "SUSPENDED")]
        Suspended,

        [EnumMember(Value = "ABANDONED")]
        Abandoned,

        [EnumMember(Value = "CLOSED")]
        Closed,

        [EnumMember(Value = "CANCELLED")]
        Cancelled,

        [EnumMember(Value = "DELAYED")]
        Delayed
    }
}