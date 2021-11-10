using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.USHome.Models
{
   public class Techjobslist
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<TrainingCourse> gettrainingcourse { get; set; }
    }

    public class TrainingCourse
    {
        public int tagid { get; set; }
        public string tag { get; set; }
        public string tagurl { get; set; }
        public string imagepath { get; set; }
    }
}
