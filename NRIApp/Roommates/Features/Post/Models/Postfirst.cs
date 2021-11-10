using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Roommates.Features.Post.Models
{
   public class Postfirst
    {
        public int supercategoryid { get; set; }
        public string supercategoryvalue { get; set; }
        public string primarycategoryid { get; set; }
        public string primarycategoryvalue { get; set; }
        public int secondarycategoryid { get; set; }
        public string secondarycategoryvalue { get; set; }
        public int tertiarycategoryid { get; set; }
        public string categoryname { get; set; }
        public string maincategory { get; set; }
        public string adtype { get; set; }
        public string RMcategory { get; set; }
        public string Locationname { get; set; }
        public string Newcityurl { get; set; }
        public string City { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Countrycode { get; set; }
        public string ContactPhone { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
        //public string Lat { get; set; }
        //public string Long { get; set; }
        public string userlat { get; set; }
        public string userlong { get; set; }
        public string Country { get; set; }
        public string Movedate { get; set; }
        public string gender { get; set; }
        public string ExpRent { get; set; }
        public string HideRent { get; set; }
        public string RentNegotiable { get; set; }
        public string LocationAddress { get; set; }
        public int adid { get; set; }
        public string mobileverify { get; set; }
        public string deviceid { get; set; }
        public string devicetypeid { get; set; }
        public string devicename { get; set; }
        public string postedvia { get; set; }
        public string userpid { get; set; }
        public int agentid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string pricemode { get; set; }
        public string attachedbathtype { get; set; }
        public string accomodate { get; set; }
        public string stayperiod { get; set; }
        public string businessname { get; set; }
        public string businessLicenseno { get; set; }
        public string agentphotourl { get; set; }
        public string BusinessAddress { get; set; }
        public string ipaddress { get; set; }
        public string usertype { get; set; }
        public string imgname { get; set; }
        public int noofremainingads { get; set; }
        public string package { get; set; }
        public int noofdays { get; set; }

        //Added to prefill forms
        public string service { get; set; }
        public string Isagent { get; set; }
        public string Subcategory { get; set; }
        public string Category { get; set; }
        public string Ispaid { get; set; }
        public string Photoscnt { get; set; }
        public string HtmlPhotopath { get; set; }
        public string Imagepath { get; set; }
        public string XmlPhotopath { get; set; }
        public string Adtype { get; set; }
        public string Additionalcity { get; set; }
        public string Citycount { get; set; }
        public string Citytotalamount { get; set; }
        public string Tags { get; set; }
        public string Companyweburl { get; set; }
        public string url { get; set; }
        public string Bestindustry { get; set; }
        public string Minsalary { get; set; }
        public string Maxsalary { get; set; }
        public string Qualification { get; set; }
        public string Visastatus { get; set; }
        public string Experience { get; set; }
        public string Experiencefrom { get; set; }  
        public string Experienceto { get; set; }
        public string Employmenttype { get; set; }
        public string Jobtype { get; set; }
        public string Jobroleid { get; set; }
        public string Postedby { get; set; }
        public string Campaignid { get; set; }
        public string Customerid { get; set; }
        public string Ordertype { get; set; }
        //public string Userpid { get; set; }
        public string Mailerstatus { get; set; }
        public string Displayphone { get; set; }
        public string Status { get; set; }
        public string Contributor { get; set; }
        //public string Description { get; set; }
        public string Titleurl { get; set; }
        public string State { get; set; }
        //public string Title { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string Streetname { get; set; }
        public string Salarymodeid { get; set; }
        public string Salarymode { get; set; }
        public string Nationwide { get; set; }
        public string Trimetros { get; set; }
        public string Linkedinurl { get; set; }
        public string Hideaddress { get; set; }
        public string Isresumeavail { get; set; }
        public string Jobrole { get; set; }
        public string Adpostedcnt { get; set; }
        public string Noofadcnt { get; set; }
        public string Expirydate { get; set; }
        public string Orderid { get; set; }
        public string Industryname { get; set; }
        public string Industryid { get; set; }
        public string Secondarycategoryvalueurl { get; set; }
        public string Primarycategoryvalueurl { get; set; }
        public string Supercategoryvalueurl { get; set; }
        public string Metrourl { get; set; }
        public string Metro { get; set; }
        //public string Statecode { get; set; }
        public string Ismobileverified { get; set; }
        public string Folderid { get; set; }
        public string Amount { get; set; }
        public string Numberofdays { get; set; }
        public string Startdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Enddate { get; set; }
        public string Islisting { get; set; }
        public string Freeadsflag { get; set; }
        public string Categoryflag { get; set; }
        public string Banneramount { get; set; }
        public string Bannerstatus { get; set; }
        public string Companytype { get; set; }
        public string Salespersonemail { get; set; }
        public string Couponcode { get; set; }
        public string Disclosedbusiness { get; set; }
        public string Gender { get; set; }
        public string Responsemail { get; set; }
        public string Benefits { get; set; }
        public string Pendingpaycouponcode { get; set; }
        public string Bonusamount { get; set; }
        public string Bonusdays { get; set; }
        public string Emailblastamount { get; set; }
        public string Addonamount { get; set; }
        public string Adtypetxt { get; set; }
        public string attachedbaths { get; set; }
        public string availablefrm { get; set; }
        public string rent { get; set; }
        //from myneedspage
        public string ismyneed { get; set; }
        public string ispostdes { get; set; }

        //added for myneed
        public string pricefrom { get; set; }
        public string priceto { get; set; }
        public string textcolor { get; set; }
        public string categoryimgurl { get; set; }
        public string mode { get; set; }
        public string licenseno { get; set; }
        //frompaymentedit
        public string ismyneedpayment { get; set; }
    }

    public class PostFirst_Result
    {
        public List<PostFirst_Result_Data> ROW_DATA { get; set; }
    }

    public class PostFirst_Result_Data {
        
        public int adid { get; set; }
        public int agentid { get; set; }
        public string ordertype { get; set; }
        public string result { get; set; }
        public string resultinfo { get; set; }
    }

  

    public class Post_OTP_Result
    {
        public string pinno { get; set; }
        public string type { get; set; }
        public string adid { get; set; }
        public int agentid { get; set; }
        public int ispayment { get; set; }
        public string status { get; set; }


    }
    public class Posting_Param
    {
        public string pricemode { get; set; }
        public int pricemodetype { get; set; }
        public string checkimage { get; set; }
        public string attachedbath { get; set; }
        public string stayperiodtxt { get; set; }
        public string stayperiodID { get; set; }
        public int accomodatecnt { get; set; }
        public string attachbathtype { get; set; }
    }

    public class AgentDetails
    {
        public List<AgentDetails_Data> ROW_DATA { get; set; }
    }
    public class AgentDetails_Data
    {
        public string packagename { get; set; }
        public int totalads { get; set; }
        public string packageamount { get; set; }
        public string adposted { get; set; }
        public string noofadslive { get; set; }
        public int noofadsremain { get; set; }
        public string packageexpirydate { get; set; }
        public string packageexpiredays { get; set; }
        public int packagedays { get; set; }
        public int agentstatus { get; set; }
        public int isagent { get; set; }
        public string agentemail { get; set; }
        public string category { get; set; }
        public int agentid { get; set; }
    }
    public class AutoLoginuserPID
    {
        public string resultinformation { get; set; }
    }
 }
