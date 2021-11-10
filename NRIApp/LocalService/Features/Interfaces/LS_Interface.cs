using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;
using NRIApp.LocalService.Features.Models;

namespace NRIApp.LocalService.Features.Interfaces
{

    interface IServiceAPI
    {
        [Get("/LS.aspx?type=primarytaglist&pageno={pageno}")]
        Task<PRIMARYTAG_LIST> GetPrimaryData(int pageno);

        [Get("/LS.aspx?type=getLeaftypes&contentid={id}")]
        Task<LEAF_TAGS> GetLeaftypes(int id);

        [Post("/serviceapi.aspx?type=PostBusinessOTP&deviceid={deviceid}&devicetypeid={devicetypeid}&devicename={devicename}")]
        Task<BUSINESS_RESULT_DATA> SubmitBusiness([Body(BodySerializationMethod.UrlEncoded)] Business business,string deviceid,string devicetypeid,string devicename);

        [Get("/serviceapi.aspx?type=ResendOTP&mobileno={mobileno}&country={country}")]
        Task<RESULT_OTP> OTPResnd(string mobileno, string country);

        [Get("/LS.aspx?type=getlocation&term={location}")]
        Task<LOCATION> GetLocationData(string location);

        [Get("/LS.aspx?type=GetusandcanadaLocationData&term={location}")]
        Task<COMMON_LOCATION> GetusandcanadaLocationData(string location);

        [Post("/serviceapi.aspx?type=LcfleadsubmitOTP&mobileverify={mobileverify}&deviceid={deviceid}&devicetypeid={devicetypeid}&devicename={devicename}&userpid={userpid}")]
        Task<LS_LEAD_OTP_RESULT> PostLeadForm([Body(BodySerializationMethod.UrlEncoded)] LS_LEAD_FORM lt, string mobileverify,string deviceid,string devicetypeid,string devicename,string userpid);

        [Get("/LS.aspx?type=GetDistributedBusiness&adid={adid}")]
        Task<LS_LEAD_DISTRIBUTE_BIZ> GetDistributedBusiness(string adid);

        [Get("/LS.aspx?type=updateMobileverified&bizid={bizid}&verified={verified}")]
        Task UpdatePostMobileverified(string bizid, string verified);

        [Get("/LS.aspx?type=getSearchajax&searchtext={searchtext}")]
        Task<LS_SEARCH> Getsearchajax(string searchtext);

        [Get("/serviceapi.aspx?type=Postbizupdatecontact&bizid={bizid}&mobile={mobileno}&countrycode={countrycode}&deviceid={deviceid}")]
        Task UpdateMobilenumber(string bizid, string mobileno,string countrycode,string deviceid);

        [Get("/serviceapi.aspx?type=GetSearchcategories&cityurl={cityurl}")]
        Task<LS_SEARCH> GetSearchcategories(string cityurl);


        [Get("/serviceapi.aspx?type=GettingPackagedetails")]
        Task<LS_PACKAGE> GetPackageData();

        [Get("/LS.aspx?type=GetNearbycities&metrourl={metrourl}")]
        Task<LOCATION_MERGE> GetNearbycities(string metrourl);

        [Get("/LS.aspx?type=GetValueaddedservices&days={days}")]
        Task<LS_VALUE_ADD_SERVICE> GetValueaddedservices(string days);

        [Post("/serviceapi.aspx?type=Applycouponcode")]
        Task<LS_COUPON_SUCCESS> Apply_couponcode([Body(BodySerializationMethod.UrlEncoded)] COUPON_DATA lt);

        [Post("/serviceapi.aspx?type=OrderSummary")]
        Task<ORDER_SUMMARY_RESULT> OrderSummaryInsert([Body(BodySerializationMethod.UrlEncoded)] ORDER_SUMMARY lt);

        [Get("/LS.aspx?type=GetAdDetails&adid={adid}")]
        Task<LS_AD_DETAILS> GetAdDetails(string adid);

