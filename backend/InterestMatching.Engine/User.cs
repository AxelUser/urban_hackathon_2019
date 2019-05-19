using System;
using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public class User
    {
        [JsonProperty("Email")] public string Email { get; set; }

        [JsonProperty("Age")] public int Age { get; set; }

        [JsonProperty("Interests")] public bool[] Interests { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("SurName")] public string SurName { get; set; }

        [JsonProperty("UserStats")] public UserStats UserStats { get; set; }

        [JsonProperty("DaysOfTheWeek")] public int[] DaysOfTheWeek { get; set; }

        public double CalculateInterestsDistanse(int[] matchingInterests)
        {
            var result = 0.0;
            for (var i = 0; i < Interests.Length; i++)
                result += Math.Abs(Convert.ToInt32(Interests[i]) - Convert.ToInt32(matchingInterests[i]));

            return Math.Sqrt(result);
        }
    }
}