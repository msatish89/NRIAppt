using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.Post.Models
{
    public class DCpackage
    {
        public List<DC_Package_DATA> ROW_DATA { get; set; }
    }
    public class DC_Package_DATA
    {
        public int contentid { get; set; }
        public string service { get; set; }
        public double amount { get; set; }
        public string ordertype { get; set; }
        public int noofdays { get; set; }
        public string ordergroup { get; set; }
        public double salesprice { get; set; }
        public int usertype { get; set; }
        public int packagedays { get; set; }
        public int numberofliveads { get; set; }
        public double addonamount { get; set; }
        public string categoryflag { get; set; }
        public int publishadstatus { get; set; }
        public string livecount { get; set; }
        public string totalcount { get; set; }
        public string packagetext { get; set; }
        public string packageid { get; set; }
    }
}
