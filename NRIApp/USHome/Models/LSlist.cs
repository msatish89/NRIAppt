using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.USHome.Models
{
   public class LSlist
    {
        public string primarytag { get; set; }
        public string primarytagurl { get; set; }
        public string tagid { get; set; }
        public string supertag { get; set; }
        public string supertagid { get; set; }
        public string nextlevelcount { get; set; }
    }

    public class LSdata
    {
        public List<LSlist> ROW_DATA { get; set; }
    }
}
