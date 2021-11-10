using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Signin.Models;

namespace NRIApp.Signin.Interfaces
{
   public interface ISignin
    {
        [Post("/login.aspx?type=signin")]
        [Headers("Login-Page: Sulekha-Login")]
        Task<Loginsucess> SulekhaLogin([Body(BodySerializationMethod.UrlEncoded)] Login requestObject);

        [Post("/login.aspx?type=signup")]
        [Headers("Signup-Page: Sulekha-signup")]
        Task<signupdetails> Sulekhasignup([Body(BodySerializationMethod.UrlEncoded)] signup requestObject);

        [Get("/login.aspx?type=forgotpassword&email={email}")]
        Task<Forgotpass> Getpassword(string email);

        [Get("/login.aspx?type=editphno&mobileno={mobileno}&countrycode={countrycode}&deviceid={deviceid}&email={email}")]
        Task<signupdetails> Editphno(string mobileno, string countrycode, string deviceid, string email);

        [Get("/login.aspx?type=resendotp&mobileno={mobileno}&countrycode={countrycode}")]
        Task<signupdetails> Resendotp(string mobileno, string countrycode);

        [Post("/login.aspx?type=verifyotp")]
        [Headers("Login-Page: Verify-Mobile")]
        Task<signupdetails> Verifyotp([Body(BodySerializationMethod.UrlEncoded)] signup requestObject);

        [Get("/login.aspx?type=loginlock&email={email}&deviceid={deviceid}&devicename={devicename}&appversion={appversion}&appid={appid}")]
        Task<Loginlock> Loginlocking(string email, string deviceid, string devicename, string appversion, string appid);
    }
}
