using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Rentals.Features.Post.Models
{
    public class RTResponse
    {
        //public int totalrecs { get; set; }
        public int pageno { get; set; }
        public int supercategoryid { get; set; }
        public int primarycategoryid { get; set; }
        public int secondarycategoryid { get; set; }
        public int tertiarycategoryid { get; set; }
        public string categoryname { get; set; }
        public string maincategory { get; set; }
        public int adtype { get; set; }
        public string primarycategoryvalue { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string supercategoryvalue { get; set; }
        public string RMcategory { get; set; }
        public string Movedate { get; set; }
        public string ExpRent { get; set; }
        public string price1 { get; set; }
        public string price2 { get; set; }
        public string Locationname { get; set; }

        public string Newcityurl { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string CountryCode { get; set; }
        public string ContactPhone { get; set; }
        public string mobileverify { get; set; }
        public string ismobileverified { get; set; }
        public string adid { get; set; }
        public string ipaddress { get; set; }
        public string detailpostid { get; set; }
        public string devicename { get; set; }
        public string devicetypeid { get; set; }
        public string deviceid { get; set; }
        public string postedvia { get; set; }

        public string cityurl { get; set; }


        public string Title { get; set; }
        public string Shortdesc { get; set; }
        public string metrourl { get; set; }
        public string pricefrom { get; set; }
        public string usertype { get; set; }
        public string petpolicy { get; set; }
        public string furnishing { get; set; }
        public string amenities { get; set; }
        public string orderby { get; set; }
        public string stayperiod { get; set; }
        public string roomtype { get; set; }
        public string isveg { get; set; }
        public string distancefrm { get; set; }
        public string distanceto { get; set; }
        public string availablefrm { get; set; }
        public string bedrooms { get; set; }
        public string noofbaths { get; set; }
        public string accommodates { get; set; }
        public string accomodationtype { get; set; }
        public double distance { get; set; }
        public string userlat { get; set; }
        public string userlong { get; set; }
        public string nearby { get; set; }

        public string countflag { get; set; }

        public string Zipcode { get; set; }
        public string statecode { get; set; }
        public string sqrfeet { get; set; }

        public string pricemode { get; set; }
        public string accomodate { get; set; }
        public string attachedbaths { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string attachedbathtype { get; set; }
        public string userpid { get; set; }
        public string Sroomtype { get; set; }
        public string allowpermission { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string LocationAddress { get; set; }
        public string lcftype { get; set; }
        public int isResponseotp { get; set; }
        //myneed
        public string StateName { get; set; }
        public string agentid { get; set; }
        public string Streetname { get; set; }
        public string priceto { get; set; }
        public string Folderid { get; set; }
        public string ismyneed { get; set; }
        public string ismyneedpayment { get; set; }
        public string Amount { get; set; }
        public string Numberofdays { get; set; }
        public string Startdate { get; set; }
        public string Freeadsflag { get; set; }
        public string Couponcode { get; set; }
        public string Disclosedbusiness { get; set; }
        public string Pendingpaycouponcode { get; set; }
        public string Bonusamount { get; set; }
        public string Bonusdays { get; set; }
        public string Emailblastamount { get; set; }
        public string Addonamount { get; set; }
        public string businessname { get; set; }
        public string Enddate { get; set; }
        public string ispostdesc { get; set; }
        public string package { get; set; }
        public string Ordertype { get; set; }
        public string mode { get; set; }
    }

    public class RTLCF_Result
    {
        public List<RTLCF_Result_Data> ROW_DATA { get; set; }
    }

    public class RTLCF_Result_Data
    {
        public string result { get; set; }
        public int allrespid { get; set; }
        public string adid { get; set; }
        public string metrourl { get; set; }
        public string metro { get; set; }
    }

    public class RTOTP_LCF_Result
    {
        public string pinno { get; set; }
        public string type { get; set; }
        public int allrespid { get; set; }
        public string Result { get; set; }
        public int ispayment { get; set; }
        public string adid { get; set; }
        public string lcfpostadid { get; set; }

    }

    public class RTTop_Business
    {
        public List<RTTop_Business_Data> ROW_DATA { get; set; }
    }
    public class RTTop_Business_Data
    {
        public int adid { get; set; }
        public string email { get; set; }
        public string title { get; set; }
        public string contactname { get; set; }
        public string streetname { get; set; }
        public string clickurl { get; set; }
        public string city { get; set; }
    }
    public class RentalsSearchList
    {
        public List<RentalsSearch> ROW_DS { get; set; }
    }
    public class RentalsSearch
    {
        public string adid { get; set; }
        public string id { get; set; }
        public string Contentid { get; set; }
        public string title { get; set; }
        public string adtype { get; set; }
        public string roomtype { get; set; }
        public string statecode { get; set; }
        public string cityname { get; set; }
        public string price1 { get; set; }
        public string cityurl { get; set; }
        public string search { get; set; }
        public string header { get; set; }
        public bool stackdetails { get; set; }
    }
    public class PopContent_RT
    {
        public string rentamt { get; set; }
        public string checkimg { get; set; }
        public string pricefrom { get; set; }
        public string ExpRent { get; set; }
    }

}
