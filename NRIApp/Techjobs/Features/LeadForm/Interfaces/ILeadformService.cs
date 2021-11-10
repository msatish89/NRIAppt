using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using NRIApp.Techjobs.Features.LeadForm.Models;
namespace NRIApp.Techjobs.Features.LeadForm.Interfaces
{
   public interface ILeadformService
    {
        [Get("/techjobsapi.aspx?type=searchcity&term={term}&country={country}")]
        Task<Cityajaxlist> Getcityajax(string term, string country);

        [Get("/techjobsapi.aspx?type=moduleajax&srchtxt={srchtxt}")]
        Task<Moduleajax> Getmoduleajax(string srchtxt);

        [Post("/leadapi.aspx?type=postleaddata")]
        [Headers("Techjobs-Lead Page: Lead-Post")]
        Task<leadsbmtdetails> SubmitLeadForm([Body(BodySerializationMethod.UrlEncoded)] Leaddetails requestObject);

        [Post("/leadapi.aspx?type=verifyotp&respid={respid}&deviceid={deviceid}&deviceos={deviceos}")]
        Task<leadsbmtdetails> otpsubmitForm(string respid, string deviceid, string deviceos);

        [Get("/leadapi.aspx?type=getleadbizdetails&respid={respid}")]
        Task<Leadsentbizlist> Getbizlist(string respid);

        [Get("/leadapi.aspx?type=resendotp&respid={respid}")]
        Task<leadsbmtdetails> Resendotp(string respid);

        [Get("/leadapi.aspx?type=editphno&respid={respid}&mobileno={mobileno}&countrycode={countrycode}")]
        Task<leadsbmtdetails> Editphno(string respid, string mobileno,string countrycode);

        [Get("/techjobsapi.aspx?type=getbizcnt&moduleid={moduleid}")]
        Task<Bizcnt> Getbizcnt(string moduleid);

    }
}
