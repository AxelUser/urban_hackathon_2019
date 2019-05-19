using System;
using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public class Meeting
    {
        [JsonProperty("Email")] public int Id { get; set; }

        [JsonProperty("FirstUserId")] public string FirstUserId { get; set; }

        [JsonProperty("SecondUserId")] public string SecondUserId { get; set; }

        [JsonProperty("Date")] public DateTime Date { get; set; }

        [JsonProperty("FirstUserSetRate")] public bool FirstUserSetRate { get; set; }

        [JsonProperty("SecondUserSetRate")] public bool SecondUserSetRate { get; set; }
    }
}