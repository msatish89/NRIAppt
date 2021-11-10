using NRIApp.LocalJobs.Features.Jobseeker.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Jobseeker.Interface
{
    public interface IJobseeker
    {
        [Get("/jobseekerprofile-api.aspx?type=fillprofile&email={email}&profileid={profileid}")]
        Task<Jobseekers> filljobprofile(string email, int profileid);

        [Get("/jobseekerprofile-api.aspx?type=fillprofile")]
        Task<Resultinformation> profiledata(Sendprofiledata data);

        [Get("/jobseekerprofile-api.aspx?type=fillskillfields&profileid={profileid}")]
        Task<SKILLS> fillskillfields(int profileid);

        [Get("/jobseekerprofile-api.aspx?type=fillexperiencefield&profileid={profileid}")]
        Task<EXPERIENCE> fillexperiencefield(int profileid);

        //[Get("/jobseekerprofile-api.aspx?type=profilepagecreate")]
        //Task<Resultinformation> profilepagecreate(Jobseekers_DATA data);

        [Post("/jobseekerprofile-api.aspx?type=profilepagecreate")]
        Task<Resultinformation> profilepagecreate([Body(BodySerializationMethod.UrlEncoded)]Jobseekers_DATA rsptf);

        //[Post("/jobseekerprofile-api.aspx?type=profilepagecreate")]
        //Task<Resultinformation> profilepagecreate([Body(BodySerializationMethod.UrlEncoded)] Jobseekers_DATA rsptf);


        //[Post("/jobseekerprofile-api.aspx?type=profilepagecreate")]
        //Task<Resultinformation> profilepagecreate([Body(BodySerializationMethod.UrlEncoded)]Jobseekers_DATA rsptf);

        [Get("/jobseekerprofile-api.aspx?type=deleteskilldata&contentid={skillcontentid}")]
        Task<Resultinformation> deleteskilldata(string skillcontentid);

        [Get("/jobseekerprofile-api.aspx?type=deleteexperiencedata&contentid={experiencecontentid}")]
        Task<Resultinformation> deleteexperiencedata(string experiencecontentid);

        //[Get("/jobseekerprofile-api.aspx?type=downloadresume&pid={pid}&mobileno={mobileno}&email={email}&jobresumeid={jobresumeid}&ipaddress={ipaddress}")]
        //Task<Resultinformation> downloadresumeurl(string pid, string mobileno,string email,string jobresumeid, string ipaddress);

        [Get("/mobileappresume.aspx?type=downloadresume&pid={pid}&mobileno={mobileno}&email={email}&jobresumeid={jobresumeid}&ipaddress={ipaddress}")]
        Task<Resultinformation> downloadresumeurl(string pid, string mobileno, string email, string jobresumeid, string ipaddress);

    }
}
