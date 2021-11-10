using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.List.Models
{
   public class DClistings
    {
        public int pageno { get; set; }
        public string rowstofetch { get; set; }
        public string supercategoryid { get; set; }
        public string tags { get; set; }
        public string supercategoryvalue { get; set; }
        public string primarycategoryvalue { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string tertiarycategoryvalue { get; set; }
        public string servicetags { get; set; }
        public int rowid { get; set; }
        public int totalrecs { get; set; }
        public string crdate { get; set; }
        public string startdate { get; set; }
        public string premiumad { get; set; }
        public string businessname { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public string contactname { get; set; }
        public string phone { get; set; }
        public string salaryrate { get; set; }
        public int hideaddress { get; set; }
        public string businessaddress { get; set; }
        public string postedago { get; set; }
        public string profiletitle { get; set; }
        //newly added
        public string objecttype { get; set; }
        public string photoid  { get; set; }
        public string photourl { get; set; }
        public string smallphotourl { get; set; }
        public string supercategoryvalueurl { get; set; }
        public string primarycategoryvalueurl { get; set; }
        public string secondarycategoryvalueurl { get; set; }
        public string multicategory { get; set; }
        public string xml_gender { get; set; }
        public string xml_languages { get; set; }
        public string xml_eligibility { get; set; }
        public string otherworkcount { get; set; }
        public string agegroupid { get; set; }
        public string xml_experience { get; set; }
        public string xml_response { get; set; }
        public string xml_certificates { get; set; }
        public string avgrating { get; set; }
        public string reviewcount { get; set; }
        public string primarycategoryid { get; set; }
        public string secondarycategoryid { get; set; }
        public string tertiarycategoryid { get; set; }
        public string newtertiarycategoryurl { get; set; }
        public string newsecondarycategoryurl { get; set; }
        public string newprimarycategoryurl { get; set; }
        public string newclickurl { get; set; }
        public string businessid { get; set; }
        public string adtype { get; set; }
        public string availablefrom { get; set; }
        public string city { get; set; }
        public string milestotravel { get; set; }
        public string maincategoryid { get; set; }
        public string businesstitleurl { get; set; }
        public string metrourl { get; set; }
        public string ordergroup { get; set; }
        public string ordertype { get; set; }
        public string customerid { get; set; }
        public string ismobileverified { get; set; }
        public string salarymode { get; set; }
        public string postedvia { get; set; }
        public string cityurl { get; set; }
        public string newcityurl { get; set; }
        public string statecode { get; set; }
        public string showvn { get; set; }
        public string zipcode { get; set; }
        public string issaved { get; set; }
        public string numberofdays { get; set; }
        public string lat { get; set; }
        public string longitude { get; set; }
        public string distance   { get; set; }
        public string virtualnanny { get; set; }
        //fromtext
        public string fromtext { get; set; }
        //sortby listing dist
        public bool distVisible { get; set; }
        public bool otherservicevisible { get; set; }
        public bool daycarecentervisible { get; set; }
        public string agegrouptxt { get; set; }
        public string availability { get; set; }
        public string dynamiccallpin { get; set; }
        public bool availabilityvisible { get; set; }
        public bool yearvisible { get; set; }
        public bool worktypevisible { get; set; }
        public bool callpinvisible { get; set; }
        public string ratingimg { get; set; }
        public bool languagevisible { get; set; }
        //worktypetxt
        public string worktypetxt { get; set; }
        public bool ageservedvisible { get; set; }
        public bool eligibilityvisible { get; set; }
        public bool nopetcarelayout { get; set; }
        public bool petcarelayout { get; set; }
        public bool salaryvisible { get; set; }

        public bool agegrouplistvisible { get; set; }
        public bool gendervisible { get; set; }
        public bool virtualnannyvisible { get; set; }
        public bool certificationvisible { get; set; }
    }

    public class DClistingdata
    {
        public List<DClistings> ROW_DATA { get; set; }
    }
    public class DClistinfo
    {
        public string cityurl { get; set; }
        public string userlat { get; set; }
        public string userlong { get; set; }
        public int pageno { get; set; }
        public string newcategoryurl { get; set; }
        public string careprovidertype { get; set; }
        public string orderby { get; set; }
        public string nearby { get; set; }
        public string landmarkid { get; set; }
        public string landmark { get; set; }
        //filter
        public string language { get; set; }
        public string agegroupid { get; set; }
        public string gendertype { get; set; }
        public string experience { get; set; }
        public string worktype { get; set; }
        public string needservice { get; set; }
        //prefill
        public string city { get; set; }

        public string date { get; set; }
        public int filtercount { get; set; }
        public int isrefreshed { get; set; }
        public string caretype { get; set; }
        //miles
        public string miles { get; set; }
        //filter prefil
        public bool locationselected { get; set; }
        public bool careservicetickimg { get; set; }
        public bool needserviceselectedcount { get; set; }
        public bool languagetickimg { get; set; }
        public bool agegroupcount { get; set; }
        public bool gendertickimg { get; set; }
        public bool worktypecount { get; set; }
        public bool radioworktypetickimg { get; set; }
        public bool calentickimg { get; set; }
        public bool experiencetickimg { get; set; }
    }
    public class worktype
    {
        public List<worktype_data> ROW_DATA { get; set; }
    }
    public class worktype_data
    {
        public string supercategoryid { get; set; }
        public string supercategoryvalue { get; set; }
        public string primarycategoryid { get; set; }
        public string primarycategoryvalue { get; set; }
        public string secondarycategoryid { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string tertiarycategoryid { get; set; }
        public string categoryname { get; set; }
        public string maincategory { get; set; }
        public string adtype { get; set; }
        public string maincategoryid { get; set; }
        public string newcategoryurl { get; set; }
        public string parentcaturl { get; set; }

        public string checkimage { get; set; }
    }
    public class response_data
    {
        public string contentid { get; set; }
        public string contactname { get; set; }
        public string emailid { get; set; }
        public string ip { get; set; }
        public string description { get; set; }
        public string phoneno { get; set; }
        public string availablefrom { get; set; }
        public string responsecategorytype { get; set; }
        public string premiumad { get; set; }
        public string agegroupid { get; set; }
        public string city { get; set; }
        public string newclickurl { get; set; }
        public string postedviaid { get; set; }
        public string postedvia { get; set; }
    }
    public class result_info
    {
        public string resultinformation { get; set; }
    }

    public partial class Saved_Ads
    {
        [JsonProperty("ROW_DATA")]
        public List<Saved_Ads_DATA> RowData { get; set; }
    }

    public partial class Saved_Ads_DATA
    {
        public string Totalrecs { get; set; }
        public string Pageno { get; set; }
        public string Rowstofetch { get; set; }
        public string Rowid { get; set; }
        public string Totalrecs1 { get; set; }
        public string Businessaddress { get; set; }
        public string Gender { get; set; }
        public string Businessid { get; set; }
        public string Title { get; set; }
        public string Titleurl { get; set; }
        public string Maincategoryid { get; set; }
        public string City { get; set; }
        public string Statecode { get; set; }
        public string Newcityurl { get; set; }
        public string Metro { get; set; }
        public string Metrourl { get; set; }
        public string Availablefrom { get; set; }
        public string Postedago { get; set; }
        public string Contactname { get; set; }
        public string Supercategoryvalue { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Secondarycategoryvalue { get; set; }
        public string Tertiarycategoryvalue { get; set; }
        public string Supercategoryvalueurl { get; set; }
        public string Primarycategoryvalueurl { get; set; }
        public string Secondarycategoryvalueurl { get; set; }
        public string Tertiarycategoryvalueurl { get; set; }
        public string Myadcnt { get; set; }
        public string Status { get; set; }
    }
}
