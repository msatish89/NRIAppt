using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class LS_LEAD_OTP_RESULT
    {
        public string pinno { get; set; }
        public string type { get; set; }
        public string adid { get; set; }
    }

    public class LS_LEAD_DISTRIBUTE_BIZ
    {
        public List<LS_LEAD_DISTRIBUTE_BIZ_DATA> ROW_DATA { get; set; }
    }


    public class LS_LEAD_DISTRIBUTE_BIZ_DATA
    {
        public string adid { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }
        public string contactname { get; set; }
        public string email { get; set; }
        public string shortdesc { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string tags { get; set; }
        public string supercategoryvalue { get; set; }
        public string primarycategoryvalue { get; set; }
        public string clickurl { get; set; }
        public string mobileno { get; set; }
        public string htmltext { get; set; }
        public string secondaryemailid { get; set; }
        public string displayorder { get; set; }
        public string oldclpostid { get; set; }
        public bool ismobile { get; set; }
        public string citystatecode { get; set; }


    }
    public class LS_LEAD_FORM
    {

        public string email { get; set; }
        public string mobileno { get; set; }
        public string ptagid { get; set; }
        public string ptag { get; set; }
        public string leaftagids { get; set; }
        public string leaftags { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string countrycode { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string state { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string Selecteddate { get; set; }
        public int supertagid { get; set; }
        public string supertag { get; set; }
        public string postedvia { get; set; }



    }
}
