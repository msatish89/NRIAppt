using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Listing.Models
{
    //public class SearchList_Jobs
    //{
    //    public List<Search> ROW_COMMON_SEARCH { get; set; }
    //}
    //public class Search
    //{
    //    public string id { get; set; }
    //    public string adid { get; set; }
    //    public string Contentid { get; set; }
    //    public string title { get; set; }
    //    public string adtype { get; set; }
    //    public string roomtype { get; set; }
    //    public string statecode { get; set; }
    //    public string cityname { get; set; }
    //    public string price1 { get; set; }
    //    public string cityurl { get; set; }
    //    public string search { get; set; }
    //    public string header { get; set; }
    //    public bool stackdetails { get; set; }
    //}
    public class Search
    {
        public int contentid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string search { get; set; }
        public string commonads { get; set; }
        public string categoryurl { get; set; }
        public string cityurl { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string businesstype { get; set; }
        public string founded { get; set; }
        public string industry { get; set; }
        public string industryid { get; set; }
        public string companysize { get; set; }
        public string crdate { get; set; }
        public string jobrole { get; set; }
        public string experience { get; set; }
        public string experiencefrom { get; set; }
        public string experienceto { get; set; }
        public string cityname { get; set; }
        public string statename { get; set; }
        public string adid { get; set; }
        public string price1 { get; set; }
        public string price2 { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string adtype { get; set; }
        public string trackurl { get; set; }
        public string jobfunction { get; set; }
        public string skill { get; set; }
        public string businessname { get; set; }
        public string minsal { get; set; }
        public string maxsal { get; set; }
        public string experiencefrom1 { get; set; }
        public string experienceto1 { get; set; }
        public string state { get; set; }


        public string header { get; set; }
        public bool stackdetails { get; set; }
    }

    public class SearchList_Jobs
    {
        public List<Search> ROW_DS { get; set; }
    }
}