        [Post("/lspayment.aspx?type=Postpaymentdetails")]
        Task<PAYMENT_RESULT_DATA> Postpaymentdetails([Body(BodySerializationMethod.UrlEncoded)] LS_PAYMENT_DETAILS lt);

        [Post("/lspayment.aspx?type=authorizepayment")]
        Task<SHOPPING_CART_DATA> getShopingcartNumber([Body(BodySerializationMethod.UrlEncoded)] LS_PAYMENT_DETAILS lt);

        [Post("/lspayment.aspx?type=getFlexpayment")]
        Task<PAYMENT_RESULT_DATA> getFlexpayment([Body(BodySerializationMethod.UrlEncoded)] LS_FLEX_PAYMENT_DETAILS lt);

        [Get("/lspayment.aspx?type=Sendorderrecipet&adid={adid}")]
        Task<LS_RECEIPT> sendorderrecipet(string adid);

        [Get("/lspayment.aspx?type=Getorderrecipet&primeid={primeid}")]
        Task<LS_FLEX_RECEIPT> Getflexorderrecipet(string primeid);

        [Get("/lspayment.aspx?type=Publishflexad&oldclpostid={oldclpostid}&primeid={primeid}")]
        Task<FLEX_PUBLISH_SUCCESS_DATA> Publishflexad(string oldclpostid,string primeid);

        [Get("/serviceapi.aspx?type=Getcallotp&countrycode={countrycode}&mobileno={mobileno}")]
        Task<RESULT_OTP> Getcallotp(string countrycode,string mobileno);

        [Get("/serviceapi.aspx?type=Verifycallsmsotp&otp={pinno}")]
        Task<RESULT_OTP> Verifyotp(string pinno);

        [Get("/ls.aspx?type=Getaddetailsbyadid&adid={adid}")]
        Task<LS_LISTINGMODEL> Getaddetailsbyadid(string adid);

        [Get("/ls.aspx?type=Getonlineclasspackagedetails&guserid={guserid}")]
        Task<LS_ONLINE_ORDER_SUMMERY> Getonlineclasspackagedetails(string guserid);

        [Get("/ls.aspx?type=Apllyonlineclasscouponcode&guserid={guserid}&couponcode={couponcode}")]
        Task<LS_ONLINE_COUPON_RESULT> Apllyonlineclasscouponcode(string guserid,string couponcode);

        [Post("/categoryconversion.aspx?type=getbillingdetails")]
        Task<BILLING_RESULT_DATA> getbillingdetails([Body(BodySerializationMethod.UrlEncoded)] LS_ONLINE_PAYMENT_BILLING_DETAILS lt);

        [Post("/categoryconversion.aspx?type=payonlineclassamount")]
        Task<ONLINE_PAYMENT_RESULT_DATA> payonlineclassamount([Body(BodySerializationMethod.UrlEncoded)] LS_ONLINE_PAYMENT_SUBMIT lt);
        [Get("/ls.aspx?type=Getonlineclassorderrecipet&paymentgudid={paymentgudid}")]
        Task<ONLINE_PAYMENT_THANKYOU_RESULT> Getonlineclassorderrecipet(string paymentgudid);


        [Get("/serviceapi.aspx?type=Getpartialaddetails&oldclpostid={oldclpostid}")]
        Task<PARTIAL_AD_DETAILS> GetPartialaddetails(string oldclpostid);


        [Post("/serviceapi.aspx?type=Addpartialadcategory")]
        Task<BUSINESS_RESULT_DATA> Addpartialadcategory([Body(BodySerializationMethod.UrlEncoded)] Business business);

        [Get("/ls.aspx?type=GetAdDetails&adid={adid}")]
        Task<LS_FLEX_AD_PACKAGE_DETAILS> Getrenewpackagedetails(string adid);

        [Get("/serviceapi.aspx?type=Getprimeid&oldclpostid={oldclpostid}")]
        Task<LS_GET_PRIME_ID> Getprimeid(string oldclpostid);
        

    }

    
}
