using NRIApp.MyNeeds.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.MyNeeds.Features.Interfaces
{
    public interface IMyNeedData
    {
        [Get("/myneedsapi.aspx?type=lsneeds&userpid={userpid}&pageno={pageno}&rowstofetch=10")]
        Task<MyNeedDataDetails> GetLSadDetails(string userpid,string pageno);

        [Get("/myneedsapi.aspx?type=Lspartialad&userpid={userpid}")]
        Task<Myneedpartialad> GetLSpartialadDetails(string userpid);

        [Get("/myneedsapi.aspx?type=Removepartiallisting&userpid={userpid}")]
        Task<MyneedsResult> RemoveLSpartialadDetails(string userpid);

        [Get("/myneedsapi.aspx?type=roommatesneeds&userpid={userpid}&pageno={pageno}&rowstofetch=10")]
        Task<MyNeedDataDetails> GetRMadDetails(string userpid, string pageno);

        [Get("/myneedsapi.aspx?type=rentalsneeds&userpid={userpid}&pageno={pageno}&rowstofetch=10")]
        Task<MyNeedDataDetails> GetRTadDetails(string userpid, string pageno);

        [Get("/myneedsapi.aspx?type=daycareneeds&userpid={userpid}&pageno={pageno}&rowstofetch=10")]
        Task<MyNeedDataDetails> GetDCadDetails(string userpid, string pageno);

        [Get("/myneedsapi.aspx?type=Eventsneeds&userpid={userpid}&pageno={pageno}&rowstofetch=10")]
        Task<MyNeedDataDetails> GetEventsadDetails(string userpid, string pageno);

        [Get("/myneedsapi.aspx?type=jobsneeds&userpid={userpid}&pageno={pageno}&rowstofetch=10")]
        Task<MyNeedDataDetails> getjobsneeds(string userpid, string pageno);

        [Get("/myneedsapi.aspx?type=lsenablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> EnableLSAd(string adid,string userpid,string lastmodify,string oldclpostid,string adstatus);

        [Get("/myneedsapi.aspx?type=rmenablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> EnableRMAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);

        [Get("/myneedsapi.aspx?type=rtenablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> EnableRTAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);

        [Get("/myneedsapi.aspx?type=lsdisablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> DisableLSAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);

        [Get("/myneedsapi.aspx?type=rmedisablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> DisableRMAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);

        [Get("/myneedsapi.aspx?type=rtdisablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> DisableRTAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);

        [Get("/myneedsapi.aspx?type=dcenablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> EnableDCAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);

        [Get("/myneedsapi.aspx?type=dcdisablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> DisableDCAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);
        
        [Get("/myneedsapi.aspx?type=jobsenablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> EnableJobsAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);

        [Get("/myneedsapi.aspx?type=jobsdisablead&adid={adid}&userpid={userpid}&lastmodifiedby={lastmodify}&oldclpostid={oldclpostid}&adstatus={adstatus}")]
        Task<AdResultData> DisableJobsAd(string adid, string userpid, string lastmodify, string oldclpostid, string adstatus);
       
        [Get("/myneedsapi.aspx?type=getjobaddetails&adid={adid}&usertype={usertype}&email={email}&service={service}")]
        Task<EditAdDetail> getadDetails(string adid, string usertype, string email,string service);
        
        [Get("/myneedsapi.aspx?type=getjobaddetails&adid={adid}&usertype={usertype}&email={email}&service={service}")]
        Task<MyNeedsDC> getaddaycareDetails(string adid, string usertype, string email, string service);
        
        [Get("/myneedsapi.aspx?type=getadresponsedetails&adid={adid}&titleurl={titleurl}&service={service}&userpid={userpid}")]
        Task<Ad_Response> getadresponsedetails(string adid, string titleurl,string service,string userpid);

        [Get("/myneedsapi.aspx?type=getreplytoresponsedetails&adid={adid}&respid={respid}&userpid={userpid}&emailid={emailid}&service={service}")]
        Task<Ad_Response> getadresponsereplydetails(string adid, string respid ,string userpid, string emailid , string service);

        [Post("/myneedsapi.aspx?type=adreply")]
        Task<AdResultData> adreply([Body(BodySerializationMethod.UrlEncoded)] Ad_Response_Data rsLcf);

        [Get("/myneedsapi.aspx?type=getreceipt&service={service}&contentid={contentid}&titleurl={titleurl}")]
        Task<Adreceipt> getreceipt(string contentid, string titleurl, string service);
        
        [Get("/myneedsapi.aspx?type=sendreceipt&adid={adid}&userpid={userpid}&emailid={emailid}&service={service}&titleurl={titleurl}")]
        Task<AdResultData> sendreceipt(string adid,  string userpid, string emailid, string service,string titleurl);

        //[Get("/myneedsapi.aspx?type=sendemailverification&email={email}&userpid={userpid}&userid={userid}&name={name}")]
        //Task<AdResultData> sendmailverifymail(string email, string name, string userpid, string userid);

        [Get("/postanadv2/MobileApp/postadcommonapp.aspx?type=sendemailverification&email={email}&userpid={userpid}&userid={userid}&name={name}")]
        Task<AdResultData> sendmailverifymail(string email, string name, string userpid, string userid);

        [Get("/myneedsapi.aspx?type=getlsresponse&email={email}")]
        Task<LSResponse> getlsresponse(string email);
    }
}
