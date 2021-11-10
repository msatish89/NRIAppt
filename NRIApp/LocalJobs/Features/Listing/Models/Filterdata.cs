using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Listing.Models
{
    public class FilterListings
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<Filterdata> ROW_DATA { get; set; }
    }
    public class Filterdata
    {
        public string contentid { get; set; }
        public string tag { get; set; }
        public string tagurl { get; set; }
        public string image { get; set; }
    }
    public class sortlist
    {
        public string sortby { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public string filterby { get; set; }
    }
}
