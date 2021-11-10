using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Listing.Models
{
    public class LocalJob
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<LocalJobResponse> ROW_DATA { get; set; }
    }
    public class LocalJobResponse
    {
        public int pageno { get; set; }
        public int rowstofetch { get; set; }
        public string objecttype { get; set; }
        public int recordshowcount { get; set; } //counts of total recs
        public string tags { get; set; }
        public string jobroleapproved { get; set; }
        public string jobroleurl { get; set; }
        public string functionalarea { get; set; }
        public string tag { get; set; }
        public string tagurl { get; set; }
        public string photoid { get; set; }
        public string thumbimgurl { get; set; }
        public string postedago { get; set; }
        public string supertagid { get; set; }
        public string supertags { get; set; }
        public string primarytags { get; set; }
        public string tagapprove { get; set; }
        public string employmenttype { get; set; }
        public string workauthorization { get; set; }
        public int resumemandatory { get; set; }
        public string supercategoryvalue { get; set; }  // i offer
        public string primarycategoryvalue { get; set; } //Non IT
        public string secondarycategoryvalue { get; set; }
        public string isadsaved { get; set; }
        public string industry { get; set; }
        public string industryurl { get; set; }
        public string businessname { get; set; }
        public string photourl { get; set; }
        public string city { get; set; } //Clint
        public string cityurl { get; set; } //clint
        public string newcityurl { get; set; } //clint-tx
        public string statecode { get; set; } //TX
        public string rowid { get; set; }
        public int totalrecs { get; set; }
        public string adid { get; set; }
        public string crdate { get; set; } //Jan 01, 2018
        public DateTime adposteddate { get; set; }//2019-09-23
        public string listingstartdate1 { get; set; }//Sep 23, 2019
        public string listingstartdate { get; set; }
        public int premiumad { get; set; }
        public string adtype { get; set; }
        //public string usagetype { get; set; } //empty
        public string oldclpostid { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public double minsal { get; set; }
        public double maxsal { get; set; }
        public string salarymodeid { get; set; }
        public string salarymode { get; set; } //Monthly
        public string contactname { get; set; }
        public string noofopening { get; set; }
        public string titleurl { get; set; }
        public int jobtypeid { get; set; }
        public string ordergroup { get; set; }
        public string customerid { get; set; }
        public string contentid { get; set; }
        public string prioritypercentage { get; set; }
        public string streetname { get; set; }
        public string qualification { get; set; }
        public string experience { get; set; } //0
        public string postedviaid { get; set; }
        public string postedvia { get; set; }
        public string experiencefrom { get; set; } //0
        public string experienceto { get; set; } //10
        public int countrycode { get; set; }
        public int ismobileverified { get; set; }
        public int functionalareaid { get; set; }
        public int jobroleid { get; set; }
        public string jobrole { get; set; }
        public int isresumeavail { get; set; }
        public int hideaddress { get; set; }
        public string industryid { get; set; }
        public string businessid { get; set; }
        public int locationtype { get; set; }
        public string businesscity { get; set; }
        public string businesscityurl { get; set; }
        public int agentcnt { get; set; }
        public int recruitercnt { get; set; }
        public int showvn { get; set; }
        public int recruiterid { get; set; }
        public bool businessVisible { get; set; }
        public string salary { get; set; }
        public string Ddate { get; set; }
        public bool addressvisible { get; set; }
        public string @long { get; set; }

        public int savedadscount { get; set; }
        public double miles { get; set; }
        public string distancedata { get; set; }
        public bool distVisible { get; set; }
    }
    public class Savedadlist_DATA
    {
        public int contentid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string crdate { get; set; }
        public string cityname { get; set; }
        public string statecode { get; set; }
        public string cityurl { get; set; }
        public string adid { get; set; }
        public double MINSAL { get; set; }
        public double MAXSAL { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string search { get; set; }
        public string adtype { get; set; }
        public string gender { get; set; }
        public int premiumad { get; set; }
        public int displayphone { get; set; }
        public string contactname { get; set; }
        public string tag { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public int myadcnt { get; set; }
        public int ordergroup { get; set; }
        public string fromsource { get; set; }
        public string resumemandatory { get; set; }

        public string salary { get; set; }
        public string salarymode { get; set; }


        //savedresumelist
        
        public string Name { get; set; }
        public string Jobresumeid { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public string Mobileno { get; set; }
        public string Email { get; set; }
        public string Pid { get; set; }
        public string Resumecount { get; set; }
        public string Rolename { get; set; }
        public string Availableresume { get; set; }
        public string Resumetype { get; set; }
        public string Resumefilename { get; set; }
        public string Htmlfilename { get; set; }
    }

    public class Savedadlist
    {
        public List<Savedadlist_DATA> ROW_DATA { get; set; }
    }
    
    public class LocalJobSenddata
    {
        public string searchtext { get; set; }
        public string userpid { get; set; }
        public string cityurl { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string zipcode { get; set; }
        public int pageno { get; set; }
        public int rowstofetch { get; set; }
        public double userlat { get; set; }
        public double userlong { get; set; }
        public string distance { get; set; }
        public string titleurl { get; set; }
        public string jobrole { get; set; }
        public string experiencefrom { get; set; } //0
        public string experienceto { get; set; } //10
        public string minsal { get; set; }
        public string maxsal { get; set; }
        public string requirement { get; set; } //offered
        public string tagurl { get; set; } //empty
        public string orderby { get; set; } //ordergroup
        public string jobtype { get; set; } //empty
        public string employmenttype { get; set; }
        public string visatypeids { get; set; }
        public string industryurl { get; set; }
        public string contentid { get; set; } //0
        public string title { get; set; }
        public string titledesc { get; set; }
        public string nearby { get; set; }
        public string metrourl { get; set; }
        public string skillids { get; set; }
        public int functionids { get; set; }
        public string industryids { get; set; }
        public string roleid { get; set; }
        public string roletxt { get; set; }
        public string businessurl { get; set; }
        public int filtercount { get; set; }
        public string dateposted { get; set; }
        public string education { get; set; }

        //jobresume prefill
        public string industrytxt { get; set; }
        public string skilltxt { get; set; }
    }
    public class ptags
    {
        public string skills { get; set; }
    }
}
