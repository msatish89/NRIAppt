using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NRIApp.DayCare.Features.Post.Models;

namespace NRIApp.DayCare.Features.Post.Interface
{
    public interface IDaycareposting
    {
        [Post("/daycareposting.aspx?type=submitpostfirst")]
        Task<Daycareposting_Result> postfirstsubmit([Body(BodySerializationMethod.UrlEncoded)] Daycareposting rsptf);

        [Get("/daycareposting.aspx?type=submitverification&businessid={businessid}&mobileno={mobileno}&countrycode={countrycode}&emailid={emailid}&usertype={usertype}&deviceid={deviceid}&userpid={userpid}&ip={ip}")]
        Task<Daycareposting_Result> mobileverification(string businessid,string mobileno,string countrycode,string emailid,string usertype,string deviceid,string userpid,string ip);

        [Get("/daycareposting.aspx?type=verifyotp&email={email}&userip={userip}&mobileno={mobileno}&cntrycode={cntrycode}&userpid={userpid}&adid={adid}&deviceos={deviceos}&deviceid={deviceid}")]
        Task<Daycareposting_Result> Verifyotp(string email,string userip, string mobileno, string cntrycode,string userpid,string adid,string deviceos,string deviceid);

        [Post("/daycareposting.aspx?type=submitpostsecond")]
        Task<Daycareposting_Result> postsecondsubmit([Body(BodySerializationMethod.UrlEncoded)] Daycareposting rsptf);
        
        [Post("/daycareposting.aspx?type=renewupdate&adid={adid}&userpid={userpid}")]
        Task<Daycareposting_Result> renewupdate(string adid,string userpid);

        [Post("/daycareposting.aspx?type=dcpayment")]
        Task<NRIApp.LocalJobs.Features.Posting.Models.JobPayment> paymentpostsecondsubmit([Body(BodySerializationMethod.UrlEncoded)] LocalJobs.Features.Posting.Models.JobPaymentdetails rsptf);

        [Get("/daycareposting.aspx?type=agegroup")]
        Task<careservice> getagegroup();

        [Get("/daycareposting.aspx?type=getnewneedservice&categoryurl={categoryurl}")]
        Task<careservice> getneedservicelist(string categoryurl);

        [Get("/daycareposting.aspx?type=getnewneedservice&categoryurl={categoryurl}&businessid={businessid}")]
        Task<careservice> getnewneedservice(string categoryurl, string businessid);

        [Get("/daycareposting.aspx?type=providerserviceprefill&businessid={businessid}")]
        Task<careservice> prefillneedservicelist(string businessid);

        [Get("/daycareposting.aspx?type=getqanda&businessid={businessid}&mandatory={mandatory}")]
        Task<qanda> getqandalist(string businessid,string mandatory);
        

        [Post("/daycareposting.aspx?type=resendotp")]
        Task<results_otp> Resendotp([Body(BodySerializationMethod.UrlEncoded)] results rsptf);

        [Post("/daycareposting.aspx?type=verifyotp&adid={adid}&email={email}&userpid={userpid}&mobileno={mobileno}&cntrycode={cntrycode}&deviceid={deviceid}&deviceos={deviceos}&userip={userip}")]
        Task<results_otp> otpsubmitForm(string adid, string email, string userpid, string mobileno, string cntrycode, string deviceid, string deviceos, string userip);

        [Get("/daycareposting.aspx?type=resendotp&mobileno={mobileno}&cntrycode={cntrycode}")]
        Task<Daycareposting_Result> Resendotp(string mobileno, string cntrycode);

        [Get("/daycareposting.aspx?type=editphno&respid={respid}&mobileno={mobileno}&countrycode={countrycode}")]
        Task<Daycareposting_Result> Editphno(string respid, string mobileno, string countrycode);
        
        //otherserviceprovider
        [Get("/daycareposting.aspx?type=getnewotherprovider&businessid={businessid}")]
        Task<Otherprovider> getotherprovider(string businessid);

        [Get("/daycareposting.aspx?type=imgsandvideolinks&businessid={businessid}&videolink={videolink}&imglinks={imglinks}&userpid={userpid}")]
        Task<LocalJobs.Features.Detail.Models.resultinfo> addimgvlinks(string businessid,string videolink,string imglinks,string userpid);

        //getreceipt
        [Get("/daycareposting.aspx?type=getreceipt&businessid={businessid}&noofdays={noofdays}")]
        Task<Orderreceipt> getorderdetails(string businessid,string noofdays);

        //jobtype
        [Get("/daycareposting.aspx?type=jobtype&businessid={businessid}")]
        Task<careservice> gjobtype(string businessid);

    }
}
