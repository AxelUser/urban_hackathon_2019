﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Text;
using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public class User
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Age")]
        public int Age { get; set; }

        [JsonProperty("Interests")]
        public int[] Interests { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SurName")]
        public string SurName { get; set; }

        [JsonProperty("UserStats")]
        public UserStats UserStats { get; set; }

        [JsonProperty("DaysOfTheWeek")]
        public DateTime[] DaysOfTheWeek { get; set; }

        public double CalculateInterestsDistanse(int[] matchingInterests)
        {
            var result = 0.0;
            for (var i = 0; i < this.Interests.Length; i++)
            {
                result += Math.Pow(Math.Abs(this.Interests[i] - matchingInterests[i]), 2);
            }

            return Math.Sqrt(result);
        }
    }
}
