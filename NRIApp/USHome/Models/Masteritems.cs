using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.USHome.Models
{
    public class Masteritems
    {
        public string Eventslist { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
        public int id { get; set; }
        public bool IsVisible { get; set; }
    }

    public class MOB_ADS
    {
        public List<MOB_ADS_DETAILS> ROW_DATA { get; set; }
    }
    public class MOB_ADS_DETAILS
    {
        public string type { get; set; }
        public string imageurl { get; set; }
        public string weburl { get; set; }
        public string vasturl { get; set; }
        public string enablead { get; set; }
        public string adunitid { get; set; }
        public string templateid { get; set; }
        public string xmldata { get; set; }
        public string displayorder { get; set; }
    }
}
