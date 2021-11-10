using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Listing.Models
{
    public class LocationOverallData
    {
        public List<Locations> ROW_DATA { get; set; }
    }
    public class Locations
    {
        public string countrycode { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public string newcityurl { get; set; }
    }
}
