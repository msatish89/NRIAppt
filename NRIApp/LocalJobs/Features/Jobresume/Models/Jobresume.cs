using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalJobs.Features.Jobresume.Models
{
    class Jobresume
    {
    }
    public partial class LocalJobresume
    {
        [JsonProperty("ROW_DATA")]
        public List<LocalJobresumeResponse> ROW_DATA { get; set; }
    }

    public partial class LocalJobresumeResponse
    {
        public int Pageno { get; set; }
        public int Rowstofetch { get; set; }
        public int Rid { get; set; }
        public string Contentid { get; set; }
        public int Totalrecs { get; set; }
        public string Rowstofetch1 { get; set; }
        public string Adid { get; set; }
        public string Isapproved { get; set; }
        public string Emailid { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Mobileno { get; set; }
        public string Jobtitle { get; set; }
        public string Jobdesc { get; set; }
        public string Crdate { get; set; }
        public string Mobileverified { get; set; }
        public string Name { get; set; }
        public string Resumepath { get; set; }
        public DateTime Modifieddate { get; set; }
        public string Experience { get; set; }
        public string Rolename { get; set; }
        public string Education { get; set; }
        public string Companyname { get; set; }
        public string Industry { get; set; }
        public string Rolenameurl { get; set; }
        public string Companyurl { get; set; }
        public string Industryurl { get; set; }
        public string Skills { get; set; }
        public string Resumetype { get; set; }
        public string Postedago { get; set; }
        public string Statename { get; set; }
        public string Statecode { get; set; }
        public string Newcityurl { get; set; }
        public string Resumefilename { get; set; }
        public string Metrourl { get; set; }
        public string Isfeatured { get; set; }
        public string Isdownloadedresume { get; set; }
        public string Downloadedresumecount { get; set; }
        public string Jobprofileid { get; set; }
        public string Photo { get; set; }
        public string Issavedresume { get; set; }
        public string Htmlfilename { get; set; }
        public string Downloadedrname { get; set; }

        //visibility
        public bool experiencevisible { get; set; }
        public bool skillsvisible { get; set; }
        public bool industryvisible { get; set; }
        public bool educationvisible { get; set; }
        public bool profilevisible { get; set; }
        public bool downloadvisible { get; set; }
    }

    public partial class Jobrole_search
    {
        [JsonProperty("ROW_DATA")]
        public List<Jobrole_search_data> RowData { get; set; }
    }

    public partial class Jobrole_search_data
    {
        public string Contentid { get; set; }
        public string Tag { get; set; }
        public string Tagurl { get; set; }
        public DateTimeOffset Crdate { get; set; }
        public string Isapproved { get; set; }
        public string Categorytype { get; set; }
        public string header { get; set; }
    }

    public  class JobresumeDetail
    {
        [JsonProperty("ROW_DATA")]
        public List<JobresumeDetail_Data> RowData { get; set; }
    }

    public  class JobresumeDetail_Data
    {
        public long Previousjobresumesid { get; set; }
        public long Previousjobprofileid { get; set; }
        public string Previouscontactperson { get; set; }
        public string Previouscity { get; set; }
        public string Previousjobrole { get; set; }
        public string Previousskill { get; set; }
        public string Previouspostedago { get; set; }
        public int Jobprofileid { get; set; }
        public string Jobresumesid { get; set; }
        public string Photos { get; set; }
        public string Contactperson { get; set; }
        public string City { get; set; }
        public string Jobrole { get; set; }
        public string Skill { get; set; }
        public string Postedago { get; set; }
        public string City1 { get; set; }
        public string Resumemobileno { get; set; }
        public string Phone { get; set; }
        public string Alternatephone { get; set; }
        public long Jobseeker { get; set; }
        public string Email { get; set; }
        public string Resumetitle { get; set; }
        public string Education { get; set; }
        public string Usmasterdegree { get; set; }
        public string Experience { get; set; }
        public string Profiledesc { get; set; }
        public long Salaryfrom { get; set; }
        public long Salaryto { get; set; }
        public string Salarymode { get; set; }
        public string Functionalarea { get; set; }
        public string Employementtype { get; set; }
        public string Jobrole1 { get; set; }
        public string Jobstatus { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }
        public string Blog { get; set; }
        public string Twitter { get; set; }
        public string Workauthorization { get; set; }
        public string Isdownloadedresume { get; set; }
        public long Hideprofile { get; set; }
        public long Issavedresume { get; set; }
        public string Resumetype { get; set; }
        public object Resumefilename { get; set; }
        public string Premiumflag { get; set; }
        public string Htmlfilename { get; set; }
        public string Nextjobprofileid { get; set; }
        public string Nextjobresumesid { get; set; }
        public string Nextcontactperson { get; set; }
        public string Nextcity { get; set; }
        public string Nextjobrole { get; set; }
        public string Nextskill { get; set; }
        public string Nextpostedago { get; set; }
        public string Objecttype { get; set; }
        public string Pagetype { get; set; }
        public string Pagetitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Pagedesc { get; set; }
        public string Heading { get; set; }
        public object Activepathpattern { get; set; }
        public string Pageuserdem { get; set; }
    }
}
