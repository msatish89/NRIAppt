using NRIApp.LocalJobs.Features.Listing.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Listing.Interfaces
{
    interface ILocalJob
    {
        [Get("/jobslisting-api.aspx?type=getlistings")]
        Task<LocalJob> Getjoblist(LocalJobSenddata jobdata);

        [Get("/jobslisting-api.aspx?type=getfilterdata&ltype={ltype}")]
        Task<FilterListings> Getfilterdata(string ltype);

        [Get("/jobslisting-api.aspx?type=getlocation&titleurl={titleurl}&nearby={nearby}")]
        Task<LocationOverallData> getoverallLocation(string titleurl, string nearby);

        [Get("/jobslisting-api.aspx?type=searchads&title={title}&cityurl={cityurl}&lat={lat}&long={lng}&metrourl={metrourl}&cityname={cityname}&countrycode={countrycode}&statecode={statecode}")]
        Task<SearchList_Jobs> searchlist(string title, string cityurl, double lat, double lng, string metrourl, string cityname, string countrycode, string statecode);

        [Get("/jobslisting-api.aspx?type=savedadlist&pid={pid}&rowstofetch={rowstofetch}")]
        Task<Savedadlist> getsavedadlist(string pid, string rowstofetch);

        [Get("/jobslisting-api.aspx?type=deltesavedAd&adid={adid}&pid={pid}")]
        Task<Features.Detail.Models.resultinfo> deletesavedadlist(string adid, string pid);

        [Get("/jobslisting-api.aspx?type=getfunctionalareas&jobtype={jobtype}")]
        Task<Posting.Models.Functionalarealist> Getfunctionalarea(int jobtype);

    }
}
