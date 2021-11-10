using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class Functionalarea
    {
        public int secondarycategoryid { get; set; }
        public string categoryname { get; set; }
        public string categoryurl { get; set; }
        public int primarycategoryid { get; set; }
        public string image { get; set; }
        public string textcolor { get; set; }
    }
    public class Functionalarealist
    {
        public List<Functionalarea> ROW_DATA { get; set; }
    }


}
