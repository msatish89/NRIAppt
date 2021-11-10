using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
   
    public class Business
    {
        public string businessname { get; set; }
        public string businessEmail { get; set; }
        public string businessAddress { get; set; }
        public string contactName { get; set; }
        public string countrycode { get; set; }
        public string contactNumber { get; set; }
        public string supertagid { get; set; }
        public string supertag { get; set; }
        public string ptag { get; set; }
        public string ptagid { get; set; }
        public string leaftags { get; set; }
        public string userpid { get; set; }
        public string useremail { get; set; }
        public string username { get; set; }
        public string businessdesc { get; set; }
        public string bizlatitude { get; set; }
        public string bizlongitude { get; set; }
        public string bizcity { get; set; }
        public string bizzipcode { get; set; }
        public string bizcountry { get; set; }
        public string bizcountrycode { get; set; }
        public string bizstate { get; set; }
        public string profimgurl { get; set; }
        public string mobileverify { get; set; }
        public string businessexp { get; set; }
        public string postedvia { get; set; }
        public string postedviaid { get; set; }

        public string oldclpostid { get; set; }


    }

    public class LS_POSTING_RESULT
    {
        public List<LS_POSTING_RESULT_DATA> ROW_DATA { get; set; }
    }

    public class LS_POSTING_RESULT_DATA
    {
        public string resultinformation { get; set; }
        public string contentid { get; set; }
        public string type { get; set; }
        public string pinno { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
    }
    public class BUSINESS_RESULT
    {
        public List<BUSINESS_RESULT_DATA> ROW_DATA { get; set; }
    }

    public class BUSINESS_RESULT_DATA
    {
        public string resultinformation { get; set; }
        public string contentid { get; set; }
        public string type { get; set; }
        public string pinno { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
        public string ispayment { get; set; }
        public string userpid { get; set; }
        public string useremail { get; set; }
        public string username { get; set; }
    }
    public class RESULT_OTP
    {

        public string pinno { get; set; }
        public string result { get; set; }
    }

    
    public class EXPERIENCE_DATA
    {
        public string id { get; set; }
        public string Expimg { get; set; }
        public string content { get; set; }
    }

    public class LS_GET_PRIME_ID
    {
        public List<LS_GET_PRIME_ID_DATA> ROW_DATA { get; set; }
    }
    public class LS_GET_PRIME_ID_DATA
    {
        public string primeid { get; set; }
     
    }
}
