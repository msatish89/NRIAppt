using System;
using System.Collections.Generic;
using System.Text;
using NRIApp.Helpers;
using Refit;
using Acr.UserDialogs;
using System.ComponentModel;
using Xamarin.Forms;
using NRIApp.LocalJobs.Features.Posting.Models;
using System.Linq;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    public class JobPackage_VM : INotifyPropertyChanged
    {
        
        IUserDialogs dialog = UserDialogs.Instance;
        public Command<string> TapOnStarterPackage { get; set; }
        public Command<string> TapOnbasicPackage { get; set; }
        public Command<string> TapOnPremiumPackage { get; set; }
        public Command<string> TapOnStandardPackage { get; set; }
        public Command CityListcommand { get; set; }
        public Command TapAddbanner { get; set; }
        public Command TapAddnationwide { get; set; }
        public Command Modifycitiescommand { get; set; }
        public Command SkipAddtionalcities { get; set; }
        public Command Submitaddtionalcities { get; set; }

        //added

        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        public Command showdaycountlist { get; set; }
        public Command selectcostdaycnt { get; set; }
        public Command topbannercmd { get; set; }
        public Command excmailercmd { get; set; }
        public Command jobslotcmd { get; set; }
        public Command bonusdayscmd { get; set; }


        public static IDictionary<string, string> addionalcities = new Dictionary<string, string>();

        public string _standardpkgdays = "60";
        public string _standardpkgamnt = "$99";
        public string _premiumpkgdays = "90";
        public string _premiumpkgamnt = "$150";
        public string _basicpkgdays = "45";
        public string _basicpkgamnt = "$80";
        public string _starterpkgdays = "25";
        public string _starterpkgamnt = "$55";
        public string categoryflag = "0";
        public bool _standardpkgbtn = true;
        public bool _selstandardpkgbtn = false;
        public bool _premiumbtn = true;
        public bool _selpremiumbtn = false;
        public bool _basicbtn = true;
        public bool _selbasicbtn = false;
        public bool _starterbtn = true;
        public bool _selstarterbtn = false;
        public bool _standardpkg = false;
        public bool _premiumpkg = false;
        public bool _basicpkg = false;
        public bool _starterpkg = false;
        public decimal _startercityamnt;
        public string _starterbanneramnt = "";
        public string _starternationwideamnt = "";

        public decimal _basiccityamnt;
        public string _basicbanneramnt = "";
        public string _basicnationwideamnt = "";

        public decimal _standardcityamnt ;
        public string _standardbanneramnt = "";
        public string _standardnationwideamnt = "";

        public decimal _premiumcityamnt ;
        public string _premiumbanneramnt = "";
        public string _premiumnationwideamnt = "";

        public List<Cityajax> _cityajax { get; set; }
        public List<Cityajax> CityAjax
        {
            get
            {
                return _cityajax;
            }
            set
            {
                _cityajax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CityAjax)));
            }
        }
        public string _trcity { get; set; }
        public string Trainingcity
        {
            get { return _trcity; }
            set
            {

                if (_trcity != value)
                {
                    _trcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trainingcity)));
                    if (SelTrainingcity != _trcity)
                        ajaxcity(_trcity);
                }

            }
        }

        public string _seltrcity { get; set; }
        public string SelTrainingcity
        {
            get { return _seltrcity; }
            set
            {

                if (_seltrcity != value)
                {
                    _seltrcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelTrainingcity)));
                }

            }
        }

        public string _selcity { get; set; }
        public string Selcity
        {
            get { return _selcity; }
            set
            {

                if (_selcity != value)
                {
                    _selcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selcity)));
                }

            }
        }
        public bool _cityvisible = false;
        public bool CityVisible
        {
            get { return _cityvisible; }
            set
            {
                if (_cityvisible != value)
                {
                    _cityvisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CityVisible)));
                }

            }
        }
        public bool standardpkgvisible
        {
            get { return _standardpkg; }
            set
            {
                _standardpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgvisible)));

            }
        }
        public bool premiumpkgvisible
        {
            get { return _premiumpkg; }
            set
            {
                _premiumpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgvisible)));

            }
        }

        public bool basicpkgvisible
        {
            get { return _basicpkg; }
            set
            {
                _basicpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgvisible)));

            }
        }
        public bool starterpkgvisible
        {
            get { return _starterpkg; }
            set
            {
                _starterpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgvisible)));

            }
        }

        public bool standardpkgbtn
        {
            get { return _standardpkgbtn; }
            set
            {
                _standardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgbtn)));

            }
        }
        public bool Selstandardpkgbtn
        {
            get { return _selstandardpkgbtn; }
            set
            {
                _selstandardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selstandardpkgbtn)));

            }
        }
        public bool premiumbtn
        {
            get { return _premiumbtn; }
            set
            {
                _premiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumbtn)));

            }
        }
        public bool Selpremiumbtn
        {
            get { return _selpremiumbtn; }
            set
            {
                _selpremiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selpremiumbtn)));

            }
        }
        public bool basicbtn
        {
            get { return _basicbtn; }
            set
            {
                _basicbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicbtn)));

            }
        }
        public bool Selbasicbtn
        {
            get { return _selbasicbtn; }
            set
            {
                _selbasicbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selbasicbtn)));

            }
        }
        public bool starterbtn
        {
            get { return _starterbtn; }
            set
            {
                _starterbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterbtn)));

            }
        }
        public bool Selstarterbtn
        {
            get { return _selstarterbtn; }
            set
            {
                _selstarterbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selstarterbtn)));

            }
        }

        public string standardpkgdays
        {
            get { return _standardpkgdays; }
            set
            {
                _standardpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgdays)));

            }
        }
        public string standardpkgamnt
        {
            get { return _standardpkgamnt; }
            set
            {
                _standardpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgamnt)));

            }
        }
        public string premiumpkgdays
        {
            get { return _premiumpkgdays; }
            set
            {
                _premiumpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgdays)));

            }
        }
        public string premiumpkgamnt
        {
            get { return _premiumpkgamnt; }
            set
            {
                _premiumpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgamnt)));

            }
        }
        public string basicpkgdays
        {
            get { return _basicpkgdays; }
            set
            {
                _basicpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgdays)));

            }
        }
        public string basicpkgamnt
        {
            get { return _basicpkgamnt; }
            set
            {
                _basicpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgamnt)));

            }
        }
        public string starterpkgdays
        {
            get { return _starterpkgdays; }
            set
            {
                _starterpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgdays)));

            }
        }
        public string starterpkgamnt
        {
            get { return _starterpkgamnt; }
            set
            {
                _starterpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgamnt)));

            }
        }

     
        public decimal startercityamnt
        {
            get { return _startercityamnt; }
            set
            {
                _startercityamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(startercityamnt)));

            }
        }
        public string starterbanneramnt
        {
            get { return _starterbanneramnt; }
            set
            {
                _starterbanneramnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterbanneramnt)));

            }
        }
        public string starternationwideamnt
        {
            get { return _starternationwideamnt; }
            set
            {
                _starternationwideamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starternationwideamnt)));

            }
        }

        public decimal basiccityamnt
        {
            get { return _basiccityamnt; }
            set
            {
                _basiccityamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basiccityamnt)));

            }
        }
        public string basicbanneramnt
        {
            get { return _basicbanneramnt; }
            set
            {
                _basicbanneramnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicbanneramnt)));

            }
        }
        public string basicnationwideamnt
        {
            get { return _basicnationwideamnt; }
            set
            {
                _basicnationwideamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicnationwideamnt)));

            }
        }

        public decimal standardcityamnt
        {
            get { return _standardcityamnt; }
            set
            {
                _standardcityamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardcityamnt)));

            }
        }
        public string standardbanneramnt
        {
            get { return _standardbanneramnt; }
            set
            {
                _standardbanneramnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardbanneramnt)));

            }
        }
        public string standardnationwideamnt
        {
            get { return _standardnationwideamnt; }
            set
            {
                _standardnationwideamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardnationwideamnt)));

            }
        }

        public decimal premiumcityamnt
        {
            get { return _premiumcityamnt; }
            set
            {
                _premiumcityamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumcityamnt)));

            }
        }
        public string premiumbanneramnt
        {
            get { return _premiumbanneramnt; }
            set
            {
                _premiumbanneramnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumbanneramnt)));

            }
        }
        public string premiumnationwideamnt
        {
            get { return _premiumnationwideamnt; }
            set
            {
                _premiumnationwideamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumnationwideamnt)));

            }
        }
        public List<JobPackages> _pack { get; set; }
        public List<JobPackages> Pack
        {
            get
            {
                return _pack;
            }
            set
            {
                _pack = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pack)));
            }
        }

        public string _nationwidechk = "CheckBoxUnChecked.png";
        public string nationwidechk
        {
            get { return _nationwidechk; }
            set
            {
                _nationwidechk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nationwidechk)));

            }
        }
        public string _bannerchk = "CheckBoxUnChecked.png";
        public string bannerchk
        {
            get { return _bannerchk; }
            set
            {
                _bannerchk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bannerchk)));

            }
        }

        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(contentviewvisible))); }
        }
        private List<perdaycount> _bonusdaycntlist;
        public List<perdaycount> bonusdaycntlist
        {
            get { return _bonusdaycntlist; }
            set { _bonusdaycntlist = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bonusdaycntlist))); }
        }
         
        List<perdaycount> bonusdaycnts = new List<perdaycount>()
        {
           new perdaycount{ daycount=5,checkimage="RadioButtonUnChecked.png"},
           new perdaycount{ daycount=10,checkimage="RadioButtonUnChecked.png"},
           new perdaycount{ daycount=15,checkimage="RadioButtonUnChecked.png"},
           new perdaycount{ daycount=20,checkimage="RadioButtonUnChecked.png"},
        };
        

        //standard addon
        private string _standardemailblastamt = "";
        public string standardemailblastamt
        {
            get { return _standardemailblastamt; }
            set { _standardemailblastamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardemailblastamt))); }
        }
        private string _standardaddonamt = "";
        public string standardaddonamt
        {
            get { return _standardaddonamt; }
            set { _standardaddonamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardaddonamt))); }
        }
        private decimal _standardperdaycost ;
        public decimal standardperdaycost
        {
            get { return _standardperdaycost; }
            set { _standardperdaycost = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardperdaycost))); }
        }
        //starter addon
        private string _starteremailblastamt = "";
        public string starteremailblastamt
        {
            get { return _starteremailblastamt; }
            set { _starteremailblastamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starteremailblastamt))); }
        }
        private string _starteraddonamt = "";
        public string starteraddonamt
        {
            get { return _starteraddonamt; }
            set { _starteraddonamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starteraddonamt))); }
        }
        private decimal _starterperdaycost;
        public decimal starterperdaycost
        {
            get { return _starterperdaycost; }
            set { _starterperdaycost = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterperdaycost))); }
        }
        //Basic addon
        private string _basicemailblastamt = "";
        public string basicemailblastamt
        {
            get { return _basicemailblastamt; }
            set { _basicemailblastamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicemailblastamt))); }
        }
        private string _basicaddonamt = "";
        public string basicaddonamt
        {
            get { return _basicaddonamt; }
            set { _basicaddonamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicaddonamt))); }
        }
        private decimal _basicperdaycost ;
        public decimal basicperdaycost
        {
            get { return _basicperdaycost; }
            set { _basicperdaycost = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicperdaycost))); }
        }
        //premium addon
        private string _premiumemailblastamt = "";
        public string premiumemailblastamt
        {
            get { return _premiumemailblastamt; }
            set { _premiumemailblastamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumemailblastamt))); }
        }
        private string _premiumaddonamt = "";
        public string premiumaddonamt
        {
            get { return _premiumaddonamt; }
            set { _premiumaddonamt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumaddonamt))); }
        }
        private decimal _premiumperdaycost ;
        public decimal premiumperdaycost
        {
            get { return _premiumperdaycost; }
            set { _premiumperdaycost = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumperdaycost))); }
        }
        private string _topbannerbtntxt = "Add";
        public string topbannerbtntxt
        {
            get { return _topbannerbtntxt; }
            set { _topbannerbtntxt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(topbannerbtntxt))); }
        }
        private string _excmailerbtntxt = "Add";
        public string excmailerbtntxt
        {
            get { return _excmailerbtntxt; }
            set { _excmailerbtntxt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(excmailerbtntxt))); }
        }
        private string _jobslotbtntxt = "Add";
        public string jobslotbtntxt
        {
            get { return _jobslotbtntxt; }
            set { _jobslotbtntxt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jobslotbtntxt))); }
        }
        private string _bonusdaybtntxt = "Add";
        public string bonusdaybtntxt
        {
            get { return _bonusdaybtntxt; }
            set { _bonusdaybtntxt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bonusdaybtntxt))); }
        }
        

        public decimal _topbannerchk =0;
        public decimal topbannerchk
        {
            get { return _topbannerchk; }
            set
            {
                _topbannerchk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(topbannerchk)));
            }
        }
        public decimal _exclusivemailerchk = 0;
        public decimal exclusivemailerchk
        {
            get { return _exclusivemailerchk; }
            set
            {
                _exclusivemailerchk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(exclusivemailerchk)));

            }
        }
        public decimal _jobslotchk = 0;
        public decimal jobslotchk
        {
            get { return _jobslotchk; }
            set
            {
                _jobslotchk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jobslotchk)));

            }
        }
        public decimal _bonusdaychk = 0;
        public decimal bonusdaychk
        {
            get { return _bonusdaychk; }
            set
            {
                _bonusdaychk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bonusdaychk)));

            }
        }


        public string _topbannerchktxt;
        public string topbannerchktxt
        {
            get { return _topbannerchktxt; }
            set
            {
                _topbannerchktxt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(topbannerchktxt)));
            }
        }
        public string _exclusivemailerchktxt;
        public string exclusivemailerchktxt
        {
            get { return _exclusivemailerchktxt; }
            set
            {
                _exclusivemailerchktxt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(exclusivemailerchktxt)));

            }
        }
        public string _jobslotchktxt;
        public string jobslotchktxt
        {
            get { return _jobslotchktxt; }
            set
            {
                _jobslotchktxt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jobslotchktxt)));

            }
        }
        public string _bonusdaychktxt;
        public string bonusdaychktxt
        {
            get { return _bonusdaychktxt; }
            set
            {
                _bonusdaychktxt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bonusdaychktxt)));

            }
        }

        Postingdata pft = new Postingdata();

        public Command topbannerviewsamplecmd { get; set; }
        public Command topbannerhwitwrkscmd { get; set; }
        public Command mailerviewinfocmd { get; set; }
        public Command jobslotviewinfocmd { get; set; }
        public Command bonusdaysviewinfocmd { get; set; }

        public bool _topbannerviewsamplevisible=false;
        public bool topbannerviewsamplevisible
        {
            get { return _topbannerviewsamplevisible; }
            set
            {
                _topbannerviewsamplevisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(topbannerviewsamplevisible)));

            }
        }
        public bool _topbannerhwitwrksvisible = false;
        public bool topbannerhwitwrksvisible
        {
            get { return _topbannerhwitwrksvisible; }
            set
            {
                _topbannerhwitwrksvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(topbannerhwitwrksvisible)));

            }
        }
        public bool _mailerviewinfovisible = false;
        public bool mailerviewinfovisible
        {
            get { return _mailerviewinfovisible; }
            set
            {
                _mailerviewinfovisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(mailerviewinfovisible)));

            }
        }
        public bool _jobslotviewinfovisible = false;
        public bool jobslotviewinfovisible
        {
            get { return _jobslotviewinfovisible; }
            set
            {
                _jobslotviewinfovisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jobslotviewinfovisible)));

            }
        }
        public bool _bonusdaysviewinfovisible = false;
        public bool bonusdaysviewinfovisible
        {
            get { return _bonusdaysviewinfovisible; }
            set
            {
                _bonusdaysviewinfovisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bonusdaysviewinfovisible)));

            }
        }
        public bool _dayscountlistsvisible = false;
        public bool dayscountlistsvisible
        {
            get { return _dayscountlistsvisible; }
            set
            {
                _dayscountlistsvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(dayscountlistsvisible)));

            }
        }
        public bool _topbanneraddimgvisible = false;
        public bool topbanneraddimgvisible
        {
            get { return _topbanneraddimgvisible; }
            set
            {
                _topbanneraddimgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(topbanneraddimgvisible)));

            }
        }
        public bool _maileraddimgvisible = false;
        public bool maileraddimgvisible
        {
            get { return _maileraddimgvisible; }
            set
            {
                _maileraddimgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(maileraddimgvisible)));
            }
        }
        public bool _jobslotaddimgvisible = false;
        public bool jobslotaddimgvisible
        {
            get { return _jobslotaddimgvisible; }
            set
            {
                _jobslotaddimgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jobslotaddimgvisible)));
            }
        }
        public bool _bonusdaysaddimgvisible = false;
        public bool bonusdaysaddimgvisible
        {
            get { return _bonusdaysaddimgvisible; }
            set
            {
                _bonusdaysaddimgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bonusdaysaddimgvisible)));
            }
        }
        public bool _totalamntvisible = true;
        public bool totalamntvisible
        {
            get { return _totalamntvisible; }
            set
            {
                _totalamntvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(totalamntvisible)));
            }
        }
        public string _savedjobslotamnt = "";
        public string savedjobslotamnt
        {
            get { return _savedjobslotamnt; }
            set
            {
                _savedjobslotamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(savedjobslotamnt)));
            }
        }
        

        public JobPackage_VM(Postingdata pst)
        {
            pft = pst;
            //bonusdaycnts.Add(5);
            //bonusdaycnts.Add(10);
            //bonusdaycnts.Add(15);
            //bonusdaycnts.Add(20);
            //bonusdaycntlist = bonusdaycnts;
            TapOnStarterPackage = new Command<string>(SelectStarterpkg);
            TapOnbasicPackage = new Command<string>(Selectbasicpkg);
            TapOnPremiumPackage = new Command<string>(SelectPremiumpkg);
            TapOnStandardPackage = new Command<string>(SelectStandpkg);

            //added
           


            GetJobpackages(pst.functionalareaid.ToString(), pst.roleid, pst.newcityurl);
            //if (lcfsecond.postscuccesbackbtn == 1)
            //{
            //    var currentpage = GetCurrentPage();
            //    currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
            //}
           
        }

        private string _Totalpayamount = "";
        public string Totalpayamount
        {
            get { return _Totalpayamount; }
            set
            {
                _Totalpayamount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Totalpayamount)));
            }
        }
        public string _cityamt { get; set; }
        public string Cityamt
        {
            get { return _cityamt; }
            set
            {
                _cityamt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cityamt)));

            }
        }
        public string _citiestext { get; set; }
        public string Citiestext
        {
            get { return _citiestext; }
            set
            {
                _citiestext = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Citiestext)));

            }
        }
        public decimal percityamnt;
        
        public decimal pkgamnt;
       // public string addbanneramnt;
        public string addnationwideamnt;
        public string pkg = "";

        private string _packageamount="";
        public string packageamount
        {
            get { return _packageamount; }
            set { _packageamount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(packageamount))); }
        }

        private string _emailblastamount = "";
        public string emailblastamount
        {
            get { return _emailblastamount; }
            set { _emailblastamount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(emailblastamount))); }
        }
        private decimal _perdaycostamount=0;
        public decimal perdaycostamount
        {
            get { return _perdaycostamount; }
            set { _perdaycostamount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(perdaycostamount))); }
        }
        private string _daycountID = "0";
        public string daycountID
        {
            get { return _daycountID; }
            set { _daycountID = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(daycountID))); }
        }
        private int _daycounttxt = 5;
        public int daycounttxt
        {
            get { return _daycounttxt; }
            set { _daycounttxt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(daycounttxt))); }
        }
        private decimal _perdaycostamounttxt;
        public decimal perdaycostamounttxt
        {
            get { return _perdaycostamounttxt; }
            set { _perdaycostamounttxt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(perdaycostamounttxt))); }
        }
        private string _addbanneramnt = "";
        public string addbanneramnt
        {
            get { return _addbanneramnt; }
            set { _addbanneramnt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(addbanneramnt))); }
        }

        private string _exclusivemaileramnt = "";
        public string exclusivemaileramnt
        {
            get { return _exclusivemaileramnt; }
            set { _exclusivemaileramnt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(exclusivemaileramnt))); }
        }

        private string _jobslotamnt = "";
        public string jobslotamnt
        {
            get { return _jobslotamnt; }
            set { _jobslotamnt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jobslotamnt))); }
        }

        private string _bonusdaysamnt = "";
        public string bonusdaysamnt
        {
            get { return _bonusdaysamnt; }
            set { _bonusdaysamnt = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bonusdaysamnt))); }
        }

       
        public JobPackage_VM(Postingdata data, string package, string amnt, decimal cityamnt, string banneramnt, string nationwideamnt,string page, string emailblastamt, string addonsmt, decimal perdaycost,int daycounttxtID, string topbannerbtntxtid,string excmailerbtntxtid,string jobslotbtntxtid,string bonusdaybtntxtid)
        {
           try
            {
                pft = data;
                percityamnt = cityamnt;
                addbanneramnt = banneramnt;
                exclusivemaileramnt = emailblastamt;
                jobslotamnt = addonsmt;
                bonusdaycntlist = bonusdaycnts;
                if(daycounttxtID==0)
                {
                    daycounttxt = 5;
                }
                else
                {
                    daycounttxt = daycounttxtID;
                }
                perdaycostamount = perdaycost;
                perdaycostamounttxt = daycounttxt * perdaycost;
                addnationwideamnt = nationwideamnt;
                Cityamt = percityamnt.ToString("N");
                Cityamt = "$" + Cityamt.ToString();
                pkg = package;
                Totalpayamount = amnt;
                packageamount = amnt;
                pkgamnt = Convert.ToDecimal(amnt.Replace("$", ""));
                //addon
                emailblastamount = emailblastamt;
                //addonamount = addonsmt;
                decimal savedamount = pkgamnt - Convert.ToDecimal(addonsmt);
                savedjobslotamnt = "You have saved $" + savedamount;

                //topbannerbtntxt = topbannerbtntxtid;
                //excmailerbtntxt = excmailerbtntxtid ;
                //jobslotbtntxtID=jobslotbtntxtid ;
                //bonusdaybtntxtID= bonusdaybtntxtid;


                CityListcommand = new Command<Cityajax>(Selectajaxcity);
                TapAddbanner = new Command(Addbanner);
                TapAddnationwide = new Command(Addnationwide);
                Modifycitiescommand = new Command(Openselectedcities);
                SkipAddtionalcities = new Command(GotoPayment);

                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);
                topbannercmd = new Command(getbanneraddonamount);
                excmailercmd = new Command(getexclusivemaileraddonamount);
                jobslotcmd = new Command(getjobslotaddonamount);
                bonusdayscmd = new Command(getbonusdaysaddonamount);
                showdaycountlist = new Command(showdaycountlists);
                selectcostdaycnt = new Command<perdaycount>(selectnoofdays);

                topbannerviewsamplecmd = new Command(showtopbannerviewsample);
                topbannerhwitwrkscmd = new Command(showtopbannerhwitwrks);
                mailerviewinfocmd = new Command(showmailerviewinfo);
                jobslotviewinfocmd = new Command(showjobslotviewinfo);
                bonusdaysviewinfocmd = new Command(showbonusdaysviewinfo);

                if (page == "cities")
                {
                    if (topbannerbtntxtid == "Remove")
                    {
                        citygetbanneraddonamount();
                    }
                    if (excmailerbtntxtid == "Remove")
                    {
                        citygetexclusivemaileraddonamount();
                    }
                    if (jobslotbtntxtid == "Remove")
                    {
                        citygetjobslotaddonamount();
                    }
                    if (bonusdaybtntxtid == "Remove")
                    {
                        citygetbonusdaysaddonamount();
                    }
                    Prefillpaymnetinfo();
                }
                else
                    addionalcities.Clear();
                
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void selectnoofdays(perdaycount days)
        {
          try
            {
                if (bonusdaybtntxt == "Remove")
                {
                    daycounttxt = days.daycount;
                    daycountID = days.daycount.ToString();
                    perdaycostamounttxt = days.daycount * perdaycostamount;
                    bonusdaychk = perdaycostamounttxt;
                    decimal price;
                    if (addionalcities.Count > 0)
                    {
                        price = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + price.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            price = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            price = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + price.ToString("N");
                    }
                    topbannerchktxt = "$" + topbannerchk.ToString("N");
                    Totalpayamount = "$" + price.ToString("N");

                    Totalpayamount = "$" + price.ToString("N");
                    Closecontentview();
                }
                else
                {
                    // dialog.Toast("Add bonus day add-ons to select number of days!");
                    daycounttxt = days.daycount;
                    daycountID = days.daycount.ToString();
                    perdaycostamounttxt = days.daycount * perdaycostamount;
                    Closecontentview();
                }
            }
            catch(Exception ex)
            {

            }
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
            totalamntvisible = true;
        }
        public void showdaycountlists()
        {
            try
            {
                var curentpage = GetCurrentPage();
                var perdaycostlistview = curentpage.FindByName<ListView>("perdaycostlist");
                foreach (var data in bonusdaycntlist)
                {
                    if (daycounttxt == data.daycount)
                    {
                        data.checkimage = "RadioButtonChecked.png";
                        daycounttxt = data.daycount;
                        perdaycostamounttxt = data.daycount * perdaycostamount;
                    }
                    else
                    {
                        data.checkimage = "RadioButtonUnChecked.png";
                    }
                }
                perdaycostlistview.ItemsSource = null;
                perdaycostlistview.ItemsSource = bonusdaycntlist;
                contentviewvisible = true;
                dayscountlistsvisible = true;
                topbannerviewsamplevisible = false;
                topbannerhwitwrksvisible = false;
                mailerviewinfovisible = false;
                jobslotviewinfovisible = false;
                bonusdaysviewinfovisible = false;
                totalamntvisible = false;
            }
            catch(Exception ex)
            {

            }
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        public void showtopbannerviewsample()
        {
            contentviewvisible = true;
            dayscountlistsvisible = false;
            topbannerviewsamplevisible = true;
            topbannerhwitwrksvisible = false;
            mailerviewinfovisible = false;
            jobslotviewinfovisible = false;
            bonusdaysviewinfovisible = false;
            totalamntvisible = false;
        }
        public void showtopbannerhwitwrks()
        {
            contentviewvisible = true;
            dayscountlistsvisible = false;
            topbannerviewsamplevisible = false;
            topbannerhwitwrksvisible = true;
            mailerviewinfovisible = false;
            jobslotviewinfovisible = false;
            bonusdaysviewinfovisible = false;
            totalamntvisible = false;
        }
        public void showmailerviewinfo()
        {
            contentviewvisible = true;
            dayscountlistsvisible = false;
            topbannerviewsamplevisible = false;
            topbannerhwitwrksvisible = false;
            mailerviewinfovisible = true;
            jobslotviewinfovisible = false;
            bonusdaysviewinfovisible = false;
            totalamntvisible = false;
        }
        public void showjobslotviewinfo()
        {
            contentviewvisible = true;
            dayscountlistsvisible = false;
            topbannerviewsamplevisible = false;
            topbannerhwitwrksvisible = false;
            mailerviewinfovisible = false;
            jobslotviewinfovisible = true;
            bonusdaysviewinfovisible = false;
            totalamntvisible = false;
        }
        public void showbonusdaysviewinfo()
        {
            contentviewvisible = true;
            dayscountlistsvisible = false;
            topbannerviewsamplevisible = false;
            topbannerhwitwrksvisible = false;
            mailerviewinfovisible = false;
            jobslotviewinfovisible = false;
            bonusdaysviewinfovisible = true;
            totalamntvisible = false;
        }
        public async void citygetbanneraddonamount()
        {
            try
            {
                decimal tamount;
                await Task.Delay(300);
                var currentpage = GetCurrentPage();
                Button topbannerbtn = currentpage.FindByName<Button>("topbannerbtn");
                topbanneraddimgvisible = true;
                topbannerbtn.BackgroundColor = Color.FromHex("#e6e6e6");
                topbannerbtn.TextColor = Color.FromHex("#333333");
                topbannerchk = Convert.ToDecimal(addbanneramnt);
                topbannerbtntxt = "Remove";
                if (addionalcities.Count > 0)
                {
                    tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                    Totalpayamount = "$" + tamount.ToString("N");
                }
                else
                {
                    if (nationwidechk == "CheckBoxChecked.png")
                        tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                    else
                        tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                    Totalpayamount = "$" + tamount.ToString("N");
                }
                topbannerchktxt = "$" + topbannerchk.ToString("N");
                Totalpayamount = "$" + tamount.ToString("N");
            }
            catch(Exception ex)
            {

            }
        }
        public async void citygetexclusivemaileraddonamount()
        {
            try
            {
                await Task.Delay(300);
                var currentpage = GetCurrentPage();
                Button mailerbtn = currentpage.FindByName<Button>("mailerbtn");
                decimal tamount;
            
                    mailerbtn.BackgroundColor = Color.FromHex("#e6e6e6");
                    mailerbtn.TextColor = Color.FromHex("#333333");
                    maileraddimgvisible = true;
                    exclusivemailerchk = Convert.ToDecimal(exclusivemaileramnt);
                    excmailerbtntxt = "Remove";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }

                    Totalpayamount = "$" + tamount.ToString("N");
                
            }
            catch(Exception ex)
            {

            }
        }
        public async void citygetjobslotaddonamount()
        {
            try
            {
                await Task.Delay(300);
                var currentpage = GetCurrentPage();
                Button jobslotbtn = currentpage.FindByName<Button>("jobslotbtn");
                decimal tamount;
                    jobslotbtn.BackgroundColor = Color.FromHex("#e6e6e6");
                    jobslotbtn.TextColor = Color.FromHex("#333333");
                    // bannerchk = "CheckBoxChecked.png";
                    jobslotaddimgvisible = true;
                    jobslotchk = Convert.ToDecimal(jobslotamnt);
                    jobslotbtntxt = "Remove";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }

                    Totalpayamount = "$" + tamount.ToString("N");
             
            }
            catch(Exception ex)
            {

            }
        }
        public async void citygetbonusdaysaddonamount()
        {
            try
            {
                await Task.Delay(300);
                var currentpage = GetCurrentPage();
                Button bonusdaybtn = currentpage.FindByName<Button>("bonusdaybtn");

                decimal tamount;
                
                    bonusdaybtn.BackgroundColor = Color.FromHex("#e6e6e6");
                    bonusdaybtn.TextColor = Color.FromHex("#333333");
                    // bannerchk = "CheckBoxChecked.png";
                    bonusdaysaddimgvisible = true;

                    Label lbl = currentpage.FindByName<Label>("daycnt");
                    daycountID = lbl.Text;
                    bonusdaychk = Convert.ToDecimal(perdaycostamounttxt);
                    bonusdaybtntxt = "Remove";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }

                    Totalpayamount = "$" + tamount.ToString("N");
                
            }
            catch(Exception ex)
            {

            }
        }
        public void getbanneraddonamount()
        {
           try
            {
                var currentpage = GetCurrentPage();
                Button topbannerbtn = currentpage.FindByName<Button>("topbannerbtn");
                decimal tamount;
                if (topbannerbtntxt == "Add")
                {
                    // bannerchk = "CheckBoxChecked.png";
                    topbanneraddimgvisible = true;
                    topbannerbtn.BackgroundColor = Color.FromHex("#e6e6e6");
                    topbannerbtn.TextColor = Color.FromHex("#333333");
                    topbannerchk = Convert.ToDecimal(addbanneramnt);
                    topbannerbtntxt = "Remove";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    topbannerchktxt = "$" + topbannerchk.ToString("N");
                    Totalpayamount = "$" + tamount.ToString("N");
                }
                else
                {
                    topbanneraddimgvisible = false;
                    topbannerbtn.BackgroundColor = Color.FromHex("#51a351");
                    topbannerbtn.TextColor = Color.White;
                    topbannerbtntxt = "Add";
                    bannerchk = "CheckBoxUnChecked.png";
                    topbannerchk = 0;
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    Totalpayamount = "$" + tamount.ToString("N");
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void getexclusivemaileraddonamount()
        {
           try
            {
                var currentpage = GetCurrentPage();
                Button mailerbtn = currentpage.FindByName<Button>("mailerbtn");
                decimal tamount;
                if (excmailerbtntxt == "Add")
                {
                    mailerbtn.BackgroundColor = Color.FromHex("#e6e6e6");
                    mailerbtn.TextColor = Color.FromHex("#333333");
                    // bannerchk = "CheckBoxChecked.png";
                    maileraddimgvisible = true;
                    exclusivemailerchk = Convert.ToDecimal(exclusivemaileramnt);
                    excmailerbtntxt = "Remove";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }

                    Totalpayamount = "$" + tamount.ToString("N");
                }
                else
                {
                    mailerbtn.BackgroundColor = Color.FromHex("#51a351");
                    mailerbtn.TextColor = Color.White;
                    maileraddimgvisible = false;
                    excmailerbtntxt = "Add";
                    exclusivemailerchk = 0;
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    Totalpayamount = "$" + tamount.ToString("N");
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void getjobslotaddonamount()
        {
           try
            {
                var currentpage = GetCurrentPage();
                Button jobslotbtn = currentpage.FindByName<Button>("jobslotbtn");
                decimal tamount;
                if (jobslotbtntxt == "Add")
                {
                    jobslotbtn.BackgroundColor = Color.FromHex("#e6e6e6");
                    jobslotbtn.TextColor = Color.FromHex("#333333");
                    // bannerchk = "CheckBoxChecked.png";
                    jobslotaddimgvisible = true;
                    jobslotchk = Convert.ToDecimal(jobslotamnt);
                    jobslotbtntxt = "Remove";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }

                    Totalpayamount = "$" + tamount.ToString("N");
                }
                else
                {
                    jobslotbtn.BackgroundColor = Color.FromHex("#51a351");
                    jobslotbtn.TextColor = Color.White;
                    jobslotaddimgvisible = false;
                    jobslotbtntxt = "Add";
                    jobslotchk = 0;
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    Totalpayamount = "$" + tamount.ToString("N");
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void getbonusdaysaddonamount()
        {
            try
            {
                var currentpage = GetCurrentPage();
                Button bonusdaybtn = currentpage.FindByName<Button>("bonusdaybtn");

                decimal tamount;
                if (bonusdaybtntxt == "Add")
                {

                    bonusdaybtn.BackgroundColor = Color.FromHex("#e6e6e6");
                    bonusdaybtn.TextColor = Color.FromHex("#333333");
                    // bannerchk = "CheckBoxChecked.png";
                    bonusdaysaddimgvisible = true;

                    Label lbl = currentpage.FindByName<Label>("daycnt");
                    daycountID = lbl.Text;
                    bonusdaychk = Convert.ToDecimal(perdaycostamounttxt);
                    bonusdaybtntxt = "Remove";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }

                    Totalpayamount = "$" + tamount.ToString("N");
                }
                else
                {
                    bonusdaybtn.BackgroundColor = Color.FromHex("#51a351");
                    bonusdaybtn.TextColor = Color.White;
                    bonusdaysaddimgvisible = false;
                    bonusdaybtntxt = "Add";
                    bonusdaychk = 0;

                    Label lbl = currentpage.FindByName<Label>("daycnt");
                    daycountID = "";
                    if (addionalcities.Count > 0)
                    {
                        tamount = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    else
                    {
                        if (nationwidechk == "CheckBoxChecked.png")
                            tamount = pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        else
                            tamount = pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                        Totalpayamount = "$" + tamount.ToString("N");
                    }
                    Totalpayamount = "$" + tamount.ToString("N");
                }
            }
            catch(Exception ex)
            {

            }
        }

        public async void Selectbasicpkg(string amnnt)
        {
            var curpage = GetCurrentPage();
          //  dialog.ShowLoading("");
            basicbtn = false;
            Selbasicbtn = true;
            premiumbtn = true;
            Selpremiumbtn = false;
            starterbtn = true;
            Selstarterbtn = false;
            standardpkgbtn = true;
            Selstandardpkgbtn = false;
            pft.validitydays = basicpkgdays;
            await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.additionalcities(pft,"basic", basicpkgamnt, basiccityamnt, basicbanneramnt, basicnationwideamnt,"", basicemailblastamt, basicaddonamt, basicperdaycost , daycounttxt, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));
            //var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
            //Pkgsucess list = await api.updatepackage(adid.ToString(), basicpkgdays, "basic", basictotalads, agentid);
            //if (list.resultinformation == "updated successfully")
            //{
            //    dialog.HideLoading();
            //    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Payment(amnnt, "basic", adid, pft));
            //}
            //else
            //{
            //    dialog.HideLoading();
            //    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Thankyou());

            //}
        }
        public async void SelectPremiumpkg(string amnnt)
        {
            var curpage = GetCurrentPage();
           // dialog.ShowLoading("");
            basicbtn = true;
            Selbasicbtn = false;
            premiumbtn = false;
            Selpremiumbtn = true;
            starterbtn = true;
            Selstarterbtn = false;
            standardpkgbtn = true;
            Selstandardpkgbtn = false;
            pft.validitydays = premiumpkgdays;
            await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.additionalcities(pft, "premium", premiumpkgamnt, premiumcityamnt, premiumbanneramnt, premiumnationwideamnt, "", premiumemailblastamt, premiumaddonamt, premiumperdaycost, daycounttxt, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));

        }
        public async void SelectStandpkg(string amnnt)
        {
            var curpage = GetCurrentPage();
           // dialog.ShowLoading("");
            basicbtn = true;
            Selbasicbtn = false;
            premiumbtn = true;
            Selpremiumbtn = false;
            starterbtn = true;
            Selstarterbtn = false;
            standardpkgbtn = false;
            Selstandardpkgbtn = true;
            pft.validitydays = standardpkgdays;
            await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.additionalcities(pft, "standard", standardpkgamnt, standardcityamnt, standardbanneramnt, standardnationwideamnt, "", standardemailblastamt, standardaddonamt, standardperdaycost, daycounttxt, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));
        }
      
        public async void GetJobpackages(string funareaid, string roleid, string newcityurl)
        {
           try
            {
                dialog.ShowLoading("");
                if(string.IsNullOrEmpty(roleid))
                {
                    roleid = "";
                }
                if(string.IsNullOrEmpty(funareaid))
                {
                    funareaid = "";
                }
                var api = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                JobPackagelist list = await api.getpackages(funareaid, roleid, newcityurl);
                Pack = list.ROW_DATA;
                if (list.ROW_DATA.Count() > 0)
                {
                    foreach (var i in Pack)
                    {
                        if (i.ordertype == "Standard")
                        {
                            standardpkgdays = i.noofdays + " days";
                            standardpkgamnt = "$" + i.amount;
                            standardcityamnt = i.cityamount;
                            standardbanneramnt = i.banneramount;
                            standardnationwideamnt = i.nationwideamount;
                            categoryflag = i.categoryflag;
                            standardpkgvisible = true;

                            standardemailblastamt = i.emailblastamount;
                            standardaddonamt = i.addonamount;
                            standardperdaycost = i.perdaycost;
                        }
                        else if (i.ordertype == "Premium")
                        {
                            premiumpkgamnt = "$" + i.amount;
                            premiumpkgdays = i.noofdays + " days";
                            premiumcityamnt = i.cityamount;
                            premiumbanneramnt = i.banneramount;
                            premiumnationwideamnt = i.nationwideamount;
                            categoryflag = i.categoryflag;
                            premiumpkgvisible = true;

                            premiumemailblastamt = i.emailblastamount;
                            premiumaddonamt = i.addonamount;
                            premiumperdaycost = i.perdaycost;
                        }
                        else if (i.ordertype == "Basic")
                        {
                            basicpkgamnt = "$" + i.amount;
                            basicpkgdays = i.noofdays + " days";
                            basiccityamnt = i.cityamount;
                            basicbanneramnt = i.banneramount;
                            basicnationwideamnt = i.nationwideamount;
                            categoryflag = i.categoryflag;
                            basicpkgvisible = true;

                            basicemailblastamt = i.emailblastamount;
                            basicaddonamt = i.addonamount;
                            basicperdaycost = i.perdaycost;
                        }
                        else if (i.ordertype == "Starter")
                        {
                            starterpkgamnt = "$" + i.amount.ToString();
                            starterpkgdays = i.noofdays + " days";
                            startercityamnt = i.cityamount;
                            starterbanneramnt = i.banneramount;
                            starternationwideamnt = i.nationwideamount;
                            categoryflag = i.categoryflag;
                            starterpkgvisible = true;

                            starteremailblastamt = i.emailblastamount;
                            starteraddonamt = i.addonamount;
                            starterperdaycost = i.perdaycost;
                        }
                    }
                    pft.categoryflag = categoryflag;
                    //if ((!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneed == "1") || (string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneedpayment == "1"))
                    if(pft.ismyneed=="1")
                    {
                        if (pft.Ordertype == "Standard" || string.IsNullOrEmpty(pft.Ordertype))
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            starterbtn = true;
                            Selstarterbtn = false;
                            standardpkgbtn = false;
                            Selstandardpkgbtn = true;
                        }
                        else if (pft.Ordertype == "Premium")
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = false;
                            Selpremiumbtn = true;
                            starterbtn = true;
                            Selstarterbtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                        }
                        else if (pft.Ordertype == "Basic")
                        {
                            basicbtn = false;
                            Selbasicbtn = true;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            starterbtn = true;
                            Selstarterbtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                        }
                        else if (pft.Ordertype == "Starter")
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            starterbtn = false;
                            Selstarterbtn = true;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;

                        }
                    }


                    await Task.Delay(100);
                    var currentpage = GetCurrentPage();
                    var scrollview = currentpage.FindByName<ScrollView>("packagescroll");
                    var stacklayout = currentpage.FindByName<StackLayout>("packagelayout");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                           // scrollview.ScrollToAsync(stacklayout, ScrollToPosition.Center, false);
                            scrollview.ScrollToAsync(550, 0, false);
                            //  await ScrollView. ScrollToAsync((Grid)grid, ScrollToPosition.End, false);
                        });
                    dialog.HideLoading();
                }
                await Task.Delay(500);
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void SelectStarterpkg(string amnnt)
        {
            var curpage = GetCurrentPage();
            try
            {
                //dialog.ShowLoading("");
               
                basicbtn = true;
                Selbasicbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                starterbtn = false;
                Selstarterbtn = true;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                pft.validitydays = starterpkgdays;
                await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.additionalcities(pft, "starter", starterpkgamnt, startercityamnt, starterbanneramnt, starternationwideamnt, "", starteremailblastamt, starteraddonamt, starterperdaycost, daycounttxt, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                dialog.HideLoading();
            }
        }

        public async void ajaxcity(string city)
        {
           try
            {
                Selcity = "";
                var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                Cityajaxlist list = await nsAPI.Getcityajax(city, "all");
                CityAjax = list.ROW_DS;
                if (CityAjax != null)
                {
                    foreach (var i in list.ROW_DS)
                    {
                        i.fullcity = i.city + ", " + i.statecode;
                    }
                    CityVisible = true;
                }
                else
                    CityVisible = false;
            }
            catch(Exception ex)
            {
            }
        }

        public void Selectedcitiescount()
        {
            try
            {
                var curpage = GetCurrentPage();
                var stack = curpage.FindByName<StackLayout>("stackmodify");
                var btnnext = curpage.FindByName<Button>("btncitynext");
                var btnskip = curpage.FindByName<Button>("btncityskip");
                if (addionalcities.Count > 0)
                {

                    stack.IsVisible = true;
                    //btnnext.IsVisible = true;
                    //btnskip.IsVisible = false;

                    if (addionalcities.Count == 1)
                    {

                        Citiestext = "Totally " + addionalcities.Count + " city selected by you";
                    }
                    else
                    {
                        Citiestext = "Totally " + addionalcities.Count + " citie(s) selected by you";
                    }
                }
                else
                {
                    stack.IsVisible = false;
                    //btnnext.IsVisible = false;
                    //btnskip.IsVisible = true;
                }

            }
            catch (Exception ee)
            {

            }
        }
        public void Selectajaxcity(Cityajax model)
        {
          
            try
            {
               
                if (!addionalcities.ContainsKey(model.zipcode) && pft.zipcode!=model.zipcode)
                {
                    //SelTrainingcity = model.city + ", " + model.statecode;
                    //Trainingcity = model.city + ", " + model.statecode;
                    //Selcity = model.citystatecodeurl;
                    nationwidechk = "CheckBoxUnChecked.png";
                    pft.isnationwidechk = "0";
                    SelTrainingcity = "";
                    Trainingcity = "";
                    Selcity = "";
                    addionalcities.Add(model.zipcode, model.city );
                    CityVisible = false;
                    if (addionalcities.Count == 1)
                    {

                        Citiestext = "Totally " + addionalcities.Count + " city selected by you";
                    }
                    else
                    {
                        Citiestext = "Totally " + addionalcities.Count + " citie(s) selected by you";
                    }
                    decimal citiesamnt = addionalcities.Count * Convert.ToDecimal(percityamnt);
                    decimal totalpay = citiesamnt + Convert.ToDecimal(pkgamnt) +topbannerchk+exclusivemailerchk+jobslotchk+bonusdaychk;
                    Totalpayamount = "$"+ totalpay.ToString("N");
                    Selectedcitiescount();
                    dialog.Toast("You have added " + model.city + ", " + model.statecode + ", " + model.countrycode);
                }
                else
                {
                    dialog.Toast("Already added " + model.city + ", " + model.statecode + ", " + model.countrycode);
                   // Trainingcity = "";
                    CityVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
           
               
        }

        public void Addbanner()
        {
            decimal price;
            var curpage = GetCurrentPage();
            var btnnext = curpage.FindByName<Button>("btncitynext");
            var btnskip = curpage.FindByName<Button>("btncityskip");
            if (bannerchk == "CheckBoxUnChecked.png")
            {
                bannerchk = "CheckBoxChecked.png";
                pft.isbannerchk = "1";
                if (addionalcities.Count > 0)
                {
                    price = Convert.ToDecimal(addbanneramnt) + pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt);
                    Totalpayamount = "$" + price.ToString("N");
                }
                else
                {
                    if(nationwidechk == "CheckBoxChecked.png")
                    price = Convert.ToDecimal(addbanneramnt) + pkgamnt + Convert.ToDecimal(addnationwideamnt);
                    else
                        price = Convert.ToDecimal(addbanneramnt) + pkgamnt;
                    Totalpayamount = "$" + price.ToString("N");
                }
                //btnnext.IsVisible = true;
                //btnskip.IsVisible = false;
            }
            else
            {
                bannerchk = "CheckBoxUnChecked.png";
                pft.isbannerchk = "0";
                if (addionalcities.Count > 0)
                {
                    price =  pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt);
                    Totalpayamount = "$" + price.ToString("N");
                }
                else
                {
                    if (nationwidechk == "CheckBoxChecked.png")
                        price = pkgamnt + Convert.ToDecimal(addnationwideamnt);
                    else
                        price = pkgamnt;
                    Totalpayamount = "$" + price.ToString("N");
                }
            }
                

        }

        public void Addnationwide()
        {
            decimal price;
            addionalcities.Clear();
            var curpage = GetCurrentPage();
            var btnnext = curpage.FindByName<Button>("btncitynext");
            var btnskip = curpage.FindByName<Button>("btncityskip");
            if (nationwidechk== "CheckBoxUnChecked.png")
            {
                nationwidechk = "CheckBoxChecked.png";
                pft.isnationwidechk = "1";
               // if (bannerchk == "CheckBoxChecked.png")
                    price =  pkgamnt + Convert.ToDecimal(addnationwideamnt) + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                //else
                  //  price = pkgamnt + Convert.ToDecimal(addnationwideamnt);
                Totalpayamount = "$" + price.ToString("N");
                //btnnext.IsVisible = true;
                //btnskip.IsVisible = false;
            }
            else
            {
                pft.isnationwidechk = "0";
                nationwidechk = "CheckBoxUnChecked.png";
               // if(bannerchk== "CheckBoxChecked.png")
                price =  pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk;
                //else
                  //  price = + pkgamnt;
                Totalpayamount = "$" + price.ToString("N");
            }
            Selectedcitiescount();

        }
        public async void Openselectedcities()
        {
            var curpage = GetCurrentPage();         
            await curpage.Navigation.PushModalAsync(new NRIApp.LocalJobs.Features.Posting.Views.addedcitiesdisplay(pft, pkg,pkgamnt.ToString(),percityamnt,addbanneramnt,addnationwideamnt,emailblastamount, jobslotamnt, perdaycostamount.ToString(), daycounttxt, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));

        }
        public event PropertyChangedEventHandler PropertyChanged;

        public static Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }

        public static Page GetCurrentModalPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();

            return currentPage;
        }

        public async void Prefillpaymnetinfo()
        {
            dialog.ShowLoading("",null);
            decimal price;

            if (addionalcities.Count > 0)
            {
                price = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt)+ topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk; ;
                Totalpayamount = "$" + price.ToString("N");
            }
            else
            {
                if (nationwidechk == "CheckBoxChecked.png")
                    price =  pkgamnt + Convert.ToDecimal(addnationwideamnt)+topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk; 
                else
                    price =  pkgamnt + topbannerchk + exclusivemailerchk + jobslotchk + bonusdaychk; ;
                Totalpayamount = "$" + price.ToString("N");
            }


            //if (pft.isbannerchk == "1")
            //{
            //    bannerchk = "CheckBoxChecked.png";
            //    if (addionalcities.Count > 0)
            //    {
            //        price = Convert.ToDecimal(addbanneramnt) + pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt);
            //        Totalpayamount = "$" + price.ToString("N");
            //    }
            //    else
            //    {
            //        if (nationwidechk == "CheckBoxChecked.png")
            //            price = Convert.ToDecimal(addbanneramnt) + pkgamnt + Convert.ToDecimal(addnationwideamnt);
            //        else
            //            price = Convert.ToDecimal(addbanneramnt) + pkgamnt;
            //        Totalpayamount = "$" + price.ToString("N");
            //    }
            //}
            //else
            //{
            //    bannerchk = "CheckBoxUnChecked.png";
            //    if (addionalcities.Count > 0)
            //    {
            //        price = pkgamnt + addionalcities.Count * Convert.ToDecimal(percityamnt);
            //        Totalpayamount = "$" + price.ToString("N");
            //    }
            //    else
            //    {
            //        if (nationwidechk == "CheckBoxChecked.png")
            //            price = pkgamnt + Convert.ToDecimal(addnationwideamnt);
            //        else
            //            price = pkgamnt;
            //        Totalpayamount = "$" + price.ToString("N");
            //    }
            //}
            await Task.Delay(2000);
            Selectedcitiescount();
            dialog.HideLoading();
        }

        public async void GotoPayment()
        {
           var curpage = GetCurrentPage();
                                                                    
            await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.JobPayment(pkgamnt.ToString(),addbanneramnt,addnationwideamnt,addionalcities.Count(),pkg,Totalpayamount,percityamnt.ToString(),pft,topbannerchk,exclusivemailerchk,jobslotchk,bonusdaychk ,daycountID));
        }
    }
}

