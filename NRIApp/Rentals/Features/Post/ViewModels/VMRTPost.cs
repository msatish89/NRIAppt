using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Refit;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.Views;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Interface;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Windows.Input;
using Plugin.Media.Abstractions;
using System.IO;
using Plugin.Media;

namespace NRIApp.Rentals.Features.Post.ViewModels
{
   public class VMRTPost : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Dollarpattern = @"^[1-9]\d*(\.\d+)?$";
        string Namepattern = "^[0-9A-Za-z ]+$";
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        private const string GooglePlacesUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        public static int postadid_RT;


        public static string _Pinno;
        public string Gender;

        public Command TapBackPage { get; set; }
        public Command TapSearchPage { get; set; }
        public Command GenderMaleCommand { get; set; }
        public Command GenderFemaleCommand { get; set; }
        public Command GenderAnyCommand { get; set; }
        public ICommand postcmdsecond { get; set; }
        public ICommand postdescrptionsubmit { get; set; }
        public ICommand SubmitOTPPost { get; set; }
        public Command ResendOTP { get; set; }
        public Command ChangemobileNumber { get; set; }
        public Command SubmitChangeMobile { get; set; }
        public Command Selectgoogleaddress { get; set; }
        public Command<RTLocation> LocationCmd { get; set; }
        public Command postcmdfirst { get; set; }
        public Command selectProfilephoto { get; set; }
        public Command RentNegoCommand { get; set; }
        public Command RentHideCommand { get; set; }

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

        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }

        public Command Adphotos { get; set; }
        public Command<string> Removeimg { get; set; }
        public Command Removeprofilepic { get; set; }
        private DateTime _FrDate = DateTime.Today;
        public DateTime FrDate
        {
            get { return _FrDate; }
            set { _FrDate = value; }
        }
        private string _MaleImg;
        public string MaleImg
        {
            get { return _MaleImg; }
            set { _MaleImg = value; OnPropertyChanged(nameof(MaleImg)); }
        }
        private string _FeMaleImg;
        public string FeMaleImg
        {
            get { return _FeMaleImg; }
            set { _FeMaleImg = value; OnPropertyChanged(nameof(FeMaleImg)); }
        }

        private string _AnyImg;
        public string AnyImg
        {
            get { return _AnyImg; }
            set { _AnyImg = value; OnPropertyChanged(nameof(AnyImg)); }
        }
        private string _txtrent;
        public string txtrent
        {
            get { return _txtrent; }
            set
            {
                _txtrent = value;
                OnPropertyChanged(nameof(txtrent));
            }
        }
        
        public string TempLocationName = "";
        private string _LocationName = "";
        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value;
                OnPropertyChanged(nameof(LocationName));
                if (!(string.IsNullOrEmpty(LocationName)) && (TempLocationName != LocationName))
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
        private string _StateName;
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }
        private string _StateCode;
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        private string _Zipcode;
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; }
        }
        private string _Lat;
        public string Lat { get { return _Lat; } set { _Lat = value; } }

        private string _Long;
        public string Long { get { return _Long; } set { _Long = value; } }

        private string _Country;
        public string Country { get { return _Country; } set { _Country = value; } }

        bool _IsVisibleList = false;
        public bool IsVisibleList
        {
            get { return _IsVisibleList; }
            set { _IsVisibleList = value; OnPropertyChanged(); }
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

        public static string businessnametxt = "";
        private string _BusinessName;
        public string BusinessName
        {
            get { return _BusinessName; }
            set { _BusinessName = value; OnPropertyChanged(nameof(BusinessName)); }
        }
        public static string Licensetxt = "";
        private string _LicenseNo;
        public string LicenseNo
        {
            get { return _LicenseNo; }
            set { _LicenseNo = value; OnPropertyChanged(nameof(LicenseNo)); }
        }

        bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set{_IsBusy = value;OnPropertyChanged();}
        }
       
        public string RentNegotiable = "0";
        public string HideRent = "0";

        private string _RentNegoImg;
        public string RentNegoImg
        {
            get { return _RentNegoImg; }
            set
            {
                _RentNegoImg = value;
                OnPropertyChanged(nameof(RentNegoImg));
            }
        }
        private string _RentHideImg;
        public string RentHideImg
        {
            get { return _RentHideImg; }
            set
            {
                _RentHideImg = value;
                OnPropertyChanged(nameof(RentHideImg));
            }
        }

        public string TempLocationAddress;
        private string _LocationAddress;
        public string LocationAddress
        {
            get { return _LocationAddress; }
            set
            {
                _LocationAddress = value;
                OnPropertyChanged(nameof(LocationAddress));
                if (LocationAddress != null && TempLocationAddress != LocationAddress)
                {
                    GetgoogleAddress();
                }
            }
        }
        
        private string _PostOTP { get; set; }
        public string PostOTP
        {
            get { return _PostOTP; }
            set { _PostOTP = value.Trim(); }
        }

        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set { _Countrycode = value; OnPropertyChanged(); }
        }

        bool _OTPIsBusy = true;
        public bool OTPIsBusy
        {
            get { return _OTPIsBusy; }
            set { _OTPIsBusy = value; OnPropertyChanged(); }
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
            set { _NewCountrycode = value; }
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
        private List<Predictions> _googleadd;
        public List<Predictions> googleadd
        {
            get { return _googleadd; }
            set { _googleadd = value; }
        }
       
        private bool _AgentInfo;
        public bool AgentInfo
        {
            get { return _AgentInfo; }
            set { _AgentInfo = value; OnPropertyChanged(nameof(AgentInfo)); }
        }
        
        private string _AdTitle;
        public string AdTitle
        {
            get { return _AdTitle; }
            set { _AdTitle = value; OnPropertyChanged(nameof(AdTitle)); }
        }
        private string _AdDescription;
        public string AdDescription
        {
            get { return _AdDescription; }
            set { _AdDescription = value; OnPropertyChanged(nameof(AdDescription)); }
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
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
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
        private string _attachedbathtxt = "No of Baths ";
        public string attachedbathtxt
        {
            get { return _attachedbathtxt; }
            set { _attachedbathtxt = value; OnPropertyChanged(nameof(attachedbathtxt)); }
        }
        private bool _agentppvisible = false;
        public bool agentppvisible
        {
            get { return _agentppvisible; }
            set { _agentppvisible = value;OnPropertyChanged(nameof(agentppvisible)); }
        }
        private bool _agentpicselected = false;
        public bool agentpicselected
        {
            get { return _agentpicselected; }
            set { _agentpicselected = value;OnPropertyChanged(nameof(agentpicselected)); }
        }
        private bool _lcflayoutvisible;
        public bool lcflayoutvisible
        {
            get { return _lcflayoutvisible; }
            set { _lcflayoutvisible = value; OnPropertyChanged(nameof(lcflayoutvisible)); }
        }
        private bool _Parkingslotinfo;
        public bool Parkingslotinfo
        {
            get { return _Parkingslotinfo; }
            set { _Parkingslotinfo = value; OnPropertyChanged(nameof(Parkingslotinfo)); }
        }
        private string _slotarea;
        public string slotarea
        {
            get { return _slotarea; }
            set { _slotarea = value; OnPropertyChanged(nameof(slotarea)); }
        }

        private string _parkingfrequency = "Select parking frequency";
        public string parkingfrequency
        {
            get { return _parkingfrequency; }
            set { _parkingfrequency = value; OnPropertyChanged(nameof(parkingfrequency)); }
        }

        private string _parkingtype = "Select parking type";
        public string parkingtype
        {
            get { return _parkingtype; }
            set { _parkingtype = value; OnPropertyChanged(); }
        }

        private bool _officespaceslotareaVisible=false;
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
            new RT_Posting_Param { stayperiodtxt = "long term (6+ months)", stayperiodID = "1", checkimage = "RadioButtonUnChecked.png" },
            new RT_Posting_Param { stayperiodtxt = "short term", stayperiodID = "2", checkimage = "RadioButtonUnChecked.png" },
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
            if (sTertiaryID == 17)
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
            if(parkingptagID != 43)
            {
                if (string.IsNullOrEmpty(sStayperiod))
                {
                    Label myEntry = currentpage.FindByName<Label>("leasetypetxt");
                    dialog.Toast("Select stayperiod");
                    return false;
                }
            }
           
            if (string.IsNullOrEmpty(LocationAddress))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtroomaddress");
                myEntry.Focus();
                dialog.Toast("Enter Address");
                return false;
            }
            if (parkingptagID == 43)
            {
                if (string.IsNullOrEmpty(sParkingslotarea))
                {
                    //Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                    dialog.Toast("Enter parking area in sqfeet");
                    return false;
                }
                if (Convert.ToInt32(sParkingslotarea)<1)
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
                if (officespacetagID == 18 || officespacetagID == 19 || officespacetagID == 20 )
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

                    if(parkingptagID!=43)
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
        public bool postdescriptionvalidation()
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

        RTCategoryList ctl = new RTCategoryList();

        RTPostFirst pt = new RTPostFirst();
        //postfirst
        public static int sTertiaryID;
        public static int sSecondaryID;
        public VMRTPost(RTCategoryList clist)
        {
            try
            {
                sTertiaryID = clist.tertiarycategoryid;
                sSecondaryID = clist.secondarycategoryid;
               
                if(sTertiaryID == 17 || sSecondaryID == 17)
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
                
                
                if(clist.ismyneed=="1")
                {
                    ctl = clist;
                    needsprefill(clist);
                }
                else
                {
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
                
                OnPropertyChanged(nameof(Countrycode));
                LocationCmd = new Command<RTLocation>(OnLocationClick);
                TapBackPage = new Command(ClickBackPage);
                TapSearchPage = new Command(ClickSearchPage);
                postcmdfirst = new Command(clickpostcmdfirst);
                
            }
            catch (Exception e) 
            {
            }
        }
        public void needsprefill(RTCategoryList clist)
        {
            try
            {
                pt.supercategoryid = clist.supercategoryid;
                pt.supercategoryvalue = clist.supercategoryvalue;
                pt.primarycategoryid = clist.primarycategoryid;
                pt.primarycategoryvalue = clist.primarycategoryvalue;
                pt.secondarycategoryid = clist.secondarycategoryid;
                pt.secondarycategoryvalue = clist.secondarycategoryvalue;
                pt.tertiarycategoryid = clist.tertiarycategoryid;
                pt.tertiarycategoryvalue = clist.tertiarycategoryvalue;
                pt.categoryname = clist.categoryname;
                pt.maincategory = clist.maincategory;
                pt.categoryimgurl = clist.categoryimgurl;
                // pt.textcolor = clist.textcolor;

                pt.Locationname = LocationName = clist.Locationname;
                OnPropertyChanged(LocationName);
                pt.City = citytxt = clist.City;
                pt.Newcityurl = clist.Newcityurl;
                pt.ContactName = ContactName = clist.Contributor;
                pt.ContactEmail = ContactEmail = clist.Email;
                pt.Countrycode  = clist.Countrycode;
                pt.ContactPhone = ContactPhone = clist.Phone;
                pt.StateName = StateName = clist.StateName;
                pt.StateCode = StateCode = clist.Statecode;
                pt.Zipcode = Zipcode = clist.Zipcode;
                pt.Lat = Lat = clist.Lat;
                pt.Long = Long = clist.Long;
                pt.Country = Country = clist.Country;
                //pt.countflag = clist.countflag;

                pt.adid = clist.adid;
                pt.agentid = clist.agentid;
                //Convert.ToDateTime(clist.movedate.ToString("MM/dd/yyyy"));
                pt.pricefrom = clist.pricefrom;
                pt.priceto = clist.priceto;
                pt.Rent = Rentamount = clist.Rent;
                pt.attachedbaths = sAttachedbath = clist.attachedbaths;

                pt.availablefrm = clist.availablefrm;
                // FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);
                pt.pricemode = clist.pricemode;
                pt.accomodate = clist.accomodate;
                pt.stayperiod = clist.stayperiod;
                pt.title = clist.title;
                pt.description = clist.description;
                pt.type = clist.type;
                pt.usertype = clist.usertype;

                pt.agentphotourl = clist.agentphotourl;
                pt.attachedbathtype = clist.attachedbathtype;

                //prefilldataadded
                pt.service = clist.service;
                pt.Isagent = clist.Isagent;
                pt.Subcategory = clist.Subcategory;
                pt.Category=pt.maincategory = clist.Category;
                pt.Ispaid = clist.Ispaid;
                pt.Photoscnt = clist.Photoscnt;
                pt.HtmlPhotopath = clist.HtmlPhotopath;
                pt.Imagepath = clist.Imagepath;
                pt.XmlPhotopath = clist.XmlPhotopath;
                pt.Adtype = clist.Adtype;
                pt.Additionalcity = clist.Additionalcity;
                pt.Citycount = clist.Citycount;
                pt.Citytotalamount = clist.Citytotalamount;
                pt.Tags = clist.Tags;
                //pt.Companyweburl = clist.Companyweburl;
                pt.url = clist.url;
                pt.Bestindustry = clist.Bestindustry;
                //pt.Minsalary = clist.Minsalary;
                //pt.Maxsalary = clist.Maxsalary;
                //pt.Qualification = clist.Qualification;
                //pt.Visastatus = clist.Visastatus;
                //pt.Experience = clist.Experience;
                //pt.Experiencefrom = clist.Experiencefrom;
                //pt.Experienceto = clist.Experienceto;
                //pt.Employmenttype = clist.Employmenttype;
                //pt.Jobtype = clist.Jobtype;
                //pt.Jobroleid = clist.Jobroleid;
                pt.Postedby = clist.Postedby;
                pt.Campaignid = clist.Campaignid;
                pt.Customerid = clist.Customerid;
                pt.Ordertype = clist.Ordertype;
                pt.userpid = clist.Userpid;
                pt.Mailerstatus = clist.Mailerstatus;
                pt.Displayphone = clist.Displayphone;
                pt.Status = clist.Status;
                pt.Contributor = clist.Contributor;
                pt.description = clist.description;
                pt.Titleurl = clist.Titleurl;
                pt.State = clist.State;
                pt.title = clist.title;
                pt.Streetname = LocationAddress = clist.Streetname;
                pt.Salarymodeid = clist.Salarymodeid;
                pt.Salarymode = clist.Salarymode;
                pt.Nationwide = clist.Nationwide;
                pt.Trimetros = clist.Trimetros;
                pt.Linkedinurl = clist.Linkedinurl;
                pt.Hideaddress = clist.Hideaddress;
                //pt.Isresumeavail = clist.Isresumeavail;
                //pt.Jobrole = clist.Jobrole;
                pt.Adpostedcnt = clist.Adpostedcnt;
                pt.Noofadcnt = clist.Noofadcnt;
                pt.Expirydate = clist.Expirydate;
                pt.Orderid = clist.Orderid;
                //pt.Industryname = clist.Industryname;
                //pt.Industryid = clist.Industryid;
                pt.Secondarycategoryvalueurl = clist.Secondarycategoryvalueurl;
                pt.Primarycategoryvalueurl = clist.Primarycategoryvalueurl;
                pt.Supercategoryvalueurl = clist.Supercategoryvalueurl;
                pt.Metrourl = clist.Metrourl;
                pt.Metro = clist.Metro;
                pt.StateCode = clist.Statecode;
                pt.Ismobileverified = clist.Ismobileverified;
                pt.Folderid = clist.Folderid;
                pt.Amount = clist.Amount;
                pt.Numberofdays = clist.Numberofdays;
                pt.Startdate = clist.Startdate;
                pt.Phone = clist.Phone;
                pt.Email = clist.Email;
                pt.Enddate = clist.Enddate;
                pt.Islisting = clist.Islisting;
                pt.Freeadsflag = clist.Freeadsflag;
                pt.Categoryflag = clist.Categoryflag;
                pt.Banneramount = clist.Banneramount;
                pt.Bannerstatus = clist.Bannerstatus;
                pt.Companytype = clist.Companytype;
                pt.Salespersonemail = clist.Salespersonemail;
                pt.Couponcode = clist.Couponcode;
                pt.Disclosedbusiness = clist.Disclosedbusiness;
                pt.Gender = clist.Gender;
                pt.Responsemail = clist.Responsemail;
                pt.Benefits = clist.Benefits;
                pt.Pendingpaycouponcode = clist.Pendingpaycouponcode;
                pt.Bonusamount = clist.Bonusamount;
                pt.Bonusdays = clist.Bonusdays;
                pt.Emailblastamount = clist.Emailblastamount;
                pt.Addonamount = clist.Addonamount;
                pt.businessname = clist.businessname;
                pt.City = clist.City;


                //from myneedspage

                LocationName = clist.City + ", " + clist.Statecode;
                TempLocationName = clist.City + ", " + clist.Statecode;
                OnPropertyChanged(nameof(LocationName));
                OnPropertyChanged(nameof(TempLocationName));
                citytxt = clist.Locationname;
                Newycityurl = clist.Newcityurl;
                Zipcode = clist.Zipcode;
                BusinessName = clist.businessname;
                businessnametxt = clist.businessname;
                LicenseNo = clist.licenseno;
                Licensetxt = clist.licenseno;
                ContactPhone = clist.Phone;

                //Selectcountry = clist.Countrycode;
                pt.ismyneed = clist.ismyneed;
                pt.ismyneedpayment = clist.ismyneedpayment;
                pt.Movedate = clist.Movedate;
                if (!string.IsNullOrEmpty(pt.Movedate))
                {
                    FrDate = DateTime.ParseExact(clist.Movedate, "MM/dd/yyyy", null);
                }
                pt.adtype = Convert.ToInt32(clist.adtype);
                pt.sqrfeet = clist.area;
                if(string.IsNullOrEmpty(pt.sqrfeet))
                {
                    pt.sqrfeet = clist.sqrfeet;
                }
                Lat = clist.Lat;
                Long = clist.Long;
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
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
                    var name = "rentals" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.postadid_RT + ".jpeg";

                    //smallimage image
                    byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 150, 150);
                    MemoryStream smallimagestrm = new MemoryStream(smallimage);
                    // ms.Seek(0, SeekOrigin.Begin);
                    // ms.Write(smallimage, 0, smallimage.Length);
                    profilepicurl = await CommonMethods.Uploadimagestoblob("cdn/rentals/images/smallimage", name, smallimagestrm);
                    smallimagestrm.Dispose();
                    if (!string.IsNullOrEmpty(profilepicurl))
                    {
                        var CurrentPage = GetCurrentPage();
                        var imgpath = CurrentPage.FindByName<Image>("agentprofilepic");
                        imgpath.Source = profilepicurl;
                        agentpicselected = true;
                    }
                    //large image
                    byte[] large = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 346, 250);
                    MemoryStream largeimagestrm = new MemoryStream(large);
                    await CommonMethods.Uploadimagestoblob("cdn/rentals/images/large", name, largeimagestrm);
                    largeimagestrm.Dispose();

                    //small image
                    byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 106, 80);
                    MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                    await CommonMethods.Uploadimagestoblob("cdn/rentals/images/small", name, verysmallimgstrm);
                    verysmallimgstrm.Dispose();

                    //thumbnail image
                    byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 226, 170);
                    MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                    await CommonMethods.Uploadimagestoblob("cdn/rentals/images/thumbnail", name, thumbnailimagestrm);
                    thumbnailimagestrm.Dispose();

                    //thumbnailfull image
                    byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 770, 577);
                    MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                    await CommonMethods.Uploadimagestoblob("cdn/rentals/images/thumbnailfull", name, thumbnailfullimagestrm);
                    thumbnailfullimagestrm.Dispose();

                    //thumbnailmedium image
                    byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 400, 260);
                    MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                    await CommonMethods.Uploadimagestoblob("cdn/rentals/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                    thumbnailmediumimagestrm.Dispose();

                    //thumbs image
                    byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 80, 60);
                    MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                    await CommonMethods.Uploadimagestoblob("cdn/rentals/images/thumbs", name, thumbsimagestrm);
                    thumbsimagestrm.Dispose();
                    dialog.HideLoading();
                }
            }
            catch (Exception ee)
            {

            }

        }


        public static List<string> AdmultiImage = new List<string>();
        public static List<string> AdmultiImagetoDB = new List<string>();

        public static void Adimgurl(string imageurls,string photoname)
        {
            AdmultiImagetoDB.Add(photoname);
            AdmultiImage.Add(imageurls);
            var currentpage = getcurrentpage();
            var findimglist = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
            findimglist.ItemsSource = null;
            findimglist.ItemsSource = AdmultiImage;
        }
        private static Page getcurrentpage()
        {
            var currentpage = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
        public async void Selectphonto()
        {
            try
            {
                await DependencyService.Get<IMultiMediaPickerService>().PickPhotosAsync(postadid_RT.ToString());
            }
            catch(Exception ex)
            {

            }
        }
        //postsecond
        public static int parkingptagID = 0;
        public static int officespacetagID = 0;
        public static int hoteltagID = 0;
        public VMRTPost(RTPostFirst clist, int adid, int agentid, string ordertype)
        {
            if (AdmultiImage == null && clist.ismyneed != "1")
            {
                sPricemode = "";
                sAttachedbath = "";
                sStayperiod = "";
                sAccomodate = "";
                sParkingslotarea = "";
                sParkingfrquency = "";
                sParkingtype = "";
                profilepicurl = "";
                officespacetagID = 0;
                parkingptagID = 0;
                hoteltagID = 0;
                AdmultiImage = new List<string>();
                AdmultiImagetoDB = new List<string>();
            }
            try
            {

                dialog.ShowLoading("");
                if(clist.usertype=="2")
                {
                    agentppvisible = true;
                }
                else
                {
                    agentppvisible = false;
                }
                if(clist.primarycategoryvalue== "Parking Garage")
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
                if(clist.primarycategoryvalue== "Office Space" || clist.primarycategoryvalue == "Retail Outlet" || clist.primarycategoryvalue=="Others")
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
                if(clist.primarycategoryvalue=="Hotels")
                {
                    officespaceslotareaVisible = false;
                    hoteltagID = 11;
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

                ctl.Locationname = clist.Locationname;
                ctl.Newcityurl = clist.Newcityurl;
                ctl.StateName = clist.StateName;
                ctl.StateCode = clist.StateCode;
                ctl.Zipcode = clist.Zipcode;
                ctl.Lat = clist.userlat;
                ctl.Long = clist.userlong;
                ctl.Country = clist.Country;

                ctl.ContactName = clist.ContactName;
                ctl.ContactEmail = clist.ContactEmail;
                ctl.Countrycode = clist.Countrycode;
                ctl.ContactPhone = clist.ContactPhone;
                
                ctl.adid = clist.adid;
                ctl.agentid = clist.agentid;
                ctl.usertype = clist.usertype;
                ctl.agentphotourl = clist.agentphotourl;
                ctl.RentNegotiable = clist.RentNegotiable;
                ctl.HideRent=clist.HideRent;
                ctl.Movedate = clist.Movedate;
                ctl.attachedbathtype = clist.attachedbathtype;
                ctl.stayperiod=clist.stayperiod;
                ctl.accomodate = clist.accomodate;
                TapBackPage = new Command(ClickBackPage);
                TapSearchPage = new Command(ClickSearchPage);

                //MaleImg = "RadioButtonUnChecked.png";
                //FeMaleImg = "RadioButtonUnChecked.png";
                //AnyImg = "RadioButtonChecked.png";
                //GenderMaleCommand = new Command(ChangeGenderMale);
                //GenderFemaleCommand = new Command(ChangeGenderFemale);
                //GenderAnyCommand = new Command(ChangeGenderAny);

                Adphotos = new Command(Selectphoto);

                RentNegoImg = "CheckBoxUnChecked.png";
                RentHideImg = "CheckBoxUnChecked.png";
                RentNegoCommand = new Command(ChangeRentNegoCommand);
                RentHideCommand = new Command(ChangeRentHideCommand);
                Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
                postcmdsecond = new Command(clickpostcmdsecond);
                postdescrptionsubmit = new Command<RTCategoryList>(clickpostdescription);

                pricemodelistdata = pricemodelist;
                stayperiodlistdata = stayperiodlist;
                accomodatelistdata = accomodatelist;
                attachbathlistdata = attachbathlist;

                Taponpricemode = new Command<RT_Posting_Param>(showpricemodelist);
                Taponstayperiod = new Command<RT_Posting_Param>(showstayperiodlist);
                Taponaccomodates = new Command<RT_Posting_Param>(showaccomodatelist);
                Taponattachedbathtype = new Command<RT_Posting_Param>(showbathtypelist);
                Taponparkingfrequency = new Command<RT_Posting_Param>(showparkingfrequencylist);
                Taponparkingtype = new Command<RT_Posting_Param>(showparkingtypelist);

                Selectpricemode = new Command<RT_Posting_Param>(SelectpricemodeID);
                Selectstayperiod = new Command<RT_Posting_Param>(SelectstayperiodID);
                Selectaccomodate = new Command<RT_Posting_Param>(Selectaccomodatecnt);
                Selectbathtype = new Command<RT_Posting_Param>(SelectbathtypeID);
                Selectparkingfrequency = new Command<RT_Posting_Param>(SelectparkingfrequencyID);
                Selectparkingtype = new Command<RT_Posting_Param>(SelectparkingtypeID);

                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);
                Removeimg = new Command<string>(removeimgfromlist);

                selectProfilephoto = new Command(uploadprofilepic);
                Removeprofilepic = new Command(removeagentprofilepic);
                if (clist.ismyneed == "1")
                {
                    pst = clist;
                    if(clist.ispostdes!="1")
                    {
                        clist.ispostdes = "0";
                    }
                    postsecondprefill(pst);
                    // needsprefill(clist);
                }
                dialog.HideLoading();
            }
            catch (Exception e) { dialog.HideLoading(); }
        }
        public void postsecondprefill(RTPostFirst clist)
        {
            try
            {
                var currentpage = getcurrentpage();
                if (clist.ispostdes == "1")
                {
                    //postthirdform
                    AdTitle = clist.title;
                    AdDescription = clist.description;
                    if (VMRTCategory.sCategoryID == 11)
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
                    //ctl.Movedate = clist.Movedate;
                    if(officespaceslotareaVisible == true)
                    {
                        officeslotarea = clist.sqrfeet;
                    }
                    if(Parkingslotinfo == true)
                    {
                        slotarea = clist.sqrfeet;
                    }
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
                    Rentamount = txtrent = clist.Rent;
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
                    if (!string.IsNullOrEmpty(clist.RentNegotiable))
                    {
                        if (clist.RentNegotiable == "1")
                        {
                            RentNegoImg = "CheckBoxChecked.png";
                            HideRent = "1";
                        }
                        else
                        {
                            RentHideImg = "CheckBoxUnChecked.png";
                            HideRent = "0";
                        }
                    }

                    LocationAddress = TempLocationAddress = clist.Streetname;
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
                    if (!string.IsNullOrEmpty(clist.XmlPhotopath))
                    {
                        AdmultiImage = new List<string>();
                        string[] img = clist.XmlPhotopath.Split(',');
                        foreach (var image in img)
                        {
                            AdmultiImage.Add(image);
                        }
                    }
                    if (!string.IsNullOrEmpty(clist.availablefrm))
                    {
                        if (clist.availablefrm.Contains("/"))
                        {
                            FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);
                        }
                        else
                        {
                            FrDate = DateTime.ParseExact(clist.availablefrm, "mm-dd-yyyy", null);
                        }
                    }      //FrDate = DateTime.ParseExact(clist.Movedate, "MM/dd/yyyy", null);
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public VMRTPost(RTPostFirst clist, int adid, int agentid, string ordertype,string page)
        {
           
            try
            {

                dialog.ShowLoading("");
                if (clist.usertype == "2")
                {
                    agentppvisible = true;
                }
                else
                {
                    agentppvisible = false;
                }
                if (clist.primarycategoryvalue == "Parking Garage")
                {
                    parkingptagID = clist.primarycategoryid;
                    Parkingslotinfo = true;
                    lcflayoutvisible = false;
                    officespaceslotareaVisible = false;
                }
                else
                {
                    Parkingslotinfo = false;
                    lcflayoutvisible = true;
                    officespaceslotareaVisible = true;
                }
                if (clist.primarycategoryvalue == "Office Space" || clist.primarycategoryvalue == "Retail Outlet" || clist.primarycategoryvalue == "Others")
                {
                    officespacetagID = clist.primarycategoryid;
                    Parkingslotinfo = false;
                    lcflayoutvisible = false;
                    officespaceslotareaVisible = true;
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

                ctl.Locationname = clist.Locationname;
                ctl.Newcityurl = clist.Newcityurl;
                ctl.StateName = clist.StateName;
                ctl.StateCode = clist.StateCode;
                ctl.Zipcode = clist.Zipcode;
                ctl.Lat = clist.userlat;
                ctl.Long = clist.userlong;
                ctl.Country = clist.Country;

                ctl.ContactName = clist.ContactName;
                ctl.ContactEmail = clist.ContactEmail;
                ctl.Countrycode = clist.Countrycode;
                ctl.ContactPhone = clist.ContactPhone;

                ctl.adid = clist.adid;
                ctl.agentid = clist.agentid;
                ctl.usertype = clist.usertype;
                ctl.agentphotourl = clist.agentphotourl;
                ctl.RentNegotiable = clist.RentNegotiable;
                ctl.HideRent = clist.HideRent;
                ctl.Movedate = clist.Movedate;
                ctl.attachedbathtype = clist.attachedbathtype;
                ctl.stayperiod = clist.stayperiod;
                ctl.accomodate = clist.accomodate;
                TapBackPage = new Command(ClickBackPage);
                TapSearchPage = new Command(ClickSearchPage);

                //MaleImg = "RadioButtonUnChecked.png";
                //FeMaleImg = "RadioButtonUnChecked.png";
                //AnyImg = "RadioButtonChecked.png";
                //GenderMaleCommand = new Command(ChangeGenderMale);
                //GenderFemaleCommand = new Command(ChangeGenderFemale);
                //GenderAnyCommand = new Command(ChangeGenderAny);

                Adphotos = new Command(Selectphoto);

                RentNegoImg = "CheckBoxUnChecked.png";
                RentHideImg = "CheckBoxUnChecked.png";
                RentNegoCommand = new Command(ChangeRentNegoCommand);
                RentHideCommand = new Command(ChangeRentHideCommand);
                Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
                postcmdsecond = new Command(clickpostcmdsecond);
                postdescrptionsubmit = new Command<RTCategoryList>(clickpostdescription);

                pricemodelistdata = pricemodelist;
                stayperiodlistdata = stayperiodlist;
                accomodatelistdata = accomodatelist;
                attachbathlistdata = attachbathlist;

                Taponpricemode = new Command<RT_Posting_Param>(showpricemodelist);
                Taponstayperiod = new Command<RT_Posting_Param>(showstayperiodlist);
                Taponaccomodates = new Command<RT_Posting_Param>(showaccomodatelist);
                Taponattachedbathtype = new Command<RT_Posting_Param>(showbathtypelist);
                Taponparkingfrequency = new Command<RT_Posting_Param>(showparkingfrequencylist);
                Taponparkingtype = new Command<RT_Posting_Param>(showparkingtypelist);

                Selectpricemode = new Command<RT_Posting_Param>(SelectpricemodeID);
                Selectstayperiod = new Command<RT_Posting_Param>(SelectstayperiodID);
                Selectaccomodate = new Command<RT_Posting_Param>(Selectaccomodatecnt);
                Selectbathtype = new Command<RT_Posting_Param>(SelectbathtypeID);
                Selectparkingfrequency = new Command<RT_Posting_Param>(SelectparkingfrequencyID);
                Selectparkingtype = new Command<RT_Posting_Param>(SelectparkingtypeID);

                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);
                Removeimg = new Command<string>(removeimgfromlist);

                selectProfilephoto = new Command(uploadprofilepic);
                Removeprofilepic = new Command(removeagentprofilepic);
                if (clist.ismyneed == "1")
                {
                    pst = clist;
                    postsecondprefill(pst);
                    // needsprefill(clist);
                }
                dialog.HideLoading();
            }
            catch (Exception e) { dialog.HideLoading(); }
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
            catch (Exception ex)
            {

            }
        }

        public void removeimgfromlist(string imglist)
        {
            if (AdmultiImage.Contains(imglist))
            {
                AdmultiImage.Remove(imglist);
            }
            string dbimg = imglist.Replace("http://cdnnrisulekhalive.blob.core.windows.net:80/cdn/rentals/images/smallimage/", "");
            if (AdmultiImagetoDB.Contains(dbimg))
            {
                AdmultiImagetoDB.Remove(dbimg);
            }
            
           var currentpage = GetCurrentPage();
            var lst = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
            lst.ItemsSource = null;
            lst.ItemsSource = AdmultiImage;
        }
        public async void Selectphoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                dialog.Toast("This is not support on your device.");
                return;
            }
            else
            {
                await DependencyService.Get<IMultiMediaPickerService>().PickPhotosAsync(postadid_RT.ToString());
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
        public void SelectpricemodeID(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label pricemodetxtclr = currentpage.FindByName<Label>("pricemodetxt");
            pricemodetxtclr.TextColor = Color.FromHex("#212121");
            pricemodetxt = posting_Param.pricemode;
            sPricemode = posting_Param.pricemode;

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
        public void SelectparkingtypeID(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label parkingtypetxtclr = currentpage.FindByName<Label>("parkingtype");
            parkingtypetxtclr.TextColor = Color.FromHex("#212121");
            parkingtype = posting_Param.parkingtype;
            sParkingtype = posting_Param.parkingtype;
            contentviewvisible = false;
        }
        public void SelectparkingfrequencyID(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label parkingfrequencytxtclr = currentpage.FindByName<Label>("parkingfrequency");
            parkingfrequencytxtclr.TextColor = Color.FromHex("#212121");
            parkingfrequency = posting_Param.parkingfrequency;
            sParkingfrquency = posting_Param.parkingfreqencyID;
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
            parkingfrequencyvisible = false;
            parkingtypevisible = false;

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
        }
        public void showparkingfrequencylist(RT_Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            var parkingfreq = currentpage.FindByName<ListView>("parkingfrequencytype");
            foreach (var freq in parkingfrequencylist)
            {
                if (parkingfrequency == freq.parkingfrequency)
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
           // selectrentvisible = false;
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
           // selectrentvisible = false;
            parkingtypevisible = true;
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
        }
        //postotp
        public VMRTPost(RTPostFirst pstotp, string pinno)
        {
            try
            {
                pst.adtype = Commonsettings.CRTAdTypeID;
                pst.RTcategory = Commonsettings.CRTCategory;
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
                pst.agentid = pstotp.agentid;
                pst.usertype = pstotp.usertype;
                pst.mobileverify = "0";

                pst.title = pstotp.title;
                pst.description = pstotp.description;

                
                pst.mobileverify = "0";
                pst.deviceid = Commonsettings.UserDeviceId;
                //pst.Locationname = Commonsettings.Usercity;
                pst.devicename = Commonsettings.UserMobileOS;
                pst.postedvia = Commonsettings.UserMobileOS;
                pst.userpid = Commonsettings.UserPid;
                pst.pricemode = sPricemode;
                pst.attachedbathtype = sAttachedbath;
                pst.stayperiod = sStayperiod;
                pst.accomodate = sAccomodate;
                pst.ipaddress = ipaddress;
                pst.City = pstotp.City;
                //if (string.IsNullOrEmpty(pst.agentid))
                //{
                //    pst.agentid = "0";
                //}
                if(!string.IsNullOrEmpty(pstotp.agentphotourl))
                {
                    pst.agentphotourl = pstotp.agentphotourl;
                }
                pst.sqrfeet = sParkingslotarea;
                if (!string.IsNullOrEmpty(sParkingfrquency))
                {
                    pst.stayperiod = sParkingfrquency;
                    pst.accomodationtype = sParkingtype;
                }
                if (sTertiaryID == 17)
                {
                    pst.businessname = businessnametxt;
                    pst.businessLicenseno = Licensetxt;
                }

                //SubmitOTPPost = new Command(Click_SubmitOTPPost);
                SubmitOTPPost = new Command(async () => await Click_SubmitOTPPost(pst));
              //  ResendOTP = new Command(ClickResendOTP);
                ResendOTP = new Command(async () => await ClickResendOTP(pst));

                //NewCountrycode = Countrycode;
                ChangemobileNumber = new Command(ClickChangeMObile);
               // SubmitChangeMobile = new Command(ClickSubmitChangeMobile);
                SubmitChangeMobile = new Command(async () =>await ClickSubmitChangeMobile(pst));

                _Pinno = pinno;
            }
            catch (Exception e) { }

        }
        public async void clickpostdescription(RTCategoryList category)
        {
            try
            {
                pst.pricemode = sPricemode;
                pst.attachedbathtype = sAttachedbath;

                pst.accomodate = sAccomodate;

                pst.ipaddress = ipaddress;
                if (!string.IsNullOrEmpty(profilepicurl))
                {
                    pst.agentphotourl = profilepicurl;
                }
                if (parkingptagID == 43)
                {
                    pst.sqrfeet = sParkingslotarea;
                    pst.accomodationtype = sParkingtype;
                    pst.stayperiod = sParkingfrquency;
                }
                else
                {
                    pst.stayperiod = sStayperiod;
                    pst.sqrfeet = sParkingslotarea;
                }
                if (officespacetagID == 18 || officespacetagID == 19 || officespacetagID == 20)
                {
                    pst.sqrfeet = sParkingslotarea;
                }



                pst.imgname = string.Join(";", AdmultiImagetoDB.ToArray());

                if (!string.IsNullOrEmpty(pst.imgname))
                {
                    var multipleimguploading = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                    var imguploadresult = await multipleimguploading.insertphotos(postadid_RT.ToString(), pst.imgname);
                    var inserted = imguploadresult;
                }
                pst.adtype = Commonsettings.CRTAdTypeID;
                pst.RTcategory = Commonsettings.CRTCategory;
                if (pst.ismyneed =="1"&&pst.ismyneedpayment!="1")
                {
                    if (postdescriptionvalidation())
                    {
                        pst.mode = "edit";
                        var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await PostSubmit.otppostsecondsubmit(pst);
                        pst.agentid = data.agentid;

                        if (data != null)
                        {
                            var curpage = getcurrentpage();
                            if (data.type == "0")
                            {
                                await curpage.Navigation.PushAsync(new PostOTP(pst, data.pinno));
                            }
                            else
                            {
                                var publishAd = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                                var publishagentad = await publishAd.publishad(pst.adid.ToString());
                                if (publishagentad.resultinformation == "sucess")
                                {
                                    //await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.postsuccess(pst.adid.ToString(), pst.usertype, ""));
                                    await curpage.Navigation.PushAsync(new Roommates.Features.Post.Views.EditThankyou());
                                }
                            }
                        }
                    }
                }
                else if(pst.ismyneedpayment=="1")
                {
                    var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                    var data = await PostSubmit.otppostsecondsubmit(pst);
                    if(data!=null)
                    {
                        var cpage = getcurrentpage();
                        if (data.type == "0")
                        {
                            await cpage.Navigation.PushAsync(new PostOTP(pst, data.pinno));
                        }
                        else
                        {
                            if (data.ispayment == 1)
                            {
                                await cpage.Navigation.PushAsync(new Package_RT(pst));
                            }
                            else
                            {
                                // await curpage.Navigation.PushAsync(new Thankyou());
                                freeadpost(data.adid, pst.usertype);
                            }
                        }
                        
                    }
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
                    pst.Country = ctl.Country;

                    pst.Movedate = ctl.Movedate;
                    pst.ExpRent = Rentamount;

                    pst.HideRent = ctl.HideRent;
                    pst.RentNegotiable = ctl.RentNegotiable;


                    pst.LocationAddress = postlocationaddress;
                    pst.adid = ctl.adid;
                    pst.agentid = ctl.agentid;
                    pst.usertype = ctl.usertype;
                    pst.mobileverify = "1";
                    pst.deviceid = Commonsettings.UserDeviceId;
                    pst.devicename = Commonsettings.UserMobileOS;
                    pst.postedvia = Commonsettings.UserMobileOS;
                    pst.userpid = Commonsettings.UserPid;
                    pst.agentphotourl = ctl.agentphotourl;

                    pst.title = AdTitle;
                    pst.description = AdDescription;
                    //pst.gender = Gender;
                    //pst.attachedbathtype = ctl.attachedbathtype;
                    //if (string.IsNullOrEmpty(pst.agentid))
                    //{
                    //    pst.agentid = "0";
                    //}
                    pst.userlat = ctl.Lat;
                    pst.userlong = ctl.Long;
                    pst.City = ctl.Locationname;
                    if (sTertiaryID == 17)
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


                    
                    if (postdescriptionvalidation())
                    {
                        dialog.ShowLoading("");
                        if (pst.agentid != 0)
                        {
                            var curpage = getcurrentpage();
                            var agentdata = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                            var agentdatadetail = await agentdata.getagentdetails(pst.agentid);
                            if (agentdatadetail != null && agentdatadetail.ROW_DATA.Count > 0)
                            {
                                if (agentdatadetail.ROW_DATA.Last().noofadsremain != 0 && agentdatadetail.ROW_DATA.Last().totalads != 0)
                                {
                                    if (pst.ismyneed == "1" && pst.ismyneedpayment == "1")
                                    {
                                        var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
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
                                                    if (data.ispayment == 1)
                                                    {
                                                        await curpage.Navigation.PushAsync(new Package_RT(pst));
                                                    }
                                                    else
                                                    {
                                                        // await curpage.Navigation.PushAsync(new Thankyou());
                                                        freeadpost(data.adid, pst.usertype);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        pst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                        pst.package = agentdatadetail.ROW_DATA.Last().packagename;
                                        var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
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
                                                    var api = RestService.For<NRIApp.Rentals.Features.Post.Interface.IPaymentRT>(Commonsettings.RoommatesAPI);
                                                    if (adid == 0)
                                                    {
                                                        adid = pst.adid;
                                                    }
                                                    Pkgsucess_RT list = await api.updatepackage(adid.ToString(), agentdatadetail.ROW_DATA.Last().packagedays.ToString(), agentdatadetail.ROW_DATA.Last().packagename, agentdatadetail.ROW_DATA.Last().totalads.ToString(), pst.agentid);

                                                    if (list.resultinformation == "updated successfully")
                                                    {
                                                        var publishAd = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                                                        var publishagentad = await publishAd.publishad(adid.ToString());
                                                        if (publishagentad.resultinformation == "sucess")
                                                        {
                                                            await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.postsuccess(pst.adid.ToString(), pst.usertype, ""));
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
                                    var alert = await dialog.ConfirmAsync("Alert!", "disable one of your ads to post", "Gotomyneeds", "package");
                                    if (alert)
                                    {
                                        //byte[] encData_byte = new byte[Commonsettings.UserEmail.Length];
                                        //encData_byte = System.Text.Encoding.UTF8.GetBytes(Commonsettings.UserEmail);
                                        //string encodedData = Convert.ToBase64String(encData_byte);
                                        //Device.OpenUri(new Uri("https://techjobs.sulekha.com/mobileapp/autologin.aspx?useremail=" + encodedData));
                                        //Device.OpenUri(new Uri("http://myaccount.sulekha.com/my-needs"));
                                        await curpage.Navigation.PushAsync(new MyNeeds.Features.Views.MyNeeds());
                                    }
                                    else
                                    {
                                        pst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                        pst.package = agentdatadetail.ROW_DATA.Last().packagename;
                                        var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
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
                                                    if (data.ispayment == 1)
                                                    {
                                                        await curpage.Navigation.PushAsync(new Package_RT(pst));
                                                    }
                                                    else
                                                    {
                                                        // await curpage.Navigation.PushAsync(new Thankyou());
                                                        freeadpost(data.adid, pst.usertype);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
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
                                            if (data.ispayment == 1)
                                            {
                                                await curpage.Navigation.PushAsync(new Package_RT(pst));
                                            }
                                            else
                                            {
                                                // await curpage.Navigation.PushAsync(new Thankyou());
                                                freeadpost(data.adid, pst.usertype);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                            var data = await PostSubmit.otppostsecondsubmit(pst);
                            pst.agentid = data.agentid;

                            if (data != null)
                            {
                                var curpage = GetCurrentPage();
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
                                        if (data.ispayment == 1)
                                        {
                                            await curpage.Navigation.PushAsync(new Package_RT(pst));
                                        }
                                        else
                                        {
                                            // await curpage.Navigation.PushAsync(new Thankyou());
                                            freeadpost(data.adid, pst.usertype);
                                        }
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
        public async void ClickBackPage()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PopAsync();
        }
        public async void ClickSearchPage()
        {
            var curpage = GetCurrentPage();
           // await curpage.Navigation.PushAsync(new RTSearch());
        }

        //public void ChangeGenderMale()
        //{
        //    AnyImg = "RadioButtonUnChecked.png";
        //    MaleImg = "RadioButtonChecked.png";
        //    FeMaleImg = "RadioButtonUnChecked.png";
        //    Gender = "male";
        //}
        //public void ChangeGenderFemale()
        //{
        //    AnyImg = "RadioButtonUnChecked.png";
        //    MaleImg = "RadioButtonUnChecked.png";
        //    FeMaleImg = "RadioButtonChecked.png";
        //    Gender = "female";
        //}
        //public void ChangeGenderAny()
        //{
        //    AnyImg = "RadioButtonChecked.png";
        //    MaleImg = "RadioButtonUnChecked.png";
        //    FeMaleImg = "RadioButtonUnChecked.png";
        //    Gender = "any";
        //}

        public async void GetLocationname()
        {
            try
            {
                Zipcode = "";
                var LocationAPI = RestService.For<IRTCategory>(Commonsettings.Localservices);
                Models.RTLocationRowData response = await LocationAPI.getlocation(LocationName);
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
        public  void OnLocationClick(RTLocation obj)
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
        public static int adid;
        public static int agentid;
        public static string ordertype = "";
        public async void clickpostcmdfirst()
        {
            try
            {
                if (Validations())
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
                    RTPostFirst pst = new RTPostFirst();
                    if(pt.ismyneed=="1")
                    {
                        pst = pt;
                        if(string.IsNullOrEmpty(pst.categoryname))
                        {
                            pst.maincategory = "rentals";
                        }
                    }
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
                    Commonsettings.CRTCategory = "Rentals";
                    Commonsettings.CategoryType = "Rentals";
                    pst.userpid = Commonsettings.UserPid;

                    if (sTertiaryID == 17)
                    {
                        pst.businessname = BusinessName;
                        businessnametxt = BusinessName;
                        pst.businessLicenseno = LicenseNo;
                        Licensetxt = LicenseNo;
                    }

                    if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                    {
                        autologin(ContactName,ContactEmail,ContactPhone);
                    }

                    pst.userpid = Commonsettings.UserPid;
                    pst.adtype = Commonsettings.CRTAdTypeID;
                    pst.RTcategory = Commonsettings.CRTCategory;
                    if (pt.ismyneed=="1")
                    {
                        postadid_RT = pt.adid;
                        var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await PostSubmit.postfirstsubmit(pst);
                        if (data != null)
                        {
                            var curpage = GetCurrentPage();
                            await curpage.Navigation.PushAsync(new postsecond(pst, data.ROW_DATA.First().adid, data.ROW_DATA.First().agentid, data.ROW_DATA.First().ordertype));
                        }
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
                        pst.usertype = ctl.usertype;
                        //  pst.Locationname = LocationName;
                        pst.Newcityurl = Newycityurl;
                        pst.ContactName = ContactName;
                        pst.ContactEmail = ContactEmail;
                        pst.Countrycode = Countrycode;
                        pst.ContactPhone = ContactPhone;
                        pst.deviceid = Commonsettings.UserDeviceId;
                        pst.ipaddress = ipaddress;


                        var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await PostSubmit.postfirstsubmit(pst);

                        var result = data.ROW_DATA;
                        adid = result.First().adid;
                        postadid_RT = result.First().adid;
                        

                        pst.adid = result.First().adid;
                        pst.agentid = result.First().agentid;
                        agentid = result.First().agentid;
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
            catch (Exception e) { }
        }
        public async void autologin(string name, string email, string phoneno)
        {
           try
            {
                var login = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                var logindata = await login.getuserpid(name, email);
                Commonsettings.UserPid = logindata.resultinformation;
                Commonsettings.UserName = name;
                Commonsettings.UserEmail = email;
                Commonsettings.UserMobileno = phoneno;
            }
            catch(Exception e)
            {

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
        RTPostFirst pst = new RTPostFirst();
        public static string Rentamount = "";
        public static string postlocationaddress;
        public async void clickpostcmdsecond()
        {
            try
            {
                if(string.IsNullOrEmpty(slotarea))
                {
                    sParkingslotarea = officeslotarea;
                }
                else
                {
                    sParkingslotarea = slotarea;
                }

                if (Validationssecond())
                {
                    pst.gender = Gender;
                    //string sdate = FrDate.ToString("yyyy-MM-dd");
                    pst.Movedate = FrDate.ToString("MM/dd/yyyy");
                    pst.ExpRent = txtrent;
                    Rentamount = txtrent;
                    pst.HideRent = HideRent;
                    pst.RentNegotiable = RentNegotiable;
                    pst.attachedbathtype = sAttachedbath;

                    pst.accomodate = sAccomodate;
                    pst.LocationAddress = LocationAddress;
                    postlocationaddress = LocationAddress;
                    pst.deviceid = Commonsettings.UserDeviceId;
                    pst.deviceid = Commonsettings.UserDeviceId;
                    pst.devicename = Commonsettings.UserMobileOS;
                    pst.postedvia = Commonsettings.UserMobileOS;
                    pst.userpid = Commonsettings.UserPid;
                    pst.sqrfeet = sParkingslotarea;
                    if (!string.IsNullOrEmpty(profilepicurl))
                    {
                        pst.agentphotourl = profilepicurl;
                    }
                    if (parkingptagID == 43)
                    {
                        pst.stayperiod = sParkingfrquency;
                    }
                    else
                    {
                        pst.stayperiod = sStayperiod;
                    }
                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        pst.devicetypeid = "1";
                    }
                    else
                        pst.devicetypeid = "2";
                    dialog.ShowLoading("");
                    pst.adtype = Commonsettings.CRTAdTypeID;
                    pst.RTcategory = Commonsettings.CRTCategory;
                    if (pst.ismyneed=="1")
                    {
                        pst.ispostdes = "1";
                        var currentpg = GetCurrentPage();
                        await currentpg.Navigation.PushAsync(new PostDescription_RT(pst, adid, agentid, ordertype));
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
                        pst.Country = ctl.Country;
                        pst.userlat = ctl.Lat;
                        pst.userlong = ctl.Long;
                        
                        pst.adid = ctl.adid;
                        pst.agentid = ctl.agentid;
                        pst.usertype = ctl.usertype;
                        pst.mobileverify = "1";
                        

                        pst.title = AdTitle;
                        pst.description = AdDescription;
                        

                        var currentpg = GetCurrentPage();
                        await currentpg.Navigation.PushAsync(new PostDescription_RT(pst, adid, agentid, ordertype));
                    }
                    dialog.HideLoading();
                 
                }
            }
            catch (Exception e)
            {
            }
        }

        public async Task Click_SubmitOTPPost(RTPostFirst postfirst)
        {
            if (PostOTP == _Pinno)
            {
                try
                {
                    dialog.ShowLoading(""); var adid = "";
                    var curpage = GetCurrentPage();
                    if (postfirst.ismyneed == "1" && postfirst.ismyneedpayment != "1")
                    {
                        //var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        //var data = await PostSubmit.postsecondsubmit(pst);
                        var PostOTPSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                        var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);
                        var publishAd = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                        var publishagentad = await publishAd.publishad(adid.ToString());
                        await curpage.Navigation.PushAsync(new Roommates.Features.Post.Views.EditThankyou());
                    }
                    else
                    {
                        if (postfirst.agentid != 0)
                        {
                            var agentdata = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                            var agentdatadetail = await agentdata.getagentdetails(postfirst.agentid);
                            if (agentdatadetail != null && agentdatadetail.ROW_DATA.Count > 0)
                            {
                                if (agentdatadetail.ROW_DATA.Last().noofadsremain != 0 && agentdatadetail.ROW_DATA.Last().totalads != 0)
                                {
                                    pst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                    if(postfirst.ismyneed=="1"&&postfirst.ismyneedpayment=="1")
                                    {
                                        var PostSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
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
                                                    if (data.ispayment == 1)
                                                    {
                                                        await curpage.Navigation.PushAsync(new Package_RT(pst));
                                                    }
                                                    else
                                                    {
                                                        // await curpage.Navigation.PushAsync(new Thankyou());
                                                        freeadpost(data.adid, pst.usertype);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var PostOTPSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                                        var usertype = postfirst.usertype;
                                        var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);

                                        var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                                        Pkgsucess_RT list = await api.updatepackage(postfirst.adid.ToString(), agentdatadetail.ROW_DATA.Last().packagedays.ToString(), agentdatadetail.ROW_DATA.Last().packagename, agentdatadetail.ROW_DATA.Last().totalads.ToString(), postfirst.agentid);

                                        if (list.resultinformation == "updated successfully")
                                        {
                                            var publishAd = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                                            var publishagentad = await publishAd.publishad(postfirst.adid.ToString());
                                            if (publishagentad.resultinformation == "sucess")
                                            {

                                                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.postsuccess(pst.adid.ToString(), pst.usertype, ""));
                                            }
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
                                        // Device.OpenUri(new Uri("http://myaccount.sulekha.com/my-needs"));
                                       // var currpage = GetCurrentPage();
                                        await curpage.Navigation.PushAsync(new MyNeeds.Features.Views.MyNeeds());
                                    }
                                    else
                                    {
                                        pst.noofremainingads = agentdatadetail.ROW_DATA.Last().noofadsremain;
                                        pst.package = agentdatadetail.ROW_DATA.Last().packagename;
                                        var PostOTPSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                                        var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);

                                        //var curpage = GetCurrentPage();
                                        await curpage.Navigation.PushAsync(new Package_RT(postfirst));
                                    }

                                }
                            }
                            else
                            {
                                var PostOTPSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                                var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);

                               // var curpage = GetCurrentPage();
                                await curpage.Navigation.PushAsync(new Package_RT(postfirst));
                            }
                        }
                        else
                        {
                            var PostOTPSubmit = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                            var usertype = postfirst.usertype;
                            var data = await PostOTPSubmit.otppostsecondsubmit(postfirst);
                            try
                            {
                                if (data != null)
                                {
                                    lcfsecond.postsuccessbackbtn = 1;
                                    if (data.agentid != 0)
                                    {
                                       // var curpage = GetCurrentPage();
                                        await curpage.Navigation.PushAsync(new Package_RT(postfirst));
                                    }
                                    else
                                    {
                                        if (data.ispayment == 1)
                                        {
                                            adid = data.adid;
                                            //var curpage = GetCurrentPage();
                                            await curpage.Navigation.PushAsync(new Package_RT(postfirst));
                                        }
                                        else
                                        {
                                           // var curpage = GetCurrentPage();
                                            //await curpage.Navigation.PushAsync(new Thankyou());
                                            freeadpost(data.adid, usertype);
                                        }
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
                var nodays = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                Pkgsucess_RT FreeadDuration = await nodays.getnoofdays(usertype);
                var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                Pkgsucess_RT list = await api.updatepackage(adid, FreeadDuration.resultinformation, "Free", "", 0);
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
        public async Task ClickResendOTP(RTPostFirst pst)
        {
            try
            {
                dialog.ShowLoading("");
                var resendotp = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
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

        public async Task ClickSubmitChangeMobile(RTPostFirst pst) 
        {
            try
            {
                if (Validationsnewphone())
                {
                    dialog.ShowLoading("");
                    pst.Countrycode = Countrycode;
                    pst.ContactPhone = NewContactPhone;
                    var resendotp = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                    var data = await resendotp.getresendotp(pst.ContactPhone, pst.Countrycode);
                    _Pinno = data.pinno;
                    IsBusy = false;
                    OTPIsBusy = true;
                    dialog.HideLoading();
                }
            }
            catch (Exception e) 
            {

            }
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
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}
