using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using Newtonsoft.Json;

namespace NRIApp.Rentals.Features.List.Models
{
    public class ListRentalCategory
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<ListRentalCategoryData> ROW_DATA { get; set; }
    }

    public class ListRentalCategoryData
    {
        public int pageno { get; set; }
        public int totalrecs { get; set; }
        public string Postedago { get; set; }
        public int Adid { get; set; }
        public int Premiumad { get; set; }
        public string Adtype { get; set; }
        public string Statecode { get; set; }
        public string Title { get; set; }
        public string Shortdesc { get; set; }
        public string price1 { get; set; }
        public string price2 { get; set; }
        public int Priceto { get; set; }
        public int Pricetype { get; set; }
        public int Gender { get; set; }
        public string Roomtype { get; set; }
        public string City { get; set; }
        public string Thumbimgurl { get; set; }
        public string Availablefrm { get; set; }
        public int Rowstofetch { get; set; }
        public int Rowid { get; set; }
        public string Pricemode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Contactname { get; set; }
        public string Titleurl { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string sqfeet { get; set; }
        public string Negotiable { get; set; }
        public string noofrooms { get; set; }
        public string areamin { get; set; }
        public string orderby { get; set; }
        public double distance { get; set; }
        public double userlat { get; set; }
        public double userlong { get; set; }
        public string nearby { get; set; }
        public string noofbaths { get; set; }
        public bool distVisible { get; set; }
        public string distancedata { get; set; }
        public string streetname { get; set; }
        public bool thumburlvisible { get; set; }
        public string availablefrm { get; set; }
        public string postedago { get; set; }
        public string contactname { get; set; }
        public bool addressvisible { get; set; }
        public string hideaddress { get; set; }
        public string hiderent { get; set; }
        public string availablefromtext { get; set; }
        public bool parkingvisible { get; set; }
        //public string pricetext = "$";
        //public string priceFullText => string.Format("{0}{1}", pricetext, price1);
        //public string pricemodetext="/";
        //public string pricemodefulltext => string.Format("{0}{1}", pricemodetext, Pricemode);
    }

    public class RTDetailCategory
    {
        [JsonProperty("ROW_DATA")]
        public List<RTDetailCategoryData> RowData { get; set; }
    }
    public class RTDetailCategoryData
    {
        public string Contentid { get; set; }
        public string Adid { get; set; }
        public string Title { get; set; }
        public string Shortdesc { get; set; }
        public string Premiumad { get; set; }
        public string Gendertext { get; set; }
        public string Shortdescri { get; set; }
        public string City { get; set; }
        public string Streetname { get; set; }
        public double Citylat { get; set; }
        public double Citylong { get; set; }
        public string Primarytag { get; set; }
        public string Thumbimgurl { get; set; }
        public string Contactname { get; set; }
        public string Email { get; set; }
        public int Ismobileverified { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string Availablefrm { get; set; }
        public string Attachedbaths { get; set; }
        public string Smokepolicy { get; set; }
        public string Petpolicy { get; set; }
        public string Isfurnished { get; set; }
        public int Stayperiod { get; set; }
        public int Ismobileverified1 { get; set; }
        public int Isadsaved { get; set; }
        public string negotiable { get; set; }
        public int Accomodate { get; set; }
        public string Pricemode { get; set; }
        public string Isveg { get; set; }
        public DateTime Openhouseschema { get; set; }
        public string openhouseschedule { get; set; }
        public string Utilities { get; set; }
        public string Amenities { get; set; }
        public int Totalrecs { get; set; }
        public double Deposit { get; set; }
        public string newcityurl { get; set; }
        public string newtitleurl { get; set; }
        public string titleurl { get; set; }
        public string adtypetext { get; set; }
        public string nextnewtitleurl { get; set; }

        public string photourl { get; set; }
        public string Mobileno { get; set; }
        public string Phoneno { get; set; }
        public string Faxno { get; set; }
        public string Tags { get; set; }
        public long Price1 { get; set; }
        public long price2 { get; set; }
        public string Noofrooms { get; set; }
        public long sqfeet { get; set; }
        public string Videourl { get; set; }
       // public string Noofbaths { get; set; }
        public string openhousestarttime { get; set; }
        public string openhouseendtime { get; set; }
        public int hiderent { get; set; }
        public int hideaddress { get; set; }
        public string adtype { get; set; }
        public string roomtypetext { get; set; }
        public string accomodationtype { get; set; }
        public int roomtype { get; set; }
        public string areamin { get; set; }
        public string noofrooms { get; set; }
        public string noofbaths { get; set; }
    }
    public class ListOfRTImages
    {
        public List<RTThumbimgurl> ROW_DATA { get; set; }
    }
    public class RTThumbimgurl
    {
        public string Title { get; set; }
        public string Thumburl { get; set; }
        public string Photourl { get; set; }
        public int totalrecs { get; set; }
    }
    public class RTListofamenity
    {
        public List<RTamenity> ROW_DATA { get; set; }
    }
    public class RTamenity
    {
        public string category { get; set; }
    }
    public class RTListofutility
    {
        public List<RTutility> ROW_DATA { get; set; }
    }
    public class RTutility
    {
        public string category { get; set; }
    }
    public class RTSaveAD
    {
        [JsonProperty("ROW_DATA")]
        public List<RTSaveAdList> ROW_DATA { get; set; }
    }
    public class RTSaveAdList
    {
        public int totalcnt { get; set; }
        public int myadcnt { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string titleurl { get; set; }
        public string adid { get; set; }
        public string cityurl { get; set; }
    }
    public class RTDeleteAD
    {
        [JsonProperty("ROW_DATA")]
        public List<RTDeleteAdList> ROW_DATA { get; set; }
    }
    public class RTDeleteAdList
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
    public class RTReportflag
    {
        [JsonProperty("ROW_DATA")]
        public List<RT_ReportflagData> ROW_DATA { get; set; }
    }
    public class RT_ReportflagData
    {
        public string resultinfo { get; set; }
    }
}


