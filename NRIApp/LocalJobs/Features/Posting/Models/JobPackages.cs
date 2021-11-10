using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Posting.Models
{
   public class JobPackages
    {
        public string amount { get; set; }
        public string ordertype { get; set; }
        public string noofdays { get; set; }
        public decimal cityamount { get; set; }
        public string banneramount { get; set; }
        public string nationwideamount { get; set; }
        public string categoryflag { get; set; }

        public string emailblastamount { get; set; }
        public string addonamount { get; set; }
        public decimal perdaycost { get; set; }

    }
    public class JobPackagelist
    {
       public List<JobPackages> ROW_DATA { get; set; }
    }
    public class JobPayment_Params
    {
        public string months { get; set; }
        public string monthname { get; set; }
        public int years { get; set; }
        public string checkimage { get; set; }
    }

    public class JobPkgsucess
    {
        public string resultinformation { get; set; }
        public string discountamount { get; set; }
        public string totalamount { get; set; }
        public string packageamount { get; set; }
        public string isfreeadpublish { get; set; }
    }
    public class JobPackages_INFO_DATA
    {
        public string orderid { get; set; }
        public string amount { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string ordertype { get; set; }
        public string clickurl { get; set; }
        public string newcityurl { get; set; }
        public string adsid { get; set; }
        public string titleurl { get; set; }

        public string addonamount { get; set; }
        public string banneramount { get; set; }
        public string emailblastamount { get; set; }
    }
    public class JobPackage_INFO
    {
        public List<JobPackages_INFO_DATA> ROW_DATA { get; set; }
    }

    public class perdaycount
    {
        public int daycount { get; set; }
        public string  checkimage { get; set; }
    }
}
