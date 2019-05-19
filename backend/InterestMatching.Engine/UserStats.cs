using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public class UserStats
    {
        [JsonProperty("Sociability")] public int Sociability { get; set; }

        [JsonProperty("Professionalism")] public int Professionalism { get; set; }

        [JsonProperty("GoodMood")] public int GoodMood { get; set; }

        [JsonProperty("MatchingCount")] public int MatchingCount { get; set; }

        [JsonProperty("Rudeness")] public int Rudeness { get; set; }

        [JsonProperty("NonPunctuality")] public int NonPunctuality { get; set; }

        [JsonProperty("SelfCentered")] public int SelfCentered { get; set; }
    }
}