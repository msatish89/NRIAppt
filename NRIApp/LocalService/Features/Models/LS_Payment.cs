using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class LS_Payment
    {
        public string months { get; set; }
        public int years { get; set; }
        public string checkimage { get; set; }
    }
    public class SHOPPING_CART_DATA
    {
        public string Ordernumber { get; set; }
        public string TotalPrice { get; set; }
        public string cartid { get; set; }
        public string Authorize_TransId { get; set; }
        public string Authorize_AuthCode { get; set; }
        public string StrObjecttitle { get; set; }
        public string Result { get; set; }
    }
    public class FLEX_PAYMENT_SUCCESS_DATA
    {
        public string name { get; set; }
        public string value { get; set; }
        public string guid { get; set; }
        public string msg { get; set; }
        public string url { get; set; }
    }
    public class FLEX_PUBLISH_SUCCESS_DATA
    {
        public string alladid { get; set; }
        public string oldCLPostID { get; set; }
        public string result { get; set; }
        public string requirement { get; set; }
        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ContactName { get; set; }
        public string countryurl { get; set; }
        public string stateurl { get; set; }
        public string cityurl { get; set; }
        public string country { get; set; }
        public string statename { get; set; }
        public string cityname { get; set; }
        public string ListingEndDate { get; set; }
        public string AdStatus { get; set; }
        public string linkurl { get; set; }
        public string zipcode { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string newcityurl { get; set; }
        public string pstagid { get; set; }
        
    }
    public class LS_PAYMENT_DETAILS
    {
        public string userid { get; set; }
        public string userpid { get; set; }
        public string email { get; set; }
        public string adid { get; set; }
        public string bizmode { get; set; }
        public string contactname { get; set; }
        public string address { get; set; }
        public string contactno { get; set; }
        public string mobilecountry { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string contactemail { get; set; }
        public string selectremitindia { get; set; }
        public string cardnumber { get; set; }
        public string expmonth { get; set; }
        public string expyear { get; set; }
        public string cvv { get; set; }
        public string AuthCode { get; set; }
        public string TransactionId { get; set; }
        public string postedviaid { get; set; }
        public string couponcode { get; set; }
        public string postedvia { get; set; }
        public string Ordernumber { get; set; }
        public string TotalPrice { get; set; }
        public string cartid { get; set; }
        public string Authorize_TransId { get; set; }
        public string Authorize_AuthCode { get; set; }
        public string StrObjecttitle { get; set; }

    }
    public class LS_FLEX_PAYMENT_DETAILS
    {
        public string userid { get; set; }
        public string userpid { get; set; }
        public string email { get; set; }
        public string adid { get; set; }
        public string bizmode { get; set; }
        public string contactname { get; set; }
        public string address { get; set; }
        public string contactno { get; set; }
        public string mobilecountry { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string contactemail { get; set; }
        public string selectremitindia { get; set; }
        public string cardnumber { get; set; }
        public string expmonth { get; set; }
        public string expyear { get; set; }
        public string cvv { get; set; }
        public string AuthCode { get; set; }
        public string TransactionId { get; set; }
        public string postedviaid { get; set; }
        public string couponcode { get; set; }
        public string postedvia { get; set; }
        public string Ordernumber { get; set; }
        public string TotalPrice { get; set; }
        public string cartid { get; set; }
        public string Authorize_TransId { get; set; }
        public string Authorize_AuthCode { get; set; }
        public string StrObjecttitle { get; set; }
        public string Businessname { get; set; }
        public string Primeid { get; set; }
        public string Oldclpostid { get; set; }
        public string Couponcode { get; set; }
        public string Primeguid { get; set; }

    }

    public class LS_AD_DETAILS
    {
        public List<LS_AD_DETAILS_DATA> ROW_DATA { get; set; }
    }

    public class LS_AD_DETAILS_DATA
    {
        public string streetname { get; set; }
        public string zipcode { get; set; }
        public string newcityurl { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string contributor { get; set; }

    }

    public class LS_RECEIPT
    {
        public List<LS_RECEIPT_DATA> ROW_EMAIL_RECEPT { get; set; }
    }

    public class LS_RECEIPT_DATA
    {

        public string adid { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string adtype { get; set; }
        public string ordertype { get; set; }
        public string mailerstatus { get; set; }
        public string status { get; set; }
        public string adsid { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string verifieddate { get; set; }
        public string crdate { get; set; }
        public string contributor { get; set; }
        public string orderid { get; set; }
        public string addonamount { get; set; }
        public string citytotalamount { get; set; }
        public string amount { get; set; }
        public string adtitle { get; set; }
        public string transid { get; set; }
        public string cardname { get; set; }
        public string numberofdays { get; set; }
        public string agentid { get; set; }
        public string noofadsremain { get; set; }
        public string lastrenewaldate { get; set; }
        public string cityurl { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string primarytagurl { get; set; }
        public string profileimgurl { get; set; }
        public string streetname { get; set; }
        public string mobileno { get; set; }
        public string supertagurl { get; set; }
        public string zipcode { get; set; }
        public string salesandmanageremail { get; set; }
        public string country { get; set; }
        public string linkurl { get; set; }
        public string email { get; set; }
    }
    public class LS_FLEX_RECEIPT
    {
        public List<LS_FLEX_RECEIPT_DATA> ROW_DATA { get; set; }
    }

    public class LS_FLEX_RECEIPT_DATA
    {

        public string mgremailid { get; set; }
        public string salesemailid { get; set; }
        public string csemailid { get; set; }
        public string mgrname { get; set; }
        public string salesname { get; set; }
        public string csname { get; set; }
        public string result { get; set; }
        public string amount { get; set; }
        public string ptag { get; set; }
        public string city { get; set; }
        public string title { get; set; }
        public string oldclpostid { get; set; }
        public string crdate { get; set; }
        public string days { get; set; }
        public string email { get; set; }
        public string billingemail { get; set; }
        public string contactname { get; set; }
        public string assured { get; set; }
        public string transid { get; set; }
        public string productemail { get; set; }
        public string servicetags { get; set; }
        public string addons { get; set; }
        public string pextracity { get; set; }
        public string pextrametro { get; set; }
        public string pextrastate { get; set; }
        public string pnationalwide { get; set; }
        public string adid { get; set; }
        public string pname { get; set; }
        public string pricingtypevia { get; set; }
    }

    public class LS_FLEX_AD_PACKAGE_DETAILS
    {
        public List<LS_FLEX_AD_PACKAGE_DETAILS_DATA> ROW_DATA { get; set; }
    }

    public class LS_FLEX_AD_PACKAGE_DETAILS_DATA
    {

        public string service { get; set; }
        public string adid { get; set; }
        public string adsid { get; set; }
        public string agentid { get; set; }
        public string isagent { get; set; }
        public string xml_photopath { get; set; }
        public string imagepath { get; set; }
        public string html_photopath { get; set; }
        public string photoscnt { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string adtype { get; set; }
        public string supercategoryid { get; set; }
        public string primarycategoryid { get; set; }
        public string secondarycategoryid { get; set; }
        public string tertiarycategoryid { get; set; }
        public string supercategoryvalue { get; set; }
        public string primarycategoryvalue { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string tertiarycategoryvalue { get; set; }
        public string tags { get; set; }
        public string businessname { get; set; }
        public string busscontactname { get; set; }
        public string supertagid { get; set; }
        public string qtsnanswers { get; set; }
        public string qtnanswersothers { get; set; }
        public string availablefrom { get; set; }
        public string availableto { get; set; }
        public string availabledays { get; set; }


        public string industryexp { get; set; }
        public string bussestablish { get; set; }
        public string videourl { get; set; }
        public string hideaddress { get; set; }
        public string trimetros { get; set; }
        public string extrametros { get; set; }
        public string nationwide { get; set; }
        public string salarymode { get; set; }
        public string salarymodeid { get; set; }
        public string streetname { get; set; }
        public string zipcode { get; set; }
        public string lat { get; set; }
        public string newcityurl { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string description { get; set; }
        public string contributor { get; set; }
        public string status { get; set; }
        public string displayphone { get; set; }
        public string mailerstatus { get; set; }
        public string userpid { get; set; }
        public string ordertype { get; set; }
        public string customerid { get; set; }
        public string campaignid { get; set; }
        public string postedby { get; set; }
        public string enddate { get; set; }

        public string email { get; set; }
        public string phone { get; set; }
        public string startdate { get; set; }
        public string numberofdays { get; set; }
        public string amount { get; set; }
        public string folderid { get; set; }
        public string countrycode { get; set; }
        public string ismobileverified { get; set; }
        public string statecode { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string agentstatus { get; set; }
        public string totalads { get; set; }
        public string adsposted { get; set; }
        public string noofadslive { get; set; }
        public string noofadsremain { get; set; }
        public string agentadordertype { get; set; }
        public string agentcity { get; set; }
        public string agentzipcode { get; set; }
        public string agentstatecode { get; set; }
        public string agentstate { get; set; }
        public string agentcountry { get; set; }
        public string agentbusinessname { get; set; }
        public string agentcontactname { get; set; }
        public string agentbusestablish { get; set; }
        public string agentwebsite { get; set; }
        public string agentaddress { get; set; }
        public string agentemail { get; set; }
        public string packageexpiredate { get; set; }

        public string packageexpiredays { get; set; }
        public string supercategoryvalueurl { get; set; }
        public string primarycategoryvalueurl { get; set; }
        public string secondarycategoryvalueurl { get; set; }
        public string tertiarycategoryvalueurl { get; set; }
        public string noofextrametros { get; set; }
        public string extrametrosamount { get; set; }
        public string pkgamount { get; set; }
        public string overallamount { get; set; }
        public string promotometros { get; set; }
        public string servicetype { get; set; }
        public string profileimgurl { get; set; }
        public string licensenumber { get; set; }
        public string aboutexperience { get; set; }
        public string licenseapproved { get; set; }
        public string referralurl { get; set; }
        public string stepno { get; set; }
        public string addonamount { get; set; }
        public string addontype { get; set; }
        public string ispaid { get; set; }
        public string postedtype { get; set; }
        public string couponstatus { get; set; }
        public string couponcode { get; set; }
        public string discountamount { get; set; }
        public string overallamtwithdiscount { get; set; }
        public string issalead { get; set; }
        public string salespersonemail { get; set; }
    }
}
