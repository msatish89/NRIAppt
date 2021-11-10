using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.Post.Models;


namespace NRIApp.Roommates.Features.Post.Interface
{
    interface IRMCategory
    {
        [Get("/roommatesapi.aspx?type=category&categorylevel={catlevelid}&supercategoryid={supcatid}&primarycategoryid={pricatid}&secondarycategoryid={seccatid}&maincategory=roommates")]
        Task<CategoryRowData> getcategory(int catlevelid, int supcatid, string pricatid, int seccatid);

        [Get("/LS.aspx?type=getlocation&term={locationname}")]
        Task<LocationRowData> getlocation(string locationname);

        [Post("/roommatesapi.aspx?type=submitotplcf&mobileverify={mobileverify}")]
        Task<OTP_LCF_Result> OTPResponselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]Response rsLcf,string mobileverify);

        [Post("/roommatesapi.aspx?type=submitlcf")]
        Task<LCF_Result> Responselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]Response rsLcf);

        [Get("/roommatesapi.aspx?type=topbusiness&category={category}&fromdate={date}&gender={gender}&cityurl={cityurl}")]
        Task<Top_Business> gettopbusiness(string category, string date, string gender, string cityurl);

        [Post("/roommatesapi.aspx?type=submitpostfirst")]
        Task<PostFirst_Result> postfirstsubmit([Body(BodySerializationMethod.UrlEncoded)]Postfirst rsptf);

        [Post("/roommatesapi.aspx?type=submitotppostsecond")]
        Task<Post_OTP_Result> otppostsecondsubmit([Body(BodySerializationMethod.UrlEncoded)]Postfirst rsptf);

        [Post("/roommatesapi.aspx?type=submitpostsecond")]
        Task<PostFirst_Result> postsecondsubmit([Body(BodySerializationMethod.UrlEncoded)]Postfirst rsptf);

        [Get("/roommatesapi.aspx?type=resendotp&mobileno={phone}&country={countrycode}")]
        Task<Post_OTP_Result> getresendotp(string phone, string countrycode);

        [Get("/roommatesapi.aspx?type=searchmore&title={title}&passingcityurl={city}")]
        Task <SearchList> getsearchresult(string title,string city);
        
        [Get("/roommatesapi.aspx?type=searchmore&passingcityurl={city}")]
        Task<SearchList> getsearchmore(string city);

        [Get("/roommatesapi.aspx?type=addmultiphotos&adid={adid}&imgname={imgname}")]
        Task<PostFirst_Result> insertphotos(string adid,string imgname);

        //agentdetails
        [Get("/roommatesapi.aspx?type=agentdetails&agentid={agentid}")]
        Task<AgentDetails> getagentdetails(int agentid);

        [Get("/roommatesapi.aspx?type=loginpid&firstname={name}&logonname={email}")]
        Task<AutoLoginuserPID> getuserpid(string name, string email);

        //needpost
        [Post("/roommatesapi.aspx?type=submitneedpostfirst")]
        Task<PostFirst_Result> postneedfirstsubmit([Body(BodySerializationMethod.UrlEncoded)] Postfirst rsptf);
        //myneedlcfposting
        [Post("/roommatesapi.aspx?type=submitotplcfneedpost&mobileverify={mobileverify}")]
        Task<OTP_LCF_Result> OTPResponselcfsubmitneedpost([Body(BodySerializationMethod.UrlEncoded)] Response rsLcf, string mobileverify);
    }
}
