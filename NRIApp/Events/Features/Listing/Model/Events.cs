using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Events.Features.Listing.Model
{
    public class Events
    {

    }

    public class EVENTS_LISTING
    {
        public List<EVENTS_LISTING_DATA> ROW_DATA { get; set; }
    }
    public class EVENTS_LISTING_DATA
    {
        public int pageno { get; set; }
        public string rowstofetch { get; set; }
        public string objecttype { get; set; }
        public string rowid { get; set; }
        public string imageurl { get; set; }
        public int totalrecs { get; set; }
        public string eventid { get; set; }
        public string ticketingid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string eventdate { get; set; }
        public string eventtime { get; set; }
        public string orgname { get; set; }
        public string venue { get; set; }
        public string venueaddress { get; set; }
        public string venuecity { get; set; }
        public string venuestate { get; set; }
        public string venuecountry { get; set; }
        public string venuezip { get; set; }
        public string statecode { get; set; }
        public string cityname { get; set; }
        public string metro { get; set; }
        public string hostedby { get; set; }
        public string evtdate { get; set; }
        public string mapurl { get; set; }
        public string type { get; set; }
        public string customdate { get; set; }
        public string startprice { get; set; }
        public string soldout { get; set; }
        public string formateddate { get; set; }
        public string fulladdress { get; set; }
        public string ticketcost { get; set; }

        public bool issold { get; set; }
        public string eventmnth { get; set; }
        public string eventday { get; set; }
        public string category { get; set; }
        public bool categoryavail { get; set; }
        public bool soldtype { get; set; }

        public bool Isbuyavail { get; set; }
        public string tag { get; set; }
        public string header { get; set; }
        public string contentid { get; set; }
        public string islivestream { get; set; }
        public string btntext { get; set; }
        public bool islivestreamavail { get; set; }

        public string streaming { get; set; }

        public string streamingimg { get; set; }

    }

    public class EVENTS_BANNER
    {
        public List<EVENTS_BANNER_DATA> ROW_DATA { get; set; }
    }
    public class EVENTS_BANNER_DATA
    {
        public string title { get; set; }
        public string imgurl { get; set; }
        public string clickurl { get; set; }
        public string displayorder { get; set; }
        public string imgurl2 { get; set; }
        public string bannerday { get; set; }
        public string bannertime { get; set; }
        public string bannercity { get; set; }
        public string bannerstate { get; set; }
        public string displaymode { get; set; }
        public string imgurl2count { get; set; }
        public string eventid { get; set; }
    }
    public class HOT_EVENTS
    {
        public List<HOT_EVENTS_DATA> ROW_DATA { get; set; }
    }
    public class HOT_EVENTS_DATA
    {
        public string rowid { get; set; }
        public string eventid { get; set; }
        public string rankid { get; set; }
        public string eventtitle { get; set; }
        public string venueaddress { get; set; }
        public string eventdesc { get; set; }
        public string eventurl { get; set; }
        public string ticketurl { get; set; }
        public string eventdate { get; set; }
        public string startprice { get; set; }
        public string imageurl { get; set; }
        public string evtdate { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string statecode { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string lat { get; set; }
        public string venuename { get; set; }
        public string orgname { get; set; }
        public string eventvenueaddress { get; set; }
        public string zipcode { get; set; }
        public string eventtrend { get; set; }
        public string date { get; set; }
        public string month { get; set; }
        public string formateddtime { get; set; }
        public string minmax { get; set; }
        public string eventaddress { get; set; }
    }
    public class RESULT
    {
        public List<RESULT_DATA> ROW_DATA { get; set; }
    }
    public class RESULT_DATA
    {
        public string result { get; set; }
    }

    public class FILTER
    {
        public List<FILTER_DATA> ROW_DATA { get; set; }
    }
    public class FILTER_DATA
    {
        public string items { get; set; }
        public string itemscss { get; set; }
        public string url { get; set; }
    }

    public class FILTER_LISTING
    {
        public List<FILTER_LISTING_DATA> ROW_DATA { get; set; }
    }
    public class FILTER_LISTING_DATA
    {
        public string imageurl { get; set; }
        public string eventid { get; set; }
        public string ticketingid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string eventdate { get; set; }
        public string eventtime { get; set; }
        public string orgname { get; set; }
        public string venue { get; set; }
        public string venueaddress { get; set; }
        public string venuecity { get; set; }
        public string venuestate { get; set; }
        public string venuecountry { get; set; }
        public string venuezip { get; set; }
        public string statecode { get; set; }
        public string cityname { get; set; }
        public string metro { get; set; }
        public string hostedby { get; set; }
        //public string mapurl { get; set; }
        //public string type { get; set; }
        //public string customdate { get; set; }
        public string startprice { get; set; }
        public string soldout { get; set; }
        public string formateddate { get; set; }
        public string category { get; set; }
        public string fulladdress { get; set; }
        public string eventmnth { get; set; }
        public string eventday { get; set; }
        public bool categoryavail { get; set; }
        public string ticketcost { get; set; }
        public int pageno { get; set; }
        public int totalrecs { get; set; }
        public bool islivestreamavail { get; set; }
        public string streaming { get; set; }

        public string streamingimg { get; set; }
    }
}
