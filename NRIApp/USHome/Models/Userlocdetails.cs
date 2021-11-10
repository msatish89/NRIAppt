using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.USHome.Models
{
   public class Userlocdetails
    {
        public string city { get; set; }
        public string cityurl { get; set; }
        public string statecode { get; set; }
        public string zipcode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string countrycode { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string statename { get; set; }
    }

    public class Userloc
    {
        public List<Userlocdetails> ROW_DATA { get; set; }
    }
}
