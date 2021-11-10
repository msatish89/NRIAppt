using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Roommates.Features.Post.Models
{
   public class Packages
    {
        public string amount { get; set; }
        public string ordertype { get; set; }
        public string noofdays { get; set; }
        public string featuretitle { get; set; }
    }
    public class Packagelist
    {
       public List<Packages> ROW_DATA { get; set; }
    }
    public class Payment_Params
    {
        public string months { get; set; }
        public string monthname { get; set; }
        public int years { get; set; }
        public string checkimage { get; set; }
    }

    public class Pkgsucess
    {
        public string resultinformation { get; set; }
        public string discountamount { get; set; }
        public string totalamount { get; set; }
        public string packageamount { get; set; }
        public string isfreeadpublish { get; set; }
        public string couponapplied { get; set; }
         
    }
    public class Packages_INFO_DATA
    {
        public string orderid { get; set; }
        public string amount { get; set; }
        public string adpostdate { get; set; }
        public string adexpirydate { get; set; }
        public string packagetype { get; set; }
        public string linkurl { get; set; }
        public string newcityurl { get; set; }
        public string adid { get; set; }
    }
    public class Package_INFO
    {
        public List<Packages_INFO_DATA> ROW_DATA { get; set; }
    }

    
}
