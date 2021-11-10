using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.List.Models
{
   
    public class DCsearch
    {
        [JsonProperty("ROW_DS")]
        public List<DCsearch_Data> RowDs { get; set; }
    }

    public  class DCsearch_Data
    {
        public string Title { get; set; }
        public string Titleurl { get; set; }
        public string Search { get; set; }
        public string Businessid { get; set; }
        public string Maincategoryid { get; set; }
        public string City { get; set; }
        public string Statecode { get; set; }
        public string Newcityurl { get; set; }
        public string Metro { get; set; }
        public string Metrourl { get; set; }
        public string Availablefrom { get; set; }
        public string Salaryrate { get; set; }
        public string Contactname { get; set; }
        public string Postedago { get; set; }
        public string Supercategoryvalue { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Secondarycategoryvalue { get; set; }
        public string Tertiarycategoryvalue { get; set; }
        public string Supercategoryvalueurl { get; set; }
        public string Primarycategoryvalueurl { get; set; }
        public string Secondarycategoryvalueurl { get; set; }
        public string Tertiarycategoryvalueurl { get; set; }
        public string Supercategoryid { get; set; }
        public string Primarycategoryid { get; set; }
        public string Secondarycategoryid { get; set; }
        public string Tertiarycategoryid { get; set; }
        public string Commonads { get; set; }
        public string Catgoryadscount { get; set; }
        public string Trackurl { get; set; }
        public Uri Thumbimgurl { get; set; }
        public string Newtertiarycategoryurl { get; set; }
        public string Newsecondarycategoryurl { get; set; }
        public string Newprimarycategoryurl { get; set; }
        public string Servicetags { get; set; }
        public string Gender { get; set; }

        public string header { get; set; }
        public bool stackdetails { get; set; }
        public string adtype { get; set; }
        public string tertiarycategoryvalue { get; set; }
        public string secondarycategoryvalue { get; set; }
    }
}
