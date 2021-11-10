using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{




    public class LS_DETAIL
    {
        public List<LS_DETAIL_DATA> ROW_DATA { get; set; }
    }

    public class LS_DETAIL_DATA
    {

        public string adid { get; set; }
        public string customerid { get; set; }
        public string campaignid { get; set; }
        public string contactname { get; set; }
        public string ismobileverified { get; set; }
        public string crdate1 { get; set; }
        public string premiumad { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string availablefrom { get; set; }
        public string availableto { get; set; }
        public string adtype { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string listingstartdate { get; set; }
        public string industryexp { get; set; }
        public string listingenddate { get; set; }
        public string ip { get; set; }
        public string userpid { get; set; }
        public string contributor { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string phoneno { get; set; }
        public string website { get; set; }
        public string usertype { get; set; }
        public string ordergroup { get; set; }
        public string prioritypercentage { get; set; }
        public string adstatus { get; set; }
        public string upddate { get; set; }
        public string titleurl { get; set; }
        public string htmlpath { get; set; }
        public string oldclpostid { get; set; }
        public string htmlurl { get; set; }
        public string tags { get; set; }
        public string tag { get; set; }
        public string tagurl { get; set; }
        public string locations { get; set; }
        public string lat { get; set; }
        public string longitude { get; set; }
        public string photoid { get; set; }
        public string general_contentid { get; set; }
        public string generaltitlelower { get; set; }
        public string general_title { get; set; }
        public string general_titleurl { get; set; }
        public string folderid { get; set; }
        public string streetname { get; set; }
        public string virtualno { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string zipcode { get; set; }
        public string metrocityurl { get; set; }
        public string statecode { get; set; }
        public string statename { get; set; }
        public string country { get; set; }
        public string countryurl { get; set; }
        public string citylat { get; set; }
        public string citylong { get; set; }
        public string primarytag { get; set; }
        public string primarytagurl { get; set; }
        public string metrolat { get; set; }
        public string metrolong { get; set; }
        public string nma { get; set; }
        public string metrostateurl { get; set; }
        public string metrocountrycode { get; set; }
        public string metrostate { get; set; }
        public string metrocountry { get; set; }
        public string newcityurl { get; set; }
        public string metrourl { get; set; }
        public string metro { get; set; }
        public string contentid { get; set; }
        public string objecttype { get; set; }
        public string pagetype { get; set; }
        public string pagetitle { get; set; }
        public string activemenu { get; set; }
        public string locobjecttype { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public string pagedesc { get; set; }
        public string heading { get; set; }
        public string activepathpattern { get; set; }
        public string photourl { get; set; }
        public string servicename { get; set; }
        public string serviceurl { get; set; }
        public string supertagid { get; set; }
        public string supertag { get; set; }
        public string supertagurl { get; set; }
        public string primarytagid { get; set; }
        public string primarytags { get; set; }
        public string primarysupertags { get; set; }
        public string primarytagsurl { get; set; }
        public string responsecnt { get; set; }
        public string metros { get; set; }
        public string maincityurl { get; set; }
        public string emailverified { get; set; }
        public string postedviaid { get; set; }
        public string postedvia { get; set; }
        public string ogobjtype { get; set; }
        public string ogobjgroup { get; set; }
        public string ogobjpagetype { get; set; }
        public string pageusersegment { get; set; }
        public string pageuserdem { get; set; }
        public string tagsid { get; set; }
        public string membersince { get; set; }
        public string userlat { get; set; }
        public string userlong { get; set; }
        public string profileimgurl { get; set; }
        public string othermetros { get; set; }
        public string servicestype { get; set; }
        public string bizimgurl { get; set; }
        public string biztaglin { get; set; }
        public string reviewcnt { get; set; }
        public string virno { get; set; }
        public string tpservid { get; set; }
        public string offerscnt { get; set; }
        public string ltyp4and5 { get; set; }
        public string ltyp3and5 { get; set; }
        public string adlivestatus { get; set; }
        public string hidelocation { get; set; }
        public string facebookfeed { get; set; }
        public string twitterfeed { get; set; }
        public string facebooklink { get; set; }
        public string twitterlink { get; set; }
        public string gpluslink { get; set; }
        public string pinterestlink { get; set; }
        public string instagramlink { get; set; }
        public string linkedinlink { get; set; }
        public string ownername { get; set; }
        public string websiteslink { get; set; }
        public string servicewithid { get; set; }
        public string viewno { get; set; }
        public string seoalternatetagname { get; set; }
        public string offeredmetrocnt { get; set; }
        public string adsestablished { get; set; }
        public string claimedstatus { get; set; }
        public string metroalias { get; set; }
        public string seoschematext { get; set; }
        public string servingtextname { get; set; }
        public string ratingavg { get; set; }
        public bool israting { get; set; }
        public string servicetypes { get; set; }
        public string toptags { get; set; }
        public string adstiming { get; set; }
        public string adavailability { get; set; }
        public string isadsaved { get; set; }
        public string Isrecomended { get; set; }
        public string timingstatus { get; set; }
        public string twilionumber { get; set; }
        public string twiliopin { get; set; }
        public string calltotwilio { get; set; }


    }

    public class LS_LTYPE
    {
        public List<LS_LTYPE_DATA> ROW_DATA { get; set; }
    }
    public class LS_LTYPE_DATA
    {
        public string tag { get; set; }
        public string tagurl { get; set; }
    }
    public class LS_AD_CITIES
    {
        public List<LS_AD_CITIES_DATA> ROW_DATA { get; set; }
    }
    public class LS_AD_CITIES_DATA
    {
        public string city { get; set; }
        public string cityurl { get; set; }
        public string locationtype { get; set; }
    }
    public class LS_AD_PHOTOS
    {
        public List<LS_AD_PHOTOS_DATA> ROW_DATA { get; set; }
    }
    public class LS_AD_PHOTOS_DATA
    {
        public string totalrecs { get; set; }
        public string pageno { get; set; }
        public string rowstofetch { get; set; }
        public string thumburl { get; set; }
        public string PhotoUrl { get; set; }
        public string photoid { get; set; }
        public string adid { get; set; }
        public string type { get; set; }
        public string adtitle { get; set; }
        public string adurl { get; set; }
        public string CrDate { get; set; }
        public string oldthumburl { get; set; }
        public string oldphotourl { get; set; }
    }
    public class LS_AD_VIDEOS
    {
        public List<LS_AD_VIDEOS_DATA> ROW_DATA { get; set; }
    }
    public class LS_AD_VIDEOS_DATA
    {
        public string totalrecs { get; set; }
        public string contentid { get; set; }
        public string adid { get; set; }
        public string videourl { get; set; }
        public string videoimgurl { get; set; }
    }
    public class LS_AD_SERVICES
    {
        public List<LS_AD_SERVICES_DATA> ROW_DATA { get; set; }
    }
    public class LS_AD_SERVICES_DATA
    {
        public string menucardid { get; set; }
        public string adid { get; set; }
        public string mainmenuid { get; set; }
        public string mainmenu { get; set; }
        public string submenu { get; set; }
        public string submenudescr { get; set; }
        public int isheader { get; set; }
        public string originalprice { get; set; }
        public string supertagid { get; set; }
        public string offerprice { get; set; }
        public string pricetype { get; set; }
        public string servicetype { get; set; }
        public string isbatch { get; set; }
        public string batchstartdate { get; set; }
        public string batchenddate { get; set; }
        public string timing { get; set; }
        public string batchdatexml { get; set; }
        public string packagedescr { get; set; }
        public string imgurl { get; set; }
        public bool detailblkvisible { get; set; }
        public bool submenuvisible { get; set; }
        public bool originalpricevisible { get; set; }
        public string image { get; set; }
        public string Heading { get; set; }
    }
    public class LS_AD_GROUP_SERVICES
    {
        public string Heading { get; set; }
        public string image { get; set; }
        public List<LS_AD_SERVICES_DATA> Services { get; set; }
        public LS_AD_GROUP_SERVICES() { }
        public LS_AD_GROUP_SERVICES(string name, List<LS_AD_SERVICES_DATA> SERVICES)
        {
            Heading = Heading;
            Services = SERVICES;
        }
    }

    public class LS_REVIEWS
    {
        public List<LS_REVIEWS_DATA> ROW_DATA { get; set; }
    }

    public class LS_REVIEWS_DATA
    {
        public string pageno { get; set; }
        public string rowstofetch { get; set; }
        public string rowid { get; set; }
        public string reviewid { get; set; }
        public string contributor { get; set; }
        public string photourl { get; set; }
        public string shortdesc { get; set; }
        public string siteid { get; set; }
        public string rating { get; set; }
        public string pid { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string reviewdt1 { get; set; }
        public string reviewdt2 { get; set; }
        public string postedago { get; set; }
        public string reviewphotocount { get; set; }
        public string reviewphotourls { get; set; }
        public int ovreviewcount { get; set; }
        public string ovratingavg { get; set; }
        public string ovworkquality { get; set; }
        public string ovworkqualityavg { get; set; }
        public string ovvalue { get; set; }
        public string ovvalueavg { get; set; }
        public string ovcsutomerservice { get; set; }
        public string ovcsutomerserviceavg { get; set; }
        public string adid { get; set; }
        public string commonads { get; set; }
        public string imgtext { get; set; }
        public bool imgvisible { get; set; }
        public bool imgnovisible { get; set; }

    }

    public class LS_REVIEW_POST
    {
        public List<LS_REVIEW_POST_RESULT> ROW_DATA { get; set; }
    }

    public class LS_REVIEW_POST_RESULT
    {
        public string resultinformation { get; set; }
        public string reviewid { get; set; }
    }

    public class LS_REVIEW_REPLY
    {
        public List<LS_REVIEW_REPLY_DATA> ROW_DATA { get; set; }
    }

    public class LS_REVIEW_REPLY_DATA
    {
        public string contentid { get; set; }
        public string pid { get; set; }
        public string contributor { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string description { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string supertag { get; set; }
        public string primarytag { get; set; }
        public string rating { get; set; }
        public string replycomment { get; set; }
        public string workqrating { get; set; }
        public string valuerating { get; set; }
        public string custserrating { get; set; }
        public string postedago { get; set; }
        public string replytext { get; set; }

    }
    public class LS_REVIEW_REPLY_POST
    {
        public List<LS_REVIEW_REPLY_POST_DATA> ROW_DATA { get; set; }
    }

    public class LS_REVIEW_REPLY_POST_DATA
    {
        public string result { get; set; }
    }

    public class LS_FAVORITE_SAVED
    {
        public List<LS_FAVORITE_SAVED_DATA> ROW_DATA { get; set; }
    }

    public class LS_FAVORITE_SAVED_DATA
    {
        public string totalcnt { get; set; }
        public string myadcnt { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string titleurl { get; set; }
        public string adid { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string cityurl { get; set; }
        public string issavedad { get; set; }
    }

    public class LS_REPORT_LIST
    {
        public string reportlist { get; set; }
        public int reportid { get; set; }
    }
    public class LS_RECOMEND
    {
        public List<LS_RECOMEND_DATA> ROW_DATA { get; set; }
    }

    public class LS_RECOMEND_DATA
    {
        public string result { get; set; }
        public string ovreccount { get; set; }
    }

    public class LS_REPORT
    {
        public List<LS_REPORT_DATA> ROW_DATA { get; set; }
    }

    public class LS_REPORT_DATA
    {
        public string resultinfo { get; set; }
    }

    

}
