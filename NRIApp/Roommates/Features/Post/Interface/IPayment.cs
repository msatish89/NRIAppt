using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.Post.Models;

namespace NRIApp.Roommates.Features.Post.Interface
{
    interface IPayment
    {
        [Get("/roommatesapi.aspx?type=getpackages&adid={adid}&userpid={userpid}&usertype={usertype}")]
        Task<Packagelist> getpackages(string adid, string userpid, string usertype);
        [Get("/roommatesapi.aspx?type=changeadstatus&adid={adid}&userpid={userpid}")]
        Task<Packagelist> changeadstatus(string adid, string userpid);
        [Get("/roommatesapi.aspx?type=updatepackage&adid={adid}&days={days}&ordertype={ordertype}&totalads={totalads}&agentid={agentid}")]
        Task<Pkgsucess> updatepackage(string adid, string days, string ordertype,string totalads, int agentid);

        [Post("/roommatesapi.aspx?type=authorizepayment")]
        [Headers("shoppingcart-Page: shoppingcart-Post")]
        Task<shoppingcart> Saveshoppingcart([Body(BodySerializationMethod.UrlEncoded)] Shoppingcartdetails requestObject);

        [Post("/roommatesapi.aspx?type=addpayment")]
        [Headers("Payment-Page: Payment-Post")]
        Task<Paymentsucess> Addpayment([Body(BodySerializationMethod.UrlEncoded)] Payment requestObject);

        //postsuccess
        [Get("/roommatesapi.aspx?type=getpaymentinfo&adid={adid}&usertype={usertype}&packagetype={packagetype}")]
        Task<Package_INFO> getpaymentinfo(string adid,string usertype,string packagetype);

        [Get("/roommatesapi.aspx?type=getnoofdays&usertype={usertype}")]
        Task<Pkgsucess> getnoofdays(string usertype);

        //publishad
        [Get("/roommatesapi.aspx?type=publishad&adid={adid}")]
        Task<Pkgsucess> publishad(string adid);
        //applycoupon
        [Get("/roommatesapi.aspx?type=applycoupon&adid={adid}&couponcode={couponcode}&ordertype={ordertype}")]
        Task<Pkgsucess> applycouponcode(string adid,string couponcode,string ordertype);
    }
}
