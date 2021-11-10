using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Signin.Models
{
   public class signup
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
        public string deviceid { get; set; }
        public string deviceos { get; set; }

    }

    public class signupdetails
    {
        public string countrycode { get; set; }
        public string mobileno { get; set; }
        public string pid { get; set; }
        public string result { get; set; }
        public string otpno { get; set; }
        public string divresult { get; set; }
    }

    public class signupsucess
    {
        public List<signupdetails> ROW_PRC_ADD_USER_INFO { get; set; }
    }
    public class Loginlock
    {
        public string resultinformation { get; set; }
    }
}
