using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.Post.Models
{
    public class Daycareposting
    {
        public string Categorylevel { get; set; }
        public string supercategoryid { get; set; }
        public string primarycategoryid { get; set; }
        public string secondarycategoryid { get; set; }
        public string tertiarycategoryid { get; set; }


       
        public string supercategoryvalue { get; set; }
        public string supercategoryvalueurl { get; set; }
        public string primarycategoryvalue { get; set; }
        public string primarycategoryvalueurl { get; set; }
        public string categoryname { get; set; }
        public string categoryurl { get; set; }
        public string maincategory { get; set; }
        public string adtype { get; set; }

        
        public string businessid { get; set; }
        public string isprefill { get; set; }
        public string emailid { get; set; }
        public string secondarycategoryvalue { get; set; }
        public string tertiarycategoryvalue { get; set; }
        public string contributor { get; set; }
        public string postedby { get; set; }
        public string userpid { get; set; }
        public string ip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double longtitude { get; set; }

        //Added
        public string salary { get; set; }
        public string salarytype { get; set; }
        public string salarytypeid { get; set; }
        public string adtitle { get; set; }
        public string description { get; set; }
        public string contactName { get; set; }
        //public string licenseNo { get; set; }
        public string ContactNo { get; set; }
        public string locationAddress { get; set; }
        public string selcityname { get; set; }
        public string hideemail { get; set; }
        public string photo { get; set; }
        public string videourl { get; set; }
        public string gender { get; set; }
        public string careexperience { get; set; }
        public string languages { get; set; }
        public string certifications { get; set; }
        public string responsetime { get; set; }
        public string drive { get; set; }
        public string vehicle { get; set; }
        public string license { get; set; }
        public string licenseno { get; set; }
        public string activityenrichments { get; set; }
        public string caretypes { get; set; }
        public string jobtype { get; set; }
        public string agegroups { get; set; }
       
        public string premiumad { get; set; }
        public string usertype { get; set; }
        //postsecond
        public string businessname { get; set; }
        public string hidemap { get; set; }

        //postthird
        public string hidephonenumber { get; set; }

        public string otherserviceprovider { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string availablitytable { get; set; }
        public string dayschedule { get; set; }
        //live in live out preference 
        public string preference { get; set; }
        public string countrycode { get; set; }
        public string postedvia { get; set; }
        public string postedviaid { get; set; }
        public string deviceid { get; set; }

        public string maincategoryid { get; set; }
        //mobileverified
        public string pinno { get; set; }
        public string status { get; set; }
        //package 
        public string newcityurl { get; set; }
        public string numberofdays { get; set; }
        public string ordertype { get; set; }
        public string ordergroup { get; set; }
        public string pkgid { get; set; }
        public string summercamp { get; set; }
        public string categoryflag { get; set; }
        public string freeadstatus { get; set; }
        public string addonamount { get; set; }
        public string establishmentyear { get; set; }
        //virtualnanny
        public string virtualnanny { get; set; }
        public string ordertypetxt { get; set; }

        //new proc
        public string newcategoryurl { get; set; }
        //otpresponse
        public string isresponse { get; set; }
        public string supertagid { get; set; }

        public string responsecategorytype { get; set; }
        public string availablefrom { get; set; }
        public string newclickurl { get; set; }
        //myneeds
                    public string Businesstitleurl { get; set; }
                    public string Profiletitle { get; set; }
                    //public string Contactname { get; set; }
					public string Businessaddress { get; set; }
                    //public string Zipcode { get; set; }
                    //public string City { get; set; }
                    //public string State { get; set; }
                    public string Customerid { get; set; }
                    public string Campaignid { get; set; }
                    public string Campaignstarts { get; set; }
                    public string Campaignend { get; set; }
                    //public string Country { get; set; }
					public string Education { get; set; }
                    public string Experience { get; set; }
                    public string Backgroundcheck { get; set; }
                    public string Salarydescription { get; set; }
                    public string Perkdescription { get; set; }
                    public string Workingfrom { get; set; }
                    public string Workingto { get; set; }
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
                    //public string Premiumad { get; set; }
                    //public string Availablefrom { get; set; }
                    public string Availableto { get; set; }
                    public string Neededdays { get; set; }
                    public string Salarymode { get; set; }
                    public string Salarymodeid { get; set; }
                    //public string Gender { get; set; }
                    //public string Userpid { get; set; }
                    public string Startdate { get; set; }
                    public string Enddate { get; set; }
                    public string Verifieddate { get; set; }
                    public string Status { get; set; }
                    public string Agegroupid { get; set; }
					public string Supertag { get; set; }
                    //public string Supertagid { get; set; }
					public string Primarytag { get; set; }
                    public string Cityurl { get; set; }
                    public string Languageid { get; set; }
                    //public string Language { get; set; }
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
					public string Photoid { get; set; }
					public string Videoid { get; set; }
					public string Externalvideourl { get; set; }
                    public string Britecode { get; set; }
                    public string Age { get; set; }
                    public string Website { get; set; }
                    public string Agentid { get; set; }
                    //public string Newcityurl { get; set; }
                    public string Folderid { get; set; }
                    //public string Ordergroup { get; set; }
                    //public string Ordertype { get; set; }
                    //public string Numberofdays { get; set; }
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
                    //public string Usertype { get; set; }
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
                    //public string Addonamount { get; set; }
                    public string Addontype { get; set; }
                    public string Categoryflag { get; set; }
                    public string Businessorigin { get; set; }
                    public string Adsourceurl { get; set; }
                    public string Packageid { get; set; }
        public string ismyneed { get; set; }
        public string ismyneedpayment { get; set; }
        public string mode { get; set; }
        public string workdesc { get; set; }
        public string salaryratemax { get; set; }

        public int tranportation { get; set; }
        public int comfortablewithpets { get; set; }
        public string drivinglicense { get; set; }
        public int smoke { get; set; }
    }
  
    public class Daycareposting_Result
    {
        public string result { get; set; }
        public string pinno { get; set; }

        //postfirstresults
        public string resultinformation { get; set; }
        public string businessid { get; set; }
        public string maincategoryid { get; set; }
        public string agentid { get; set; }
        public string usertype { get; set; }
        public string premiumad { get; set; }
        //mobileverification
        
        public string otpsend { get; set; }
        public string ispayment { get; set; }

        public string otpno { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
        public string blockstatus { get; set; }
        public string freeadstatus { get; set; }
    }
    //agegroup & Needed service
    public class careservicedata
    {
        public string supertagid { get; set; }
        public string supertag { get; set; }
        public string supertagurl { get; set; }
        public string primarytagid { get; set; }
        public string primarytag { get; set; }
        public string primarytagurl { get; set; }
        public int isapproved { get; set; }
    }

    public class careservice
    {
        public List<careservicedata> ROW_DATA { get; set; }
    }
    
    public class qanda
    {
        public List<qandadata> ROW_DATA { get; set; }
    }
    public class qandadata
    {
        public string questionid { get; set; }
        public string question { get; set; }
        public string xml_answers { get; set; }
        public string questiongroup { get; set; }
        public string isanswered { get; set; }
        public string controltype { get; set; }
        public string xml_answeredas { get; set; }
        public string questionorder { get; set; }
    }
    public class Options
    {
        public List<string> option { get; set; }
    }

    public class optiondata
    {
        public Options options { get; set; }
    }
    public class tset
    {
        public string tsetval { get; set; }
        public string tsetvalimg { get; set; }
        public string tsetvalID { get; set; }
    }
    public class dchourtime
    {
        public string day { get; set; }
        public string dayidvalue { get; set; }
        public string fromhour { get; set; }
        public string tohour { get; set; }
        public string hourID { get; set; }
    }
    public class DC_Availability
    {
        public string weekdays { get; set; }
        public string timings { get; set; }
        public string timingsID { get; set; }
        public string weekdayID { get; set; }
        public string CheckImage { get; set; }
        public string avalilabilityID { get; set; }
    }

    //OTP
    public class results
    {
        public string otpsend { get; set; }
        public string otpno { get; set; }
        public string respid { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
    }
    public class results_otp
    {
        public string otpsend { get; set; }
        public string otpno { get; set; }
        public string respid { get; set; }
        public string mobileno { get; set; }
        public string countrycode { get; set; }
    }

    //  getotherproviders
    public class Otherprovider_DATA
    {
        public int supercategoryid { get; set; }
        public int primarycategoryid { get; set; }
        public int secondarycategoryid { get; set; }
        public int tertiarycategoryid { get; set; }
        public string otherprovider { get; set; }
        public string adtnlcats { get; set; }
        public string supertagurl { get; set; }
    }

    public class Otherprovider
    {
        public List<Otherprovider_DATA> ROW_DATA { get; set; }
    }
    public class Orderreceipt
    {
        public List<Orderreceipt_data> ROW_DATA { get; set; }
    }

    public class Orderreceipt_data
    {
        public string Businessid { get; set; }
        public string Maincategory { get; set; }
        public string Ordertype { get; set; }
        public string Mailerstatus { get; set; }
        public string Status { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public string Verifieddate { get; set; }
        public string Crdate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Contributor { get; set; }
        public string Orderid { get; set; }
        public string Amount { get; set; }
        public string Adtitle { get; set; }
        public string Discountamount { get; set; }
        public string Packageamount { get; set; }
        public string Paidamount { get; set; }
        public string Addonamount { get; set; }
        public string Transid { get; set; }
        public string Cardname { get; set; }
        public string newlinkurl { get; set; }
    }

}
