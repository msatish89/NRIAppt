using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Windows.Input;
using NRIApp.Roommates.Features.List.Views;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;

namespace NRIApp.Roommates.Features.Post.ViewModels
{
    public class VMLCF : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;

        public event PropertyChangedEventHandler PropertyChanged;


        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Dollarpattern = @"^[1-9]\d*(\.\d+)?$";
        string Namepattern = "^[0-9A-Za-z ]+$";

        private DateTime _FrDate = DateTime.Today;
        private string _MaleImg;
        private string _FeMaleImg;
        private string _AnyImg;





        private string _Newycityurl = "";
        public string Newycityurl
        {
            get { return _Newycityurl; }
            set { _Newycityurl = value; OnPropertyChanged(); }
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
        private string _LCFOTP { get; set; }
        private string _NewContactPhone;
        bool _OTPIsBusy = true;
        public Command TapBackPage { get; set; }
        public Command TapSearchPage { get; set; }
        public Command lcfcmdnext { get; set; }
        public Command lcfcmdskip { get; set; }

        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }


        public ICommand Taponpricemode { get; set; }
        public ICommand Taponstayperiod { get; set; }
        public ICommand Taponaccomodates { get; set; }
        public ICommand Taponattachedbathtype { get; set; }

        public ICommand Selectpricemode { get; set; }
        public ICommand Selectstayperiod { get; set; }
        public ICommand Selectaccomodate { get; set; }
        public ICommand Selectbathtype { get; set; }

