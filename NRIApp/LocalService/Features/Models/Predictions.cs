using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class Googlecity
    {
        public List<Predictions> Predictions { get; set; }
    }

    public class Predictions
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("place_id")]
        public string Place_id { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        public List<PredictionMatchedSubstring> matched_substrings { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("terms")]
        public List<PredictionTerm> Terms { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }
    public class PredictionTerm
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
    public class PredictionMatchedSubstring
    {
        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
