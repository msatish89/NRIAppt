using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Refit;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.Views;
using NRIApp.Rentals.Features.Post.Interface;
using NRIApp.Helpers;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using System.Windows.Input;
using NRIApp.Rentals.Features.List.Views;
using Plugin.Connectivity;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;

namespace NRIApp.Rentals.Features.Post.ViewModels
{
    public class VMRTLCF : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Dollarpattern = @"^[1-9]\d*(\.\d+)?$";
        string Namepattern = "^[0-9A-Za-z ]+$";
        public static string _Pinno;

        public Command TapBackPage { get; set; }
        public Command TapSearchPage { get; set; }
        public Command lcfcmdnext { get; set; }
        public Command ChangemobileNumber { get; set; }
        public Command SubmitChangeMobile { get; set; }
        public Command lcfcmdsubmit { get; set; }
        public Command lcfcmdskip { get; set; }
        public Command insertlcf { get; set; }
        public ICommand LocationCmd { protected set; get; }

        public Command lcfdescrptionsubmit { get; set; }


        private DateTime _FrDate = DateTime.Today;
        public DateTime FrDate
        {
            get { return _FrDate; }
            set { _FrDate = value; }
        }
        // private string _txtrent= "Expected Rent in $";
        private string _txtrent ="";
        public string txtrent
        {
            get { return _txtrent; }
            set { _txtrent = value; OnPropertyChanged(nameof(txtrent)); }
        }
        public string TempLocationName = "";
        private string _LocationName = "";
        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value.Trim();
                if (string.IsNullOrEmpty(LocationName))
                {
                    TempLocationName = "";
                }
                if (!string.IsNullOrEmpty(LocationName) && TempLocationName != LocationName)
                {
                    GetLocationname();
                }
            }
        }
        private string _Newycityurl = "";
        public string Newycityurl
        {
            get { return _Newycityurl; }
            set { _Newycityurl = value; }
        }
        private List<RTLocation> _LocationList { get; set; }
        public List<RTLocation> LocationList
        {
            get { return _LocationList; }
            set { _LocationList = value; }
        }
        private bool _isvisiblelist = false;
        public bool IsVisibleList
        {
            get { return _isvisiblelist; }
            set
            {
                _isvisiblelist = value;
                OnPropertyChanged(nameof(IsVisibleList));
            }
        }
        private string _ContactName = "";
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; OnPropertyChanged(); }
        }
        private string _ContactEmail = "";
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; OnPropertyChanged(); }
        }
        private string _ContactPhone = "";
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; OnPropertyChanged(); }
        }
        private string _Locationurl = Commonsettings.Usercityurl;
        public string Locationurl
        {
            get { return _Locationurl; }
            set
            {
                _Locationurl = value;
                OnPropertyChanged();
            }
        }
        private string _Zipcode;
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; }
        }
        bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set{_IsBusy = value;OnPropertyChanged();}
        }
        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set
            {
                _Countrycode = value;
            }
        }
        private string _LCFOTP { get; set; }
        public string LCFOTP
        {
            get { return _LCFOTP; }
            set { _LCFOTP = value.Trim(); }
        }

        bool _OTPIsBusy = true;
        public bool OTPIsBusy
        {
            get { return _OTPIsBusy; }
            set
            {
                _OTPIsBusy = value;
                OnPropertyChanged();
            }
        }
        private string _NewSelectcountry = "+1";
        public string NewSelectcountry
        {
            get { return _NewSelectcountry; }
            set
            {
                _NewSelectcountry = value;
                OnPropertyChanged(NewSelectcountry);
            }
        }
        private List<string> _NewCountrycode;
        public List<string> NewCountrycode
        {
            get { return _NewCountrycode; }
            set
            {
                _NewCountrycode = value;
            }
        }
        private string _NewContactPhone;
        public string NewContactPhone
        {
            get { return _NewContactPhone; }
            set
            {
                _NewContactPhone = value;
                OnPropertyChanged();
            }
        }
        private bool _lcfskipvisible = false;
        public bool Lcfskipvisible
        {
            get { return _lcfskipvisible; }
            set { _lcfskipvisible = value; OnPropertyChanged(); }
        }
        public static string adtitletxt = "";
        private string _AdTitle;
        public string AdTitle
        {
            get { return _AdTitle; }
            set { _AdTitle = value; OnPropertyChanged(nameof(AdTitle)); }
        }
        public static string addescriptiontxt = "";
        private string _AdDescription;
        public string AdDescription
        {
            get { return _AdDescription; }
            set { _AdDescription = value; OnPropertyChanged(nameof(AdDescription)); }
        }
        private string _countflag;
        public string countflag
        {
            get { return _countflag; }
            set { _countflag = value; OnPropertyChanged(); }
        }
        private bool _pricemodevisible = false;
        public bool pricemodevisible
        {
            get { return _pricemodevisible; }
            set { _pricemodevisible = value; OnPropertyChanged(nameof(pricemodevisible)); }
        }
        private bool _stayperiodvisible = false;
        public bool stayperiodvisible
        {
            get { return _stayperiodvisible; }
            set { _stayperiodvisible = value; OnPropertyChanged(nameof(stayperiodvisible)); }
        }
        private bool _accomodatesvisible = false;
        public bool accomodatesvisible
        {
            get { return _accomodatesvisible; }
            set { _accomodatesvisible = value; OnPropertyChanged(nameof(accomodatesvisible)); }
        }
        private bool _attachedbathvisible = false;
        public bool attachedbathvisible
        {
            get { return _attachedbathvisible; }
            set { _attachedbathvisible = value; OnPropertyChanged(nameof(attachedbathvisible)); }
        }
        private bool _selectrentvisible = false;
        public bool selectrentvisible
        {
            get { return _selectrentvisible; }
            set { _selectrentvisible = value; OnPropertyChanged(nameof(selectrentvisible)); }
        }
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _parkingfrequencyvisible = false;
        public bool parkingfrequencyvisible
        {
            get { return _parkingfrequencyvisible; }
            set { _parkingfrequencyvisible = value; OnPropertyChanged(nameof(parkingfrequencyvisible)); }
        }
        private bool _parkingtypevisible = false;
        public bool parkingtypevisible
        {
            get { return _parkingtypevisible; }
            set { _parkingtypevisible = value; OnPropertyChanged(nameof(parkingtypevisible)); }
        }
        private string _pricemodetxt = "Select price mode";
        public string pricemodetxt
        {
            get { return _pricemodetxt; }
            set { _pricemodetxt = value; OnPropertyChanged(nameof(pricemodetxt)); }
        }
        private string _leasetypetxt = "Select lease type";
        public string leasetypetxt
        {
            get { return _leasetypetxt; }
            set { _leasetypetxt = value; OnPropertyChanged(nameof(leasetypetxt)); }
        }
        private string _accomodatestxt = "Accomodates";
        public string accomodatestxt
        {
            get { return _accomodatestxt; }
            set { _accomodatestxt = value; OnPropertyChanged(nameof(accomodatestxt)); }
        }
        private string _attachedbathtxt = "No of Baths";
        public string attachedbathtxt
        {
            get { return _attachedbathtxt; }
            set { _attachedbathtxt = value; OnPropertyChanged(nameof(attachedbathtxt)); }
        }
        private bool _Parkingslotinfo;
        public bool Parkingslotinfo
        {
            get { return _Parkingslotinfo; }
            set { _Parkingslotinfo = value; OnPropertyChanged(nameof(Parkingslotinfo)); }
        }
        private bool _lcflayoutvisible;
        public bool lcflayoutvisible
        {
            get { return _lcflayoutvisible; }
            set { _lcflayoutvisible = value; OnPropertyChanged(nameof(lcflayoutvisible)); }
        }
        private string _slotarea;
        public string slotarea
        {
            get { return _slotarea; }
            set { _slotarea = value; OnPropertyChanged(nameof(slotarea)); }
        }

        private string _parkingfrequency="Select parking frequency";
        public string parkingfrequency
        {
            get { return _parkingfrequency; }
            set { _parkingfrequency = value; OnPropertyChanged(nameof(parkingfrequency)); }
        }

        private string _parkingtype="Select parking type";
        public string parkingtype
        {
            get { return _parkingtype; }
            set { _parkingtype = value; OnPropertyChanged(); }
        }
        private bool _officespaceslotareaVisible = false;
        public bool officespaceslotareaVisible
        {
            get { return _officespaceslotareaVisible; }
            set { _officespaceslotareaVisible = value; OnPropertyChanged(nameof(officespaceslotareaVisible)); }
        }
        private bool _stayperiodlblvisible = false;
        public bool stayperiodlblvisible
        {
            get { return _stayperiodlblvisible; }
            set { _stayperiodlblvisible = value; OnPropertyChanged(nameof(stayperiodlblvisible)); }
        }
        private string _officeslotarea;
        public string officeslotarea
        {
            get { return _officeslotarea; }
            set { _officeslotarea = value; OnPropertyChanged(nameof(officeslotarea)); }
        }
        private bool _noofbathvisible = false;
        public bool noofbathvisible
        {
            get { return _noofbathvisible; }
            set { _noofbathvisible = value; OnPropertyChanged(nameof(noofbathvisible)); }
        }
        public static string sPricemode = "";
        public static string sAttachedbath = "";
        public static string sStayperiod = "";
        public static string sAccomodate = "";
        public static string sParkingslotarea = "";
        public static string sParkingfrquency = "";
        public static string sParkingtype = "";
        public List<RT_Posting_Param> pricemodelistdata { get; set; }
        public List<RT_Posting_Param> attachbathlistdata { get; set; }
        public List<RT_Posting_Param> stayperiodlistdata { get; set; }
        public List<RT_Posting_Param> accomodatelistdata { get; set; }
        public List<RT_Posting_Param> parkingfrequencylistdata { get; set; }
        public List<RT_Posting_Param> parkingtypelistdata { get; set; }

        public List<RT_Posting_Param> parkingfrequencylist = new List<RT_Posting_Param>()
        {
            new RT_Posting_Param{parkingfrequency="One time",parkingfreqencyID="1",checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{parkingfrequency="Daily",parkingfreqencyID="2", checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{parkingfrequency="Weekly",parkingfreqencyID="3", checkimage="RadioButtonUnChecked.png"},
        };
        public List<RT_Posting_Param> parkingtypelist = new List<RT_Posting_Param>()
        {
            new RT_Posting_Param{parkingtype="Any",checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{parkingtype="Cars", checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{parkingtype="Motorbikes", checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{parkingtype="Others", checkimage="RadioButtonUnChecked.png"},
        };
        public List<RT_Posting_Param> pricemodelist = new List<RT_Posting_Param>()
        {
            new RT_Posting_Param{pricemode="Per Night",pricemodetype=1, checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{pricemode="Per Day",pricemodetype=2, checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{pricemode="Per Week",pricemodetype=3, checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{pricemode="Per Month",pricemodetype=4, checkimage="RadioButtonUnChecked.png"},

        };
        public List<RT_Posting_Param> attachbathlist = new List<RT_Posting_Param>()
        {
            new RT_Posting_Param{attachedbath="1",attachbathtype="1",checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{attachedbath="2",attachbathtype="2",checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{attachedbath="3",attachbathtype="3",checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{attachedbath="4",attachbathtype="4",checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{attachedbath="More than 4",attachbathtype="5",checkimage="RadioButtonUnChecked.png"}
        };
        public List<RT_Posting_Param> stayperiodlist = new List<RT_Posting_Param>()
        {
            new RT_Posting_Param { stayperiodtxt = "Long term (6+ months)", stayperiodID = "1", checkimage = "RadioButtonUnChecked.png" },
            new RT_Posting_Param { stayperiodtxt = "Short term", stayperiodID = "2", checkimage = "RadioButtonUnChecked.png" },
            new RT_Posting_Param { stayperiodtxt = "Any", stayperiodID = "0", checkimage = "RadioButtonUnChecked.png" }
        };
        public List<RT_Posting_Param> accomodatelist = new List<RT_Posting_Param>()
        {
            new RT_Posting_Param{accomodatecnt=1,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=2,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=3,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=4,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=5,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=6,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=7,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=8,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=9,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=10,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=11,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=12,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=13,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=14,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=15,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=16,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=17,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=18,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=19,checkimage="RadioButtonUnChecked.png"},
            new RT_Posting_Param{accomodatecnt=20,checkimage="RadioButtonUnChecked.png"},
        };
        public List<PopContent_RT> RentRange = new List<PopContent_RT>()
        {
             new PopContent_RT{ checkimg="RadioButtonUnChecked.png",rentamt="Any",pricefrom="0",ExpRent="100000"},
            new PopContent_RT{ checkimg="RadioButtonUnChecked.png",rentamt="Up to $500",pricefrom="0", ExpRent="500"},
            new PopContent_RT{ checkimg="RadioButtonUnChecked.png",rentamt="$500 - $1000",pricefrom="500",ExpRent="1000"},
            new PopContent_RT{ checkimg="RadioButtonUnChecked.png",rentamt="$1000 and above",pricefrom="1000",ExpRent="100000"},
        };


        public Command SubmitOTPLCF { get; set; }
        public Command ResendOTP { get; set; }
        public ICommand RentPopupTap { get; set; }

        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }


        public ICommand Taponpricemode { get; set; }
        public ICommand Taponstayperiod { get; set; }
        public ICommand Taponaccomodates { get; set; }
        public ICommand Taponattachedbathtype { get; set; }
        public ICommand Taponparkingfrequency { get; set; }
        public ICommand Taponparkingtype { get; set; }

        public ICommand Selectpricemode { get; set; }
        public ICommand Selectstayperiod { get; set; }
        public ICommand Selectaccomodate { get; set; }
        public ICommand Selectbathtype { get; set; }
        public ICommand Selectparkingfrequency { get; set; }
        public ICommand Selectparkingtype { get; set; }

        public List<PopContent_RT> Rentrangelist { get; set; }
        public ICommand Selectrent { get; set; }
        public bool Validations()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
           
            if (string.IsNullOrEmpty(txtrent))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtrent");
                myEntry.Focus();
                dialog.Toast("Enter rent amount");
                return false;
            }
            
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
            if (parkingptagID != 43)
            {
                if (string.IsNullOrEmpty(sStayperiod))
                {
                    Label myEntry = currentpage.FindByName<Label>("leasetypetxt");
                    dialog.Toast("Select stayperiod");
                    return false;
                }
            }

            if (parkingptagID == 43)
            {
                if (string.IsNullOrEmpty(sParkingslotarea))
                {
                    //Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                    dialog.Toast("Enter parking area in sqfeet");
                    return false;
                }
                if (Convert.ToInt32(sParkingslotarea) < 1)
                {
                    //Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                    dialog.Toast("Enter minimum sqfeet value");
                    return false;
                }
                if (string.IsNullOrEmpty(sParkingfrquency))
                {
                    //Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                    dialog.Toast("Select parking frequency");
                    return false;
                }
                if (string.IsNullOrEmpty(sParkingtype))
                {
                    //Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                    dialog.Toast("Select parking type");
                    return false;
                }
            }
            else
            {
                if (officespacetagID == 18 || officespacetagID == 19 || officespacetagID == 20)
                {
                    if (string.IsNullOrEmpty(officeslotarea))
                    {
                        // Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                        dialog.Toast("Enter area in sqfeet");
                        return false;
                    }
                    if (Convert.ToInt32(officeslotarea) < 1)
                    {
                        //Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                        dialog.Toast("Enter minimum sqfeet value");
                        return false;
                    }
                }
                else
                {
                    if(hoteltagID!=11)
                    {
                        if (string.IsNullOrEmpty(officeslotarea))
                        {
                            // Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                            dialog.Toast("Enter area in sqfeet");
                            return false;
                        }
                        if (Convert.ToInt32(officeslotarea) < 1)
                        {
                            //Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                            dialog.Toast("Enter minimum sqfeet value");
                            return false;
                        }
                    }
                    
                    if (string.IsNullOrEmpty(sAccomodate))
                    {
                        Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                        dialog.Toast("Select number of accomodates");
                        return false;
                    }

                    if (parkingptagID != 43)
                    {
                        if (string.IsNullOrEmpty(sAttachedbath))
                        {
                            Label myEntry = currentpage.FindByName<Label>("attachedbathtxt");
                            dialog.Toast("Select number of baths");
                            return false;
                        }
                    }
                }

            }
            return result;
        }
        public bool Validationssecond() {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(Zipcode))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtlocation");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Enter your location";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(ContactName.Trim()))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactname");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your name";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (!Regex.IsMatch(ContactName, Namepattern))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactname");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter a valid name";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(ContactEmail))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactemail");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your email id";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactemail");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter a valid email id";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(ContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Enter your mobile number";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (ContactPhone.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Minimum 10 Digits required";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (!CheckValidPhone(ContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Enter valid mobile number";
                myEntry.PlaceholderColor = Color.Red;
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
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your mobile number";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (NewContactPhone.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtnewcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Minimum 10 Digits required";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            else if (!CheckValidPhone(NewContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtnewcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter valid mobile number";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            return result;
        }

        public bool lcfdescriptionvalidation()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(AdTitle))
            {
                dialog.Toast("Enter your AD title");
                return false;
            }
            if (string.IsNullOrEmpty(AdDescription))
            {
                dialog.Toast("Enter your description");
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

        RTCategoryList ctl = new RTCategoryList();

        RTResponse res = new RTResponse();
        //lcf
        public static int parkingptagID = 0;
        public static int officespacetagID = 0;
        public static int hoteltagID = 0;
        public VMRTLCF(RTCategoryList clist)
        {
            if (clist.ismyneed != "1")
            {
                sPricemode = "";
                sAttachedbath = "";
                sStayperiod = "";
                sAccomodate = "";
                sParkingslotarea = "";
                sParkingfrquency = "";
                sParkingtype = "";
                officespacetagID = 0;
                parkingptagID = 0;
                hoteltagID = 0;
            }

            try
            {

                if (clist.primarycategoryid == 43)
                {
                    parkingptagID = clist.primarycategoryid;
                    Parkingslotinfo = true;
                    lcflayoutvisible = false;
                }
                else
                {
                    Parkingslotinfo = false;
                    lcflayoutvisible = true;
                }
                if (clist.primarycategoryvalue == "Parking Garage")
                {
                    parkingptagID = clist.primarycategoryid;
                    Parkingslotinfo = true;
                    lcflayoutvisible = false;
                    officespaceslotareaVisible = false;
                    stayperiodlblvisible = false;
                    noofbathvisible = false;
                }
                else
                {
                    Parkingslotinfo = false;
                    lcflayoutvisible = true;
                    officespaceslotareaVisible = true;
                    stayperiodlblvisible = true;
                    noofbathvisible = true;
                }
                if (clist.primarycategoryvalue == "Office Space" || clist.primarycategoryvalue == "Retail Outlet" || clist.primarycategoryvalue == "Others")
                {
                    officespacetagID = clist.primarycategoryid;
                    Parkingslotinfo = false;
                    lcflayoutvisible = false;
                    officespaceslotareaVisible = true;
                    noofbathvisible = false;
                }
                else
                {
                    noofbathvisible = true;
                }
                if (clist.primarycategoryvalue == "Hotels")
                {
                    officespaceslotareaVisible = false;
                    hoteltagID = 11;
                }
                Rentrangelist = RentRange;
                if (clist.primarycategoryvalue == "Single Family Home")
                    res.primarycategoryvalue = "1";
                if (clist.primarycategoryvalue == "Apartment")
                    res.primarycategoryvalue = "2";
                if (clist.primarycategoryvalue == "Condo")
                    res.primarycategoryvalue = "3";
                if (clist.primarycategoryvalue == "Office Space")
                    res.primarycategoryvalue = "4";
                if (clist.primarycategoryvalue == "Retail Outlet")
                    res.primarycategoryvalue = "5";
                if (clist.primarycategoryvalue == "Town house")
                    res.primarycategoryvalue = "6";
                if (clist.primarycategoryvalue == "Homes")
                    res.primarycategoryvalue = "8";
                if (clist.primarycategoryvalue == "Houses")
                    res.primarycategoryvalue = "9";
                if (clist.primarycategoryvalue == "Hostels")
                    res.primarycategoryvalue = "10";
                if (clist.primarycategoryvalue == "Hotels")
                    res.primarycategoryvalue = "11";
                if (clist.primarycategoryvalue == "Basement Apartment")
                    res.primarycategoryvalue = "12";
                if (clist.primarycategoryvalue == "Parking Garage")
                    res.primarycategoryvalue = "13";
                if (clist.primarycategoryvalue == "Others")
                    res.primarycategoryvalue = "7";
                if (clist.secondarycategoryvalue == "1 Bed")
                    res.secondarycategoryvalue = "1";
                if (clist.secondarycategoryvalue == "2 Beds")
                    res.secondarycategoryvalue = "2";
                if (clist.secondarycategoryvalue == "3 Beds")
                    res.secondarycategoryvalue = "3";
                if (clist.secondarycategoryvalue == "4+ Beds")
                    res.secondarycategoryvalue = "4";


                
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.secondarycategoryvalue = clist.secondarycategoryvalue;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.supercategoryvalue = clist.supercategoryvalue;
                TapBackPage = new Command(ClickBackPage);
                TapSearchPage = new Command(ClickSearchPage);
                //lcfcmdnext = new Command<RTCategoryList>(clicklcfcmdnext);
                PopupContentTap = new Command(Closepopupcontent);
                ContentViewTap = new Command(CloseContentview);
                RentPopupTap = new Command<PopContent_RT>(Viewrentrange);

                //New LCF
                pricemodelistdata = pricemodelist;
                stayperiodlistdata = stayperiodlist;
                accomodatelistdata = accomodatelist;
                attachbathlistdata = attachbathlist;
                Rentrangelist = RentRange;

                parkingtypelistdata = parkingtypelist;
                parkingfrequencylistdata = parkingfrequencylist;

                Taponpricemode = new Command<RT_Posting_Param>(showpricemodelist);
                Taponstayperiod = new Command<RT_Posting_Param>(showstayperiodlist);
                Taponaccomodates = new Command<RT_Posting_Param>(showaccomodatelist);
                Taponattachedbathtype = new Command<RT_Posting_Param>(showbathtypelist);
                RentPopupTap = new Command<PopContent_RT>(Viewrentrange);
                Taponparkingfrequency = new Command<RT_Posting_Param>(showparkingfrequencylist);
                Taponparkingtype = new Command<RT_Posting_Param>(showparkingtypelist);

                Selectpricemode = new Command<RT_Posting_Param>(SelectpricemodeID);
                Selectstayperiod = new Command<RT_Posting_Param>(SelectstayperiodID);
                Selectaccomodate = new Command<RT_Posting_Param>(Selectaccomodatecnt);
                Selectbathtype = new Command<RT_Posting_Param>(SelectbathtypeID);
                Selectrent = new Command<PopContent_RT>(Closerentpopup);
                Selectparkingfrequency = new Command<RT_Posting_Param>(SelectparkingfrequencyID);
                Selectparkingtype = new Command<RT_Posting_Param>(SelectparkingtypeID);

                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);

                lcfcmdnext = new Command(async () => await clicklcfcmdnext(res));
                lcfcmdskip = new Command(async () => await TapOnSkip(res));

                if(clist.ismyneed=="1")
                {
                    ctl = clist;
                    needsprefill(clist);
                }

            }
            catch (Exception e) { }
        }

        public static string RTpriceto = "";
        public static string RTpricefrom = "";


        public void needsprefill(RTCategoryList clist)
        {
            try
            {
                //date rent pricemode leasetype accomodates noofbaths areainsqft parkingslot/frequency
                res.Locationname = LocationName = TempLocationName = clist.Locationname;
                OnPropertyChanged(LocationName);
                res.City = city = clist.City;
                res.Newcityurl = newcityurl = clist.Newcityurl;
                res.ContactName = ContactName = clist.Contributor;
                res.ContactEmail = ContactEmail = clist.Email;
                res.CountryCode = Countrycode = clist.Countrycode;
                res.ContactPhone = ContactPhone = clist.Phone;
                res.StateName = state = clist.StateName;
                res.statecode = statecode = clist.Statecode;
                res.Zipcode = Zipcode = zipcode = clist.Zipcode;
                res.userlat = clist.Long;
                res.Country = country = clist.Country;
                res.countflag = clist.countflag.ToString();
                res.title = clist.title;
                res.description = clist.description;
                res.adid = clist.adid.ToString();
                res.agentid = clist.agentid.ToString();
                res.pricefrom = clist.pricefrom;
                res.priceto = clist.priceto;
                //res.gender = clist.gender;
                res.ExpRent = clist.Rent;
                res.attachedbaths = sAttachedbath = clist.attachedbaths;

                res.availablefrm = clist.availablefrm;
               // FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);


                res.Streetname = locationaddress = clist.Streetname;
                res.statecode = statecode = clist.Statecode; //check statecde
                res.ismobileverified = clist.Ismobileverified;
                res.Folderid = clist.Folderid;
                res.Amount = clist.Amount;
                res.Numberofdays = clist.Numberofdays;
                res.Startdate = clist.Startdate;
                res.Enddate = clist.Enddate;
                //res.Islisting = clist.Islisting;
                res.Freeadsflag = clist.Freeadsflag;
                res.Couponcode = clist.Couponcode;
                res.Disclosedbusiness = clist.Disclosedbusiness;
                //res.Gender = Gender = clist.Gender;
                res.Pendingpaycouponcode = clist.Pendingpaycouponcode;
                res.Bonusamount = clist.Bonusamount;
                res.Bonusdays = clist.Bonusdays;
                res.Emailblastamount = clist.Emailblastamount;
                res.Addonamount = clist.Addonamount;
                res.businessname = clist.businessname;
                res.City = clist.City;


                //from myneedspage

                //LocationName = clist.City + ", " + clist.statecode;
                //TempLocationName = clist.City + ", " + clist.statecode;
                //OnPropertyChanged(nameof(LocationName));
                //OnPropertyChanged(nameof(TempLocationName));
                city = clist.Locationname;
                Newycityurl = clist.Newcityurl;
                //BusinessName = clist.businessname;
                //businessnametxt = clist.businessname;

                res.ismyneed = clist.ismyneed;
                res.Movedate = clist.Movedate;
               
                    //prefill
                    txtrent = clist.Rent;
                pricemodetxt = sPricemode = clist.pricemode;
                leasetypetxt = sStayperiod = clist.stayperiod;
                if (clist.stayperiod == "1")
                {
                    leasetypetxt = "Long term (6+ months)";
                    sStayperiod = clist.stayperiod;
                }
                else if (clist.stayperiod == "2")
                {
                    leasetypetxt = "Short term";
                    sStayperiod = clist.stayperiod;
                }
                else if (clist.stayperiod == "0")
                {
                    leasetypetxt = "Any";
                    sStayperiod = clist.stayperiod;
                }
                accomodatestxt = sAccomodate = clist.accomodate;
                attachedbathtxt = sAttachedbath = clist.noofbaths;
                officeslotarea = clist.area;
                slotarea = clist.area;
                parkingfrequency = clist.stayperiod;
                parkingtype = clist.accomodate;
                res.userlat = clist.Lat;
                res.userlong = clist.Long;
                res.ismyneed = clist.ismyneed;
                res.ismyneedpayment = clist.ismyneedpayment;
                latitude = float.Parse(res.userlat);
                longitude = float.Parse(res.userlong);
                res.Ordertype = clist.Ordertype;
                if (!string.IsNullOrEmpty(clist.availablefrm))
                {
                    //2019-09-20
                    //if (clist.availablefrm.Contains("/"))
                    //{
                    //    FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);
                    //}
                    //else
                    //{
                    //    FrDate = DateTime.ParseExact(clist.availablefrm, "MM-dd-yyyy", null);
                    //}

                    try
                    {
                        if (clist.availablefrm.Contains("/"))
                        {
                            FrDate = DateTime.ParseExact(clist.availablefrm, "yyyy/MM/dd/", null);
                        }
                        else
                        {
                            FrDate = DateTime.ParseExact(clist.availablefrm, "yyyy-MM-dd", null);
                        }
                    }
                    catch(Exception ex)
                    {
                        FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);
                    }
                }
                
            }
            catch(Exception ex)
            {

            }

        }
        public void SelectpricemodeID(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label pricemodetxtclr = currentpage.FindByName<Label>("pricemodetxt");
            pricemodetxtclr.TextColor = Color.FromHex("#212121");
            pricemodetxt = posting_Param.pricemode;
            sPricemode = posting_Param.pricemode;

            contentviewvisible = false;
        }
        public void SelectparkingtypeID(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label parkingtypetxtclr = currentpage.FindByName<Label>("parkingtype");
            parkingtypetxtclr.TextColor= Color.FromHex("#212121");
            parkingtype = posting_Param.parkingtype;
            sParkingtype = posting_Param.parkingtype;
            contentviewvisible = false;
        }
        public void SelectparkingfrequencyID(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label parkingfrequencytxtclr = currentpage.FindByName<Label>("parkingfrequency");
            parkingfrequencytxtclr.TextColor= Color.FromHex("#212121");
            parkingfrequency = posting_Param.parkingfrequency;
            sParkingfrquency = posting_Param.parkingfreqencyID;
            contentviewvisible = false;
        }
        public void SelectstayperiodID(RT_Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label stayperiodtxtclr = curntpg.FindByName<Label>("leasetypetxt");
            stayperiodtxtclr.TextColor = Color.FromHex("#212121");
            leasetypetxt = posting_Param.stayperiodtxt;
            sStayperiod = posting_Param.stayperiodID;
            contentviewvisible = false;
        }
        public void Selectaccomodatecnt(RT_Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label accomodatestxtclr = curntpg.FindByName<Label>("accomodatestxt");
            accomodatestxtclr.TextColor = Color.FromHex("#212121");
            accomodatestxt = posting_Param.accomodatecnt.ToString();
            sAccomodate = posting_Param.accomodatecnt.ToString();
            contentviewvisible = false;
        }
        public void SelectbathtypeID(RT_Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label attachedbathtxtclr = curntpg.FindByName<Label>("attachedbathtxt");
            attachedbathtxtclr.TextColor = Color.FromHex("#212121");
            attachedbathtxt = posting_Param.attachedbath;
            sAttachedbath = posting_Param.attachbathtype;
            contentviewvisible = false;
        }
        public void Closerentpopup(PopContent_RT popContent)
        {
            var currentpg = GetCurrentPage();
            txtrent = popContent.rentamt;
            res.pricefrom = popContent.pricefrom;
            res.ExpRent = popContent.ExpRent;
            Label pricemodetxtclr = currentpg.FindByName<Label>("txtrent");
            pricemodetxtclr.TextColor = Color.FromHex("#212121");
            contentviewvisible = false;
        }
        public void showpricemodelist(RT_Posting_Param posting_Param)
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
            selectrentvisible = false;
            parkingtypevisible = false;
            parkingfrequencyvisible = false;

        }
        public void showparkingfrequencylist(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            var parkingfreq = currentpage.FindByName<ListView>("parkingfrequencytype");
            foreach(var freq in parkingfrequencylist)
            {
                if(parkingfrequency==freq.parkingfrequency)
                {
                    freq.checkimage = "RadioButtonChecked.png";
                    parkingfrequency = freq.parkingfrequency;
                    sParkingfrquency = freq.parkingfreqencyID;
                }
                else
                {
                    freq.checkimage = "RadioButtonUnChecked.png";
                }
            }
            parkingfreq.ItemsSource = null;
            parkingfreq.ItemsSource = parkingfrequencylist;
            contentviewvisible = true;
            pricemodevisible = false;
            stayperiodvisible = false;
            accomodatesvisible = false;
            attachedbathvisible = false;
            selectrentvisible = false;
            parkingtypevisible = false;
            parkingfrequencyvisible = true;
        }
        public void showparkingtypelist(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            var parkingtyp = currentpage.FindByName<ListView>("parkingtyep");
            foreach (var type in parkingtypelist)
            {
                if (parkingtype == type.parkingtype)
                {
                    type.checkimage = "RadioButtonChecked.png";
                    parkingtype = type.parkingtype;
                    sParkingtype = type.parkingtype;
                }
                else
                {
                    type.checkimage = "RadioButtonUnChecked.png";
                }
            }
            parkingtyp.ItemsSource = null;
            parkingtyp.ItemsSource = parkingtypelist;
            contentviewvisible = true;
            pricemodevisible = false;
            stayperiodvisible = false;
            accomodatesvisible = false;
            attachedbathvisible = false;
            selectrentvisible = false;
            parkingtypevisible = true;
            parkingfrequencyvisible = false;

        }
        public void showstayperiodlist(RT_Posting_Param posting_Param)
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
            selectrentvisible = false;
            parkingtypevisible = false;
            parkingfrequencyvisible = false;
        }
        public void showaccomodatelist(RT_Posting_Param posting_Param)
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
            selectrentvisible = false;
            parkingtypevisible = false;
            parkingfrequencyvisible = false;
        }
        public void showbathtypelist(RT_Posting_Param posting_Param)
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
            selectrentvisible = false;
            parkingtypevisible = false;
            parkingfrequencyvisible = false;
        }
      
        public void Viewrentrange(PopContent_RT popContent)
        {
            var curentpage = GetCurrentPage();
            var rentlistview = curentpage.FindByName<ListView>("listdata");
            foreach (var amt in Rentrangelist)
            {

                if (txtrent == amt.rentamt)
                {
                    amt.checkimg = "RadioButtonChecked.png";
                    txtrent = amt.rentamt;
                }
                else
                {
                    amt.checkimg = "RadioButtonUnChecked.png";
                }

            }

            rentlistview.ItemsSource = null;
            rentlistview.ItemsSource = RentRange;
            contentviewvisible = true;
            pricemodevisible = false;
            stayperiodvisible = false;
            accomodatesvisible = false;
            attachedbathvisible = false;
            selectrentvisible = true;
            parkingtypevisible = false;
            parkingfrequencyvisible = false;
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        //frame
        public void Closepopupcontent()
        {
            var currentpg = GetCurrentPage();
            var contentlayout = currentpg.FindByName<ContentView>("contentlayout");
            contentlayout.IsVisible = true;
        }
        public void CloseContentview()
        {
            var currentpg = GetCurrentPage();
            var contentlayout = currentpg.FindByName<ContentView>("contentlayout");
            contentlayout.IsVisible = false;
        }
        public void rentpopup()
        {
            var currentpg = GetCurrentPage();
            var contentlayout = currentpg.FindByName<ContentView>("contentlayout");
            contentlayout.IsVisible = true;
        }
        //LcfotpRT
        public VMRTLCF(RTResponse clist, string pinno,string diff)
        {
            try
            {
                if(clist.ismyneed=="1")
                {
                    res = clist;
                }
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.primarycategoryvalue = clist.primarycategoryvalue;
                res.supercategoryvalue = clist.supercategoryvalue;
                res.pricefrom = clist.pricefrom;
                

                res.adtype = Commonsettings.CAdTypeID;
                if (Commonsettings.CRMCategory != null)
                    res.RMcategory = Commonsettings.CRMCategory;
                else
                res.RMcategory = clist.RMcategory;
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                //res.gender = clist.gender;
                res.Movedate = clist.Movedate;
                // res.ExpRent = txtrent;
                res.ExpRent = clist.ExpRent;
                res.Locationname = clist.Locationname;
                //CityName = res.Locationname;
                res.Newcityurl = clist.Newcityurl;
                res.ContactName = clist.ContactName;
                res.ContactEmail = clist.ContactEmail;
                res.CountryCode = clist.CountryCode;
                res.ContactPhone = clist.ContactPhone;
                res.devicename = clist.devicename;
                res.devicetypeid = clist.devicetypeid;
                res.adid = clist.adid;
                res.Shortdesc = clist.Shortdesc;
                res.userpid = Commonsettings.UserPid;
                res.devicename = Commonsettings.UserMobileOS;
                res.postedvia = Commonsettings.UserMobileOS;
                res.ipaddress = ipaddress;
                res.deviceid = Commonsettings.UserDeviceId;
                //if (string.IsNullOrEmpty(latitude.ToString()) || (latitude.ToString() == "0"))
                if (latitude != 0)
                {
                    res.userlat = latitude.ToString();
                    res.userlong = longitude.ToString();
                    res.Zipcode = zipcode.ToString();
                    res.statecode = statecode;
                    res.Country = country;
                    res.City = city;
                    res.LocationAddress =res.Locationname= locationaddress;
                }
                else
                {
                    res.userlat = Commonsettings.UserLat.ToString();
                    res.userlong = Commonsettings.UserLong.ToString();
                    res.Zipcode = Commonsettings.Userzipcode;
                    res.statecode = Commonsettings.Userstatecode;
                    res.Country = Commonsettings.Usercountry;
                    res.City = Commonsettings.Usercity;
                    res.LocationAddress= Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                }
                res.pricemode = sPricemode;
                res.accomodate = sAccomodate;
                res.stayperiod = sStayperiod;
                res.attachedbaths = sAttachedbath;
                res.lcftype = clist.lcftype;

                res.countflag = clist.countflag;
                res.title = adtitletxt;
                res.description = addescriptiontxt;
                res.pricemode = sPricemode;
                res.attachedbathtype = sAttachedbath;
                res.accomodate = sAccomodate;
                res.sqrfeet = sParkingslotarea;
                if (!string.IsNullOrEmpty(sParkingfrquency))
                {
                    res.stayperiod = sParkingfrquency;
                    res.accomodationtype = sParkingtype;
                }
                // SubmitOTPLCF = new Command(Click_SubmitOTPLCF);
                SubmitOTPLCF = new Command(async () => await Click_SubmitOTPLCF(res));
                ResendOTP = new Command(async () => await ClickResendOTP(res));

                //NewCountrycode = Countrycode;
                ChangemobileNumber = new Command(ClickChangeMObile);
                SubmitChangeMobile = new Command(async () => await ClickSubmitChangeMobile(res));

                _Pinno = pinno;
                if(clist.ismyneed=="1")
                {
                    res = clist;
                    postsecondprefill(clist);
                }
            }
            catch (Exception e) { }

        }
        //lcfsecond
        public static string SelectedCityName = "";
        public VMRTLCF(RTResponse clist, DateTime frdate, string textrent,string countflag)
        {
            try
            {
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.primarycategoryvalue = clist.primarycategoryvalue;
                res.secondarycategoryvalue = clist.secondarycategoryvalue;
                res.supercategoryvalue = clist.supercategoryvalue;
                //res.gender = clist.gender;
                res.Movedate = clist.Movedate;
                res.pricefrom = clist.pricefrom;
                res.ExpRent = clist.ExpRent;
                res.Newcityurl = Locationurl;
                res.countflag = countflag;
                res.CountryCode = clist.CountryCode;
                res.ContactPhone = clist.ContactPhone;
               
                
                TapBackPage = new Command(ClickBackPage);
                TapSearchPage = new Command(ClickSearchPage);

                if (countflag == "1")
                {
                    Lcfskipvisible = true;
                }
                else
                {
                    Lcfskipvisible = false;
                }
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
             
                if (latitude != 0)
                {
                    res.userlat = latitude.ToString();
                    res.userlong = longitude.ToString();
                    res.statecode = statecode;
                    res.Zipcode = zipcode.ToString();
                    res.City = city;
                    res.Country = country;
                    res.LocationAddress = res.Locationname = locationaddress;
                }
                else
                {
                    res.userlat = Commonsettings.UserLat.ToString();
                    res.userlong = Commonsettings.UserLong.ToString();
                    res.statecode = Commonsettings.Userstatecode;
                    res.Zipcode = Commonsettings.Userzipcode.ToString();
                    res.City = Commonsettings.Usercity;
                    res.Country = Commonsettings.Usercountry;
                    res.LocationAddress= Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                }
                SelectedCityName = TempLocationName;
                OnPropertyChanged(nameof(Countrycode));
                FrDate = frdate;
              //  txtrent = textrent;
                LocationCmd = new Command<RTLocation>(OnLocationClick);

                lcfdescrptionsubmit = new Command(async () => await lcfpostdescription(res));
                lcfcmdsubmit = new Command(async () => await clicklcfcmdsubmit(res));
                lcfcmdskip = new Command(async () => await TapOnSkip(res));
                insertlcf=new Command(async () =>await insertintoLCF(res));
                if(clist.ismyneed =="1")
                {
                    res = clist;
                    postsecondprefill(clist);
                }
            }
            catch (Exception e) { }
        }
        public void postsecondprefill(RTResponse needcatlist)
        {
            try
            {
                if(needcatlist.ispostdesc=="1")
                {
                    adtitletxt = AdTitle = needcatlist.title;
                    addescriptiontxt = AdDescription = needcatlist.description;
                }
                else
                {
                    LocationName = TempLocationName = res.Streetname;
                    IsVisibleList = false;
                    IsBusy = false;
                    OnPropertyChanged(nameof(IsBusy));
                    res.Locationname = LocationName = needcatlist.Locationname;
                    res.Newcityurl = newcityurl = needcatlist.Newcityurl;
                    res.ContactName = ContactName = needcatlist.ContactName;
                    res.ContactEmail = ContactEmail = needcatlist.ContactEmail;
                    res.CountryCode = Countrycode = needcatlist.CountryCode;

                    res.ContactPhone = ContactPhone = needcatlist.ContactPhone;
                    res.StateName = state = needcatlist.StateName;
                    res.statecode = statecode = needcatlist.statecode;
                    res.Zipcode = Zipcode = needcatlist.Zipcode;
                    res.userlat = needcatlist.userlat;
                    res.userlong = needcatlist.userlong;
                    res.Country = needcatlist.Country;
                    res.ismyneedpayment = needcatlist.ismyneedpayment;
                    //res.Movedate = FrDate.ToString("MM/dd/yyyy");
                    res.Ordertype = needcatlist.Ordertype;
                    if (!string.IsNullOrEmpty(needcatlist.availablefrm))
                    {
                        if (needcatlist.availablefrm.Contains("/"))
                        {
                            FrDate = DateTime.ParseExact(needcatlist.availablefrm, "MM/dd/yyyy", null);
                        }
                        else
                        {
                            FrDate = DateTime.ParseExact(needcatlist.availablefrm, "yyyy-mm-dd", null);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public static string lcftype = "";
        public async Task insertintoLCF(RTResponse categoryList)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                if (Validationssecond())
                {
                    dialog.ShowLoading("");
                    if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                    {
                        var login = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var logindata = await login.getuserpid(ContactName, ContactEmail);
                        Commonsettings.UserPid = logindata.resultinformation;
                        Commonsettings.UserName = ContactName;
                        Commonsettings.UserEmail = ContactEmail;
                        Commonsettings.UserMobileno = ContactPhone;
                    }
                    res.adtype = Commonsettings.CRTAdTypeID;
                    if (string.IsNullOrEmpty(res.categoryname))
                        res.RMcategory = categoryList.primarycategoryvalue;
                    else
                        res.RMcategory = Commonsettings.CRTCategory;

                    res.supercategoryid = categoryList.supercategoryid;
                    res.primarycategoryid = categoryList.primarycategoryid;
                    res.secondarycategoryid = categoryList.secondarycategoryid;
                    res.primarycategoryvalue = categoryList.primarycategoryvalue;
                    res.supercategoryvalue = categoryList.supercategoryvalue;
                    res.maincategory = categoryList.maincategory;
                    if (!string.IsNullOrEmpty(VMRTLCF.RTpriceto))
                    {
                        res.ExpRent = VMRTLCF.RTpriceto;
                    }
                    if (!string.IsNullOrEmpty(VMRTLCF.RTpricefrom))
                    {
                        res.pricefrom = VMRTLCF.RTpricefrom;
                    }
                    res.Movedate = categoryList.Movedate;
                    res.Locationname = LocationName;
                    res.City = LocationName;
                    if (!string.IsNullOrEmpty(Locationurl))
                        res.Newcityurl = Locationurl;
                    else
                        Locationurl = categoryList.Newcityurl;
                    res.ContactName = ContactName;
                    res.ContactEmail = ContactEmail;
                    res.CountryCode = Countrycode;
                    res.ContactPhone = ContactPhone;
                    res.mobileverify = "1";
                    res.ismobileverified = "1";

                    res.devicename = Commonsettings.UserMobileOS;
                    res.postedvia = Commonsettings.UserMobileOS;
                    res.userpid = Commonsettings.UserPid;
                    res.ipaddress = ipaddress;
                    res.deviceid = Commonsettings.UserDeviceId;

                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        res.devicetypeid = "1";
                    }
                    else
                        res.devicetypeid = "2";
                    
                    if (latitude != 0)
                    {
                        res.userlat = latitude.ToString();
                        res.userlong = longitude.ToString();
                        res.Zipcode = zipcode.ToString();
                        res.statecode = statecode;
                        res.City = city;
                        res.Country = country;
                        res.LocationAddress = res.Locationname = locationaddress;
                    }
                    else
                    {
                        res.userlat = Commonsettings.UserLat.ToString();
                        res.userlong = Commonsettings.UserLong.ToString();
                        res.Zipcode = Commonsettings.Userzipcode;
                        res.statecode = Commonsettings.Userstatecode;
                        res.City = Commonsettings.Usercity;
                        res.Country = Commonsettings.Usercountry;
                        res.LocationAddress = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    }
                    res.pricemode = sPricemode;
                    res.accomodate = sAccomodate;
                    res.stayperiod = sStayperiod;
                    res.attachedbaths = sAttachedbath;

                    res.countflag = categoryList.countflag;
                    res.Movedate = FrDate.ToString("yyyy-MM-dd");
                    res.title = AdTitle;
                    adtitletxt = res.title;
                    res.description = AdDescription;
                    addescriptiontxt = res.description;
                    res.pricemode = sPricemode;
                    res.attachedbathtype = sAttachedbath;
                    res.accomodate = sAccomodate;

                    if (!string.IsNullOrEmpty(sParkingfrquency))
                    {
                        res.stayperiod = sParkingfrquency;
                        res.accomodationtype = sParkingtype;
                        res.sqrfeet = sParkingslotarea;
                    }
                    else
                    {
                        res.stayperiod = sStayperiod;
                    }
                    res.lcftype = "lcf";

                    var LCFSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                    var data = await LCFSubmit.OTPResponselcfsubmit(res, res.mobileverify);

                    if (data != null)
                    {
                        var curpage = GetCurrentPage();
                        data.type = "1";
                        if (data.type == "0")
                        {
                            res.isResponseotp = 1;
                            await curpage.Navigation.PushAsync(new LcfotpRT(res, data.pinno));
                        }
                        else
                        {
                                Sortby_RT.orderby = "";
                                res.orderby = "date";
                                Filter_RT.priceto = "";
                                await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                        }
                    }
                    dialog.HideLoading();
                }

            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection!");
                string msg = e.Message.ToString();
            }
        }
        public async Task TapOnSkip(RTResponse categoryList)
        {
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(categoryList.Newcityurl))
            {
                Locationurl = Commonsettings.Usercityurl;
                OnPropertyChanged(nameof(Locationurl));
            }
            else
            {
                Locationurl = categoryList.Newcityurl;
                OnPropertyChanged(nameof(Locationurl));
            }
            Sortby_RT.orderby = "";
            categoryList.orderby = "date";
            Filter_RT.priceto = "";
            await currentpage.Navigation.PushAsync(new RentalList(categoryList, Newycityurl));
        }
        public VMRTLCF(RTResponse clist, string pinno)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                res.adtype = Commonsettings.CRTAdTypeID;
                res.RMcategory = Commonsettings.CRTCategory;
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.Movedate = clist.Movedate;
                res.ExpRent = clist.ExpRent;
                res.Locationname = clist.Locationname;
                res.Newcityurl = clist.Newcityurl;
                res.ContactName = clist.ContactName;
                res.ContactEmail = clist.ContactEmail;
                res.CountryCode = clist.CountryCode;
                res.ContactPhone = clist.ContactPhone;
                res.deviceid = clist.deviceid;
                res.devicename = clist.devicename;
                res.devicetypeid = clist.devicetypeid;
                res.ipaddress = ipaddress;

               // SubmitOTPLCF = new Command(Click_SubmitOTPLCF);
                SubmitOTPLCF = new Command(async () => await Click_SubmitOTPLCF(res));
                ResendOTP = new Command(async () => await ClickResendOTP(res));

                //NewCountrycode = Countrycode;
                ChangemobileNumber = new Command(ClickChangeMObile);
                SubmitChangeMobile = new Command(async () => await ClickSubmitChangeMobile(res));

                _Pinno = pinno;
            }
            catch (Exception e)
            { }
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
        public async void ClickSearchPage()
        {
            var curpage = GetCurrentPage();
          //  await curpage.Navigation.PushAsync(new RTSearch());
        }
        public async Task clicklcfcmdnext(RTResponse catlist)
        {
            try
            {
                if (string.IsNullOrEmpty(slotarea))
                {
                    sParkingslotarea = officeslotarea;
                }
                else
                {
                    sParkingslotarea = slotarea;
                }
                if (Validations())
                {
                    dialog.ShowLoading("");
                    if(ctl.ismyneed =="1")
                    {
                        if (string.IsNullOrEmpty(Newycityurl))
                        {
                            Newycityurl = Commonsettings.Usercityurl;
                        }
                        res.Movedate = FrDate.ToString("yyyy-MM-dd");
                        //catlist.pricefrom = "0";
                        res.ExpRent = txtrent;
                        res.Newcityurl = Locationurl;
                        res.pageno = 1;
                        res.sqrfeet = sParkingslotarea;
                        res.stayperiod = sStayperiod;
                        res.accomodate = sAccomodate;
                        res.pricemode = sPricemode;
                        res.attachedbathtype = sAttachedbath;
                        if (parkingptagID == 43)
                        {
                            res.accomodationtype = sParkingtype;
                            res.stayperiod = sParkingfrquency;
                        }

                        if (string.IsNullOrEmpty(catlist.furnishing))
                            catlist.furnishing = "999";
                        await Task.Delay(1000);
                        var curpage = GetCurrentPage();
                        await curpage.Navigation.PushAsync(new lcfsecond(res, FrDate, txtrent, countflag));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Newycityurl))
                        {
                            Newycityurl = Commonsettings.Usercityurl;
                        }
                        catlist.Movedate = FrDate.ToString("yyyy-MM-dd");
                        //catlist.pricefrom = "0";
                        catlist.ExpRent = txtrent;
                        catlist.Newcityurl = Locationurl;
                        catlist.pageno = 1;
                        res.sqrfeet = sParkingslotarea;
                        if (parkingptagID == 43)
                        {
                            res.accomodationtype = sParkingtype;
                            res.stayperiod = sParkingfrquency;
                        }

                        if (string.IsNullOrEmpty(catlist.furnishing))
                            catlist.furnishing = "999";
                        //if (string.IsNullOrEmpty(catlist.petpolicy))
                        //    catlist.petpolicy = "999";
                        //var rentalapi = RestService.For<Features.List.Interface.IListRentalCategory>(Commonsettings.RoommatesAPI);
                        //Features.List.Models.ListRentalCategory response = await rentalapi.GetRentalList(catlist, Locationurl);
                        //if (response.ROW_DATA.Count > 0)
                        //{
                        //    countflag = "1";
                        //}
                        //else
                        //{
                        //    countflag = "0";
                        //}
                        await Task.Delay(1000);
                        var curpage = GetCurrentPage();
                        await curpage.Navigation.PushAsync(new lcfsecond(catlist, FrDate, txtrent, countflag));
                    }
                    dialog.HideLoading();
                }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        public async void GetLocationname()
        {
            try
            {
                Zipcode = "";
                var LocationAPI = RestService.For<IRTCategory>(Commonsettings.Localservices);
                var response = await LocationAPI.getlocation(LocationName);
                foreach (var item in response.ROW_DATA)
                {
                    item.citystatecode = item.city + ", " + item.statecode;
                }
                _LocationList = response.ROW_DATA;
                _isvisiblelist = true;
                _Locationurl = newcityurl;
                OnPropertyChanged(nameof(Locationurl));
                OnPropertyChanged(nameof(LocationList));
                OnPropertyChanged(nameof(IsVisibleList));
            }
            catch (Exception ed)
            {

            }
        }
        static string city = string.Empty, statecode = string.Empty, state = string.Empty, country = string.Empty, newcityurl = string.Empty;
        static float latitude = 0.00f, longitude = 0.00f;static string zipcode;
        public static string locationaddress = "";
        public void OnLocationClick(RTLocation ld)
        {
            city = ld.city; statecode = ld.statecode; state = ld.statename; latitude = ld.latitude;
            longitude = ld.longitude; zipcode = ld.zipcode; country = ld.country; newcityurl = ld.newcityurl;
            _LocationName = TempLocationName = ld.city + ", " + ld.statecode;
            _Locationurl = newcityurl;
            Zipcode = ld.zipcode;
            locationaddress = ld.city + ", " + ld.statecode;
            OnPropertyChanged(nameof(Locationurl));
            OnPropertyChanged(nameof(LocationName));
            OnPropertyChanged(nameof(IsVisibleList));
            _isvisiblelist = false;
        }


        public static string contactnumber = "";
        public async Task clicklcfcmdsubmit(RTResponse categoryList)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                if (Validationssecond())
                {
                    dialog.ShowLoading("");
                    if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                    {
                        var login = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var logindata = await login.getuserpid(ContactName, ContactEmail);
                        Commonsettings.UserPid = logindata.resultinformation;
                        Commonsettings.UserName = ContactName;
                        Commonsettings.UserEmail = ContactEmail;
                        Commonsettings.UserMobileno = ContactPhone;
                    }
                    //RTResponse res = new RTResponse();
                    res.adtype = Commonsettings.CRTAdTypeID;
                    if (string.IsNullOrEmpty(res.categoryname))
                        res.RMcategory = categoryList.primarycategoryvalue;
                    else
                        res.RMcategory = Commonsettings.CRTCategory;
                    
                    res.supercategoryid = categoryList.supercategoryid;
                    res.primarycategoryid = categoryList.primarycategoryid;
                    res.secondarycategoryid = categoryList.secondarycategoryid;
                    res.primarycategoryvalue = categoryList.primarycategoryvalue;
                    res.secondarycategoryvalue = categoryList.secondarycategoryvalue;
                    // res.categoryname = ctl.categoryname;
                    res.maincategory = categoryList.maincategory;
                    if(!string.IsNullOrEmpty(VMRTLCF.RTpriceto))
                    {
                        res.ExpRent = VMRTLCF.RTpriceto;
                    }
                    if (!string.IsNullOrEmpty(VMRTLCF.RTpricefrom))
                    {
                        res.pricefrom = VMRTLCF.RTpricefrom;
                    }
                    //res.ExpRent=txtrent;
                    // res.Movedate= FrDate.ToString("MM/dd/yyyy");
                    //res.Movedate = FrDate.ToString("yyyy-MM-dd");
                    res.Movedate = categoryList.Movedate;
                    res.Locationname = LocationName;
                    if (!string.IsNullOrEmpty(Locationurl))
                        res.Newcityurl = Locationurl;
                    else
                        Locationurl = categoryList.Newcityurl;
                    //res.Newcityurl = Locationurl;
                    res.ContactName = ContactName;
                    res.ContactEmail = ContactEmail;
                    res.CountryCode = Countrycode;
                    res.ContactPhone = ContactPhone;
                    contactnumber = res.ContactPhone;
                    res.mobileverify = "1";
                    res.ismobileverified = "1";
                    
                    res.devicename = Commonsettings.UserMobileOS;
                    res.postedvia = Commonsettings.UserMobileOS;
                    res.ipaddress = ipaddress;
                    res.userpid = Commonsettings.UserPid;
                    res.deviceid = Commonsettings.UserDeviceId;

                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        res.devicetypeid = "1";
                    }
                    else
                        res.devicetypeid = "2";

                    //if (string.IsNullOrEmpty(latitude.ToString()) || (latitude.ToString() == "0"))
                    if (latitude != 0)
                    {
                        res.userlat = latitude.ToString();
                        res.userlong = longitude.ToString();
                        res.Zipcode = zipcode.ToString();
                        res.statecode = statecode;
                        res.Country = country;
                        res.LocationAddress = locationaddress;
                    }
                    else
                    {
                        res.userlat = Commonsettings.UserLat.ToString();
                        res.userlong = Commonsettings.UserLong.ToString();
                        res.Zipcode = Commonsettings.Userzipcode;
                        res.statecode = Commonsettings.Userstatecode;
                        res.Country = Commonsettings.Usercountry;
                        res.LocationAddress= Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    }
                    res.pricemode = sPricemode;
                    res.accomodate = sAccomodate;
                    res.stayperiod = sStayperiod;
                    res.attachedbaths = sAttachedbath;
                   
                    var currentpage = GetCurrentPage();
                    res.pageno = 1;
                    if(res.ismyneed=="1")
                    {
                        //res.lcftype = "post";
                        res.ispostdesc = "1";
                       // await currentpage.Navigation.PushAsync(new LcfDescription_RT(res, FrDate));
                    }
                    await currentpage.Navigation.PushAsync(new LcfDescription_RT(res, FrDate));
                   
                    dialog.HideLoading();
                }

            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection!");
                string msg = e.Message.ToString();
            }
        }
        RTPostFirst pst = new RTPostFirst();
        public async Task lcfpostdescription(RTResponse categoryList)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                if (lcfdescriptionvalidation())
                {
                    dialog.ShowLoading("");
                    //RTResponse res = new RTResponse();
                    //if(categoryList.ismyneed=="1")
                    if (string.IsNullOrEmpty(res.categoryname))
                    {
                        res.categoryname = res.secondarycategoryvalue;
                    }
                    res.package = res.Ordertype;
                    res.mode = "edit";
                    res.lcftype = "post";
                    if (res.ismyneed == "1" && res.ismyneedpayment != "1")
                    {
                        res.title = AdTitle;
                        res.description = AdDescription;
                        var LCFSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await LCFSubmit.OTPResponselcfsubmit(res, res.mobileverify);
                      
                       
                        if (data != null)
                        {
                            var curpage = GetCurrentPage();
                            if (data.type == "0")
                            {
                                await curpage.Navigation.PushAsync(new LcfotpRT(res, data.pinno));
                            }
                            else
                            {
                                var PostSubmitpublish = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                                var publishad = await PostSubmitpublish.publishad(res.adid);
                                // await curpage.Navigation.PushAsync(new lcfsuccess("success", 2, res, Locationurl));
                                await curpage.Navigation.PushAsync(new lcfthankyou());
                            }
                        }
                    }
                    else if (res.ismyneedpayment == "1")
                    {
                        res.title = AdTitle;
                        res.description = AdDescription;
                        res.lcftype = "post";
                        if (string.IsNullOrEmpty(res.categoryname))
                        {
                            res.categoryname = res.secondarycategoryvalue;
                        }
                        res.package = res.Ordertype;
                        var curpage = GetCurrentPage();
                        var LCFSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await LCFSubmit.OTPResponselcfsubmit(res, res.mobileverify);
                        if (data.type == "0")
                        {
                            await curpage.Navigation.PushAsync(new LcfotpRT(res, data.pinno));
                        }
                        else
                        {
                            if (data.ispayment == 1 && data.Result != "exists")
                            {
                                pst.ContactName = res.ContactName;
                                pst.ContactEmail = res.ContactEmail;
                                pst.ContactPhone = res.ContactPhone;
                                pst.StateCode = res.statecode;
                                pst.Countrycode = res.CountryCode;
                                pst.City = res.City;
                                pst.Country = res.Country;
                                pst.Zipcode = res.Zipcode;
                                pst.LocationAddress = res.LocationAddress;
                                pst.userpid = res.userpid;
                                pst.adid = Convert.ToInt32(res.adid);
                                await curpage.Navigation.PushAsync(new Package_RT(pst));
                            }
                            else
                            {
                                Sortby_RT.orderby = "";
                                res.orderby = "date";
                                Filter_RT.priceto = "";
                                // await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                                if (data.Result == "exists")
                                {
                                    await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                                }
                                else
                                {
                                    freeadpost(data.adid, "1");
                                }

                            }
                        }
                    }
                    else
                    {
                        res.adtype = Commonsettings.CRTAdTypeID;
                        if (string.IsNullOrEmpty(res.categoryname))
                            res.RMcategory = categoryList.primarycategoryvalue;
                        else
                            res.RMcategory = Commonsettings.CRTCategory;

                        res.supercategoryid = categoryList.supercategoryid;
                        res.primarycategoryid = categoryList.primarycategoryid;
                        res.secondarycategoryid = categoryList.secondarycategoryid;
                        res.primarycategoryvalue = categoryList.primarycategoryvalue;
                        res.secondarycategoryvalue = categoryList.secondarycategoryvalue;
                        res.supercategoryvalue = categoryList.supercategoryvalue;
                        // res.categoryname = ctl.categoryname;
                        res.maincategory = categoryList.maincategory;
                        if (!string.IsNullOrEmpty(VMRTLCF.RTpriceto))
                        {
                            res.ExpRent = VMRTLCF.RTpriceto;
                        }
                        if (!string.IsNullOrEmpty(VMRTLCF.RTpricefrom))
                        {
                            res.pricefrom = VMRTLCF.RTpricefrom;
                        }
                        //res.ExpRent=txtrent;
                        // res.Movedate= FrDate.ToString("MM/dd/yyyy");
                        res.Movedate = categoryList.Movedate;
                        res.Locationname = LocationName;
                        res.City = LocationName;
                        if (!string.IsNullOrEmpty(Locationurl))
                            res.Newcityurl = Locationurl;
                        else
                            Locationurl = categoryList.Newcityurl;
                        //res.Newcityurl = Locationurl;
                        res.ContactName = ContactName;
                        res.ContactEmail = ContactEmail;
                        res.CountryCode = categoryList.CountryCode;
                        res.ContactPhone = categoryList.ContactPhone;
                        res.mobileverify = "1";
                        res.ismobileverified = "1";

                        res.devicename = Commonsettings.UserMobileOS;
                        res.postedvia = Commonsettings.UserMobileOS;
                        res.userpid = Commonsettings.UserPid;
                        res.ipaddress = ipaddress;
                        res.deviceid = Commonsettings.UserDeviceId;

                        if (Commonsettings.UserMobileOS == "iphone")
                        {
                            res.devicetypeid = "1";
                        }
                        else
                            res.devicetypeid = "2";

                        //if (string.IsNullOrEmpty(latitude.ToString()) || (latitude.ToString() == "0"))
                        if (latitude != 0 && !string.IsNullOrEmpty(latitude.ToString()))
                        {
                            res.userlat = latitude.ToString();
                            res.userlong = longitude.ToString();
                            res.Zipcode = zipcode.ToString();
                            res.statecode = statecode;
                            res.City = city;
                            res.Country = country;
                            res.LocationAddress = res.Locationname = locationaddress;
                        }
                        else
                        {
                            res.userlat = Commonsettings.UserLat.ToString();
                            res.userlong = Commonsettings.UserLong.ToString();
                            res.Zipcode = Commonsettings.Userzipcode;
                            res.statecode = Commonsettings.Userstatecode;
                            res.City = Commonsettings.Usercity;
                            res.Country = Commonsettings.Usercountry;
                            res.LocationAddress = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        }
                        res.pricemode = sPricemode;
                        res.accomodate = sAccomodate;
                        res.stayperiod = sStayperiod;
                        res.attachedbaths = sAttachedbath;

                        res.countflag = categoryList.countflag;
                        res.Movedate = FrDate.ToString("yyyy-MM-dd");
                        res.title = AdTitle;
                        adtitletxt = res.title;
                        res.description = AdDescription;
                        addescriptiontxt = res.description;
                        res.pricemode = sPricemode;
                        res.attachedbathtype = sAttachedbath;
                        res.accomodate = sAccomodate;
                        res.lcftype = "post";
                        if (!string.IsNullOrEmpty(sParkingfrquency))
                        {
                            res.stayperiod = sParkingfrquency;
                            res.accomodationtype = sParkingtype;
                        }
                        else
                        {
                            res.stayperiod = sStayperiod;
                        }
                        res.sqrfeet = sParkingslotarea;

                        var LCFSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await LCFSubmit.OTPResponselcfsubmit(res, res.mobileverify);

                        if (data != null)
                        {
                            var curpage = GetCurrentPage();
                            if (data.type == "0")
                            {
                                await curpage.Navigation.PushAsync(new LcfotpRT(res, data.pinno));
                            }
                            else
                            {
                                if (data.ispayment == 1 && data.Result != "exists")
                                {
                                    pst.ContactName = res.ContactName;
                                    pst.ContactEmail = res.ContactEmail;
                                    pst.ContactPhone = res.ContactPhone;
                                    pst.StateCode = res.statecode;
                                    pst.Countrycode = res.CountryCode;
                                    pst.City = res.City;
                                    pst.Country = res.Country;
                                    pst.Zipcode = res.Zipcode;
                                    pst.LocationAddress = res.LocationAddress;
                                    pst.userpid = res.userpid;
                                    pst.adid = Convert.ToInt32(data.lcfpostadid);
                                    await curpage.Navigation.PushAsync(new Package_RT(pst));
                                }
                                else
                                {
                                    Sortby_RT.orderby = "";
                                    res.orderby = "date";
                                    Filter_RT.priceto = "";
                                    // await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                                    if (data.Result == "exists")
                                    {
                                        await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                                    }
                                    else
                                    {
                                        freeadpost(data.adid, "1");
                                    }

                                }
                            }
                        }
                    }
                    dialog.HideLoading();
                }

            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection!");
                string msg = e.Message.ToString();
            }
        }
        public async Task  Click_SubmitOTPLCF(RTResponse res)
        {
            if (LCFOTP == _Pinno)
            {
                try
                {
                    dialog.ShowLoading("");
                    var LCFOTPSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                    // var LCFOTPSubmit = RestService.For<IRTOTPLCF>(Commonsettings.RoommatesAPI);
                    res.ismobileverified = "1";
                    if (string.IsNullOrEmpty(Locationurl))
                    {
                        Locationurl = res.Newcityurl;
                    }
                    else
                    {
                        Locationurl = res.Newcityurl;
                    }
                    var curpage = GetCurrentPage();
                    if (res.ismyneed=="1"&&res.ismyneedpayment!="1")
                    {
                        var LCFOTPSubmitneed = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await LCFOTPSubmitneed.OTPResponselcfsubmit(res, "0");
                        try
                        {
                            if (data != null)
                            {
                                var PostSubmitpublish = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                                var publishad = await PostSubmitpublish.publishad(res.adid.ToString());
                                await curpage.Navigation.PushAsync(new Roommates.Features.Post.Views.EditThankyou());
                            }

                        }
                        catch (Exception e) { }
                    }
                    else
                    {
                        var data = await LCFOTPSubmit.OTPResponselcfsubmit(res, "0");
                        try
                        {

                            if (data != null)
                            {
                                lcfsecond.postsuccessbackbtn = 1;
                                if (data.ispayment == 1 && data.Result != "exists")
                                {
                                    pst.ContactName = res.ContactName;
                                    pst.ContactEmail = res.ContactEmail;
                                    pst.ContactPhone = res.ContactPhone;
                                    pst.StateCode = res.statecode;
                                    pst.Countrycode = res.CountryCode;
                                    pst.City = res.City;
                                    pst.Country = res.Country;
                                    pst.Zipcode = res.Zipcode;
                                    pst.LocationAddress = res.LocationAddress;
                                    pst.userpid = res.userpid;
                                    //pst.adid = data.allrespid;
                                    try
                                    {
                                        pst.adid = Convert.ToInt32(data.lcfpostadid);
                                        string addd = pst.adid.ToString();

                                        
                                        await curpage.Navigation.PushAsync(new Package_RT(pst));
                                    }
                                    catch (Exception ec)
                                    {
                                        dialog.HideLoading();
                                    }
                                }
                                else
                                {
                                    // await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                                    freeadpost(data.adid, "1");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dialog.HideLoading();
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
            var nodays = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
            Pkgsucess_RT FreeadDuration = await nodays.getnoofdays(usertype);
            var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
            Pkgsucess_RT list = await api.updatepackage(adid, FreeadDuration.resultinformation, "Free","",0);
            if (list.resultinformation == "updated successfully")
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new postsuccess(adid,usertype,"Free"));
            }
        }
        public async Task ClickResendOTP(RTResponse res)
        {
            try
            {

                dialog.ShowLoading("");
                var resendotp = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                var data = await resendotp.getresendotp(res.ContactPhone, res.CountryCode);
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

        public async Task ClickSubmitChangeMobile(RTResponse res)
        {
            try
            {
                if (Validationsnewphone())
                {
                    dialog.ShowLoading("");
                    res.CountryCode = NewSelectcountry;
                    res.ContactPhone = NewContactPhone;
                    var resendotp = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                    var data = await resendotp.getresendotp(NewContactPhone, NewSelectcountry);
                    _Pinno = data.pinno;
                    IsBusy = false;
                    OTPIsBusy = true;
                    dialog.HideLoading();
                }
            }
            catch (Exception e) { }

        }

        public VMRTLCF(int isResponseotp,RTResponse clist, string pinno)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.primarycategoryvalue = clist.primarycategoryvalue;
                res.supercategoryvalue = clist.supercategoryvalue;
                res.pricefrom = clist.pricefrom;


                res.adtype = Commonsettings.CAdTypeID;
                if (Commonsettings.CRMCategory != null)
                    res.RMcategory = Commonsettings.CRMCategory;
                else
                    res.RMcategory = clist.RMcategory;
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                //res.gender = clist.gender;
                res.Movedate = clist.Movedate;
                // res.ExpRent = txtrent;
                res.ExpRent = clist.ExpRent;
                res.Locationname = clist.Locationname;
                //CityName = res.Locationname;
                res.Newcityurl = clist.Newcityurl;
                res.ContactName = clist.ContactName;
                res.ContactEmail = clist.ContactEmail;
                res.CountryCode = clist.CountryCode;
                res.ContactPhone = clist.ContactPhone;
                res.devicename = clist.devicename;
                res.devicetypeid = clist.devicetypeid;
                res.adid = clist.adid;
                res.Shortdesc = clist.Shortdesc;
                res.userpid = Commonsettings.UserPid;
                res.devicename = Commonsettings.UserMobileOS;
                res.postedvia = Commonsettings.UserMobileOS;
                res.ipaddress = ipaddress;
                res.deviceid = Commonsettings.UserDeviceId;
                //if (string.IsNullOrEmpty(latitude.ToString()) || (latitude.ToString() == "0"))
                if (latitude != 0)
                {
                    res.userlat = latitude.ToString();
                    res.userlong = longitude.ToString();
                    res.Zipcode = zipcode.ToString();
                    res.statecode = statecode;
                    res.Country = country;
                    res.City = city;
                    res.LocationAddress = res.Locationname = locationaddress;
                }
                else
                {
                    res.userlat = Commonsettings.UserLat.ToString();
                    res.userlong = Commonsettings.UserLong.ToString();
                    res.Zipcode = Commonsettings.Userzipcode;
                    res.statecode = Commonsettings.Userstatecode;
                    res.Country = Commonsettings.Usercountry;
                    res.City = Commonsettings.Usercity;
                    res.LocationAddress = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                }
                //fromdetailpage
                res.allowpermission = clist.allowpermission;
                res.Shortdesc = clist.Shortdesc;
                res.pricemode = sPricemode;
                res.accomodate = sAccomodate;
                res.stayperiod = sStayperiod;
                res.attachedbaths = sAttachedbath;
                res.lcftype = clist.lcftype;

                res.countflag = clist.countflag;
                res.title = adtitletxt;
                res.description = addescriptiontxt;
                res.pricemode = sPricemode;
                res.attachedbathtype = sAttachedbath;
                res.accomodate = sAccomodate;
                res.sqrfeet = sParkingslotarea;
                if (!string.IsNullOrEmpty(sParkingfrquency))
                {
                    res.stayperiod = sParkingfrquency;
                    res.accomodationtype = sParkingtype;
                }
              
                // SubmitOTPLCF = new Command(Click_SubmitOTPLCF);
                SubmitOTPLCF = new Command(async () => await Click_ResponseSubmitOTPLCF(res));
               // ResendOTP = new Command(ClickResendOTP);
                ResendOTP = new Command(async () => await ClickResendOTP(res));

                //NewCountrycode = Countrycode;
                ChangemobileNumber = new Command(ClickChangeMObile);
                //SubmitChangeMobile = new Command(ClickSubmitChangeMobile);
                SubmitChangeMobile = new Command(async () =>await ClickSubmitChangeMobile(res));

                _Pinno = pinno;
            }
            catch (Exception e) { }

        }
        public async Task Click_ResponseSubmitOTPLCF(RTResponse res)
        {
            if (LCFOTP == _Pinno)
            {
                try
                {
                    dialog.ShowLoading("");
                    var LCFOTPSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                    // var LCFOTPSubmit = RestService.For<IRTOTPLCF>(Commonsettings.RoommatesAPI);
                    res.ismobileverified = "1";
                    if (string.IsNullOrEmpty(Locationurl))
                    {
                        Locationurl = res.Newcityurl;
                    }
                    else
                    {
                        Locationurl = res.Newcityurl;
                    }
                    var curpage = GetCurrentPage();
                    if (res.lcftype == "post" && res.ismyneed == "1" && res.ismyneedpayment!="1")
                    {
                        var LCFOTPSubmitneed = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await LCFOTPSubmitneed.OTPResponselcfsubmit(res, "0");
                        try
                        {
                            if (data != null)
                            {
                                var PostSubmitpublish = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                                var publishad = await PostSubmitpublish.publishad(res.adid.ToString());
                                await curpage.Navigation.PushAsync(new Roommates.Features.Post.Views.EditThankyou());
                            }

                        }
                        catch (Exception e) { }
                    }
                    else
                    {
                        var data = await LCFOTPSubmit.OTPResponselcfsubmit(res, "0");
                        try
                        {
                            
                            if (data != null)
                            {
                                if (res.lcftype == "lcf")
                                {
                                    await curpage.Navigation.PushAsync(new Thankyou());
                                }
                                else
                                {
                                    await curpage.Navigation.PushAsync(new Thankyou_RT());
                                }
                            }
                        }
                        catch (Exception e)
                        {
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
    }
}
