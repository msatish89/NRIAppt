using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using NRIApp.Techjobs.Features.Home.Models;
using System.Threading.Tasks;

namespace NRIApp.Techjobs.Features.Home.Interfaces
{
   public interface IHomeservice
    {
        [Get("/techjobsapi.aspx?type=courses")]
        Task<Courselist> Getcourses();

        [Get("/techjobsapi.aspx?type=modules&courseid={courseid}")]
        Task<Modulelist> Getmodules(string courseid);

        [Get("/techjobsapi.aspx?type=techjobssearch&searchtext={searchtext}")]
        Task<Searchlist> Getsearch(string searchtext);
    }
}
