using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class JobPaymentdetails
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
            public string ordertype { get; set; }
            public string adcity { get; set; }
            public string adstate { get; set; }
            public string adzipcode { get; set; }
            public string adstatecode { get; set; }
            public string adlat { get; set; }
            public string adlong { get; set; }
            public string adcountry { get; set; }
            public string adname { get; set; }
            public string ademail { get; set; }
            public string adphone { get; set; }
       public string categoryflag { get; set; }
        public string nationwide { get; set; }
        public string bannerstatus { get; set; }
        public string addedcities { get; set; }
        public string banneramnt { get; set; }
        public string nationwideamnt { get; set; }
        public int citycnt { get; set; }

        public string addonamount { get; set; }
        public string emailblastamount { get; set; }
        public string bonusdays { get; set; }
        
        //daycare
        public string ordergroup { get; set; }
        public string noofdays { get; set; }
        public string summercamp { get; set; }
        public string discountamt { get; set; }
        //job
        public string mode { get; set; }
        public string pkgid { get; set; }
    }
    public class JobPayment
    {
        public int ordernumber { get; set; }
        public string shopingcartid { get; set; }
        public string errormsg { get; set; }
    }
}
