using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class Skillslist
    {
        public int supertagid { get; set; }
        public string supertag { get; set; }
        public int primarytagid { get; set; }
        public string primarytag { get; set; }
        public int rowstofetch { get; set; }
        public int rowid { get; set; }
        public int pageno { get; set; }
        public int totalrecs { get; set; }
    }

    public class Skills
    {
        public List<Skillslist> ROW_DATA { get; set; }
    }

    //public class newskill
    //{
    //    public string newskilltxt { get; set; }
    //}
    public class Addskilldata
    {
        public int resultinfo { get; set; }
    }

    public class Addskill
    {
        public List<Addskilldata> ROW_DATA { get; set; }
    }
}
