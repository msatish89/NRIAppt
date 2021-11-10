using Newtonsoft.Json;
using NRIApp.DayCare.Features.Post.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.MyNeeds.Features.Models
{
    public class MyNeedData
    {
        public string title { get; set; }
        public string titleurl { get; set; }
        public string addate { get; set; }
        public string adtype { get; set; }
        public string responses { get; set; }
        public string adstatus { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string services { get; set; }

        public string adid { get; set; }
        public string contributor { get; set; }
        public string oldclpostid { get; set; }
        public string location { get; set; }
        public bool AdTitlevisible { get; set; }
        public bool LSNeedData { get; set; }
        public string LSAdimg { get; set; }
        public string premiumad { get; set; }

        public bool disablevisible { get; set; }
        public bool enablevisible { get; set; }
        public bool renewvisible { get; set; }
        public bool upgradevisible { get; set; }
        public bool editvisible { get; set; }
        public bool paymentvisible { get; set; }
        public bool nooptionvisible { get; set; }
        public bool sendverificationmailvisible { get; set; }
        public bool viewadvisible { get; set; }
        public string partialstepads { get; set; }
        public bool responsevisible { get; set; }
        public string unreadcount { get; set; }
        public bool unreadcountvisible { get; set; }
        //daycare
        public string contentid { get; set; }
        public bool premiumadreceiptvisible { get; set; }
        //jobs
        public string newcityurl { get; set; }
        //dc
        public string adstatustxt { get; set; }
        public bool adstatusvisible { get; set; }
        public bool adidvisible { get; set; }

        public string jobrole { get; set; }

    }
    public class MyNeedDataDetails
    {
        public List<MyNeedData> ROW_DATA { get; set; }
    }

    public class Myneedpartialad
    {
        public List<MyNeedpartialData> ROW_DATA { get; set; }
    }

    public class MyNeedpartialData
    {
        public string adid { get; set; }
        public string title { get; set; }
        public string titlerep { get; set; }
        public string streetname { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string statecode { get; set; }
        public string tag { get; set; }
        public string LSAdimg { get; set; }
        public bool LSNeedData { get; set; }
        public string stepno { get; set; }
        public string profileimgurl { get; set; }
        public string primarytag { get; set; }
        public string phoneno { get; set; }

        public bool paymentvisible { get; set; }
        public bool finishlistvisible { get; set; }
    }

    public class AdResultData
    {
        public string resultinformation { get; set; }
    }

    public  class EditAdDetail
    {
        [JsonProperty("ROW_DATA")]
        public List<EditAdDetail_Data> ROW_DATA { get; set; }
    }

    public  class EditAdDetail_Data
    {
        public string Service { get; set; }
        public int Adid { get; set; }
        //public int Adsid { get; set; }
        public int Agentid { get; set; }
        public string Isagent { get; set; }
        public string XmlPhotopath { get; set; }
        public string Imagepath { get; set; }
        public string HtmlPhotopath { get; set; }
        public string Photoscnt { get; set; }
        public string Ispaid { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Adtype { get; set; }
        public string Additionalcity { get; set; }
        public string Citycount { get; set; }
        public string Metrototalamount { get; set; }
        public string Citytotalamount { get; set; }
        public int Supercategoryid { get; set; }
        public int Primarycategoryid { get; set; }
        public int Secondarycategoryid { get; set; }
        public string Supercategoryvalue { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Secondarycategoryvalue { get; set; }
        public string Tertiarycategoryid { get; set; }
        public string Tertiarycategoryvalue { get;set; }
        public string Tags { get; set; }
        public string Businessname { get; set; }
        public string licenseno { get; set; }
        public string Companyweburl { get; set; }
        public string Url { get; set; }
        public string Supertagid { get; set; }
        public string Bestindustry { get; set; }
        public string Minsalary { get; set; }
        public string Maxsalary { get; set; }
        public string Qualification { get; set; }
        public string Visastatus { get; set; }
        public string Experience { get; set; }
        public string Experiencefrom { get; set; }
        public string Experienceto { get; set; }
        public string Noofopening { get; set; }
        public string Employmenttype { get; set; }
        public string Jobtype { get; set; }
        public string Jobroleid { get; set; }
        public string Jobrole { get; set; }
        public string Isresumeavail { get; set; }
        public string Hideaddress { get; set; }
        public string Linkedinurl { get; set; }
        public string Trimetros { get; set; }
        public string Nationwide { get; set; }
        public string Salarymode { get; set; }
        public string Salarymodeid { get; set; }
        public string Streetname { get; set; }
        public string Zipcode { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Newcityurl { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string Titleurl { get; set; }
        public string Description { get; set; }
        public string Contributor { get; set; }
        public string Status { get; set; }
        public string Displayphone { get; set; }
        public string Mailerstatus { get; set; }
        public string Userpid { get; set; }
        public string Ordertype { get; set; }
        public string Customerid { get; set; }
        public string Campaignid { get; set; }
        public string Postedby { get; set; }
        public string Enddate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Startdate { get; set; }
        public string Numberofdays { get; set; }
        public string Amount { get; set; }
        public string Folderid { get; set; }
        public string Countrycode { get; set; }
        public string Ismobileverified { get; set; }
        public string Statecode { get; set; }
        public string Metro { get; set; }
        public string Metrourl { get; set; }
        public string Supercategoryvalueurl { get; set; }
        public string Primarycategoryvalueurl { get; set; }
        public string Secondarycategoryvalueurl { get; set; }
        public string Industryid { get; set; }
        public string Industryname { get; set; }
        public string Orderid { get; set; }
        public string Expirydate { get; set; }
        public string Noofadcnt { get; set; }
        public string Adpostedcnt { get; set; }
        public string Responsemail { get; set; }
        public string Gender { get; set; }
        public string Disclosedbusiness { get; set; }
        public string Couponcode { get; set; }
        public string Salespersonemail { get; set; }
        public string Companytype { get; set; }
        public string Bannerstatus { get; set; }
        public string Banneramount { get; set; }
        public string Categoryflag { get; set; }
        public string Freeadsflag { get; set; }
        public string Islisting { get; set; }
        public string Addonamount { get; set; }
        public string Emailblastamount { get; set; }
        public string Bonusdays { get; set; }
        public string Bonusamount { get; set; }
        public string Pendingpaycouponcode { get; set; }
        public string Benefits { get; set; }
        public string Rent { get; set; }
        public string attachedbaths { get; set; }
        public string availablefrm { get; set; }
        public string Movedate { get; set; }
        public string stayperiod { get; set; }
        public string accomodate { get; set; }
        public string pricemode { get; set; }
        //rentals
        //public int tertiarycategoryid { get; set; }
        //public string tertiarycategoryvalue { get; set; }
        public string noofbaths { get; set; }
        public string area { get; set; }

        //daycare needs
        public string Businesstitleurl { get; set; }
        public string Profiletitle { get; set; }
        public string Contactname { get; set; }
        public string Businessaddress { get; set; }
        public string Campaignstarts { get; set; }
        public string Campaignend { get; set; }
        public string Education { get; set; }
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
        public string Premiumad { get; set; }
        public string Availablefrom { get; set; }
        public string Availableto { get; set; }
        public string Neededdays { get; set; }
        public string Verifieddate { get; set; }
        public string Agegroupid { get; set; }
        public string Agegroup { get; set; }
        public string Primarytagid { get; set; }
        public string Supertag { get; set; }
        public string Primarytag { get; set; }
        public string Cityurl { get; set; }
        public string Languageid { get; set; }
        public string Language { get; set; }
        public string Trainingid { get; set; }
        public string Training { get; set; }
        public string Airportcode { get; set; }
        public string Noofpersons { get; set; }
        public string Neededtime { get; set; }
        public string Statename { get; set; }
        public string Nooflivefreeads { get; set; }
        public string Freeadposted { get; set; }
        public string Freeliveadid { get; set; }
        public string Freeliveadtitle { get; set; }
        public string Freeliveadtitleurl { get; set; }
        public string Freeliveadnewcityurl { get; set; }
        public string Radius { get; set; }
        public string Photoid { get; set; }
        public string Videoid { get; set; }
        public string Externalvideourl { get; set; }
        public string Britecode { get; set; }
        public string Age { get; set; }
        public string Website { get; set; }
        public string Ordergroup { get; set; }
        public string Upgradeordergroup { get; set; }
        public string Upgradeordertype { get; set; }
        public string Upgradenumberofdays { get; set; }
        public string Alladid { get; set; }
        public string Services { get; set; }
        public string Worktype { get; set; }
        public string Carespecialchild { get; set; }
        public string Hideemail { get; set; }
        public string Classtypeid { get; set; }
        public string Classtype { get; set; }
        public string Pettypeid { get; set; }
        public string Pettype { get; set; }
        public string Contentid { get; set; }
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
        public string Discountamt { get; set; }
        public string Paymentamt { get; set; }
        public string Addontype { get; set; }
        public string Businessorigin { get; set; }
        public string Adsourceurl { get; set; }
        public string Packageid { get; set; }
        public string Virtualnanny { get; set; }
        public string Maincategoryid { get; set; }
        public string Photos { get; set; }
        public string Videourl { get; set; }
    }

    public class Ad_Response
    {
        [JsonProperty("ROW_DATA")]
        public List<Ad_Response_Data> RowData { get; set; }
    }

    public class Ad_Response_Data
    {
        public string Totalrecs { get; set; }
        public string Pageno { get; set; }
        public string Rowstofetch { get; set; }
        public string Rowid { get; set; }
        public string Contentid { get; set; }
        public string Respid { get; set; }
        public string Adid { get; set; }
        public string Premiumad { get; set; }
        public string Crdate { get; set; }
        public string Maincatid { get; set; }
        public string Subcatid { get; set; }
        public string Isvalid { get; set; }
        public string Countrylocid { get; set; }
        public string Statelocid { get; set; }
        public string Citylocid { get; set; }
        public string Locationid { get; set; }
        public string Title { get; set; }
        public string Shortdesc { get; set; }
        public string Ip { get; set; }
        public string Responsetype { get; set; }
        public string Userpid { get; set; }
        public string Contributor { get; set; }
        public string Baseaddress { get; set; }
        public string Email { get; set; }
        public string Mobileno { get; set; }
        public string phoneimage { get; set; }
        public string Usertype { get; set; }
        public string City { get; set; }
        public string Titleurl { get; set; }
        public string Contactname { get; set; }
        public string Oldclrespid { get; set; }
        public string Adphonenumber { get; set; }
        public string ActiveResponse { get; set; }
        public string Unreadcount { get; set; }
        public string Postedago { get; set; }
        public string Responseread { get; set; }
        public string Lastresponded { get; set; }
        public string Services { get; set; }
        public string Serviceurl { get; set; }
        public string Postedviaid { get; set; }
        public string Postedvia { get; set; }
        public string Oldclpostid { get; set; }
        public string Ismobileverified { get; set; }
        public string Emailverified { get; set; }
        public string Recaptchascore { get; set; }
        public string isresume { get; set; }
        public bool isresumevisible { get; set; }
        //replytoresponse
        public string replytext { get; set; }
        public string businessid { get; set; }
        public string contactnameimg { get; set; }
    }

    public class MyneedsResult
    {
        public List<Result_Data> ROW_DATA { get; set; }
    }
    public  class Adreceipt
    {
        [JsonProperty("ROW_DATA")]
        public List<Adreceipt_DATA> RowData { get; set; }
    }
    public  class Adreceipt_DATA
    {
        public string Adid { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Adtype { get; set; }
        public string Ordertype { get; set; }
        public string Mailerstatus { get; set; }
        public string Status { get; set; }
        public string Adsid { get; set; }
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
        public string Transid { get; set; }
        public string Cardname { get; set; }
        public string Numberofdays { get; set; }
        public string Agentid { get; set; }
        public string Recruiterid { get; set; }
        public string Adlinkurl { get; set; }
    }

    public class MyNeedsDC
    {
        public List<MyNeedsDC_Data> ROW_DATA { get; set; }
    }
    public partial class MyNeedsDC_Data
    {
        public string Businessid { get; set; }
        public string Businessname { get; set; }
        public string Businesstitleurl { get; set; }
        public string Profiletitle { get; set; }
        public string Contactname { get; set; }
        public string Licenseno { get; set; }
        public string Adtype { get; set; }
        public string Businessname1 { get; set; }
        public string Businesstitleurl1 { get; set; }
        public string Businessaddress { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Customerid { get; set; }
        public string Campaignid { get; set; }
        public string Campaignstarts { get; set; }
        public string Campaignend { get; set; }
        public string Country { get; set; }
        //public string Crdate { get; set; }
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
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Maincategoryid { get; set; }
        public string Photoid { get; set; }
        public string Photos { get; set; }
        public string Videoid { get; set; }
        public string Videourl { get; set; }
        public string Externalvideourl { get; set; }
        public string Britecode { get; set; }
        public string Age { get; set; }
        public string Website { get; set; }
        public string Agentid { get; set; }
        public string Newcityurl { get; set; }
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
        public string Supercategoryid { get; set; }
        public string Supercategoryvalue { get; set; }
        public string Primarycategoryid { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Secondarycategoryid { get; set; }
        public string Secondarycategoryvalue { get; set; }
        public string Tertiarycategoryid { get; set; }
        public string Tertiarycategoryvalue { get; set; }
        public string Countrycode { get; set; }
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
        public string Primarytagmobile { get; set; }
        public string categoryvalues { get; set; }
        public string salaryratemax { get; set; }

        public int tranportation { get; set; }
        public int comfortablewithpets { get; set; }
        public string drivinglicense { get; set; }
        public int smoke { get; set; }
        public string jobtype { get; set; }
    }

    public partial class LSResponse
    {
        [JsonProperty("ROW_DATA")]
        public List<LSResponse_Data> RowData { get; set; }
    }

    public partial class LSResponse_Data
    {
        public string Premiumad { get; set; }
        public string Adid { get; set; }
        public string Title { get; set; }
        public string Titlerep { get; set; }
        public string Phoneno { get; set; }
        public string Profileimgurl { get; set; }
        public string Statecode { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Streetname { get; set; }
        public string Ratingavg { get; set; }
        public Uri Clickurl { get; set; }
        public string Primarytag { get; set; }
        public string Email { get; set; }
        public string Adstatus { get; set; }
        public string Oldclpostid { get; set; }
        public string Typadid { get; set; }
        public string Typphoneno { get; set; }
        public string Typtext { get; set; }
        public string Reviewcount { get; set; }
        public string Leafservice { get; set; }
        public string Ptag { get; set; }
    }
}


