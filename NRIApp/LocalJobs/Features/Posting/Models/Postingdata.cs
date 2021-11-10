using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class Postingdata
    {
        public int jobtype { get; set; }
        public int functionalareaid { get; set; }
        public string functionalarea { get; set; }
        public string roleid { get; set; }
        public string rolename { get; set; }
        public string skillids { get; set; }
        public string skills { get; set; }
        public string joblocation { get; set; }
        public string newcityurl { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string lat { get; set; }
        public string longitude { get; set; }
        public string minexp { get; set; }
        public string maxexp { get; set; }
        public string minsalary { get; set;}
        public string maxsalary { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string username { get; set; }
        public string usermobileno { get; set; }
        public string contrycode { get; set; }
        public string useremail { get; set; }
        public string ip { get; set; }
        public string userpid { get; set; }
        public string bizname { get; set; }
        public string bizaddress { get; set; }
        public string bizlogo { get; set; }
        public string ismobileverified { get; set; }
        public string country { get; set; }
        public string adid { get; set; }
        public string postedvia { get; set; }
        public string deviceid { get; set; }
        public string salmode { get; set; }
        public string salmodeid { get; set; }
        public string isbannerchk { get; set; }
        public string isnationwidechk { get; set; }
        public string usertype { get; set; }
        public string categoryflag { get; set; }
        public int isbestinindustry { get; set; }
        public string employmenttype { get; set; }
        public int discloseid { get; set; }

        public string respid { get; set; }
        //ordersummary
        public string validitydays { get; set; }

        //myneeds page
        public string isagent { get; set; }
        public string xml_photopath { get; set; }
        public string imagepath { get; set; }
        public string html_photopath { get; set; }
        public string Photoscnt { get; set; }
        public string Folderid { get; set; }
        public string Amount { get; set; }
        public string Ispaid { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Adtype { get; set; }
        public int Supercategoryid { get; set; }
        public int Primarycategoryid { get; set; }
        public int Secondarycategoryid { get; set; }
        public string Supercategoryvalue { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Secondarycategoryvalue { get; set; }
        public string Tags { get; set; }
        public string licenseno { get; set; }
        public string Companyweburl { get; set; }
        public string Supertagid { get; set; }
        public string Qualification { get; set; }
        public string Visastatus { get; set; }
        public string Experience { get; set; }
        public string Isresumeavail { get; set; }
        public string Hideaddress { get; set; }
        public string Linkedinurl { get; set; }
        public string Titleurl { get; set; }
        public string Displayphone { get; set; }
        public string Mailerstatus { get; set; }
        public string Ordertype { get; set; }
        public string Startdate { get; set; }
        public string Numberofdays { get; set; }
        public string Metro { get; set; }
        public string Metrourl { get; set; }
        public string Supercategoryvalueurl { get; set; }
        public string Primarycategoryvalueurl { get; set; }
        public string Secondarycategoryvalueurl { get; set; }
        public string Orderid { get; set; }
        public string Expirydate { get; set; }
        public string Noofadcnt { get; set; }
        public string Adpostedcnt { get; set; }
        public string Responsemail { get; set; }
        public string Gender { get; set; }
        //public string Disclosedbusiness { get; set; }
        public string Couponcode { get; set; }
        public string Salespersonemail { get; set; }
        public string Companytype { get; set; }
        public string Freeadsflag { get; set; }
        public string Islisting { get; set; }
        public string Addonamount { get; set; }
        public string Emailblastamount { get; set; }
        public string Bonusdays { get; set; }
        public string Bonusamount { get; set; }
        public string Pendingpaycouponcode { get; set; }
        public string Benefits { get; set; }
        public string Bannerstatus { get; set; }
        public string Banneramount { get; set; }
        public string Enddate { get; set; }
        public string Campaignstarts { get; set; }
        public string Campaignend { get; set; }
        public string Bestindustry { get; set; }
        public string Experiencefrom { get; set; }
        public string Experienceto { get; set; }
        public string Noofopening { get; set; }
        public string Status { get; set; }
        public string ismyneed { get; set; }
        public string mode { get; set; }
        public string disclosedbusiness { get; set; }
        public string appcouponcode { get; set; }
    }

    public class postingsbmtdetails
    {
        public string otpsend { get; set; }
        public string otpno { get; set; }
        public string adid { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
        public string usertype { get; set; }
        public string blockstatus { get; set; }
        public string ispayment { get; set; }
    }

    public class Businessdetails
    {
        public string businessname { get; set; }
        public string contentid { get; set; }
    }

    public class Businessdata
    {
        public List<Businessdetails> ROW_DATA { get; set; }
    }

    public class Salarymodes
    {
        public string salmode { get; set; }
        public string salmodeval { get; set; }
        public string salmodeid { get; set; }
        public string checkimage { get; set; }
    }
}
