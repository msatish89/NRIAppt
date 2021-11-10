using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.LeadForm.Models
{
    public class Cityajax
    {
        public string countrycode { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string zipcode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string fullcity { get; set; }
        public string header { get; set; }
        public string citystatecodeurl { get; set; }
        public string metrourl { get; set; }
        public string statename { get; set; }
    }

    public class Cityajaxlist
    {
        public List<Cityajax> ROW_DS { get; set; }
    }
}
