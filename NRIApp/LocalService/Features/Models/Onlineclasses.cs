using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.LocalService.Features.Models
{
   public class Onlineclasses
    {
        public int pageno { get; set; }
        public int rowstofetch { get; set; }
        public int totalrecs { get; set; }
        public int rowid { get; set; }
        public string classmastertitle { get; set; }
        public string ptag { get; set; }
        public string classlevels { get; set; }
        public string classmode { get; set; }
        public string languages { get; set; }
        public string instructorname { get; set; }
        public string classtimings { get; set; }
        public string guserid { get; set; }
        public string classexperience { get; set; }
        public string sizeallowed { get; set; }
        public string seatsleft { get; set; }
        public string classdate { get; set; }
        public string classtime { get; set; }
        public string amount { get; set; }
        public string age { get; set; }
        public string minage { get; set; }
        public string maxage { get; set; }
        public string classlength { get; set; }
        public string classmasterid { get; set; }
    }

    public class LSOnlinedata
    {
      
        public List<Onlineclasses> ROW_DATA { get; set; }
    }

    public class LSOnlinedetails
    {
        public string adid { get; set; }
        public string crdate1 { get; set; }
        public string premiumad { get; set; }
        public string availablefrom { get; set; }
        public string availableto { get; set; }
        public string contactname { get; set; }
        public string profileimgurl { get; set; }
        public string contributor { get; set; }
        public string classstartdate { get; set; }
        public string classenddate { get; set; }
        public string userpid { get; set; }
        public string tags { get; set; }
        public string tag { get; set; }
        public string tagurl { get; set; }
        public string primarytag { get; set; }
        public string classmastertitle { get; set; }
        public string classmasertitleurl { get; set; }
        public string classmasterdescription { get; set; }
        public string classtimezone { get; set; }
        public string classtags { get; set; }
        public string categoryid { get; set; }
        public string classcategories { get; set; }
        public string leveltypeid { get; set; }
        public string classlevels { get; set; }
        public string classmodes { get; set; }
        public string languages { get; set; }
        public string sizeallowed { get; set; }
        public string seatsleft { get; set; }
        public string classlengthtime { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string timezone { get; set; }
        public string minage { get; set; }
        public string maxage { get; set; }
        public string actualamount { get; set; }
        public string discountamount { get; set; }
        public string guserid { get; set; }
        public string classexperience { get; set; }
        public string amount { get; set; }
        public string classtimings { get; set; }
        public string classdatetime { get; set; }
        public string classdate { get; set; }
        public string classtime { get; set; }
        public string startdatescsv { get; set; }
       public string issavedclass { get; set; }


    }

    public class LSOnlinedetailData
    {
        public List<LSOnlinedetails> ROW_DATA { get; set; }
    }
    public class LSInstructor
    {
        public List<LSOnlineinstructordetails> ROW_DATA { get; set; }
    }
    public class LSOnlineinstructordetails
    {
        public string instructorlang { get; set; }
        public string instructorexp { get; set; }
        public string instructorprofileimg { get; set; }
        public string instructoraudience { get; set; }
        public string instructorskills { get; set; }
    }

    public class Classtimings
    {
        public string classdetailid { get; set; }
        public string availablefrom { get; set; }
        public string availableto { get; set; }
        public string classtime { get; set; }
        public string timezones { get; set; }
        public string actualamount { get; set; }
        public string amount { get; set; }
        public string guserid { get; set; }
        public string isfirstclassfree { get; set; }
        public string sizeallowed { get; set; }
        public string classlength { get; set; }
        public string classremainingsize { get; set; }
        public string classavailtext { get; set; }
        public string classdate { get; set; }
    }

    public class Classtimingsdata
    {
        public List<Classtimings> ROW_DATA { get; set; }
    }
    public class timingzones
    {
        public string time { get; set; }
    }
   
    public class requirements
    {
        public string title { get; set; }
    }
    public class requirementsdata
    {
        public List<requirements> ROW_DATA { get; set; }
    }
    public class learns
    {
        public string whatwilllearn { get; set; }
    }
    public class learnsdata
    {
        public List<learns> ROW_DATA { get; set; }
    }
    public class faqs
    {
        public string questioncontent { get; set; }
        public string answercontent { get; set; }
    }
    public class faqsdata
    {
        public List<faqs> ROW_DATA { get; set; }
    }
public class bookamrk
    {
        public string resulyinfo { get; set; }
    }
    public class bookamrkdata
    {
        public List<bookamrk> ROW_DATA { get; set; }
    }
    public class LS_ONLINE_DETAIL
    {
        public List<LS_ONLINE_DETAIL_DATA> ROW_DATA { get; set; }
    }

    public class LS_ONLINE_DETAIL_DATA
    {
        public string pageno { get; set; }
        public string rowstofetch { get; set; }
        public string totalrecs { get; set; }
        public string rowid { get; set; }
        public string rowtid { get; set; }
        public string classmastertitle { get; set; }
        public string linkurl { get; set; }
        public string ptag { get; set; }
        public string classmasterdescription { get; set; }
        public string classmasterid { get; set; }
        public string leveltypeid { get; set; }
        public string classlevels { get; set; }
        public string classmodeid { get; set; }
        public string classmode { get; set; }
        public string languageid { get; set; }
        public string languages { get; set; }
        public string instructorid { get; set; }
        public string instructorname { get; set; }
        public string classtimings { get; set; }
        public string discountpercentage { get; set; }
        public string guserid { get; set; }
        public string imgurl { get; set; }
        public string classexperience { get; set; }
        public string sizeallowed { get; set; }
        public string seatsleft { get; set; }
        public string classlength { get; set; }
        public string minage { get; set; }
        public string maxage { get; set; }
        public string amount { get; set; }
        public string actualamount { get; set; }
        public string discountamount { get; set; }
        public string discounttype { get; set; }
        public string ispaid { get; set; }
        public string isbatch { get; set; }
        public string classdate { get; set; }
        public string classtime { get; set; }
        public string timezones { get; set; }
        public string classdetailid { get; set; }
        public string isbatchmultipledata { get; set; }
        public string classstartdate { get; set; }
        public string classenddate { get; set; }
        public string crdate { get; set; }
        public string isfirstclassfree { get; set; }

        public string avgstudensts { get; set; }

        public string avgage { get; set; }

        public string batchdate { get; set; }

        public string batchtime { get; set; }

        public string finalamount { get; set; }
        public string batchseatsleft { get; set; }

        public bool isbatchavail { get; set;  }

        public string sizeallowtxt { get; set; }

    }

    public class LS_ONLINE_ORDER_SUMMERY
    {
        public List<LS_ONLINE_ORDER_SUMMERY_DATA> ROW_DATA { get; set; }
    }

    public class LS_ONLINE_ORDER_SUMMERY_DATA
    {
        public string contentid { get; set; }
        public string category { get; set; }
        public string categoryurl { get; set; }
        public string primarytagurl { get; set; }
        public string categorydetail { get; set; }
        public string guserid { get; set; }
        public string amount { get; set; }
        public string originalamount { get; set; }
        public string discountamount { get; set; }
        public string htmlcode { get; set; }
        public string discountpercentage { get; set; }
        public string linkurl { get; set; }
        public string ispaid { get; set; }
        public string countcurrency { get; set; }
        public string countcurrencyconversion { get; set; }
        public string countcurrencyamount { get; set; }
    }

    public class LS_ONLINE_COUPON_RESULT
    {
        public List<LS_ONLINE_COUPON_RESULT_DATA> ROW_DATA { get; set; }
    }

    public class LS_ONLINE_COUPON_RESULT_DATA
    {
        public string couponstatus { get; set; }
        public string discountamount { get; set; }
        public string htmlcode { get; set; }
        public string coupondescription { get; set; }
        public string countcurrencycurrent { get; set; }
    }

    public class LS_ONLINE_PAYMENT_SUBMIT
    {
        public string userid { get; set; }
        public string userpid { get; set; }
        public string email { get; set; }
        public string conversionprocessid { get; set; }
        public string guid { get; set; }
        public string couponcodevalue { get; set; }
        public string ddlcountry { get; set; }
        public string cardnumber { get; set; }
        public string expmonth { get; set; }
        public string expyear { get; set; }
        public string cardname { get; set; }
        public string cvvno { get; set; }
        public string postedviaid { get; set; }
        public string postedvia { get; set; }


    }
    public class LS_ONLINE_PAYMENT_BILLING_DETAILS
    {
        public string contactname { get; set; }
        public string contactemail { get; set; }
        public string contactno { get; set; }
        public string conamount { get; set; }
        public string guid { get; set; }
        public string ddlcountry { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string statecode { get; set; }
        public string LocationName { get; set; }
        public string streetname { get; set; }
        public string postedviaid { get; set; }
        public string postedvia { get; set; }

    }

    public class BILLING_RESULT
    {
        public List<BILLING_RESULT_DATA> ROW_DATA { get; set; }
    }
    public class BILLING_RESULT_DATA
    {
        public string result { get; set; }
        public string conversionid { get; set; }
        public string guid { get; set; }

    }
    public class ONLINE_PAYMENT_RESULT_DATA
    {
        public string result { get; set; }
        public string conversionid { get; set; }
        public string guid { get; set; }

    }
    public class ONLINE_PAYMENT_THANKYOU_RESULT
    {
        public List<ONLINE_PAYMENT_THANKYOU_RESULT_DATA> ROW_DATA { get; set; }
    }

    public class ONLINE_PAYMENT_THANKYOU_RESULT_DATA
    {
        public string guid { get; set; }
        public string OrderID { get; set; }
        public string paid_amount { get; set; }
        public string htmlcode { get; set; }
        public string upddate { get; set; }
        public string archivedata { get; set; }
        public string linkurl { get; set; }
        public string paymentmode { get; set; }
        public string transid { get; set; }
        public string ispaid { get; set; }
        public string ptag { get; set; }
        public string insname { get; set; }

    }


    public class PARTIAL_AD_DETAILS
    {
        public List<PARTIAL_AD_DETAILS_DATA> ROW_DATA { get; set; }
    }

    public class PARTIAL_AD_DETAILS_DATA
    {
        public string title { get; set; }
        public string titleurl { get; set; }
        public string streetname { get; set; }
        public string shortdesc { get; set; }
        public string supertagid { get; set; }
        public string supertag { get; set; }
        public string supertagurl { get; set; }
        public string primarytagid { get; set; }
        public string primarytag { get; set; }
        public string primarysupertags { get; set; }
        public string primarytagsurl { get; set; }
        public string tags { get; set; }
        public string tagids { get; set; }
        public string profileimgurl { get; set; }
        public string bizimgurl { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
        public string email { get; set; }

        public string industryexp { get; set; }


    }
}
