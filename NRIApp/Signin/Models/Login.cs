using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Signin.Models
{
   public class Login
    {
        public string useremail { get; set; }
        public string password { get; set; }
    }

    public class Logindetails
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string pid { get; set; }
        public string status { get; set; }
        public string userid { get; set; }
    }

    public class Loginsucess
    {
        public List<Logindetails> ROW_PRC_CHECKIN_MOBILE_LOGIN { get; set; }
    }

    public class Forgotpass
    {
        public string output { get; set; }
    }
}
