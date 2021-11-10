using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{

    public class LS_LISTINGMODEL
    {
        public List<LS_LISTINGMODEL_DATA> ROW_DATA { get; set; }
    }

    public class LS_LISTINGMODEL_DATA
    {
        public string rowid { get; set; }
        public int totalrecs { get; set; }
        public string bizrunrate { get; set; }
        public string adstatus { get; set; }
        public string oid { get; set; }
        public string locationtype { get; set; }
        public string adid { get; set; }
        public string rowid1 { get; set; }
        public string title { get; set; }
        public string contactname { get; set; }
        public string premiumad { get; set; }
        public string adtype { get; set; }
        public string oldclpostid { get; set; }
        public string shortdesc { get; set; }
        public string city { get; set; }
        public string titleurl { get; set; }
        public string ordergroup { get; set; }
        public string contentid { get; set; }
        public string email { get; set; }
        public string virtualno { get; set; }
        public double miles { get; set; }
        public string streetname { get; set; }
        public string cityurl { get; set; }
        public string newcityurl { get; set; }
        public string statecode { get; set; }
        public string thumbimgurl { get; set; }
        public string metrourl { get; set; }
        public string ismobileverified { get; set; }
        public string zipcode { get; set; }
        public string lat { get; set; }
        public string longitutude { get; set; }
        public string viewno { get; set; }

        public int pageno { get; set; }
        public string rowstofetch { get; set; }
        public string emailverified { get; set; }

        public string isadsaved { get; set; }
        public string maincityurl { get; set; }
        public string maincity { get; set; }
        public string mainstatecode { get; set; }
        public string reviewcount { get; set; }
        public string ratingavg { get; set; }
        public string servicewithid { get; set; }
        public string photoscnt { get; set; }
        public string ltype { get; set; }


        public string citystatecode { get; set; }
        public string totalmiles { get; set; }
        public bool ImgVisible { get; set; }
        public bool NoimgVisible { get; set; }
        public string nobizimg { get; set; }
        public bool rateavil { get; set; }
        public bool Isvirtualno { get; set; }
        public bool Novirtualno { get; set; }
        public bool isreviewcount { get; set; }
        public string reviews { get; set; }
        public string levels { get; set; }
        public string offertitle { get; set; }
        public bool isofferavail { get; set; }
        public bool isratingavail { get; set; }
        public string adavailability { get; set; }
        public string adavailabilitytext { get; set; }
        public string avalabilitycolor { get; set; }
        public string timing { get; set; }
        public string timing1 { get; set; }
        public string timing2 { get; set; }
        public string timing3 { get; set; }
        public string timing1color { get; set; }
        public string timing2color { get; set; }
        public bool timing1avail { get; set; }
        public bool timing2avail { get; set; }
        public bool timing3avail { get; set; }
        
        public bool timingavail { get; set; }

        public string starimg { get; set; }
        public string rating { get; set; }

        public bool ismilesavail { get; set; }
        public string timingstatus { get; set; }
        public bool timingavailother { get; set; }
        public string timingother { get; set; }
        public string citiescnttxt { get; set; }

        public string twilionumber { get; set; }
        public string twiliopin { get; set; }
        public string calltotwilio { get; set; }
        public string countrycode { get; set; }
    }

    public class SERVICE_LEAF_TYPES
    {
        public List<SERVICE_LEAF_TYPES_DATA> ROW_DATA { get; set; }
    }
    public class SERVICE_LEAF_TYPES_DATA
    {
        public int tagid { get; set; }
        public string tagurl { get; set; }
        public string tag { get; set; }
        public int ltypeid { get; set; }
    }
    public class LS_RESP_FORM
    {
        public string adid { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string countrycode { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
 
        public string country { get; set; }
        public string Selecteddate { get; set; }
        public string postedvia { get; set; }
        public string selectedservicetypeid { get; set; }
        public string selectedservicetypetext { get; set; }
        public string premiumid { get; set; }
        public string pagetype { get; set; }

        public string userpid { get; set; }
        public string loginusername { get; set; }



    }
    public class LISTING_SEARCH_DEFAULT
    {
        public List<LISTING_SEARCH_DEFAULT_DATA> ROW_DATA { get; set; }
    }
    public class LISTING_SEARCH_DEFAULT_DATA
    {
        public int tagid { get; set; }
        public string tagurl { get; set; }
        public string tag { get; set; }
        public string header { get; set; }
        public int type { get; set; }
        public string mobileno { get; set; }
        public string cityurl { get; set; }
        public string premiumad { get; set; }
        public string stagid { get; set; }
    }
    public class LISTING_SORTING
    {
        public string id { get; set; }
        public string text { get; set; }
        public string image { get; set; }
    }

}
