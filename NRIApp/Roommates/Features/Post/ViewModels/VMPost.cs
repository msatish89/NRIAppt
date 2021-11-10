using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Refit;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.Views;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Interface;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Windows.Input;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Location = NRIApp.Roommates.Features.Post.Models.Location;
using System.Collections.ObjectModel;

namespace NRIApp.Roommates.Features.Post.ViewModels
{
    
    public class VMPost : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;

        public event PropertyChangedEventHandler PropertyChanged;

        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Dollarpattern = @"^[1-9]\d*(\.\d+)?$";
        string Namepattern = "^[0-9A-Za-z ]+$";
        private const string GooglePlacesUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        public static int postadid_RM ;

        private DateTime _FrDate = DateTime.Today;
        private string _MaleImg;
        private string _FeMaleImg;
        private string _AnyImg;

        private string _txtrent;

        private string _LocationName = "";

        public string TempLocationName = "";

        private string _Newycityurl = "";

        bool _IsVisibleList = false;
        
        private string _ContactName = "";
        private string _ContactEmail = "";
        private string _ContactPhone = "";

        bool _IsBusy = false;
        bool _OTPIsBusy = true;

        private string _StateName;
        private string _StateCode;
        private string _Zipcode;
        private string _Lat;
        private string _Long;
        private string _Country;


        private string _RentNegoImg;
        private string _RentHideImg;

        private string _LocationAddress;

        public string TempLocationAddress;

        private string _PostOTP { get; set; }

        private List<string> _Countrycode;

        private string _Selectcountry = "+1";


        private string _NewSelectcountry = "+1";

        private List<string> _NewCountrycode;

        private string _NewContactPhone;

        private List<Predictions> _googleadd;


        public Command TapBackPage { get; set; }
      
        public Command TapSearchPage { get; set; }
       
        public DateTime FrDate
        {
            get { return _FrDate; }
            set { _FrDate = value; }
        }
        public string MaleImg
        {
            get { return _MaleImg; }
            set
            {
                _MaleImg = value;
                OnPropertyChanged(nameof(MaleImg));
            }
        }

        public string FeMaleImg
        {
            get { return _FeMaleImg; }
            set
            {
                _FeMaleImg = value;
                OnPropertyChanged(nameof(FeMaleImg));
            }
        }

        public string AnyImg
        {
            get { return _AnyImg; }
            set
            {
                _AnyImg = value;
                OnPropertyChanged(nameof(AnyImg));
            }
        }
        public string txtrent
        {
            get { return _txtrent; }
            set
            {
                _txtrent = value;
                OnPropertyChanged(nameof(txtrent));
            }
        }

        public Command GenderMaleCommand { get; set; }
       
        public Command GenderFemaleCommand { get; set; }
        
        public Command GenderAnyCommand { get; set; }
       

