using NRIApp.LocalJobs.Features.Jobresume.Models;
using NRIApp.LocalJobs.Features.Listing.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Jobresume.Interfaces
{
    public interface IJobresume
    {
        [Get("/jobsresumelisting-api.aspx?type=getresumelistings")]
        Task<LocalJobresume> Getjoblist(LocalJobSenddata jobdata);

        [Get("/jobsresumelisting-api.aspx?type=jobrolesearch&tag={tag}&ltype={ltype}")]
        Task<Jobrole_search> Getjobroles(string tag,string ltype);

        [Get("/jobsresumelisting-api.aspx?type=getresumedetail&email={email}&profileid={profileid}&pid={pid}")]
        Task<JobresumeDetail> Getsingledatadetail(string email, string profileid,string pid);

        //[Get("/jobslisting-api.aspx?type=getfilterdata&ltype={ltype}")]
        //Task<FilterListings> Getfilterdata(string ltype);

        //[Get("/jobslisting-api.aspx?type=getlocation&titleurl={titleurl}&nearby={nearby}")]
        //Task<LocationOverallData> getoverallLocation(string titleurl, string nearby);

        [Get("/jobslisting-api.aspx?type=searchads&title={title}&cityurl={cityurl}&lat={lat}&long={lng}&metrourl={metrourl}&cityname={cityname}&countrycode={countrycode}&statecode={statecode}")]
        Task<SearchList_Jobs> searchlist(string title, string cityurl, double lat, double lng, string metrourl, string cityname, string countrycode, string statecode);

        [Get("/jobsresumelisting-api.aspx?type=savedadslisting&pid={pid}&rowstofetch={rowstofetch}")]
        Task<Savedadlist> getsavedadlist(string pid, string rowstofetch);

        [Get("/jobsresumelisting-api.aspx?type=saveresume&contentid={contentid}&pid={pid}")]
        Task<Features.Detail.Models.resultinfo> savedad(string contentid, string pid);

        [Get("/jobsresumelisting-api.aspx?type=removeresume&contentid={adid}&pid={pid}")]
        Task<Features.Detail.Models.resultinfo> deletesavedad(string adid, string pid);

        [Get("/mobileappresume.aspx?type=jobresumedownload&pid={pid}&mobileno={mobileno}&email={email}&jobresumeid={jobresumeid}&ipaddress={ipaddress}")]
        Task<Jobseeker.Models.Resultinformation> downloadresumeurl(string pid, string mobileno, string email, string jobresumeid, string ipaddress);

        [Get("/jobsresumelisting-api.aspx?type=previewresume&pid={pid}&email={email}&jobresumeid={jobresumeid}&name={name}")]
        Task<Jobseeker.Models.Resultinformation> previewresume(string pid, string email, string jobresumeid, string name);

        [Get("/jobsresumelisting-api.aspx?type=jobseekerresponse")]
        Task<Detail.Models.postresponseresult_data> postjobresponse(Detail.Models.postresponse postres);
    }
}
