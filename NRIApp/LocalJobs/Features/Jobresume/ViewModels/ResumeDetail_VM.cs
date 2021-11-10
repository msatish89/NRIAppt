using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using Refit;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobresume.Interfaces;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Jobresume.ViewModels
{
    public class ResumeDetail_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public Command TapOnAdsave { get; set; }
        public Command twittertap { get; set; }
        public Command facebooktap { get; set; }
        public Command websitetap { get; set; }
        public Command linkedintap { get; set; }
        public Command Taptodownload { get; set; }
        public Command Tapcontactjobseeker { get; set; }

        public Command previewcmd { get; set; }
        public static int isdownloadresumeprofilecnt = 0;
        public ResumeDetail_VM(string profileid,string emailid)
        {
            ProfileID = profileid;
            EmailID = emailid;
            getprofiledetails(profileid,emailid);
            TapOnAdsave = new Command(TapOnAdtoSave);
            twittertap = new Command(gototwitterlink);
            facebooktap = new Command(gotofacebooklink);
            websitetap = new Command(gotowebsitelink);
            linkedintap = new Command(gotolinkedinlink);
            Taptodownload = new Command(downloadresume);
            Tapcontactjobseeker = new Command(contactjobseeker);
            previewcmd = new Command(previewresume);
        }
        private string _ProfileID;
        public string ProfileID
        {
            get { return _ProfileID; }
            set { _ProfileID = value; OnPropertyChanged(nameof(ProfileID)); }
        }
        private string _EmailID;
        public string EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; OnPropertyChanged(nameof(EmailID)); }
        }
        private string _contactperson;
        public string contactperson
        {
            get { return _contactperson; }
            set { _contactperson = value;OnPropertyChanged(nameof(contactperson)); }
        }
        private string _jobrole;
        public string jobrole
        {
            get { return _jobrole; }
            set { _jobrole = value; OnPropertyChanged(nameof(jobrole)); }
        }
        private string _postedago;
        public string postedago
        {
            get { return _postedago; }
            set { _postedago = value; OnPropertyChanged(nameof(postedago)); }
        }
        private string _city;
        public string city
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(city)); }
        }
        private string _adsaveimg = "Favorite.png";
        public string AdSaveimg
        {
            get { return _adsaveimg; }
            set { _adsaveimg = value; OnPropertyChanged(nameof(AdSaveimg)); }
        }
        private string _adresult = "";
        public string adresult
        {
            get { return _adresult; }
            set { _adresult = value; OnPropertyChanged(nameof(adresult)); }
        }
        private string _resumetitle = "";
        public string resumetitle
        {
            get { return _resumetitle; }
            set { _resumetitle = value; OnPropertyChanged(nameof(resumetitle)); }
        }
        private string _premiumad = "";
        public string premiumad
        {
            get { return _premiumad; }
            set { _premiumad = value; OnPropertyChanged(nameof(premiumad)); }
        }
        private int _adid ;
        public int adid
        {
            get { return _adid; }
            set { _adid = value; OnPropertyChanged(nameof(adid)); }
        }
        private string _education = "";
        public string education
        {
            get { return _education; }
            set { _education = value; OnPropertyChanged(nameof(education)); }
        }
        private bool _educationvisible=false;
        public bool educationvisible
        {
            get { return _educationvisible; }
            set { _educationvisible = value; OnPropertyChanged(nameof(educationvisible)); }
        }
        private string _functionalarea = "";
        public string functionalarea
        {
            get { return _functionalarea; }
            set { _functionalarea = value; OnPropertyChanged(nameof(functionalarea)); }
        }
        private bool _functionalareavisible = false;
        public bool functionalareavisible
        {
            get { return _functionalareavisible; }
            set { _functionalareavisible = value; OnPropertyChanged(nameof(functionalareavisible)); }
        }
        private string _experience = "";
        public string experience
        {
            get { return _experience; }
            set { _experience = value; OnPropertyChanged(nameof(experience)); }
        }
        private bool _experiencevisible = false;
        public bool experiencevisible
        {
            get { return _experiencevisible; }
            set { _experiencevisible = value; OnPropertyChanged(nameof(experiencevisible)); }
        }
        private string _photos = "ProfileUserIcon.png";
        public string photos
        {
            get { return _photos; }
            set { _photos = value; OnPropertyChanged(nameof(photos)); }
        }
        private List<string> _skilllist;
        public List<string> skilllist
        {
            get { return _skilllist; }
            set { _skilllist = value; OnPropertyChanged(nameof(skilllist)); }
        }
        private bool _skillsvisible = false;
        public bool skillsvisible
        {
            get { return _skillsvisible; }
            set { _skillsvisible = value; OnPropertyChanged(nameof(skillsvisible)); }
        }
        private string _profiledesc = "";
        public string profiledesc
        {
            get { return _profiledesc; }
            set { _profiledesc = value; OnPropertyChanged(nameof(profiledesc)); }
        }
        private string _usmasterdegree = "";
        public string usmasterdegree
        {
            get { return _usmasterdegree; }
            set { _usmasterdegree = value; OnPropertyChanged(nameof(usmasterdegree)); }
        }
        private bool _employementvisible = false;
        public bool employementvisible
        {
            get { return _employementvisible; }
            set { _employementvisible = value; OnPropertyChanged(nameof(employementvisible)); }
        }
        private List<string> _emptypelist;
        public List<string> emptypelist
        {
            get { return _emptypelist; }
            set { _emptypelist = value; OnPropertyChanged(nameof(emptypelist)); }
        }
        private string _employementtype = "";
        public string employementtype
        {
            get { return _employementtype; }
            set { _employementtype = value; OnPropertyChanged(nameof(employementtype)); }
        }
        private string _jobstatus = "";
        public string jobstatus
        {
            get { return _jobstatus; }
            set { _jobstatus = value; OnPropertyChanged(nameof(jobstatus)); }
        }
        private List<Jobseeker.Models.SKILLDATA> _fullskilllist;
        public List<Jobseeker.Models.SKILLDATA> fullskilllist
        {
            get { return _fullskilllist; }
            set { _fullskilllist = value; OnPropertyChanged(nameof(fullskilllist)); }
        }
        private List<Jobseeker.Models.EXPERIENCEDATA> _experiencelist;
        public List<Jobseeker.Models.EXPERIENCEDATA> experiencelist
        {
            get { return _experiencelist; }
            set { _experiencelist = value; OnPropertyChanged(nameof(experiencelist)); }
        }
        private bool _skillslistviewvisible = false;
        public bool skillslistviewvisible
        {
            get { return _skillslistviewvisible; }
            set { _skillslistviewvisible = value; OnPropertyChanged(nameof(skillslistviewvisible)); }
        }
        private bool _experiencelistviewvisible = false;
        public bool experiencelistviewvisible
        {
            get { return _experiencelistviewvisible; }
            set { _experiencelistviewvisible = value; OnPropertyChanged(nameof(experiencelistviewvisible)); }
        }
        private string _fblink = "";
        public string fblink
        {
            get { return _fblink; }
            set { _fblink = value; OnPropertyChanged(nameof(fblink)); }
        }
        private string _bloglink = "";
        public string bloglink
        {
            get { return _bloglink; }
            set { _bloglink = value; OnPropertyChanged(nameof(bloglink)); }
        }
        private string _linkedinlink = "";
        public string linkedinlink
        {
            get { return _linkedinlink; }
            set { _linkedinlink = value; OnPropertyChanged(nameof(linkedinlink)); }
        }
        private string _twitterlink = "";
        public string twitterlink
        {
            get { return _twitterlink; }
            set { _twitterlink = value; OnPropertyChanged(nameof(twitterlink)); }
        }
        private bool _facebookvisible = false;
        public bool facebookvisible
        {
            get { return _facebookvisible; }
            set { _facebookvisible = value; OnPropertyChanged(nameof(facebookvisible)); }
        }
        private bool _twittervisible = false;
        public bool twittervisible
        {
            get { return _twittervisible; }
            set { _twittervisible = value; OnPropertyChanged(nameof(twittervisible)); }
        }
        private bool _blogvisible = false;
        public bool blogvisible
        {
            get { return _blogvisible; }
            set { _blogvisible = value; OnPropertyChanged(nameof(blogvisible)); }
        }
        private bool _linkedinvisible = false;
        public bool linkedinvisible
        {
            get { return _linkedinvisible; }
            set { _linkedinvisible = value; OnPropertyChanged(nameof(linkedinvisible)); }
        }
        private bool _jobstatusvisible = false;
        public bool jobstatusvisible
        {
            get { return _jobstatusvisible; }
            set { _jobstatusvisible = value; OnPropertyChanged(nameof(jobstatusvisible)); }
        }
        private string _resumeid = "";
        public string resumeid
        {
            get { return _resumeid; }
            set { _resumeid = value; OnPropertyChanged(nameof(resumeid)); }
        }
        private bool _socialnetworkvisible = true;
        public bool socialnetworkvisible
        {
            get { return _socialnetworkvisible; }
            set { _socialnetworkvisible = value; OnPropertyChanged(nameof(socialnetworkvisible)); }
        }
        private bool _downloadvisible = true;
        public bool downloadvisible
        {
            get { return _downloadvisible; }
            set { _downloadvisible = value; OnPropertyChanged(nameof(downloadvisible)); }
        }
        
        List<string> Countryitem = new List<string>() {
            "+1","+91"
        };
        public async void getprofiledetails(string profileid,string emailid)
        {
            try
            {
                isdownloadresumeprofilecnt = 0;
                dialogs.ShowLoading("", null);
                var singledataAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                var response = await singledataAPI.Getsingledatadetail(Commonsettings.UserEmail, profileid,Commonsettings.UserPid);
                if(response!=null)
                {
                    resumetitle = response.RowData.Last().Resumetitle;
                    if(!string.IsNullOrEmpty(response.RowData.Last().Premiumflag))
                    {
                        premiumad = response.RowData.Last().Premiumflag;
                    }
                    resumeid = response.RowData.Last().Jobresumesid;
                    adid = response.RowData.Last().Jobprofileid;
                    contactperson = response.RowData.Last().Contactperson;
                    jobrole = response.RowData.Last().Jobrole;
                    postedago = "Updated " + response.RowData.Last().Postedago;
                    city = response.RowData.Last().City;
                    if (response.RowData.Last().Issavedresume == 0)
                    {
                        AdSaveimg = "HeartGray.png";
                        adresult = "0";
                    }
                    if (response.RowData.Last().Issavedresume == 1)
                    {
                        AdSaveimg = "HeartGrayActive.png";
                        adresult = "1";
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Isdownloadedresume) && response.RowData.Last().Isdownloadedresume == "1")
                    {
                        downloadvisible = false;
                    }
                    else
                    {
                        downloadvisible = true;
                    }
                    if(!string.IsNullOrEmpty(response.RowData.Last().Education))
                    {
                        educationvisible = true;
                        education = response.RowData.Last().Education;
                    }
                    if(!string.IsNullOrEmpty(response.RowData.Last().Functionalarea))
                    {
                        functionalareavisible = true;
                        functionalarea = response.RowData.Last().Functionalarea;
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Experience))
                    {
                        experiencevisible = true;
                        if(response.RowData.Last().Experience=="1")
                        {
                            experience = response.RowData.Last().Experience + " Year";
                        }
                        else if(response.RowData.Last().Experience=="0")
                        {
                            experience = " Fresher";
                        }
                        else
                        {
                            experience = response.RowData.Last().Experience + " Years";
                        }
                    }
                    if(!string.IsNullOrEmpty(response.RowData.Last().Photos))
                    {
                        photos = response.RowData.Last().Photos;
                    }

                    if (!string.IsNullOrEmpty(response.RowData.Last().Skill))
                    {
                        skillsvisible = true;
                        skilllist = new List<string>();
                        string[] skillsarr = response.RowData.Last().Skill.Split(',');
                        skilllist = skillsarr.ToList();
                        int height = skillsarr.Count() * 20;
                       // getskillsheightrequest(height);
                    }

                    profiledesc = response.RowData.Last().Profiledesc;
                    if(!string.IsNullOrEmpty(response.RowData.Last().Usmasterdegree))
                    {
                        if(response.RowData.Last().Usmasterdegree=="1")
                        {
                            usmasterdegree = "Yes";
                        }
                        else
                        {
                            usmasterdegree = "No";
                        }
                    }
                    else
                    {
                        usmasterdegree = "No";
                    }

                    if (!string.IsNullOrEmpty(response.RowData.Last().Employementtype))
                    {
                            employementvisible = true;
                            emptypelist = new List<string>();
                            string[] additionalarr = response.RowData.Last().Employementtype.Split(',');
                            foreach (var additionalgrpdta in additionalarr)
                            {
                               if (!string.IsNullOrEmpty(additionalgrpdta)) 
                                {
                                  string agegroupdata = additionalgrpdta.Substring(0, additionalgrpdta.LastIndexOf("::") + 0);
                                //additionalservicelist.Add(additionalgrpdta);
                                emptypelist.Add(agegroupdata);
                                }
                            }
                        //emptypelist = additionalarr.ToList();
                        //employementtype = emptypelist;
                        employementtype = String.Join(",", emptypelist);
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Jobstatus))
                    {
                        if (response.RowData.Last().Jobstatus == "available-now")
                        {
                            jobstatusvisible = true;
                            jobstatus = "Available to join now!";
                        }
                    }
                }
                if(!string.IsNullOrEmpty(response.RowData.Last().Facebook))
                {
                    facebookvisible = true;
                    fblink = response.RowData.Last().Facebook;
                }
                if (!string.IsNullOrEmpty(response.RowData.Last().Blog))
                {
                    blogvisible = true;
                    bloglink = response.RowData.Last().Blog;
                }
                if (!string.IsNullOrEmpty(response.RowData.Last().Twitter))
                {
                    twittervisible = true;
                    twitterlink = response.RowData.Last().Twitter;
                }
                if (!string.IsNullOrEmpty(response.RowData.Last().Linkedin))
                {
                    linkedinvisible = true;
                    linkedinlink = response.RowData.Last().Linkedin;
                }
                if(string.IsNullOrEmpty(response.RowData.Last().Linkedin)&& string.IsNullOrEmpty(response.RowData.Last().Twitter)&& string.IsNullOrEmpty(response.RowData.Last().Blog)&& string.IsNullOrEmpty(response.RowData.Last().Twitter))
                {
                    socialnetworkvisible = false;
                }
                getskillsdata();
                getexperiencedata();
                await Task.Delay(300);
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public async void getskillsdata()
        {
            try
            {
                    var skillapi = RestService.For<Jobseeker.Interface.IJobseeker>(Commonsettings.LocaljobsAPI);
                    var skilldata = await skillapi.fillskillfields(adid);
                    if (skilldata != null && skilldata.ROW_DATA.Count > 0)
                    {
                        skillslistviewvisible = true;
                        foreach (var data in skilldata.ROW_DATA)
                        {

                        if (string.IsNullOrEmpty(data.yearsofexperience))
                        {
                            data.yoevisible = false;
                        }
                        else
                        {
                            data.yoevisible = true;
                        }
                        if (string.IsNullOrEmpty(data.skills))
                        {
                            data.empskillsvisble = false;
                        }
                        else
                        {
                            data.empskillsvisble = true;
                        }
                         data.rating = data.rating + "/10".ToString();
                         data.yearsofexperience = data.yearsofexperience + " Years";

                        if(data.rating=="1")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColorGray2";
                            data.rating3 = "RatingColorGray3";
                            data.rating4 = "RatingColorGray4";
                            data.rating5 = "RatingColorGray5";
                            data.rating6 = "RatingColorGray6";
                            data.rating7 = "RatingColorGray7";
                            data.rating8 = "RatingColorGray8";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "1";
                        }
                        else if (data.rating == "2")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColorGray3";
                            data.rating4 = "RatingColorGray4";
                            data.rating5 = "RatingColorGray5";
                            data.rating6 = "RatingColorGray6";
                            data.rating7 = "RatingColorGray7";
                            data.rating8 = "RatingColorGray8";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "2";
                        }
                        else if (data.rating == "3")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColorGray4";
                            data.rating5 = "RatingColorGray5";
                            data.rating6 = "RatingColorGray6";
                            data.rating7 = "RatingColorGray7";
                            data.rating8 = "RatingColorGray8";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "3";
                        }
                        else if (data.rating == "4")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColor4.png";
                            data.rating5 = "RatingColorGray5";
                            data.rating6 = "RatingColorGray6";
                            data.rating7 = "RatingColorGray7";
                            data.rating8 = "RatingColorGray8";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "4";
                        }
                        else if (data.rating == "5")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColor4.png";
                            data.rating5 = "RatingColor5.png";
                            data.rating6 = "RatingColorGray6";
                            data.rating7 = "RatingColorGray7";
                            data.rating8 = "RatingColorGray8";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "5";
                        }
                        else if (data.rating == "6")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColor4.png";
                            data.rating5 = "RatingColor5.png";
                            data.rating6 = "RatingColor6.png";
                            data.rating7 = "RatingColorGray7";
                            data.rating8 = "RatingColorGray8";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "6";
                        }
                        else if (data.rating == "7")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColor4.png";
                            data.rating5 = "RatingColor5.png";
                            data.rating6 = "RatingColor6.png";
                            data.rating7 = "RatingColor7.png";
                            data.rating8 = "RatingColorGray8";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "7";
                        }
                        else if (data.rating == "8")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColor4.png";
                            data.rating5 = "RatingColor5.png";
                            data.rating6 = "RatingColor6.png";
                            data.rating7 = "RatingColor7.png";
                            data.rating8 = "RatingColor8.png";
                            data.rating9 = "RatingColorGray9";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "8";
                        }
                        else if (data.rating == "9")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColor4.png";
                            data.rating5 = "RatingColor5.png";
                            data.rating6 = "RatingColor6.png";
                            data.rating7 = "RatingColor7.png";
                            data.rating8 = "RatingColor8.png";
                            data.rating9 = "RatingColor9.png";
                            data.rating10 = "RatingColorGray10";
                            data.rating = "9";
                        }
                        else if (data.rating == "10")
                        {
                            data.rating1 = "RatingColor1.png";
                            data.rating2 = "RatingColor2.png";
                            data.rating3 = "RatingColor3.png";
                            data.rating4 = "RatingColor4.png";
                            data.rating5 = "RatingColor5.png";
                            data.rating6 = "RatingColor6.png";
                            data.rating7 = "RatingColor7.png";
                            data.rating8 = "RatingColor8.png";
                            data.rating9 = "RatingColor9.png";
                            data.rating10 = "RatingColor10.png";
                            data.rating = "10";
                        }
                        else
                        {
                            data.rating1 = "RatingColorGray1.png";
                            data.rating2 = "RatingColorGray2.png";
                            data.rating3 = "RatingColorGray3.png";
                            data.rating4 = "RatingColorGray4.png";
                            data.rating5 = "RatingColorGray5.png";
                            data.rating6 = "RatingColorGray6.png";
                            data.rating7 = "RatingColorGray7.png";
                            data.rating8 = "RatingColorGray8.png";
                            data.rating9 = "RatingColorGray9.png";
                            data.rating10 = "RatingColorGray10.png";
                            data.rating = "0";
                        }
                    }
                        fullskilllist = skilldata.ROW_DATA;
                    }
                
            }
            catch (Exception ex)
            {

            }
        }
        public async void getexperiencedata()
        {
            try
            {
                    var experienceapi = RestService.For<Jobseeker.Interface.IJobseeker>(Commonsettings.LocaljobsAPI);
                    var experiencedata = await experienceapi.fillexperiencefield(adid);
                    if (experiencedata != null && experiencedata.ROW_DATA.Count > 0)
                    {
                        //wat abt tilldate 
                        foreach (var data in experiencedata.ROW_DATA)
                        {
                            data.frmmonthnametxt = data.frmmonthname;
                            data.fromyeartext = data.frmmonthname + " " + data.frmyear + " to " + data.tomonthname +" "+ data.toyear;
                            data.frmmonthname = data.frmmonthname + " " + data.frmyear + " to " + data.tomonthname + " " + data.toyear;
                        }
                        experiencelist = experiencedata.ROW_DATA;
                    }
            }
            catch (Exception ex)
            {
            }
        }
        public async void getskillsheightrequest(int height)
        {
            try
            {
                await Task.Delay(500);
                var currentpage = GetCurrentPage();
                HVScrollGridView skillslistview = currentpage.FindByName<HVScrollGridView>("skillslistview");
                skillslistview.HeightRequest = height;
                if (skillslistview.HeightRequest > 100)
                {
                    skillslistview.HeightRequest = height + 10;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async void TapOnAdtoSave()
        {
            try
            {
                dialogs.ShowLoading("", null);
                string email = Commonsettings.UserEmail;
                var currentpage = GetCurrentPage();
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    if (adresult == "0")
                    {
                        AdSaveimg = "HeartGrayActive.png";
                        var savead = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                        Features.Detail.Models.resultinfo adsaveresponse = await savead.savedad(resumeid.ToString(), Commonsettings.UserPid);
                        string res = adsaveresponse.result;
                        adresult = "1";
                    }
                    else
                    {
                        AdSaveimg = "HeartGray.png";
                        var deletead = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                        Features.Detail.Models.resultinfo addeleteresponse = await deletead.deletesavedad(resumeid.ToString(), Commonsettings.UserPid);
                        string res = addeleteresponse.result;
                        adresult = "0";
                    }
                }
                dialogs.HideLoading();
                if (adresult == "1")
                {
                    dialogs.Toast("Ad saved successfully");
                }
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public void gototwitterlink()
        {
            try
            {
                Device.OpenUri(new Uri(twitterlink));
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                dialogs.Toast("Invalid url...");
            }

        }
        public void gotofacebooklink()
        {
            try
            {
                Device.OpenUri(new Uri(fblink));
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                dialogs.Toast("Invalid url...");
            }

        }
        public void gotowebsitelink()
        {
            try
            {
                Device.OpenUri(new Uri(bloglink));
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                dialogs.Toast("Invalid url...");
            }

        }
        public void gotolinkedinlink()
        {
            try
            {
                Device.OpenUri(new Uri(linkedinlink));
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                dialogs.Toast("Invalid url...");
            }
        }
        public static int logincode = 0;
        public async void downloadresume()
        {
            try
            {
                string ipaddress = DependencyService.Get<Techjobs.Features.LeadForm.Interfaces.IIPAddressManager>().GetIPAddress();
                //https://techjobs.sulekha.com/Mobileapp/localjobs
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                    logincode = 1;
                }
                else
                {
                    dialogs.ShowLoading("", null);
                    var resumedownloadapi = RestService.For<IJobresume>("https://localjobs.sulekha.com/mobileappresume");
                    var downloaddata = await resumedownloadapi.downloadresumeurl(Commonsettings.UserPid, Commonsettings.UserMobileno, Commonsettings.UserEmail,resumeid, ipaddress);
                    dialogs.HideLoading();
                   // downloaddata.result = "expired";
                    if (downloaddata.result == "sucess" && !string.IsNullOrEmpty(downloaddata.resumepath))
                    {
                        // Device.OpenUri(new Uri(downloaddata.resumepath));
                        var alert = await Application.Current.MainPage.DisplayAlert("Available Resume Count  " + downloaddata.availiableresumes, "", "Ok", " ");
                        Device.OpenUri(new Uri(downloaddata.resumepath));
                        // downloadvisible = false;
                       // await Task.Delay(300);
                        // getprofiledetails(ProfileID, EmailID);
                        isdownloadresumeprofilecnt = 1;
                    }
                    else if (downloaddata.result == "daylimit")
                    {
                        dialogs.Toast("Your daylimit is over...");
                    }
                    else if (downloaddata.result == "expired")
                    {
                        var alert = await Application.Current.MainPage.DisplayAlert("To download the resume, Kindly subscribe to our ", "Resume Package Services.", "Yes", "Back");
                        if (alert)
                        {
                            var currentpage = GetCurrentPage();
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
                        }
                    }
                    else if (downloaddata.result == "subscribe")
                    {
                        var alert = await Application.Current.MainPage.DisplayAlert("Your ad is expired to download the resume, Kindly subscribe to our ", "Resume Package Services.", "Yes", "Back");
                        if (alert)
                        {
                            var currentpage = GetCurrentPage();
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void previewresume()
        {
            try
            {
                //http://localjobs.sulekha.com/classifiedshtmls/toolresumes/9786010259.html
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushAsync(new LocalJobs.Features.Jobresume.Views.Previewresume(resumeid));
            }
            catch (Exception ex)
            {

            }
        }

        public async void contactjobseeker()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new LocalJobs.Features.Jobresume.Views.Contactjobseeker(adid.ToString(), premiumad));
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
