using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NRIApp.DayCare.Features.List.Models;
using NRIApp.DayCare.Features.Detail.Models;

namespace NRIApp.DayCare.Features.List.Interface
{
    public interface IDClistings
    {
        [Post("/daycareapi.aspx?type=listings")]
        Task<DClistingdata> getcarelistings([Body(BodySerializationMethod.UrlEncoded)] DClistinfo requestObject);

        [Post("/daycareapi.aspx?type=sublistings")]
        Task<DClistingdata> getcaresublistings([Body(BodySerializationMethod.UrlEncoded)] DClistinfo requestObject);

        [Get("/daycareapi.aspx?type=search&searchtxt={searchtxt}&cityurl={cityurl}&lat={lat}&long={lng}&metrourl={metrourl}&passingcityurl={passingcityurl}")]
        Task<DCsearch> searchlist(string searchtxt, string cityurl, string lat, string lng, string metrourl, string passingcityurl);

        [Get("/daycareapi.aspx?type=allcareservices")]
        Task<DCfilter> getallservices();

        [Get("/daycareapi.aspx?type=landmark&title={title}")]
        Task<LandmarkList> getlandmarklist(string title);

        [Post("/daycareapi.aspx?type=worktype")]
        Task<worktype> getworktype([Body(BodySerializationMethod.UrlEncoded)] DClistinfo requestObject);

        [Post("/daycareapi.aspx?type=postresponse")]
        Task<result_info> responsesubmit([Body(BodySerializationMethod.UrlEncoded)] response_data requestObject);

        [Get("/daycareapi.aspx?type=detail&businessid={contentid}&userpid={userpid}")]
        Task<CareDetail> getdetails(string contentid, string userpid);

        [Get("/daycareapi.aspx?type=savedadlist&pid={userpid}")]
        Task<Saved_Ads> getsavedadlist(string userpid);
    }
}