        public Command insertlcf { get; set; }
        private bool _lcfskipvisible = false;
        public bool Lcfskipvisible
        {
            get { return _lcfskipvisible; }
            set { _lcfskipvisible = value; OnPropertyChanged(); }
        }
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
                _MaleImg = value; OnPropertyChanged(nameof(MaleImg));
            }
        }
        public string FeMaleImg
        {
            get { return _FeMaleImg; }
            set { _FeMaleImg = value; OnPropertyChanged(nameof(FeMaleImg)); }
        }
        public string AnyImg
        {
            get { return _AnyImg; }
            set { _AnyImg = value; OnPropertyChanged(nameof(AnyImg)); }
        }

        //private string _txtrent = "Expected Rent in $";
        private string _txtrent = "";
        public string txtrent
        {
            get { return _txtrent; }
            set { _txtrent = value; OnPropertyChanged(nameof(txtrent)); }
        }
        private string _listinggender;
        public string ListingGender
        {
            get { return _listinggender; }
            set { _listinggender = value; OnPropertyChanged(); }
        }
        public Command GenderMaleCommand { get; set; }

        public Command GenderFemaleCommand { get; set; }

        public Command GenderAnyCommand { get; set; }


        public string Gender = "any";
        private string _Zipcode ;
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; }
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


        private List<Location> _LocationList { get; set; }
        public List<Location> LocationList
        {
            get { return _LocationList; }
            set
            {
                _LocationList = value;
            }
        }

        private string _Locationurl = Commonsettings.Usercityurl;
        public string Locationurl
        {
            get { return _Locationurl; }
            set { _Locationurl = value; OnPropertyChanged(); }
        }
        public static string contactnumber = "";
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; OnPropertyChanged(); }
        }

        public ICommand LocationCmd { protected set; get; }


        public Command lcfcmdsubmit { get; set; }
        public Command lcfdescrptionsubmit { get; set; }
        bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(); }
        }

        private bool _isvisiblelist = false;
        public bool IsVisibleList
        {
            get { return _isvisiblelist; }
            set { _isvisiblelist = value; OnPropertyChanged(nameof(IsVisibleList)); }
        }

        public string LCFOTP
        {
            get { return _LCFOTP; }
            set { _LCFOTP = value.Trim(); }
        }

        public static string _Pinno;

        public Command ChangemobileNumber { get; set; }

        public Command SubmitChangeMobile { get; set; }

        public bool OTPIsBusy
        {
            get { return _OTPIsBusy; }
            set { _OTPIsBusy = value; OnPropertyChanged(); }
        }

        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set { _Countrycode = value; OnPropertyChanged(); }
        }
        private string _NewSelectcountry;
        public string NewSelectcountry
        {
            get { return _NewSelectcountry; }
            set { _NewSelectcountry = value; OnPropertyChanged(NewSelectcountry); }
        }

        public string NewContactPhone
        {
            get { return _NewContactPhone; }
            set { _NewContactPhone = value; OnPropertyChanged(); }
        }
        private string _countflag;
        public string countflag
        {
            get { return _countflag; }
            set { _countflag = value; OnPropertyChanged(); }
        }

        private string _cityname;
        public string CityName
        {
            get { return _cityname; }
            set { _cityname = value; OnPropertyChanged(); }
        }
        private string _metrourl;
        public string metrourl
        {
            get { return _metrourl; }
            set { _metrourl = value; OnPropertyChanged(); }
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
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _selectrentvisible = false;
        public bool selectrentvisible
        {
            get { return _selectrentvisible; }
            set { _selectrentvisible = value; OnPropertyChanged(nameof(selectrentvisible)); }
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
        private string _attachedbathtxt = "Attached Bath";
        public string attachedbathtxt
        {
            get { return _attachedbathtxt; }
            set { _attachedbathtxt = value; OnPropertyChanged(nameof(attachedbathtxt)); }
        }
        //public string _sPricemode = "";
        //public string sPricemode
        //{
        //    get { return _sPricemode; }
        //    set { _sPricemode = value; OnPropertyChanged(nameof(sPricemode)); }
        //}
        public static string sPricemode = "";
        public static string sAttachedbath = "";
        public static string sStayperiod = "";
        public static string sAccomodate = "";
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
        public List<PopContent> RentRange = new List<PopContent>()
        {
            new PopContent{ checkimg="RadioButtonUnChecked.png",rentamt="Any",pricefrom="0",ExpRent="100000"},
            new PopContent{ checkimg="RadioButtonUnChecked.png",rentamt="Up to $500",pricefrom="0", ExpRent="500"},
            new PopContent{ checkimg="RadioButtonUnChecked.png",rentamt="$500 - $1000",pricefrom="500",ExpRent="1000"},
            new PopContent{ checkimg="RadioButtonUnChecked.png",rentamt="$1000 and above",pricefrom="1000",ExpRent="100000"},

        };

        public Command SubmitOTPLCF { get; set; }
        public Command ResendOTP { get; set; }
        public bool Validations()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(txtrent))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtrent");
                myEntry.Focus();
                dialog.Toast("Enter Rent amount");
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
            if (string.IsNullOrEmpty(sStayperiod))
            {
                // Label myEntry = currentpage.FindByName<Label>("leasetypetxt");
                dialog.Toast("Select stayperiod");
                return false;
            }
            if (string.IsNullOrEmpty(sAccomodate))
            {
                Label myEntry = currentpage.FindByName<Label>("accomodatestxt");
                dialog.Toast("Select number of accomodates");
                return false;
            }
            if (string.IsNullOrEmpty(sAttachedbath))
            {
                //  Label myEntry = currentpage.FindByName<Label>("attachedbathtxt");
                dialog.Toast("Select attached bathtype");
                return false;
            }

           
            //if (!Regex.IsMatch(txtrent, Dollarpattern))
            //{
            //    Entry myEntry = currentpage.FindByName<Entry>("txtrent");
            //    myEntry.Focus();
            //    myEntry.Text = "";
            //    myEntry.Placeholder = "Dollar formate is required";
            //    myEntry.PlaceholderColor = Color.Red;
            //    dialog.Toast("Dollar formate required");
            //    return false;
            //}
            //if (txtrent.Length > 6)
            //{
            //    Entry myEntry = currentpage.FindByName<Entry>("txtrent");
            //    myEntry.Focus();
            //    myEntry.Text = "";
            //    myEntry.Placeholder = "Maximum 6 Digits only";
            //    myEntry.PlaceholderColor = Color.Red;
            //    dialog.Toast("Maximum 6 digits only");
            //    return false;
            //}
            return result;
        }
        public bool Validationssecond()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(Zipcode))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtlocation");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Enter Location";
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
                myEntry.Placeholder = "Please enter your email";
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
                myEntry.Placeholder = "Please enter your mobile number";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (ContactPhone.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Minimum 10 digits required";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (!CheckValidPhone(ContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
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
        public bool Validationsnewphone()
        {
            bool result = true;
            var currentpage = GetCurrentPage();

            if (string.IsNullOrEmpty(NewContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtnewcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Enter your mobile number";
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
            if (!CheckValidPhone(NewContactPhone))
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

        Response res = new Response();

        public List<PopContent> Rentrangelist { get; set; }
        public ICommand Selectrent { get; set; }
        public ICommand RentPopupTap { get; set; }
        //lcf
        CategoryList needcatlist = new CategoryList();
        public VMLCF(CategoryList clist)
        {
            try
            {
                
                //myneed
               if(clist.ismyneed!="1")
                {
                    sPricemode = "";
                    sAttachedbath = "";
                    sStayperiod = "";
                    sAccomodate = "";
                }
                Rentrangelist = RentRange;
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.primarycategoryvalue = clist.primarycategoryvalue;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.supercategoryvalue = clist.supercategoryvalue;
                string pid = Commonsettings.UserPid;


                if (clist.primarycategoryvalue == "Single Room")
                    res.primarycategoryvalue = "2";
                if (clist.primarycategoryvalue == "Shared Room")
                    res.primarycategoryvalue = "1";
                if (clist.primarycategoryvalue == "Paying Guest")
                    res.primarycategoryvalue = "4";

                MaleImg = "RadioButtonUnChecked.png";
                FeMaleImg = "RadioButtonUnChecked.png";
                AnyImg = "RadioButtonChecked.png";
                GenderMaleCommand = new Command(ChangeGenderMale);
                GenderFemaleCommand = new Command(ChangeGenderFemale);
                GenderAnyCommand = new Command(ChangeGenderAny);

                clist.countflag = countflag;
                //frame
                PopupContentTap = new Command(Closepopupcontent);
                ContentViewTap = new Command(CloseContentview);

                //NewLCF
                pricemodelistdata = pricemodelist;
                stayperiodlistdata = stayperiodlist;
                accomodatelistdata = accomodatelist;
                attachbathlistdata = attachbathlist;
                Rentrangelist = RentRange;

                TapBackPage = new Command(ClickBackPage);
                Taponpricemode = new Command<Posting_Param>(showpricemodelist);
                Taponstayperiod = new Command<Posting_Param>(showstayperiodlist);
                Taponaccomodates = new Command<Posting_Param>(showaccomodatelist);
                Taponattachedbathtype = new Command<Posting_Param>(showbathtypelist);
                RentPopupTap = new Command<PopContent>(Viewrentrange);

                Selectpricemode = new Command<Posting_Param>(SelectpricemodeID);
                Selectstayperiod = new Command<Posting_Param>(SelectstayperiodID);
                Selectaccomodate = new Command<Posting_Param>(Selectaccomodatecnt);
                Selectbathtype = new Command<Posting_Param>(SelectbathtypeID);
                Selectrent = new Command<PopContent>(Closerentpopup);

                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);

                lcfcmdnext = new Command(async () => await clicklcfcmdnext(res));
                lcfcmdskip = new Command(async () => await TapOnSkip(res));
                if(clist.ismyneed=="1")
                {
                    if(string.IsNullOrEmpty(clist.categoryname))
                    {
                        needcatlist.categoryname = clist.secondarycategoryvalue;
                    }
                    needcatlist = clist;
                    postsecondprefill(needcatlist);
                }
            }
            catch (Exception e) { }
        }
      
        public void postsecondprefill(CategoryList clist)
        {
            try
            {
                //var currentpage = GetCurrentPage();
                //if (clist.ispostdes == "1")
                //{
                //    //postthirdform
                //    AdTitle = clist.Title;
                //    AdDescription = clist.Description;
                //}
                //else
                //{
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
                    // Label stayperiodtxtclr = currentpage.FindByName<Label>("leasetypetxt");
                    // stayperiodtxtclr.TextColor = Color.FromHex("#212121");
                    accomodatestxt = clist.accomodate;
                    sAccomodate = clist.accomodate;

                    pricemodetxt = clist.pricemode;
                    sPricemode = clist.pricemode;
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
                txtrent = clist.Rent;
                    //if (!string.IsNullOrEmpty(clist.HideRent))
                    //{
                        //if (clist.HideRent == "1")
                        //{
                            //RentHideImg = "CheckBoxChecked.png";
                            //HideRent = "1";
                        //}
                        //else
                        //{
                            //RentHideImg = "CheckBoxUnChecked.png";
                            //HideRent = "0";
                        //}
                    //}

                    LocationName = TempLocationName = clist.Streetname;
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
                // }

                res.ExpRent = needcatlist.Rent;
                res.attachedbaths = sAttachedbath;
                //res.attachedbaths =sAttachedbath = needcatlist.attachedbaths;
                res.accomodate = sAccomodate = needcatlist.accomodate;
                res.availablefrm = needcatlist.availablefrm;
                
                res.pricemode = sPricemode = needcatlist.pricemode;
                res.stayperiod = sStayperiod = needcatlist.stayperiod;
                res.supercategoryid = needcatlist.supercategoryid;
                res.supercategoryvalue = needcatlist.supercategoryvalue;
                res.primarycategoryid = needcatlist.primarycategoryid;
                res.primarycategoryvalue = needcatlist.primarycategoryvalue;
                res.secondarycategoryid = needcatlist.secondarycategoryid;
                res.secondarycategoryvalue = needcatlist.secondarycategoryvalue;
                res.tertiarycategoryid = needcatlist.tertiarycategoryid.ToString();
                res.categoryname = needcatlist.categoryname;
                res.maincategory = needcatlist.maincategory;
                res.adtype = needcatlist.adtype;
                //res.categoryimgurl = needcatlist.categoryimgurl;
                // res.textcolor = needcatlist.textcolor;

                res.Locationname = LocationName = needcatlist.Locationname ;
                res.Newcityurl = newcityurl = needcatlist.Newcityurl;
                res.ContactName = ContactName = needcatlist.ContactName;
                res.ContactEmail = ContactEmail = needcatlist.ContactEmail;
                res.CountryCode = Countrycode = needcatlist.Countrycode;
                    
                res.ContactPhone = ContactPhone=needcatlist.ContactPhone;
                res.StateName = state = needcatlist.StateName;
                res.StateCode = statecode = needcatlist.Statecode;
                res.Zipcode = Zipcode = needcatlist.Zipcode;
                res.userlat  = needcatlist.Lat;
                res.userlong = needcatlist.Long;
                res.Country = needcatlist.Country;
                // res.countflag = needcatlist.countflag;

                res.adid = needcatlist.adid.ToString();
                res.agentid = needcatlist.agentid.ToString();

                res.Movedate = FrDate.ToString("MM/dd/yyyy");
                res.pricefrom = needcatlist.pricefrom;
                res.priceto = needcatlist.priceto;
                res.gender = Gender = needcatlist.gender;

                res.title = AdTitle=adtitletxt = needcatlist.title;
                res.description = addescriptiontxt= AdDescription = needcatlist.description;
                //res.type = needcatlist.type;
                res.usertype = needcatlist.usertype;

                //res.agentphotourl = needcatlist.agentphotourl;
                //res.attachedbathtype = sAttachedbath = needcatlist.attachedbathtype;


                //prefilldataadded
                res.service = needcatlist.service;
                res.Isagent = needcatlist.Isagent;
                res.Subcategory = needcatlist.Subcategory;
                res.Category = needcatlist.Category;
                res.Ispaid = needcatlist.Ispaid;
                //res.Photoscnt = needcatlist.Photoscnt;
                //res.HtmlPhotopath = needcatlist.HtmlPhotopath;
                //res.Imagepath = needcatlist.Imagepath;
                //res.XmlPhotopath = needcatlist.XmlPhotopath;
                //res.Adtype = needcatlist.Adtype;
                //res.Additionalcity = needcatlist.Additionalcity;
                //res.Citycount = needcatlist.Citycount;
                //res.Citytotalamount = needcatlist.Citytotalamount;
                res.Tags = needcatlist.Tags;
                //res.Companyweburl = needcatlist.Companyweburl;
                //res.url = needcatlist.url;
                //res.Bestindustry = needcatlist.Bestindustry;
                //res.Minsalary = needcatlist.Minsalary;
                //res.Maxsalary = needcatlist.Maxsalary;
                //res.Qualification = needcatlist.Qualification;
                //res.Visastatus = needcatlist.Visastatus;
                //res.Experience = needcatlist.Experience;
                //res.Experiencefrom = needcatlist.Experiencefrom;
                //res.Experienceto = needcatlist.Experienceto;
                //res.Employmenttype = needcatlist.Employmenttype;
                //res.Jobtype = needcatlist.Jobtype;
                //res.Jobroleid = needcatlist.Jobroleid;
                res.Postedby = needcatlist.Postedby;
                //res.Campaignid = needcatlist.Campaignid;
                //res.Customerid = needcatlist.Customerid;
                res.Ordertype = needcatlist.Ordertype;
                res.userpid = needcatlist.Userpid;
                //res.Mailerstatus = needcatlist.Mailerstatus;
                res.Displayphone = needcatlist.Displayphone;
                res.Status = needcatlist.Status;
                res.Contributor = needcatlist.Contributor;
                res.description = needcatlist.description;
                res.Titleurl = needcatlist.Titleurl;
                res.statecode = needcatlist.Statecode;
                res.title = needcatlist.title;
                res.Streetname = locationaddress=needcatlist.Streetname;
                res.Salarymodeid = needcatlist.Salarymodeid;
                res.Salarymode = needcatlist.Salarymode;
                res.Nationwide = needcatlist.Nationwide;
                res.Hideaddress = needcatlist.Hideaddress;
                //res.Isresumeavail = needcatlist.Isresumeavail;
                //res.Jobrole = needcatlist.Jobrole;
                res.Adpostedcnt = needcatlist.Adpostedcnt;
                res.Noofadcnt = needcatlist.Noofadcnt;
                res.Expirydate = needcatlist.Expirydate;
                res.Orderid = needcatlist.Orderid;
                res.Secondarycategoryvalueurl = needcatlist.Secondarycategoryvalueurl;
                res.Primarycategoryvalueurl = needcatlist.Primarycategoryvalueurl;
                res.Supercategoryvalueurl = needcatlist.Supercategoryvalueurl;
                res.Metrourl = needcatlist.Metrourl;
                res.Metro = needcatlist.Metro;
                //res.StateCode = needcatlist.Statecode;
                res.ismobileverified = needcatlist.Ismobileverified;
                res.Folderid = needcatlist.Folderid;
                res.Amount = needcatlist.Amount;
                res.Numberofdays = needcatlist.Numberofdays;
                res.Startdate = needcatlist.Startdate;
                res.Phone = needcatlist.Phone;
                res.Email = needcatlist.Email;
                res.Enddate = needcatlist.Enddate;
                res.Islisting = needcatlist.Islisting;
                res.Freeadsflag = needcatlist.Freeadsflag;
                res.Categoryflag = needcatlist.Categoryflag;
                
                res.Salespersonemail = needcatlist.Salespersonemail;
                res.Couponcode = needcatlist.Couponcode;
                res.Disclosedbusiness = needcatlist.Disclosedbusiness;
                res.Responsemail = needcatlist.Responsemail;
                res.Pendingpaycouponcode = needcatlist.Pendingpaycouponcode;
                res.businessname = needcatlist.businessname;
                res.ismyneed = needcatlist.ismyneed;
                //res.ipaddress = ipaddress;
                res.Category = res.maincategory = needcatlist.Category;
                res.ismyneedpayment = needcatlist.ismyneedpayment;
                if (!string.IsNullOrEmpty(needcatlist.availablefrm))
                {
                    if(needcatlist.availablefrm.Contains("/"))
                    {
                        FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);
                    }
                    else
                    {
                        FrDate = DateTime.ParseExact(needcatlist.availablefrm, "yyyy-mm-dd", null);
                    }
                    //2020-11-19
                    //FrDate = DateTime.ParseExact(needcatlist.availablefrm, "MM/dd/yyyy", null);
                    //FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);
                    //var datastring = clist.availablefrm;
                    //var format = "dd/MM/yyyy HH:mm:ss";
                    //var date = DateTime.ParseExact(datastring, format, System.Globalization.CultureInfo.InvariantCulture);
                    //FrDate = clist.availablefrm.tod DateTime.ParseExact(clist.availablefrm, "dd-MM-yyyy HH:mm:ss", null);
                    //DateTime.ParseExact("01-08-2012 15:36:25", "dd-MM-yyyy HH:mm:ss", null);
                }
            }
            catch (Exception ex)
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
        public void SelectpricemodeID(Posting_Param posting_Param)
        {
            var currentpage = GetCurrentPage();
            Label pricemodetxtclr = currentpage.FindByName<Label>("pricemodetxt");
            pricemodetxtclr.TextColor = Color.FromHex("#212121");
            pricemodetxt = posting_Param.pricemode;
            sPricemode = posting_Param.pricemode;

            contentviewvisible = false;
        }
        public void SelectstayperiodID(Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label stayperiodtxtclr = curntpg.FindByName<Label>("leasetypetxt");
            stayperiodtxtclr.TextColor = Color.FromHex("#212121");
            leasetypetxt = posting_Param.stayperiodtxt;
            sStayperiod = posting_Param.stayperiodID;
            contentviewvisible = false;
        }
        public void Selectaccomodatecnt(Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label accomodatestxtclr = curntpg.FindByName<Label>("accomodatestxt");
            accomodatestxtclr.TextColor = Color.FromHex("#212121");
            accomodatestxt = posting_Param.accomodatecnt.ToString();
            sAccomodate = posting_Param.accomodatecnt.ToString();
            contentviewvisible = false;
        }
        public void SelectbathtypeID(Posting_Param posting_Param)
        {
            var curntpg = GetCurrentPage();
            Label attachedbathtxtclr = curntpg.FindByName<Label>("attachedbathtxt");
            attachedbathtxtclr.TextColor = Color.FromHex("#212121");
            attachedbathtxt = posting_Param.attachedbath;
            sAttachedbath = posting_Param.attachbathtype;
            contentviewvisible = false;
        }
        public void Closerentpopup(PopContent popContent)
        {
            var currentpg = GetCurrentPage();
            txtrent = popContent.rentamt;
            res.pricefrom = popContent.pricefrom;
            res.ExpRent = popContent.ExpRent;
            Label pricemodetxtclr = currentpg.FindByName<Label>("txtrent");
            pricemodetxtclr.TextColor = Color.FromHex("#212121");
            contentviewvisible = false;
            //var contentlayout = currentpg.FindByName<ContentView>("contentlayout");
            //contentlayout.IsVisible = false;
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
            selectrentvisible = false;

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
            selectrentvisible = false;
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
            selectrentvisible = false;
        }
        public void showbathtypelist(Posting_Param posting_Param)
        {
            var curentpage = GetCurrentPage();
            var bathtype = curentpage.FindByName<ListView>("bathtype");

            //var contentviewframeheight = curentpage.FindByName<Frame>("Framelayout");
            //contentviewframeheight.HeightRequest = 280;
            //contentviewframeheight.WidthRequest = 280;

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
        }
        public VMLCF()
        {
            Rentrangelist = RentRange;
            Selectrent = new Command<PopContent>(Closerentrangepopup);
        }
        public void Closerentrangepopup(PopContent popContent)
        {
            var currentpg = GetCurrentPage();
            txtrent = popContent.rentamt;
            foreach (var amt in RentRange)
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
            res.pricefrom = popContent.pricefrom;
            res.ExpRent = popContent.ExpRent;
            Rentrangelist = RentRange;

            currentpg.Navigation.PopAllPopupAsync();
        }
        public void Viewrentrange(PopContent popContent)
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

        //lcfsecond
        public static string SelectedCityName = "";
        public VMLCF(Response clist, DateTime frdate, string gender, string textrent, string countflag)
        {
            try
            {

                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.primarycategoryvalue = clist.primarycategoryvalue;
                res.supercategoryvalue = clist.supercategoryvalue;
                res.gender = clist.gender;
                res.Movedate = clist.Movedate;
                res.pricefrom = clist.pricefrom;
                res.ExpRent = clist.ExpRent;
                res.Newcityurl = Locationurl;
                res.countflag = countflag;
                res.CountryCode = clist.CountryCode;
                res.Country = clist.Country;
                res.ContactName = clist.ContactName;
                res.stayperiod = clist.stayperiod;
                res.attachedbaths = clist.attachedbaths;
                res.ContactPhone = clist.ContactPhone;

                TapBackPage = new Command(ClickBackPage);

                //if(countflag=="1")
                //{
                //    Lcfskipvisible = true;
                //}
                //else
                //{
                //    Lcfskipvisible = false;
                //}

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
                SelectedCityName = TempLocationName;
                OnPropertyChanged(nameof(Countrycode));
                LocationCmd = new Command<Location>(OnLocationClick);
                if (latitude != 0)
                {
                    res.userlat = latitude.ToString();
                    res.userlong = longitude.ToString();
                    res.statecode = statecode;
                    res.Zipcode = zipcode.ToString();
                    res.LocationAddress = LocationName = locationaddress;
                    res.Country = country;
                    res.City = city;
                }
                else
                {
                    res.userlat = Commonsettings.UserLat.ToString();
                    res.userlong = Commonsettings.UserLong.ToString();
                    res.statecode = Commonsettings.Userstatecode;
                    res.Zipcode = Commonsettings.Userzipcode.ToString();
                    res.LocationAddress = Commonsettings.Usercity + " ," + Commonsettings.Userstatecode;
                    res.Country = Commonsettings.Usercountry;
                    res.City = Commonsettings.Usercity;
                }
                FrDate = frdate;
                ListingGender = gender;
                // txtrent = textrent;
                //lcfdescrptionsubmit = new Command<CategoryList>(lcfpostdescription);
                lcfdescrptionsubmit = new Command(async () => await lcfpostdescription(res));
                lcfcmdsubmit = new Command(async () => await clicklcfcmdsubmit(res));
                lcfcmdskip = new Command(async () => await TapOnSkip(res));
                insertlcf = new Command(async () => await insertintoLCF(res));
                if (clist.ismyneed == "1")
                {
                    //pst = clist;
                    clist.ispostdes = "1";
                    //postsecondprefill(pst);
                    res = clist;
                     needsprefill(clist);
                }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection!");
            }

        }

        public void needsprefill(Response clist)
        {
            try
            {
               
                if(clist.ispostdes=="1")
                {
                    AdTitle = adtitletxt = clist.title;
                    AdDescription = addescriptiontxt = clist.description;
                }
                else
                {
                    res.Locationname = LocationName = TempLocationName = clist.Locationname;
                    OnPropertyChanged(LocationName);
                    res.City = city = clist.City;
                    res.Newcityurl = newcityurl = clist.Newcityurl;
                    res.ContactName = ContactName = clist.Contributor;
                    res.ContactEmail = ContactEmail = clist.Email;
                    res.CountryCode = Countrycode = clist.CountryCode;
                    res.ContactPhone = ContactPhone = clist.Phone;
                    res.StateName = state = clist.StateName;
                    res.StateCode = statecode = clist.statecode;
                    res.Zipcode = Zipcode = clist.Zipcode;
                    res.Lat = clist.Lat;
                    res.Long = clist.Long;
                    res.Country = country = clist.Country;
                    res.countflag = clist.countflag;

                    res.adid = clist.adid;
                    res.agentid = clist.agentid;
                    res.pricefrom = clist.pricefrom;
                    res.priceto = clist.priceto;
                    res.gender = clist.gender;
                    res.ExpRent = clist.rent;
                    res.attachedbaths = sAttachedbath = clist.attachedbaths;

                    res.availablefrm = clist.availablefrm;
                    FrDate = DateTime.ParseExact(clist.availablefrm, "MM/dd/yyyy", null);


                    res.Streetname = locationaddress = clist.Streetname;
                    res.statecode = statecode = clist.statecode;
                    res.ismobileverified = clist.ismobileverified;
                    res.Folderid = clist.Folderid;
                    res.Amount = clist.Amount;
                    res.Numberofdays = clist.Numberofdays;
                    res.Startdate = clist.Startdate;
                    res.Phone = clist.Phone;
                    res.Email = clist.Email;
                    res.Enddate = clist.Enddate;
                    res.Islisting = clist.Islisting;
                    res.Freeadsflag = clist.Freeadsflag;
                    res.Couponcode = clist.Couponcode;
                    res.Disclosedbusiness = clist.Disclosedbusiness;
                    res.Gender = Gender = clist.Gender;
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
                    Zipcode = clist.Zipcode;
                    //BusinessName = clist.businessname;
                    //businessnametxt = clist.businessname;
                    ContactPhone = clist.Phone;

                    Countrycode = clist.CountryCode;
                    //ctl.ismyneed = clist.ismyneed;
                    //ctl.ismyneedpayment = clist.ismyneedpayment;
                    //ctl.movedate = clist.Movedate;
                    res.ismyneed = clist.ismyneed;
                    res.ismyneedpayment = clist.ismyneedpayment;
                    res.movedate = clist.Movedate;
                    if (!string.IsNullOrEmpty(clist.Movedate))
                    {
                        FrDate = DateTime.ParseExact(clist.Movedate, "MM/dd/yyyy", null);
                    }
                }
                

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async Task insertintoLCF(Response categoryList)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                if (Validationssecond())
                {
                    dialog.ShowLoading("");
                    // Response res = new Response();

                    if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                    {
                        var login = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        var logindata = await login.getuserpid(ContactName, ContactEmail);
                        Commonsettings.UserPid = logindata.resultinformation;
                        Commonsettings.UserName = ContactName;
                        Commonsettings.UserEmail = ContactEmail;
                        Commonsettings.UserMobileno = ContactPhone;
                    }

                    res.adtype = Commonsettings.CAdTypeID.ToString();

                    if (string.IsNullOrEmpty(res.RMcategory))
                        res.RMcategory = categoryList.primarycategoryvalue;
                    else
                        res.RMcategory = Commonsettings.CRTCategory;

                    if (res.countflag == "1")
                    {
                        Lcfskipvisible = true;
                    }
                    else
                    {
                        Lcfskipvisible = false;
                    }
                    res.supercategoryid = categoryList.supercategoryid;
                    res.primarycategoryid = categoryList.primarycategoryid;
                    res.secondarycategoryid = categoryList.secondarycategoryid;
                    res.primarycategoryvalue = categoryList.primarycategoryvalue;
                    res.supercategoryvalue = categoryList.supercategoryvalue;
                    res.categoryname = categoryList.categoryname;
                    res.maincategory = categoryList.maincategory;
                    res.gender = categoryList.gender;
                    // string sdate = categoryList.movedate.ToString("MM/dd/yyyy");
                    res.Movedate = categoryList.Movedate;
                    res.pricefrom = categoryList.pricefrom;
                    res.ExpRent = categoryList.ExpRent;
                    res.Locationname = LocationName;
                    res.ContactName = ContactName;
                    res.ContactEmail = ContactEmail;
                    if (!string.IsNullOrEmpty(Locationurl))
                        res.Newcityurl = Locationurl;
                    else
                        Locationurl = categoryList.Newcityurl;
                    res.metrourl = metrourl;
                    res.CountryCode = Countrycode;
                    res.Country = categoryList.Country;
                    res.ContactPhone = ContactPhone;
                    // Commonsettings.UserMobileno = ContactPhone;
                    res.devicename = Commonsettings.UserMobileOS;
                    res.ismobileverified = "1";
                    res.ipaddress = ipaddress;
                    res.deviceid = Commonsettings.UserDeviceId;
                    res.postedvia = Commonsettings.UserMobileOS;
                    res.userpid = Commonsettings.UserPid;

                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        res.devicetypeid = "1";
                    }
                    else
                        res.devicetypeid = "2";


                    //if (latitude != 0 && !string.IsNullOrEmpty(latitude.ToString()))
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
                    res.attachedbathtype = sAttachedbath;
                    res.lcftype = "lcf";


                    var OTPLCFSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    var data = await OTPLCFSubmit.OTPResponselcfsubmit(res, "1");

                    if (data != null)
                    {
                        var curpage = GetCurrentPage();
                       // data.type = "1";
                       // data.ispayment = 2;
                        if (data.type == "0")
                        {
                            res.isResponseotp = 1;
                            await curpage.Navigation.PushAsync(new lcfOTP(res, data.pinno));
                        }
                        else
                        {
                            Sortby_RM.orderby = "";
                            res.orderby = "date";
                            Filter_RM.priceto = "";
                            await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                            // }
                        }
                        dialog.HideLoading();
                    }
                    dialog.HideLoading();
                    /* LCF OTP End  */
                }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection!");
            }
        }
        public async Task TapOnSkip(Response categoryList)
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
            Sortby_RM.orderby = "";
            categoryList.orderby = "date";
            Filter_RM.priceto = "";
            Filter_RM.pricefrm = "";
            await currentpage.Navigation.PushAsync(new Category(categoryList, Locationurl));
        }

        //lcfOTP
        public VMLCF(Response clist, string pinno)
        {
            try
            {
                if(clist.ismyneed=="1")
                {
                    res = clist;
                }
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                ctl.supercategoryid = clist.supercategoryid;
                ctl.primarycategoryid = clist.primarycategoryid;
                ctl.secondarycategoryid = clist.secondarycategoryid;
                ctl.categoryname = clist.categoryname;
                ctl.maincategory = clist.maincategory;

                res.adtype = Commonsettings.CAdTypeID.ToString();
                res.RMcategory = Commonsettings.CRMCategory;
                res.supercategoryid = clist.supercategoryid;
                res.primarycategoryid = clist.primarycategoryid;
                res.secondarycategoryid = clist.secondarycategoryid;
                res.categoryname = clist.categoryname;
                res.maincategory = clist.maincategory;
                res.primarycategoryvalue = clist.primarycategoryvalue;
                res.supercategoryvalue = clist.supercategoryvalue;
                res.gender = clist.gender;
                res.Movedate = clist.Movedate;
                res.pricefrom = clist.pricefrom;
                res.ExpRent = clist.ExpRent;
                res.Locationname = clist.Locationname;
                res.adid = clist.adid;
                CityName = res.Locationname;
                //if(!string.IsNullOrEmpty(Locationurl))
                //{
                //    Locationurl = res.Newcityurl;
                //}
                //else
                //{
                //    res.Newcityurl = Locationurl;
                //}
                res.Newcityurl = clist.Newcityurl;
                res.ContactName = clist.ContactName;
                res.ContactEmail = clist.ContactEmail;
                res.CountryCode = clist.CountryCode;
                res.ContactPhone = clist.ContactPhone;
                res.devicetypeid = clist.devicetypeid;
                res.userpid = Commonsettings.UserPid;
                res.devicename = Commonsettings.UserMobileOS;
                res.postedvia = Commonsettings.UserMobileOS;
                res.ipaddress = ipaddress;
                res.deviceid = Commonsettings.UserDeviceId;
                res.lcftype = clist.lcftype;
                //if (string.IsNullOrEmpty(latitude.ToString()) || (latitude.ToString() == "0"))
                if (latitude != 0)
                {
                    res.userlat = latitude.ToString();
                    res.userlong = longitude.ToString();
                    res.Zipcode = zipcode.ToString();
                    res.statecode = statecode;
                    res.Country = country;
                    res.LocationAddress = locationaddress;
                    res.City = city;
                    res.Locationname = locationaddress;
                }
                else
                {
                    res.userlat = Commonsettings.UserLat.ToString();
                    res.userlong = Commonsettings.UserLong.ToString();
                    res.Zipcode = Commonsettings.Userzipcode;
                    res.statecode = Commonsettings.Userstatecode;
                    res.Country = Commonsettings.Usercountry;
                    res.LocationAddress = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                }

                res.pricemode = sPricemode;
                res.accomodate = sAccomodate;
                res.stayperiod = sStayperiod;
                res.attachedbaths = sAttachedbath;


                res.countflag = clist.countflag;
              //  res.Movedate = FrDate.ToString("yyyy-MM-dd");
                res.title = adtitletxt;
                res.description = addescriptiontxt;
                res.pricemode = sPricemode;
                res.attachedbathtype = sAttachedbath;
                res.accomodate = sAccomodate;

                res.allowpermission = List.ViewModels.VM_Detail.allowpermissionID.ToString();

                //SubmitOTPLCF = new Command(Click_SubmitOTPLCF);
                SubmitOTPLCF = new Command(async () => await Click_SubmitOTPLCF(res));
                ResendOTP = new Command(async () => await ClickResendOTP(res));
                //ResendOTP = new Command(ClickResendOTP);

                //NewCountrycode = Countrycode;
                ChangemobileNumber = new Command(ClickChangeMObile);
                SubmitChangeMobile = new Command(async () => await ClickSubmitChangeMobile(res));

                _Pinno = pinno;
            }
            catch (Exception e) { }

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
        public async Task clicklcfcmdnext(Response catlist)
        {
            try
            {
                if (Validations())
                {
                    dialog.ShowLoading("");

                    var curpage = GetCurrentPage();
                    if (needcatlist.ismyneed=="1" ||needcatlist.ismyneedpayment=="1")
                    {
                        res.ExpRent = txtrent;
                        res.attachedbaths = needcatlist.attachedbaths;
                        res.accomodate = sAccomodate;
                        res.availablefrm = FrDate.ToString("MM/dd/yyyy");
                        res.pricemode = sPricemode;
                        res.stayperiod = sStayperiod;
                        res.supercategoryid = needcatlist.supercategoryid;
                        res.supercategoryvalue = needcatlist.supercategoryvalue;
                        res.primarycategoryid = needcatlist.primarycategoryid;
                        res.primarycategoryvalue = needcatlist.primarycategoryvalue;
                        res.secondarycategoryid = needcatlist.secondarycategoryid;
                        res.secondarycategoryvalue = needcatlist.secondarycategoryvalue;
                        res.tertiarycategoryid = needcatlist.tertiarycategoryid.ToString();
                        res.categoryname = needcatlist.categoryname;
                        res.maincategory = needcatlist.maincategory;
                        res.adtype = needcatlist.adtype;
                        res.categoryimgurl = needcatlist.categoryimgurl;
                        // res.textcolor = needcatlist.textcolor;

                        res.Locationname = needcatlist.Locationname = LocationName;
                        res.Newcityurl = needcatlist.Newcityurl;
                        res.ContactName = ContactName;
                        res.ContactEmail = ContactEmail;
                        res.CountryCode = Countrycode;
                        res.ContactPhone = ContactPhone;
                        res.StateName = state;
                        res.StateCode = statecode;
                        res.Zipcode = Zipcode;
                        res.userlat = latitude.ToString();
                        res.userlong = longitude.ToString();
                        res.Country = needcatlist.Country;
                        // res.countflag = needcatlist.countflag;

                        res.adid = needcatlist.adid.ToString();
                        res.agentid = needcatlist.agentid.ToString();

                        res.Movedate = FrDate.ToString("MM/dd/yyyy");
                        res.pricefrom = needcatlist.pricefrom;
                        res.priceto = needcatlist.priceto;
                        res.gender = needcatlist.gender;

                        res.title = needcatlist.title;
                        res.description = needcatlist.description;
                        //res.type = needcatlist.type;
                        res.usertype = needcatlist.usertype;

                        res.agentphotourl = needcatlist.agentphotourl;
                        res.attachedbathtype = needcatlist.attachedbathtype;


                        //prefilldataadded
                        res.service = needcatlist.service;
                        res.Isagent = needcatlist.Isagent;
                        res.Subcategory = needcatlist.Subcategory;
                        res.Category = needcatlist.Category;
                        res.Ispaid = needcatlist.Ispaid;
                        res.Photoscnt = needcatlist.Photoscnt;
                        res.HtmlPhotopath = needcatlist.HtmlPhotopath;
                        res.Imagepath = needcatlist.Imagepath;
                        res.XmlPhotopath = needcatlist.XmlPhotopath;
                        //res.Adtype = needcatlist.Adtype;
                        res.Additionalcity = needcatlist.Additionalcity;
                        res.Citycount = needcatlist.Citycount;
                        res.Citytotalamount = needcatlist.Citytotalamount;
                        res.Tags = needcatlist.Tags;
                        //res.Companyweburl = needcatlist.Companyweburl;
                        res.url = needcatlist.url;
                        res.Bestindustry = needcatlist.Bestindustry;
                        //res.Minsalary = needcatlist.Minsalary;
                        //res.Maxsalary = needcatlist.Maxsalary;
                        //res.Qualification = needcatlist.Qualification;
                        //res.Visastatus = needcatlist.Visastatus;
                        //res.Experience = needcatlist.Experience;
                        //res.Experiencefrom = needcatlist.Experiencefrom;
                        //res.Experienceto = needcatlist.Experienceto;
                        //res.Employmenttype = needcatlist.Employmenttype;
                        //res.Jobtype = needcatlist.Jobtype;
                        //res.Jobroleid = needcatlist.Jobroleid;
                        res.Postedby = needcatlist.Postedby;
                        //res.Campaignid = needcatlist.Campaignid;
                        //res.Customerid = needcatlist.Customerid;
                        res.Ordertype = needcatlist.Ordertype;
                        res.userpid = needcatlist.Userpid;
                        //res.Mailerstatus = needcatlist.Mailerstatus;
                        res.Displayphone = needcatlist.Displayphone;
                        res.Status = needcatlist.Status;
                        res.Contributor = needcatlist.Contributor;
                        res.description = needcatlist.description;
                        res.Titleurl = needcatlist.Titleurl;
                        res.State = needcatlist.State;
                        res.title = needcatlist.title;
                        res.Streetname = locationaddress;
                        res.Salarymodeid = needcatlist.Salarymodeid;
                        res.Salarymode = needcatlist.Salarymode;
                        res.Nationwide = needcatlist.Nationwide;
                        res.Trimetros = needcatlist.Trimetros;
                        //res.Linkedinurl = needcatlist.Linkedinurl;
                        res.Hideaddress = needcatlist.Hideaddress;
                        //res.Isresumeavail = needcatlist.Isresumeavail;
                        //res.Jobrole = needcatlist.Jobrole;
                        res.Adpostedcnt = needcatlist.Adpostedcnt;
                        res.Noofadcnt = needcatlist.Noofadcnt;
                        res.Expirydate = needcatlist.Expirydate;
                        res.Orderid = needcatlist.Orderid;
                        //res.Industryname = needcatlist.Industryname;
                        //res.Industryid = needcatlist.Industryid;
                        res.Secondarycategoryvalueurl = needcatlist.Secondarycategoryvalueurl;
                        res.Primarycategoryvalueurl = needcatlist.Primarycategoryvalueurl;
                        res.Supercategoryvalueurl = needcatlist.Supercategoryvalueurl;
                        res.Metrourl = needcatlist.Metrourl;
                        res.Metro = needcatlist.Metro;
                        //res.StateCode = needcatlist.Statecode;
                        res.ismobileverified = needcatlist.Ismobileverified;
                        res.Folderid = needcatlist.Folderid;
                        res.Amount = needcatlist.Amount;
                        res.Numberofdays = needcatlist.Numberofdays;
                        res.Startdate = needcatlist.Startdate;
                        res.Phone = needcatlist.Phone;
                        res.Email = needcatlist.Email;
                        res.Enddate = needcatlist.Enddate;
                        res.Islisting = needcatlist.Islisting;
                        res.Freeadsflag = needcatlist.Freeadsflag;
                        res.Categoryflag = needcatlist.Categoryflag;
                        res.Banneramount = needcatlist.Banneramount;
                        res.Bannerstatus = needcatlist.Bannerstatus;
                        res.Companytype = needcatlist.Companytype;
                        res.Salespersonemail = needcatlist.Salespersonemail;
                        res.Couponcode = needcatlist.Couponcode;
                        res.Disclosedbusiness = needcatlist.Disclosedbusiness;
                        res.Gender = needcatlist.gender;
                        res.Responsemail = needcatlist.Responsemail;
                        res.Benefits = needcatlist.Benefits;
                        res.Pendingpaycouponcode = needcatlist.Pendingpaycouponcode;
                        res.Bonusamount = needcatlist.Bonusamount;
                        res.Bonusdays = needcatlist.Bonusdays;
                        res.Emailblastamount = needcatlist.Emailblastamount;
                        res.Addonamount = needcatlist.Addonamount;
                        res.businessname = needcatlist.businessname;
                        res.City = city;
                        res.ismyneed = needcatlist.ismyneed;
                        res.ipaddress = ipaddress;
                        res.Category = res.maincategory = needcatlist.Category;


                        res.Movedate = FrDate.ToString("yyyy-MM-dd");
                        res.gender = Gender;
                        res.Newcityurl = Locationurl;
                        res.pageno = 1;
                        res.ExpRent = txtrent;
                        await curpage.Navigation.PushAsync(new lcfsecond(res, FrDate, res.gender, catlist.ExpRent, countflag));
                        //await curpage.Navigation.PushAsync(new lcfsecond(needcatlist, FrDate, Gender, catlist.ExpRent, countflag));
                    }
                    else
                    {
                        
                        catlist.Movedate = FrDate.ToString("yyyy-MM-dd");
                        catlist.gender = Gender;
                        catlist.Newcityurl = Locationurl;
                        catlist.pageno = 1;
                        catlist.ExpRent = txtrent;

                        if (string.IsNullOrEmpty(catlist.furnishing))
                            catlist.furnishing = "999";
                        if (string.IsNullOrEmpty(catlist.petpolicy))
                            catlist.petpolicy = "999";

                        await curpage.Navigation.PushAsync(new lcfsecond(catlist, FrDate, Gender, catlist.ExpRent, countflag));
                    }
                    dialog.HideLoading();

                    //catlist.ExpRent = txtrent;
                    //catlist.pricefrom = "0";
                    // catlist.attachedbaths = "-1";
                    //catlist.pricefrom = Rentrange_Popup.pricefrom;
                    //catlist.ExpRent = Rentrange_Popup.priceto;
                    // catlist.primarycategoryvalue = VMcategory.IsChecked==truecategoryname;
                    //var roomAPI = RestService.For<Features.List.Interface.ILisCategory>(Commonsettings.RoommatesAPI);
                    //    NRIApp.Roommates.Features.List.Models.ListRowData response = await roomAPI.Getroomlist(catlist, Locationurl);

                    //    if (response.ROW_DATA.Count > 0)
                    //    {
                    //        countflag = "1";
                    //    }
                    //    else
                    //    {
                    //        countflag = "0";
                    //    }
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
                var LocationAPI = RestService.For<IRMCategory>(Commonsettings.Localservices);
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
        static float latitude = 0.00f, longitude = 0.00f; static string zipcode, locationaddress = "";
        public static string RM_metrourl = "";
        public void OnLocationClick(Location ld)
        {
            city = ld.city; statecode = ld.statecode; state = ld.statename; latitude = ld.latitude;
            longitude = ld.longitude; zipcode = ld.zipcode; country = ld.country; newcityurl = ld.newcityurl;
            _LocationName = TempLocationName = ld.city + ", " + ld.statecode;
            locationaddress = ld.city + ", " + ld.statecode;
            _Locationurl = newcityurl;
            metrourl = ld.metrourl;
            RM_metrourl = ld.metrourl;
            Zipcode = ld.zipcode;
            OnPropertyChanged(nameof(metrourl));
            OnPropertyChanged(nameof(LocationName));
            OnPropertyChanged(nameof(Locationurl));
            OnPropertyChanged(nameof(IsVisibleList));
            _isvisiblelist = false;
        }

        public async Task clicklcfcmdsubmit(Response categoryList)
        {

            try
            {
                
                if (Validationssecond())
                {
                    dialog.ShowLoading("");
                    if(res.ismyneed =="1"|| res.ismyneedpayment=="1")
                    {
                        if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                        {
                            var login = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var logindata = await login.getuserpid(ContactName, ContactEmail);
                            Commonsettings.UserPid = logindata.resultinformation;
                            Commonsettings.UserName = ContactName;
                            Commonsettings.UserEmail = ContactEmail;
                            Commonsettings.UserMobileno = ContactPhone;
                        }

                        res.adtype = Commonsettings.CAdTypeID.ToString();
                        if (string.IsNullOrEmpty(res.RMcategory))
                            res.RMcategory = categoryList.primarycategoryvalue;
                        else
                            res.RMcategory = Commonsettings.CRTCategory;

                        res.supercategoryid = categoryList.supercategoryid;
                        res.primarycategoryid = categoryList.primarycategoryid;
                        res.secondarycategoryid = categoryList.secondarycategoryid;
                        res.primarycategoryvalue = categoryList.primarycategoryvalue;
                        res.categoryname = categoryList.categoryname;
                        res.maincategory = categoryList.maincategory;
                        res.gender = categoryList.gender;
                        res.Movedate = categoryList.Movedate;
                        res.ExpRent = categoryList.ExpRent;
                        res.Locationname = LocationName;
                        res.ContactName = ContactName;
                        res.ContactEmail = ContactEmail;
                        if (!string.IsNullOrEmpty(Locationurl))
                            res.Newcityurl = Locationurl;
                        else
                            Locationurl = categoryList.Newcityurl;
                        res.metrourl = metrourl;
                        res.CountryCode = Countrycode;

                        res.ContactPhone = ContactPhone;
                        contactnumber = res.ContactPhone;
                        // Commonsettings.UserMobileno = ContactPhone;
                        res.devicename = Commonsettings.UserMobileOS;
                        res.ismobileverified = "1";
                        res.ipaddress = ipaddress;
                        res.deviceid = Commonsettings.UserDeviceId;
                        res.postedvia = Commonsettings.UserMobileOS;
                        res.userpid = Commonsettings.UserPid;
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
                            res.Country = country;
                            res.LocationAddress = locationaddress;
                            res.Locationname = locationaddress;
                        }
                        else
                        {
                            res.userlat = Commonsettings.UserLat.ToString();
                            res.userlong = Commonsettings.UserLong.ToString();
                            res.Zipcode = Commonsettings.Userzipcode;
                            res.statecode = Commonsettings.Userstatecode;
                            res.Country = Commonsettings.Usercountry;
                            res.LocationAddress = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        }

                        //res.pricemode = sPricemode;
                        //res.accomodate = sAccomodate;
                        //res.stayperiod = sStayperiod;
                        //res.attachedbaths = sAttachedbath;
                        //res.adid = categoryList.adid;
                        //res.countflag = categoryList.countflag;
                        res.Movedate = FrDate.ToString("yyyy-MM-dd");
                        res.pageno = 1;
                        var currentpage = GetCurrentPage();
                        res.lcftype = "post";
                        var curpage = GetCurrentPage();
                   
                        await curpage.Navigation.PushAsync(new LcfDescription(res, FrDate));


                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                        {
                            var login = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                            var logindata = await login.getuserpid(ContactName, ContactEmail);
                            Commonsettings.UserPid = logindata.resultinformation;
                            Commonsettings.UserName = ContactName;
                            Commonsettings.UserEmail = ContactEmail;
                            Commonsettings.UserMobileno = ContactPhone;
                        }

                        res.adtype = Commonsettings.CAdTypeID.ToString();
                        if (string.IsNullOrEmpty(res.RMcategory))
                            res.RMcategory = categoryList.primarycategoryvalue;
                        else
                            res.RMcategory = Commonsettings.CRTCategory;

                        res.supercategoryid = categoryList.supercategoryid;
                        res.primarycategoryid = categoryList.primarycategoryid;
                        res.secondarycategoryid = categoryList.secondarycategoryid;
                        res.primarycategoryvalue = categoryList.primarycategoryvalue;
                        res.categoryname = categoryList.categoryname;
                        res.maincategory = categoryList.maincategory;
                        res.gender = categoryList.gender;
                        // string sdate = categoryList.movedate.ToString("MM/dd/yyyy");
                        res.Movedate = categoryList.Movedate;
                        res.ExpRent = categoryList.ExpRent;
                        res.Locationname = LocationName;
                        res.ContactName = ContactName;
                        res.ContactEmail = ContactEmail;
                        if (!string.IsNullOrEmpty(Locationurl))
                            res.Newcityurl = Locationurl;
                        else
                            Locationurl = categoryList.Newcityurl;
                        res.metrourl = metrourl;
                        res.CountryCode = Countrycode;

                        res.ContactPhone = ContactPhone;
                        contactnumber = res.ContactPhone;
                        // Commonsettings.UserMobileno = ContactPhone;
                        res.devicename = Commonsettings.UserMobileOS;
                        res.ismobileverified = "1";
                        res.ipaddress = ipaddress;
                        res.deviceid = Commonsettings.UserDeviceId;
                        res.postedvia = Commonsettings.UserMobileOS;
                        res.userpid = Commonsettings.UserPid;
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
                            res.Country = country;
                            res.LocationAddress = locationaddress;
                            res.Locationname = locationaddress;
                        }
                        else
                        {
                            res.userlat = Commonsettings.UserLat.ToString();
                            res.userlong = Commonsettings.UserLong.ToString();
                            res.Zipcode = Commonsettings.Userzipcode;
                            res.statecode = Commonsettings.Userstatecode;
                            res.Country = Commonsettings.Usercountry;
                            res.LocationAddress = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        }

                        res.pricemode = sPricemode;
                        res.accomodate = sAccomodate;
                        res.stayperiod = sStayperiod;
                        res.attachedbaths = sAttachedbath;
                        res.adid = categoryList.adid;
                        res.countflag = categoryList.countflag;
                        res.Movedate = FrDate.ToString("yyyy-MM-dd");
                        res.pageno = 1;
                        var currentpage = GetCurrentPage();
                        res.lcftype = "post";
                        var curpage = GetCurrentPage();
                        var roomAPI = RestService.For<Features.List.Interface.ILisCategory>(Commonsettings.RoommatesAPI);
                        NRIApp.Roommates.Features.List.Models.ListRowData response = await roomAPI.Getroomlist(res, Locationurl);

                        if (response.ROW_DATA.Count > 0)
                        {
                            countflag = "1";
                        }
                        else
                        {
                            countflag = "0";
                        }
                        res.countflag = countflag;
                        await curpage.Navigation.PushAsync(new LcfDescription(res, FrDate));

                    }

                    dialog.HideLoading();
                    /* LCF OTP End  */
                }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection!");
            }
        }

        Postfirst pst = new Postfirst();
        public static string lcftype = "";
        public async Task lcfpostdescription(Response categoryList)
        {

            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                if (lcfdescriptionvalidation())
                {
                    dialog.ShowLoading("");
                    // Response res = new Response();
                    if(res.ismyneed=="1" && res.ismyneedpayment!="1")
                    {
                        res.title = AdTitle;
                        res.description = AdDescription;
                        res.lcftype = "post";
                        if(string.IsNullOrEmpty(res.categoryname))
                        {
                            res.categoryname = res.secondarycategoryvalue;
                        }
                        res.package = res.Ordertype;
                        res.mode = "edit";
                        var OTPLCFSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        var data = await OTPLCFSubmit.OTPResponselcfsubmitneedpost(res, "1");

                        if (data != null)
                        {
                            var curpage = GetCurrentPage();
                            if (data.type == "0")
                            {
                                await curpage.Navigation.PushAsync(new lcfOTP(res, data.pinno));
                            }
                            else
                            {
                                var PostSubmitpublish = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                                var publishad = await PostSubmitpublish.publishad(res.adid);
                                await curpage.Navigation.PushAsync(new lcfsuccess("success", 2, res, Locationurl));
                            }
                        }
                    }
                    else if(res.ismyneedpayment == "1")
                    {
                        res.title = AdTitle;
                        res.description = AdDescription;
                        res.lcftype = "post";
                        if (string.IsNullOrEmpty(res.categoryname))
                        {
                            res.categoryname = res.secondarycategoryvalue;
                        }
                        res.package = res.Ordertype;
                        //res.mode = "edit";
                        var OTPLCFSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        var data = await OTPLCFSubmit.OTPResponselcfsubmitneedpost(res, "1");

                        if (data != null)
                        {
                            var curpage = GetCurrentPage();
                            if (data.type == "0")
                            {
                                await curpage.Navigation.PushAsync(new lcfOTP(res, data.pinno));
                            }
                            else
                            {
                                if (data.ispayment == 1 && data.Result != "exists")
                                {
                                    //pst.ContactName = res.ContactName;
                                    //pst.ContactEmail = res.ContactEmail;
                                    //pst.ContactPhone = res.ContactPhone;
                                    //pst.StateCode = res.statecode;
                                    //pst.Countrycode = res.CountryCode;
                                    //pst.City = res.City;
                                    //pst.Country = res.Country;
                                    //pst.Zipcode = res.Zipcode;
                                    //pst.LocationAddress = res.LocationAddress;
                                    //pst.userpid = res.userpid;
                                    //pst.adid = Convert.ToInt32(data.lcfpostadid);
                                    await curpage.Navigation.PushAsync(new Package_RM(pst));
                                }
                                else
                                {
                                    //await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res.RMcategory, res.Movedate, res.gender, Locationurl));
                                    Sortby_RM.orderby = "";
                                    res.orderby = "date";
                                    Filter_RM.priceto = "";

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
                    else
                    {
                        res.adtype = Commonsettings.CAdTypeID.ToString();

                        if (string.IsNullOrEmpty(res.RMcategory))
                            res.RMcategory = categoryList.primarycategoryvalue;
                        else
                            res.RMcategory = Commonsettings.CRTCategory;

                        if (res.countflag == "1")
                        {
                            Lcfskipvisible = true;
                        }
                        else
                        {
                            Lcfskipvisible = false;
                        }

                        res.supercategoryid = categoryList.supercategoryid;
                        res.primarycategoryid = categoryList.primarycategoryid;
                        res.secondarycategoryid = categoryList.secondarycategoryid;
                        res.primarycategoryvalue = categoryList.primarycategoryvalue;
                        res.supercategoryvalue = categoryList.supercategoryvalue;
                        res.categoryname = categoryList.categoryname;
                        res.maincategory = categoryList.maincategory;
                        res.gender = categoryList.gender;
                        // string sdate = categoryList.movedate.ToString("MM/dd/yyyy");
                        res.Movedate = categoryList.Movedate;
                        res.pricefrom = categoryList.pricefrom;
                        res.ExpRent = categoryList.ExpRent;
                        res.Locationname = LocationName;
                        res.ContactName = ContactName;
                        res.ContactEmail = ContactEmail;
                        if (!string.IsNullOrEmpty(Locationurl))
                            res.Newcityurl = Locationurl;
                        else
                            Locationurl = categoryList.Newcityurl;
                        res.metrourl = metrourl;
                        res.CountryCode = categoryList.CountryCode;
                        res.ContactPhone = contactnumber;
                        // Commonsettings.UserMobileno = ContactPhone;
                        res.devicename = Commonsettings.UserMobileOS;
                        res.ismobileverified = "1";
                        res.ipaddress = ipaddress;
                        res.deviceid = Commonsettings.UserDeviceId;
                        res.postedvia = Commonsettings.UserMobileOS;
                        res.userpid = Commonsettings.UserPid;

                        if (Commonsettings.UserMobileOS == "iphone")
                        {
                            res.devicetypeid = "1";
                        }
                        else
                            res.devicetypeid = "2";


                        //if (latitude != 0 && !string.IsNullOrEmpty(latitude.ToString()))

                        if (latitude != 0)
                        {
                            res.userlat = latitude.ToString();
                            res.userlong = longitude.ToString();
                            res.Zipcode = zipcode.ToString();
                            res.statecode = statecode;
                            res.Country = country;
                            res.LocationAddress = locationaddress;
                            res.City = city;
                            res.Locationname = locationaddress;
                        }
                        else
                        {
                            res.userlat = Commonsettings.UserLat.ToString();
                            res.userlong = Commonsettings.UserLong.ToString();
                            res.Zipcode = Commonsettings.Userzipcode;
                            res.statecode = Commonsettings.Userstatecode;
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
                        res.attachedbathtype = sAttachedbath;
                        res.lcftype = "post";




                        var OTPLCFSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        var data = await OTPLCFSubmit.OTPResponselcfsubmit(res, "1");

                        if (data != null)
                        {
                            var curpage = GetCurrentPage();
                            if (data.type == "0")
                            {
                                await curpage.Navigation.PushAsync(new lcfOTP(res, data.pinno));
                            }
                            else
                            {
                                // data.ispayment = 2;
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
                                    await curpage.Navigation.PushAsync(new Package_RM(pst));
                                }
                                else
                                {
                                    //await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res.RMcategory, res.Movedate, res.gender, Locationurl));
                                    Sortby_RM.orderby = "";
                                    res.orderby = "date";
                                    Filter_RM.priceto = "";

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
                            dialog.HideLoading();
                        }
                    }
                    dialog.HideLoading();
                    /* LCF OTP End  */
                }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection!");
            }
        }

        public async Task Click_SubmitOTPLCF(Response res)
        {
            if (LCFOTP == _Pinno)
            {
                try
                {
                    dialog.ShowLoading("");
                    res.deviceid = Commonsettings.UserDeviceId;
                    res.postedvia = Commonsettings.UserMobileOS;
                    if (Commonsettings.UserMobileOS.ToLower() == "iphone")
                    {
                        res.devicetypeid = "1";
                    }
                    else
                        res.devicetypeid = "2";
                    if (string.IsNullOrEmpty(Locationurl))
                    {
                        Locationurl = res.Newcityurl;
                    }
                    else
                    {
                        Locationurl = res.Newcityurl;
                    }
                    var LCFOTPSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    res.ismobileverified = "1";
                    var curpage = GetCurrentPage();
                    if (res.ismyneed=="1"&& res.ismyneedpayment!="1")
                    {
                        var data = await LCFOTPSubmit.OTPResponselcfsubmitneedpost(res, "0");
                        try
                        {
                            if (data != null)
                            {
                                var PostSubmitpublish = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                                var publishad = await PostSubmitpublish.publishad(pst.adid.ToString());
                                await curpage.Navigation.PushAsync(new EditThankyou());
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
                                // data.ispayment = 2;
                                lcfsecond.postscuccesbackbtn = 1;
                                if (data.ispayment == 1 && data.Result != "exists")
                                {
                                    pst.ContactName = res.ContactName;
                                    pst.ContactEmail = res.ContactEmail;
                                    pst.ContactPhone = res.ContactPhone;
                                    pst.StateCode = res.statecode;
                                    pst.Countrycode = res.CountryCode;
                                    pst.Country = res.Country;
                                    pst.City = res.City;
                                    pst.Country = res.Country;
                                    pst.Zipcode = res.Zipcode;
                                    pst.LocationAddress = res.LocationAddress;
                                    pst.adid = Convert.ToInt32(data.lcfpostadid);
                                    pst.userpid = res.userpid;
                                    await curpage.Navigation.PushAsync(new Package_RM(pst));
                                }
                                else
                                {
                                    //  await curpage.Navigation.PushAsync(new lcfsuccess(data.Result, data.allrespid, res, Locationurl));
                                    freeadpost(data.adid, "1");
                                }

                            }
                            
                        }
                        catch (Exception e) { }
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
            catch(Exception e)
            {

            }
        }
        public async Task ClickResendOTP(Response res)
        {
            try
            {

                dialog.ShowLoading("");
                var resendotp = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
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

        public async Task ClickSubmitChangeMobile(Response res)
        {
            try
            {
                if (Validationsnewphone())
                {
                    dialog.ShowLoading("");
                    res.CountryCode = Countrycode;
                    res.ContactPhone = NewContactPhone;
                    var resendotp = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    var data = await resendotp.getresendotp(res.ContactPhone, res.CountryCode);
                    _Pinno = data.pinno;
                    IsBusy = false;
                    OTPIsBusy = true;
                    dialog.HideLoading();
                }
            }
            catch (Exception e) { }

        }

        public VMLCF(int isresponseotp, Response clist, string pinno)
        {
            string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
            ctl.supercategoryid = clist.supercategoryid;
            ctl.primarycategoryid = clist.primarycategoryid;
            ctl.secondarycategoryid = clist.secondarycategoryid;
            ctl.categoryname = clist.categoryname;
            ctl.maincategory = clist.maincategory;

            res.adtype = Commonsettings.CAdTypeID.ToString();
            res.RMcategory = Commonsettings.CRMCategory;
            res.supercategoryid = clist.supercategoryid;
            res.primarycategoryid = clist.primarycategoryid;
            res.secondarycategoryid = clist.secondarycategoryid;
            res.categoryname = clist.categoryname;
            res.maincategory = clist.maincategory;
            res.primarycategoryvalue = clist.primarycategoryvalue;
            res.supercategoryvalue = clist.supercategoryvalue;
            res.gender = clist.gender;
            res.Movedate = clist.Movedate;
            res.pricefrom = clist.pricefrom;
            res.ExpRent = clist.ExpRent;
            res.Locationname = clist.Locationname;
            res.adid = clist.adid;
            CityName = res.Locationname;
          
            res.Newcityurl = clist.Newcityurl;
            res.ContactName = clist.ContactName;
            res.ContactEmail = clist.ContactEmail;
            res.CountryCode = clist.CountryCode;
            res.ContactPhone = clist.ContactPhone;
            res.userpid = Commonsettings.UserPid;
            res.devicename = Commonsettings.UserMobileOS;
            res.postedvia = Commonsettings.UserMobileOS;
            res.ipaddress = ipaddress;
            res.deviceid = Commonsettings.UserDeviceId;
            res.lcftype = clist.lcftype;
           
            if (latitude != 0)
            {
                res.userlat = latitude.ToString();
                res.userlong = longitude.ToString();
                res.Zipcode = zipcode.ToString();
                res.statecode = statecode;
                res.Country = country;
                res.LocationAddress = locationaddress;
                res.City = city;
                res.Locationname = locationaddress;
            }
            else
            {
                res.userlat = Commonsettings.UserLat.ToString();
                res.userlong = Commonsettings.UserLong.ToString();
                res.Zipcode = Commonsettings.Userzipcode;
                res.statecode = Commonsettings.Userstatecode;
                res.Country = Commonsettings.Usercountry;
                res.LocationAddress = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            }

            res.pricemode = sPricemode;
            res.accomodate = sAccomodate;
            res.stayperiod = sStayperiod;
            res.attachedbaths = sAttachedbath;

            res.countflag = clist.countflag;
            res.Movedate = clist.Movedate;
            res.title = adtitletxt;
            res.description = addescriptiontxt;
            res.pricemode = sPricemode;
            res.attachedbathtype = sAttachedbath;
            res.accomodate = sAccomodate;

            //FromDetailpage
            res.Shortdesc = clist.Shortdesc;
            res.allowpermission = clist.allowpermission;
            //SubmitOTPLCF = new Command(Click_SubmitOTPLCF);
            SubmitOTPLCF = new Command(async () => await Click_ResponseSubmitOTPLCF(res));
            ResendOTP = new Command(async () => await ClickResendOTP(res));
            //ResendOTP = new Command(ClickResendOTP);

            //NewCountrycode = Countrycode;
            ChangemobileNumber = new Command(ClickChangeMObile);
            //SubmitChangeMobile = new Command(ClickSubmitChangeMobile);
            SubmitChangeMobile = new Command(async () => await ClickSubmitChangeMobile(res));

            _Pinno = pinno;
        }

        public async Task Click_ResponseSubmitOTPLCF(Response res)
        {
            if (LCFOTP == _Pinno)
            {
                try
                {
                    dialog.ShowLoading("");
                    res.deviceid = Commonsettings.UserDeviceId;
                    res.postedvia = Commonsettings.UserMobileOS;
                    if (Commonsettings.UserMobileOS == "iphone")
                    {
                        res.devicetypeid = "1";
                    }
                    else
                        res.devicetypeid = "2";
                    if (string.IsNullOrEmpty(Locationurl))
                    {
                        Locationurl = res.Newcityurl;
                    }
                    else
                    {
                        Locationurl = res.Newcityurl;
                    }
                    var LCFOTPSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    res.ismobileverified = "1";
                    var curpage = GetCurrentPage();
                    if (res.lcftype=="post" && res.ismyneed=="1" && res.ismyneedpayment!="1")
                    {
                        var data = await LCFOTPSubmit.OTPResponselcfsubmitneedpost(res, "0");
                        try
                        {
                            if (data != null)
                            {
                                var PostSubmitpublish = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                                var publishad = await PostSubmitpublish.publishad(pst.adid.ToString());
                                await curpage.Navigation.PushAsync(new EditThankyou());
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
                                    await curpage.Navigation.PushAsync(new Thankyou_RM());
                                }
                            }
                        }
                        catch (Exception e) { }
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
