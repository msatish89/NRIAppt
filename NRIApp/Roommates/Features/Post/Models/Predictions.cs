using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NRIApp.Roommates.Features.Post.Models
{
    public class Googlecity
    {
        public List<Predictions> Predictions { get; set; }
    }
   public  class Predictions
    {
        public string Description { get; set; }
        public string Place_id { get; set; }
        public string Reference { get; set; }
        public string City { get; set; }
        public List<PredictionMatchedSubstring> matched_substrings { get; set; }
        public string Id { get; set; }
        public List<PredictionTerm> Terms { get; set; }
        public List<string> Types { get; set; }
    }

    public class PredictionTerm
    {
        public int Offset { get; set; }
        public string Value { get; set; }
    }
    public class PredictionMatchedSubstring
    {
        public int Length { get; set; }
        public int Offset { get; set; }
    }
}
