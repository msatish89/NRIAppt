using NRIApp.LocalJobs.Features.Jobseeker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Acr.UserDialogs;
using Refit;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Posting.Models;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;

namespace NRIApp.LocalJobs.Features.Jobseeker.ViewModels
{
    public class ProfessionalDetailsVM : INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public Command prebachelorCommand { get; set; }
        public Command bachelorCommand { get; set; }
        public Command masterCommand { get; set; }
        public Command doctorateCommand { get; set; }
        public Command RyesCommand { get; set; }
        public Command RnoCommand { get; set; }
        public Command employedcommand { get; set; }
        public Command noticeperiodCommand { get; set; }
        public Command joinnowCommand { get; set; }
        public Command Taponexperience { get; set; }
        public Command Taponsalaryrange { get; set; }
        public Command Taponfunctionalarea { get; set; }
        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        public Command savedetails { get; set; }
        public Command canceldtl { get; set; }

        public Command<CParams> selectperdatarange { get; set; }
        public Command<CParams> selectexperience { get; set; }
        public Command<Functionalarea> selectfunctionalarea { get; set; }

        public List<CParams> perrangelistdata { get; set; }
        public List<CParams> experienceyrslistdata { get; set; }
        public List<Functionalarea> Functionalareaslist { get; set; }
        public List<Filterdata> getemployementtypelist { get; set; }
        public List<Filterdata> getemployementtypelist1 { get; set; }
        public List<CParams> perrangelist = new List<CParams>()
        {
            new CParams{perdata="Per Hour",perdataid ="182,Hourly", checkimage="RadioButtonUnChecked.png"},
            new CParams{perdata="Per Day",perdataid="183,Daily", checkimage="RadioButtonUnChecked.png"},
            new CParams{perdata="Per Week",perdataid="184,Weekly", checkimage="RadioButtonUnChecked.png"},
            new CParams{perdata="Per Month",perdataid="185,Monthly", checkimage="RadioButtonUnChecked.png"},
            new CParams{perdata="Per Year",perdataid="192,Yearly", checkimage="RadioButtonUnChecked.png"},
        };
        public List<CParams> experienceyrslist = new List<CParams>()
        {
            new CParams{years="Freshers",yearsid=0,  checkimage="RadioButtonUnChecked.png"},
            new CParams{years="1",yearsid=1, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="2",yearsid=2, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="3",yearsid=3, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="4",yearsid=4, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="5",yearsid=5, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="6",yearsid=6, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="7",yearsid=7, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="8",yearsid=8, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="9",yearsid=9, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="10",yearsid=10, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="11",yearsid=11, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="12",yearsid=12, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="13",yearsid=13, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="14",yearsid=14, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="15",yearsid=15, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="16",yearsid=16, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="17",yearsid=17, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="18",yearsid=18, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="19",yearsid=19, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="20",yearsid=20, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="21",yearsid=21, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="22",yearsid=22, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="23",yearsid=23, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="24",yearsid=24, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="25",yearsid=25, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="26",yearsid=26, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="27",yearsid=27, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="28",yearsid=28, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="29",yearsid=29, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="30",yearsid=30, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="31",yearsid=31, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="32",yearsid=32, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="33",yearsid=33, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="34",yearsid=34, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="35",yearsid=35, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="36",yearsid=36, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="37",yearsid=37, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="38",yearsid=38, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="39",yearsid=39, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="40",yearsid=40, checkimage="RadioButtonUnChecked.png"},
            //new CParams{years="40+",yearsid=40, checkimage="RadioButtonUnChecked.png"},
        };
        private string _YesImg = "";
        public string YesImg
        {
            get { return _YesImg; }
            set { _YesImg = value; OnPropertyChanged(nameof(YesImg)); }
        }
        private string _NoImg = "";
        public string NoImg
        {
            get { return _NoImg; }
            set { _NoImg = value; OnPropertyChanged(nameof(NoImg)); }
        }
        private string _prebachelorimg = "";
        public string prebachelorimg
        {
            get { return _prebachelorimg; }
            set { _prebachelorimg = value; OnPropertyChanged(nameof(prebachelorimg)); }
        }
        private string _bachelorimg = "";
        public string bachelorimg
        {
            get { return _bachelorimg; }
            set { _bachelorimg = value; OnPropertyChanged(nameof(bachelorimg)); }
        }
        private string _masterimg = "";
        public string masterimg
        {
            get { return _masterimg; }
            set { _masterimg = value; OnPropertyChanged(nameof(masterimg)); }
        }
        private string _doctorateimg = "";
        public string doctorateimg
        {
            get { return _doctorateimg; }
            set { _doctorateimg = value; OnPropertyChanged(nameof(doctorateimg)); }
        }
        private string _employedimg = "";
        public string employedimg
        {
            get { return _employedimg; }
            set { _employedimg = value; OnPropertyChanged(nameof(employedimg)); }
        }
        private string _noticeperiodimg = "";
        public string noticeperiodimg
        {
            get { return _noticeperiodimg; }
            set { _noticeperiodimg = value; OnPropertyChanged(nameof(noticeperiodimg)); }
        }
        private string _joinnowimg = "";
        public string joinnowimg
        {
            get { return _joinnowimg; }
            set { _joinnowimg = value; OnPropertyChanged(nameof(joinnowimg)); }
        }
        private int _isusmasterdegree;
        public int isusmasterdegree
        {
            get { return _isusmasterdegree; }
            set { _isusmasterdegree = value; OnPropertyChanged(nameof(isusmasterdegree)); }
        }
        private string _education = "";
        public string education
        {
            get { return _education; }
            set { _education = value; OnPropertyChanged(nameof(education)); }
        }
        private string _employementstatus = "";
        public string employementstatus
        {
            get { return _employementstatus; }
            set { _employementstatus = value; OnPropertyChanged(nameof(employementstatus)); }
        }
        private string _perdatatxt = "Select";
        public string perdatatxt
        {
            get { return _perdatatxt; }
            set { _perdatatxt = value; OnPropertyChanged(nameof(perdatatxt)); }
        }
        private string _perdataIDtxt ;
        public string perdataIDtxt
        {
            get { return _perdataIDtxt; }
            set { _perdataIDtxt = value; OnPropertyChanged(nameof(perdataIDtxt)); }
        }
        private string _functionalareatxt = "Select";
        public string functionalareatxt
        {
            get { return _functionalareatxt; }
            set { _functionalareatxt = value; OnPropertyChanged(nameof(functionalareatxt)); }
        }
        private string _functionalareatxtID;
        public string functionalareatxtID
        {
            get { return _functionalareatxtID; }
            set { _functionalareatxtID = value; OnPropertyChanged(nameof(functionalareatxtID)); }
        }
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _perdatalistviewvisble = false;
        public bool perdatalistviewvisble
        {
            get { return _perdatalistviewvisble; }
            set { _perdatalistviewvisble = value; OnPropertyChanged(nameof(perdatalistviewvisble)); }
        }
        private bool _experiencelistviewvisble = false;
        public bool experiencelistviewvisble
        {
            get { return _experiencelistviewvisble; }
            set { _experiencelistviewvisble = value; OnPropertyChanged(nameof(experiencelistviewvisble)); }
        }
        private bool _functionalarealistviewvisble = false;
        public bool functionalarealistviewvisble
        {
            get { return _functionalarealistviewvisble; }
            set { _functionalarealistviewvisble = value; OnPropertyChanged(nameof(functionalarealistviewvisble)); }
        }
        
