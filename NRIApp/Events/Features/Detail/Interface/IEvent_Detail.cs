using NRIApp.Events.Features.Detail.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.Events.Features.Detail.Interface
{
    interface IEvent_Detail
    {
        [Get("/eventsapi.aspx?type=EventsDetails&eventid={eventid}&userpid={userpid}")]
        Task<EVENT_DETAILS> GetEventsDetails(string eventid, string userpid);

        [Get("/eventsapi.aspx?type=SaveBizfavorite&adid={adid}&pid={pid}&issaved={issaved}&ticketingid={ticketingid}")]
        Task<EVENT_FAVORITE_SAVED> SaveandremoveBizfavorite(string adid, string pid, string issaved,string ticketingid);

        [Get("/serviceapi.aspx?type=Sendreport&adid={adid}&userpid={pid}&useremail={email}&reportid={reportid}&rptemail={reprtmail}&rptdesc={reportdesc}&adtitle={adtitle}&adurl={adurl}&userid={userid}")]
        Task<EVENT_REPORT> Sendreport(string reportid, string reprtmail, string reportdesc, string adid, string adtitle, string adurl, string pid, string email, string userid);

        [Get("/eventsapi.aspx?type=Eventsartist&eventid={eventid}")]
        Task<EVENT_ARTIST> GetEventsartist(string eventid);

        [Get("/eventsapi.aspx?type=Trackbuyingtickets&eventid={seventid}&userpid={userPid}&deviceos={deviceos}")]
        Task<EVENT_RESULT> Trackbuyingtickets(string seventid, string userPid,string deviceos);

        [Post("/rsvp_process.aspx")]
        Task<EVENT_DETAILS_REGISTER> rsvpregister([Body(BodySerializationMethod.UrlEncoded)] EVENT_TICKET_DETAILS lt);

        [Get("/eventsapi.aspx?type=getuserpidandguidbyemail&email={email}")]
        Task<EVENT_USER_DETAILS> getuserpidandguidbyemail(string email);
        [Get("/eventsapi.aspx?type=getEventtimezones&eventid={eventid}")]
        Task<EVENT_TIMEZONE> GetEventtimezones(string eventid);

        [Get("/eventsapi.aspx?type=Eventtickiting&ticketid={ticketid}")]
        Task<EVENT_TICKETING_DETAILS> GetEventtickiting(string ticketid);

        [Post("/eventspayment.aspx?type=ticket_Cal")]
        Task<Ticket_Calculation> GetCaluculation([Body(BodySerializationMethod.UrlEncoded)] Sendpaymentchoosedetails slp);

        [Post("/eventspayment.aspx?type=addorder")]
        Task<Checkout_Result> Addshopingcartorder([Body(BodySerializationMethod.UrlEncoded)] Event_Checkout_Parameters ecp);
        [Post("/ticketingprocess.aspx")]
        Task<paymentresult> Submitfinalorder([Body(BodySerializationMethod.UrlEncoded)] Ticketing_final_submit_paraams tfs);
        [Get("/eventsapi.aspx?type=Checkoutdetails&orderid={checkoutid}")]
        Task<Checkout_Details> Checkoutdetails(string checkoutid);
    }
}
