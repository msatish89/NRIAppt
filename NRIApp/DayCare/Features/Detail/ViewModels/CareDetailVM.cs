using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Acr.UserDialogs;
using Refit;
using NRIApp.DayCare.Features.Detail.Interface;
using NRIApp.Helpers;
using NRIApp.DayCare.Features.List.Models;
using NRIApp.DayCare.Features.List.Interface;
using NRIApp.LocalJobs.Features.Detail.Models;
using Plugin.Share;
using NRIApp.DayCare.Features.Detail.Models;
using System.Windows.Input;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.DayCare.Features.Detail.Views;
using System.Threading.Tasks;
using NRIApp.DayCare.Features.Post.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using Refractored.XamForms.PullToRefresh;

namespace NRIApp.DayCare.Features.Detail.ViewModels
{
    public class CareDetailVM:INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public Command adsavecmd { get; set; }
        public Command sharecmd { get; set; }
        public Command<Rating_Details> commentviewcmd { get; set; }
        public Command reportflagcmd { get; set; }
        public ICommand Customeroverallclickratingcommand { get; set; }
        public ICommand customerclickratingcommand { get; set; }
        public ICommand Workclickratingcommand { get; set; }
        public ICommand valueclickratingcommand { get; set; }
        public Command publishreview { get; set; }
        public Command responsecmd { get; set; }
        public Command viewnumbercmd { get; set; }
        public Command photourlclickcommand { get; set; }
        public Command videourlclickcommand { get; set; }
        public Command gotofulldesc { get; set; }
        public static int viewnumcnt = 0;
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        private string _businessname = "";
        public string businessname
        {
            get { return _businessname; }
            set { _businessname = value;OnPropertyChanged(nameof(businessname)); }
        }
        private string _tertiarycatvalue = "";
        public string tertiarycatvalue
        {
            get { return _tertiarycatvalue; }
            set { _tertiarycatvalue = value; OnPropertyChanged(nameof(tertiarycatvalue)); }
        }
        private string _businessid = "";
        public string businessid
        {
            get { return _businessid; }
            set { _businessid = value; OnPropertyChanged(nameof(businessid)); }
        }
        private string _lastrenewdate = "";
        public string lastrenewdate
        {
            get { return _lastrenewdate; }
            set { _lastrenewdate = value; OnPropertyChanged(nameof(lastrenewdate)); }
        }
        private string _businessaddress = "";
        public string businessaddress
        {
            get { return _businessaddress; }
            set { _businessaddress = value; OnPropertyChanged(nameof(businessaddress)); }
        }
        private string _experience = "";
        public string experience
        {
            get { return _experience; }
            set { _experience = value; OnPropertyChanged(nameof(experience)); }
        }
        private string _salary = "";
        public string salary
        {
            get { return _salary; }
            set { _salary = value; OnPropertyChanged(nameof(salary)); }
        }
        private string _gender = "";
        public string gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(nameof(gender)); }
        }
        private string _certification = "";
        public string certification
        {
            get { return _certification; }
            set { _certification = value; OnPropertyChanged(nameof(certification)); }
        }
        private string _fromdate = "";
        public string fromdate
        {
            get { return _fromdate; }
            set { _fromdate = value; OnPropertyChanged(nameof(fromdate)); }
        }
        private string _agegroup = "";
        public string agegroup
        {
            get { return _agegroup; }
            set { _agegroup = value; OnPropertyChanged(nameof(agegroup)); }
        }
        private string _description = "";
        public string description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(description)); }
        }
        private string _descheadertxt = "";
        public string descheadertxt
        {
            get { return _descheadertxt; }
            set { _descheadertxt = value; OnPropertyChanged(nameof(descheadertxt)); }
        }
        
        private bool _mailverified = true;
        public bool mailverified
        {
            get { return _mailverified; }
            set { _mailverified = value; OnPropertyChanged(nameof(mailverified)); }
        }
        private bool _mobileverified = false;
        public bool mobileverified
        {
            get { return _mailverified; }
            set { _mailverified = value; OnPropertyChanged(nameof(mobileverified)); }
        }
        private bool _experiencevisible = false;
        public bool experiencevisible
        {
            get { return _experiencevisible; }
            set { _experiencevisible = value; OnPropertyChanged(nameof(experiencevisible)); }
        }
        private int _agegrouplistheight;
        public int agegrouplistheight
        {
            get { return _agegrouplistheight; }
            set { _agegrouplistheight = value;OnPropertyChanged(nameof(agegrouplistheight)); }
        }
        private List<string> _agegrouplist;
        public List<string> agegrouplist
        {
            get { return _agegrouplist; }
            set { _agegrouplist = value; OnPropertyChanged(nameof(agegrouplist)); }
        }
        private List<string> _additionalservicelist;
        public List<string> additionalservicelist
        {
            get { return _additionalservicelist; }
            set { _additionalservicelist = value; OnPropertyChanged(nameof(additionalservicelist)); }
        }
        private List<string> _servicetaglist;
        public List<string> servicetaglist
        {
            get { return _servicetaglist; }
            set { _servicetaglist = value; OnPropertyChanged(nameof(servicetaglist)); }
        }
        private List<string> _virtualoptionlist;
        public List<string> virtualoptionlist
        {
            get { return _virtualoptionlist; }
            set { _virtualoptionlist = value; OnPropertyChanged(nameof(virtualoptionlist)); }
        }
        private List<string> _photourllist;
        public List<string> photourllist
        {
            get { return _photourllist; }
            set { _photourllist = value; OnPropertyChanged(nameof(photourllist)); }
        }
        private List<string> _videourllist;
        public List<string> videourllist
        {
            get { return _videourllist; }
            set { _videourllist = value; OnPropertyChanged(nameof(videourllist)); }
        }
        private List<string> _availabletiminglist;
        public List<string> availabletiminglist
        {
            get { return _availabletiminglist; }
            set { _availabletiminglist = value; OnPropertyChanged(nameof(availabletiminglist)); }
        }
        private List<string> _languagelist;
        public List<string> languagelist
        {
            get { return _languagelist; }
            set { _languagelist = value; OnPropertyChanged(nameof(languagelist)); }
        }
        private List<Rating_Details> _getreviewdetail;
        public List<Rating_Details> getreviewdetail
        {
            get { return _getreviewdetail; }
            set { _getreviewdetail = value; OnPropertyChanged(nameof(getreviewdetail)); }
        }
        private bool _reviewcommentsvisible;
        public bool reviewcommentsvisible
        {
            get { return _reviewcommentsvisible; }
            set { _reviewcommentsvisible = value; OnPropertyChanged(nameof(reviewcommentsvisible)); }
        }
        private string _rating;
        public string rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(nameof(rating)); }
        }
        private string _reviewdesc;
        public string reviewdesc
        {
            get { return _reviewdesc; }
            set { _reviewdesc = value; OnPropertyChanged(nameof(reviewdesc)); }
        }
        public string _Customerrating1 = "Star_inactive.png";
        public string CustomerRating1
        {
            get { return _Customerrating1; }
            set
            {
                _Customerrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating1)));

            }
        }
        public string _Customerrating2 = "Star_inactive.png";
        public string CustomerRating2
        {
            get { return _Customerrating2; }
            set
            {
                _Customerrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating2)));

            }
        }
        public string _Customerrating3 = "Star_inactive.png";
        public string CustomerRating3
        {
            get { return _Customerrating3; }
            set
            {
                _Customerrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating3)));

            }
        }
        public string _Customerrating4 = "Star_inactive.png";
        public string CustomerRating4
        {
            get { return _Customerrating4; }
            set
            {
                _Customerrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating4)));

            }
        }
        public string _Customerrating5 = "Star_inactive.png";
        public string CustomerRating5
        {
            get { return _Customerrating5; }
            set
            {
                _Customerrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating5)));

            }
        }
        public string _ratingbar = "0/5";
        public string Ratingbar
        {
            get { return _ratingbar; }
            set
            {
                _ratingbar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ratingbar)));

            }
        }
        private string _businesstitleurl = "";
        public string businesstitleurl
        {
            get { return _businesstitleurl; }
            set { _businesstitleurl = value;OnPropertyChanged(nameof(businesstitleurl)); }
        }
        private string _cityurl = "";
        public string cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(nameof(cityurl)); }
        }
        private string _adresult = "";
        public string adresult
        {
            get { return _adresult; }
            set { _adresult = value; OnPropertyChanged(nameof(adresult)); }
        }
        private string _adsaveimg = "HeartGray.png";
        public string AdSaveimg
        {
            get { return _adsaveimg; }
            set { _adsaveimg = value; OnPropertyChanged(nameof(AdSaveimg)); }
        }
        private string _adurl = "";
        public string adurl
        {
            get { return _adurl; }
            set { _adurl = value; OnPropertyChanged(nameof(adurl)); }
        }
        private string _title = "";
        public string title
        {
            get { return _title; }
            set { _title = value;OnPropertyChanged(nameof(title)); }
        }
        private string _emsu = "CheckBoxUnChecked.png";
        public string emsu
        {
            get { return _emsu; }
            set { _emsu = value; OnPropertyChanged(nameof(emsu)); }
        }
        private string _lmsu = "CheckBoxUnChecked.png";
        public string lmsu
        {
            get { return _lmsu; }
            set { _lmsu = value; OnPropertyChanged(nameof(lmsu)); }
        }
        private string _easu = "CheckBoxUnChecked.png";
        public string easu
        {
            get { return _easu; }
            set { _easu = value; OnPropertyChanged(nameof(easu)); }
        }
        private string _lasu = "CheckBoxUnChecked.png";
        public string lasu
        {
            get { return _lasu; }
            set { _lasu = value; OnPropertyChanged(nameof(lasu)); }
        }
        private string _eesu = "CheckBoxUnChecked.png";
        public string eesu
        {
            get { return _eesu; }
            set { _eesu = value; OnPropertyChanged(nameof(eesu)); }
        }
        private string _lesu = "CheckBoxUnChecked.png";
        public string lesu
        {
            get { return _lesu; }
            set { _lesu = value; OnPropertyChanged(nameof(lesu)); }
        }
        private string _onsu = "CheckBoxUnChecked.png";
        public string onsu
        {
            get { return _onsu; }
            set { _onsu = value; OnPropertyChanged(nameof(onsu)); }
        }
        private string _emsa = "CheckBoxUnChecked.png";
        public string emsa
        {
            get { return _emsa; }
            set { _emsa = value; OnPropertyChanged(nameof(emsa)); }
        }
        private string _lmsa = "CheckBoxUnChecked.png";
        public string lmsa
        {
            get { return _lmsa; }
            set { _lmsa = value; OnPropertyChanged(nameof(lmsa)); }
        }
        private string _easa = "CheckBoxUnChecked.png";
        public string easa
        {
            get { return _easa; }
            set { _easa = value; OnPropertyChanged(nameof(easa)); }
        }
        private string _lasa = "CheckBoxUnChecked.png";
        public string lasa
        {
            get { return _lasa; }
            set { _lasa = value; OnPropertyChanged(nameof(lasa)); }
        }
        private string _eesa = "CheckBoxUnChecked.png";
        public string eesa
        {
            get { return _eesa; }
            set { _eesa = value; OnPropertyChanged(nameof(eesa)); }
        }
        private string _lesa = "CheckBoxUnChecked.png";
        public string lesa
        {
            get { return _lesa; }
            set { _lesa = value; OnPropertyChanged(nameof(lesa)); }
        }
        private string _onsa = "CheckBoxUnChecked.png";
        public string onsa
        {
            get { return _onsa; }
            set { _onsa = value; OnPropertyChanged(nameof(onsa)); }
        }
        private string _emmo = "CheckBoxUnChecked.png";
        public string emmo
        {
            get { return _emmo; }
            set { _emmo = value; OnPropertyChanged(nameof(emmo)); }
        }
        private string _lmmo = "CheckBoxUnChecked.png";
        public string lmmo
        {
            get { return _lmmo; }
            set { _lmmo = value; OnPropertyChanged(nameof(lmmo)); }
        }
        private string _eamo = "CheckBoxUnChecked.png";
        public string eamo
        {
            get { return _eamo; }
            set { _eamo = value; OnPropertyChanged(nameof(eamo)); }
        }
        private string _lamo = "CheckBoxUnChecked.png";
        public string lamo
        {
            get { return _lamo; }
            set { _lamo = value; OnPropertyChanged(nameof(lamo)); }
        }
        private string _eemo = "CheckBoxUnChecked.png";
        public string eemo
        {
            get { return _eemo; }
            set { _eemo = value; OnPropertyChanged(nameof(eemo)); }
        }
        private string _lemo = "CheckBoxUnChecked.png";
        public string lemo
        {
            get { return _lemo; }
            set { _lemo = value; OnPropertyChanged(nameof(lemo)); }
        }
        private string _onmo = "CheckBoxUnChecked.png";
        public string onmo
        {
            get { return _onmo; }
            set { _onmo = value; OnPropertyChanged(nameof(onmo)); }
        }
        private string _emtu = "CheckBoxUnChecked.png";
        public string emtu
        {
            get { return _emtu; }
            set { _emtu = value; OnPropertyChanged(nameof(emtu)); }
        }
        private string _lmtu = "CheckBoxUnChecked.png";
        public string lmtu
        {
            get { return _lmtu; }
            set { _lmtu = value; OnPropertyChanged(nameof(lmtu)); }
        }
        private string _eatu = "CheckBoxUnChecked.png";
        public string eatu
        {
            get { return _eatu; }
            set { _eatu = value; OnPropertyChanged(nameof(eatu)); }
        }
        private string _latu = "CheckBoxUnChecked.png";
        public string latu
        {
            get { return _latu; }
            set { _latu = value; OnPropertyChanged(nameof(latu)); }
        }
        private string _eetu = "CheckBoxUnChecked.png";
        public string eetu
        {
            get { return _eetu; }
            set { _eetu = value; OnPropertyChanged(nameof(eetu)); }
        }
        private string _letu = "CheckBoxUnChecked.png";
        public string letu
        {
            get { return _letu; }
            set { _letu = value; OnPropertyChanged(nameof(letu)); }
        }
        private string _ontu = "CheckBoxUnChecked.png";
        public string ontu
        {
            get { return _ontu; }
            set { _ontu = value; OnPropertyChanged(nameof(ontu)); }
        }
        private string _emwe = "CheckBoxUnChecked.png";
        public string emwe
        {
            get { return _emwe; }
            set { _emwe = value; OnPropertyChanged(nameof(emwe)); }
        }
        private string _lmwe = "CheckBoxUnChecked.png";
        public string lmwe
        {
            get { return _lmwe; }
            set { _lmwe = value; OnPropertyChanged(nameof(lmwe)); }
        }
        private string _eawe = "CheckBoxUnChecked.png";
        public string eawe
        {
            get { return _eawe; }
            set { _eawe = value; OnPropertyChanged(nameof(eawe)); }
        }
        private string _lawe = "CheckBoxUnChecked.png";
        public string lawe
        {
            get { return _lawe; }
            set { _lawe = value; OnPropertyChanged(nameof(lawe)); }
        }
        private string _eewe = "CheckBoxUnChecked.png";
        public string eewe
        {
            get { return _eewe; }
            set { _eewe = value; OnPropertyChanged(nameof(eewe)); }
        }
        private string _lewe = "CheckBoxUnChecked.png";
        public string lewe
        {
            get { return _lewe; }
            set { _lewe = value; OnPropertyChanged(nameof(lewe)); }
        }
        private string _onwe = "CheckBoxUnChecked.png";
        public string onwe
        {
            get { return _onwe; }
            set { _onwe = value; OnPropertyChanged(nameof(onwe)); }
        }
        private string _emth = "CheckBoxUnChecked.png";
        public string emth
        {
            get { return _emth; }
            set { _emth = value; OnPropertyChanged(nameof(emth)); }
        }
        private string _lmth = "CheckBoxUnChecked.png";
        public string lmth
        {
            get { return _lmth; }
            set { _lmth = value; OnPropertyChanged(nameof(lmth)); }
        }
        private string _eath = "CheckBoxUnChecked.png";
        public string eath
        {
            get { return _eath; }
            set { _eath = value; OnPropertyChanged(nameof(eath)); }
        }
        private string _lath = "CheckBoxUnChecked.png";
        public string lath
        {
            get { return _lath; }
            set { _lath = value; OnPropertyChanged(nameof(lath)); }
        }
        private string _eeth = "CheckBoxUnChecked.png";
        public string eeth
        {
            get { return _eeth; }
            set { _eeth = value; OnPropertyChanged(nameof(eeth)); }
        }
        private string _leth = "CheckBoxUnChecked.png";
        public string leth
        {
            get { return _leth; }
            set { _leth = value; OnPropertyChanged(nameof(leth)); }
        }
        private string _onth = "CheckBoxUnChecked.png";
        public string onth
        {
            get { return _onth; }
            set { _onth = value; OnPropertyChanged(nameof(onth)); }
        }
        private string _emfr = "CheckBoxUnChecked.png";
        public string emfr
        {
            get { return _emfr; }
            set { _emfr = value; OnPropertyChanged(nameof(emfr)); }
        }
        private string _lmfr = "CheckBoxUnChecked.png";
        public string lmfr
        {
            get { return _lmfr; }
            set { _lmfr = value; OnPropertyChanged(nameof(lmfr)); }
        }
        private string _eafr = "CheckBoxUnChecked.png";
        public string eafr
        {
            get { return _eafr; }
            set { _eafr = value; OnPropertyChanged(nameof(eafr)); }
        }
        private string _lafr = "CheckBoxUnChecked.png";
        public string lafr
        {
            get { return _lafr; }
            set { _lafr = value; OnPropertyChanged(nameof(lafr)); }
        }
        private string _eefr = "CheckBoxUnChecked.png";
        public string eefr
        {
            get { return _eefr; }
            set { _eefr = value; OnPropertyChanged(nameof(eefr)); }
        }
        private string _lefr = "CheckBoxUnChecked.png";
        public string lefr
        {
            get { return _lefr; }
            set { _lefr = value; OnPropertyChanged(nameof(lefr)); }
        }
        private string _onfr = "CheckBoxUnChecked.png";
        public string onfr
        {
            get { return _onfr; }
            set { _onfr = value; OnPropertyChanged(nameof(onfr)); }
        }
        private bool _availabilityframelayout = false;
        public bool availabilityframelayout
        {
            get { return _availabilityframelayout; }
            set { _availabilityframelayout = value;OnPropertyChanged(nameof(availabilityframelayout)); }
        }
        private bool _langaugelayout = false;
        public bool langaugelayout
        {
            get { return _langaugelayout; }
            set { _langaugelayout = value; OnPropertyChanged(nameof(langaugelayout)); }
        }
        private string _premiumid = "";
        public string premiumad
        {
            get { return _premiumid; }
            set { _premiumid = value; OnPropertyChanged(nameof(premiumad)); }
        }
        private string _formadurl = "";
        public string formadurl
        {
            get { return _formadurl; }
            set { _formadurl = value; OnPropertyChanged(nameof(formadurl)); }
        }
        private bool _virtualsittinglayout = false;
        public bool virtualsittinglayout
        {
            get { return _virtualsittinglayout; }
            set { _virtualsittinglayout = value; OnPropertyChanged(nameof(virtualsittinglayout)); }
        }
        private bool _photourllayout = false;
        public bool photourllayout
        {
            get { return _photourllayout; }
            set { _photourllayout = value; OnPropertyChanged(nameof(photourllayout)); }
        }
        private bool _videourllayout = false;
        public bool videourllayout
        {
            get { return _videourllayout; }
            set { _videourllayout = value; OnPropertyChanged(nameof(videourllayout)); }
        }
        private bool _htmldescvisible = false;
        public bool htmldescvisible
        {
            get { return _htmldescvisible; }
            set { _htmldescvisible = value; OnPropertyChanged(nameof(htmldescvisible)); }
        }
        private bool _availabilitytiminglayout = false;
        public bool availabilitytiminglayout
        {
            get { return _availabilitytiminglayout; }
            set { _availabilitytiminglayout = value; OnPropertyChanged(nameof(availabilitytiminglayout)); }
        }
        private bool _gendervisble = false;
        public bool gendervisble
        {
            get { return _gendervisble; }
            set { _gendervisble = value; OnPropertyChanged(nameof(gendervisble)); }
        }
        private bool _salaryvisible = false;
        public bool salaryvisible
        {
            get { return _salaryvisible; }
            set { _salaryvisible = value; OnPropertyChanged(nameof(salaryvisible)); }
        }
        private bool _certificationvisible = true;
        public bool certificationvisible
        {
            get { return _certificationvisible; }
            set { _certificationvisible = value; OnPropertyChanged(nameof(certificationvisible)); }
        }
        
        private bool _fromdatevisible = false;
        public bool fromdatevisible
        {
            get { return _fromdatevisible; }
            set { _fromdatevisible = value; OnPropertyChanged(nameof(fromdatevisible)); }
        }
        private bool _servicetagsvisible = false;
        public bool servicetagsvisible
        {
            get { return _servicetagsvisible; }
            set { _servicetagsvisible = value; OnPropertyChanged(nameof(servicetagsvisible)); }
        }
        private bool _agegrouplistvisible = false;
        public bool agegrouplistvisible
        {
            get { return _agegrouplistvisible; }
            set { _agegrouplistvisible = value; OnPropertyChanged(nameof(agegrouplistvisible)); }
        }
        private bool _additionalservicelistvisible = false;
        public bool additionalservicelistvisible
        {
            get { return _additionalservicelistvisible; }
            set { _additionalservicelistvisible = value; OnPropertyChanged(nameof(additionalservicelistvisible)); }
        }
        //private bool _detailedreviewvisible = false;
        //public bool detailedreviewvisible
        //{
        //    get { return _detailedreviewvisible; }
        //    set { _detailedreviewvisible = value; OnPropertyChanged(nameof(detailedreviewvisible)); }
        //}

        public string _workrating1 = "Star_inactive.png";
        public string workrating1
        {
            get { return _workrating1; }
            set
            {
                _workrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating1)));

            }
        }
        public string _workrating2 = "Star_inactive.png";
        public string workrating2
        {
            get { return _workrating2; }
            set
            {
                _workrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating2)));

            }
        }
        public string _workrating3 = "Star_inactive.png";
        public string workrating3
        {
            get { return _workrating3; }
            set
            {
                _workrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating3)));

            }
        }
        public string _workrating4 = "Star_inactive.png";
        public string workrating4
        {
            get { return _workrating4; }
            set
            {
                _workrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating4)));

            }
        }
        public string _workrating5 = "Star_inactive.png";
        public string workrating5
        {
            get { return _workrating5; }
            set
            {
                _workrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating5)));

            }
        }
        private string _workrating;
        public string workrating
        {
            get { return _workrating; }
            set { _workrating = value; OnPropertyChanged(nameof(workrating)); }
        }
        private string _valuerating;
        public string valuerating
        {
            get { return _valuerating; }
            set { _valuerating = value; OnPropertyChanged(nameof(valuerating)); }
        }

        public string _valuerating1 = "Star_inactive.png";
        public string valuerating1
        {
            get { return _valuerating1; }
            set
            {
                _valuerating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(valuerating1)));

            }
        }
        public string _valuerating2 = "Star_inactive.png";
        public string valuerating2
        {
            get { return _valuerating2; }
            set
            {
                _valuerating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(valuerating2)));

            }
        }
        public string _valuerating3 = "Star_inactive.png";
        public string valuerating3
        {
            get { return _valuerating3; }
            set
            {
                _valuerating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(valuerating3)));

            }
        }
        public string _valuerating4 = "Star_inactive.png";
        public string valuerating4
        {
            get { return _valuerating4; }
            set
            {
                _valuerating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(valuerating4)));

            }
        }
        public string _valuerating5 = "Star_inactive.png";
        public string valuerating5
        {
            get { return _valuerating5; }
            set
            {
                _valuerating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(valuerating5)));

            }
        }

        private string _customersubrating;
        public string customersubrating
        {
            get { return _customersubrating; }
            set { _customersubrating = value; OnPropertyChanged(nameof(customersubrating)); }
        }

        public string _customersubrating1 = "Star_inactive.png";
        public string customersubrating1
        {
            get { return _customersubrating1; }
            set
            {
                _customersubrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(customersubrating1)));

            }
        }
        public string _customersubrating2 = "Star_inactive.png";
        public string customersubrating2
        {
            get { return _customersubrating2; }
            set
            {
                _customersubrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(customersubrating2)));

            }
        }
        public string _customersubrating3 = "Star_inactive.png";
        public string customersubrating3
        {
            get { return _customersubrating3; }
            set
            {
                _customersubrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(customersubrating3)));

            }
        }
        public string _customersubrating4 = "Star_inactive.png";
        public string customersubrating4
        {
            get { return _customersubrating4; }
            set
            {
                _customersubrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(customersubrating4)));

            }
        }
        public string _customersubrating5 = "Star_inactive.png";
        public string customersubrating5
        {
            get { return _customersubrating5; }
            set
            {
                _customersubrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(customersubrating5)));

            }
        }
        private HtmlWebViewSource _htmldescription;
        public HtmlWebViewSource htmldescription
        {
            get { return _htmldescription; }
            set { _htmldescription = value;OnPropertyChanged(nameof(htmldescription)); }
        }
        private string _folderid = "";
        public string folderid
        {
            get { return _folderid; }
            set { _folderid = value; OnPropertyChanged(nameof(folderid)); }
        }
        private string _photourl = "";
        public string photourl
        {
            get { return _photourl; }
            set { _photourl = value; OnPropertyChanged(nameof(photourl)); }
        }
        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
           // Items.Clear();

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {

                //for (int i = 0; i < 100; i++)
                //    Items.Add(DateTime.Now.AddMinutes(i).ToString("F"));

                IsBusy = false;

                // page.DisplayAlert("Refreshed", "You just refreshed the page! Nice job! Pull to refresh is now disabled", "OK");
                //this.CanRefresh = false;
                gotorefresh();
                return false;
            });
        }
        public CareDetailVM(string businesid,string Adurl,string premiumad)
        {
            
            businessid = businesid;
            formadurl = Adurl;
            sharecmd = new Command(TapOnShare);
            commentviewcmd = new Command<Rating_Details>(addreply);
            Customeroverallclickratingcommand = new Command(clickoverallrating);
            Workclickratingcommand = new Command(clickworkrating);
            valueclickratingcommand = new Command(clickvaluerating);
            customerclickratingcommand = new Command(clickcustomerrating);
            reportflagcmd = new Command(Taponreportflag);
            publishreview = new Command(publishreviewdetails);
            adsavecmd = new Command(TapOnAdtoSave);
            responsecmd = new Command(gotoresposeform);
            viewnumbercmd = new Command(gotoviewnumberform);
            photourlclickcommand = new Command<string>(photourlclick);
            videourlclickcommand = new Command<string>(videourlclick);
            gotofulldesc = new Command(gotohtmldescription);
           // RefreshCommand = new Command(gotorefresh);
            getcaredetails(businessid, Adurl);
        }
        public async void gotohtmldescription()
        {
            var currentpage = Getcurrentpage();
            await currentpage.Navigation.PushAsync(new Detail.Views.HtmlDescription(folderid,businessid));
        }
        public async void photourlclick(string photourl)
        {
            var currentpage = Getcurrentpage();
            await currentpage.Navigation.PushModalAsync(new Detail.Views.Imagedetail(photourl));

        }
        public void gotorefresh()
        {
            IsBusy = true;
            getcaredetails(businessid, formadurl);
            IsBusy = false;
        }
        public void videourlclick(string videourl)
        {
            //var currentpage = Getcurrentpage();
            //await currentpage.Navigation.PushAsync(new Detail.Views.Imagedetail());
            try
            {
                Device.OpenUri(new Uri(videourl));
            }
            catch(Exception ex)
            {
                dialogs.Toast("Invalid URL");
            }
        }
        public async void addreply(Rating_Details reviewsdtl)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = Getcurrentpage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                var currentpage = Getcurrentpage();
                await currentpage.Navigation.PushAsync(new ReplytoReview(reviewsdtl));
            }
        }
        DClistings data = new DClistings();
        public async void gotoresposeform()
        {
            dialogs.ShowLoading("", null);
            string email = Commonsettings.UserEmail;
            var currentpage = Getcurrentpage();
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                data.businessid = businessid;
                data.newclickurl = formadurl;
                data.premiumad = premiumad;
                data.primarycategoryid = primarycategoryid;
                data.maincategoryid = maincategoryid;
               // data.supercategoryid = supercategoryid;
                //OTP
                await currentpage.Navigation.PushAsync(new List.Views.ResponseForm(data, "response"));
            }
            dialogs.HideLoading();
        }
        public async void gotoviewnumberform()
        {
            var currentpage = Getcurrentpage();
            dialogs.ShowLoading("", null);
            string email = Commonsettings.UserEmail;
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                data.businessid = businessid;
                data.newclickurl = formadurl;
                data.premiumad = premiumad;
                if (viewnumcnt == 0)
                {
                    viewnumcnt = 1;
                    await currentpage.Navigation.PushAsync(new List.Views.ViewNumberForm(data, "viewnumber"));
                    viewnumcnt = 0;
                }
            }
            dialogs.HideLoading();
        }
        public async void Taponreportflag()
        {
            var currentpage = Getcurrentpage();
            //Device.BeginInvokeOnMainThread(async () => 
            await currentpage.Navigation.PushModalAsync(new Report_DC(businessid,adurl,businessname));
        }
        Add_Rating_Details ratingdata = new Add_Rating_Details();
        public async void publishreviewdetails()
        {
            try
            {
                dialogs.ShowLoading("", null);
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = Getcurrentpage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    if (Reviewvalidation())
                    {
                        ratingdata.pid = Commonsettings.UserPid;
                        ratingdata.contributor = Commonsettings.UserName;
                        ratingdata.shortdesc = reviewdesc;
                        ratingdata.objectid = businessid;
                        ratingdata.ipaddress = ipaddress;
                        ratingdata.city = Commonsettings.Usercity;
                        ratingdata.statename = Commonsettings.Userstate;
                        ratingdata.lat = Commonsettings.UserLat.ToString();
                        ratingdata.@long = Commonsettings.UserLong.ToString();
                        ratingdata.zipcode = Commonsettings.Userzipcode;
                        ratingdata.careerrating = valuerating;
                        ratingdata.workrating = workrating;
                        ratingdata.rating = rating;
                        ratingdata.managementrating = customersubrating;
                        var addreview = RestService.For<ICarecenter>(Commonsettings.DaycareAPI);
                        var addreviewdetails = await addreview.addreview(ratingdata);
                        if (addreviewdetails.value == "success")//success
                        {

                            CustomerRating1 = "Star_inactive.png";
                            CustomerRating2 = "Star_inactive.png";
                            CustomerRating3 = "Star_inactive.png";
                            CustomerRating4 = "Star_inactive.png";
                            CustomerRating5 = "Star_inactive.png";


                            //Careerrating1 = "Star_inactive.png";
                            //Careerrating2 = "Star_inactive.png";
                            //Careerrating3 = "Star_inactive.png";
                            //Careerrating4 = "Star_inactive.png";
                            //Careerrating5 = "Star_inactive.png";

                            //workrating1 = "Star_inactive.png";
                            //workrating2 = "Star_inactive.png";
                            //workrating3 = "Star_inactive.png";
                            //workrating4 = "Star_inactive.png";
                            //workrating5 = "Star_inactive.png";

                            //managementrating1 = "Star_inactive.png";
                            //managementrating2 = "Star_inactive.png";
                            //managementrating3 = "Star_inactive.png";
                            //managementrating4 = "Star_inactive.png";
                            //managementrating5 = "Star_inactive.png";

                            Ratingbar = "0/5";

                            dialogs.Toast("Review published successfully");
                            reviewdesc = "";
                            getreviewdetails();
                        }
                    }
                }
                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                string msg = ex.Message;
            }
        }
        
        public void TapOnShare()
        {
            //http://daycare.sulekha.com/new-york-ny/care-services/reliable-babysitter-nanny-available-106289
            try
            {
                //adurl = "http://daycare.sulekha.com/"+ cityurl+ "care-services"+businesstitleurl + "-" + businessid;
                CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
                {
                    Url = adurl
                });
            }
            catch(Exception ex)
            {
            }
        }
        public List<CareSaveAdList> AdSaveList { get; set; }
        public List<CareDeleteAdList> AdDeleteList { get; set; }
        public async void TapOnAdtoSave()
        {
            try
            {
                dialogs.ShowLoading("", null);
                string email = Commonsettings.UserEmail;
                var currentpage = Getcurrentpage();
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    if (adresult == "0")
                    {
                        AdSaveimg = "FavoriteYellow.png";
                        var savead = RestService.For<ICareDetail>(Commonsettings.DaycareAPI);
                        CareSaveAD adsaveresponse = await savead.SaveAdData(businessid, email, Commonsettings.UserPid);
                        AdSaveList = adsaveresponse.ROW_DATA;
                        adresult = "1";
                    }
                    else
                    {
                        AdSaveimg = "HeartGray.png";
                        var deletead = RestService.For<ICareDetail>(Commonsettings.DaycareAPI);
                        CareDeleteAD addeleteresponse = await deletead.DeleteAdData(businessid, Commonsettings.UserPid);
                        AdDeleteList = addeleteresponse.ROW_DATA;
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
        private string _maincategoryid = "";
        public string maincategoryid
        {
            get { return _maincategoryid; }
            set { _maincategoryid = value;OnPropertyChanged(nameof(maincategoryid)); }
        }
        private string _supercategoryid = "";
        public string supercategoryid
        {
            get { return _supercategoryid; }
            set { _supercategoryid = value; OnPropertyChanged(nameof(supercategoryid)); }
        }
        private string _primarycategoryid = "";
        public string primarycategoryid
        {
            get { return _primarycategoryid; }
            set { _primarycategoryid = value; OnPropertyChanged(nameof(primarycategoryid)); }
        }
        private string _Additionalservicetxt = "";
        public string Additionalservicetxt
        {
            get { return _Additionalservicetxt; }
            set { _Additionalservicetxt = value; OnPropertyChanged(nameof(Additionalservicetxt)); }
        }
        private string _servicetext = "";
        public string servicetext
        {
            get { return _servicetext; }
            set { _servicetext = value; OnPropertyChanged(nameof(servicetext)); }
        }
        private string _agegrouptext = "";
        public string agegrouptext
        {
            get { return _agegrouptext; }
            set { _agegrouptext = value; OnPropertyChanged(nameof(agegrouptext)); }
        }
        private bool _tertiarycatvalvisible = true;
        public bool tertiarycatvalvisible
        {
            get { return _tertiarycatvalvisible; }
            set { _tertiarycatvalvisible = value;OnPropertyChanged(nameof(tertiarycatvalvisible)); }
        }
        private bool _virtualtagvisible = false;
        public bool virtualtagvisible
        {
            get { return _virtualtagvisible; }
            set { _virtualtagvisible = value; OnPropertyChanged(nameof(virtualtagvisible)); }
        }
        private string _careexperiencetxt = "";
        public string careexperiencetxt
        {
            get { return _careexperiencetxt; }
            set { _careexperiencetxt = value; OnPropertyChanged(nameof(careexperiencetxt)); }
        }
        private bool _reviewvisible = true;
        public bool reviewvisible
        {
            get { return _reviewvisible; }
            set { _reviewvisible = value; OnPropertyChanged(nameof(reviewvisible)); }
        }
        private string _reponsebtntext = "Get Quote";
        public string reponsebtntext
        {
            get { return _reponsebtntext; }
            set { _reponsebtntext = value; OnPropertyChanged(nameof(reponsebtntext)); }
        }
        
        string allavailabilitylist = "emsu,lmsu,easu,lasu,eesu,lesu,onsu,emsa,lmsa,easa,lasa,eesa,lesa,onsa,emmo,lmmo,eamo,lamo,eemo,lemo,onmo,emtu,lmtu,eatu,latu,eetu,letu,ontu,emwe,lmwe,eawe,lawe,eewe,lewe,onwe,emth,lmth,eath,lath,eeth,leth,onth,emfr,lmfr,eafr,lafr,eefr,lefr,onfr";
        public async void getcaredetails(string businesid,string adurll)
        {
           try
            {
                dialogs.ShowLoading("",null);
                await Task.Delay(500);
                
                var CareDetailApi = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                var CareDetaildata = await CareDetailApi.getdetails(businesid, Commonsettings.UserPid);
                //106289, "24037117"
                //care-services
                //var CareDetaildata = await CareDetailApi.getdetails("253646", "24037117");
                maincategoryid = CareDetaildata.ROW_DATA[0].Maincategoryid;
               // supercategoryid = CareDetaildata.ROW_DATA[0].Supertagid;
                primarycategoryid = CareDetaildata.ROW_DATA[0].Primarycategoryid;
               
                //if (CareDetaildata.ROW_DATA[0].Secondarycategoryvalue == "Care Center" || CareDetaildata.ROW_DATA[0].Secondarycategoryvalue.ToLower().Contains("center"))
                //{
                //    // detailedreviewvisible = true;
                //    salaryvisible = false;
                //    certificationvisible = false;
                //}
                //else
                //{
                //    detailedreviewvisible = false;
                //}
                if (CareDetaildata!=null)
                {
                    businessid = CareDetaildata.ROW_DATA[0].Businessid;
                    //adurl = "http://daycare.sulekha.com/" + cityurl + "care-services" + businesstitleurl + "-" + businessid;
                    adurl = "http://daycare.sulekha.com/" + adurll;
                    businessname = CareDetaildata.ROW_DATA[0].Businessname;
                    tertiarycatvalue = CareDetaildata.ROW_DATA[0].Tertiarycategoryvalue;
                    if(string.IsNullOrEmpty(tertiarycatvalue))
                    {
                        tertiarycatvalvisible = false;
                    }
                    if(!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Virtualnanny))
                    {
                        if(CareDetaildata.ROW_DATA[0].Virtualnanny!="0")
                        {
                            virtualtagvisible = true;
                        }
                    }
                    lastrenewdate = CareDetaildata.ROW_DATA[0].Lastreneweddate;
                    businessaddress = CareDetaildata.ROW_DATA[0].Businessaddress;
                    description = CareDetaildata.ROW_DATA[0].Description;
                    descheadertxt= CareDetaildata.ROW_DATA[0].Contactname+"'s Note" ;
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Ismobileverified))
                    {
                        if(CareDetaildata.ROW_DATA[0].Ismobileverified=="1")
                        {
                            mobileverified = true;
                        }
                    }
                    if(CareDetaildata.ROW_DATA[0].Supercategoryvalue== "I need Care")
                    {
                        Additionalservicetxt = "Additional Services Required";
                        servicetext = "Responsibilities";
                        agegrouptext = "Child age group";
                        if(CareDetaildata.ROW_DATA[0].Primarycategoryvalue== "Pet Care")
                        {
                            servicetext = "Pet Care Needs";
                        }
                        reviewvisible = false;
                        reponsebtntext = "Contact Now";
                    }
                    else
                    {
                        reviewvisible = true;
                        reponsebtntext = "Get Quote";
                        Additionalservicetxt = "Additional Services Provided";
                        servicetext = "Services Offered";
                        agegrouptext = "Age Group Handled";
                        if (CareDetaildata.ROW_DATA[0].Primarycategoryvalue == "Pet Care")
                        {
                            servicetext = "Pet Care Provider";
                        }
                    }
                   
                    if(!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Careexperience))
                    {
                        experiencevisible = true;
                        experience = CareDetaildata.ROW_DATA[0].Careexperience.Replace("<answers><answer>", "").Replace("</answer></answers>", "");
                        if (CareDetaildata.ROW_DATA[0].Secondarycategoryvalue == "Care Center" || CareDetaildata.ROW_DATA[0].Secondarycategoryvalue == "Elder Care" || CareDetaildata.ROW_DATA[0].Secondarycategoryvalue == "Child Care Center" || CareDetaildata.ROW_DATA[0].Secondarycategoryvalue == "Pet Care Center" || CareDetaildata.ROW_DATA[0].Secondarycategoryvalue == "Elder Care Center" )
                        {
                            careexperiencetxt = "Established Since";
                        }
                        else
                        {
                            careexperiencetxt = "Experience";
                            if (experience == "0" || experience.ToLower() == "beginner")
                            {
                                experience = "Beginner";
                            }
                            else if (experience == "1")
                            {
                                experience = experience + " Year";
                            }
                            else
                            {
                                experience = experience + " Years";
                            }
                        }
                       
                    }
                    if (CareDetaildata.ROW_DATA[0].Issaved == "0")
                    {
                        AdSaveimg = "HeartGray.png";
                        adresult = "0";
                    }
                    if (CareDetaildata.ROW_DATA[0].Issaved == "1")
                    {
                        AdSaveimg = "FavoriteYellow.png";
                        adresult = "1";
                    }

                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Salaryrate.ToString())&& !string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Salarymode))
                    {
                        salaryvisible = true;
                        salary = Math.Round(CareDetaildata.ROW_DATA[0].Salaryrate, 1) + " /" + CareDetaildata.ROW_DATA[0].Salarymode;
                    }
                    if(!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Gender.Replace("<answers><answer>", "").Replace("</answer></answers>", "")))
                    {
                        gendervisble = true;
                        gender = CareDetaildata.ROW_DATA[0].Gender.Replace("<answers><answer>", "").Replace("</answer></answers>", "");
                    }
                    if(!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].certification))
                    {
                        if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].certification.Replace(",<answers><answer>", "").Replace("<answers><answer>", "").Replace("</answer></answers>", "").Replace("</answer><answers>", ",")))
                        {
                            certification = CareDetaildata.ROW_DATA[0].certification.Replace(",<answers><answer>", "").Replace("</answer><answer>",",").Replace("<answers><answer>", "").Replace("</answer></answers>", "").Replace("</answer><answers>", ",");
                        }
                        else
                        {
                            certification = "No Certification";
                        }
                    }
                    else
                    {
                        certification = "No Certification";
                    }

                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Needfrom))
                    {
                        fromdatevisible = true;
                        fromdate = "From : " + CareDetaildata.ROW_DATA[0].Needfrom;
                    }
                    
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].servicetags))
                    {
                        servicetagsvisible = true;
                        servicetaglist = new List<string>();
                        string[] servicetagarr = CareDetaildata.ROW_DATA[0].servicetags.Split(',');
                        //foreach (var servicetagdta in servicetagarr)
                        //{
                        //    if (!string.IsNullOrEmpty(servicetagdta))
                        //    {
                        //        servicetaglist.Add(servicetagdta);
                        //    }
                        //}
                        servicetaglist = servicetagarr.ToList();
                        int height = servicetagarr.Count() * 20;
                        getservicetagheightrequest(height);
                    }
                    if (CareDetaildata.ROW_DATA[0].Primarycategoryvalue == "Pet Care")
                    {
                        if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Primarytag1))
                        {
                            servicetagsvisible = true;
                            servicetaglist = new List<string>();
                            string[] servicetagarr = CareDetaildata.ROW_DATA[0].Primarytag1.Split(',');
                            //foreach (var servicetagdta in servicetagarr)
                            //{
                            //    if (!string.IsNullOrEmpty(servicetagdta))
                            //    {
                            //        servicetaglist.Add(servicetagdta);
                            //    }
                            //}
                            servicetaglist = servicetagarr.ToList();
                            int height = servicetagarr.Count() * 20;
                            getservicetagheightrequest(height);
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Agegroup))
                    {
                        agegrouplistvisible = true;
                        agegrouplist = new List<string>();
                        string[] agegrouptagarr = CareDetaildata.ROW_DATA[0].Agegroup.Split(',');
                        //foreach (var sagegroupdta in agegrouptagarr)
                        //{
                        //    if (!string.IsNullOrEmpty(sagegroupdta))
                        //    {
                        //        agegrouplist.Add(sagegroupdta);
                        //    }
                        //}
                        agegrouplist = agegrouptagarr.ToList();
                        int height = agegrouptagarr.Count() * 20;
                        getagegroupheightrequest(height);
                    }

                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].additionalservice) )
                     {
                        string additionalservice = CareDetaildata.ROW_DATA[0].additionalservice.Replace("<answers><answer>", "").Replace("</answer><answer>", ",").Replace("</answer></answers>", "");
                        if(!string.IsNullOrEmpty(additionalservice))
                        {
                            additionalservicelistvisible = true;
                            additionalservicelist = new List<string>();
                            string[] additionalarr = additionalservice.Split(',');
                            //foreach (var additionalgrpdta in additionalarr)
                            //{
                            //    if (!string.IsNullOrEmpty(additionalgrpdta))
                            //    {
                            //        // string agegroupdata = agegrpdta.Substring(0, agegrpdta.LastIndexOf("::") + 0);
                            //        additionalservicelist.Add(additionalgrpdta);
                            //    }
                            //}
                            additionalservicelist = additionalarr.ToList();
                            int height = additionalarr.Count() * 20;
                            getadditionalserviceheightrequest(height);
                        }
                        //<answers><answer>Light Cooking</answer><answer>Light Housekeeping</answer></answers>
                      
                    }
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Language))
                    {
                        langaugelayout = true;
                        languagelist = new List<string>();
                        string ldata = CareDetaildata.ROW_DATA[0].Language.Replace(".", "").Replace("addmore-", "");
                        string[] languagegrouparr = ldata.Split(',');
                        //foreach (var languagegrpdta in languagegrouparr)
                        //{
                        //    if (!string.IsNullOrEmpty(languagegrpdta))
                        //    {
                        //        string languagegroupdata = languagegrpdta;
                        //        languagelist.Add(languagegroupdata);
                        //    }
                        //}
                        languagelist = languagegrouparr.ToList();
                        int height = languagegrouparr.Count() * 20;
                        getlanguageheightrequest(height);
                    }
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Virtualnannyplatform))
                    {
                        virtualsittinglayout = true;
                        virtualoptionlist = new List<string>();
                        string ldata = CareDetaildata.ROW_DATA[0].Virtualnannyplatform;
                        string[] virtualoptionparr = ldata.Split(',');
                        //foreach (var virtualoptiondta in virtualoptionparr)
                        //{
                        //    if (!string.IsNullOrEmpty(virtualoptiondta))
                        //    {
                        //        string virtualoptiondata = virtualoptiondta;
                        //        virtualoptionlist.Add(virtualoptiondata);
                        //    }
                        //}
                        virtualoptionlist = virtualoptionparr.ToList();
                        int height = virtualoptionparr.Count() * 20;
                        getvirtualoptionheightrequest(height);
                    }
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Availability))
                    {
                       
                        bool result = false;
                        string[] availablityarr = CareDetaildata.ROW_DATA[0].Availability.Split(',');
                        foreach (var availabletimedta in availablityarr)
                        {
                            if (allavailabilitylist.Contains(availabletimedta))
                            {
                                result = true;
                            }
                        }
                        if (result == true)
                        {
                            availabilityframelayout = true;
                            getavailability(CareDetaildata.ROW_DATA[0].Availability);
                        }
                        else
                        {
                           try
                            {
                                availabilityframelayout = false;
                                availabilitytiminglayout = true;
                                availabletiminglist = new List<string>();
                                string ldata = CareDetaildata.ROW_DATA[0].Availability;
                                string[] availabletimingarr = ldata.Split(',');
                                foreach (var availabletimedta in availabletimingarr)
                                {
                                    if (!string.IsNullOrEmpty(availabletimedta))
                                    {
                                        string availabletimedata = availabletimedta.Replace("::", " - ").Replace("Mon", "Monday").Replace("Tue", "Tuesday").Replace("Wed", "Wednesday").Replace("Thu", "Thursday").Replace("Fri", "Friday").Replace("Sat", "Saturday").Replace("Sun", "Sunday");
                                        availabletiminglist.Add(availabletimedata);
                                    }
                                }
                                int height = availabletimingarr.Count() * 20;
                                getavailabletimeheightrequest(height);
                            }
                            catch(Exception ex)
                            {

                            }
                        }

                    }

                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Photourl))
                    {
                       
                            photourllayout = true;
                            photourllist = new List<string>();
                            string ldata = CareDetaildata.ROW_DATA[0].Photourl;
                            string[] photourlarr = ldata.Split(';');
                            //foreach (var photolistdta in photourlarr)
                            //{
                            //    if (!string.IsNullOrEmpty(photolistdta))
                            //    {
                            //        string photourlata = photolistdta;
                            //        photourllist.Add(photourlata);
                            //    }
                            //}
                            photourllist = photourlarr.ToList();
                        //int height = virtualoptionparr.Count() * 20;
                        //getvirtualoptionheightrequest(height);
                        await Task.Delay(500);
                        
                    }
                    //string pattern = @"/(ftp|http|https):\/\/?((www|\w\w)\.)?youtube.com(\w+:{0,1}\w*@)?(\S+)(:([0-9])+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/";
                    string pattern = @"^(https?\:\/\/)?((www\.)?youtube\.com|youtu\.?be)\/.+$";
                    //CareDetaildata.ROW_DATA[0].Videourl = "https://www.youtube.com/watch?v=rLTFWCFz-kw";
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Videourl) && (Regex.IsMatch(CareDetaildata.ROW_DATA[0].Videourl, pattern)))
                    {
                       try
                        {
                            //string url = CareDetaildata.ROW_DATA[0].Videourl;
                            //url = "https://www.youtube.com/watch?v=rLTFWCFz-kw";
                            //Uri uri = new Uri(url);
                            //WebRequest request = HttpWebRequest.Create(uri);
                            //WebResponse response = request.GetResponse();
                            //StreamReader reader = new StreamReader(response.GetResponseStream());
                            //string result = reader.ReadToEnd();
                            //if (!string.IsNullOrEmpty(result))
                            //{
                            //    videourllayout = true;
                            //}
                            //else
                            //{
                            //    videourllayout = false;
                            //}
                            videourllayout = true;
                            videourllist = new List<string>();
                            string ldata = CareDetaildata.ROW_DATA[0].Videourl;
                            string[] videourlarr = ldata.Split(',');
                            foreach (var videolistdta in videourlarr)
                            {
                                if (!string.IsNullOrEmpty(videolistdta))
                                {
                                    string videourlata = videolistdta;
                                    videourllist.Add(videourlata);
                                }
                            }
                            //int height = virtualoptionparr.Count() * 20;
                            //getvirtualoptionheightrequest(height);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                   
                    if (!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Htmlurl) || !string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Folderid))
                    {
                       
                        try
                        {
                            string url = "http://daycare.sulekha.com/classifiedshtmls/" + CareDetaildata.ROW_DATA[0].Folderid + "/" + businesid + ".html";
                            Uri uri = new Uri(url);
                            WebRequest request = HttpWebRequest.Create(uri);
                            WebResponse response = request.GetResponse();
                            StreamReader reader = new StreamReader(response.GetResponseStream());
                            string result = reader.ReadToEnd();
                            if(!string.IsNullOrEmpty(result))
                            {
                                folderid = CareDetaildata.ROW_DATA[0].Folderid;
                                htmldescvisible = true;
                            }
                        }
                        catch(Exception ex)
                        {
                            htmldescvisible = false;
                        }
                       
                    }

                    if(!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Secondarycategoryvalue))
                    {
                        if (CareDetaildata.ROW_DATA[0].Secondarycategoryvalue == "Care Center" || CareDetaildata.ROW_DATA[0].Secondarycategoryvalue.ToLower().Contains("center"))
                        {
                            // detailedreviewvisible = true;
                            salaryvisible = false;
                            certificationvisible = false;
                        }
                    }
                }
                if(!string.IsNullOrEmpty(CareDetaildata.ROW_DATA[0].Photourl))
                {
                    photourl = CareDetaildata.ROW_DATA[0].Photourl;
                }
                getreviewdetails();
                // getavailability(CareDetaildata.ROW_DATA[0].Availability);
                IsBusy = false;
                dialogs.HideLoading();
               // IsBusy = false;
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
                IsBusy = false;
            }
            IsBusy = false;
        }
        
        public void getavailability(string availability)
        {
            try
            {
                string[] availabilityarr = availability.Split(',');
                var currentpage = Getcurrentpage();
                StackLayout availabilitylayout = currentpage.FindByName<StackLayout>("availabilitylayout");
                Grid gridlayout = currentpage.FindByName<Grid>("gridlayout");
                Image img = new Image();
                foreach (var data in availabilityarr)
                {
                    if(allavailabilitylist.Contains(data))
                    {
                        if (data == "emsu")
                        {
                            emsu = "CheckBoxChecked.png";
                        }
                        else if(data== "lmsu")
                        {
                            lmsu = "CheckBoxChecked.png";
                        }
                        else if (data == "easu")
                        {
                            easu = "CheckBoxChecked.png";
                        }
                        else if (data == "lasu")
                        {
                            lasu = "CheckBoxChecked.png";
                        }
                        else if (data == "eesu")
                        {
                            eesu = "CheckBoxChecked.png";
                        }
                        else if (data == "lesu")
                        {
                            lesu = "CheckBoxChecked.png";
                        }
                        else if (data == "onsu")
                        {
                            onsu = "CheckBoxChecked.png";
                        }
                        else if (data == "emsa")
                        {
                            emsa = "CheckBoxChecked.png";
                        }
                        else if (data == "lmsa")
                        {
                            lmsa = "CheckBoxChecked.png";
                        }
                        else if (data == "easa")
                        {
                            easa = "CheckBoxChecked.png";
                        }
                        else if (data == "lasa")
                        {
                            lasa = "CheckBoxChecked.png";
                        }
                        else if (data == "eesa")
                        {
                            eesa = "CheckBoxChecked.png";
                        }
                        else if (data == "lesa")
                        {
                            lesa = "CheckBoxChecked.png";
                        }
                        else if (data == "onsa")
                        {
                            onsa = "CheckBoxChecked.png";
                        }
                        else if (data == "emmo")
                        {
                            emmo = "CheckBoxChecked.png";
                        }
                        else if (data == "lmmo")
                        {
                            lmmo = "CheckBoxChecked.png";
                        }
                        else if (data == "eamo")
                        {
                            eamo = "CheckBoxChecked.png";
                        }
                        else if (data == "eemo")
                        {
                            eemo = "CheckBoxChecked.png";
                        }
                        else if (data == "lemo")
                        {
                            lemo = "CheckBoxChecked.png";
                        }
                        else if (data == "onmo")
                        {
                            onmo = "CheckBoxChecked.png";
                        }
                        else if (data == "emtu")
                        {
                            emtu = "CheckBoxChecked.png";
                        }
                        else if (data == "lmtu")
                        {
                            lmtu = "CheckBoxChecked.png";
                        }
                        else if (data == "eatu")
                        {
                            eatu = "CheckBoxChecked.png";
                        }
                        else if (data == "eetu")
                        {
                            eetu = "CheckBoxChecked.png";
                        }
                        else if (data == "letu")
                        {
                            letu = "CheckBoxChecked.png";
                        }
                        else if (data == "ontu")
                        {
                            ontu = "CheckBoxChecked.png";
                        }
                        else if (data == "emwe")
                        {
                            emwe = "CheckBoxChecked.png";
                        }
                        else if (data == "lmwe")
                        {
                            lmwe = "CheckBoxChecked.png";
                        }
                        else if (data == "eawe")
                        {
                            eawe = "CheckBoxChecked.png";
                        }
                        else if (data == "lawe")
                        {
                            lawe = "CheckBoxChecked.png";
                        }
                        else if (data == "eewe")
                        {
                            eewe = "CheckBoxChecked.png";
                        }
                        else if (data == "lewe")
                        {
                            lewe = "CheckBoxChecked.png";
                        }
                        else if (data == "onwe")
                        {
                            onwe = "CheckBoxChecked.png";
                        }
                        else if (data == "emth")
                        {
                            emth = "CheckBoxChecked.png";
                        }
                        else if (data == "lmth")
                        {
                            lmth = "CheckBoxChecked.png";
                        }
                        else if (data == "eath")
                        {
                            eath = "CheckBoxChecked.png";
                        }
                        else if (data == "lath")
                        {
                            lath = "CheckBoxChecked.png";
                        }
                        else if (data == "eeth")
                        {
                            eeth = "CheckBoxChecked.png";
                        }
                        else if (data == "leth")
                        {
                            leth = "CheckBoxChecked.png";
                        }
                        else if (data == "onth")
                        {
                            onth = "CheckBoxChecked.png";
                        }
                        else if (data == "emfr")
                        {
                            emfr = "CheckBoxChecked.png";
                        }
                        else if (data == "lmfr")
                        {
                            lmfr = "CheckBoxChecked.png";
                        }
                        else if (data == "eafr")
                        {
                            eafr = "CheckBoxChecked.png";
                        }
                        else if (data == "lafr")
                        {
                            lafr = "CheckBoxChecked.png";
                        }
                        else if (data == "eefr")
                        {
                            eefr = "CheckBoxChecked.png";
                        }
                        else if (data == "lefr")
                        {
                            lefr = "CheckBoxChecked.png";
                        }
                        else if (data == "onfr")
                        {
                            onfr = "CheckBoxChecked.png";
                        }
                        else if (data == "lamo")
                        {
                            lamo = "CheckBoxChecked.png";
                        }
                        else if (data == "latu")
                        {
                            latu = "CheckBoxChecked.png";
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public async void getadditionalserviceheightrequest(int height)
        {
            try
            {
                await Task.Delay(500);
                var currentpage = Getcurrentpage();
                Helpers.ListviewScrollbar additionalservicelst = currentpage.FindByName<Helpers.ListviewScrollbar>("additionalservicelist");
                additionalservicelst.HeightRequest = height;
                if(additionalservicelst.HeightRequest>100)
                {
                    additionalservicelst.HeightRequest = height + 10;
                }
            }
            catch(Exception ex)
            {

            }
        }
        public  void getavailabletimeheightrequest(int height)
        {
            try
            {
                //await Task.Delay(500);
                var currentpage = Getcurrentpage();
                Helpers.ListviewScrollbar availabletiminglist = currentpage.FindByName<Helpers.ListviewScrollbar>("availabletiminglist");
                availabletiminglist.HeightRequest = height;
                if (availabletiminglist.HeightRequest > 100)
                {
                    availabletiminglist.HeightRequest = height + 10;
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async void getservicetagheightrequest(int height)
        {
            try
            {
                await Task.Delay(500);
                var currentpage = Getcurrentpage();
                //Helpers.ListviewScrollbar servicetaglist = currentpage.FindByName<Helpers.ListviewScrollbar>("servicetaglist");
                HVScrollGridView servicetaglist = currentpage.FindByName<HVScrollGridView>("servicetaglist");
                servicetaglist.HeightRequest = height;
                if (servicetaglist.HeightRequest > 100)
                {
                    servicetaglist.HeightRequest = height + 10;
                }
            }
            catch(Exception ex)
            {
            }
        }
        public async void getlanguageheightrequest(int height)
        {
            try
            {
                await Task.Delay(500);
                var currentpage = Getcurrentpage();
                Helpers.ListviewScrollbar languagelist = currentpage.FindByName<Helpers.ListviewScrollbar>("languagelist");
                languagelist.HeightRequest = height;
                if (languagelist.HeightRequest > 100)
                {
                    languagelist.HeightRequest = height + 10;
                }
            }
            catch (Exception ex)
            {

            }
           
        }
        public async void getagegroupheightrequest(int height)
        {
            try
            {
                await Task.Delay(500);
                var currentpage = Getcurrentpage();
                Helpers.ListviewScrollbar agegrouplayout = currentpage.FindByName<Helpers.ListviewScrollbar>("agegrouplist");
                agegrouplayout.HeightRequest = height;
                if (agegrouplayout.HeightRequest > 100)
                {
                    agegrouplayout.HeightRequest = height + 10;
                }
            }
            catch(Exception ex)
            {

            }
           
        }
        public async void getvirtualoptionheightrequest(int height)
        {
            try
            {
                await Task.Delay(500);
                var currentpage = Getcurrentpage();
                Helpers.ListviewScrollbar virtualoptionlayout = currentpage.FindByName<Helpers.ListviewScrollbar>("virtualoptionlist");
                virtualoptionlayout.HeightRequest = height;
            }
            catch (Exception ex)
            {

            }
           
        }
        public async void getreviewdetails()
        {
            try
            {
                dialogs.ShowLoading("", null);
                var getreviewapi = RestService.For<ICarecenter>(Commonsettings.DaycareAPI);
                var getreviewdetails = await getreviewapi.getreviewdetails(businessid);
                if (getreviewdetails != null && getreviewdetails.ROW_DATA.Count > 0)
                {
                    reviewcommentsvisible = true;
                    foreach (var item in getreviewdetails.ROW_DATA)
                    {
                        if (string.IsNullOrEmpty(item.photourl))
                        {
                            item.imgtext = item.contributor.Substring(0, 1).ToUpper();
                            item.imgvisible = false;
                            item.imgnovisible = true;
                        }
                        else
                        {
                            item.imgnovisible = false;
                            item.imgvisible = true;
                        }
                    }
                    getreviewdetail = getreviewdetails.ROW_DATA;
                    dialogs.HideLoading();
                }
                else
                {
                    reviewcommentsvisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public bool Reviewvalidation()
        {
            bool result = true;
            var currentpage = Getcurrentpage();
            if (string.IsNullOrEmpty(rating))
            {
                dialogs.Toast("Please select your rating");
                return false;
            }
            if (string.IsNullOrEmpty(reviewdesc) || reviewdesc.Trim().Length == 0)
            {
                dialogs.Toast("Description cannot be empty");
                return false;
            }
            if (!string.IsNullOrEmpty(reviewdesc))
            {
                if (reviewdesc.Length > 1500)
                {
                    dialogs.Toast("Description must be between 1500 characters");
                    return false;
                }
            }

            return result;
        }
        private void clickoverallrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                CustomerRating1 = "StarActive.png";
                CustomerRating2 = "Star_inactive.png";
                CustomerRating3 = "Star_inactive.png";
                CustomerRating4 = "Star_inactive.png";
                CustomerRating5 = "Star_inactive.png";
                rating = "1";
                Ratingbar = "1/5";


            }
            else if (id == "2")
            {
                if (CustomerRating2 == "StarActive.png")
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "Star_inactive.png";
                    CustomerRating3 = "Star_inactive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "1";
                    Ratingbar = "1/5";
                }
                else
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "Star_inactive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "2";
                    Ratingbar = "2/5";
                }

            }
            else if (id == "3")
            {
                if (CustomerRating3 == "StarActive.png")
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "Star_inactive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "2";
                    Ratingbar = "2/5";
                }
                else
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "3";
                    Ratingbar = "3/5";
                }

            }
            else if (id == "4")
            {
                if (CustomerRating4 == "StarActive.png")
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "3";
                    Ratingbar = "3/5";
                }
                else
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "StarActive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "4";
                    Ratingbar = "4/5";
                }

            }
            else if (id == "5")
            {
                if (CustomerRating5 == "StarActive.png")
                {

                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "StarActive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "4";
                    Ratingbar = "4/5";
                }
                else
                {

                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "StarActive.png";
                    CustomerRating5 = "StarActive.png";
                    rating = "5";
                    Ratingbar = "5/5";
                }
            }
            rating = id;
        }

        private void clickworkrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                workrating1 = "StarActive.png";
                workrating2 = "Star_inactive.png";
                workrating3 = "Star_inactive.png";
                workrating4 = "Star_inactive.png";
                workrating5 = "Star_inactive.png";
            }
            else if (id == "2")
            {
                if (workrating2 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "Star_inactive.png";
                    workrating3 = "Star_inactive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "Star_inactive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }

            }
            else if (id == "3")
            {
                if (workrating3 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "Star_inactive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }

            }
            else if (id == "4")
            {
                if (workrating4 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "StarActive.png";
                    workrating5 = "Star_inactive.png";
                }

            }
            else if (id == "5")
            {
                if (workrating5 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "StarActive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "StarActive.png";
                    workrating5 = "StarActive.png";
                }

            }
            workrating = id;
        }

        private void clickvaluerating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                valuerating1 = "StarActive.png";
                valuerating2 = "Star_inactive.png";
                valuerating3 = "Star_inactive.png";
                valuerating4 = "Star_inactive.png";
                valuerating5 = "Star_inactive.png";
            }
            else if (id == "2")
            {
                if (valuerating2 == "StarActive.png")
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "Star_inactive.png";
                    valuerating3 = "Star_inactive.png";
                    valuerating4 = "Star_inactive.png";
                    valuerating5 = "Star_inactive.png";
                }
                else
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    valuerating3 = "Star_inactive.png";
                    valuerating4 = "Star_inactive.png";
                    valuerating5 = "Star_inactive.png";
                }

            }
            else if (id == "3")
            {
                if (valuerating3 == "StarActive.png")
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    valuerating3 = "Star_inactive.png";
                    valuerating4 = "Star_inactive.png";
                    valuerating5 = "Star_inactive.png";
                }
                else
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    valuerating3 = "StarActive.png";
                    valuerating4 = "Star_inactive.png";
                    valuerating5 = "Star_inactive.png";
                }

            }
            else if (id == "4")
            {
                if (valuerating4 == "StarActive.png")
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    valuerating3 = "StarActive.png";
                    valuerating4 = "Star_inactive.png";
                    valuerating5 = "Star_inactive.png";
                }
                else
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    valuerating3 = "StarActive.png";
                    valuerating4 = "StarActive.png";
                    valuerating5 = "Star_inactive.png";
                }

            }
            else if (id == "5")
            {
                if (valuerating5 == "StarActive.png")
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    valuerating3 = "StarActive.png";
                    valuerating4 = "StarActive.png";
                    valuerating5 = "Star_inactive.png";
                }
                else
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    valuerating3 = "StarActive.png";
                    valuerating4 = "StarActive.png";
                    valuerating5 = "StarActive.png";
                }

            }
            valuerating = id;
        }

        private void clickcustomerrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                customersubrating1 = "StarActive.png";
                customersubrating2 = "Star_inactive.png";
                customersubrating3 = "Star_inactive.png";
                customersubrating4 = "Star_inactive.png";
                customersubrating5 = "Star_inactive.png";
            }
            else if (id == "2")
            {
                if (customersubrating2 == "StarActive.png")
                {
                    customersubrating1 = "StarActive.png";
                    customersubrating2 = "Star_inactive.png";
                    customersubrating3 = "Star_inactive.png";
                    customersubrating4 = "Star_inactive.png";
                    customersubrating5 = "Star_inactive.png";
                }
                else
                {
                    customersubrating1 = "StarActive.png";
                    customersubrating2 = "StarActive.png";
                    customersubrating3 = "Star_inactive.png";
                    customersubrating4 = "Star_inactive.png";
                    customersubrating5 = "Star_inactive.png";
                }

            }
            else if (id == "3")
            {
                if (customersubrating3 == "StarActive.png")
                {
                    customersubrating1 = "StarActive.png";
                    customersubrating2 = "StarActive.png";
                    customersubrating3 = "Star_inactive.png";
                    customersubrating4 = "Star_inactive.png";
                    customersubrating5 = "Star_inactive.png";
                }
                else
                {
                    valuerating1 = "StarActive.png";
                    valuerating2 = "StarActive.png";
                    customersubrating3 = "StarActive.png";
                    customersubrating4 = "Star_inactive.png";
                    customersubrating5 = "Star_inactive.png";
                }

            }
            else if (id == "4")
            {
                if (customersubrating4 == "StarActive.png")
                {
                    customersubrating1 = "StarActive.png";
                    customersubrating2 = "StarActive.png";
                    customersubrating3 = "StarActive.png";
                    customersubrating4 = "Star_inactive.png";
                    customersubrating5 = "Star_inactive.png";
                }
                else
                {
                    customersubrating1 = "StarActive.png";
                    customersubrating2 = "StarActive.png";
                    customersubrating3 = "StarActive.png";
                    customersubrating4 = "StarActive.png";
                    customersubrating5 = "Star_inactive.png";
                }

            }
            else if (id == "5")
            {
                if (customersubrating5 == "StarActive.png")
                {
                    customersubrating1 = "StarActive.png";
                    customersubrating2 = "StarActive.png";
                    customersubrating3 = "StarActive.png";
                    customersubrating4 = "StarActive.png";
                    customersubrating5 = "Star_inactive.png";
                }
                else
                {
                    customersubrating1 = "StarActive.png";
                    customersubrating2 = "StarActive.png";
                    customersubrating3 = "StarActive.png";
                    customersubrating4 = "StarActive.png";
                    customersubrating5 = "StarActive.png";
                }

            }
            customersubrating = id;
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page Getcurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
