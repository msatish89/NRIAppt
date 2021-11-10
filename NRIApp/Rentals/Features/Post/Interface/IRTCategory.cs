using System;
using System.Collections.Generic;
using System.Text;
using NRIApp.Rentals.Features.Post.Models;
using Refit;
using System.Threading.Tasks;

namespace NRIApp.Rentals.Features.Post.Interface
{
    interface IRTCategory
    {
        [Get("/rentalsapi.aspx?type=category&categorylevel={catlevelid}&supercategoryid={supcatid}&primarycategoryid={pricatid}&secondarycategoryid={seccatid}&maincategory=rentals")]
        Task<RTCategoryRowData> getcategory(int catlevelid, int supcatid, int pricatid, int seccatid);

        [Get("/LS.aspx?type=getlocation&term={locationname}")]
        Task<RTLocationRowData> getlocation(string locationname);

        [Post("/rentalsapi.aspx?type=submitotplcf&mobileverify={mobileverify}")]
        Task<RTOTP_LCF_Result> OTPResponselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]RTResponse rsLcf, string mobileverify);

        // [Post("/rentalsapi.aspx?type=submitlcf")]
        [Post("/rentalsapi.aspx?type=submitotplcf&mobileverify={mobileverify}")]
        Task<RTLCF_Result> Responselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]RTResponse rsLcf, string mobileverify);

        [Get("/rentalsapi.aspx?type=topbusiness&category={category}&fromdate={date}&cityurl={cityurl}")]
        Task<RTTop_Business> gettopbusiness(string category, string date, string cityurl);

        [Post("/rentalsapi.aspx?type=submitpostfirst")]
        Task<RTPostFirst_Result> postfirstsubmit([Body(BodySerializationMethod.UrlEncoded)] RTPostFirst  rsptf);

        [Post("/rentalsapi.aspx?type=submitotppostsecond")]
        Task<RTPost_OTP_Result> otppostsecondsubmit([Body(BodySerializationMethod.UrlEncoded)] RTPostFirst rsptf);

        [Post("/rentalsapi.aspx?type=submitpostsecond")]
        Task<RTPostFirst_Result> postsecondsubmit([Body(BodySerializationMethod.UrlEncoded)] RTPostFirst rsptf);

        [Get("/rentalsapi.aspx?type=resendotp&mobileno={phone}&country={countrycode}")]
        Task<RTPost_OTP_Result> getresendotp(string phone, string countrycode);

        [Get("/rentalsapi.aspx?type=search&title={title}&passingcityurl={city}")]
        Task<RentalsSearchList> getsearchresult(string title, string city);
        
        [Get("/rentalsapi.aspx?type=addmultiphotos&adid={adid}&imgname={imgname}")]
        Task<RTPostFirst_Result> insertphotos(string adid, string imgname);

        [Get("/rentalsapi.aspx?type=agentdetails&agentid={agentid}")]
        Task<AgentDetails_RT> getagentdetails(int agentid);

        [Get("/rentalsapi.aspx?type=loginpid&firstname={name}&logonname={email}")]
        Task<Login_PID_RT> getuserpid(string name, string email);

        //[Post("/roommatesapi.aspx?type=submitotplcfneedpost&mobileverify={mobileverify}")]
        //Task<RTPost_OTP_Result> OTPResponselcfsubmitneedpost([Body(BodySerializationMethod.UrlEncoded)] RTResponse rsLcf, string mobileverify);
    }
}
