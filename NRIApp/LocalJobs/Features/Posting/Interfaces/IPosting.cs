using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Posting.Interfaces
{
   public interface IPosting
    {
        [Get("/jobsposting-api.aspx?type=jobroleajax&term={term}")]
        Task<Jobroleslist> GetJobroleajax(string term);
        [Get("/jobsposting-api.aspx?type=getfunctionalareas&jobtype={jobtype}")]
        Task<Functionalarealist> Getfunctionalarea(int jobtype);
        [Get("/jobsposting-api.aspx?type=jobroles&funcid={funcid}")]
        Task<Jobroleslist> GetJobroles(int funcid);
        [Get("/jobsposting-api.aspx?type=jobskills&funcid={funcid}&pageno={pageno}")]
        Task<Skills> GetSkills(int funcid, int pageno);
        [Post("/jobsposting-api.aspx?type=postfirststep")]
        [Headers("Localjobs-Posting Page: Jobs-Post")]
        Task<postingsbmtdetails> Jobsposting([Body(BodySerializationMethod.UrlEncoded)] Postingdata requestObject);
        [Post("/jobsposting-api.aspx?type=verifyotp&adid={adid}&email={email}&userpid={userpid}&mobileno={mobileno}&cntrycode={cntrycode}&deviceid={deviceid}&deviceos={deviceos}&userip={userip}")]
        Task<postingsbmtdetails> otpsubmitForm(string adid, string email, string userpid, string mobileno, string cntrycode, string deviceid, string deviceos, string userip);

        [Get("/jobsposting-api.aspx?type=resendotp&mobileno={mobileno}&cntrycode={cntrycode}")]
        Task<postingsbmtdetails> Resendotp(string mobileno, string cntrycode);

        [Get("/jobsposting-api.aspx?type=editphno&respid={respid}&mobileno={mobileno}&countrycode={countrycode}")]
        Task<postingsbmtdetails> Editphno(string respid, string mobileno, string countrycode);

        [Get("/jobsposting-api.aspx?type=businessajax&term={term}")]
        Task<Businessdata> Businessajax(string term);

        [Post("/jobsposting-api.aspx?type=postsecondstep")]
        [Headers("Localjobs-Posting Page: Jobs-Postsec")]
        Task<postingsbmtdetails> Jobspostingsecondstep([Body(BodySerializationMethod.UrlEncoded)] Postingdata requestObject);

        //newskilladd
        [Get("/jobsposting-api.aspx?type=addnewskills&term={newskill}")]
        Task<Addskill> addskill(string newskill);
    }
}
