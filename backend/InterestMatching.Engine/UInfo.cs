using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InterestMatching.Engine
{
    public class UInfo
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Age")]
        public string Age { get; set; }
    }
}
