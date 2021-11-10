using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalService.Features.Interfaces
{
    interface ILS_Detailpage
    {
        [Get("/serviceapi.aspx?type=GetDetailpagedata&adid={adid}&cityurl={cityurl}&titleurl={titleurl}&userpid={pid}")]
        Task<LS_DETAIL> GetDetaildata(string adid, string cityurl,string titleurl,string pid);


        [Get("/serviceapi.aspx?type=Getltypedata&adid={adid}&ltype={ltype}")]
        Task<LS_LTYPE> Getltypedata(string adid, string ltype);

        [Get("/serviceapi.aspx?type=Getadcities&adid={adid}")]
        Task<LS_AD_CITIES> Getadcities(string adid);
        [Get("/serviceapi.aspx?type=GetVideos&adid={adid}")]
        Task<LS_AD_VIDEOS> GetVideos(string adid);
        [Get("/serviceapi.aspx?type=GetPhotos&adid={adid}")]
        Task<LS_AD_PHOTOS> GetPhotos(string adid);
        [Get("/serviceapi.aspx?type=GetAdServices&adid={adid}")]
        Task<LS_AD_SERVICES> GetAdServices(string adid);

        [Get("/serviceapi.aspx?type=GetAdReviews&adid={adid}&pageno={pageno}")]
        Task<LS_REVIEWS> GetAdReviews(string adid, string pageno);

        [Get("/serviceapi.aspx?type=PublishReview&adid={adid}&Rating={Rating}&contributor={contributor}&loggedphotourl={loggedphotourl}&loginpid={loginpid}&title={title}&shortdesc={shortdesc}&adurl={adurl}&ptagid={ptagid}&ptag={ptag}&ptagurl={ptagurl}&stagid={stagid}&stag={stag}&stagurl={stagurl}&city={city}&state={state}&zip={zip}&lat={lat}&longtitude={longtitude}&country={country}")]
        Task<LS_REVIEW_POST> PublishReview(string adid, string Rating,string contributor,string loggedphotourl,string loginpid,string title,string shortdesc,string adurl,string ptagid,string ptag,string ptagurl,string stagid,string stag,string stagurl,string city,string state,string zip,string lat,string longtitude,string country);

        [Get("/serviceapi.aspx?type=GetAdDetailReview&reviewid={reviewid}")]
        Task<LS_REVIEWS> GetAdDetailReview(string reviewid);

        [Get("/serviceapi.aspx?type=GetAdReviewReply&reviewid={reviewid}")]
        Task<LS_REVIEW_REPLY> GetAdReviewReply(string reviewid);

        [Get("/serviceapi.aspx?type=Postreviewreply&reviewid={reviewid}&adid={adid}&desc={desc}&contributor={contributor}&userpid={userpid}")]
        Task<LS_REVIEW_REPLY> PostReviewReply(string reviewid,string adid,string desc,string contributor, string userpid);

        [Get("/serviceapi.aspx?type=SaveBizfavorite&adid={adid}&pid={pid}&email={email}&issaved={issaved}")]
        Task<LS_FAVORITE_SAVED> SaveandremoveBizfavorite(string adid,string pid,string email, string issaved);

        [Get("/serviceapi.aspx?type=Submitrecommendads&adid={adid}&userpid={pid}")]
        Task<LS_RECOMEND> Submitrecommendads(string adid, string pid);

        [Get("/serviceapi.aspx?type=Sendreport&adid={adid}&userpid={pid}&useremail={email}&reportid={reportid}&rptemail={reprtmail}&rptdesc={reportdesc}&adtitle={adtitle}&adurl={adurl}&userid={userid}")]
        Task<LS_REPORT> Sendreport (string reportid,string reprtmail,string reportdesc,string adid,string adtitle,string adurl, string pid, string email, string userid);
        
        [Get("/LS.aspx?type=GetdetailOnlineclasses&adid={adid}")]
        Task<LS_ONLINE_DETAIL> GetOnlineclasses(string adid);
    }
}