        private string _experienceyrtxt = "Select";
        public string experienceyrtxt
        {
            get { return _experienceyrtxt; }
            set { _experienceyrtxt = value; OnPropertyChanged(nameof(experienceyrtxt)); }
        }
        private int _experienceyrtxtID ;
        public int experienceyrtxtID
        {
            get { return _experienceyrtxtID; }
            set { _experienceyrtxtID = value; OnPropertyChanged(nameof(experienceyrtxtID)); }
        }
        private int _functionalareaid;
        public int functionalareaid
        {
            get { return _functionalareaid; }
            set { _functionalareaid = value; OnPropertyChanged(nameof(functionalareaid)); }
        }
        private string _minsalary;
        public string minsalary
        {
            get { return _minsalary; }
            set { _minsalary = value; OnPropertyChanged(nameof(minsalary)); }
        }
        private string _maxsalary = "";
        public string maxsalary
        {
            get { return _maxsalary; }
            set { _maxsalary = value; OnPropertyChanged(nameof(maxsalary)); }
        }
        private string _checkedemployementtypevalue = "";
        public string checkedemployementtypevalue
        {
            get { return _checkedemployementtypevalue; }
            set { _checkedemployementtypevalue = value; OnPropertyChanged(nameof(checkedemployementtypevalue)); }
        }
        Jobseekers_DATA data = new Jobseekers_DATA();
        public ProfessionalDetailsVM(Jobseekers_DATA Sdata)
        {
            data = Sdata;
            if(Sdata.education== "pre-bachelor")
            {
                prebachelorimg = "RadioButtonChecked.png";
                bachelorimg = "RadioButtonUnChecked.png";
                masterimg = "RadioButtonUnChecked.png";
                doctorateimg = "RadioButtonUnChecked.png";
                education = "pre-bachelor";
            }
            else if(Sdata.education == "bachelor")
            {
                prebachelorimg = "RadioButtonUnChecked.png";
                bachelorimg = "RadioButtonChecked.png";
                masterimg = "RadioButtonUnChecked.png";
                doctorateimg = "RadioButtonUnChecked.png";
                education = "bachelor";
            }
            else if (Sdata.education == "master")
            {
                prebachelorimg = "RadioButtonUnChecked.png";
                bachelorimg = "RadioButtonUnChecked.png";
                masterimg = "RadioButtonChecked.png";
                doctorateimg = "RadioButtonUnChecked.png";
                education = "master";
            }
            else if (Sdata.education == "doctorate")
            {
                prebachelorimg = "RadioButtonUnChecked.png";
                bachelorimg = "RadioButtonUnChecked.png";
                masterimg = "RadioButtonUnChecked.png";
                doctorateimg = "RadioButtonChecked.png";
                education = "doctorate";
            }
            else
            {
                prebachelorimg = "RadioButtonChecked.png";
                bachelorimg = "RadioButtonUnChecked.png";
                masterimg = "RadioButtonUnChecked.png";
                doctorateimg = "RadioButtonUnChecked.png";
                education = "pre-bachelor";
            }
            
            if(Sdata.usmasterdegree ==1)
            {
                NoImg = "RadioButtonUnChecked.png";
                YesImg = "RadioButtonChecked.png";
                isusmasterdegree = 1;
            }
            else
            {
                YesImg = "RadioButtonUnChecked.png";
                NoImg = "RadioButtonChecked.png";
                isusmasterdegree = 0;
            }
            if(Sdata.jobstatus== "employed")
            {
                noticeperiodimg = "RadioButtonUnChecked.png";
                joinnowimg = "RadioButtonUnChecked.png";
                employedimg = "RadioButtonChecked.png";
                employementstatus = "employed";
            }
            else if (Sdata.jobstatus == "notice-period")
            {
                noticeperiodimg = "RadioButtonChecked.png";
                joinnowimg = "RadioButtonUnChecked.png";
                employedimg = "RadioButtonUnChecked.png";
                employementstatus = "notice-period";
            }
            else if (Sdata.jobstatus == "available-now")
            {
                noticeperiodimg = "RadioButtonUnChecked.png";
                joinnowimg = "RadioButtonChecked.png";
                employedimg = "RadioButtonUnChecked.png";
                employementstatus = "available-now";
            }
            else
            {
                noticeperiodimg = "RadioButtonUnChecked.png";
                joinnowimg = "RadioButtonUnChecked.png";
                employedimg = "RadioButtonChecked.png";
                employementstatus = "employed";
            }
            checkedemployementtypevalue = Sdata.employementtype;
            minsalary = Sdata.salaryfrom.ToString();
            maxsalary = Sdata.salaryto.ToString();
            
            if(Sdata.salarymode== "182,Hourly")
            {
                perdatatxt = "Per Hour";
                perdataIDtxt = "182,Hourly";
            }
            else if (Sdata.salarymode == "183,Daily")
            {
                perdatatxt = "Per Day";
                perdataIDtxt = "183,Daily";
            }
            else if (Sdata.salarymode == "184,Weekly")
            {
                perdatatxt = "Per Week";
                perdataIDtxt = "184,Weekly";
            }
            else if (Sdata.salarymode == "185,Monthly")
            {
                perdatatxt = "Per Month";
                perdataIDtxt = "185,Monthly";
            }
            else if (Sdata.salarymode == "192,Yearly")
            {
                perdatatxt = "Per Year";
                perdataIDtxt = "192,Yearly";
            }
            prebachelorCommand = new Command(prebachelorcmd);
            bachelorCommand = new Command(bachelorcmd);
            masterCommand = new Command(mastercmd);
            doctorateCommand = new Command(doctoratecmd);

            RyesCommand = new Command(Ryescmd);
            RnoCommand = new Command(Rnocmd);

            ContentViewTap = new Command(Closecontentview);
            PopupContentTap = new Command(showcontentview);

            employedcommand = new Command(employedcmd);
            noticeperiodCommand = new Command(noticeperiodcmd);
            joinnowCommand = new Command(joinnowcmd);

            Taponexperience = new Command(showexperiencelist);
            Taponsalaryrange = new Command(showrangelist);
            Taponfunctionalarea = new Command(showfunctionalarealist);

            selectperdatarange = new Command<CParams>(SelectperdatarangeID);
            selectfunctionalarea = new Command<Functionalarea>(SelectfunctionalareaID);
            selectexperience = new Command<CParams>(SelectexperienceyrsID);

            savedetails = new Command(saveprofessionaldetails);
            canceldtl = new Command(cancel);

            getemployementtype(Sdata.employementtype);

            if(Sdata.salarymode== "182,Hourly")
            {
                perdatatxt = "Per Hour";
            }
            if (Sdata.salarymode == "183,Daily")
            {
                perdatatxt = "Per Day";
            }
            if (Sdata.salarymode == "184,Weekly")
            {
                perdatatxt = "Per Week";
            }
            if (Sdata.salarymode == "185,Monthly")
            {
                perdatatxt = "Per Month";
            }
            if (Sdata.salarymode == "192,Yearly")
            {
                perdatatxt = "Per Year";
            }
            perdataIDtxt = Sdata.salarymode;
            if(Sdata.experience==0)
            {
                experienceyrtxt = "Freshers";
            }
            else
            {
                experienceyrtxt = Sdata.experience.ToString();
            }
            
            experienceyrtxtID = Sdata.experience;

            getfunctionalareadata(Sdata.functionalarea);
            functionalareatxtID = Sdata.functionalarea;


        }
        public async void getfunctionalareadata(string functionalarea)
        {
           try
            {
                dialogs.ShowLoading("", null);
                var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Functionalarealist list = await LJAPI.Getfunctionalarea(4);
                foreach (var dt in list.ROW_DATA)
                {
                    if (functionalarea == dt.categoryurl)
                    {
                        functionalareatxt = dt.categoryname;
                    }
                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {

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
        public void showrangelist()
        {
            var curentpage = GetCurrentpage();
            var perdatarange = curentpage.FindByName<ListView>("perdatarange");
            foreach (var data in perrangelist)
            {
                if (perdatatxt == data.perdata)
                {
                    data.checkimage = "RadioButtonChecked.png";
                    perdatatxt = data.perdata;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }
            perdatarange.ItemsSource = null;
            perdatarange.ItemsSource = perrangelist;
            contentviewvisible = true;
            perdatalistviewvisble = true;
            experiencelistviewvisble = false;
            functionalarealistviewvisble = false;
        }
        public void SelectperdatarangeID(CParams c_param)
        {
            var curntpg = GetCurrentpage();
            Label perdatatxtclr = curntpg.FindByName<Label>("perdatatxt");
            perdatatxtclr.TextColor = Color.FromHex("#212121");
            perdatatxt = c_param.perdata;
            perdataIDtxt = c_param.perdataid;
            contentviewvisible = false;
        }
        public void showexperiencelist()
        {
            var curentpage = GetCurrentpage();
            var experiencerange = curentpage.FindByName<ListView>("experiencerange");
            foreach (var data in experienceyrslist)
            {
                if (experienceyrtxt == data.years)
                {
                    data.checkimage = "RadioButtonChecked.png";
                    experienceyrtxt = data.years.ToString();
                    experienceyrtxtID = data.yearsid;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }
            experiencerange.ItemsSource = null;
            experiencerange.ItemsSource = experienceyrslist;
            contentviewvisible = true;
            perdatalistviewvisble = false;
            experiencelistviewvisble = true;
            functionalarealistviewvisble = false;
        }
        public void SelectexperienceyrsID(CParams c_param)
        {
            var curntpg = GetCurrentpage();
            Label experienceyrtxtclr = curntpg.FindByName<Label>("experienceyrtxt");
            experienceyrtxtclr.TextColor = Color.FromHex("#212121");
            experienceyrtxt = c_param.years;
            experienceyrtxtID = c_param.yearsid;
            contentviewvisible = false;
        }
        public async void showfunctionalarealist()
        {
            //pass functionalarea url 
           try
            {
                dialogs.ShowLoading("", null);
                var curentpage = GetCurrentpage();
                var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Functionalarealist list = await LJAPI.Getfunctionalarea(4);
                Functionalareaslist = list.ROW_DATA;
                var functionalarea = curentpage.FindByName<ListView>("functionalarea");
                foreach (var data in Functionalareaslist)
                {
                    if (functionalareatxt == data.categoryname)
                    {
                        data.image = "RadioButtonChecked.png";
                        functionalareatxt = data.categoryname;
                        functionalareatxtID = data.categoryurl;
                    }
                    else
                    {
                        data.image = "RadioButtonUnChecked.png";
                    }
                }
                functionalarea.ItemsSource = null;
                functionalarea.ItemsSource = Functionalareaslist;
                contentviewvisible = true;
                perdatalistviewvisble = false;
                experiencelistviewvisble = false;
                functionalarealistviewvisble = true;
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public void SelectfunctionalareaID(Functionalarea c_param)
        {
            var curntpg = GetCurrentpage();
            Label functionalareatxtclr = curntpg.FindByName<Label>("functionalareatxt");
            functionalareatxtclr.TextColor = Color.FromHex("#212121");
            functionalareatxt = c_param.categoryname;
            functionalareatxtID = c_param.categoryurl;
            contentviewvisible = false;
        }
        public void Ryescmd()
        {
            NoImg = "RadioButtonUnChecked.png";
            YesImg = "RadioButtonChecked.png";
            isusmasterdegree = 1;
        }
        public void Rnocmd()
        {
            YesImg = "RadioButtonUnChecked.png";
            NoImg = "RadioButtonChecked.png";
            isusmasterdegree = 0;
        }
        public void prebachelorcmd()
        {
            prebachelorimg = "RadioButtonChecked.png";
            bachelorimg = "RadioButtonUnChecked.png";
            masterimg = "RadioButtonUnChecked.png";
            doctorateimg = "RadioButtonUnChecked.png";
            education = "pre-bachelor";
        }
        public void bachelorcmd()
        {
            prebachelorimg = "RadioButtonUnChecked.png";
            bachelorimg = "RadioButtonChecked.png";
            masterimg = "RadioButtonUnChecked.png";
            doctorateimg = "RadioButtonUnChecked.png";
            education = "bachelor";
        }
        public void mastercmd()
        {
            prebachelorimg = "RadioButtonUnChecked.png";
            bachelorimg = "RadioButtonUnChecked.png";
            masterimg = "RadioButtonChecked.png";
            doctorateimg = "RadioButtonUnChecked.png";
            education = "master";
        }
        public void doctoratecmd()
        {
            prebachelorimg = "RadioButtonUnChecked.png";
            bachelorimg = "RadioButtonUnChecked.png";
            masterimg = "RadioButtonUnChecked.png";
            doctorateimg = "RadioButtonChecked.png";
            education = "doctorate";
        }

        public void employedcmd()
        {
            noticeperiodimg = "RadioButtonUnChecked.png";
            joinnowimg = "RadioButtonUnChecked.png";
            employedimg = "RadioButtonChecked.png";
            employementstatus = "employed";
        }
        public void noticeperiodcmd()
        {
            noticeperiodimg = "RadioButtonChecked.png";
            joinnowimg = "RadioButtonUnChecked.png";
            employedimg = "RadioButtonUnChecked.png";
            employementstatus = "notice-period";
        }
        public void joinnowcmd()
        {
            noticeperiodimg = "RadioButtonUnChecked.png";
            joinnowimg = "RadioButtonChecked.png";
            employedimg = "RadioButtonUnChecked.png";
            employementstatus = "available-now";
        }
        public async void getfunctionalareadata(int functid)
        {
            try
            {
                dialogs.ShowLoading("", null);
                var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Functionalarealist list = await LJAPI.Getfunctionalarea(4);
                foreach (var data in list.ROW_DATA)
                {
                    if (functid == data.secondarycategoryid)
                    {
                        data.image = "RadioButtonChecked.png";
                        functionalareaid = data.secondarycategoryid;
                    }
                    else
                    {
                        data.image = "RadioButtonUnChecked.png";
                    }
                }
                // Functionalareadata.ItemsSource = list.ROW_DATA;
                Functionalareaslist = list.ROW_DATA;
                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
      
        public async void getemployementtype(string employementtype)
        {
            try
            {
                dialogs.ShowLoading("", null);
                var LJAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                FilterListings list = await LJAPI.Getfilterdata("5");
                
                double lstcnt = list.ROW_DATA.Count;
                double halfcnt = Math.Round(lstcnt / 2, 1);
                getemployementtypelist = list.ROW_DATA.Take(Convert.ToInt32(halfcnt)).ToList();
                getemployementtypelist1 = list.ROW_DATA.Skip(Convert.ToInt32(halfcnt)).ToList();
                foreach (var data in getemployementtypelist)
                {
                    //BoxView boxx = new BoxView();
                    //boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    //boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;chbx.Color = Color.DarkGreen;chbx.BoxBackgroundColor = Color.LightGray;

                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    chbx.ClassId = data.tag;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_employementypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(employementtype))
                    {
                        string[] emptypelst = employementtype.Split(',');
                        foreach (var i in emptypelst)
                        {
                            if (data.tag == i)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    var currentpage = GetCurrentpage();
                    var Employmenttypelayout = currentpage.FindByName<StackLayout>("Employmenttypelayout");
                    Employmenttypelayout.Children.Add(chbx);
                  //  Employmenttypelayout.Children.Add(boxx);
                }
                foreach (var data in getemployementtypelist1)
                {
                    //BoxView boxx = new BoxView();
                    //boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    //boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    chbx.ClassId = data.tag;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_employementypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(employementtype))
                    {
                        string[] emptypelst = employementtype.Split(',');
                        foreach (var i in emptypelst)
                        {
                            if (data.tag == i)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    var currentpage = GetCurrentpage();
                    var Employmenttypelayout = currentpage.FindByName<StackLayout>("Employmenttypelayout1");
                    Employmenttypelayout.Children.Add(chbx);
                   // Employmenttypelayout.Children.Add(boxx);
                }
                    dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                string msg = ex.Message;
            }
           
        }
        private void Chb_employementypeCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked==true)
                {
                    if (checkedemployementtypevalue != null && checkedemployementtypevalue != "")
                    {
                        if (!checkedemployementtypevalue.Contains(category.ClassId))
                        {
                            checkedemployementtypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedemployementtypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedemployementtypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedemployementtypevalue = string.Join(",", indexvalue);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public bool validation()
        {
            bool result = true;
            if (string.IsNullOrEmpty(education))
            {
                dialogs.Toast("Please select education");
                return false;
            }
            if (string.IsNullOrEmpty(isusmasterdegree.ToString()))
            {
                dialogs.Toast("Select master degree");
                return false;
            }
            if (string.IsNullOrEmpty(minsalary))
            {
                dialogs.Toast("Please enter minimum salary");
                return false;
            }
            if (string.IsNullOrEmpty(maxsalary))
            {
                dialogs.Toast("Please enter maximum salary");
                return false;
            }
            if (maxsalary.Contains("."))
            {
                dialogs.Toast("maximum salary should be numeric");
                return false;
            }
            if (minsalary.Contains("."))
            {
                dialogs.Toast("minimum salary should be numeric");
                return false;
            }
            if (Convert.ToInt64(minsalary) > Convert.ToInt64(maxsalary))
            {
                dialogs.Toast("Maximum salary should be greater than minimum salary");
                return false;
            }
            if (string.IsNullOrEmpty(perdataIDtxt))
            {
                dialogs.Toast("Please select salary mode");
                return false;
            }
            if (string.IsNullOrEmpty(functionalareatxtID))
            {
                dialogs.Toast("Please select functional area");
                return false;
            }
            if (string.IsNullOrEmpty(employementstatus))
            {
                dialogs.Toast("Please select Job status");
                return false;
            }
            if (string.IsNullOrEmpty(checkedemployementtypevalue))
            {
                dialogs.Toast("Please select Employement type");
                return false;
            }

            return result;
        }
        public async void saveprofessionaldetails()
        {
            try
            {
                dialogs.ShowLoading("", null);
               if(validation())
                {
                    data.education = education;
                    data.usmasterdegree = isusmasterdegree;
                    //data.salaryfrom = Math.Round(minsalary);
                    data.salaryfrom = Convert.ToDouble(minsalary); 
                    data.salaryto = Convert.ToDouble(maxsalary);
                    data.salarymode = perdataIDtxt;
                    data.experience = experienceyrtxtID;
                    data.functionalarea = functionalareatxtID;
                    data.employementtype = checkedemployementtypevalue;
                    data.jobstatus = employementstatus;
                    data.block = "Professional";
                    var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var profiledatas = await profileupdata.profilepagecreate(data);
                    if (profiledatas != null)
                    {
                        var currentpage = GetCurrentpage();
                        //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                        Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                        currentpage.Navigation.RemovePage(page1);
                        currentpage.Navigation.RemovePage(page2);
                        await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                    }
                    else
                    {
                        dialogs.Toast("Try again later...");
                    }
                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {

            }
        }
        public void cancel()
        {
            var currentpage = GetCurrentpage();
            currentpage.Navigation.PopAsync();
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
