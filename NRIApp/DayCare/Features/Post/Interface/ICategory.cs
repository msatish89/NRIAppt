using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;
using NRIApp.DayCare.Features.Post.Models;

namespace NRIApp.DayCare.Features.Post.Interface
{
    public interface ICategory
    {
        [Get("/daycareapi.aspx?type=category&categorylevel={Categorylevel}&supercategoryid={supercategoryid}&primarycategoryid={primarycategoryid}&secondarycategoryid={secondarycategoryid}&maincategory=day-care")]
        Task<DCCategoryRowData> GetCareTypes(string Categorylevel, string supercategoryid, string primarycategoryid, string secondarycategoryid);
   
        [Get("/LS.aspx?type=getlocation&term={locationname}")]
        Task<ListOfLocationData> getlocation(string locationname);
 
        [Post("/daycareapi.aspx?type=submitlcf")]
        Task<DC_ListofResult> Responselcfsubmit([Body(BodySerializationMethod.UrlEncoded)]DC_CategoryList rsLcf);

        //activityenrichment
        [Get("/daycareapi.aspx?type=activityenrichment")]
        Task<activityenrichment_data> getactivityenrichment();
        
    }
}
