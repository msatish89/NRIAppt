using NRIApp.Events.Features.Detail.Model;
using NRIApp.Events.Features.Listing.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.Events.Features.Listing.Interface
{
    interface IEvents
    {
        [Get("/eventsapi.aspx?type=Eventslisting&lat={lat}&longitude={longitude}&pageno={pageno}")]
        Task<EVENTS_LISTING> GetEventsListing(string lat, string longitude, int pageno);

        [Get("/eventsapi.aspx?type=Eventshomebanners&metrourl={metrourl}")]
        Task<EVENTS_BANNER> GetEventshomebanners(string metrourl);

        [Get("/eventsapi.aspx?type=HotEvents&metrourl={metrourl}&cityurl={cityurl}&tagtype={tag}&pageno={pageno}")]
        Task<HOT_EVENTS> GetHotEvents(string metrourl,string cityurl,string tag,string pageno);

        [Get("/eventsapi.aspx?type=Shortlistevent&type={type}&eventid={eventid}&ticketingid={ticketingid}&pid={pid}")]
        Task<RESULT> GetShortlist(string type, string eventid,string ticketingid,string pid);

        [Get("/eventsapi.aspx?type=Getfiltercategory&metrourl={metrourl}")]
        Task<FILTER> Getcategory(string metrourl);

        [Get("/eventsapi.aspx?type=Getfilterlang&metrourl={metrourl}")]
        Task<FILTER> Getlanguages(string metrourl);
        Task Geteventsticketingdetails(string ticketid);
        [Get("/eventsapi.aspx?type=Getfilterlisting&metrourl={metrourl}&category={category}&language={language}&startprice={startprice}&endprice={endprice}&startdate={startdate}&enddate={enddate}&pageno={pageno}")]
        Task<FILTER_LISTING> GetFilterlisting(string metrourl,string category,string language,string startprice,string endprice,string startdate,string enddate,int pageno);

        [Get("/eventsapi.aspx?type=Eventsearchajax&searchtext={searchtext}")]
        Task<EVENTS_LISTING> Eventsearchajax(string searchtext);

        [Get("/eventsapi.aspx?type=Eventtickiting&ticketid={ticketid}")]
        Task<EVENT_TICKETING_DETAILS> Getticketingdetails(string ticketid);
    }
}