        public string Gender;

        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value;
                OnPropertyChanged(nameof(LocationName));
                if (string.IsNullOrEmpty(LocationName))
                {
                    TempLocationName = "";
                }
                if (!(string.IsNullOrEmpty(LocationName)) && (TempLocationName != LocationName))
                {
                    GetLocationname();
                }
            }
        }

        public string Newycityurl
        {
            get { return _Newycityurl; }
            set { _Newycityurl = value; }
        }
        private List<Location> _LocationList { get; set; }
        public List<Location> LocationList
        {
            get { return _LocationList; }
            set
            {
                _LocationList = value;
            }
        }

        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value;OnPropertyChanged(nameof(StateName)); }
        }
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value;OnPropertyChanged(nameof(StateCode)); }
        }
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; }
        }
        public string Lat
        {
            get { return _Lat; }
            set { _Lat = value; }
        }
        public string Long
        {
            get { return _Long; }
            set { _Long = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public bool IsVisibleList
        {
            get { return _IsVisibleList; }
            set
            {
                _IsVisibleList = value;
                OnPropertyChanged();
            }
        }
        public string ContactName
        {
            get { return _ContactName; }
            set
            {
                _ContactName = value;
                OnPropertyChanged();
            }
        }
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set
            {
                _ContactEmail = value;
                OnPropertyChanged();
            }
        }
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set
            {
                _ContactPhone = value;
                OnPropertyChanged();
            }
        }
        public static string businessnametxt = "";
        private string _BusinessName;
        public string BusinessName
        {
            get { return _BusinessName; }
            set { _BusinessName = value;OnPropertyChanged(nameof(BusinessName)); }
        }
        public static string Licensetxt = "";
        private string _LicenseNo;
        public string LicenseNo
        {
            get { return _LicenseNo; }
            set { _LicenseNo = value; OnPropertyChanged(nameof(LicenseNo)); }
        }
        private string _selectedphotocount;
        public string selectedphotocount
        {
            get { return _selectedphotocount; }
            set { _selectedphotocount = value;OnPropertyChanged(nameof(selectedphotocount)); }
        }



        public Command<Location> LocationCmd { get; set; }
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }
        public Command postcmdfirst { get; set; }
        public Command Adphotos { get; set; }
        public Command selectProfilephoto { get; set; }

        public static string RentNegotiable = "0";
        public static string HideRent = "0";
       
        public Command RentNegoCommand { get; set; }
        
        public Command RentHideCommand { get; set; }
      
        public string RentNegoImg
        {
            get { return _RentNegoImg; }
            set
            {
                _RentNegoImg = value;
                OnPropertyChanged(nameof(RentNegoImg));
            }
        }
        public string RentHideImg
        {
            get { return _RentHideImg; }
            set
            {
                _RentHideImg = value;
                OnPropertyChanged(nameof(RentHideImg));
            }
        }

        public string LocationAddress {
            get { return _LocationAddress; }
            set { _LocationAddress = value;
                OnPropertyChanged(nameof(LocationAddress));
                if (LocationAddress != null && TempLocationAddress != LocationAddress)
                {
                    GetgoogleAddress();
                }
            }
        }
        public ICommand postcmdsecond { get; set; }
        

        public Command SubmitOTPPost { get; set; }

        public Command ResendOTP { get; set; }
        public ICommand postdescrptionsubmit { get; set; }

        public ICommand Taponpricemode { get; set; }
        public ICommand Taponstayperiod { get; set; }
        public ICommand Taponaccomodates { get; set; }
        public ICommand Taponattachedbathtype { get; set; }

        public ICommand Selectpricemode { get; set; }
        public ICommand Selectstayperiod { get; set; }
        public ICommand Selectaccomodate { get; set; }
        public ICommand Selectbathtype { get; set; }

        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        //public Command Removeimg { get; set; }
        public Command<string> Removeimg { get; set; }
        public Command Removeprofilepic { get; set; }

        public string PostOTP
        {
            get { return _PostOTP; }
            set { _PostOTP = value.Trim(); }
        }
        public static string  _Pinno;

        public List<string> Countrycode
        {
            get { return _Countrycode; }
            set { _Countrycode = value;
            }
        }

        List<string> Countryitem = new List<string>() {

            "+1","+91"
        };

       
        public string Selectcountry
        {
            get { return _Selectcountry; }
            set { _Selectcountry = value;
                OnPropertyChanged(Selectcountry);
            }
        }

        public Command ChangemobileNumber { get; set; }
       
        public Command SubmitChangeMobile { get; set; }
       
        public bool OTPIsBusy
        {
            get { return _OTPIsBusy; }
            set
            {
                _OTPIsBusy = value;
                OnPropertyChanged();
            }
        }

        public string NewSelectcountry
        {
            get { return _NewSelectcountry; }
            set
            {
                _NewSelectcountry = value;
                OnPropertyChanged(NewSelectcountry);
            }
        }

        public List<string> NewCountrycode
        {
            get { return _NewCountrycode; }
            set
            {
                _NewCountrycode = value;
            }
        }

        public string NewContactPhone
        {
            get { return _NewContactPhone; }
            set
            {
                _NewContactPhone = value;
                OnPropertyChanged();
            }
        }

        public List<Predictions> googleadd
        {
            get { return _googleadd; }
            set { _googleadd = value; }
        }
        private bool _AgentInfo;
        public bool AgentInfo
        {
            get { return _AgentInfo; }
            set { _AgentInfo = value;OnPropertyChanged(nameof(AgentInfo)); }
        }
       
        private string _AdTitle;
        public string AdTitle
        {
            get { return _AdTitle; }
            set { _AdTitle = value;OnPropertyChanged(nameof(AdTitle)); }
        }
        private string _AdDescription;
        public string AdDescription
        {
            get { return _AdDescription; }
            set { _AdDescription = value; OnPropertyChanged(nameof(AdDescription)); }
        }
        
        private bool _pricemodevisible=false;
        public bool pricemodevisible
        {
            get { return _pricemodevisible; }
            set { _pricemodevisible = value;OnPropertyChanged(nameof(pricemodevisible)); }
        }
        private bool _stayperiodvisible=false;
        public bool stayperiodvisible
        {
            get { return _stayperiodvisible; }
            set { _stayperiodvisible = value; OnPropertyChanged(nameof(stayperiodvisible)); }
        }
        private bool _accomodatesvisible=false;
        public bool accomodatesvisible
        {
            get { return _accomodatesvisible; }
            set { _accomodatesvisible = value; OnPropertyChanged(nameof(accomodatesvisible)); }
        }
        private bool _attachedbathvisible=false;
        public bool attachedbathvisible
        {
            get { return _attachedbathvisible; }
            set { _attachedbathvisible = value; OnPropertyChanged(nameof(attachedbathvisible)); }
        }
        private bool _contentviewvisible=false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value;OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private string _pricemodetxt="Select price mode";
        public string pricemodetxt
        {
            get { return _pricemodetxt; }
            set { _pricemodetxt = value; OnPropertyChanged(nameof(pricemodetxt)); }
        }
        private string _leasetypetxt="Select lease type";
        public string leasetypetxt
        {
            get { return _leasetypetxt; }
            set { _leasetypetxt = value; OnPropertyChanged(nameof(leasetypetxt)); }
        }
        private string _accomodatestxt="Accomodates";
        public string accomodatestxt
        {
            get { return _accomodatestxt; }
            set { _accomodatestxt = value; OnPropertyChanged(nameof(accomodatestxt)); }
        }
        private string _attachedbathtxt="Attached Bath";
        public string attachedbathtxt
        {
            get { return _attachedbathtxt; }
            set { _attachedbathtxt = value; OnPropertyChanged(nameof(attachedbathtxt)); }
        }
        private bool _agentppvisible = false;
        public bool agentppvisible
        {
            get { return _agentppvisible; }
            set { _agentppvisible = value; OnPropertyChanged(nameof(agentppvisible)); }
        }
        private bool _agentpicselected = false;
        public bool agentpicselected
        {
            get { return _agentpicselected; }
            set { _agentpicselected = value;OnPropertyChanged(nameof(agentpicselected)); }
        }
        public static string sPricemode="" ;
        public static string sAttachedbath = "";
        public static string sStayperiod = "";
        public static string sAccomodate="" ;
        public List<Posting_Param> pricemodelistdata { get; set; }
        public List<Posting_Param> attachbathlistdata { get; set; }
        public List<Posting_Param> stayperiodlistdata { get; set; }
        public List<Posting_Param> accomodatelistdata { get; set; }

        public List<Posting_Param> pricemodelist = new List<Posting_Param>()
        {
            new Posting_Param{pricemode="Per Night",pricemodetype=1, checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{pricemode="Per Day",pricemodetype=2, checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{pricemode="Per Week",pricemodetype=3, checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{pricemode="Per Month",pricemodetype=4, checkimage="RadioButtonUnChecked.png"},
            
        };
        public List<Posting_Param> attachbathlist = new List<Posting_Param>()
        {
            new Posting_Param{attachedbath="Yes",attachbathtype="1",checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{attachedbath="No",attachbathtype="0",checkimage="RadioButtonUnChecked.png"}
        };
        public List<Posting_Param> stayperiodlist = new List<Posting_Param>()
        {
            new Posting_Param { stayperiodtxt = "Long term (6+ months)", stayperiodID = "1", checkimage = "RadioButtonUnChecked.png" },
            new Posting_Param { stayperiodtxt = "Short term", stayperiodID = "2", checkimage = "RadioButtonUnChecked.png" },
            new Posting_Param { stayperiodtxt = "Any", stayperiodID = "0", checkimage = "RadioButtonUnChecked.png" }
        };
        public List<Posting_Param> accomodatelist = new List<Posting_Param>()
        {
            new Posting_Param{accomodatecnt=1,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=2,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=3,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=4,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=5,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=6,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=7,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=8,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=9,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=10,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=11,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=12,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=13,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=14,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=15,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=16,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=17,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=18,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=19,checkimage="RadioButtonUnChecked.png"},
            new Posting_Param{accomodatecnt=20,checkimage="RadioButtonUnChecked.png"},
        };
        public Command Selectgoogleaddress { get; set; }
        public bool Validations()
        {
            bool result = true;
            var currentpage = GetCurrentPage();

            if (string.IsNullOrEmpty(ContactName.Trim()))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactname");
                myEntry.Focus();
                dialog.Toast("Enter Contact Name");
                return false;
            }
            if (!Regex.IsMatch(ContactName, Namepattern))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactname");
                myEntry.Focus();
                dialog.Toast("Enter Proper Name");
                return false;
            }
            if (string.IsNullOrEmpty(ContactEmail))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactemail");
                myEntry.Focus();
                dialog.Toast("Enter Contact Email");
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactemail");
                myEntry.Focus();
                dialog.Toast("Enter Proper Email");
                return false;
            }
            if (string.IsNullOrEmpty(ContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                dialog.Toast("Enter Contact Number");
                return false;
            }
            if (ContactPhone.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                dialog.Toast("Minimum 10 Digits required");
                return false;
            }
            else if (!CheckValidPhone(ContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                dialog.Toast("Please enter valid mobile number");
                return false;
            }
            if (string.IsNullOrEmpty(Zipcode))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtlocation");
                myEntry.Focus();
                dialog.Toast("Enter Location");
                return false;
            }
           
            //Agent validation
            if (VMCategory.Scategoryid==11)
            {
                if (string.IsNullOrEmpty(BusinessName))
                {
                    Entry myEntry = currentpage.FindByName<Entry>("Businessname");
                    myEntry.Focus();
                    dialog.Toast("Enter Business Name");
                    return false;
                }
                if (string.IsNullOrEmpty(LicenseNo))
                {
                    Entry myEntry = currentpage.FindByName<Entry>("LicenseNo");
                    myEntry.Focus();
                    dialog.Toast("Enter License Number");
                    return false;
                }

                //if (string.IsNullOrEmpty(Lat))
                //{
                //    dialog.Toast("Select the Location");
                //    return false;
                //}
            }

            return result;
        }

        public bool Validationssecond()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(txtrent))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtrent");
                myEntry.Focus();
                dialog.Toast("Enter Rent");
                return false;
            }
            //if (!Regex.IsMatch(txtrent, Dollarpattern))
            //{
            //    Entry myEntry = currentpage.FindByName<Entry>("txtrent");
            //    myEntry.Focus();
            //    dialog.Toast("Dollar Format Required");
            //    return false;
            //}
            if (txtrent.Length > 6)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtrent");
                myEntry.Focus();
                dialog.Toast("Maximum 6 Digits only");
                return false;
            }
            if (Convert.ToInt32(txtrent) < 100)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtrent");
                myEntry.Focus();
                dialog.Toast("Minimum allowed value is : 100");
                return false;
            }
            if (string.IsNullOrEmpty(sPricemode))
            {
                Label myEntry = currentpage.FindByName<Label>("pricemodetxt");
                //myEntry.Focus();
                dialog.Toast("Select price mode");
                return false;
            }

            if (string.IsNullOrEmpty(sAttachedbath))
            {
                Label myEntry = currentpage.FindByName<Label>("attachedbathtxt");
                dialog.Toast("Select attached bathtype");
                return false;
            }

            if (string.IsNullOrEmpty(sStayperiod))
            {
                Label myEntry = currentpage.FindByName<Label>("leasetypetxt");
                dialog.Toast("Select stayperiod");
                return false;
            }
            if (string.IsNullOrEmpty(sAccomodate))
            {
                Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                dialog.Toast("Select number of accomodates");
                return false;
            }
            if (string.IsNullOrEmpty(LocationAddress))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtroomaddress");
                myEntry.Focus();
                dialog.Toast("Enter Address");
                return false;
            }
            //if (string.IsNullOrEmpty(TempLocationAddress))
            //{
            //    Entry myEntry = currentpage.FindByName<Entry>("txtroomaddress");
            //    myEntry.Focus();
            //    dialog.Toast("Select from Ajax");
            //    return false;
            //}
            return result;
        }
        public bool postdescriptionvalidation()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(AdTitle))
            {
                //Entry myEntry = currentpage.FindByName<Entry>("adtitle");
                //myEntry.Focus();
                dialog.Toast("Enter your AD title");
                return false;
            }
            if (string.IsNullOrEmpty(AdDescription))
            {
                //Entry myEntry = currentpage.FindByName<Entry>("ADdescription");
                //myEntry.Focus();
                dialog.Toast("Enter your description");
                return false;
            }
            return result;
        }
        public bool Validationsnewphone()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(NewContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtnewcontactphone");
                myEntry.Focus();
                dialog.Toast("Enter Contact Number");
                return false;
            }

            if (NewContactPhone.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtnewcontactphone");
                myEntry.Focus();
                dialog.Toast("Minimum 10 Digits required");
                return false;
            }
            if (!CheckValidPhone(NewContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtnewcontactphone");
                myEntry.Focus();
                dialog.Toast("Please enter valid mobile number");
                return false;
            }
            return result;
        }

        bool CheckValidPhone(string inputPhone)
        {
            bool bPhnumValid = false;
            string firstchar = inputPhone.Substring(0, 1);
            if (inputPhone.Length > 6)
            {
                for (var iphnum = 0; iphnum < 6; iphnum++)
                {
                    string chara = inputPhone[iphnum].ToString();
                    if (chara != firstchar)
                        bPhnumValid = true;

                }
            }
            return bPhnumValid;
        }


        CategoryList ctl = new CategoryList();

        Postfirst pt = new Postfirst();
        //postfirst
        public VMPost(CategoryList clist)
        {
            try
            {
                
                if (clist.ismyneed=="1" || clist.ismyneedpayment=="1")
                {
                   
                    //ctl = clist;
                    if (VMCategory.Scategoryid == 11)
                    {
                        AgentInfo = true;
                        ctl.usertype = "2";
                        BusinessName = clist.businessname;
                        LicenseNo = clist.licenseno;
                    }
                    else
                    {
                        AgentInfo = false;
                        ctl.usertype = "1";
                    }
                    clist.usertype = ctl.usertype;
                    needsprefill(clist);
                }
                else
                {
                    if (VMCategory.Scategoryid == 11)
                    {
                        AgentInfo = true;
                        ctl.usertype = "2";
                    }
                    else
                    {
                        AgentInfo = false;
                        ctl.usertype = "1";
                    }
                    ctl.supercategoryid = clist.supercategoryid;
                    ctl.supercategoryvalue = clist.supercategoryvalue;
                    ctl.primarycategoryid = clist.primarycategoryid;
                    ctl.primarycategoryvalue = clist.primarycategoryvalue;
                    ctl.secondarycategoryid = clist.secondarycategoryid;
                    ctl.secondarycategoryvalue = clist.secondarycategoryvalue;
                    ctl.tertiarycategoryid = clist.tertiarycategoryid;
                    ctl.categoryname = clist.categoryname;
                    ctl.maincategory = clist.maincategory;
                    ctl.movedate = clist.movedate;
                    ctl.pricefrom = "0";
                    ctl.priceto = clist.priceto;

                    if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                        ContactEmail = Commonsettings.UserEmail;
                    if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                        ContactName = Commonsettings.UserName;
                    if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                        ContactPhone = Commonsettings.UserMobileno;
                    if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                    {
                        LocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        TempLocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        Newycityurl = Commonsettings.Usercityurl;
                        Zipcode = Commonsettings.Userzipcode;
                    }

                }
                Countrycode = Countryitem;
                OnPropertyChanged(nameof(Countrycode));

                TapBackPage = new Command(ClickBackPage);
                // TapSearchPage = new Command(ClickSearchPage);

               
                LocationCmd = new Command<Location>(OnLocationClick);

                postcmdfirst = new Command(clickpostcmdfirst);
            }
            catch (Exception e) { }
        }
        public void needsprefill(CategoryList clist)
        {
            try
            {
                ctl.supercategoryid = clist.supercategoryid;
                ctl.supercategoryvalue = clist.supercategoryvalue;
                ctl.primarycategoryid = clist.primarycategoryid;
                ctl.primarycategoryvalue = clist.primarycategoryvalue;
                ctl.secondarycategoryid = clist.secondarycategoryid;
                ctl.secondarycategoryvalue = clist.secondarycategoryvalue;
                ctl.tertiarycategoryid = clist.tertiarycategoryid;
                ctl.categoryname = clist.categoryname;
                ctl.maincategory = clist.maincategory;
                ctl.adtype = clist.adtype;
                ctl.categoryimgurl = clist.categoryimgurl;
               // ctl.textcolor = clist.textcolor;

                ctl.Locationname = LocationName = clist.Locationname;
                LocationName = clist.Locationname;
                TempLocationName = clist.Locationname;
                if (string.IsNullOrEmpty(LocationName))
                {
                    ctl.Locationname = LocationName = clist.StateName + ", " + clist.Statecode;
                    LocationName = clist.StateName + ", " + clist.Statecode;
                    TempLocationName = clist.StateName + ", " + clist.Statecode;
                }
                OnPropertyChanged(LocationName);
                ctl.City = citytxt = clist.City;
                ctl.Newcityurl =  clist.Newcityurl;
                ctl.ContactName = ContactName = clist.Contributor;
                ctl.ContactEmail = ContactEmail = clist.Email;
                ctl.Countrycode = Selectcountry = clist.Countrycode;
                ctl.ContactPhone = ContactPhone=  clist.Phone;
                ctl.StateName = StateName = clist.StateName;
                ctl.StateCode = StateCode = clist.Statecode;
                ctl.Zipcode = Zipcode= clist.Zipcode;
                ctl.Lat = Lat = clist.Lat;
                ctl.Long = Long= clist.Long;
                ctl.Country = Country= clist.Country;
                ctl.countflag = clist.countflag;

                ctl.adid = clist.adid;
                ctl.agentid = clist.agentid;
                //Convert.ToDateTime(clist.movedate.ToString("MM/dd/yyyy"));
                ctl.pricefrom = clist.pricefrom;
                ctl.priceto = clist.priceto;
                ctl.gender  = clist.gender;
                ctl.Rent =Rentamount= clist.Rent;
                ctl.attachedbaths =sAttachedbath= clist.attachedbaths;
                
                ctl.availablefrm = clist.availablefrm;
               // FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);
                ctl.pricemode = clist.pricemode;
                ctl.accomodate =  clist.accomodate;
                ctl.stayperiod = clist.stayperiod;
                ctl.title = clist.title;
                ctl.description = clist.description;
                ctl.type = clist.type;
                ctl.usertype = clist.usertype;

                ctl.agentphotourl = clist.agentphotourl;
                ctl.attachedbathtype = clist.attachedbathtype;


                //prefilldataadded
                ctl.service = clist.service;
                ctl.Isagent = clist.Isagent;
                ctl.Subcategory = clist.Subcategory;
                ctl.Category = clist.Category;
                ctl.Ispaid = clist.Ispaid;
                ctl.Photoscnt = clist.Photoscnt;
                ctl.HtmlPhotopath = clist.HtmlPhotopath;
                ctl.Imagepath = clist.Imagepath;
                ctl.XmlPhotopath = clist.XmlPhotopath;
                ctl.Adtype = clist.Adtype;
                ctl.Additionalcity = clist.Additionalcity;
                ctl.Citycount = clist.Citycount;
                ctl.Citytotalamount = clist.Citytotalamount;
                ctl.Tags = clist.Tags;
                //ctl.Companyweburl = clist.Companyweburl;
                ctl.url = clist.url;
                ctl.Bestindustry = clist.Bestindustry;
                //ctl.Minsalary = clist.Minsalary;
                //ctl.Maxsalary = clist.Maxsalary;
                //ctl.Qualification = clist.Qualification;
                //ctl.Visastatus = clist.Visastatus;
                //ctl.Experience = clist.Experience;
                //ctl.Experiencefrom = clist.Experiencefrom;
                //ctl.Experienceto = clist.Experienceto;
                //ctl.Employmenttype = clist.Employmenttype;
                //ctl.Jobtype = clist.Jobtype;
                //ctl.Jobroleid = clist.Jobroleid;
                ctl.Postedby = clist.Postedby;
                ctl.Campaignid = clist.Campaignid;
                ctl.Customerid = clist.Customerid;
                ctl.Ordertype = clist.Ordertype;
                ctl.Userpid = clist.Userpid;
                ctl.Mailerstatus = clist.Mailerstatus;
                ctl.Displayphone = clist.Displayphone;
                ctl.Status = clist.Status;
                ctl.Contributor = clist.Contributor;
                ctl.description = clist.description;
                ctl.Titleurl = clist.Titleurl;
                ctl.State = clist.State;
                ctl.title = clist.title;
                ctl.Streetname = LocationAddress = clist.Streetname;
                ctl.Salarymodeid = clist.Salarymodeid;
                ctl.Salarymode = clist.Salarymode;
                ctl.Nationwide = clist.Nationwide;
                ctl.Trimetros = clist.Trimetros;
                ctl.Linkedinurl = clist.Linkedinurl;
                ctl.Hideaddress = clist.Hideaddress;
                //ctl.Isresumeavail = clist.Isresumeavail;
                //ctl.Jobrole = clist.Jobrole;
                ctl.Adpostedcnt = clist.Adpostedcnt;
                ctl.Noofadcnt = clist.Noofadcnt;
                ctl.Expirydate = clist.Expirydate;
                ctl.Orderid = clist.Orderid;
                //ctl.Industryname = clist.Industryname;
                //ctl.Industryid = clist.Industryid;
                ctl.Secondarycategoryvalueurl = clist.Secondarycategoryvalueurl;
                ctl.Primarycategoryvalueurl = clist.Primarycategoryvalueurl;
                ctl.Supercategoryvalueurl = clist.Supercategoryvalueurl;
                ctl.Metrourl = clist.Metrourl;
                ctl.Metro = clist.Metro;
                ctl.Statecode = clist.Statecode;
                ctl.Ismobileverified = clist.Ismobileverified;
                ctl.Folderid = clist.Folderid;
                ctl.Amount = clist.Amount;
                ctl.Numberofdays = clist.Numberofdays;
                ctl.Startdate = clist.Startdate;
                ctl.Phone = clist.Phone;
                ctl.Email = clist.Email;
                ctl.Enddate = clist.Enddate;
                ctl.Islisting = clist.Islisting;
                ctl.Freeadsflag = clist.Freeadsflag;
                ctl.Categoryflag = clist.Categoryflag;
                ctl.Banneramount = clist.Banneramount;
                ctl.Bannerstatus = clist.Bannerstatus;
                ctl.Companytype = clist.Companytype;
                ctl.Salespersonemail = clist.Salespersonemail;
                ctl.Couponcode = clist.Couponcode;
                ctl.Disclosedbusiness = clist.Disclosedbusiness;
                ctl.Gender = clist.Gender;
                ctl.Responsemail = clist.Responsemail;
                ctl.Benefits = clist.Benefits;
                ctl.Pendingpaycouponcode = clist.Pendingpaycouponcode;
                ctl.Bonusamount = clist.Bonusamount;
                ctl.Bonusdays = clist.Bonusdays;
                ctl.Emailblastamount = clist.Emailblastamount;
                ctl.Addonamount = clist.Addonamount;
                ctl.businessname = clist.businessname;
                ctl.City = clist.City;


                //from myneedspage

                
                OnPropertyChanged(nameof(LocationName));
                OnPropertyChanged(nameof(TempLocationName));
                citytxt = clist.Locationname;
                Newycityurl = clist.Newcityurl;
                Zipcode = clist.Zipcode;
                BusinessName = clist.businessname;
                businessnametxt = clist.businessname;
                ContactPhone = clist.Phone;
                LicenseNo = clist.licenseno;
                Licensetxt = clist.licenseno;

                if(!clist.Countrycode.Contains("+"))
                {
                    clist.Countrycode = "+" + clist.Countrycode;
                }
                Selectcountry = clist.Countrycode;
                _Selectcountry = clist.Countrycode;
                _NewSelectcountry = clist.Countrycode;
                NewSelectcountry = clist.Countrycode; ;
                ctl.ismyneed = clist.ismyneed;
                ctl.ismyneedpayment = clist.ismyneedpayment;
                ctl.movedate = clist.movedate;
                if(!string.IsNullOrEmpty(clist.movedate))
                {
                    FrDate = DateTime.ParseExact(clist.movedate, "MM/dd/yyyy", null);
                }
                postadid_RM = clist.adid;
            }
            catch(Exception ex)
            {

            }
        }
        private string _profileimgurl;
        public string profileimgurl
        {
            get { return _profileimgurl; }
            set { value = _profileimgurl;OnPropertyChanged(nameof(profileimgurl)); }
        }
       
        public async void Selectphoto()
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    dialog.Toast("This is not support on your device.");
                    return;
                }
                else
                {
                    await DependencyService.Get<IMultiMediaPickerService>().PickPhotosAsync(postadid_RM.ToString());
                }
            }
            catch(Exception e)
            {

            }
        }
        //postsecond
        public static string postordertype = "";
        public VMPost(Postfirst clist, int adid, int agentid, string ordertype,string page)
        {
            try
            {
              
                if (clist.usertype == "2")
                {
                    agentppvisible = true;
                }
                else
                {
                    agentppvisible = false;
                }
                postordertype = ordertype;
                ctl.supercategoryid = clist.supercategoryid;
                ctl.supercategoryvalue = clist.supercategoryvalue;
                ctl.primarycategoryid = clist.primarycategoryid;
                ctl.primarycategoryvalue = clist.primarycategoryvalue;
                ctl.secondarycategoryid = clist.secondarycategoryid;
                ctl.secondarycategoryvalue = clist.secondarycategoryvalue;
                ctl.tertiarycategoryid = clist.tertiarycategoryid;
                ctl.categoryname = clist.categoryname;
                ctl.maincategory = clist.maincategory;
                ctl.usertype = clist.usertype;


                ctl.Newcityurl = clist.Newcityurl;
                ctl.ContactName = clist.ContactName;
                ctl.ContactEmail = clist.ContactEmail;
                ctl.Countrycode = clist.Countrycode;
                ctl.ContactPhone = clist.ContactPhone;
                ctl.StateName = clist.StateName;
                ctl.StateCode = clist.StateCode;
                ctl.Zipcode = clist.Zipcode;
                ctl.Lat = clist.userlat;
                ctl.Long = clist.userlong;
                ctl.Country = clist.Country;
                ctl.Locationname = clist.Locationname;

                ctl.adid = adid;
                ctl.agentid = clist.agentid;
                ctl.gender = clist.gender;
                ctl.movedate = clist.Movedate;
                ctl.agentphotourl = clist.agentphotourl;

                ctl.attachedbathtype = clist.attachedbathtype;

                if (AdmultiImage.Count > 0)
                {
                    selectedphotocount = AdmultiImage.Count.ToString() + " photos selected";
                    OnPropertyChanged(nameof(selectedphotocount));
                }



                TapBackPage = new Command(ClickBackPage);
                // TapSearchPage = new Command(ClickSearchPage);

                MaleImg = "RadioButtonUnChecked.png";
                FeMaleImg = "RadioButtonUnChecked.png";
                AnyImg = "RadioButtonChecked.png";
                GenderMaleCommand = new Command(ChangeGenderMale);
                GenderFemaleCommand = new Command(ChangeGenderFemale);
                GenderAnyCommand = new Command(ChangeGenderAny);

                RentNegoImg = "CheckBoxUnChecked.png";
                RentHideImg = "CheckBoxUnChecked.png";
                RentNegoCommand = new Command(ChangeRentNegoCommand);
                RentHideCommand = new Command(ChangeRentHideCommand);

                Adphotos = new Command(Selectphoto);
                Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
                postcmdsecond = new Command<CategoryList>(clickpostcmdsecond);
                postdescrptionsubmit = new Command<CategoryList>(clickpostdescription);

                pricemodelistdata = pricemodelist;
                stayperiodlistdata = stayperiodlist;
                accomodatelistdata = accomodatelist;
                attachbathlistdata = attachbathlist;

                Taponpricemode = new Command<Posting_Param>(showpricemodelist);
                Taponstayperiod = new Command<Posting_Param>(showstayperiodlist);
                Taponaccomodates = new Command<Posting_Param>(showaccomodatelist);
                Taponattachedbathtype = new Command<Posting_Param>(showbathtypelist);

                Selectpricemode = new Command<Posting_Param>(SelectpricemodeID);
                Selectstayperiod = new Command<Posting_Param>(SelectstayperiodID);
                Selectaccomodate = new Command<Posting_Param>(Selectaccomodatecnt);
                Selectbathtype = new Command<Posting_Param>(SelectbathtypeID);

                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);
                selectProfilephoto = new Command(uploadprofilepic);
                //toremoveimages from photourl List
                Removeimg = new Command<string>(removeimgfromlist);
                Removeprofilepic = new Command(removeagentprofilepic);
                if (clist.ismyneed == "1")
                {
                    pst = clist;
                    clist.ispostdes = "1";
                    postsecondprefill(clist);
                    // needsprefill(clist);
                }
            }
            catch (Exception e) { }
        }
        public VMPost(Postfirst  clist, int adid,int agentid, string ordertype)
        {
            if(AdmultiImage==null&&clist.ismyneed!="1")
            {
                AdmultiImage = new List<string>();
                AdmultiImagetoDB = new List<string>();
                profilepicurl = "";
                sPricemode = "";
                sAttachedbath = "";
                sStayperiod = "";
                sAccomodate = "";
            }
            
            
            try
            {
                if(clist.usertype=="2")
                {
                    agentppvisible = true;
                }
                else
                {
                    agentppvisible = false;
                }
                postordertype = ordertype;
                ctl.supercategoryid = clist.supercategoryid;
                ctl.supercategoryvalue = clist.supercategoryvalue;
                ctl.primarycategoryid = clist.primarycategoryid;
                ctl.primarycategoryvalue = clist.primarycategoryvalue;
                ctl.secondarycategoryid = clist.secondarycategoryid;
                ctl.secondarycategoryvalue = clist.secondarycategoryvalue;
                ctl.tertiarycategoryid = clist.tertiarycategoryid;
                ctl.categoryname = clist.categoryname;
                ctl.maincategory = clist.maincategory;
                ctl.usertype = clist.usertype;

                
                ctl.Newcityurl = clist.Newcityurl;
                ctl.ContactName = clist.ContactName;
                ctl.ContactEmail = clist.ContactEmail;
                ctl.Countrycode = clist.Countrycode;
                ctl.ContactPhone = clist.ContactPhone;
                ctl.StateName = clist.StateName;
                ctl.StateCode = clist.StateCode;
                ctl.Zipcode = clist.Zipcode;
                ctl.Lat = clist.userlat;
                ctl.Long = clist.userlong;
                ctl.Country = clist.Country;
                ctl.Locationname = clist.Locationname;

                ctl.adid = adid;
                ctl.agentid = clist.agentid;
                ctl.gender = clist.gender;
                ctl.movedate = clist.Movedate;
                ctl.agentphotourl = clist.agentphotourl;

                ctl.attachedbathtype = clist.attachedbathtype;
                if(AdmultiImage==null)
                {
                    selectedphotocount = "0 photos selected";
                }
                else if(AdmultiImage.Count>0)
                {
                    selectedphotocount = AdmultiImage.Count.ToString() + " photos selected";
                    OnPropertyChanged(nameof(selectedphotocount));
                }
                
                

                 TapBackPage = new Command(ClickBackPage);
               // TapSearchPage = new Command(ClickSearchPage);

                MaleImg = "RadioButtonUnChecked.png";
                FeMaleImg = "RadioButtonUnChecked.png";
                AnyImg = "RadioButtonChecked.png";
                GenderMaleCommand = new Command(ChangeGenderMale);
                GenderFemaleCommand = new Command(ChangeGenderFemale);
                GenderAnyCommand = new Command(ChangeGenderAny);

                RentNegoImg = "CheckBoxUnChecked.png";
                RentHideImg = "CheckBoxUnChecked.png";
                RentNegoCommand = new Command(ChangeRentNegoCommand);
                RentHideCommand = new Command(ChangeRentHideCommand);

                Adphotos = new Command(Selectphoto);
                Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
                postcmdsecond = new Command<CategoryList>(clickpostcmdsecond);
                postdescrptionsubmit = new Command<CategoryList>(clickpostdescription);

                pricemodelistdata = pricemodelist;
                stayperiodlistdata = stayperiodlist;
                accomodatelistdata = accomodatelist;
                attachbathlistdata = attachbathlist;

                Taponpricemode = new Command<Posting_Param>(showpricemodelist);
                Taponstayperiod = new Command<Posting_Param>(showstayperiodlist);
                Taponaccomodates = new Command<Posting_Param>(showaccomodatelist);
                Taponattachedbathtype = new Command<Posting_Param>(showbathtypelist);

                Selectpricemode = new Command<Posting_Param>(SelectpricemodeID);
                Selectstayperiod = new Command<Posting_Param>(SelectstayperiodID);
                Selectaccomodate = new Command<Posting_Param>(Selectaccomodatecnt);
                Selectbathtype = new Command<Posting_Param>(SelectbathtypeID);

                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);
                selectProfilephoto = new Command(uploadprofilepic);
                //toremoveimages from photourl List
                Removeimg = new Command<string>(removeimgfromlist);
                Removeprofilepic = new Command(removeagentprofilepic);
                if(clist.ismyneed =="1")
                {
                    pst = clist;
                    clist.ispostdes = "0";
                    postsecondprefill(pst);
                   // needsprefill(clist);
                }
            }
            catch (Exception e) { }
        }
        public void postsecondprefill(Postfirst clist)
        {
            try
            {
                var currentpage = getcurrentpage();
                if(clist.ispostdes=="1")
                {
                    //postthirdform
                    AdTitle = clist.title;
                    AdDescription = clist.description;
                    if (VMCategory.Scategoryid == 11)
                    {
                        BusinessName = clist.businessname;
                        LicenseNo = clist.businessLicenseno;
                        LocationAddress = clist.LocationAddress;
                    }
                }
                else
                {
                    if (clist.stayperiod == "1")
                    {
                        leasetypetxt = "Long term (6+ months)";
                    }
                    else if (clist.stayperiod == "2")
                    {
                        leasetypetxt = "Short term";
                    }
                    else if (clist.stayperiod == "0")
                    {
                        leasetypetxt = "Any";
                    }
                    sStayperiod = clist.stayperiod;
                    ctl.movedate = clist.Movedate;
                    
                    // Label stayperiodtxtclr = currentpage.FindByName<Label>("leasetypetxt");
                    // stayperiodtxtclr.TextColor = Color.FromHex("#212121");
                    accomodatestxt = sAccomodate = clist.accomodate;
                    pricemodetxt = sPricemode = clist.pricemode;
                   // Label pricemodetxtclr = currentpage.FindByName<Label>("pricemodetxt");
                   // pricemodetxtclr.TextColor = Color.FromHex("#212121");

                    if (clist.attachedbaths == "1")
                    {
                        attachedbathtxt = "Yes";
                        sAttachedbath = "1";
                    }
                    else
                    {
                        attachedbathtxt = "No";
                        sAttachedbath = "0";
                    }

                   // Label attachedbathtxtclr = currentpage.FindByName<Label>("attachedbathtxt");
                   // attachedbathtxtclr.TextColor = Color.FromHex("#212121");
                    Rentamount = txtrent = clist.rent;
                    if (!string.IsNullOrEmpty(clist.HideRent))
                    {
                        if (clist.HideRent == "1")
                        {
                            RentHideImg = "CheckBoxChecked.png";
                            HideRent = "1";
                        }
                        else
                        {
                            RentHideImg = "CheckBoxUnChecked.png";
                            HideRent = "0";
                        }
                    }

                    LocationAddress = TempLocationAddress= clist.Streetname;

                    if(string.IsNullOrEmpty(clist.Streetname))
                    {
                        LocationAddress = TempLocationAddress = clist.Locationname;
                    }
                    
                    Lat = clist.userlat;
                    Long = clist.userlong;
                    citytxt = clist.City;

                    IsVisibleList = false;
                    IsBusy = false;
                    OnPropertyChanged(nameof(IsBusy));
                    pricemodetxt = clist.pricemode;
                    
                    if (!string.IsNullOrEmpty(clist.gender))
                    {
                        if (clist.gender == "Male")
                        {
                            MaleImg = "RadioButtonChecked.png";
                            FeMaleImg = "RadioButtonUnChecked.png";
                            AnyImg = "RadioButtonUnChecked.png";
                            Gender = "male";
                        }
                        else
                        {
                            MaleImg = "RadioButtonUnChecked.png";
                            FeMaleImg = "RadioButtonChecked.png";
                            AnyImg = "RadioButtonUnChecked.png";
                            Gender = "female";
                        }
                    }
                    else
                    {
                        MaleImg = "RadioButtonUnChecked.png";
                        FeMaleImg = "RadioButtonUnChecked.png";
                        AnyImg = "RadioButtonChecked.png";
                        Gender = "any";
                    }
                    //imageprefil
                    if(!string.IsNullOrEmpty(clist.XmlPhotopath))
                    {
                        AdmultiImage = new List<string>();
                        string[] img = clist.XmlPhotopath.Split(',');
                        foreach(var image in img)
                        {
                            AdmultiImage.Add(image);
                        }

                    }
                    FrDate = DateTime.ParseExact(clist.Movedate, "MM/dd/yyyy", null);
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void removeagentprofilepic()
        {
            try
            {
                profilepicurl = "";
                var CurrentPage = GetCurrentPage();
                var imgpath = CurrentPage.FindByName<Image>("agentprofilepic");
                imgpath.Source = "";
                agentpicselected = false;
            }
            catch(Exception ex)
            {

            }
        }
        public void removeimgfromlist(string imglist)
        {
            if(AdmultiImage.Contains(imglist))
            {
                AdmultiImage.Remove(imglist);
            }
            string dbimg = imglist.Replace("http://cdnnrisulekhalive.blob.core.windows.net:80/cdn/roommates/images/smallimage/", "");
            if (AdmultiImagetoDB.Contains(dbimg))
            {
                AdmultiImagetoDB.Remove(dbimg);
            }
            var currentpage = GetCurrentPage();
            var lst = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
            lst.ItemsSource = null;
            lst.ItemsSource = AdmultiImage;
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        public void SelectpricemodeID(Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label pricemodetxtclr = currentpage.FindByName<Label>("pricemodetxt");
            pricemodetxtclr.TextColor = Color.FromHex("#212121");
            pricemodetxt = posting_Param.pricemode;
            sPricemode= posting_Param.pricemode;

            contentviewvisible = false;
        }
        public void SelectstayperiodID(Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label stayperiodtxtclr = curntpg.FindByName<Label>("leasetypetxt");
            stayperiodtxtclr.TextColor = Color.FromHex("#212121");
            leasetypetxt = posting_Param.stayperiodtxt;
            sStayperiod= posting_Param.stayperiodID;
            contentviewvisible = false;
        }
        public void Selectaccomodatecnt(Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label accomodatestxtclr = curntpg.FindByName<Label>("accomodatestxt");
            accomodatestxtclr.TextColor = Color.FromHex("#212121");
            accomodatestxt = posting_Param.accomodatecnt.ToString();
            sAccomodate= posting_Param.accomodatecnt.ToString();
            contentviewvisible = false;
        }
        public void SelectbathtypeID(Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label attachedbathtxtclr = curntpg.FindByName<Label>("attachedbathtxt");
            attachedbathtxtclr.TextColor = Color.FromHex("#212121");
            attachedbathtxt = posting_Param.attachedbath;
            sAttachedbath= posting_Param.attachbathtype;
            contentviewvisible = false;
        }
        public void showpricemodelist(Posting_Param posting_Param)
        {
            var curentpage = GetCurrentPage();
            var pricemode = curentpage.FindByName<ListView>("pricemode");
            foreach (var amt in pricemodelist)
            {
                if (pricemodetxt == amt.pricemode)
                {
                    amt.checkimage = "RadioButtonChecked.png";
                    pricemodetxt = amt.pricemode;
                }
                else
                {
                    amt.checkimage = "RadioButtonUnChecked.png";
                }

            }
          
            pricemode.ItemsSource = null;
            pricemode.ItemsSource = pricemodelist;
            contentviewvisible = true;
            pricemodevisible = true;
            stayperiodvisible = false;
            accomodatesvisible = false;
            attachedbathvisible = false;

        }
        public void showstayperiodlist(Posting_Param posting_Param)
        {
            var curentpage = GetCurrentPage();
            var stayperiod = curentpage.FindByName<ListView>("stayperiod");
            foreach (var amt in stayperiodlist)
            {
                if (leasetypetxt == amt.stayperiodtxt)
                {
                    amt.checkimage = "RadioButtonChecked.png";
                    leasetypetxt = amt.stayperiodtxt;
                }
                else
                {
                    amt.checkimage = "RadioButtonUnChecked.png";
                }

            }

            stayperiod.ItemsSource = null;
            stayperiod.ItemsSource = stayperiodlist;
            contentviewvisible = true;
            pricemodevisible = false;
            stayperiodvisible = true;
            accomodatesvisible = false;
            attachedbathvisible = false;
        }
        public void showaccomodatelist(Posting_Param posting_Param)
        {
            var curentpage = GetCurrentPage();
            var accomodate = curentpage.FindByName<ListView>("accomodate");
            foreach (var amt in accomodatelist)
            {
                if (accomodatestxt == amt.accomodatecnt.ToString())
                {
                    amt.checkimage = "RadioButtonChecked.png";
                    accomodatestxt = amt.accomodatecnt.ToString();
                }
                else
                {
                    amt.checkimage = "RadioButtonUnChecked.png";
                }

            }

            accomodate.ItemsSource = null;
            accomodate.ItemsSource = accomodatelist;
            contentviewvisible = true;
            pricemodevisible = false;
            stayperiodvisible = false;
            accomodatesvisible = true;
            attachedbathvisible = false;
        }
        public void showbathtypelist(Posting_Param posting_Param)
        {
            var curentpage = GetCurrentPage();
            var bathtype = curentpage.FindByName<ListView>("bathtype");
            foreach (var amt in attachbathlist)
            {
                if (attachedbathtxt == amt.attachedbath)
                {
                    amt.checkimage = "RadioButtonChecked.png";
                    attachedbathtxt = amt.attachedbath;
                }
                else
                {
                    amt.checkimage = "RadioButtonUnChecked.png";
                }

            }

            bathtype.ItemsSource = null;
            bathtype.ItemsSource = attachbathlist;
            contentviewvisible = true;
            pricemodevisible = false;
            stayperiodvisible = false;
            accomodatesvisible = false;
            attachedbathvisible = true;
        }
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        //PostOTP
        public VMPost(Postfirst pstotp, string pinno)
        {
            try
            {
                if(pstotp.ismyneed=="1")
                {
                    pst = pstotp;
                }
                pst.adtype = Commonsettings.CAdTypeID.ToString();
                pst.RMcategory = Commonsettings.CRMCategory;
                pst.supercategoryid = pstotp.supercategoryid;
                pst.supercategoryvalue = pstotp.supercategoryvalue;
                pst.primarycategoryid = pstotp.primarycategoryid;
                pst.primarycategoryvalue = pstotp.primarycategoryvalue;
                pst.secondarycategoryid = pstotp.secondarycategoryid;
                pst.secondarycategoryvalue = pstotp.secondarycategoryvalue;
                pst.tertiarycategoryid = pstotp.tertiarycategoryid;
                pst.categoryname = pstotp.categoryname;
                pst.maincategory = pstotp.maincategory;
                pst.Locationname = pstotp.Locationname;
                pst.Newcityurl = pstotp.Newcityurl;
                pst.ContactName = pstotp.ContactName;
                pst.ContactEmail = pstotp.ContactEmail;
                pst.Countrycode = pstotp.Countrycode;
                pst.ContactPhone = pstotp.ContactPhone;
                pst.StateName = pstotp.StateName;
                pst.StateCode = pstotp.StateCode;
                pst.Zipcode = pstotp.Zipcode;
                pst.userlat = pstotp.userlat;
                pst.userlong = pstotp.userlong;
                pst.Country = pstotp.Country;
                pst.gender = pstotp.gender;
                pst.Movedate = pstotp.Movedate;
                pst.ExpRent = pstotp.ExpRent;
                pst.HideRent = pstotp.HideRent;
                pst.RentNegotiable = pstotp.RentNegotiable;
                pst.LocationAddress = pstotp.LocationAddress;
                pst.adid = pstotp.adid;
                //pst.deviceid = pstotp.deviceid;
                pst.devicename = pstotp.devicename;
                pst.devicetypeid = pstotp.devicetypeid;
                pst.mobileverify = "0";
                pst.deviceid = Commonsettings.UserDeviceId;
                pst.usertype = pstotp.usertype;
                pst.agentid = pstotp.agentid;
                // pst.Locationname = Commonsettings.Usercity;
               
                pst.City = pstotp.City;
               
                pst.devicename = Commonsettings.UserMobileOS;
                pst.postedvia = Commonsettings.UserMobileOS;
                pst.userpid = Commonsettings.UserPid;

                pst.title = pstotp.title;
                pst.description = pstotp.description;

                pst.pricemode = sPricemode;
                pst.attachedbathtype = sAttachedbath;
                pst.stayperiod = sStayperiod;
                pst.accomodate = sAccomodate;
                pst.ipaddress = ipaddress;

                
                if(!string.IsNullOrEmpty(pstotp.agentphotourl))
                {
                    pst.agentphotourl = pstotp.agentphotourl;
                }
                    if (VMCategory.Scategoryid == 11)
                    {
                        pst.businessname = businessnametxt;
                        pst.businessLicenseno = Licensetxt;
                    }
               
                if (Commonsettings.UserMobileOS == "iphone")
                {
                    pst.devicetypeid = "1";
                }
                else
                    pst.devicetypeid = "2";
                // SubmitOTPPost = new Command(Click_SubmitOTPPost);
                SubmitOTPPost = new Command(async () => await Click_SubmitOTPPost(pst));
                //ResendOTP = new Command(ClickResendOTP);
                ResendOTP = new Command(async () => await ClickResendOTP(pst));
                NewCountrycode = Countryitem;
                ChangemobileNumber = new Command(ClickChangeMObile);
                SubmitChangeMobile = new Command(async () =>await ClickSubmitChangeMobile(pst));

                _Pinno = pinno;
            }
            catch (Exception e) { }

    }
        private Plugin.Media.Abstractions.MediaFile _mediaFile;
        public static string profilepicurl = "";
        public async void uploadprofilepic()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    dialog.Toast("This is not support on your device.");
                    return;
                }
                else
                {
                    //http://cdnnrisulekhalive.blob.core.windows.net:80/cdn/roommates/images/smallimage/roommates_2019-07-23-05-05-08-771_10032620.jpeg
                    string readerdata = ""; Stream imgstream = null;
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()).ConfigureAwait(true);
                    if (_mediaFile == null) return;
                    dialog.ShowLoading("");
                    var memoryStream = new MemoryStream();
                    Stream stream = null;
                    stream = _mediaFile.GetStream();
                    stream.CopyTo(memoryStream);
                    var base64 = Convert.ToBase64String(memoryStream.ToArray());
                    byte[] myBynary = memoryStream.ToArray();
                    Random rnd = new Random(); int Randomnumber = rnd.Next(1, 1000);
                    string integerValue = Randomnumber.ToString();
                    //http://indianroommates.sulekha.com/agentprofileimages/agent/thumbs/18459913.jpg
                    //profilepicurl = "http://indianroommates.sulekha.com/agentprofileimages/agent/thumbs/" + adid;
                    var name = "roommates" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Roommates.Features.Post.ViewModels.VMPost.postadid_RM + ".jpeg";

                    //smallimage image
                    byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 150, 150);
                    MemoryStream smallimagestrm = new MemoryStream(smallimage);
                    // ms.Seek(0, SeekOrigin.Begin);
                    // ms.Write(smallimage, 0, smallimage.Length);
                    profilepicurl = await CommonMethods.Uploadimagestoblob("cdn/roommates/images/smallimage", name, smallimagestrm);
                    smallimagestrm.Dispose();
                    if(!string.IsNullOrEmpty(profilepicurl))
                    {
                        var CurrentPage = GetCurrentPage();
                        var imgpath = CurrentPage.FindByName<Image>("agentprofilepic");
                        imgpath.Source = profilepicurl;
                        agentpicselected = true;
                    }
                    //large image
                    byte[] large = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 346, 250);
                    MemoryStream largeimagestrm = new MemoryStream(large);
                    await CommonMethods.Uploadimagestoblob("cdn/roommates/images/large", name, largeimagestrm);
                    largeimagestrm.Dispose();

                    //small image
                    byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 106, 80);
                    MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                    await CommonMethods.Uploadimagestoblob("cdn/roommates/images/small", name, verysmallimgstrm);
                    verysmallimgstrm.Dispose();

                    //thumbnail image
                    byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 226, 170);
                    MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                    await CommonMethods.Uploadimagestoblob("cdn/roommates/images/thumbnail", name, thumbnailimagestrm);
                    thumbnailimagestrm.Dispose();

                    //thumbnailfull image
                    byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 770, 577);
                    MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                    await CommonMethods.Uploadimagestoblob("cdn/roommates/images/thumbnailfull", name, thumbnailfullimagestrm);
                    thumbnailfullimagestrm.Dispose();

                    //thumbnailmedium image
                    byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 400, 260);
                    MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                    await CommonMethods.Uploadimagestoblob("cdn/roommates/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                    thumbnailmediumimagestrm.Dispose();

                    //thumbs image
                    byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 80, 60);
                    MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                    await CommonMethods.Uploadimagestoblob("cdn/roommates/images/thumbs", name, thumbsimagestrm);
                    thumbsimagestrm.Dispose();

                    dialog.HideLoading();
                }
            }
            catch (Exception ee)
            {

            }

        }


        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        public async void ClickBackPage()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PopAsync();
        }
        //public async void ClickSearchPage()
        //{
        //    var curpage = GetCurrentPage();
        //    await curpage.Navigation.PushAsync(new RMSearch());
        //}

        public void ChangeGenderMale()
        {
            AnyImg = "RadioButtonUnChecked.png";
            MaleImg = "RadioButtonChecked.png";
            FeMaleImg = "RadioButtonUnChecked.png";
            Gender = "male";
        }
        public void ChangeGenderFemale()
        {
            AnyImg = "RadioButtonUnChecked.png";
            MaleImg = "RadioButtonUnChecked.png";
            FeMaleImg = "RadioButtonChecked.png";
            Gender = "female";
        }
        public void ChangeGenderAny()
        {
            AnyImg = "RadioButtonChecked.png";
            MaleImg = "RadioButtonUnChecked.png";
            FeMaleImg = "RadioButtonUnChecked.png";
            Gender = "any";
        }

        public async void GetLocationname()
        {
            try
            {
                Zipcode = "";
                var LocationAPI = RestService.For<IRMCategory>(Commonsettings.Localservices);
                Models.LocationRowData response = await LocationAPI.getlocation(LocationName);
                foreach (var item in response.ROW_DATA)
                {
                    item.citystatecode = item.city + ", " + item.statecode;
                }             
               
                OnPropertyChanged(nameof(LocationList));
                IsVisibleList = true;
                LocationList = response.ROW_DATA;
            }
            catch (Exception e) { }
        }
        public static string citytxt = "";
        public void OnLocationClick(Location obj)
        {
            try
            {
                var city = obj.citystatecode;
                LocationName = TempLocationName = city;
                citytxt = LocationName;
                Newycityurl = obj.newcityurl;
                StateName = obj.statename;
                StateCode = obj.statecode;
                Zipcode = obj.zipcode.ToString();
                Lat = obj.latitude.ToString();
                Long = obj.longitude.ToString();
                Country = obj.country;

                IsVisibleList = false;
            }
            catch (Exception e) { }
        }
        public static int adid ;
        public static int agentid ;
        public static string ordertype = "";
        
        public async void clickpostcmdfirst()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
            {
                try
                {
                    if (Validations())
                    {
                        dialog.ShowLoading("");
                        if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                        {
                            var login = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var logindata = await login.getuserpid(ContactName, ContactEmail);
                            Commonsettings.UserPid = logindata.resultinformation;
                            Commonsettings.UserName = ContactName;
                            Commonsettings.UserEmail = ContactEmail;
                            Commonsettings.UserMobileno = ContactPhone;
                        }
                        Postfirst pst = new Postfirst();
                        pst.adtype = Commonsettings.CAdTypeID.ToString();
                        pst.RMcategory = Commonsettings.CRMCategory;
                        if (ctl.ismyneed=="1" || ctl.ismyneedpayment=="1")
                        {
                            pst.rent = ctl.Rent;
                            pst.attachedbaths = ctl.attachedbaths;
                            pst.accomodate = ctl.accomodate;
                            pst.availablefrm = FrDate.ToString("MM/dd/yyyy");
                            pst.pricemode = ctl.pricemode;
                            pst.stayperiod = ctl.stayperiod;
                            pst.supercategoryid = ctl.supercategoryid;
                            pst.supercategoryvalue = ctl.supercategoryvalue;
                            pst.primarycategoryid = ctl.primarycategoryid;
                            pst.primarycategoryvalue = ctl.primarycategoryvalue;
                            pst.secondarycategoryid = ctl.secondarycategoryid;
                            pst.secondarycategoryvalue = ctl.secondarycategoryvalue;
                            pst.tertiarycategoryid = ctl.tertiarycategoryid;
                            pst.categoryname = ctl.categoryname;
                            pst.maincategory = ctl.maincategory;
                            pst.adtype = ctl.adtype;
                            pst.categoryimgurl = ctl.categoryimgurl;
                           // pst.textcolor = ctl.textcolor;

                            pst.Locationname = ctl.Locationname = LocationName;
                            pst.Newcityurl = Newycityurl;
                            pst.ContactName = ContactName;
                            pst.ContactEmail = ContactEmail;
                            pst.Countrycode = Selectcountry;
                            pst.ContactPhone = ContactPhone;
                            pst.StateName = StateName;
                            pst.State= StateName;
                            pst.StateCode = StateCode;
                            pst.Zipcode = Zipcode;
                            pst.Lat = pst.userlat = Lat;
                            pst.Long = pst.userlong = Long;
                            pst.Country = ctl.Country;
                            pst.Streetname = citytxt;
                            // pst.countflag = ctl.countflag;

                            pst.adid = ctl.adid;
                            pst.agentid = ctl.agentid;

                            pst.Movedate = FrDate.ToString("MM/dd/yyyy");
                            pst.pricefrom = ctl.pricefrom;
                            pst.priceto = ctl.priceto;
                            pst.gender = ctl.gender;

                            pst.title = ctl.title;
                            pst.description = ctl.description;
                            //pst.type = ctl.type;
                            pst.usertype = ctl.usertype;

                            pst.agentphotourl = ctl.agentphotourl;
                            pst.attachedbathtype = ctl.attachedbathtype;


                            //prefilldataadded
                            pst.service = ctl.service;
                            pst.Isagent = ctl.Isagent;
                            pst.Subcategory = ctl.Subcategory;
                            pst.Category = ctl.Category;
                            pst.Ispaid = ctl.Ispaid;
                            pst.Photoscnt = ctl.Photoscnt;
                            pst.HtmlPhotopath = ctl.HtmlPhotopath;
                            pst.Imagepath = ctl.Imagepath;
                            pst.XmlPhotopath = ctl.XmlPhotopath;
                            //pst.Adtype = ctl.Adtype;
                            pst.Additionalcity = ctl.Additionalcity;
                            pst.Citycount = ctl.Citycount;
                            pst.Citytotalamount = ctl.Citytotalamount;
                            pst.Tags = ctl.Tags;
                            //pst.Companyweburl = ctl.Companyweburl;
                            pst.url = ctl.url;
                            pst.Bestindustry = ctl.Bestindustry;
                            //pst.Minsalary = ctl.Minsalary;
                            //pst.Maxsalary = ctl.Maxsalary;
                            //pst.Qualification = ctl.Qualification;
                            //pst.Visastatus = ctl.Visastatus;
                            //pst.Experience = ctl.Experience;
                            //pst.Experiencefrom = ctl.Experiencefrom;
                            //pst.Experienceto = ctl.Experienceto;
                            //pst.Employmenttype = ctl.Employmenttype;
                            //pst.Jobtype = ctl.Jobtype;
                            //pst.Jobroleid = ctl.Jobroleid;
                            pst.Postedby = ctl.Postedby;
                            //pst.Campaignid = ctl.Campaignid;
                            //pst.Customerid = ctl.Customerid;
                            pst.Ordertype = ctl.Ordertype;
                            pst.userpid = ctl.Userpid;
                            //pst.Mailerstatus = ctl.Mailerstatus;
                            pst.Displayphone = ctl.Displayphone;
                            pst.Status = ctl.Status;
                            pst.Contributor = ctl.Contributor;
                            pst.description = ctl.description;
                            pst.Titleurl = ctl.Titleurl;
                            //pst.State = ctl.State;
                            pst.title = ctl.title;
                            
                            pst.Salarymodeid = ctl.Salarymodeid;
                            pst.Salarymode = ctl.Salarymode;
                            pst.Nationwide = ctl.Nationwide;
                            pst.Trimetros = ctl.Trimetros;
                            pst.Linkedinurl = ctl.Linkedinurl;
                            pst.Hideaddress = ctl.Hideaddress;
                            //pst.Isresumeavail = ctl.Isresumeavail;
                            //pst.Jobrole = ctl.Jobrole;
                            pst.Adpostedcnt = ctl.Adpostedcnt;
                            pst.Noofadcnt = ctl.Noofadcnt;
                            pst.Expirydate = ctl.Expirydate;
                            pst.Orderid = ctl.Orderid;
                            //pst.Industryname = ctl.Industryname;
                            //pst.Industryid = ctl.Industryid;
                            pst.Secondarycategoryvalueurl = ctl.Secondarycategoryvalueurl;
                            pst.Primarycategoryvalueurl = ctl.Primarycategoryvalueurl;
                            pst.Supercategoryvalueurl = ctl.Supercategoryvalueurl;
                            pst.Metrourl = ctl.Metrourl;
                            pst.Metro = ctl.Metro;
                            //pst.StateCode = ctl.Statecode;
                            pst.Ismobileverified = ctl.Ismobileverified;
                            pst.Folderid = ctl.Folderid;
                            pst.Amount = ctl.Amount;
                            pst.Numberofdays = ctl.Numberofdays;
                            pst.Startdate = ctl.Startdate;
                            pst.Phone = ctl.Phone;
                            pst.Email = ctl.Email;
                            pst.Enddate = ctl.Enddate;
                            pst.Islisting = ctl.Islisting;
                            pst.Freeadsflag = ctl.Freeadsflag;
                            pst.Categoryflag = ctl.Categoryflag;
                            pst.Banneramount = ctl.Banneramount;
                            pst.Bannerstatus = ctl.Bannerstatus;
                            pst.Companytype = ctl.Companytype;
                            pst.Salespersonemail = ctl.Salespersonemail;
                            pst.Couponcode = ctl.Couponcode;
                            pst.Disclosedbusiness = ctl.Disclosedbusiness;
                            pst.Gender = ctl.gender;
                            pst.Responsemail = ctl.Responsemail;
                            pst.Benefits = ctl.Benefits;
                            pst.Pendingpaycouponcode = ctl.Pendingpaycouponcode;
                            pst.Bonusamount = ctl.Bonusamount;
                            pst.Bonusdays = ctl.Bonusdays;
                            pst.Emailblastamount = ctl.Emailblastamount;
                            pst.Addonamount = ctl.Addonamount;
                            pst.businessname = ctl.businessname;
                            pst.City = citytxt;
                            pst.ismyneed = ctl.ismyneed;
                            pst.ismyneedpayment = ctl.ismyneedpayment;
                            pst.ipaddress = ipaddress;
                            pst.Category=pst.maincategory = ctl.Category;
                            if (VMCategory.Scategoryid == 11)
                            {
                                pst.businessname = BusinessName;
                                businessnametxt = BusinessName;
                                pst.businessLicenseno = LicenseNo;
                                Licensetxt = LicenseNo;
                            }
                            var curpage = GetCurrentPage();
                            var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var data = await PostSubmit.postneedfirstsubmit(pst);
                            await curpage.Navigation.PushAsync(new postsecond(pst, ctl.adid, ctl.agentid, ctl.Ordertype));
                        }
                        else
                        {
                           
                            pst.supercategoryid = ctl.supercategoryid;
                            pst.supercategoryvalue = ctl.supercategoryvalue;
                            pst.primarycategoryid = ctl.primarycategoryid;
                            pst.primarycategoryvalue = ctl.primarycategoryvalue;
                            pst.secondarycategoryid = ctl.secondarycategoryid;
                            pst.secondarycategoryvalue = ctl.secondarycategoryvalue;
                            pst.tertiarycategoryid = ctl.tertiarycategoryid;
                            pst.usertype = ctl.usertype;
                            pst.categoryname = ctl.categoryname;
                            pst.maincategory = ctl.maincategory;
                            pst.Locationname = LocationName;
                            pst.Newcityurl = Newycityurl;
                            pst.ContactName = ContactName;
                            pst.ContactEmail = ContactEmail;
                            pst.Countrycode = Selectcountry;
                            pst.ContactPhone = ContactPhone;

                            pst.ipaddress = ipaddress;


                            if (string.IsNullOrEmpty(Lat))
                            {
                                pst.userlat = Commonsettings.UserLat.ToString();
                                pst.userlong = Commonsettings.UserLong.ToString();
                                pst.StateCode = Commonsettings.Userstatecode;
                                pst.Zipcode = Commonsettings.Userzipcode;
                                pst.StateName = Commonsettings.Userstate;
                                pst.LocationAddress = Commonsettings.Usercity + " ," + Commonsettings.Userstatecode;
                                pst.Locationname = Commonsettings.Usercity;
                                pst.Country = Commonsettings.Usercountry;
                            }
                            else
                            {
                                pst.userlat = Lat;
                                pst.userlong = Long;
                                pst.StateCode = StateCode;
                                pst.Zipcode = Zipcode;
                                pst.StateName = StateName;
                                pst.LocationAddress = LocationAddress;
                                pst.Locationname = citytxt;
                                pst.Country = Country;
                            }

                            pst.deviceid = Commonsettings.UserDeviceId;
                            if (VMCategory.Scategoryid == 11)
                            {
                                pst.businessname = BusinessName;
                                businessnametxt = BusinessName;
                                pst.businessLicenseno = LicenseNo;
                                Licensetxt = LicenseNo;
                            }

                            pst.userpid = Commonsettings.UserPid;
                            var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var data = await PostSubmit.postfirstsubmit(pst);

                            var result = data.ROW_DATA;
                            adid = result.First().adid;
                            postadid_RM = result.First().adid;
                            Commonsettings.CRMCategory = "Roommates";
                            Commonsettings.CategoryType = "Roommates";
                            agentid = result.First().agentid;
                            pst.agentid = result.First().agentid;
                            ordertype = result.First().ordertype;
                            if (result != null)
                            {
                                var curpage = GetCurrentPage();
                                await curpage.Navigation.PushAsync(new postsecond(pst, result.First().adid, result.First().agentid, result.First().ordertype));
                            }
                        }
                        dialog.HideLoading();
                    }
                }
                catch (Exception e)
                {
                    dialog.HideLoading();
                    dialog.Toast("Kindly check your internet connection!");
                }
            }
            else
            {
                dialog.Toast("Kindly check your internet connection!");
            }
        }
        public void ChangeRentNegoCommand()
        {
            if (RentNegotiable == "0")
            {
                RentNegoImg = "CheckBoxChecked.png";
                RentNegotiable = "1";
            }
            else 
            {
                RentNegoImg = "CheckBoxUnChecked.png";
                RentNegotiable = "0";
            }
        }
        public void ChangeRentHideCommand()
        {
            if (HideRent == "0")
            {
                RentHideImg = "CheckBoxChecked.png";
                HideRent = "1";
            }
            else
            {
                RentHideImg = "CheckBoxUnChecked.png";
                HideRent = "0";
            }
        }
        Postfirst pst = new Postfirst();
        public static string Rentamount = "";
        public static string postlocationaddress;

        public static List<string> AdmultiImage = new List<string>();
        public static List<string> AdmultiImagetoDB = new List<string>();

        public static void Adimgurl(string imageurls,string photoname)
        {
            //string[] img = imageurls.Split(',');
            //foreach (var i in img)
            //{
            //    AdmultiImage.Add(i);
            //}
            //AdmultiImagetoDB.Add("http://usimg.sulekhalive.com/cdn/rentals/images/thumbnail/" + photoname);
            try
            {
                AdmultiImagetoDB.Add(photoname);
                AdmultiImage.Add(imageurls);
                var currentpage = getcurrentpage();
                var findimglist = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
                findimglist.ItemsSource = null;
                findimglist.ItemsSource = AdmultiImage;
            }
            catch(Exception ex)
            {
            }
        }
        private static Page getcurrentpage()
        {
            var currentpage = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
        public async void clickpostcmdsecond(CategoryList category)
        {
           
                if (Validationssecond())
                {
                pst.adtype = Commonsettings.CAdTypeID.ToString();
                pst.RMcategory = Commonsettings.CRMCategory;
                if (pst.ismyneed=="1")
                {
                    dialog.ShowLoading("");
                   
                    pst.stayperiod = sStayperiod;
                    pst.ExpRent = txtrent;
                    pst.pricemode = sPricemode;
                    pst.attachedbaths = sAttachedbath;
                    pst.accomodate = sAccomodate;
                    pst.City = citytxt;
                    pst.userlat = Lat;
                    pst.userlong = Long;
                    pst.gender = Gender;
                    pst.Locationname = citytxt;
                    if(string.IsNullOrEmpty(pst.Locationname))
                    {
                        pst.Locationname = pst.LocationAddress;
                    }
                    pst.LocationAddress = LocationAddress;
                    if (VMCategory.Scategoryid == 11)
                    {
                        pst.businessname = BusinessName;
                        pst.businessLicenseno = LicenseNo;
                        pst.BusinessAddress = LocationAddress;
                    }
                    if(adid==0)
                    {
                        adid = pst.adid;
                    }
                    var currentpg = GetCurrentPage();
                    await currentpg.Navigation.PushAsync(new PostDescription(pst, adid, agentid, ordertype));
                    dialog.HideLoading();
                }
                else
                {
                   
                    pst.supercategoryid = ctl.supercategoryid;
                    pst.supercategoryvalue = ctl.supercategoryvalue;
                    pst.primarycategoryid = ctl.primarycategoryid;
                    pst.primarycategoryvalue = ctl.primarycategoryvalue;
                    pst.secondarycategoryid = ctl.secondarycategoryid;
                    pst.secondarycategoryvalue = ctl.secondarycategoryvalue;
                    pst.tertiarycategoryid = ctl.tertiarycategoryid;
                    pst.categoryname = ctl.categoryname;
                    pst.maincategory = ctl.maincategory;
                    pst.Locationname = ctl.Locationname;
                    pst.Newcityurl = ctl.Newcityurl;
                    pst.ContactName = ctl.ContactName;
                    pst.ContactEmail = ctl.ContactEmail;
                    pst.Countrycode = ctl.Countrycode;
                    pst.ContactPhone = ctl.ContactPhone;
                    pst.StateName = ctl.StateName;
                    pst.StateCode = ctl.StateCode;
                    pst.Zipcode = ctl.Zipcode;
                    pst.userlat = ctl.Lat;
                    pst.userlong = ctl.Long;
                    pst.Country = ctl.Country;
                    pst.usertype = ctl.usertype;
                    pst.agentid = ctl.agentid;
                    pst.City = ctl.Locationname;
                    if (string.IsNullOrEmpty(Gender))
                    {
                        Gender = "any";
                        pst.gender = Gender;
                    }
                    else
                    {
                        pst.gender = Gender;
                    }
                    pst.LocationAddress = LocationAddress;
                    postlocationaddress = LocationAddress;

                    pst.Movedate = FrDate.ToString("MM/dd/yyyy");
                    pst.ExpRent = txtrent;
                    Rentamount = txtrent;
                    pst.HideRent = HideRent;
                    pst.RentNegotiable = RentNegotiable;
                    pst.adid = ctl.adid;
                    pst.mobileverify = "1";
                    pst.deviceid = Commonsettings.UserDeviceId;
                    pst.devicename = Commonsettings.UserMobileOS;
                    pst.postedvia = Commonsettings.UserMobileOS;
                    pst.userpid = Commonsettings.UserPid;

                    pst.title = AdTitle;
                    pst.description = AdDescription;

                    pst.pricemode = sPricemode;
                    pst.attachedbathtype = sAttachedbath;
                    pst.stayperiod = sStayperiod;
                    pst.accomodate = sAccomodate;
                    if (!string.IsNullOrEmpty(profilepicurl))
                    {
                        pst.agentphotourl = profilepicurl;
                    }
                    if (VMCategory.Scategoryid == 11)
                    {
                        pst.businessname = BusinessName;
                        pst.businessLicenseno = LicenseNo;
                        pst.BusinessAddress = LocationAddress;
                    }

                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        pst.devicetypeid = "1";
                    }
                    else
                    {
                        pst.devicetypeid = "2";
                    }

                    dialog.ShowLoading("");
                    var currentpg = GetCurrentPage();
                    await currentpg.Navigation.PushAsync(new PostDescription(pst, adid, agentid, ordertype));
                    dialog.HideLoading();
                }
            }
        }
      
        public async void clickpostdescription(CategoryList category)
        {
            try
            {
                pst.title = AdTitle;
                pst.description = AdDescription;

                pst.pricemode = sPricemode;
                pst.attachedbathtype = sAttachedbath;
                pst.stayperiod = sStayperiod;
                pst.accomodate = sAccomodate;
                pst.agentphotourl = ctl.agentphotourl;

                //http://usimg.sulekhalive.com/cdn/rentals/images/thumbnail/rentals_2019-06-26-11-44-04-615_9999810.jpg

                pst.imgname = string.Join(";", AdmultiImagetoDB.ToArray());

                if (!string.IsNullOrEmpty(pst.imgname))
                {
                    var multipleimguploading = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    var imguploadresult = await multipleimguploading.insertphotos(postadid_RM.ToString(), pst.imgname);
                    var inserted = imguploadresult;
                }

                pst.ipaddress = ipaddress;

                if (VMCategory.Scategoryid == 11)
                {
                    pst.businessname = businessnametxt;
                    pst.businessLicenseno = Licensetxt;
                    pst.BusinessAddress = postlocationaddress;
                }
                pst.adtype = Commonsettings.CAdTypeID.ToString();
                pst.RMcategory = Commonsettings.CRMCategory;
                if (Commonsettings.UserMobileOS == "iphone")
                {
                    pst.devicetypeid = "1";
                }
                else
                    pst.devicetypeid = "2";


                if (pst.ismyneed == "1" && pst.ismyneedpayment != "1")//edit
                {
                    dialog.ShowLoading("");
                    pst.package = pst.Ordertype;
                    pst.ContactEmail = pst.Email;
                    pst.ContactPhone = pst.Phone;
                    pst.mobileverify = "1";
                    pst.ContactName = pst.Contributor;
                    pst.LocationAddress = pst.Streetname;
                    pst.userlat = pst.Lat;
                    pst.userlong = pst.Long;
                    pst.StateName = pst.State;
                    pst.title = AdTitle;
                    pst.description = AdDescription;
                    pst.mode = "edit";
                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        pst.devicetypeid = "1";
                    }
                    else
                        pst.devicetypeid = "2";

                    pst.deviceid = Commonsettings.UserDeviceId;
                    pst.devicename = Commonsettings.UserMobileOS;
                    pst.postedvia = Commonsettings.UserMobileOS;
                    pst.userpid = Commonsettings.UserPid;
                    pst.ipaddress = ipaddress;
                    pst.HideRent = HideRent;
                    pst.RentNegotiable = RentNegotiable;
                    ////var PostSubmit1 = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    ////var data1 = await PostSubmit1.postneedfirstsubmit(pst);
                    //var needPostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    //var nddata = await needPostSubmit.postsecondsubmit(pst);
                    var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    var nddata = await PostSubmit.otppostsecondsubmit(pst);
                    // if(data!=null)
                    // {
                    var currentpage = getcurrentpage();
                    if (nddata.type == "0")
                    {
                        await currentpage.Navigation.PushAsync(new PostOTP(pst, nddata.pinno));
                    }
                    else
                    {
                        var PostSubmitpublish = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                        var publishad = await PostSubmitpublish.publishad(pst.adid.ToString());
                        await currentpage.Navigation.PushAsync(new EditThankyou());
                    }

                    dialog.HideLoading();
                }
                else if (pst.ismyneed != "1" && pst.ismyneedpayment == "1") //renew
                {
                    dialog.ShowLoading("", null);
                    pst.package = pst.Ordertype;
                    pst.ContactEmail = pst.Email;
                    pst.ContactPhone = pst.Phone;
                    pst.mobileverify = "1";
                    pst.ContactName = pst.Contributor;
                    pst.LocationAddress = pst.Streetname;
                    pst.userlat = pst.Lat;
                    pst.userlong = pst.Long;
                    pst.StateName = pst.State;
                    pst.title = AdTitle;
                    pst.description = AdDescription;
                    //pst.mode = "edit";
                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        pst.devicetypeid = "1";
                    }
                    else
                        pst.devicetypeid = "2";

                    pst.deviceid = Commonsettings.UserDeviceId;
                    pst.devicename = Commonsettings.UserMobileOS;
                    pst.postedvia = Commonsettings.UserMobileOS;
                    pst.userpid = Commonsettings.UserPid;
                    pst.ipaddress = ipaddress;
                    pst.HideRent = HideRent;
                    pst.RentNegotiable = RentNegotiable;
                    var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    var nddata = await PostSubmit.otppostsecondsubmit(pst);
                    // if(data!=null)
                    // {
                    var currentpage = getcurrentpage();
                    if (nddata.type == "0")
                    {
                        await currentpage.Navigation.PushAsync(new PostOTP(pst, nddata.pinno));
                    }
                    else
                    {
                        if (nddata.ispayment == 1)
                        {
                            await currentpage.Navigation.PushAsync(new Package_RM(pst));
                        }
                        else
                        {
                            // await curpage.Navigation.PushAsync(new Thankyou());
                            freeadpost(nddata.adid, pst.usertype);
                        }
                    }
                    dialog.HideLoading();
                }
                else
                {
                   
                    pst.supercategoryid = ctl.supercategoryid;
                    pst.supercategoryvalue = ctl.supercategoryvalue;
                    pst.primarycategoryid = ctl.primarycategoryid;
                    pst.primarycategoryvalue = ctl.primarycategoryvalue;
                    pst.secondarycategoryid = ctl.secondarycategoryid;
                    pst.secondarycategoryvalue = ctl.secondarycategoryvalue;
                    pst.tertiarycategoryid = ctl.tertiarycategoryid;
                    pst.categoryname = ctl.categoryname;
                    pst.maincategory = ctl.maincategory;
                    pst.Locationname = ctl.Locationname;
                    pst.Newcityurl = ctl.Newcityurl;
                    pst.ContactName = ctl.ContactName;
                    pst.ContactEmail = ctl.ContactEmail;
                    pst.Countrycode = ctl.Countrycode;
                    pst.ContactPhone = ctl.ContactPhone;
                    pst.usertype = ctl.usertype;
                    pst.agentid = ctl.agentid;
                    pst.Country = ctl.Country;

                    if (string.IsNullOrEmpty(ctl.gender))
                    {
                        Gender = "any";
                        pst.gender = Gender;
                    }
                    else
                    {
                        pst.gender = ctl.gender;
                    }
                    pst.LocationAddress = postlocationaddress;
                    pst.Movedate = ctl.movedate;
                    pst.ExpRent = Rentamount;
                    pst.HideRent = HideRent;
                    pst.RentNegotiable = RentNegotiable;
                    pst.adid = ctl.adid;
                    pst.mobileverify = "1";
                    pst.deviceid = Commonsettings.UserDeviceId;
                    pst.devicename = Commonsettings.UserMobileOS;
                    pst.postedvia = Commonsettings.UserMobileOS;
                    pst.userpid = Commonsettings.UserPid;


                    pst.StateName = ctl.StateName;
                    pst.StateCode = ctl.StateCode;
                    pst.Zipcode = ctl.Zipcode;
                    pst.userlat = ctl.Lat;
                    pst.userlong = ctl.Long;
                    pst.Country = ctl.Country;
                    pst.City = ctl.Locationname;


                    if (postdescriptionvalidation())
                    {
                        dialog.ShowLoading("");
                        var curpage = getcurrentpage();

                        if (pst.agentid != 0)
                        {
                            var agentdata = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var agentdatadetail = await agentdata.getagentdetails(pst.agentid);

                            if (agentdatadetail != null && agentdatadetail.ROW_DATA.Count > 0)
                            {
                                if (agentdatadetail.ROW_DATA.Last().noofadsremain != 0 && agentdatadetail.ROW_DATA.Last().totalads != 0 )
                                {
                                    if(pst.ismyneed == "1" && pst.ismyneedpayment=="1")
                                    {
                                        var PostSubmitt = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                        var datat = await PostSubmitt.otppostsecondsubmit(pst);
                                        if (datat != null)
                                        {
                                            if (datat.type == "0")
                                            {
                                                await curpage.Navigation.PushAsync(new PostOTP(pst, datat.pinno));
                                            }
                                            else
                                            {
                                                if (datat.ispayment == 1)
                                                {
                                                    await curpage.Navigation.PushAsync(new Package_RM(pst));
                                                }
                                                else
                                                {
                                                    // await curpage.Navigation.PushAsync(new Thankyou());
                                                    freeadpost(datat.adid, pst.usertype);
                                                }
                                                // await curpage.Navigation.PushAsync(new Package_RM(pst));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        pst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                        pst.package = agentdatadetail.ROW_DATA.Last().packagename;
                                        var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                        var data = await PostSubmit.otppostsecondsubmit(pst);
                                        if (data != null)
                                        {
                                            if (data.status == "1")
                                            {
                                                await curpage.Navigation.PushAsync(new Thankyou());
                                            }
                                            else
                                            {
                                                if (data.type == "0")
                                                {
                                                    await curpage.Navigation.PushAsync(new PostOTP(pst, data.pinno));
                                                }
                                                else
                                                {
                                                    var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                                                    Pkgsucess list = await api.updatepackage(adid.ToString(), agentdatadetail.ROW_DATA.Last().packagedays.ToString(), agentdatadetail.ROW_DATA.Last().packagename, agentdatadetail.ROW_DATA.Last().totalads.ToString(), pst.agentid);

                                                    if (list.resultinformation == "updated successfully")
                                                    {
                                                        var publishAd = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                                                        var publishagentad = await publishAd.publishad(adid.ToString());
                                                        if (publishagentad.resultinformation == "sucess")
                                                        {
                                                            await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.postsuccess(pst.adid.ToString(), pst.usertype, pst.package));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        await curpage.Navigation.PushAsync(new Thankyou());
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    var alert = await dialog.ConfirmAsync("Alert!", "disable one of your ads to post", "Go to myneeds", "package");
                                    if (alert)
                                    {
                                        //byte[] encData_byte = new byte[Commonsettings.UserEmail.Length];
                                        //encData_byte = System.Text.Encoding.UTF8.GetBytes(Commonsettings.UserEmail);
                                        //string encodedData = Convert.ToBase64String(encData_byte);
                                        //Device.OpenUri(new Uri("https://techjobs.sulekha.com/mobileapp/autologin.aspx?useremail=" + encodedData));
                                        await curpage.Navigation.PushAsync(new MyNeeds.Features.Views.MyNeeds());

                                    }
                                    else
                                    {
                                        pst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                        pst.package = agentdatadetail.ROW_DATA.Last().packagename;
                                        var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                        var data = await PostSubmit.otppostsecondsubmit(pst);
                                        if (data != null)
                                        {
                                            if (data.type == "0")
                                            {
                                                await curpage.Navigation.PushAsync(new PostOTP(pst, data.pinno));
                                            }
                                            else
                                            {
                                                await curpage.Navigation.PushAsync(new Package_RM(pst));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                var data = await PostSubmit.otppostsecondsubmit(pst);
                                if (data != null)
                                {
                                    if (data.type == "0")
                                    {
                                        await curpage.Navigation.PushAsync(new PostOTP(pst, data.pinno));
                                    }
                                    else
                                    {
                                        if (data.ispayment == 1)
                                        {
                                            await curpage.Navigation.PushAsync(new Package_RM(pst));
                                        }
                                        else
                                        {
                                            // await curpage.Navigation.PushAsync(new Thankyou());
                                            freeadpost(data.adid, pst.usertype);
                                        }
                                        // await curpage.Navigation.PushAsync(new Package_RM(pst));
                                    }
                                }
                            }
                        }
                        else
                        {
                            var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            Post_OTP_Result data = await PostSubmit.otppostsecondsubmit(pst);

                            if (data != null)
                            {
                                pst.agentid = data.agentid;
                                if (data.status == "1")
                                {
                                    await curpage.Navigation.PushAsync(new Thankyou());
                                }
                                else
                                {
                                    if (data.type == "0")
                                    {
                                        await curpage.Navigation.PushAsync(new PostOTP(pst, data.pinno));
                                    }
                                    else
                                    {
                                        //if (data.agentid != 0)
                                        //{
                                        //    await curpage.Navigation.PushAsync(new Package_RM(pst));
                                        //}
                                        //else
                                        //{
                                        if (data.ispayment == 1)
                                        {
                                            await curpage.Navigation.PushAsync(new Package_RM(pst));
                                        }
                                        else
                                        {
                                            // await curpage.Navigation.PushAsync(new Thankyou());
                                            freeadpost(data.adid, pst.usertype);
                                        }
                                        //}
                                    }
                                }
                            }
                        }
                        dialog.HideLoading();
                    }
                }

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        
        public async Task Click_SubmitOTPPost(Postfirst postfirst)
        {
            if (PostOTP == _Pinno)
            {
                try
                {
                    dialog.ShowLoading("");

                    if(pst.ismyneed=="1" && pst.ismyneedpayment!="1" )
                    {
                        //var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        //var data = await PostSubmit.postsecondsubmit(pst);
                        var PostOTPSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);
                        var PostSubmitpublish = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                        var publishad = await PostSubmitpublish.publishad(pst.adid.ToString());

                        var currentpage = getcurrentpage();
                        await currentpage.Navigation.PushAsync(new EditThankyou());
                    }
                    else
                    {
                        if (postfirst.agentid != 0)
                        {
                            var agentdata = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var agentdatadetail = await agentdata.getagentdetails(postfirst.agentid);
                            if (agentdatadetail != null && agentdatadetail.ROW_DATA.Count > 0)
                            {
                                if (agentdatadetail.ROW_DATA.Last().noofadsremain != 0 && agentdatadetail.ROW_DATA.Last().totalads != 0)
                                {
                                    if (pst.ismyneed == "1" && pst.ismyneedpayment == "1")
                                    {
                                        var currpage = GetCurrentPage();
                                        var PostSubmitt = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                        var datat = await PostSubmitt.otppostsecondsubmit(pst);
                                        if (datat != null)
                                        {
                                            if (datat.type == "0")
                                            {
                                                await currpage.Navigation.PushAsync(new PostOTP(pst, datat.pinno));
                                            }
                                            else
                                            {
                                                if (datat.ispayment == 1)
                                                {
                                                    await currpage.Navigation.PushAsync(new Package_RM(pst));
                                                }
                                                else
                                                {
                                                    // await curpage.Navigation.PushAsync(new Thankyou());
                                                    freeadpost(datat.adid, pst.usertype);
                                                }
                                                // await curpage.Navigation.PushAsync(new Package_RM(pst));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        postfirst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                        postfirst.package = agentdatadetail.ROW_DATA.Last().packagename;
                                        var PostOTPSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                        var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);

                                        var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                                        Pkgsucess list = await api.updatepackage(adid.ToString(), agentdatadetail.ROW_DATA.Last().packagedays.ToString(), agentdatadetail.ROW_DATA.Last().packagename, agentdatadetail.ROW_DATA.Last().totalads.ToString(), postfirst.agentid);

                                        if (list.resultinformation == "updated successfully")
                                        {
                                            var publishAd = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                                            var publishagentad = await publishAd.publishad(adid.ToString());
                                            if (publishagentad.resultinformation == "sucess")
                                            {
                                                var curpage = GetCurrentPage();
                                                await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.postsuccess(pst.adid.ToString(), pst.usertype, ""));
                                            }

                                        }
                                        else
                                        {
                                            var curpage = GetCurrentPage();
                                            await curpage.Navigation.PushAsync(new Thankyou());
                                        }
                                    }
                                }
                                else
                                {
                                    var alert = await dialog.ConfirmAsync("Alert!", "disable one of your ads to post", "Gotomyneeds", "package");
                                    if (alert)
                                    {
                                        //byte[] encData_byte = new byte[Commonsettings.UserEmail.Length];
                                        //encData_byte = System.Text.Encoding.UTF8.GetBytes(Commonsettings.UserEmail);
                                        //string encodedData = Convert.ToBase64String(encData_byte);
                                        //Device.OpenUri(new Uri("https://techjobs.sulekha.com/mobileapp/autologin.aspx?useremail=" + encodedData));
                                        var currpage = GetCurrentPage();
                                        await currpage.Navigation.PushAsync(new MyNeeds.Features.Views.MyNeeds());
                                    }
                                    else
                                    {
                                        pst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                        pst.package = agentdatadetail.ROW_DATA.Last().packagename;
                                        var PostOTPSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                        var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);
                                        var curpage = GetCurrentPage();
                                        await curpage.Navigation.PushAsync(new Package_RM(postfirst));
                                    }
                                }
                            }
                            else
                            {
                                var PostOTPSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                                var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);
                                var curpage = GetCurrentPage();
                                await curpage.Navigation.PushAsync(new Package_RM(postfirst));
                            }
                        }
                        else
                        {
                            var PostOTPSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);
                            var usertype = postfirst.usertype;
                            postfirst.agentid = data.agentid;
                            int agentid = data.agentid;
                            try
                            {
                                if (data != null)
                                {

                                    lcfsecond.postscuccesbackbtn = 1;
                                    // postadid_RM = data.adid;

                                    //agent 
                                    if (data.ispayment == 1)
                                    {
                                        var curpage = GetCurrentPage();
                                        await curpage.Navigation.PushAsync(new Package_RM(postfirst));
                                    }
                                    else
                                    {
                                        var curpage = GetCurrentPage();
                                        //await curpage.Navigation.PushAsync(new Thankyou());
                                        freeadpost(data.adid, usertype);
                                    }
                                }

                            }
                            catch (Exception e)
                            {
                            }
                        }

                    }
                    dialog.HideLoading();
                }
                catch (Exception e)
                {
                    dialog.Toast("Wrong OTP");
                    dialog.HideLoading();
                }
            }
            else
            {
                dialog.Toast("Wrong OTP");
            }
        }
        public async void freeadpost(string adid,string usertype)
        {
            try
            {
                var nodays = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Pkgsucess FreeadDuration = await nodays.getnoofdays(usertype);
                var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Pkgsucess list = await api.updatepackage(adid, FreeadDuration.resultinformation, "Free", "", 0);
                if (list.resultinformation == "updated successfully")
                {
                    var curpage = GetCurrentPage();
                    await curpage.Navigation.PushAsync(new postsuccess(adid, usertype, "Free"));
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async Task ClickResendOTP(Postfirst pst)
        {
            try
            {

                dialog.ShowLoading("");
                var resendotp = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                var data = await resendotp.getresendotp(pst.ContactPhone, pst.Countrycode);
                _Pinno = data.pinno;
                dialog.Toast("OTP has been sent to your mobile");
                dialog.HideLoading();

            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }

        public void ClickChangeMObile()
        {
            IsBusy = true;
            OTPIsBusy = false;

        }

        public async Task ClickSubmitChangeMobile(Postfirst pst)
        {
            try
            {
                if(Validationsnewphone())
                {
                    dialog.ShowLoading("");
                    pst.Countrycode = NewSelectcountry;
                    pst.ContactPhone = NewContactPhone;
                    var resendotp = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    var data = await resendotp.getresendotp(pst.ContactPhone, pst.Countrycode);
                    _Pinno = data.pinno;
                    IsBusy = false;
                    OTPIsBusy = true;
                    dialog.HideLoading();
                }
            }
            catch (Exception e) { }


        }

        public async void GetgoogleAddress()
        {
            try
            {
                string content = GooglePlacesUrl + "?input=" + LocationAddress + "&components=country:us&key=" + Commonsettings.GoogleAPIkey;
                var client = new HttpClient();
                var response = await client.PostAsync(content, new StringContent("")).ConfigureAwait(false);
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Googlecity>(data);

                OnPropertyChanged(nameof(googleadd));
                _googleadd = result.Predictions;
                OnPropertyChanged(nameof(IsBusy));
                IsBusy = true;
            }
            catch (Exception e) { }
        }
        public void OnSelectgoogleaddress(Predictions pt)
        {
            LocationAddress = TempLocationAddress = pt.Description;
            OnPropertyChanged(nameof(IsBusy));
            IsBusy = false;
        }
    }
}
