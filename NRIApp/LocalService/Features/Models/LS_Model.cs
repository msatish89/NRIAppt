using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    class LS_Model
    {
    }
    public class PRIMARYTAG_LIST
    {
        public List<LS_PRIMARYTAG_LIST> ROW_DATA { get; set; }
    }
    public class LS_PRIMARYTAG_LIST
    {
        public int tagid { get; set; }
        public string primarytag { get; set; }
        public string primarytagurl { get; set; }
        public string tagcnt { get; set; }
        public string totalrecs { get; set; }
        public int pageno { get; set; }
        public int supertagid { get; set; }
        public string supertag { get; set; }
        public string supertagurl { get; set; }

    }

    public class LOCATION_MERGE
    {
        public List<LOCATION_MERGE_DATA> ROW_DS { get; set; }
    }
    public class LOCATION_MERGE_DATA
    {
        public string countrycode { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string statename { get; set; }
        public int zipcode { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public string primaryrecord { get; set; }
        public string cityurl { get; set; }
        public string ispopular { get; set; }
        public string radius { get; set; }
        public int pincode { get; set; }
        public string newcityurl { get; set; }
        public string metrourl { get; set; }
        public string citystatecode { get; set; }
        public string citystatecodeurl { get; set; }
    }


    public class LOCATION
    {
        public List<LOCATION_DATA> ROW_DATA { get; set; }
    }

    public class COMMON_LOCATION
    {
        public List<LOCATION_DATA> ROW_DS { get; set; }
    }
    public class LOCATION_DATA
    {
        public string countrycode { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string statename { get; set; }
        public string zipcode { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public string primaryrecord { get; set; }
        public string cityurl { get; set; }
        public string ispopular { get; set; }
        public string radius { get; set; }
        public string pincode { get; set; }
        public string newcityurl { get; set; }
        public string metrourl { get; set; }
        public string citystatecode { get; set; }
        public string citystatecodeurl { get; set; }
    }
    public class LEAF_TAGS
    {
        public List<LEAF_TAGS_DATA> ROW_DATA { get; set; }
    }

    public class LEAF_TAGS_DATA
    {
        public int tagid { get; set; }
        public string tagurl { get; set; }
        public string tag { get; set; }
    }   

    public class LS_SEARCH
    {
        public List<LS_SEARCH_DATA> ROW_DATA { get; set; }
    }
    public class LS_SEARCH_DATA
    {
        public string ptagid { get; set; }
        public string ptag { get; set; }
        public string ptagurl { get; set; }
        public int ltype { get; set; }
        public int stagid { get; set; }
        public string stag { get; set; }
        public string stagurl { get; set; }
        public int tagcnt { get; set; }


    }
    public class SERVICE_TYPE
    {
        public int id { get; set; }
        public string Servicetype { get; set; }
        public string selectedcolor { get; set; }
    }
    public class RESULT_SET
    {
        public string resultinformation { get; set; }
    }
}
