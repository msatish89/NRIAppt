using NRIApp.DayCare.Features.Detail.Models;
using NRIApp.LocalJobs.Features.Detail.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.DayCare.Features.Detail.Interface
{
    public interface ICareDetail
    {
        [Get("/daycareapi.aspx?type=getsingledata&contentid={contentid}&titleurl={titleurl}&userpid={userpid}")]
        Task<CareDetail> Getsingledatadetail(string contentid, string titleurl, string userpid);

        [Get("/daycareapi.aspx?type=savead&adid={adid}&emailid={email}&userpid={pid}")]
        Task<CareSaveAD> SaveAdData(string adid, string email, string pid);

        [Get("/daycareapi.aspx?type=removead&adid={adid}&pid={pid}")]
        Task<CareDeleteAD> DeleteAdData(string adid, string pid);

        [Get("/daycareapi.aspx?type=reportflag&adid={adid}&adtitle={adtitle}&reportid={reportid}&adurl={adurl}&email={email}&description={description}")]
        Task<CareReportflagresult_DATA> GetReport(string adid,string adtitle,string reportid, string adurl,string email,string description);

        //[Get("/jobslisting-api.aspx?type=postresponse")]
        //Task<Detaildata> Applyforjob(string contentid, string titleurl, string pid);
        //[Get("/jobslisting-api.aspx?type=postresponse")]
        //Task<postresponseresult_data> postjobresponse(postresponse postres);

        //[Get("/daycareapiapi.aspx?type=getreviewheader&review={review}&objectid={objectid}")]
        //Task<RatingData> GetheaderReview(string review, string objectid);

        [Get("/daycareapi.aspx?type=getreviewreplydtl&reviewid={reviewid}&adid={adid}")]
        Task<Reviewreplydetail> GetAdReviewReplydtl(string reviewid, string adid);

        [Get("/daycareapi.aspx?type=ratingreview&objectid={objectid}")]
        Task<RatingData> getreviewdetails(string objectid);

        [Get("/daycareapi.aspx?type=addreview")]
        Task<resultinfo> addreview(Add_Rating_Details ratingdetails);

        [Get("/daycareapi.aspx?type=replyforreview&reviewid={reviewid}&adid={adid}&pid={pid}&shortdesc={shortdesc}&contributor={contributor}")]
        Task<resultinfo> replytoreview(string reviewid, string adid, string pid, string shortdesc, string contributor);
    }
}
