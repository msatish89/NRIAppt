using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.List.Models
{
    public class DCfilter
    {
        [JsonProperty("ROW_DATA")]
        public List<DCfilter_DATA> RowData { get; set; }
    }
    public  class DCfilter_DATA
    {
        public string Contentid { get; set; }
        public string Categorylevel { get; set; }
        public string Superurl { get; set; }
        public string Supercategoryid { get; set; }
        public string Primaryurl { get; set; }
        public string Primarycategoryid { get; set; }
        public string Secondaryurl { get; set; }
        public string Secondarycategoryid { get; set; }
        public string Tertiaryurl { get; set; }
        public string Tertiarycategoryid { get; set; }
        public string Maincategory { get; set; }
        public string Maincategoryid { get; set; }
        public string Newcategoryurl { get; set; }
        public string Newcategory { get; set; }

        public string checkimage { get; set; }
    }

    public  class LandmarkList
    {
        [JsonProperty("ROW_DATA")]
        public List<LandmarkList_DATA> RowData { get; set; }
    }

    public  class LandmarkList_DATA
    {
        public string Contentid { get; set; }
        public string Landmark { get; set; }
        public string Landmarkurl { get; set; }
        public string City { get; set; }
        public string Cityurl { get; set; }
        public string Statename { get; set; }
        public string Statecode { get; set; }
        public string Address { get; set; }
        public string Metro { get; set; }
        public string Metrourl { get; set; }
        public string Countrycode { get; set; }
        public string Landmarktype { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Abbreviation { get; set; }
        public string Mobileno { get; set; }
        public string Zipcode { get; set; }
        public string Website { get; set; }
    }

    public class language
    {
        public string languagetxt { get; set; }
        public string languageid { get; set; }
        public string image { get; set; }
    }
   
}
