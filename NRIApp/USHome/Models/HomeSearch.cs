using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.USHome.Models
{
   public class HomeSearch
    {
        public string id { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string tagtype { get; set; }
        public string resulttype { get; set; }
        public string header { get; set; }
        public string supertag { get; set; }
        public string supertagid { get; set; }
        public string adid { get; set; }
        public bool stackdetails { get; set; }
        public bool jobstackdetails { get; set; }
        public string adtype { get; set; }
        public string roomtype { get; set; }
        public string price1 { get; set; }
        public string price2 { get; set; }
        public string price { get; set; }
        public string cityname { get; set; }
        public string statecode { get; set; }
        public string tags { get; set; }
        public string jobrole { get; set; }
        public string ltype { get; set; }
        public string search { get; set; }
        public string contentid { get; set; }
        //daycare
        public string secondarycategoryvalue { get; set; }
        public string tertiarycategoryvalue { get; set; }
        public string city { get; set; }
        public string businessid { get; set; }
    }

    public class HomeSearchlist
    {
        public List<HomeSearch> ROW_DS { get; set; }
    }
}
