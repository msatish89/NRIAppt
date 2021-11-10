using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.LocalJobs.Features.Detail.Models
{
    public class Detaildata
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<Single_Detaildata> ROW_DATA { get; set; }
    }
    public class Single_Detaildata
    {
        public int adid { get; set; }
        public string contactname { get; set; }
        public int premiumad { get; set; }
        public string campaignid { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string orderbydate { get; set; }
        public int adtype { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string htmldesc { get; set; }
        public string htmlurl { get; set; }
        public HtmlWebViewSource htmldescweb { get; set; }
        public DateTime listingstartdate { get; set; }
        public DateTime listingenddate { get; set; }
        public string contributor { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string phoneno { get; set; }
        public int usertype { get; set; }
        public int ordergroup { get; set; }
        public double prioritypercentage { get; set; }
        public int adstatus { get; set; }
        public int allcities { get; set; }
        public string tags { get; set; }
        public string tag { get; set; }
        public string locations { get; set; }
        public double lat { get; set; }
        public double @long { get; set; }
        public int showvn { get; set; }
        public string minsal { get; set; }
        public string maxsal { get; set; }
        public int salarymodeid { get; set; }
        public string salarymode { get; set; }
        public string photoid { get; set; }
        public string streetname { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string zipcode { get; set; }
        public string metrocityurl { get; set; }
        public string statecode { get; set; }
        public string statename { get; set; }
        public string country { get; set; }
        public string countryurl { get; set; }
        public double citylat { get; set; }
        public double citylong { get; set; }
        public string supertag { get; set; }
        public string supertagurl { get; set; }
        public string primarytag { get; set; }
        public string primarytagurl { get; set; }
       // public double metrolat { get; set; }
       // public double metrolong { get; set; }
        public string nma { get; set; }
        public string metrostateurl { get; set; }
        public string metrocountrycode { get; set; }
        public string metrostate { get; set; }
        public string metrocountry { get; set; }
        public string statecode1 { get; set; }
        public string newcityurl { get; set; }
        public string metrourl { get; set; }
        public string metro { get; set; }
        public string metroalias { get; set; }
        public string contentid { get; set; }
        public string objecttype { get; set; }
        public string pagetype { get; set; }
        public string pagetitle { get; set; }
        public string activemenu { get; set; }
        public string locobjecttype { get; set; }
        public string description { get; set; }
        public string pagedesc { get; set; }
        public string heading { get; set; }
        public string photourl { get; set; }
        public string noofopening { get; set; }
        public int totalcitycnt { get; set; }
        public string servicename { get; set; }
        public string serviceurl { get; set; }
        public string experience { get; set; }
        public int jobtypeid { get; set; }
        public string qualification { get; set; }
        public string experiencefrom { get; set; }
        public string experienceto { get; set; }
        public string employmenttype { get; set; }
        public int locationdisplay { get; set; }
        public string workauthorization { get; set; }
        public string employmenttypeurl { get; set; }
        public string workauthorizationurl { get; set; }
        public string functionalareaurl { get; set; }
        public string supertagid { get; set; }
        public string supertags { get; set; }
        public string supertagsurl { get; set; }
        public string primarytags { get; set; }
        public string primarytagsurl { get; set; }
        public string tagapprove { get; set; }
        public string jobroleurl { get; set; }
        public int responsecnt { get; set; }
        public int activeresponsecnt { get; set; }
        public string businesscity { get; set; }
        public string businesscityurl { get; set; }
        public string businessnewcityurl { get; set; }
        public string businessstatecode { get; set; }
        public int postedviaid { get; set; }
        public string postedvia { get; set; }
        public string pageusersegment { get; set; }
        public string pageuserdem { get; set; }
        public int countrycode { get; set; }
        public int ismobileverified { get; set; }
        public int functionalareaid { get; set; }
        public int jobroleid { get; set; }
        public string jobrole { get; set; }
        public int isresumeavail { get; set; }
        public object linkedinurl { get; set; }
        public int hideaddress { get; set; }
        public string supercategoryvalue { get; set; }
        public string primarycategoryvalue { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string supercategoryvalueurl { get; set; }
        public string primarycategoryvalueurl { get; set; }
        public string secondarycategoryvalueurl { get; set; }
        public int industryid { get; set; }
        public string industry { get; set; }
        public string industryurl { get; set; }
        public string businessname { get; set; }
        public int isadsaved { get; set; }
        public string businesstitleurl { get; set; }
        public string functionalarea { get; set; }
        public int jobroleapproved { get; set; }
        public string gender { get; set; }
        public string companytype { get; set; }
        public int resumemandatory { get; set; }
        public string benefits { get; set; }
        public string titleurl { get; set; }
        public string postedago { get; set; }
        public DateTime crdate1 { get; set; }
        public int businessid { get; set; }

        public string folderid { get; set; }
        /* 
         public string ogobjtype { get; set; }
         public string website { get; set; }
         public string keywords { get; set; }
         public int ogobjgroup { get; set; }
         public string multicities { get; set; }
         public string ogobjpagetype { get; set; }
         public int prevadid { get; set; }
         public string prevtitle { get; set; }
         public string prevnewtitleurl { get; set; }
         public string prevcity { get; set; }
         public string prevnewcityurl { get; set; }
         public int nextadid { get; set; }
         public string nexttitle { get; set; }
         public string nextnewtitleurl { get; set; }
         public string nextcity { get; set; }
         public string nextnewcityurl { get; set; }
         public string prevstatecode { get; set; }
         public string prevtagapprove { get; set; }
         public string prevexperiencefrom { get; set; }
         public string prevexperienceto { get; set; }
         public int prevnationalwide { get; set; }
         public int prevmulticity { get; set; }
         public string prevmulticities { get; set; }
         public string nextstatecode { get; set; }
         public string nexttagapprove { get; set; }
         public string nextexperiencefrom { get; set; }
         public string nextexperienceto { get; set; }
         public int nextnationalwide { get; set; }
         public int nextmulticity { get; set; }
         public string nextmulticities { get; set; }
         public int general_contentid { get; set; }
         public string generaltitlelower { get; set; }
         public string general_title { get; set; }
         public string general_titleurl { get; set; }
         public string folderid { get; set; }
         public string videourl { get; set; }
         public string activepathpattern { get; set; }
         public string videourl1 { get; set; }
         public int multicity { get; set; }
         public int nationalwide { get; set; }
         
         public int businessid { get; set; }
         public int customerid { get; set; }
         public int campaignid { get; set; }
         public string ip { get; set; }
         public int userpid { get; set; }
         public string baseaddress { get; set; }
         public string faxno { get; set; }
         public DateTime upddate { get; set; }
         public string titleurl { get; set; }
         public string htmlpath { get; set; }
         public object vendorid { get; set; }
         public int oldclpostid { get; set; }
         public string htmlurl { get; set; }
         public string tagurl { get; set; }*/
    }
    public class JobsSaveAD
    {
        [JsonProperty("ROW_DATA")]
        public List<JobsSaveAdList> ROW_DATA { get; set; }
    }
    public class JobsSaveAdList
    {
        public int totalcnt { get; set; }
        public int myadcnt { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string titleurl { get; set; }
        public string adid { get; set; }
        public string cityurl { get; set; }
    }
    public class JobsDeleteAD
    {
        [JsonProperty("ROW_DATA")]
        public List<JobsDeleteAdList> ROW_DATA { get; set; }
    }
    public class JobsDeleteAdList
    {
        public int totalcnt { get; set; }
        public int myadcnt { get; set; }
        public int issavedad { get; set; }
    }
    public class ReportFlag
    {
        public string reportlist { get; set; }
        public int reportid { get; set; }
        public string checkimage { get; set; }
    }
    public class Reportflagresult_DATA
    {
        public string resultinfo { get; set; }
    }
    public class postresponse
    {
        public string contentid { get; set; }
        public string name { get; set; }
        public string shortdesc { get; set; }
        public string userpid { get; set; }
        public string ipaddress { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string contributor { get; set; }
        public string BaseAddress { get; set; }//?
        public string UserType { get; set; }//?
        public string respondedas { get; set; }//?
        public string respageurl { get; set; }//?
        public string city { get; set; }
        public string slotno { get; set; }
        public int premiumad { get; set; }
        //pagetype
        public string postedviaid { get; set; } //191-android  188-iphone  193-ipad
        public string postedvia { get; set; }
        public string usersegment { get; set; }//?
        public string relocate { get; set; }
        public double ipuserlat { get; set; }
        public double ipuserlong { get; set; }
        public string countrycode { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string responsecategroy { get; set; }
    }
    public class postresponseresult
    {
        //[JsonProperty("ROW_DATA")] 
        [JsonProperty("ROW_DATA")]
        public List<postresponseresult_data> ROW_DATA { get; set; }
    }
    public class postresponseresult_data
    {
        public string resultinfo { get; set; }
        //public string allrespid { get; set; }
        //public int servicerespid { get; set; }
        //public int isvalid { get; set; }
        //public string result { get; set; }
        //public string email { get; set; }
        //public string contactname { get; set; }
        //public string title { get; set; }
        //public string titleurl { get; set; }
        //public string allresonsepid { get; set; }
        //public string newtitleurl { get; set; }
        //public string premiumad { get; set; }
        //public string adid { get; set; }
        //public string showvn { get; set; }
        //public string mobileno { get; set; }
        //public string slotno { get; set; }
        //public string responsecategroy { get; set; }
        //public string responsemail { get; set; }
        //public string pagetype { get; set; }
        //public string oldclpostid { get; set; }
        //public string htmltxt { get; set; }
        //public string relocate { get; set; }
        //public string adresponsecount { get; set; }
        //public string freeadresponsecount { get; set; }
        //public string responseshowflag { get; set; }
    }

    public class Joboffers
    {
        public List<joboffersdata> ROW_DATA { get; set; }
    }
    public class joboffersdata
    {
        public int totalrecs { get; set; }
        public int pageno { get; set; }
        public int rowstofetch { get; set; }
        public string adid { get; set; }
        public string newcityurl { get; set; }
        public string jobrole { get; set; }
        public string applyingfor { get; set; }
        public string businessid { get; set; }
        public string checkimage { get; set; }
    }

}
