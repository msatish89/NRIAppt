using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NRIApp.Techjobs.Features.Home.Models
{
    public class Modulelist
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<PTags> ROW_DATA { get; set; }
    }
    public class PTags
    {
        public string moduleid { get; set; }
        public string module { get; set; }
    }

   
}
