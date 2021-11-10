using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Rentals.Features.List.Models;
using NRIApp.Rentals.Features.Post.Models;

namespace NRIApp.Rentals.Features.List.Interface
{
    interface IListRentalCategory
    {
        [Get("/rentalsapi.aspx?type=listing&citystateurl={cityurl}&distance=75&rowstofetch=10")]
        Task<ListRentalCategory> GetRentalList(RTResponse categoryurl, string cityurl);
  
        [Get("/rentalsapi.aspx?type=getsingledatadetail&adid={contentid}&pid={pid}")]
        Task<RTDetailCategory> GetRTDetailListData(string contentid,string pid);
 
        [Get("/rentalsapi.aspx?type=getphotourl&adid={adid}")]
        Task<ListOfRTImages> GetRTListofImages(string adid);
  
        [Get("/rentalsapi.aspx?type=getamenities&adid={adid}")]
        Task<RTListofamenity> GetamenityList(string adid);
  
        [Get("/rentalsapi.aspx?type=getutilities&adid={adid}")]
        Task<RTListofutility> GetutilityList(string adid);
 
        [Get("/rentalsapi.aspx?type=saveadlist&adid={adid}&email={email}&pid={pid}")]
        Task<RTSaveAD> SaveAdData(string adid, string email, string pid);
  
        [Get("/rentalsapi.aspx?type=deleteadlist&adid={adid}&pid={pid}")]
        Task<RTDeleteAD> DeleteAdData(string adid, string pid);
   
        [Get("/rentalsapi.aspx?type=reportsflag&adid={adid}&tuserid={tuserid}&adtitle={adtitle}&sitename={sitename}&mobileno={mobileno}&reportid={reportid}&adurl={adurl}&siteid={siteid}&shortdesc={flagreason}&email={contactemail}")]
        Task<RTReportflag> GetReport(string adid, string tuserid, string adtitle, string sitename, string mobileno, string reportid, string adurl, string siteid,string flagreason,string contactemail);
   
        [Post("/rentalsapi.aspx?type=submitotplcf")]
        Task<RTOTP_LCF_Result> OTPResponselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]RTResponse rsLcf);

        [Post("/rentalsapi.aspx?type=submitresponse&mobileverify={mobileverify}")]
        Task<RTOTP_LCF_Result> Responsesubmit([Body(BodySerializationMethod.UrlEncoded)]RTResponse rsLcf,string mobileverify);
    }

}
