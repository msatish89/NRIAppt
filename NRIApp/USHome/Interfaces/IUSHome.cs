using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NRIApp.USHome.Models;
using NRIApp.Techjobs.Features.LeadForm.Models;

namespace NRIApp.USHome.Interfaces
{
   public interface IUSHome
    {
        [Get("/homeapi.aspx?type=techcourses&rowstofetch=11")]
        Task<Techjobslist> GetItechjobs();

        [Get("/techjobsapi.aspx?type=getloc&lat={lat}&longi={longi}")]
        Task<Userloc> Getuserloc(double lat, double longi);

        [Get("/techjobsapi.aspx?type=homesearchajax&searchtext={searchtext}&cityurl={cityurl}&lat={lat}&longitude={longitude}")]
        Task<HomeSearchlist> GetHomesearch(string searchtext, string cityurl,double lat,double longitude);

        [Get("/homeapi.aspx?type=lslist&cityurl={cityurl}")]
        Task<LSdata> GetLslist(string cityurl);

        [Get("/techjobsapi.aspx?type=popularcities")]
        Task<Cityajaxlist> Getpopularcities();

        [Get("/homeapi.aspx?type=eventsfavorite&pid={pid}")]
        Task<EVENT_FAVORITE> Geteventsfavorite(string pid);

        [Get("/homeapi.aspx?type=roommateandrentalfavorite&pid={pid}&servicetype={servicetype}")]
        Task<RM_FAVORITE> Getroommateandrentalfavorite(string pid,string servicetype);

        [Get("/homeapi.aspx?type=servicefavorite&pid={pid}")]
        Task<SERVICES_FAVORITE> Getservicefavorite(string pid);

        [Get("/homeapi.aspx?type=techjobsfavorite&pid={pid}")]
        Task<TECHJOBS_FAVORITE> Gettechjobsfavorite(string pid);

        [Get("/homeapi.aspx?type=getmobads&os={os}")]
        Task<MOB_ADS> Getmobileads(string os);

        //Notification
        [Get("/homeapi.aspx?type=Savedevicetoken&deviceid={deviceid}&token={token}")]
        Task<RESULTS> getnotification(string deviceid, string token);
    }
}
