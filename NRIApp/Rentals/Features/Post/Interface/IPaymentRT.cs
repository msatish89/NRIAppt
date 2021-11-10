using NRIApp.Rentals.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.Rentals.Features.Post.Interface
{
    interface IPaymentRT
    {
        [Get("/rentalsapi.aspx?type=getpackages&adid={adid}&userpid={userpid}&usertype={usertype}")]
        Task<RTPackagelist> getpackages(string adid, string userpid, string usertype);
        [Get("/rentalsapi.aspx?type=changeadstatus&adid={adid}&userpid={userpid}")]
        Task<RTPackagelist> changeadstatus(string adid, string userpid);

        [Get("/LS.aspx?type=GetValueaddedservices&days={days}")]
        Task<VALUE_ADD_SERVICE> GetValueaddedservices(string days);
        
        [Get("/rentalsapi.aspx?type=updatepackage&adid={adid}&days={days}&ordertype={ordertype}&totalads={totalads}&agentid={agentid}")]
        Task<Pkgsucess_RT> updatepackage(string adid, string days, string ordertype, string totalads, int agentid);

        [Post("/rentalsapi.aspx?type=authorizepayment")]
        [Headers("shoppingcart-Page: shoppingcart-Post")]
        Task<shoppingcart_RT> Saveshoppingcart([Body(BodySerializationMethod.UrlEncoded)] Shoppingcartdetails_RT requestObject);

        [Post("/rentalsapi.aspx?type=addpayment")]
        [Headers("Payment-Page: Payment-Post")]
        Task<Paymentsucess_RT> Addpayment([Body(BodySerializationMethod.UrlEncoded)] Payment_RT requestObject);

        //postsuccess
        [Get("/rentalsapi.aspx?type=getpaymentinfo&adid={adid}&usertype={usertype}&packagetype={packagetype}")]
        Task<RTPackage_INFO> getpaymentinfo(string adid,string usertype,string packagetype);

        [Get("/rentalsapi.aspx?type=getnoofdays&usertype={usertype}")]
        Task<Pkgsucess_RT> getnoofdays(string usertype);

        //publish
        [Get("/rentalsapi.aspx?type=publishad&adid={adid}")]
        Task<Pkgsucess_RT> publishad(string adid);

        [Get("/rentalsapi.aspx?type=applycoupon&adid={adid}&couponcode={couponcode}&ordertype={ordertype}")]
        Task<Pkgsucess_RT> applycouponcode(string adid, string couponcode, string ordertype);
    }
}
