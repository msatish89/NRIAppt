using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Rentals.Features.Post.Models
{

    public class adtype
    {
        public int typeid { get; set; }
        public int stagid { get; set; }
        public string type { get; set; }
        public string textcolor { get; set; }
    }
    public class sortlist
    {
        public string oderbylist { get; set; }
        public string oderlist { get; set; }
        public string type { get; set; }
        public string image { get; set; }
    }
    public class RTCategoryRowData
    {
        public List<RTCategoryList> ROW_DATA { get; set; }
    }
    public class RTCategoryList
    {
        public int supercategoryid { get; set; }
        public string supercategoryvalue { get; set; }
        public int primarycategoryid { get; set; }
        public string primarycategoryvalue { get; set; }
        public int secondarycategoryid { get; set; }
        public string secondarycategoryvalue { get; set; }
        public int tertiarycategoryid { get; set; }
        public string tertiarycategoryvalue { get; set; }
        public string categoryname { get; set; }
        public string maincategory { get; set; }
        public string adtype { get; set; }
        public string RMcategory { get; set; }
        public string Movedate { get; set; }

        public string ExpRent { get; set; }

        public string categoryimgurl { get; set; }
        public string textcolor { get; set; }

        public string Locationname { get; set; }
        public string Newcityurl { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Countrycode { get; set; }
        public string ContactPhone { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Country { get; set; }
        public int countflag { get; set; }

        public int adid { get; set; }
        public int agentid { get; set; }
        public string usertype { get; set; }

        public string agentphotourl { get; set; }
        public string RentNegotiable { get; set; }
        public string HideRent { get; set; }
        public string attachedbathtype { get; set; }
        public string stayperiod { get; set; }
        public string accomodate { get; set; }
        //myneedsprefill
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
        public string Postedby { get; set; }
        public string Campaignid { get; set; }
        public string Customerid { get; set; }
        public string Ordertype { get; set; }
        public string Userpid { get; set; }
        public string Mailerstatus { get; set; }
        public string Displayphone { get; set; }
        public string Status { get; set; }
        public string Contributor { get; set; }
        //public string Description { get; set; }
        public string Titleurl { get; set; }
        public string State { get; set; }
        //public string Title { get; set; }
        public string Streetname { get; set; }
        public string Salarymodeid { get; set; }
        public string Salarymode { get; set; }
        public string Nationwide { get; set; }
        public string Trimetros { get; set; }
        public string Linkedinurl { get; set; }
        public string Hideaddress { get; set; }
        public string Adpostedcnt { get; set; }
        public string Noofadcnt { get; set; }
        public string Expirydate { get; set; }
        public string Orderid { get; set; }
        public string Secondarycategoryvalueurl { get; set; }
        public string Primarycategoryvalueurl { get; set; }
        public string Supercategoryvalueurl { get; set; }
        public string Metrourl { get; set; }
        public string Metro { get; set; }
        public string Statecode { get; set; }
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
        public string businessname { get; set; }
        public string City { get; set; }
        //from myneedspage
        public string ismyneed { get; set; }
        public string Rent { get; set; }
        public string attachedbaths { get; set; }
        public string availablefrm { get; set; }
        public string pricemode { get; set; }
        public string licenseno { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string pricefrom { get; set; }
        public string priceto { get; set; }
        public string rent { get; set; }
        public string area { get; set; }
        public string noofbaths { get; set; }
        public string ismyneedpayment { get; set; }
        public string sqrfeet { get; set; }
    }

    public class RTLocationRowData
    {
        public List<RTLocation> ROW_DATA { get; set; }
    }
    public class RTLocation
    {
        public string countrycode { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string statename { get; set; }
        public string zipcode { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public string primaryrecord { get; set; }
        public string cityurl { get; set; }
        public string radius { get; set; }
        public string pincode { get; set; }
        public string newcityurl { get; set; }
        public string metrourl { get; set; }
        public string citystatecode { get; set; }
    }
    class offerorwant
    {
    }
}
