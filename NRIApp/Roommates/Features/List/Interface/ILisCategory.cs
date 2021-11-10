using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.List.Models;
using NRIApp.Roommates.Features.List.ViewModels;
using NRIApp.Roommates.Features.Post.Models;

namespace NRIApp.Roommates.Features.List.Interface
{
    interface ILisCategory
    {
        [Get("/roommatesapi.aspx?type=listing&citystateurl={cityurl}&rowstofetch=10")]
        Task<ListRowData> Getroomlist(Response categoryurl, string cityurl);
        //[Get("/roommatesapi.aspx?type=listing&citystateurl={cityurl}&distance=75&pageno=1&rowstofetch=100")]
        //Task<ListRowData> Getroomlist([Body(BodySerializationMethod.UrlEncoded)]Response category, string cityurl);

        [Get("/roommatesapi.aspx?type=getphotourl&adid={adid}")]
        Task<ListOfImages> GetListofImages(string adid);
  
        [Get("/roommatesapi.aspx?type=getsingledatadetail&contentid={contentid}&pid={pid}")]
        Task<DetailList> GetDetailListData(string contentid,string pid);
   
        [Get("/roommatesapi.aspx?type=getamenities&adid={adid}")]
        Task<Listofamenity> GetamenityListofData(string adid);
   
        [Get("/roommatesapi.aspx?type=getutilities&adid={adid}")]
        Task<Listofutility> GetutilityListofData(string adid);
   
        [Get("/roommatesapi.aspx?type=saveadlist&adid={adid}&email={email}&pid={pid}")]
        Task<RMSaveAD> SaveAdData(string adid,string email,string pid);

        [Get("/roommatesapi.aspx?type=deleteadlist&adid={adid}&pid={pid}")]
        Task<RMDeleteAD> DeleteAdData(string adid,string pid);
   
        [Get("/roommatesapi.aspx?type=reportsflag&adid={adid}&tuserid={tuserid}&adtitle={adtitle}&sitename={sitename}&mobileno={mobileno}&reportid={reportid}&adurl={adurl}&siteid={siteid}&shortdesc={flagreason}&email={contactemail}")]
        Task<RMReportflag> GetReport(string adid, string tuserid,string adtitle, string sitename, string mobileno,string reportid,string adurl,string siteid,string flagreason,string contactemail);
   
        [Post("/roommatesapi.aspx?type=submitotplcf")]
        Task<OTP_LCF_Result> OTPResponselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]Response rsLcf);

        [Post("/roommatesapi.aspx?type=submitresponse&mobileverify={mobileverify}")]
        Task<OTP_LCF_Result> Responsesubmit([Body(BodySerializationMethod.UrlEncoded)]Response rsLcf,string mobileverify);

    }
}
