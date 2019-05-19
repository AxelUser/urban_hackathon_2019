using System;
using System.Linq;
using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public class User
    {
        [JsonProperty("Email")] public string Email { get; set; }

        [JsonProperty("Age")] public int Age { get; set; }

        [JsonProperty("Interests")] public int[] Interests { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("SurName")] public string SurName { get; set; }

        [JsonProperty("UserStats")] public UserStats UserStats { get; set; }

        [JsonProperty("DaysOfTheWeek")] public int[] DaysOfTheWeek { get; set; }

        public double CalculateInterestsDistanse(int[] matchingInterests)
        {
            var result = Interests.Select((t, i) => Math.Pow(Math.Abs(t - matchingInterests[i]), 2)).Sum();

            return Math.Sqrt(result);
        }
    }
}