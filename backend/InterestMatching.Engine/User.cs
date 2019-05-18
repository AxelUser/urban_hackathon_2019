using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Text;

namespace InterestMatching.Engine
{
    public class User
    {
        public string Email { get; set; }

        public int Age { get; set; }

        public int[] Interests { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public UserStats UserStats { get; set; }

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
