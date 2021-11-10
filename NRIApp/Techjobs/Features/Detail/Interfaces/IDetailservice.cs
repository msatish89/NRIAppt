using NRIApp.Techjobs.Features.Detail.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.Techjobs.Features.Detail.Interfaces
{
   public interface IDetailservice
    {
        [Get("/techjobsapi.aspx?type=trainingdates&moduleid={moduleid}&bizid={businessid}")]
        Task<Trainingdates> Gettrainingdates(string moduleid, string businessid);

        [Get("/techjobsapi.aspx?type=moduledetails&moduleid={moduleid}&bizid={businessid}&pid={pid}")]
        Task<ModuleDetaildata> Getmoduledetails(string moduleid, string businessid,string pid);

        [Get("/techjobsapi.aspx?type=services&moduleid={moduleid}&bizid={businessid}")]
        Task<Services> Getservices(string moduleid, string businessid);

        [Get("/techjobsapi.aspx?type=trainingvideos&moduleid={moduleid}&bizid={businessid}")]
        Task<Videos> Gettrainingvideos(string moduleid, string businessid);

        [Get("/techjobsapi.aspx?type=bizmodules&bizid={businessid}&pageno={pageno}")]
        Task<Bizmodules> Getbizmodules(string businessid, string pageno);

        [Post("/techjobsapi.aspx?type=postreview")]
        [Headers("Techjobs-Business Page: Review-Post")]
        Task<Reviewresult> Postreview([Body(BodySerializationMethod.UrlEncoded)] Addreview requestObject);

        [Get("/techjobsapi.aspx?type=bizreviews&moduleid={moduleid}&bizid={businessid}")]
        Task<Getreviews> Getmodulereviews(string moduleid, string businessid);

        [Get("/leadapi.aspx?type=verifyph&code={code}&mobileno={mobileno}")]
        Task<OTPdetails> Verifyphone(string code, string mobileno);

        [Get("/techjobsapi.aspx?type=reviewsbyid&reviewid={reviewid}")]
        Task<Getreviews> Getreviewsbyid(string reviewid);

        [Get("/techjobsapi.aspx?type=verifyotp&code={code}&mobileno={mobileno}&reviewid={reviewid}")]
        Task<OTPdetails> Verification(string code, string mobileno, string reviewid);

        [Post("/techjobsapi.aspx?type=addreply")]
        [Headers("Techjobs-Review Page: Add-Comment")]
        Task<Resultinfo> Addcomment([Body(BodySerializationMethod.UrlEncoded)] Comment requestObject);

        [Get("/techjobsapi.aspx?type=getreply&reviewid={reviewid}&bizid={businessid}")]
        Task<GetReplies> Getreplys(string reviewid, string businessid);

        [Get("/techjobsapi.aspx?type=deletefavorite&pid={pid}&pageurl={pageurl}")]
        Task<Result> Deletefavorite(string pid, string pageurl);

        [Get("/techjobsapi.aspx?type=savefavorite&pid={pid}&pageurl={pageurl}&businessid={businessid}&courseid={courseid}&moduleid={moduleid}&title={title}&country={country}")]
        Task<Result> Savefavorite(string pid, string pageurl,string businessid,string courseid,string moduleid,string title,string country);
    }
}
