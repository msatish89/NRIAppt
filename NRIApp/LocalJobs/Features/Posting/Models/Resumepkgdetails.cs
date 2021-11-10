using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class Resumepkgdetails
    {
        public string usertypeid { get; set; }
        public string businessname { get; set; }
        public string industry { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneno { get; set; }
        public string countrycode { get; set; }
        public string userpid { get; set; }
        public string postedvia { get; set; }
        public string ip { get; set; }
        public string totalresume { get; set; }
        public string totalamount { get; set; }
        public string totaladscount { get; set; }
        public string iscustompackage { get; set; }
        public string agentid { get; set; }
        public string ismobileverified { get; set; }
        public string pkgname { get; set; }
        public string appcouponcode { get; set; }
    }

    public class ResumePackages
    {
        public string amount { get; set; }
        public string ordertype { get; set; }
        public string noofdays { get; set; }
        public string numberofliveads { get; set; }
        public string packagename { get; set; }
    }
    public class ResumePackagelist
    {
        public List<ResumePackages> ROW_DATA { get; set; }
    }

    public class Industries
    {
        public string featurename { get; set; }
        public string contentid { get; set; }
    }

    public class Industrylist
    {
        public List<Industries> ROW_DATA { get; set; }
    }

    public class JobResult
    {
        public string agentid { get; set; }
        public string pkgamount { get; set; }
        public string noofdays { get; set; }
    }

    public class Resumepaymentdetails
    {
        public string name { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string billingcity { get; set; }
        public string billingzipcode { get; set; }
        public string billingaddress { get; set; }
        public string billingstatecode { get; set; }
        public string billingcountry { get; set; }
        public string userpid { get; set; }
        public string agentid { get; set; }
        public string ip { get; set; }
        public string postedviaid { get; set; }
        public string couponcode { get; set; }
        public string totalamount { get; set; }
        public string usertype { get; set; }
        public string cardnumber { get; set; }
        public string expmnth { get; set; }
        public string expyear { get; set; }
        public string cvv { get; set; }
        public string totalresume { get; set; }
        public string totaladscount { get; set; }
        public string iscustompackage { get; set; }
    }
    public class ResumePayment
    {
        public int ordernumber { get; set; }
        public string shopingcartid { get; set; }
        public string errormsg { get; set; }
    }

    public class Resume_INFO_DATA
    {
        public string orderid { get; set; }
        public string amount { get; set; }
        public string posteddate { get; set; }
        public string enddate { get; set; }
        public string resumescount { get; set; }
        public string adscount { get; set; }
    }
    public class Resume_INFO
    {
        public List<Resume_INFO_DATA> ROW_DATA { get; set; }
    }
}
