using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class Package
    {

    }

    public class LS_PACKAGE
    {
        public List<LS_PACKAGE_DATA> ROW_DATA { get; set; }
    }
    public class LS_PACKAGE_DATA
    {
        public string service { get; set; }
        public string ordertype { get; set; }
        public int noofdays { get; set; }
        public string usertype { get; set; }
        public string amount { get; set; }
        public string originalamt { get; set; }
        public string savepercentage { get; set; }
        public string saveperday { get; set; }
        public int mcamount { get; set; }
        public int mcoriginalamt { get; set; }
        public string mcsavepercentage { get; set; }
        public string mcsaveperday { get; set; }
        public int addonamountbasic { get; set; }
        public int addonamountsilver { get; set; }
        public int addonamountbasic_contentid { get; set; }
        public int addonamountsilver_contentid { get; set; }
        public int addonamountbasic_originalamt { get; set; }
        public string addonamountbasic_savepercentage { get; set; }
        public string addonamountbasic_saveperday { get; set; }
        public int addonamountsilver_originalamt { get; set; }
        public string addonamountsilver_savepercentage { get; set; }
        public string addonamountsilver_saveperday { get; set; }
    }

    public class SELECTED_PACKAGE
    {
        public string adid { get; set; }
        public string noofdays { get; set; }
        public decimal packageamount { get; set; }
        public decimal valueserviceamount { get; set; }
        public decimal addtionalcitiesamount { get; set; }
        public decimal amount { get; set; }
        public decimal couponamount { get; set; }
        public decimal mcamount { get; set; }
        public decimal mcoriginalamt { get; set; }
        public string mcsavepercentage { get; set; }
        public string mcsaveperday { get; set; }
    }

    public class LS_VALUE_GROUP_SERVICES
    {
        public string Heading { get; set; }
        public string image { get; set; }
        public List<LS_VALUE_GROUP_SERVICES_DATA> Services { get; set; }
        public LS_VALUE_GROUP_SERVICES() { }
        public LS_VALUE_GROUP_SERVICES(string name, List<LS_VALUE_GROUP_SERVICES_DATA> SERVICES)
        {
            Heading = Heading;
            Services = SERVICES;
        }
    }
    public class LS_VALUE_GROUP_SERVICES_DATA
    {
        public string title { get; set; }
        public string features { get; set; }
        public string isactive { get; set; }

    }
    public class LS_VALUE_ADD_SERVICE
    {
        public List<LS_VALUE_ADD_SERVICE_DATA> ROW_DATA { get; set; }
    }

    public class LS_VALUE_ADD_SERVICE_DATA
    {
        public string contentid { get; set; }
        public string title { get; set; }
        public string features { get; set; }
        public string crdate { get; set; }
        public string isactive { get; set; }
        public decimal addonamountbasic { get; set; }
        public string addonamountbasic_contentid { get; set; }
        public float addonamountbasic_originalamt { get; set; }
        public string addonamountbasic_savepercentage { get; set; }
        public string addonamountbasic_saveperday { get; set; }
    }

    public class LS_COUPON_SUCCESS
    {
        public List<LS_COUPON_SUCCESS_DATA> ROW_DATA { get; set; }
    }
    public class LS_COUPON_SUCCESS_DATA
    {
        public string couponstatus { get; set; }
        public decimal discountamount { get; set; }
        public string coupondescription { get; set; }
    }

    public class COUPON_DATA
    {
        public string couponcode { get; set; }
        public decimal mccitycount { get; set; }
        public string noofdays { get; set; }
        public string addonordertype { get; set; }
        public string email { get; set; }
    }
    public class ORDER_SUMMARY
    {
        public string cityurl { get; set; }
        public string adid { get; set; }
        public string city { get; set; }
        public string addonordertype { get; set; }
        public string couponcode { get; set; }
        public string salespersonemail { get; set; }
        public string emailblast_4 { get; set; }
        public string postedviaid { get; set; }
        public string postedvia { get; set; }
        public string contactemail_4 { get; set; }
        public string noofdays { get; set; }
        public string userpid { get; set; }

    }

    public class ORDER_SUMMARY_RESULT
    {
        public List<ORDER_SUMMARY_RESULT_DATA> ROW_DATA { get; set; }
    }
    public class ORDER_SUMMARY_RESULT_DATA
    {
        public string result { get; set; }
        public string adid { get; set; }
        public string couponstatus { get; set; }
        public string discountamount { get; set; }
    }

    public class PAYMENT_RESULT
    {
        public List<PAYMENT_RESULT_DATA> ROW_DATA { get; set; }
    }
    public class PAYMENT_RESULT_DATA
    {
        public string result { get; set; }
        public string adid { get; set; }
        public string guid { get; set; }
    }
    public class LS_FLEX_PACKAGE
    {
        public List<LS_FLEX_PACKAGE_DATA> ROW_DATA { get; set; }
    }

    public class LS_FLEX_PACKAGE_DATA
    {
        public string perlead_amount { get; set; }
        public int pack1_days { get; set; }
        public int pack2_days { get; set; }
        public int pack3_days { get; set; }
        public int pack4_days { get; set; }
        public int pack5_days { get; set; }
        public int pack6_days { get; set; }
        public int pack1_months { get; set; }
        public int pack2_months { get; set; }
        public int pack3_months { get; set; }
        public int pack4_months { get; set; }
        public int pack5_months { get; set; }
        public int pack6_months { get; set; }
        public int pack1_id { get; set; }
        public int pack2_id { get; set; }
        public int pack3_id { get; set; }
        public int pack4_id { get; set; }
        public int pack5_id { get; set; }
        public int pack6_id { get; set; }

        public string pack1_name { get; set; }
        public string pack2_name { get; set; }
        public string pack3_name { get; set; }
        public string pack4_name { get; set; }
        public string pack5_name { get; set; }
        public string pack6_name { get; set; }
        public float pack1_overall_amount { get; set; }
        public float pack2_overall_amount { get; set; }
        public float pack3_overall_amount { get; set; }
        public float pack4_overall_amount { get; set; }
        public float pack5_overall_amount { get; set; }
        public float pack6_overall_amount { get; set; }
        public float pack1_overall_amount_max { get; set; }
        public float pack2_overall_amount_max { get; set; }
        public float pack3_overall_amount_max { get; set; }
        public float pack4_overall_amount_max { get; set; }
        public float pack5_overall_amount_max { get; set; }
        public float pack6_overall_amount_max { get; set; }

        public float pack1_lead_internal { get; set; }
        public float pack2_lead_internal { get; set; }
        public float pack3_lead_internal { get; set; }
        public float pack4_lead_internal { get; set; }
        public float pack5_lead_internal { get; set; }
        public float pack6_lead_internal { get; set; }
        public float pack1_lead_internal_max { get; set; }
        public float pack2_lead_internal_max { get; set; }
        public float pack3_lead_internal_max { get; set; }
        public float pack4_lead_internal_max { get; set; }
        public float pack5_lead_internal_max { get; set; }
        public float pack6_lead_internal_max { get; set; }
        public int stepno { get; set; }
        public string city { get; set; }
        public string ptag { get; set; }
        public string designtype { get; set; }
    }

    public class LS_AD_GROUP_FEATURES
    {
        public string Heading { get; set; }
        public string image { get; set; }
        public List<LS_AD_GROUP_FEATURES_DATA> Services { get; set; }
        public LS_AD_GROUP_FEATURES() { }
        public LS_AD_GROUP_FEATURES(string name, List<LS_AD_GROUP_FEATURES_DATA> SERVICES)
        {
            Heading = Heading;
            Services = SERVICES;
        }
    }
    public class LS_AD_GROUP_FEATURES_DATA
    {
        public string daysfeatures { get; set; }
    }
    public class LS_PAYMENT_FEATURE
    {
        public string Heading { get; set; }
        public string image { get; set; }
        public List<LS_PAYMENT_FEATURES> Services { get; set; }
        public LS_PAYMENT_FEATURE() { }
        public LS_PAYMENT_FEATURE(string name, List<LS_PAYMENT_FEATURES> SERVICES)
        {
            Heading = Heading;
            Services = SERVICES;
        }
    }

    public class LS_PAYMENT_FEATURES
    {
        public string head { get; set; }

        public string desc { get; set; }
        public string image { get; set; }
        public string listtype { get; set; }
        public bool isshow { get; set; }

    }

    public class LS_FLEX_SELECT_PACKAGE
    {
        public string noofdays { get; set; }
        public float package { get; set; }
        public float couponamt { get; set; }
        public float finalamt { get; set; }
        public string packagetype { get; set; }
        public string oldclpostid { get; set; }
        public string hdnactualamount { get; set; }
        public string primarycategory { get; set; }
    }

    public class LS_FLEX_SELECTED_RESULT
    {
        public List<LS_FLEX_SELECTED_RESULT_DATA> ROW_DATA { get; set; }
    }

    public class LS_FLEX_SELECTED_RESULT_DATA
    {
        public string name { get; set; }
        public string result { get; set; }
        public string guid { get; set; }
        public string renewupgrageadid { get; set; }
        public string passleadid { get; set; }
        public string pendingadid { get; set; }
    }



    public class LS_PRIME_BY_GUID_DATA
    {
        public string primeid { get; set; }
        public string guid { get; set; }
        public string days { get; set; }
        public string package_name { get; set; }
        public string overall_amount { get; set; }
        public string overall_amount_disc { get; set; }
        public string passed_amount { get; set; }
        public string package_amount { get; set; }
        public string addon_amount { get; set; }
        public string contactname { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string altmobileno { get; set; }
        public string billingemail { get; set; }
        public string streetname { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string zipcode { get; set; }
        public string lat { get; set; }


        public string statecode { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string country { get; set; }
        public string billingtype { get; set; }
        public string OrderID { get; set; }
        public string paid_amount { get; set; }
        public string upddate { get; set; }
        public string linkstatus { get; set; }
        public string oldclpostid { get; set; }
        public string couponcode { get; set; }
        public string discountamount { get; set; }
        public string ptagid { get; set; }
        public string packassured { get; set; }
        public string packagetype { get; set; }
        public string clientapprsaleswip { get; set; }
        public string salesemail { get; set; }
        public string clientswipeallowvia { get; set; }
        public string billingstreetname { get; set; }
        public string pricingtypevia { get; set; }

        public string title { get; set; }
    }

    public class LS_PRIME_BY_GUID
    {
        public List<LS_PRIME_BY_GUID_DATA> ROW_DATA { get; set; }
    }

    public class LS_PAYMENT_FEATURES_DESC
    {
        public string desc { get; set; }
    }
}
