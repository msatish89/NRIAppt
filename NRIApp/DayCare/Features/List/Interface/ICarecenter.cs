using NRIApp.LocalJobs.Features.Detail.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.DayCare.Features.List.Interface
{
    public interface ICarecenter
    {
        [Get("/daycareapiapi.aspx?type=getreviewheader&review={review}&objectid={objectid}")]
        Task<RatingData> GetheaderReview(string review, string objectid);

        [Get("/daycareapiapi.aspx?type=replydetail&reviewid={reviewid}&adid={adid}")]
        Task<Reviewreplydetail> GetAdReviewReplydtl(string reviewid, string adid);

        [Get("/daycareapi.aspx?type=getreviews&objectid={objectid}")]
        Task<RatingData> getreviewdetails(string objectid);

        [Get("/daycareapi.aspx?type=addreview")]
        Task<resultinfo> addreview(Add_Rating_Details ratingdetails);

        [Get("/daycareapiapi.aspx?type=addreply&reviewid={reviewid}&adid={adid}&shortdesc={shortdesc}&contributor={contributor}&userpid={userpid}")]
        Task<resultinfo> addreply(string reviewid, string adid,string shortdesc,string contributor,string userpid);
    }
}
