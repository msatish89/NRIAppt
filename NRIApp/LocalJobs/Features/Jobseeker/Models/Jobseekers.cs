using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Jobseeker.Models
{
   
    public class Experience_Params
    {
        public string months { get; set; }
        public string monthname { get; set; }
        public string years { get; set; }
        public string checkimage { get; set; }
    }
    public class Jobseekers_DATA
    {
        public int contentid { get; set; }
        public int jobresumesid { get; set; }
        public string contactperson { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string photos { get; set; }
        public string phone { get; set; }
        public string alternatephone { get; set; }
        public string timeto { get; set; }
        public string timeon { get; set; }
        public string workauthorization { get; set; }
        public string profiledesc { get; set; }
        public string education { get; set; }
        public int usmasterdegree { get; set; }
        public double salaryfrom { get; set; }
        public double salaryto { get; set; }
        public int salarymodeid { get; set; }
        public string salarymode { get; set; }
        public int bestindustry { get; set; }
        public int experience { get; set; }
        public string resumetitle { get; set; }
        public string functionalarea { get; set; }
        public string employementtype { get; set; }
        public string jobstatus { get; set; }
        public string jobrole { get; set; }
        public string jobcity { get; set; }
        public string jobcityurl { get; set; }
        public string jobalerts { get; set; }
        public string linkedin { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string blog { get; set; }
        public string pid { get; set; }
        public DateTime crdate { get; set; }
        public string lastupdated { get; set; }
        public string email { get; set; }
        public string hideprofile { get; set; }
        public int levelcomplete { get; set; }
        public string resumepath { get; set; }
        public string resumemobileno { get; set; }
        public string fillcount { get; set; }
        public object pagetitle { get; set; }
        public object keywords { get; set; }
        public object pagedesc { get; set; }
        public object heading { get; set; }
        public object activepathpattern { get; set; }
        public object ogobjtype { get; set; }
        public object ogobjgroup { get; set; }
        public object ogobjpagetype { get; set; }
        public string pageusersegment { get; set; }
        public string pageuserdem { get; set; }
        public object pageshortdesc { get; set; }
        public int mobileverified { get; set; }
        public int isitskill { get; set; }

        public string countrycode { get; set; }
        public string block { get; set; }

        //skillsscreen
        public string resumeskills { get; set; }
        public string hdnreviewrating { get; set; }
        public string skillexperience { get; set; }
        //skill remove//
        public string skillcontentid { get; set; }
        public string jobprofileid { get; set; }
        public string editskills { get; set; }
        public string skillyearsofexperience { get; set; }
        public string skillrating { get; set; }
        
        //experience-company
        public string expcontentid { get; set; }
        public string txtcmyname { get; set; }
        public string txtrole { get; set; }
        public string frmmonth { get; set; }
        public string frmmonthnametxt { get; set; }
        public string frmyear { get; set; }
        public string tomonth { get; set; }
        public string tomonthname { get; set; }
        public string toyear { get; set; }
        public string txtareadescrip { get; set; }

    }

    public class Jobseekers
    {
        public List<Jobseekers_DATA> ROW_DATA { get; set; }
    }

    public class Sendprofiledata
    {
        public string name { get; set; }
        public string jobrole { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string cityurl { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string workauthorization { get; set; }
        public string countrycode { get; set; }
        public string block { get; set; }

        public string profiledescription { get; set; }
    }
    public class Resultinformation
    {
        public string result { get; set; }
        public string contentid { get; set; }
        public string jobresumesid { get; set; }
        public string fieldcount { get; set; }
        public string resultinformation { get; set; }
        public string resumepath { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string availiableresumes { get; set; }
        public string resulinformation { get; set; }
    }
    public class Jobseeker_Update
    {
        public List<Jobseeker_Update_Data> ROW_DATA { get; set; }
    }

    public class Jobseeker_Update_Data
    {
        public string resulinformation { get; set; }
        public int contentid { get; set; }
        public int jobresumesid { get; set; }
        public string fillcount { get; set; }
    }
    public class SKILLDATA
    {
        public string contentid { get; set; }
        public string jobprofileid { get; set; }
        public string skills { get; set; }
        public bool yoevisible { get; set; }
        public bool empskillsvisble { get; set; }
        public string yearsofexperience { get; set; }
        public string rating { get; set; }
        public DateTime crdate { get; set; }

        //viewprofile rating 
        public string rating1 { get; set; }
        public string rating2 { get; set; }
        public string rating3 { get; set; }
        public string rating4 { get; set; }
        public string rating5 { get; set; }
        public string rating6 { get; set; }
        public string rating7 { get; set; }
        public string rating8 { get; set; }
        public string rating9 { get; set; }
        public string rating10 { get; set; }
    }

    public class SKILLS
    {
        public List<SKILLDATA> ROW_DATA { get; set; }
    }

    public class EXPERIENCEDATA
    {
        public int rowid { get; set; }
        public int contentid { get; set; }
        public int jobprofileid { get; set; }
        public string jobcompany { get; set; }
        public string designation { get; set; }
        public int frmmonth { get; set; }
        public string frmmonthname { get; set; }
        public string frmmonthnametxt { get; set; }
        public int frmyear { get; set; }
        public int tomonth { get; set; }
        public string tomonthname { get; set; }
        public int toyear { get; set; }
        public string description { get; set; }
        public DateTime crdate { get; set; }
        public string fromyeartext { get; set; }
    }

    public class EXPERIENCE
    {
        public List<EXPERIENCEDATA> ROW_DATA { get; set; }
    }

    public class CParams
    {
        //salary range
        public string perdata { get; set; }
        public string perdataid { get; set; }
        public int yearsid { get; set; }
        public string checkimage { get; set; }
        //experience 
        public string years { get; set; }
    }
}
