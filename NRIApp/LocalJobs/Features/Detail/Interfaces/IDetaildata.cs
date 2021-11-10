using NRIApp.LocalJobs.Features.Detail.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Detail.Interfaces
{
    public interface IDetaildata
    {
        [Get("/jobslisting-api.aspx?type=getsingledata&contentid={contentid}&titleurl={titleurl}&userpid={userpid}")]
        Task<Detaildata> Getsingledatadetail(string contentid, string titleurl, string userpid);

        [Get("/jobslisting-api.aspx?type=postresponse")]
        Task<Detaildata> Applyforjob(string contentid, string titleurl, string pid);

        [Get("/jobslisting-api.aspx?type=saveadlist&adid={adid}&email={email}&pid={pid}")]
        Task<JobsSaveAD> SaveAdData(string adid, string email, string pid);

        [Get("/jobslisting-api.aspx?type=deleteadlist&adid={adid}&pid={pid}")]
        Task<JobsDeleteAD> DeleteAdData(string adid, string pid);

        [Get("/jobslisting-api.aspx?type=reportflag&adid={adid}&userpid={userpid}&adtitle={adtitle}&sitename={sitename}&reportid={reportid}&adurl={adurl}&siteid={siteid}&email={email}")]
        Task<Reportflagresult_DATA> GetReport(string adid, string userpid, string adtitle, string sitename, string reportid, string adurl, string siteid, string email);

        [Get("/jobslisting-api.aspx?type=postresponse")]
        Task<postresponseresult_data> postjobresponse(postresponse postres);

        [Get("/jobslisting-api.aspx?type=applyjobrole&businessurl={businessurl}&pid={pid}")]
        Task<Joboffers> getjoboffers(string businessurl, string pid);

        //desc
        //[Get("/mobileappresume.aspx?type=getdescription&adid={adid}&folderid={folderid}")]
        //Task<Reportflagresult_DATA> getfulldesc(string adid, string folderid);


        //[Get("/jobsposting-api.aspx?type=applyjobrole&businessurl={businessurl}&pid={pid}")]
        //Task<Joboffers> uploadresume(string businessurl, string pid);

        //[Post("/jobsposting-api.aspx?type=postresponse")]
        //Task<postresponseresult_data> postjobresponse([Body(BodySerializationMethod.UrlEncoded)]postresponse rsLcf);

        //[Post("/roommatesapi.aspx?type=submitresponse&mobileverify={mobileverify}")]
        //Task<OTP_LCF_Result> Responsesubmit([Body(BodySerializationMethod.UrlEncoded)]Response rsLcf, string mobileverify);

        //[Get("/jobsposting-api.aspx?type=getlistings")]
        //Task<JobListings> Getjoblist(Sendlistdata jobdata);
    }
}
