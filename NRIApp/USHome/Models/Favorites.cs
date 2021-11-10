using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.USHome.Models
{
    class Favorites
    {
    }

    public class EVENT_FAVORITE
    {
        public List<EVENT_FAVORITE_DATA> ROW_DATA { get; set; }
    }

    public class EVENT_FAVORITE_DATA
    {

        public string totalrecs { get; set; }
        public string pageno { get; set; }
        public string rowid { get; set; }
        public string imageurl { get; set; }
        public string eventid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string venuecity { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string state { get; set; }
        public string statecode { get; set; }
        public string orgname { get; set; }
        public string eventvenue { get; set; }
        public string hostedby { get; set; }
        public string category { get; set; }
        public string evtdate { get; set; }
        public string mapurl { get; set; }
        public string type { get; set; }
        public string venue { get; set; }
        public string customdate { get; set; }
        public string venueurl { get; set; }
        public string ticketingid { get; set; }
        public string eventdate { get; set; }
        public string eventmonth { get; set; }
        public string eventdatemonth { get; set; }
        public string eventyear { get; set; }
        public string eventtime { get; set; }
        public string eventday { get; set; }
        public string citystatecode { get; set; }
    }
    public class RM_FAVORITE
    {
        public List<RM_FAVORITE_DATA> ROW_DATA { get; set; }
    }
    public class RM_FAVORITE_DATA
    {
        public string totalrecs { get; set; }
        public string pageno { get; set; }
        public string rowstofetch { get; set; }
        public string rowid { get; set; }
        public string contentid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string crdate { get; set; }
        public string AdStatus { get; set; }
        public string cityname { get; set; }
        public string statecode { get; set; }
        public string cityurl { get; set; }
        public string adid { get; set; }
        public string price1 { get; set; }
        public string price2 { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string search { get; set; }
        public string adtype { get; set; }
        public string gender { get; set; }
        public string roomtype { get; set; }
        public string premiumad { get; set; }
        public string displayphone { get; set; }
        public string metro { get; set; }
        public string contactname { get; set; }
        public string metrourl { get; set; }
        public string myadcnt { get; set; }
        public string citystatecode { get; set; }

    }
    public class SERVICES_FAVORITE
    {
        public List<SERVICES_FAVORITE_DATA> ROW_DATA { get; set; }
    }
    public class SERVICES_FAVORITE_DATA
    {
        public string totalrecs { get; set; }
        public string pageno { get; set; }
        public string rowstofetch { get; set; }
        public string rowid { get; set; }
        public string contentid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string crdate { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string newcityurl { get; set; }
        public string adid { get; set; }
        public string minfee { get; set; }
        public string maxfee { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string search { get; set; }
        public string adtype { get; set; }
        public string premiumad { get; set; }
        public string contactname { get; set; }
        public string tag { get; set; }
        public string adstatus { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string myadcnt { get; set; }
        public string primarytag { get; set; }
        public string servicewithid { get; set; }
        public string profileimgurl { get; set; }
        public string streetname { get; set; }
        public string pid { get; set; }
        public string citystatecode { get; set; }
    }

    public class TECHJOBS_FAVORITE
    {
        public List<TECHJOBS_FAVORITE_DATA> ROW_DATA { get; set; }
    }
    public class TECHJOBS_FAVORITE_DATA
    {
        public string userpid { get; set; }
        public string pagename { get; set; }
        public string pageurl { get; set; }
        public string title { get; set; }
        public string businessid { get; set; }
        public string businessname { get; set; }
        public string moduleid { get; set; }
        public string modulename { get; set; }
        public string contentid { get; set; }
        public string trainingmode { get; set; }
        public string mode { get; set; }
        public string moduleurl { get; set; }
        public string businesstitleurl { get; set; }
    }

    public class RESULTS
    {
        public string resultinformation { get; set; }
        public string adtitle { get; set; }
    }
}
