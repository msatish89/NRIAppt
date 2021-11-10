using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.Post.Models
{

    public class DCCategoryRowData
    {
        public List<DC_CategoryList> ROW_DATA { get; set; }
    }
    public class DC_CategoryList
    {
        public string supercategoryid { get; set; }
        public string supercategoryvalue { get; set; }
        public string primarycategoryid { get; set; }
        public string primarycategoryvalue { get; set; }
        public string secondarycategoryid { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string tertiarycategoryid { get; set; }
        public string tertiarycategoryvalue { get; set; }
        public string categoryname { get; set; }
        public string maincategory { get; set; }
        public string maincategoryid { get; set; }
        public string adtype { get; set; }
        public string newcategoryurl { get; set; }

        public string categoryimgurl { get; set; }

       
        public string LocationName { get; set; }
        public string MobileNumber { get; set; }
        public string Locationname { get; set; }
        public string Newcityurl { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Countrycode { get; set; }
        public string txtcontactphone { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Country { get; set; }
        public string gender { get; set; }
        public string ExpSalary { get; set; }

        public string NeedFromDate { get; set; }
        public string NeedToDate { get; set; }

        public string textcolor { get; set; }
        public string categoryurl { get; set; }

        //myneeds
        public string Businessid { get; set; }
        public string Businessname { get; set; }
        public string Businesstitleurl { get; set; }
        public string Profiletitle { get; set; }
        //public string Contactname { get; set; }
        public string Licenseno { get; set; }
        public string Businessname1 { get; set; }
        public string Businesstitleurl1 { get; set; }
        public string Businessaddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Customerid { get; set; }
        public string Campaignid { get; set; }
        public string Campaignstarts { get; set; }
        public string Campaignend { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Backgroundcheck { get; set; }
        public string Salarydescription { get; set; }
        public string Perkdescription { get; set; }
        public string Workingfrom { get; set; }
        public string Workingto { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Genderid { get; set; }
        public string Preferredworktiming { get; set; }
        public string Staffratio { get; set; }
        public string Showvn { get; set; }
        public string Showvs { get; set; }
        public string Milestotravel { get; set; }
        public string Noofkids { get; set; }
        public string Noofadults { get; set; }
        public string Noofspecialneeds { get; set; }
        public string Salaryrate { get; set; }
        public string Perkscost { get; set; }
        public string Premiumad { get; set; }
        public string Availablefrom { get; set; }
        public string Availableto { get; set; }
        public string Neededdays { get; set; }
        public string Salarymode { get; set; }
        public string Salarymodeid { get; set; }
        public string Gender { get; set; }
        public string Userpid { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public string Verifieddate { get; set; }
        public string Status { get; set; }
        public string Agegroupid { get; set; }
        public string Agegroup { get; set; }
        public string Supertag { get; set; }
        public string Supertagid { get; set; }
        public string Primarytagid { get; set; }
        public string Primarytag { get; set; }
        public string Cityurl { get; set; }
        public string Languageid { get; set; }
        public string Language { get; set; }
        public string Trainingid { get; set; }
        public string Training { get; set; }
        public string Airportcode { get; set; }
        public string Statecode { get; set; }
        public string Noofpersons { get; set; }
        public string Neededtime { get; set; }
        public string Category { get; set; }
        public string Statename { get; set; }
        public string Metro { get; set; }
        public string Nooflivefreeads { get; set; }
        public string Freeadposted { get; set; }
        public string Freeliveadid { get; set; }
        public string Freeliveadtitle { get; set; }
        public string Freeliveadtitleurl { get; set; }
        public string Freeliveadnewcityurl { get; set; }
        public string Metrourl { get; set; }
        public string Radius { get; set; }
        //public string Maincategoryid { get; set; }
        public string Photoid { get; set; }
        public string Photos { get; set; }
        public string Videoid { get; set; }
        public string Videourl { get; set; }
        public string Externalvideourl { get; set; }
        public string Britecode { get; set; }
        public string Age { get; set; }
        public string Website { get; set; }
        public string Agentid { get; set; }
        public string Folderid { get; set; }
        public string Ordergroup { get; set; }
        public string Ordertype { get; set; }
        public string Numberofdays { get; set; }
        public string Upgradeordergroup { get; set; }
        public string Upgradeordertype { get; set; }
        public string Upgradenumberofdays { get; set; }
        public string Alladid { get; set; }
        public string Services { get; set; }
        public string Worktype { get; set; }
        public string Carespecialchild { get; set; }
        public string Hideemail { get; set; }
        public string Hideaddress { get; set; }
        public string Classtypeid { get; set; }
        public string Classtype { get; set; }
        public string Pettypeid { get; set; }
        public string Pettype { get; set; }
        public string Ismobileverified { get; set; }
        public string Contentid { get; set; }
        public string Primarycategoryvalueurl { get; set; }
        public string Secondarycategoryvalueurl { get; set; }
        public string Agentid1 { get; set; }
        public string Agentstatus { get; set; }
        public string Totalads { get; set; }
        public string Adsposted { get; set; }
        public string Noofadslive { get; set; }
        public string Noofadsremain { get; set; }
        public string Licenseno1 { get; set; }
        public string Agentdisplayphone { get; set; }
        public string Agentcity { get; set; }
        public string Agentzipcode { get; set; }
        public string Agentstatecode { get; set; }
        public string Agentcountry { get; set; }
        public string Agentbusinessname { get; set; }
        public string Agentaddress { get; set; }
        public string Agentphone { get; set; }
        public string Usertype { get; set; }
        public string Agentemail { get; set; }
        public string Agentphotourl { get; set; }
        public string Agentadordertype { get; set; }
        public string Packageexpiredate { get; set; }
        public string Packageexpiredays { get; set; }
        public string Agentmetros { get; set; }
        public string Availability { get; set; }
        public string Amount { get; set; }
        public string Discountamt { get; set; }
        public string Paymentamt { get; set; }
        public string Couponcode { get; set; }
        public string Addonamount { get; set; }
        public string Addontype { get; set; }
        public string Categoryflag { get; set; }
        public string Businessorigin { get; set; }
        public string Adsourceurl { get; set; }
        public string Packageid { get; set; }
        public string Virtualnanny { get; set; }
        public string ismyneed { get; set; }
        public string isprefill { get; set; }
        public string ismyneedpayment { get; set; }
        public string vehicle { get; set; }
        public string drive { get; set; }
        public string responsetime { get; set; }
        public string certifications { get; set; }
        public string activityenrichments { get; set; }
        public string availablitytable { get; set; }
    }

    public class ListOfLocationData
    {
        public List<LocationData> ROW_DATA { get; set; }
    }
    public class LocationData
    {

        public string city { get; set; }
        public string statecode { get; set; }

        public string statename { get; set; }
        public string newcityurl { get; set; }
        public string zipcode { get; set; }

        public string longitude { get; set; }
        public string latitude { get; set; }
        public string country { get; set; }
        public string citystatecode { get; set; }
    }

    public class DC_ListofResult
    {

        public List<Result_Data> ROW_DATA { get; set; }
    }

    public class Result_Data
    {

        public string result { get; set; }
        public int allrespid { get; set; }
        public string adid { get; set; }
        public string metrourl { get; set; }
        public string metro { get; set; }

    }

    //public class Top_Business
    //{
    //    public List<Top_Business_Data> ROW_DATA { get; set; }
    //}
    //public class Top_Business_Data
    //{
    //    public int adid { get; set; }
    //    public string email { get; set; }
    //    public string title { get; set; }
    //    public string contactname { get; set; }

    //    public string clickurl { get; set; }
    //}

}
