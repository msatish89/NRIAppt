using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using NRIApp.LocalJobs.Features.Listing.Models;
using System.Runtime.CompilerServices;
using Refit;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Detail.Interfaces;
using Acr.UserDialogs;
using Plugin.Share;
using Plugin.MapsPlugin;
using NRIApp.LocalJobs.Features.Detail.Models;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Detail.ViewModels
{
    public class JobDetailVM : INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        List<ReportFlag> listofreports = new List<ReportFlag>();
        List<ptags> skill = new List<ptags>();
        public List<JobsSaveAdList> AdSaveList { get; set; }
        public List<JobsDeleteAdList> AdDeleteList { get; set; }
        public List<Reportflagresult_DATA> ReportAd { get; set; }
        public Command Selectreportflag { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command TaptoShare { get; set; }
        public Command TapOnMap { get; set; }
        public Command Tapontransportation { get; set; }
        public Command Taponstreetview { get; set; }
        public Command TapOnAdsave { get; set; }
        public Command TapOnReportFlag { get; set; }
        public Command postresponsecmd { get; set; }
        public Command Viewnumbercmd { get; set; }
        public Command descviewmore { get; set; }
        public Command descviewless { get; set; }
        public Command TapOnCompany { get; set; }
        public Command gotofulldesc { get; set; }
        private bool _descviewmorevisible=false;
        public bool descviewmorevisible
        {
            get { return _descviewmorevisible; }
            set { _descviewmorevisible = value; OnPropertyChanged(nameof(descviewmorevisible)); }
        }
        private bool _descvielessvisible=false;
        public bool descvielessvisible
        {
            get { return _descvielessvisible; }
            set { _descvielessvisible = value; OnPropertyChanged(nameof(descvielessvisible)); }
        }
        
            
        private string _contactname;
        public string contactname
        {
            get { return _contactname; }
            set { _contactname = value; OnPropertyChanged(nameof(contactname)); }
        }
        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(title)); }
        }
        private string _titleurl;
        public string titleurl
        {
            get { return _titleurl; }
            set { _titleurl = value; OnPropertyChanged(nameof(titleurl)); }
        }
        private string _cityurl;
        public string cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(nameof(cityurl)); }
        }
        private string _contentid;
        public string contentid
        {
            get { return _contentid; }
            set { _contentid = value; OnPropertyChanged(nameof(contentid)); }
        }
        private string _pagetitle;
        public string pagetitle
        {
            get { return _pagetitle; }
            set { _pagetitle = value; OnPropertyChanged(nameof(pagetitle)); }
        }
        private string _shortdesc;
        public string shortdesc
        {
            get { return _shortdesc; }
            set { _shortdesc = value; OnPropertyChanged(nameof(shortdesc)); }
        }
        private string _Fulldesc;
        public string Fulldesc
        {
            get { return _Fulldesc; }
            set { _Fulldesc = value; OnPropertyChanged(nameof(Fulldesc)); }
        }
        private string _description;
        public string description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(description)); }
        }
        private string _contributor;
        public string contributor
        {
            get { return _contributor; }
            set { _contributor = value; OnPropertyChanged(nameof(contributor)); }
        }
        private string _streetname;
        public string streetname
        {
            get { return _streetname; }
            set { _streetname = value; OnPropertyChanged(nameof(streetname)); }
        }
        private double _Adlatitude;
        public double Adlatitude
        {
            get { return _Adlatitude; }
            set { _Adlatitude = value; OnPropertyChanged(nameof(Adlatitude)); }
        }
        private double _Adlongitude;
        public double Adlongitude
        {
            get { return _Adlongitude; }
            set { _Adlongitude = value; OnPropertyChanged(nameof(Adlongitude)); }
        }
        private string _city;
        public string city
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(city)); }
        }
        private string _noofopening;
        public string noofopening
        {
            get { return _noofopening; }
            set { _noofopening = value; OnPropertyChanged(nameof(noofopening)); }
        }
        private string _qualification;
        public string qualification
        {
            get { return _qualification; }
            set { _qualification = value; OnPropertyChanged(nameof(qualification)); }
        }
        private string _experience;
        public string experience
        {
            get { return _experience; }
            set { _experience = value; OnPropertyChanged(nameof(experience)); }
        }
        private string _employementtype;
        public string employementtype
        {
            get { return _employementtype; }
            set { _employementtype = value; OnPropertyChanged(nameof(employementtype)); }
        }
        private string _workauthorization;
        public string workauthorization
        {
            get { return _workauthorization; }
            set { _workauthorization = value; OnPropertyChanged(nameof(workauthorization)); }
        }
        private string _salary;
        public string salary
        {
            get { return _salary; }
            set { _salary = value; OnPropertyChanged(nameof(salary)); }
        }
        private string _salarymode;
        public string salarymode
        {
            get { return _salarymode; }
            set { _salarymode = value; OnPropertyChanged(nameof(salarymode)); }
        }
        private string _jobrole;
        public string jobrole
        {
            get { return _jobrole; }
            set { _jobrole = value; OnPropertyChanged(nameof(jobrole)); }
        }
        private string _companyname;
        public string companyname
        {
            get { return _companyname; }
            set { _companyname = value; OnPropertyChanged(nameof(companyname)); }
        }
        private string _industry;
        public string industry
        {
            get { return _industry; }
            set { _industry = value; OnPropertyChanged(nameof(industry)); }
        }
        private bool _industryvisible;
        public bool industryvisible
        {
            get { return _industryvisible; }
            set { _industryvisible = value; OnPropertyChanged(nameof(industryvisible)); }
        }
        
        private bool _experiencevisible;
        public bool experiencevisible
        {
            get { return _experiencevisible; }
            set { _experiencevisible = value;OnPropertyChanged(nameof(experiencevisible)); }
        }
        private bool _salaryvisible;
        public bool salaryvisible
        {
            get { return _salaryvisible; }
            set { _salaryvisible = value; OnPropertyChanged(nameof(salaryvisible)); }
        }
        private bool _addressvisible;
        public bool addressvisible
        {
            get { return _addressvisible; }
            set { _addressvisible = value; OnPropertyChanged(nameof(addressvisible)); }
        }
        private bool _noofopeningsvisible;
        public bool noofopeningsvisible
        {
            get { return _noofopeningsvisible; }
            set { _noofopeningsvisible = value; OnPropertyChanged(nameof(noofopeningsvisible)); }
        }
        private bool _companynamevisible;
        public bool companynamevisible
        {
            get { return _companynamevisible; }
            set { _companynamevisible = value; OnPropertyChanged(nameof(companynamevisible)); }
        }
        private string _jobtype;
        public string jobtype
        {
            get { return _jobtype; }
            set { _jobtype = value; OnPropertyChanged(nameof(jobtype)); }
        }
       
        private string _functionalarea;
        public string functionalarea
        {
            get { return _functionalarea; }
            set { _functionalarea = value; OnPropertyChanged(nameof(functionalarea)); }
        }
        private string _gender;
        public string gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(nameof(gender)); }
        }
        private string _reportflagtxt = "";
        public string reportflagtxt
        {
            get { return _reportflagtxt; }
            set { _reportflagtxt = value; OnPropertyChanged(nameof(reportflagtxt)); }
        }
        private int _reportflagid;
        public int reportflagid
        {
            get { return _reportflagid; }
            set { _reportflagid = value; OnPropertyChanged(nameof(reportflagid)); }
        }
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value;OnPropertyChanged(nameof(contentviewvisible)); }
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
        private string _primarytag = "";
        public string primarytag
        {
            get { return _primarytag; }
            set { _primarytag = value; OnPropertyChanged(nameof(primarytag)); }
        }
        private List<ptags> _Getskills;
        public List<ptags> Getskills
        {
            get { return _Getskills; }
            set { _Getskills = value; OnPropertyChanged(nameof(Getskills)); }
        }
        private bool _viewnumbervisible = false;
        public bool viewnumbervisible
        {
            get { return _viewnumbervisible; }
            set { _viewnumbervisible = value; OnPropertyChanged(nameof(viewnumbervisible)); }
        }
        private string _postedago = "";
        public string postedago
        {
            get { return _postedago; }
            set { _postedago = value; OnPropertyChanged(nameof(postedago)); }
        }
        private int _premiumad ;
        public int premiumad
        {
            get { return _premiumad; }
            set { _premiumad = value; OnPropertyChanged(nameof(premiumad)); }
        }
        private int _resumemandatory;
        public int resumemandatory
        {
            get { return _resumemandatory; }
            set { _resumemandatory = value; OnPropertyChanged(nameof(resumemandatory)); }
        }
        private string _businesstitleurl = "";
        public string businesstitleurl
        {
            get { return _businesstitleurl; }
            set { _businesstitleurl = value; OnPropertyChanged(nameof(businesstitleurl)); }
        }
        private int _businessID;
        public int businessID
        {
            get { return _businessID; }
            set { _businessID = value; OnPropertyChanged(nameof(businessID)); }
        }
        private bool _workauthourizationvisible = false;
        public bool workauthourizationvisible
        {
            get { return _workauthourizationvisible; }
            set { _workauthourizationvisible = value; OnPropertyChanged(nameof(workauthourizationvisible)); }
        }
        private bool _employementvisible = false;
        public bool employementvisible
        {
            get { return _employementvisible; }
            set { _employementvisible = value; OnPropertyChanged(nameof(employementvisible)); }
        }
        private bool _qualificationvisible = false;
        public bool qualificationvisible
        {
            get { return _qualificationvisible; }
            set { _qualificationvisible = value; OnPropertyChanged(nameof(qualificationvisible)); }
        }
        private bool _htmldescvisible = false;
        public bool htmldescvisible
        {
            get { return _htmldescvisible; }
            set { _htmldescvisible = value; OnPropertyChanged(nameof(htmldescvisible)); }
        }
        public string userpid = Commonsettings.UserPid;
        LocalJobResponse pstresponse = new LocalJobResponse();
        public JobDetailVM(string adid,string titleurl,string userpid)
        {
          try
            {
                // pstresponse = listdata;
               
                GetJobDetails(adid, titleurl, userpid);
                TaptoShare = new Command(TapOnShare);
                TapOnMap = new Command(TapOnMapView);
                Taponstreetview = new Command(TapOnStreetView);
                Tapontransportation = new Command(TapOnTranportation);
                TapOnReportFlag = new Command<ReportFlag>(showreportflaglist);
                TapOnAdsave = new Command(TapOnAdtoSave);
                Selectreportflag = new Command<ReportFlag>(SelectreportflagID);
                TapOnCompany = new Command(gotocompanyprofile);
                gotofulldesc = new Command(Taponfulldecs);
                //postresponsecmd = new Command(postresponse);
                postresponsecmd = new Command(async () => await postresponse(adid));
                Viewnumbercmd = new Command(async () => await viewnumber());
                PopupContentTap = new Command(showcontentview);
                ContentViewTap = new Command(Closecontentview);
                descviewmore = new Command(descviewmoreTap);
                descviewless = new Command(descviewlessTap);
                listofreports.Add(new ReportFlag { reportlist = "Spam / Duplicate", reportid = 1 });
                listofreports.Add(new ReportFlag { reportlist = "Prohibited / Fraud", reportid = 2 });
                listofreports.Add(new ReportFlag { reportlist = "Miscategorized", reportid = 3 });
            }
            catch(Exception ex)
            {

            }
        }
        private string _folderid = "";
        public string folderid
        {
            get { return _folderid; }
            set { _folderid = value;OnPropertyChanged(nameof(folderid)); }
        }
        private string _folderadid = "";
        public string folderadid
        {
            get { return _folderadid; }
            set { _folderadid = value; OnPropertyChanged(nameof(folderadid)); }
        }
        public async void GetJobDetails(string adid, string titleurl, string userpid)
        {
            try
            {
                dialogs.ShowLoading("");
                NRIApp.LocalJobs.Features.Detail.Views.Responsetq.dtlpagecode = 1;
                if (string.IsNullOrEmpty(userpid))
                {
                    userpid = "";
                }
                var singledataAPI = RestService.For<IDetaildata>(Commonsettings.LocaljobsAPI);
                var response = await singledataAPI.Getsingledatadetail(adid, titleurl, userpid);
               
                contactname = response.ROW_DATA.Last().contactname;
                //streetname = response.ROW_DATA.Last().streetname;
                streetname = response.ROW_DATA.Last().statename + "," + response.ROW_DATA.Last().statecode;
                Adlatitude = response.ROW_DATA.Last().lat;
                Adlongitude = response.ROW_DATA.Last().@long;
                titleurl = response.ROW_DATA.Last().titleurl;
                cityurl = response.ROW_DATA.Last().cityurl;
                contentid = response.ROW_DATA.Last().contentid;
                postedago = response.ROW_DATA.Last().crdate1.ToString("MMM dd, yyyy"); 
                //orderbydate is updated date
               
                premiumad = response.ROW_DATA.Last().premiumad;
                resumemandatory = response.ROW_DATA.Last().resumemandatory;
                businessID= response.ROW_DATA.Last().businessid;
                businesstitleurl = response.ROW_DATA.Last().businesstitleurl;
                if (response.ROW_DATA.Last().hideaddress == 1)
                {
                    addressvisible = false;
                }
                else
                {
                    addressvisible = true;
                }
                if (response.ROW_DATA.Last().isadsaved == 0)
                {
                    AdSaveimg = "HeartGray.png";
                    adresult = "0";
                }
                if (response.ROW_DATA.Last().isadsaved == 1)
                {
                    AdSaveimg = "HeartGrayActive.png";
                    adresult = "1";
                }
           
                title = response.ROW_DATA.Last().heading;
                jobrole = response.ROW_DATA.Last().jobrole;

                if(!string.IsNullOrEmpty(response.ROW_DATA.Last().industry))
                {
                    industryvisible = true;
                    industry = response.ROW_DATA.Last().industry;
                }
                
                if ((string.IsNullOrEmpty(response.ROW_DATA.Last().experiencefrom) || response.ROW_DATA.Last().experiencefrom == "0") && (string.IsNullOrEmpty(response.ROW_DATA.Last().experienceto) || response.ROW_DATA.Last().experienceto == "0"))
                {
                    if (!string.IsNullOrEmpty(response.ROW_DATA.Last().experience))
                    {
                        experiencevisible = true;
                        experience = response.ROW_DATA.Last().experience;
                    }
                    else
                    {
                        experiencevisible = false;
                    }
                }
                else
                {
                    experience = response.ROW_DATA.Last().experiencefrom + " - " + response.ROW_DATA.Last().experienceto + " Years";
                    experiencevisible = true;
                }
               //if(!string.IsNullOrEmpty(response.ROW_DATA.Last().minsal))
               // {
               //     double minsal
                    //if (Math.Round(response.ROW_DATA.Last().minsal, 1) == 0 && Math.Round(response.ROW_DATA.Last().maxsal, 1) == 0)
                    if((string.IsNullOrEmpty(response.ROW_DATA.Last().minsal) || response.ROW_DATA.Last().minsal=="0")&& (string.IsNullOrEmpty(response.ROW_DATA.Last().maxsal) || response.ROW_DATA.Last().maxsal == "0"))
                    {
                        salary = "Best in Industry";
                        salarymode = "";
                    }
                    else
                    {
                        double minsalry = double.Parse(response.ROW_DATA.Last().minsal);
                        double maxsalry = double.Parse(response.ROW_DATA.Last().maxsal);
                        minsalry = Math.Round(minsalry, 1);
                        maxsalry = Math.Round(maxsalry, 1);
                        //salary = "$" + response.ROW_DATA.Last().minsal + " - " + "$" + response.ROW_DATA.Last().maxsal;
                        //salarymode = " /" + response.ROW_DATA.Last().salarymode;
                        //salarymode = " /" + response.ROW_DATA.Last().salarymode;
                        if (!string.IsNullOrEmpty(response.ROW_DATA.Last().salarymode))
                        {
                            salary = " $" + minsalry + " - $" + maxsalry;
                            salarymode = " /" + response.ROW_DATA.Last().salarymode;
                        }
                        else
                        {
                            salary = " $" + minsalry + " - $" + maxsalry;
                            salarymode = "";
                        }
                    //}
                }
               
               
                if (string.IsNullOrEmpty(response.ROW_DATA.Last().businessname))
                {
                    companynamevisible = false;
                }
                else
                {
                    companynamevisible = true;
                    companyname = response.ROW_DATA.Last().businessname;
                }
                if (!string.IsNullOrEmpty(response.ROW_DATA.Last().workauthorization))
                {
                    workauthourizationvisible = true;
                    workauthorization = response.ROW_DATA.Last().workauthorization;
                }
                else
                {
                    workauthourizationvisible = false;
                    workauthorization = response.ROW_DATA.Last().workauthorization;
                }
                if (!string.IsNullOrEmpty(response.ROW_DATA.Last().employmenttype))
                {
                    employementvisible = true;
                    employementtype = response.ROW_DATA.Last().employmenttype;
                }
                else
                {
                    employementvisible = false;
                    employementtype = response.ROW_DATA.Last().employmenttype;
                }
                if (string.IsNullOrEmpty(response.ROW_DATA.Last().qualification) || response.ROW_DATA.Last().qualification=="-")
                {
                    qualificationvisible = false;
                    qualification = response.ROW_DATA.Last().qualification;
                }
                else
                {
                    qualificationvisible = true;
                    qualification = response.ROW_DATA.Last().qualification;
                }
                // if (!string.IsNullOrEmpty(response.ROW_DATA.Last().employmenttype))
                //workauthorization = response.ROW_DATA.Last().workauthorization;
                //employementtype = response.ROW_DATA.Last().employmenttype;
                //qualification = response.ROW_DATA.Last().qualification;
                functionalarea = response.ROW_DATA.Last().functionalarea;
                noofopening = response.ROW_DATA.Last().noofopening;
                if (response.ROW_DATA.Last().gender == "1")
                {
                    gender = "Male";
                }
                else if (response.ROW_DATA.Last().gender == "2")
                {
                    gender = "Female";
                }
                else if (string.IsNullOrEmpty(response.ROW_DATA.Last().gender) || response.ROW_DATA.Last().gender == "3")
                {
                    gender = "Any";
                }
                
                pagetitle = response.ROW_DATA.Last().jobrole + " Job Description";
                shortdesc = response.ROW_DATA.Last().shortdesc.Replace("&nbsp;", " ").Replace("&bull", "\n").Replace("&rsquo;", "'").Replace("&amp;", "and").Replace("/n", "\n").Replace("&ndash;"," - ");
                Fulldesc= response.ROW_DATA.Last().shortdesc.Replace("&nbsp;", " ").Replace("&bull", "\n").Replace("&rsquo;", "'").Replace("&amp;", "and").Replace("/n", "\n").Replace("&ndash;", " - ");
                if (shortdesc.Length>250)
                {
                    double desclength = shortdesc.Length;
                    double reducelength = Math.Round((desclength / 2), 1);
                    shortdesc = shortdesc.Remove(Convert.ToInt32(reducelength));
                    descviewmorevisible = true;
                }
                
                jobtype = response.ROW_DATA.Last().primarycategoryvalue;
                if (string.IsNullOrEmpty(response.ROW_DATA.Last().noofopening) || response.ROW_DATA.Last().noofopening == "0")
                {
                    noofopeningsvisible = false;
                }
                else
                {
                    noofopeningsvisible = true;
                }
                if(!string.IsNullOrEmpty(response.ROW_DATA.Last().primarytags))
                {
                    string ptaglist = response.ROW_DATA.Last().primarytags;
                    string[] skillist = ptaglist.Split(',');
                    foreach (var item in skillist)
                    {
                        skill.Add(new ptags { skills = item });
                    }
                    Getskills = skill;
                }
                if (response.ROW_DATA.Last().showvn == 1)
                {
                    viewnumbervisible = true;
                }
                else
                {
                    viewnumbervisible = false;
                }
                if(!string.IsNullOrEmpty(response.ROW_DATA.Last().htmlurl) && response.ROW_DATA.Last().premiumad ==1 && response.ROW_DATA.Last().campaignid != "0" && response.ROW_DATA.Last().adstatus != 11 && !string.IsNullOrEmpty(response.ROW_DATA.Last().folderid))
                {
                    htmldescvisible = true;
                    folderid = response.ROW_DATA.Last().folderid;
                    folderadid = response.ROW_DATA.Last().adid.ToString();
                }
                else
                {
                    htmldescvisible = false;
                }
                if (!string.IsNullOrEmpty(response.ROW_DATA.Last().orderbydate))
                {
                    postedago = response.ROW_DATA.Last().orderbydate;
                }
                dialogs.HideLoading();
            }
            catch(Exception e)
            {
                dialogs.HideLoading();
                string msg = e.Message;
            }
        }
        public async void Taponfulldecs()
        {
            try
            {
                var currntpg = GetCurrentPage();
               // await currntpg.Navigation.PushAsync(new Views.Fulldescription(htmldescription));
                 await currntpg.Navigation.PushAsync(new Views.Fulldescription(folderid,folderadid));
            }
            catch(Exception ex)
            {
            }
        }
        Search dtldata = new Search();
        public async void gotocompanyprofile()
        {
            try
            {
                dtldata.titleurl = businesstitleurl;
                dtldata.contentid = businessID;
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushAsync(new Views.CompanyProfile(dtldata));
            }
            catch(Exception ex)
            {

            }
        }
        public void descviewmoreTap()
        {
            shortdesc = Fulldesc;
            descviewmorevisible = false;
            descvielessvisible = true;
        }
        public void descviewlessTap()
        {
            double desclength = shortdesc.Length;
            double reducelength = Math.Round((desclength / 2), 1);
            shortdesc = Fulldesc.Remove(Convert.ToInt32(reducelength));
            descviewmorevisible = true;
            descvielessvisible = false;
        }
        public void TapOnShare()
        {
            //string text = "Hi, I found this Job Ad in Sulekha.com and would like to share it with you.Have a look at it and provide me your feedback.";
         try
            {
                string url = "http://localjobs.sulekha.com/" + titleurl + "_" + cityurl + "_" + contentid;
                CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
                {
                    Url = url
                });
            }
            catch(Exception ex)
            {

            }
        }
        public async void TapOnMapView()
        {
            await CrossMapsPlugin.Current.PinTo(streetname, Adlatitude, Adlongitude, 8);
        }
        public void TapOnStreetView()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("http://maps.google.com/maps?q=&layer=c&cbll=" + Adlatitude + "," + Adlongitude + "&cbp=1,90,0,0,1.0&mz=20"));
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                Device.OpenUri(new Uri("google.streetview:cbll=" + Adlatitude + "," + Adlongitude + "&cbp=1,90,,0,1.0&mz=20"));
            }
        }
        public void TapOnTranportation()
        {
            string business = "Transportation";
            if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("http://maps.google.com/?q=" + business + "near" + streetname));
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                Device.OpenUri(new Uri("http://maps.apple.com/?q=" + business + streetname.Replace(" ", "&nbsp")));
            }
        }
        public void showreportflaglist(ReportFlag posting_Param)
        {
            try
            {
                var curentpage = GetCurrentPage();
                var reportflag = curentpage.FindByName<ListView>("reportflag");
                foreach (var amt in listofreports)
                {
                    if (reportflagid == amt.reportid || amt.reportlist == "Ad was Marked.Thanks")
                    {
                        amt.checkimage = "";
                        reportflagtxt = amt.reportlist = "Ad was Marked.Thanks";
                    }
                    else
                    {
                        amt.checkimage = "RadioButtonUnChecked.png";
                    }
                }
                reportflag.ItemsSource = null;
                reportflag.ItemsSource = listofreports;
                contentviewvisible = true;
            }
            catch(Exception ex)
            {
            }
        }
        public async void SelectreportflagID(ReportFlag posting_Param)
        {
           try
            {
                dialogs.ShowLoading("", null);
                var report = RestService.For<IDetaildata>(Commonsettings.LocaljobsAPI);
                string email = Commonsettings.UserEmail;
                reportflagtxt = posting_Param.reportlist;
                reportflagid = posting_Param.reportid;
                string adurl = "http://localjobs.sulekha.com/" + titleurl + "_" + cityurl + "_" + contentid;
                //var reportsubmit = await report.GetReport(contentid, userpid, jobrole, "localjobsl.sulekha.com", reportflagid.ToString(), adurl, "6", email);
                var reportsubmit = await report.GetReport(contentid, "43528159", jobrole, "localjobsl.sulekha.com", reportflagid.ToString(), adurl, "6", "testaug@gmail.com");
                string res = reportsubmit.resultinfo;
                contentviewvisible = false;
                dialogs.HideLoading();
            }
            catch(Exception exc)
            {
                dialogs.HideLoading();
                string msg = exc.Message;
            }
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
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
                        var savead = RestService.For<IDetaildata>(Commonsettings.LocaljobsAPI);
                        JobsSaveAD adsaveresponse = await savead.SaveAdData(contentid, email, userpid);
                        AdSaveList = adsaveresponse.ROW_DATA;
                        adresult = "1";
                    }
                    else
                    {
                        AdSaveimg = "HeartGray.png";
                        var deletead = RestService.For<IDetaildata>(Commonsettings.LocaljobsAPI);
                        JobsDeleteAD addeleteresponse = await deletead.DeleteAdData(contentid, userpid);
                        AdDeleteList = addeleteresponse.ROW_DATA;
                        adresult = "0";
                    }
                }
                dialogs.HideLoading();
                if(adresult =="1")
                {
                    dialogs.Toast("Ad saved successfully");
                }
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public async Task postresponse(string adid)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new Views.Apply_Form(adid,premiumad.ToString(),resumemandatory.ToString(),""));
        }
        public async Task viewnumber()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new LocalJobs.Features.Detail.Views.View_Number());
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
