using NRIApp.LocalJobs.Features.Detail.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Detail.Interfaces
{
    public interface IComapanydata
    {
        [Get("/jobslisting-api.aspx?type=companyprofile&businessurl={businessurl}&pid={pid}")]
        Task<Companydata> Getcompanyprofile(string businessurl, string pid);

        [Get("/jobslisting-api.aspx?type=businessfollow&pid={pid}&businessid={businessid}&email={email}&contactperson={name}&followflag={followflag}")]
        Task<Businessfollow> Businessfollow(string pid, string businessid, string email, string name, string followflag);

        [Get("/jobslisting-api.aspx?type=verifyphone&countrycod={countrycod}&phone={phone}&verify={verify}&verifycode={verifycode}&pinno={pinno}")]
        Task<resultinfo> verifyphone(string countrycod, string phone, string verify, string verifycode, string pinno);

        [Get("/jobslisting-api.aspx?type=claimbusiness&email={email}&countrycode={countrycode}&contactnumber={contactnumber}&mobileverified={mobileverified}&businessurl={businessurl}")]
        Task<resultinfo> claimbusiness(string email, string countrycode, string contactnumber, string mobileverified, string businessurl);

        [Get("/jobslisting-api.aspx?type=ratingreview&objectid={objectid}")]
        Task<RatingData> getreviewdetails(string objectid);

        [Get("/jobslisting-api.aspx?type=addreview")]
        Task<resultinfo> addreview(Add_Rating_Details ratingdetails);

        [Get("/jobslisting-api.aspx?type=replyforreview&reviewid={reviewid}&adid={adid}&pid={pid}&shortdesc={shortdesc}&contributor={contributor}")]
        Task<resultinfo> replytoreview(string reviewid, string adid, string pid, string shortdesc, string contributor);

        [Get("/jobslisting-api.aspx?type=companysalarylist&businessurl={businessurl}&pageno={pageno}")]
        Task<Salarylist> salarylist(string businessurl, int pageno);

        [Get("/jobslisting-api.aspx?type=benefitlist&businessurl={businessurl}&rowstofetch={rowstofetch}")]
        Task<Businessbenifits> businessbenifitlist(string businessurl, string rowstofetch);

        [Get("/jobslisting-api.aspx?type=jobopeninglist&businessurl={businessurl}&pageno={pageno}&rowstofetch={rowstofetch}")]
        Task<Businessjobopening> jobopeninglist(string businessurl, int pageno, int rowstofetch);

        [Get("/jobslisting-api.aspx?type=getreviewheader&review={review}&objectid={objectid}")]
        Task<RatingData> GetheaderReview(string review, string objectid);

        [Get("/jobslisting-api.aspx?type=getreviewreplydtl&reviewid={reviewid}&adid={adid}")]
        Task<Reviewreplydetail> GetAdReviewReplydtl(string reviewid, string adid);

        [Get("/jobslisting-api.aspx?type=jobalert")]
        Task<resultinfo> Getjobalert(Jobalert_DATA adid);


    }
}
