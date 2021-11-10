using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.Listing.Models
{
   public class Modulelisting
    {
        public int rowid { get; set; }
        public int totalrecs { get; set; }
        public string businesstitleurl { get; set; }
        public string phone1 { get; set; }
        public string phone5 { get; set; }
        public string title { get; set; }
        public string trainingmode { get; set; }
        public int businessid { get; set; }
        public string businessname { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public int moduleid { get; set; }
        public string module { get; set; }
        public string moduleurl { get; set; }
        public int courseid { get; set; }
        public string trainingcity { get; set; }
        public string address { get; set; }
        public string nobizimg { get; set; }
        public string businessimageurl { get; set; }
        public bool ImgVisible { get; set; }
        public bool NoimgVisible { get; set; }
        public string trainmode { get; set; }
        public string facilityname { get; set; }
        public string faclity1 { get; set; }
        public string faclity2 { get; set; }
        public string faclity3 { get; set; }
        public string faclity4 { get; set; }

        public bool Isfaclity1 { get; set; }
        public bool Isfaclity2 { get; set; }
        public bool Isfaclity3 { get; set; }
        public bool Isfaclity4 { get; set; }

        public bool Isstackfac1 { get; set; }
        public bool Isstackfac2 { get; set; }
        public string rating { get; set; }
        public string ratingimg { get; set; }
        public bool israting { get; set; }
    }

    public class Moduledata
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<Modulelisting> ROW_DATA { get; set; }
    }

    public class facilities
    {
        public string facilityname { get; set; }
        public string facilityid { get; set; }
    }
    public class Trainingmodes
    {
        public string mode { get; set; }
        public string modeid { get; set; }
    }

    public class Sorting
    {
        public string id { get; set; }
        public string text { get; set; }
        public string image { get; set; }
    }
}
