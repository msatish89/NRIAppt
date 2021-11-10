using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Events.Features.Detail.Model
{
    class Detail_Model
    {
    }

    public class EVENT_DETAILS
    {
        public  List<EVENT_DETAILS_DATA> ROW_DATA {get;set;}
    }

    public class EVENT_DETAILS_DATA
    {
        public string title { get; set; }
        public string newtitleurl { get; set; }
        public string imgurl { get; set; }
        public string category { get; set; }
        public string shortlist { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string time { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string year { get; set; }
        public string organisername { get; set; }
        public string organisermobilenumber { get; set; }
        public string eventdescription { get; set; }
        public string termcondition { get; set; }
        public string faq { get; set; }
        public string ispaidad { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string ticketingid { get; set; }
        public string Venue { get; set; }
        public string soldout { get; set; }
        public string minmax { get; set; }
        public string ispayment { get; set; }
        public string livestream { get; set; }
        public string tickettypeid { get; set; }
        public string timezone { get; set; }
        public string streaming { get; set; }

        public string mobilemultidated { get; set; }

        public bool isavailmultidated { get; set; }
        public string eventsetup { get; set; }

        public string eventtype { get; set; }
        public string postedtype { get; set; }

        public string orderform { get; set; }
    }
    public class EVENT_TERMS
    {
        public string term { get; set; }
    }
    public class EVENT_FAQ
    {
        public string Heading { get; set; }
        public string Content { get; set; }
        public string image { get; set; }
        public bool detailblkvisible { get; set; }
    }

    public class EVENT_FAVORITE_SAVED
    {
        public List<EVENT_FAVORITE_SAVED_DATA> ROW_DATA { get; set; }
    }

    public class EVENT_FAVORITE_SAVED_DATA
    {
        //public string totalcnt { get; set; }
        //public string myadcnt { get; set; }
        //public string title { get; set; }
        //public string shortdesc { get; set; }
        //public string titleurl { get; set; }
        //public string adid { get; set; }
        //public string metro { get; set; }
        //public string metrourl { get; set; }
        //public string cityurl { get; set; }
        //public string issavedad { get; set; }

        public string result { get; set; }
    }
    public class EVENT_REPORT_LIST
    {
        public string reportlist { get; set; }
        public int reportid { get; set; }
    }
    public class EVENT_REPORT
    {
        public List<EVENT_REPORT_DATA> ROW_DATA { get; set; }
    }

    public class EVENT_REPORT_DATA
    {
        public string resultinfo { get; set; }
    }

    public class EVENT_ARTIST
    {
        public List<EVENT_ARTIST_DATA> ROW_DATA { get; set; }
    }

    public class EVENT_ARTIST_DATA
    {
        public string contentid { get; set; }
        public string artist { get; set; }
        public string artisturl { get; set; }
        public string imageurl { get; set; }
    }

    public class EVENT_TIMEZONE
    {
        public List<EVENT_TIMEZONE_DATA> ROW_DATA { get; set; }
    }

    public class EVENT_TIMEZONE_DATA
    {
        public string timezonename { get; set; }
        public string timezone { get; set; }
        public string monthday { get; set; }
        public string day { get; set; }
        public string time { get; set; }

        public string datetime { get; set; }
        public string city { get; set; }
        public string flag { get; set; }
        public string mobileflag { get; set; }
        public string min { get; set; }
        public string max { get; set; }
        public string htmlcode { get; set; }
        public string currency { get; set; }
    }

    public class EVENT_RESULT
    {
        public List<EVENT_RESULT_DATA> ROW_DATA { get; set; }
    }

    public class EVENT_RESULT_DATA
    {
        public string resultinformation { get; set; }
    }
    public class EVENT_DETAILS_REGISTER
    {
        public string name { get; set; }
        public string value { get; set; }
        public string errormsg { get; set; }
        public string errormsgdetails { get; set; }
        public string orderguid { get; set; }
        public string ordernumber { get; set; }
        public string ticketcount { get; set; }
        public string stage { get; set; }
    }

    public class EVENT_TICKET_DETAILS
    {
        public string txtfirstname { get; set; }
        public string txtlastname { get; set; }
        public string txthomephone { get; set; }
        public string txtworkphone { get; set; }
        public string txtaddress { get; set; }
        public string txtcity { get; set; }
        public string lbCountry { get; set; }
        public string txtstate { get; set; }
        public string txtcanadastate { get; set; }
        public string txtindiastate { get; set; }
        public string txtzip { get; set; }
        public string txtemail { get; set; }
        public string hdn_promocode { get; set; }

        public string txt_couponcode { get; set; }
        public string txtcardname { get; set; }
        public string hdn_objectid { get; set; }
        public string txtName { get; set; }
        public string txtticketingtypeid { get; set; }
        public string Userguid { get; set; }
        public string Userpid { get; set; }



    }

    public class EVENT_USER_DETAILS
    {
        public List<EVENT_USER_DETAILS_DATA> ROW_DATA { get; set; }
    }

    public class EVENT_USER_DETAILS_DATA
    {
        public string pid { get; set; }
        public string guid { get; set; }
    }

    public class Event_Times
    {
        public string date { get; set; }

        public string time { get; set; }
    }

    public class Event_Checkout_Parameters
    {
        public string Ticketingid { get; set; }

        public string Parameters { get; set; }

        public string utmsource { get; set; }

        public string utmcampaign { get; set; }

        public string isseated { get; set; }

        public string promocode { get; set; }
        public string loadtime { get; set; }

        public string loginpid { get; set; }

        public string postedvia { get; set; }

        public string postedviaid { get; set; }

        public string devicemodel { get; set; }
    }

}
