using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.LocalService.Features.Interfaces
{
    interface ILS_Prime_Form
    {
        [Get("/LS.aspx?type=Getqutionaries&tagid={tagid}")]
        Task<LS_PRIME_FORM> Getqutionaries(string tagid);

        [Get("/LS.aspx?type=Getlsflexpackage&adid={adid}")]
        Task<LS_FLEX_PACKAGE> Getlsflexpackage(string adid);

        [Post("/serviceapi.aspx?type=Selectedflexpackage")]
        Task<LS_FLEX_SELECTED_RESULT> Selectedflexpackage([Body(BodySerializationMethod.UrlEncoded)] LS_FLEX_SELECT_PACKAGE business);

        [Get("/LS.aspx?type=Getprimedata&guid={guid}")]
        Task<LS_PRIME_BY_GUID> Getprimedata(string guid);

        [Post("/serviceapi.aspx?type=Applyflexcouponcode&hdnguid={pguid}&hdnprimeid={primeid}&couponcode={couponcode}")]
        Task<LS_COUPON_SUCCESS> ApplyFlex_couponcode(string pguid,string primeid,string couponcode);
    }
}
