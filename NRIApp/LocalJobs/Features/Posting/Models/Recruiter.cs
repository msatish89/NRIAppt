using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class Recruiter
    {
        public string name { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string filepath { get; set; }
    }

    public class Recruiterdetails
    {
        public string otpsend { get; set; }
        public string otpno { get; set; }
        public string respid { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
    }
}
