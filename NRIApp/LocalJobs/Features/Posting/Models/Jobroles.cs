using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class Jobroles
    {
        public string contentid { get; set; }
        public string rolename { get; set; }
        public string roleurl { get; set; }
        public string image { get; set; }
        public string tagid { get; set; }
        public string textcolor { get; set; }
    }
    public class Jobroleslist
    {
        public List<Jobroles> ROW_DATA { get; set; }
    }
}
