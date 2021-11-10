using NRIApp.LocalJobs.Features.Posting.Models;
using NRIApp.Roommates.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Posting.Interfaces
{
    interface IJobPayment
    {
        [Get("/jobsposting-api.aspx?type=getpackages&funareaid={funareaid}&roleid={roleid}&newcityurl={newcityurl}")]
        Task<JobPackagelist> getpackages(string funareaid, string roleid, string newcityurl);

        [Post("/jobsposting-api.aspx?type=authorizepayment")]
        [Headers("shoppingcart-Page: shoppingcart-Post")]
        Task<JobPayment> Saveshoppingcart([Body(BodySerializationMethod.UrlEncoded)] JobPaymentdetails requestObject);

        //postsuccess
        [Get("/jobsposting-api.aspx?type=getpaymentinfo&adid={adid}&usertype={usertype}")]
        Task<JobPackage_INFO> getpaymentinfo(string adid, string usertype);

        //publishad
        [Get("/jobsposting-api.aspx?type=publishad&adid={adid}")]
        Task<Pkgsucess> publishad(string adid);
        //applycoupon
        [Get("/jobsposting-api.aspx?type=applycoupon&adid={adid}&couponcode={couponcode}&ordertype={ordertype}&citycnt={citycnt}&nationwideamount={nationwideamount}&banneramount={banneramount}&addonamount={addonamount}&emailblastamount={emailblastamount}&bonusdays={bonusdays}")]
        Task<Pkgsucess> applycouponcode(string adid, string couponcode, string ordertype,string citycnt,string nationwideamount, decimal banneramount, decimal addonamount, decimal emailblastamount, string bonusdays);

        [Get("/jobsposting-api.aspx?type=getresumepackages")]
        Task<ResumePackagelist> Getresumepackages();

        [Get("/jobsposting-api.aspx?type=getindustries&srchtext={srchtext}")]
        Task<Industrylist> Getindustries(string srchtext);

        [Post("/jobsposting-api.aspx?type=insertresumepkgdetails")]
        [Headers("Insertresumepkgdetails-Page: Insertresumepkgdetails-Post")]
        Task<JobResult> Insertresumepkgdetails([Body(BodySerializationMethod.UrlEncoded)] Resumepkgdetails requestObject);

        [Get("/jobsposting-api.aspx?type=resumecoupon&totaladscount={totaladscount}&couponcode={couponcode}&totalresume={totalresume}&iscustompackage={iscustompackage}")]
        Task<Pkgsucess> applyresumecoupon(string totaladscount, string couponcode, string totalresume, string iscustompackage);

        [Post("/jobsposting-api.aspx?type=resumepayment")]
        [Headers("resumepayment-Page: resumepayment-Post")]
        Task<ResumePayment> Resumepayment([Body(BodySerializationMethod.UrlEncoded)] Resumepaymentdetails requestObject);

        [Get("/jobsposting-api.aspx?type=getresumepaymentinfo&agentid={agentid}")]
        Task<Resume_INFO> getresumepaymentinfo(string agentid);

        //getcouponcode
        [Get("/jobsresumelisting-api.aspx?type=getcouponcode")]
        Task<Jobseeker.Models.Resultinformation> Getcouponcode();
    }
}
