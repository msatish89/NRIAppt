using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalService.Features.Interfaces
{
    interface ILS_Listing
    {
        [Get("/LS.aspx?type=Primarytaglisting&adid={ptagid}&cityurl={cityurl}&pageno={pageno}&distfrom={distfrom}&distto={distto}&sortby={sortby}&openorclose={openorclose}&userlat={userlat}&userlong={userlong}")]
        Task<LS_LISTINGMODEL> GetPrimaryListing(string ptagid,string cityurl,string pageno,int distfrom,int distto,string sortby,int openorclose,string userlat,string userlong);

        [Get("/LS.aspx?type=getServiceLeaftypes&contentid={id}")]
        Task<SERVICE_LEAF_TYPES> GetServiceLeaftypes(string id);


        [Post("/serviceapi.aspx?type=LcfRespsubmitOTP&mobileverify={mobileverify}&deviceid={deviceid}&devicetypeid={devicetypeid}&devicename={devicename}")]
        Task<LS_LEAD_OTP_RESULT> PostRespForm([Body(BodySerializationMethod.UrlEncoded)] LS_RESP_FORM lt, string mobileverify,string deviceid, string devicetypeid, string devicename);

        [Get("/serviceapi.aspx?type=GetListingSearchData&ptagid={ptagid}&cityurl={cityurl}")]
        Task<LISTING_SEARCH_DEFAULT> GetListingSearchData(string ptagid,string cityurl);

        [Get("/serviceapi.aspx?type=GetListingSearchajax&searchtxt={searchtxt}&cityurl={cityurl}&metrourl={metrourl}&lat={lat}&longitude={longitude}")]
        Task<LISTING_SEARCH_DEFAULT> GetListingSearchajax(string searchtxt, string cityurl, string metrourl, string lat, string longitude);

        [Get("/LS.aspx?type=getonlineclasses&tagurl={tagurl}&pageno={pageno}")]
        Task<LSOnlinedata> Getonlineclasses(string tagurl, string pageno);

        [Get("/LS.aspx?type=getonlineclassessingleinfo&contentid={contentid}&pid={pid}")]
        Task<LSOnlinedetailData> Getonlineclassessingleinfo(string contentid, string pid);

        [Get("/LS.aspx?type=getinstructordetails&classmasterid={classmasterid}")]
        Task<LSInstructor> Getinstructordetails(string classmasterid);

        [Get("/LS.aspx?type=getclasstimings&classmasterid={classmasterid}")]
        Task<Classtimingsdata> Getclasstimings(string classmasterid);

        [Get("/LS.aspx?type=getlearnings&classmasterid={classmasterid}")]
        Task<learnsdata> Getlearnings(string classmasterid);

        [Get("/LS.aspx?type=getrequirements&classmasterid={classmasterid}")]
        Task<requirementsdata> Getrequirements(string classmasterid);

        [Get("/LS.aspx?type=getfaq&classmasterid={classmasterid}")]
        Task<faqsdata> Getfaq(string classmasterid);

        [Get("/LS.aspx?type=addbookmark&classmasterid={classmasterid}&pid={pid}&email={email}&isbookmark={isbookmark}")]
        Task<bookamrkdata> Addbookmark(string classmasterid, string pid,string email, string isbookmark);
    }
    
}
