using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace NRIApp.Roommates.Features.List.Models
{

    public class ListRowData
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<ListData> ROW_DATA { get; set; }
    }
    public class ListData
    {
        public int pageno { get; set; }
        public int totalrecs { get; set; }
        public int Rowstofetch { get; set; }
        public int Rowid { get; set; }
        public int Adid { get; set; }
        public string Title { get; set; }
        public string Shortdesc { get; set; }
        public string Pricefrom { get; set; }
        public string Gender { get; set; }
        public int Roomtype { get; set; }
        public string City { get; set; }
        public string Pricemode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Titleurl { get; set; }
        public int Contentid { get; set; }
        public string Streetname { get; set; }
        public string Thumbimgurl { get; set; }
        public string Postedvia { get; set; }
        public int Hideaddress { get; set; }
        public int Countrycode { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Secondarycategoryvalue { get; set; }
        public string Availablefrm { get; set; }
        public string negotiable { get; set; }
        public string contactname { get; set; }
        public string Statecode { get; set; }
        public string openhousestarttime { get; set; }
        public string openhouseendtime { get; set; }
        public DateTime openhouseschema { get; set; }
        public string openhouseschedule { get; set; }
        public string openhouse { get; set; }
        public string attachedbaths { get; set; }
        public double userlat { get; set; }
        public double userlong { get; set; }
        public string nearby { get; set; }
        public double distance { get; set; }
        public string distancedata { get; set; }
        public string orderby { get; set; }
        public string postedago { get; set; }
        public bool distVisible { get; set; }
        public bool thumburlvisible { get; set; }
        public string adtype { get; set; }
        public string isadsaved { get; set; }
        public string isadsavedimg { get; set; }
        public int hiderent { get; set; }
        public bool addressvisible { get; set; }
        public string availablefromtext { get; set; }
        public bool Openhouseschemavisible { get; set; }
        //public string pricefromtext = "$";
        //public string pricefromFullText => string.Format("{0}{1}", pricefromtext, Pricefrom);
        //public string pricemodetext = "/";
        //public string pricemodefulltext => string.Format("{0}{1}", pricemodetext, Pricemode);
    }
    public class DetailList
    {
        [JsonProperty("ROW_DATA")]
        public List<DetailListData> RowData { get; set; }
    }

    public class DetailListData
    {
        public string Contentid { get; set; }
        public string Adid { get; set; }
        public string Title { get; set; }
        public string Newtitleurl { get; set; }
        public string Shortdesc { get; set; }
        public string Premiumad { get; set; }
        public string Gendertext { get; set; }
        public string Shortdescri { get; set; }
        public string City { get; set; }
        public string Streetname { get; set; }
        public string Country { get; set; }
        public string Countryurl { get; set; }
        public double Citylat { get; set; }
        public double Citylong { get; set; }
        public string Primarytag { get; set; }
        public string Pagedesc { get; set; }
        public string thumbimgurl { get; set; }
        public int Pricefrom { get; set; }
        public string Contactname { get; set; }
        public string Roomtype { get; set; }
        public string Email { get; set; }
        public int Ismobileverified { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Availablefrm { get; set; }
        public int Attachedbaths { get; set; }
        public string Smokepolicy { get; set; }
        public string Petpolicy { get; set; }
        public string Isfurnished { get; set; }
        public int Stayperiod { get; set; }
        public int Ismobileverified1 { get; set; }
        public int Isadsaved { get; set; }
        public string Negotiable { get; set; }
        public int Accomodate { get; set; }
        public string Pricemode { get; set; }
        public string Isveg { get; set; }
        public DateTime Openhouseschema { get; set; }
        public string openhouseschedule { get; set; }
        public string Utilities { get; set; }
        public string Amenities { get; set; }
        public int Totalrecs { get; set; }
        public double Deposit { get; set; }
        public string titleurl { get; set; }
        public string newcityurl { get; set; }
        public string newtitleurl { get; set; }
        public string adtypetext { get; set; }
        public string openhousestarttime { get; set; }
        public string openhouseendtime { get; set; }
        public int hiderent { get; set; }
        public int hideaddress { get; set; }
        public int adtype { get; set; }
    }
    public class ListOfImages
    {
        [JsonProperty("ROW_DATA")]
        public List<RMThumbimgurl> ROW_DATA { get; set; }
    }
    public class RMThumbimgurl
    {
        public string Title { get; set; }
        public string Thumburl { get; set; }
        public string Photourl { get; set; }
        public int totalrecs { get; set; }
    }
    public class Listofamenity
    {
        [JsonProperty("ROW_DATA")]
        public List<RMamenity> ROW_DATA { get; set; }
    }
    public class RMamenity
    {
        public string category { get; set; }
    }
    public class Listofutility
    {
        [JsonProperty("ROW_DATA")]
        public List<RMutility> ROW_DATA { get; set; }
    }
    public class RMutility
    {
        public string category { get; set; }
    }
    public class RMSaveAD
    {
        [JsonProperty("ROW_DATA")]
        public List<RMSaveAdList> ROW_DATA { get; set; }
    }
    public class RMSaveAdList
    {
        public int totalcnt { get; set; }
        public int myadcnt { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string titleurl { get; set; }
        public string adid { get; set; }
        public string cityurl { get; set; }
    }
    public class RMDeleteAD
    {
        [JsonProperty("ROW_DATA")]
        public List<RMDeleteAdList> ROW_DATA { get; set; }
    }
    public class RMDeleteAdList
    {
        public int totalcnt { get; set; }
        public int myadcnt { get; set; }
        public int issavedad { get; set; }
    }
    public class ReportFlag
    {
        public string reportlist { get; set; }
        public int reportid { get; set; }
    }
    public class RMReportflag
    {
        [JsonProperty("ROW_DATA")]
        public List<RM_ReportflagData> ROW_DATA { get; set; }
    }
    public class RM_ReportflagData
    {
        public string resultinfo { get; set; }
    }
    public class Report
    {
        public string siteid { get; set; }
        public string contactemail { get; set; }
        public string sitename { get; set; }
        public string adurl { get; set; }
        public string reportid { get; set; }
    }
    public class multiImage
    {
        public string img { get; set; }
    }

}

