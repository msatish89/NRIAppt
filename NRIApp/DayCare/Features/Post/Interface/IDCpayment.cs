
using NRIApp.Roommates.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.DayCare.Features.Post.Interface
{
    public interface IDCpayment
    {
        //[Get("/jobslisting-api.aspx?type=getlistings")]
        //Task<LocalJob> Getjoblist(LocalJobSenddata jobdata);

        //[Post("/daycareapi.aspx?type=submitlcf")]
        //Task<DC_ListofResult> Responselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]DC_CategoryList rsLcf);

        [Get("/daycareposting.aspx?type=applycoupon&businessid={businessid}&couponcode={couponcode}&ordergroup={ordergroup}&numberofdays={numberofdays}&summercamp={summercamp}")]
        Task<Pkgsucess> applycouponcode(string businessid, string couponcode, string ordergroup,string numberofdays, string summercamp);

        [Post("/jobsposting-api.aspx?type=authorizepayment")]
        [Headers("shoppingcart-Page: shoppingcart-Post")]
        Task<NRIApp.LocalJobs.Features.Posting.Models.JobPayment> Saveshoppingcart([Body(BodySerializationMethod.UrlEncoded)]  NRIApp.LocalJobs.Features.Posting.Models.JobPaymentdetails requestObject);
    }
}
