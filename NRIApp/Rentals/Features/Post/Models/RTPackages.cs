using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Rentals.Features.Post.Models
{
    public class RTPackages
    {
        public string amount { get; set; }
        public string ordertype { get; set; }
        public string noofdays { get; set; }
        public string featuretitle { get; set; }
    }
    public class RTPackagelist
    {
        public List<RTPackages> ROW_DATA { get; set; }
    }
    public class Payment_Params_RT
    {
        public string months { get; set; }
        public string monthname { get; set; }
        public int years { get; set; }
        public string checkimage { get; set; }
    }

   
    public class VALUE_ADD_SERVICE
    {
        public List<VALUE_ADD_SERVICE_DATA> ROW_DATA { get; set; }
    }
    public class VALUE_ADD_SERVICE_DATA
    {
        public string contentid { get; set; }
        public string title { get; set; }
        public string features { get; set; }
        public string crdate { get; set; }
        public string isactive { get; set; }
        public float addonamountbasic { get; set; }
        public string addonamountbasic_contentid { get; set; }
        public float addonamountbasic_originalamt { get; set; }
        public string addonamountbasic_savepercentage { get; set; }
        public string addonamountbasic_saveperday { get; set; }
        
        public ImageSource images { get; set; }
        public string splitedvalues { get; set; }
    }


    public class Shoppingcartdetails_RT
    {
        public string name { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string billingcity { get; set; }
        public string billingzipcode { get; set; }
        public string billingaddress { get; set; }
        public string billingstatecode { get; set; }
        public string billingcountry { get; set; }
        public string amount { get; set; }
        public string userpid { get; set; }
        public string adid { get; set; }
        public string ip { get; set; }
        public string postedviaid { get; set; }
        public string couponcode { get; set; }
        public string totalamount { get; set; }
        public string usertype { get; set; }
        public string cardnumber { get; set; }
        public string expmnth { get; set; }
        public string expyear { get; set; }
        public string cvv { get; set; }

    }

    public class shoppingcart_RT
    {
        public int ordernumber { get; set; }
        public string shopingcartid { get; set; }
        public string errormsg { get; set; }
    }

    public class Payment_RT
    {
        public string name { get; set; }
        public string email { get; set; }
        public string orderid { get; set; }
        public string amount { get; set; }
        public string transid { get; set; }
        public string billingcity { get; set; }
        public string billingstatecode { get; set; }
        public string billingcountry { get; set; }
        public string adid { get; set; }
        public string ip { get; set; }
        public string shopingcartid { get; set; }
        public string usertype { get; set; }
        public string postedviaid { get; set; }
        public string couponcode { get; set; }
        public string totalamount { get; set; }
    }
    public class Paymentsucess_RT
    {
        public string resultinformation { get; set; }
    }

    public class Pkgsucess_RT
    {
        public string resultinformation { get; set; }
        public string discountamount { get; set; }
        public string totalamount { get; set; }
        public string packageamount { get; set; }
        public string isfreeadpublish { get; set; }
    }

    public class RTPackages_INFO_DATA
    {
        public string orderid { get; set; }
        public string amount { get; set; }
        public string adpostdate { get; set; }
        public string adexpirydate { get; set; }
        public string packagetype { get; set; }
        public string linkurl { get; set; }
        public string newcityurl { get; set; }
        public string adid { get; set; }
    }
    public class RTPackage_INFO
    {
        public List<RTPackages_INFO_DATA> ROW_DATA { get; set; }
    }

}
