using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Detail.Models
{
  
    public class Companyprofiledata
    {
        public int businessid { get; set; }
        public string adid { get; set; }
        public string businessname { get; set; }
        public string businesstitleurl { get; set; }
        public string statename { get; set; }
        public string statecode { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string newcityurl { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public string contactname { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string phoneno { get; set; }
        public object faxno { get; set; }
        public string website { get; set; }
        public string aboutbussinesshtmlpath { get; set; }
        public object companylogo { get; set; }
        public string shortdesc { get; set; }
        public string businesstype { get; set; }
        public string companysize { get; set; }
        public string founded { get; set; }
        public string approvedemail { get; set; }
        public string industry { get; set; }
        public string industryurl { get; set; }
        public string jobrole { get; set; }
        public string jobroleurl { get; set; }
        public string primarytags { get; set; }
        public string primarytagsurl { get; set; }
        public string tagapprove { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string experience { get; set; }
        public string postedago { get; set; }
        public int claimflag { get; set; }
        public string businesssddress { get; set; }
        public string businessscity { get; set; }
        public string businessstatecode { get; set; }
        public string businessszipcode { get; set; }
        public string jobroleapproved { get; set; }
        public int verifyflag { get; set; }
        public string techjobstitleurl { get; set; }
        public int lsverifyflag { get; set; }
        public string lstitleurl { get; set; }
        public string experiencefrom { get; set; }
        public string experienceto { get; set; }
        public string resumemandatory { get; set; }
        public int reviewcount { get; set; }
        public string ratingavg { get; set; }
        public string functionarea { get; set; }
        public string functionareaurl { get; set; }
        public string salarymode { get; set; }
        public string minsal { get; set; }
        public string maxsal { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string linkedin { get; set; }
        public string thumbimgurl { get; set; }
        public int adscnt { get; set; }
        public int profilerecruitercnt { get; set; }
        public int jobfaircnt { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }
        //job openings list 
        public int totalrecs { get; set; }
        public int pageno { get; set; }
        public int rowstofetch { get; set; }
        public int rowid { get; set; }
        public int totalrecs1 { get; set; }
        public int multicity { get; set; }
        public int nationalwide { get; set; }
        public string multicities { get; set; }
        public string adtype { get; set; }
        public string showvn { get; set; }
        public int ordergroup { get; set; }
        public object fromsource { get; set; }
        public int adstatus { get; set; }
        public int jobroleid { get; set; }
        public DateTime crdate { get; set; }
        public int functionid { get; set; }
        public string followflag { get; set; }


        public int benefits { get; set; }
        public int reviews { get; set; }
        public int salarylist { get; set; }
        public int jobopeninglist { get; set; }


    }
    public class Companydata
    {
        public List<Companyprofiledata> ROW_DATA { get; set; }
    }

    public class businessfollowresultinfo
    {
        public string resulinformation { get; set; }
        public int followcnt { get; set; }
    }
    public class Businessfollow
    {
        public List<businessfollowresultinfo> ROW_DATA { get; set; }
    }
    public class resultinfo
    {
        public string result { get; set; }
        public string value { get; set; }
        public string phone { get; set; }
        public string mode { get; set; }
        public string pinno { get; set; }
        //claimbusinessresult
        public string dataupdation { get; set; }
        public string resultinformation { get; set; }
        public string reviewid { get; set; }

        //deletesavedad
        public string totalcnt { get; set; }
        public string myadcnt { get; set; }
        public string issavedad { get; set; }
    }

    public class Rating_Details
    {
        public int reviewid { get; set; }
        public string contributor { get; set; }
        public string photourl { get; set; }
        public string shortdesc { get; set; }
        public int siteid { get; set; }
        public int objectid { get; set; }
        public int rating { get; set; }
        public int pid { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string reviewdt1 { get; set; }
        public string reviewdt2 { get; set; }
        public string postedago { get; set; }
        public int ovreviewcount { get; set; }
        public int ovratingavg { get; set; }
        public int ovcareerrating { get; set; }
        public int ovcareerratingavg { get; set; }
        public int ovworkrating { get; set; }
        public int ovworkratingavg { get; set; }
        public int ovmanagementrating { get; set; }
        public int ovmanagementratingavg { get; set; }
        public int adid { get; set; }
        public string commonads { get; set; }

        public string imgtext { get; set; }
        public bool imgvisible { get; set; }
        public bool imgnovisible { get; set; }
    }

    public class RatingData
    {
        public List<Rating_Details> ROW_DATA { get; set; }
    }

    public class Add_Rating_Details
    {
        public string pid { get; set; }
        public string contributor { get; set; }
        public string memberaddress { get; set; }
        public string photourl { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string ipaddress { get; set; }
        public string description { get; set; }
        public string siteid { get; set; }
        public string updatedate { get; set; }
        public string objectid { get; set; }
        public string rating { get; set; }

        public string subcontenttypeid { get; set; }
        public string featureddate { get; set; }
        public string isfeatured { get; set; }
        public string ordreviewid { get; set; }
        public string objecttypeid { get; set; }
        public string brandid { get; set; }
        public string industryid { get; set; }
        public string city { get; set; }
        public string statename { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public string socialappid { get; set; }
        public string socialuserid { get; set; }
        public string reviewid { get; set; }
        public string reviewphotourl { get; set; }
        public string careerrating { get; set; }
        public string workrating { get; set; }
        public string managementrating { get; set; }


        //landingurl  htmlurl  htmlurlbase  usertrack

    }
    public class Reviewreplydetail
    {
        public List<Reviewreplydetaildata> ROW_DATA { get; set; }
    }
    public class Reviewreplydetaildata
    {
        public string reviewid { get; set; }
        public string replytext { get; set; }
        public string contributor { get; set; }
        public string description { get; set; }
        public string crdate { get; set; }
    }
    public class Salarylist
    {
        public List<Salarylistdata> ROW_DATA { get; set; }
    }
    public class Salarylistdata
    {
        // to send businessurl='sulekha-us-llc',@rowstofetch='15',@pageno='1'
        public string jobrole { get; set; }
        public string businessid { get; set; }
        public double minsal { get; set; }
        public double maxsal { get; set; }
        public string salarymode { get; set; }
        public string rowstofetch { get; set; }
        public string rowid { get; set; }
        public string pageno { get; set; }
        public int totalrecs { get; set; }
        public string salary { get; set; }
    }

    public class Businessbenifits
    {
        public List<Businessbenifitlist> ROW_DATA { get; set; }
    }
    public class Businessbenifitlist
    {
        // to send @businessurl='sulekha-us-llc',@rowstofetch='15' 
        public string benefitid { get; set; }
        public string benefits { get; set; }
    }
    public class Businessjobopeningdata
    {
        public int totalrecs { get; set; }
        public int pageno { get; set; }
        public int rowstofetch { get; set; }
        public int rowid { get; set; }
        public int totalrecs1 { get; set; }
        public int adscnt { get; set; }
        public int profilerecruitercnt { get; set; }
        public int jobfaircnt { get; set; }
        public string businessid { get; set; }
        public string adid { get; set; }
        public string businessname { get; set; }
        public string businesstitleurl { get; set; }
        public string statename { get; set; }
        public string statecode { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string newcityurl { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public string contactname { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string phoneno { get; set; }
       // public object faxno { get; set; }
        public string website { get; set; }
        public string aboutbussinesshtmlpath { get; set; }
       // public string companylogo { get; set; }
        public string shortdesc { get; set; }
        public string businesstype { get; set; }
        public string companysize { get; set; }
        public string founded { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string linkedin { get; set; }
        public string thumbimgurl { get; set; }
        public string approvedemail { get; set; }
        public string industry { get; set; }
        public string industryurl { get; set; }
        public string jobrole { get; set; }
        public string jobroleurl { get; set; }
        public string primarytags { get; set; }
        public string primarytagsurl { get; set; }
        public string tagapprove { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string experience { get; set; }
        public string postedago { get; set; }
        public int multicity { get; set; }
        public int nationalwide { get; set; }
        public string multicities { get; set; }
        public string adtype { get; set; }
        public string showvn { get; set; }
        public string businesssddress { get; set; }
        public string businessscity { get; set; }
        public string businessstatecode { get; set; }
        public string businesscityurl { get; set; }
        public string businessszipcode { get; set; }
        public string jobroleapproved { get; set; }
        public int ordergroup { get; set; }
        public object fromsource { get; set; }
        public int claimflag { get; set; }
        public int adstatus { get; set; }
        public int jobroleid { get; set; }
        public DateTime crdate { get; set; }
        public string experiencefrom { get; set; }
        public string experienceto { get; set; }
        public string resumemandatory { get; set; }
        public int reviewcount { get; set; }
        public object ratingavg { get; set; }
        public double minsal { get; set; }
        public double maxsal { get; set; }
        public string salary { get; set; }
        public string salarymode { get; set; }
        public int functionid { get; set; }
        public string followflag { get; set; }
        public string functionarea { get; set; }
        public string functionareaurl { get; set; }
        public int verifyflag { get; set; }
        public string techjobstitleurl { get; set; }
       // public int lsverifyflag { get; set; }
        public string lstitleurl { get; set; }
    }
    public class Businessjobopening
    {
        public List<Businessjobopeningdata> ROW_DATA { get; set; }
    }
    
    public class Jobalert_DATA
    {
        public string email { get; set; }
        public string metro { get; set; }
        public string jobrole { get; set; }
        public string jobroleid { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string state { get; set; }
        public string dayformat { get; set; }
        //public string state { get; set; }
        //jobtype
        //public string state { get; set; }
        //public string roletitle { get; set; }
        //public string metro { get; set; }
        public string userlat { get; set; }
        public string userlong { get; set; }
    }
}
