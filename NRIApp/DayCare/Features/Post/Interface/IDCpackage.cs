using NRIApp.DayCare.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.DayCare.Features.Post.Interface
{
    public interface IDCpackage
    {
        [Get("/daycareposting.aspx?type=getdaycarepackages")]
        Task<DCpackage> getdcpackage(string businessid,string supercategoryid,string primarycategoryid,string secondarycategoryid,string tertiarycategoryid,string newcityurl,string userpid,string usertype);

        //[Post("/daycareposting.aspx?type=resendotp")]
        //Task<results_otp> Resendotp([Body(BodySerializationMethod.UrlEncoded)] results rsptf);
    }
}
