using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.LeadForm.Models
{
    public class Leaddetails
    {
        public string name { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string businessid { get; set; }
        public string shortdesc { get; set; }
        public string trainingcity { get; set; }
        public string userpid { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string moduleid { get; set; }
        public string trainingmode { get; set; }
        public string jobmodule { get; set; }
        public string ip { get; set; }
        public string countrycode { get; set; }
        public string postedvia { get; set; }
        public string deviceid { get; set; }

    }

    public class leadsbmtdetails
    {
        public string otpsend { get; set; }
        public string otpno { get; set; }
        public string respid { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
    }

}
