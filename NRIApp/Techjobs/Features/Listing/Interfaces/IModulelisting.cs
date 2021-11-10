using NRIApp.Techjobs.Features.Listing.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.Techjobs.Features.Listing.Interfaces
{
   public interface IModulelisting
    {
        [Get("/techjobsapi.aspx?type=modulelistings&moduleid={moduleid}&trainingmode={trainingmode}&facilityids={facilityids}&userlat={userlat}&userlong={userlong}&orderby={orderby}")]
        Task<Moduledata> Getmodulelistings(string moduleid, string trainingmode,string facilityids, string userlat, string userlong, string orderby);
    }
}
