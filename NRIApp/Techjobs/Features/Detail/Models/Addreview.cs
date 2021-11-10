using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.Detail.Models
{
   public class Addreview
    {
        public string businessid { get; set; }
        public string rating { get; set; }
        public string desc { get; set; }
        public string emailid { get; set; }
        public string userpid { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public string moduleid { get; set; }
        public string mobileno { get; set; }
    }
    public class Reviewresult
    {
        public string resultinformation { get; set; }
        public string otpsend { get; set; }
        public string reviewid { get; set; }
    }
    public class OTPdetails
    {
        public string resultinformation { get; set; }
        public string otpno { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
        public string respid { get; set; }
    }
}
