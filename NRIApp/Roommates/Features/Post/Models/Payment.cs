using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Roommates.Features.Post.Models
{
   public class Shoppingcartdetails
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

    public class shoppingcart
    {
        public int ordernumber { get; set; }
        public string shopingcartid { get; set; }
        public string errormsg { get; set; }
    }

    public class Payment
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
    public class Paymentsucess
    {
        public string resultinformation { get; set; }
    }
}
