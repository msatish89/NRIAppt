using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.Detail.Models
{
    public class CareDetail
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<CareSingleDetaildata> ROW_DATA { get; set; }
    }
    public class CareSingleDetaildata
    {
        //public string Businessid { get; set; }
        //public string Contactname { get; set; }
        //public string Virtualnumber { get; set; }
        //public string Businessname { get; set; }
        //public string Businesstitleurl { get; set; }
        //public string Profiletitle { get; set; }
        //public string Licenseno { get; set; }
        //public string Businessaddress { get; set; }
        //public string Zipcode { get; set; }
        //public string City { get; set; }
        //public string Country { get; set; }
        //public DateTime Crdate { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        //public string Experience { get; set; }
        //public string Description { get; set; }
        //public string Showvn { get; set; }
        //public double Salaryrate { get; set; }
        //public string Premiumad { get; set; }
        //public string Ordergroup { get; set; }
        //public string Ordertype { get; set; }
        //public string Needfrom { get; set; }
        //public string Needto { get; set; }
        //public string Neededdays { get; set; }
        //public string Salarymode { get; set; }
        //public string Salarymodeid { get; set; }
        //public string Gender { get; set; }
        //public string Servicename { get; set; }
        //public string Serviceurl { get; set; }
        //public string Availability { get; set; }
        //public string Ismobileverified { get; set; }
        //public string Careexperience { get; set; }
        //public string Transportfacility { get; set; }
        //public string Maincategoryid { get; set; }
        //public string Secondarycategoryid { get; set; }
        //public string Tertiarycategoryid { get; set; }
        //public string Primarycategoryid { get; set; }
        //public string Supercategoryvalue { get; set; }
        //public string Primarycategoryvalue { get; set; }
        //public string Secondarycategoryvalue { get; set; }
        //public string Tertiarycategoryvalue { get; set; }
        //public string Membersince { get; set; }
        //public string Lastreneweddate { get; set; }
        //public string Crdate1 { get; set; }
        //public string Photoid { get; set; }
        //public Uri Photourl { get; set; }
        //public Uri Smallphotourl { get; set; }
        //public string Videoid { get; set; }
        //public string Videourl { get; set; }
        //public string Externalvideourl { get; set; }
        //public string Tagline { get; set; }
        //public string Agegrouptags { get; set; }
        //public string Otherservicetags { get; set; }
        //public string Additionalserviceanswers { get; set; }
        //public string Certificationsanswers { get; set; }
        //public string Workpreferenceanswers { get; set; }
        //public string Ondemandanswers { get; set; }
        //public string Vehicleanswers { get; set; }
        //public string CarePreferenceanswers { get; set; }
        //public string Traininganswers { get; set; }
        //public string Languageanswers { get; set; }
        //public string Issaved { get; set; }




      
        
        public string Customerid { get; set; }
        public string Campaignid { get; set; }
        public string Contactname1 { get; set; }
        //public object Campaignstarts { get; set; }
        //public object Campaignend { get; set; }
        //public object Education { get; set; }
        //public object Backgroundcheck { get; set; }
        //public object Salarydescription { get; set; }
        //public object Perkdescription { get; set; }
        //public object Workingfrom { get; set; }
        //public object Workingto { get; set; }
        //public object Details { get; set; }
        //public object Preferredworktiming { get; set; }
        //public string Staffratio { get; set; }
        //public object Showvs { get; set; }
        //public object Milestotravel { get; set; }
        //public string Carespecialchild { get; set; }
        //public object Noofkids { get; set; }
        //public object Noofadults { get; set; }
        //public object Noofspecialneeds { get; set; }
        //public object Perkscost { get; set; }
        //public DateTimeOffset Startdate { get; set; }
        //public DateTimeOffset Enddate { get; set; }
        //public DateTimeOffset Verifieddate { get; set; }
        public string Status { get; set; }
        public string Premiumad1 { get; set; }
        public string Businessname1 { get; set; }
        public string Userpid { get; set; }
        public string Contributor { get; set; }
        public string Email1 { get; set; }
        public string Ordergroup1 { get; set; }
        public string Htmlurl { get; set; } 
        public string Tags { get; set; }
        public string Showvn1 { get; set; }
        //public string GeneralContentid { get; set; }
        //public string Generaltitlelower { get; set; }
        //public string GeneralTitle { get; set; }
        //public string GeneralTitleurl { get; set; }
        public string Folderid { get; set; }
        public string Cityurl { get; set; }
        public string Metrocityurl { get; set; }
        public string Statecode { get; set; }
        public string Statename { get; set; }
        public string Countryurl { get; set; }
        public object Citylat { get; set; }
        public object Citylong { get; set; }
        public string Supertag { get; set; }
        public string Supertagurl { get; set; }
        public string Primarytag { get; set; }
        public string Primarytagurl { get; set; }
        //public double Metrolat { get; set; }
        //public double Metrolong { get; set; }
        //public string Nma { get; set; }
        //public object Metrostateurl { get; set; }
        //public string Metrocountrycode { get; set; }
        //public string Metrostate { get; set; }
        //public string Metrocountry { get; set; }
        //public string Statecode1 { get; set; }
        public string Newcityurl { get; set; }
        public string Metrourl { get; set; }
        public string Metro { get; set; }
        public string Contentid { get; set; }
        //public string Objecttype { get; set; }
        public string Pagetype { get; set; }
        public string Pagetitle { get; set; }
        public string Activemenu { get; set; }
        //public string Locobjecttype { get; set; }
        public string Keywords { get; set; }
        public string Description1 { get; set; }
        public string Pagedesc { get; set; }
        public string Heading { get; set; }
        public string Activepathpattern { get; set; }
        public string Prevadid { get; set; }
        public string Prevtitle { get; set; }
        public string Prevnewtitleurl { get; set; }
        public string Prevcity { get; set; }
        public string Prevnewcityurl { get; set; }
        //public string Nextadid { get; set; }
        //public string Nexttitle { get; set; }
        //public string Nextnewtitleurl { get; set; }
        //public string Nextcity { get; set; }
        //public string Nextnewcityurl { get; set; }
        public string Agegroupid { get; set; }
        public string Agegroup { get; set; }
        public string Supertag1 { get; set; }
        public string Supertagid { get; set; }
        public string Primarytagid { get; set; }
        public string Primarytag1 { get; set; }
        public string Languageid { get; set; }
        public string Language { get; set; }
        //public object Trainingid { get; set; }
        //public object Training { get; set; }
        //public string Airportcode { get; set; }
        public string Noofpersons { get; set; }
        public string Neededtime { get; set; }
        public string Statename1 { get; set; }
        public string Metro1 { get; set; }
        public string Nooflivefreeads { get; set; }
        public string Freeadposted { get; set; }
        //public string Metrourl1 { get; set; }
        public string Radius { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        //public object Brochureurl { get; set; }
        //public object Britecode { get; set; }
        public string Age { get; set; }
        public string Website { get; set; }
        public string Numberofdays { get; set; }
        public string Responsecnt { get; set; }
        public object Ogobjtype { get; set; }
        public object Ogobjgroup { get; set; }
        public object Ogobjpagetype { get; set; }
        public Uri Ogimage { get; set; }
        //public string Pageusersegment { get; set; }
        //public string Pageuserdem { get; set; }
        //public string Tagid1 { get; set; }
        //public string Tagid2 { get; set; }
        //public string Tagid3 { get; set; }
        //public string Tagid4 { get; set; }
        public string Hideaddress { get; set; }
        //public object Enrollment { get; set; }
        public string Twitterfeed { get; set; }
        public string Facebookfeed { get; set; }
        public string Bookmarkstatus { get; set; }
        public string Recommendationcount { get; set; }
        public string Recommendationstatus { get; set; }
        public string XmlBranches { get; set; }
        public string Branchstatus { get; set; }
        public string Multicategory { get; set; }
        public string Virtualnanny { get; set; }
        public string Virtualnannyplatform { get; set; }


        
        public string Businessid { get; set; }
        public string Contactname { get; set; }
        public string Virtualnumber { get; set; }
        public string Businessname { get; set; }
        public string Businesstitleurl { get; set; }
        public string Profiletitle { get; set; }
        public string Licenseno { get; set; }
        public string Businessaddress { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTimeOffset Crdate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Experience { get; set; }
        public string additionalservice { get; set; }
        public string Description { get; set; }
        public string Showvn { get; set; }
        public double Salaryrate { get; set; }
        public long Premiumad { get; set; }
        public long Ordergroup { get; set; }
        public string Ordertype { get; set; }
        public string Needfrom { get; set; }
        public string Needto { get; set; }
        public string Neededdays { get; set; }
        public string Salarymode { get; set; }
        public long Salarymodeid { get; set; }
        public string Gender { get; set; }
        public string Lastreneweddate { get; set; }
        public string Servicename { get; set; }
        public string Serviceurl { get; set; }
        public string Availability { get; set; }
        public string Maincategoryid { get; set; }
        public string Photoid { get; set; }
        public string Photourl { get; set; }
        public string Smallphotourl { get; set; }
        public string Videoid { get; set; }
        public string Videourl { get; set; }
        public string Externalvideourl { get; set; }
        public string Supercategoryvalue { get; set; }
        public string Primarycategoryvalue { get; set; }
        public string Secondarycategoryvalue { get; set; }
        public string Tertiarycategoryvalue { get; set; }
        public string Membersince { get; set; }
        public string Ismobileverified { get; set; }
        public string Secondarycategoryid { get; set; }
        public string Tertiarycategoryid { get; set; }
        public string Primarycategoryid { get; set; }
        public string Issaved { get; set; }
        public string Careexperience { get; set; }
        public string Transportfacility { get; set; }
        public string Tagline { get; set; }
        public string certification { get; set; }
        public string servicetags { get; set; }
    }
    public class CareSaveAD
    {
        [JsonProperty("ROW_DATA")]
        public List<CareSaveAdList> ROW_DATA { get; set; }
    }
    public class CareSaveAdList
    {
        public int totalcnt { get; set; }
        public int myadcnt { get; set; }
        public string title { get; set; }
        public string shortdesc { get; set; }
        public string titleurl { get; set; }
        public string adid { get; set; }
        public string cityurl { get; set; }
    }
    public class CareDeleteAD
    {
        [JsonProperty("ROW_DATA")]
        public List<CareDeleteAdList> ROW_DATA { get; set; }
    }
    public class CareDeleteAdList
    {
        public int totalcnt { get; set; }
        public int myadcnt { get; set; }
        public int issavedad { get; set; }
    }
    public class CareReportFlag
    {
        public string reportlist { get; set; }
        public int reportid { get; set; }
        public string checkimage { get; set; }
    }
    public class CareReportflagresult_DATA
    {
        public string resultinfo { get; set; }
    }

}
