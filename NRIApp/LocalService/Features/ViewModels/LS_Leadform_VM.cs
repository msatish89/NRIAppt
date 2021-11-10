using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.Helpers;
using Plugin.Messaging;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Leadform_VM : INotifyPropertyChanged
    {


        IUserDialogs _Dialog = UserDialogs.Instance;


        public event PropertyChangedEventHandler PropertyChanged;

        public string Leadlcfptagid;
        public string Leadlcfptag;
        public string Leadlcfleaftagid;
        public int Leadlcfstagid = 0;
        public string Leadlcfstag;
        private string _Leadlcfleaftags { get; set; }
        public static string _Pinno;
        public static string _Businessid;
        public static string Contactnumber;
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";

        private string _NewContactnumber;
        public string NewContactnumber
        {
            get { return _NewContactnumber; }
            set { _NewContactnumber = value.Trim(); OnPropertyChanged(nameof(NewContactnumber)); }
        }
        private string _Newselectcontact = "+1";
        public string Newselectcontact
        {
            get { return _Newselectcontact; }
            set { _Newselectcontact = value; }
        }
        private string _Email = "";
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value.Trim();
                OnPropertyChanged(Email);

            }
        }
        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set
            {

                _Countrycode = value;
                OnPropertyChanged(Countrycode);

            }
        }
        public string _Tempcity = "";
        private string _LeadCity = "";
        public string LeadCity
        {
            get { return _LeadCity; }
            set
            {
                _LeadCity = value.Trim();
                if (string.IsNullOrEmpty(LeadCity))
                {
                    _Tempcity = "";
                }
                if (!string.IsNullOrEmpty(LeadCity) && _Tempcity != LeadCity)
                {
                    cityajax();
                }
            }
        }
        private DateTime _Date = DateTime.Now.Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private string _Contactno = "";
        public string Contactno
        {
            get { return _Contactno; }
            set
            {

                _Contactno = value.Trim();
                OnPropertyChanged(Contactno);

            }
        }

        private string _Name = "";
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {


                if (_Name.Length == 0)
                {
                    _Name = value.Trim();
                }
                else
                {
                    _Name = value;
                }



                OnPropertyChanged(Name);
            }
        }

        private string _Description = "";
        public string Description
        {
            get { return _Description; }
            set
            {

                _Description = value.Trim();
                OnPropertyChanged(Description);
            }
        }


        public string _LeadOTP { get; set; }

        public string LeadOTP
        {
            get { return _LeadOTP; }
            set { _LeadOTP = value; }
        }
        public string _Imagesource { get; set; }

        public string Imagesource
        {
            get { return _Imagesource; }
            set { _Imagesource = value; }
        }

        private string _Selectcontact = "+1";
        public string Selectcontact
        {
            get { return _Selectcontact; }
            set { _Selectcontact = value; }
        }
        public string Leadlcfleaftags
        {
            get { return _Leadlcfleaftags; }
            set
            {
                _Leadlcfleaftags = value;

                OnPropertyChanged(Leadlcfleaftags);
            }
        }
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private bool _Otpblock = true;
        public bool Otpblock
        {
            get { return _Otpblock; }
            set
            {
                _Otpblock = value;
                OnPropertyChanged(nameof(Otpblock));
            }
        }
        private bool _Isvisblesmall = false;
        public bool Isvisblesmall
        {
            get { return _Isvisblesmall; }
            set
            {
                _Isvisblesmall = value;
                OnPropertyChanged(nameof(Isvisblesmall));
            }
        }
        private bool _Isvisblebig = false;
        public bool Isvisblebig
        {
            get { return _Isvisblebig; }
            set
            {
                _Isvisblebig = value;
                OnPropertyChanged(nameof(Isvisblebig));
            }
        }
        private List<LOCATION_DATA> _BindLocation;
        public List<LOCATION_DATA> BindLocation
        {
            get { return _BindLocation; }
            set
            {
                _BindLocation = value;

            }
        }
        private List<LS_LEAD_DISTRIBUTE_BIZ_DATA> _Leadsuccessbiz;
        public List<LS_LEAD_DISTRIBUTE_BIZ_DATA> Leadsuccessbiz
        {
            get { return _Leadsuccessbiz; }
            set
            {
                _Leadsuccessbiz = value;

            }
        }
        private List<string> _LeadCountrycode;
        public List<string> LeadCountrycode
        {
            get { return _LeadCountrycode; }
            set { _LeadCountrycode = value; }
        }

        List<string> Country = new List<string>() {

            "+1","+91"
        };

        public ICommand SubmitCommand { protected set; get; }
        private Command _Backbtncommand;

        public Command Backbtncommand
        {
            get { return _Backbtncommand; }
            set { _Backbtncommand = value; }
        }
        public ICommand cityListcommand { protected set; get; }
        public Command SubmitLeadotp { get; set; }
        public Command ResendOTP { get; set; }
        public Command ChangemobileNumber { get; set; }
        public Command SubmitChangeMobile { get; set; }
        public Command Closecommand { get; set; }
        public ICommand Phonecallcommand { get; set; }
        public ICommand SkipLeadformcommand { get; set; }
        public ICommand Redirecttodetailcommand { get; set; }

       
       

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public LS_Leadform_VM(int stagid, string stag, string ptagid, string ptag, string checkvalue, string checktext)
        {
            string ltags = "", ltagid = "";
            Leadlcfptagid = ptagid;
            Leadlcfptag = ptag;
            ltags = checktext;
            ltagid = checkvalue;
            if (ltags != "")
            {
                ltags = ltags.Remove(ltags.Length - 1, 1);
            }
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                Email = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                Name = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Contactno = Commonsettings.UserMobileno;
            if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                _Tempcity = LeadCity = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;

            Leadlcfleaftags = ltags;
            Leadlcfleaftagid = ltagid;
            Leadlcfstagid = stagid;
            Leadlcfstag = stag;
            SubmitCommand = new Command(OnSubmit);
            Backbtncommand = new Command(async () => await BackbtncommandClick());
            cityListcommand = new Command<LOCATION_DATA>(cityListselection);
            SkipLeadformcommand = new Command(GetListing);
            _LeadCountrycode = Country;
            OnPropertyChanged(nameof(LeadCountrycode));
            //  SearchCommand = new Command(getlocationdata);
        }
        LS_LEAD_FORM Sendllf = new LS_LEAD_FORM();
        public LS_Leadform_VM(LS_LEAD_FORM llf, string pinno)
        {
            Sendllf.email = llf.email;
            Sendllf.mobileno = llf.mobileno;
            Sendllf.name = llf.name;
            Sendllf.ptagid = llf.ptagid;
            Sendllf.leaftagids = llf.leaftagids;
            Sendllf.leaftags = llf.leaftags;
            Sendllf.city = llf.city;
            Sendllf.statecode = llf.statecode;
            Sendllf.state = llf.state;
            Sendllf.latitude = llf.latitude;
            Sendllf.longitude = llf.longitude;
            Sendllf.zipcode = llf.zipcode; Sendllf.country = llf.country; Sendllf.countrycode = llf.countrycode;
            Sendllf.Selecteddate = llf.Selecteddate;
            Sendllf.supertagid = llf.supertagid; Sendllf.supertag = llf.supertag;
            Sendllf.ptag = llf.ptag;
            SubmitLeadotp = new Command(OnSubmitLeadOtp);
            ResendOTP = new Command(OnResendOTP);
            ChangemobileNumber = new Command(OnChangeMObile);
            SubmitChangeMobile = new Command(OnSubmitChangeMobile);
            _Pinno = pinno;
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
        public LS_Leadform_VM(string adid, string type)
        {
            if (type == "leadform")
            {
                _Dialog.ShowLoading("");
                
                if (adid != "0" && adid != null)
                {
                    BindingdistributedBusiness(adid);
                }
                else if (adid == "0" || string.IsNullOrEmpty(adid))
                {
                    Isvisblesmall = false;
                    Imagesource = "SuccessBg.png";
                    Isvisblebig = true;
                    OnPropertyChanged(nameof(Isvisblesmall));
                    OnPropertyChanged(nameof(Isvisblebig));
                    OnPropertyChanged(nameof(Imagesource));
                    _Dialog.HideLoading();
                }
                //  Closecommand = new Command(OnClosecommand);
                Phonecallcommand = new Command<LS_LEAD_DISTRIBUTE_BIZ_DATA>(Clicktocall);
                Redirecttodetailcommand = new Command<LS_LEAD_DISTRIBUTE_BIZ_DATA>(Redirecttodetailpage);
            }
            else if (type == "respform")
            {
                Isvisblebig = true;
                Isvisblesmall = false;
            }

        }

        public async void BindingdistributedBusiness(string adid)
        {
            try
            {
                _Dialog.ShowLoading("");
                await Task.Delay(2000);
                var DistributeBiz = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.Localservices);
                var data = await DistributeBiz.GetDistributedBusiness(adid);
               
                if (data.ROW_DATA.Count > 0)
                {


                    foreach (var item in data.ROW_DATA)
                    {
                        if (item.mobileno == "")
                        {
                            item.ismobile = false;
                        }
                        else { item.ismobile = true; }

                        item.citystatecode = item.city + ", " + item.statecode;
                    }

                    Leadsuccessbiz = data.ROW_DATA;
                    // await Task.Delay(5000);
                    Imagesource = "SuccessBg.png";
                    Isvisblesmall = true;
                    Isvisblebig = false;
                    OnPropertyChanged(nameof(Isvisblesmall));
                    OnPropertyChanged(nameof(Imagesource));
                    OnPropertyChanged(nameof(Leadsuccessbiz));
                    OnPropertyChanged(nameof(Isvisblebig));

                }
                else
                {
                    _Dialog.ShowLoading("");
                    Isvisblesmall = false;
                    Imagesource = "SuccessBg.png";
                    Isvisblebig = true;
                    OnPropertyChanged(nameof(Isvisblesmall));
                    OnPropertyChanged(nameof(Isvisblebig));

                    OnPropertyChanged(nameof(Imagesource));
                }
                _Dialog.HideLoading();

            }
            catch (Exception e) { }
        }

        public bool Validations()
        {
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();

            bool result = true;
            if (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Name, Namepattern))
            {
                _Dialog.Toast("Enter Name");                
                var entry = CurrentPage.FindByName<Entry>("uname");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Email))
            {
                _Dialog.Toast("Enter Email");
                var entry = CurrentPage.FindByName<Entry>("uemail");
                entry.Focus();
                return false;
            }
            if (!Regex.IsMatch(Email, Emailpattern))
            {
                _Dialog.Toast("Enter Proper Email");
                var entry = CurrentPage.FindByName<Entry>("uemail");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Contactno))
            {
                _Dialog.Toast("Enter Contact Number");
                var entry = CurrentPage.FindByName<Entry>("umobileno");
                entry.Focus();
                return false;
            }
            else if (Contactno.Length < 10)
            {

                Entry myEntry = CurrentPage.FindByName<Entry>("umobileno");
                myEntry.Focus();
                _Dialog.Toast("Please enter a 10-digit mobile number");
                return false;
            }
            else if (!CheckValidPhone(Contactno))
            {
                Entry myEntry = CurrentPage.FindByName<Entry>("umobileno");
                myEntry.Focus();
                _Dialog.Toast("Please enter valid mobile number");
                return false;
            }
            if (string.IsNullOrEmpty(LeadCity) || string.IsNullOrEmpty(_Tempcity))
            {
                _Dialog.Toast("Select City");
                var entry = CurrentPage.FindByName<Entry>("ucity");
                entry.Focus();
                return false;
            }
            if (Date == null)
            {
                _Dialog.Toast("Choose Date");
                return false;
            }
            return result;
        }
        public async void OnSubmit()
        {

            string email = Email, ph = Contactno, name = Name, desc = Description,
                ptagid = Leadlcfptagid, leaftags = Leadlcfleaftagid, postedvia = "";
            try
            {
                if (Validations())
                {
                    _Dialog.ShowLoading("");
                    if (!string.IsNullOrEmpty(Commonsettings.UserMobileOS))
                    {
                        //if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                        //{
                        //    postedvia = "194";
                        //}
                        //else
                        //{
                        //    postedvia = "197";
                        //}

                        postedvia = Commonsettings.UserMobileOS.ToLower();

                    }
                    

                    if (string.IsNullOrEmpty(city))
                    {
                        city = Commonsettings.Usercity;
                        statecode = Commonsettings.Userstatecode;
                        zipcode = Commonsettings.Userzipcode;
                    }
                 

                    LS_LEAD_FORM llf = new LS_LEAD_FORM()
                    {
                        email = Email,
                        mobileno = Contactno,
                        name = Name,
                        ptagid = Leadlcfptagid,
                        ptag = Leadlcfptag,
                        leaftagids = Leadlcfleaftagid,
                        leaftags = Leadlcfleaftags,
                        city = city,
                        statecode = statecode,
                        state = state,
                        latitude = latitude,
                        longitude = longitude,
                        zipcode = zipcode,
                        country = country,
                        countrycode = Selectcontact.Replace("+", ""),
                        Selecteddate = Date.ToString("MM/dd/yyyy"),
                        supertagid = Leadlcfstagid,
                        supertag = Leadlcfstag,
                        postedvia = postedvia
                    };
                    string devicetypeid = "1";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("android"))
                    {
                        devicetypeid = "2";
                    }
                    var leadform = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                    var data = await leadform.PostLeadForm(llf, "1", Commonsettings.UserDeviceId, devicetypeid, Commonsettings.UserMobileOS,Commonsettings.UserPid);
                    if (data != null)
                    {
                        var result = data.type;
                        var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                        if (result == "0")
                        {
                            await curpage.Navigation.PushAsync(new Views.Leadform.LS_LEAD_OTP(llf, data.pinno));
                            _Pinno = data.pinno;
                        }
                        else
                        {
                            await curpage.Navigation.PushAsync(new Views.Leadform.LS_LEAD_Thankyou(data.adid));
                        }

                    }

                    _Dialog.HideLoading();
                }
            }
            catch (Exception e)
            {

            }


        }
        public async Task BackbtncommandClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }catch(Exception ex) { }
        }

        public async void cityajax()
        {
            try
            {
                var location = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.Localservices);
                var data = await location.GetLocationData(LeadCity);
                foreach (var item in data.ROW_DATA)
                {
                    item.citystatecode = item.city + ", " + item.statecode;
                }
                _BindLocation = data.ROW_DATA;
                _isBusy = true;
                OnPropertyChanged(nameof(BindLocation));
                OnPropertyChanged(nameof(IsBusy));

            }
            catch (Exception e)
            {

            }
        }

        public async void Redirecttodetailpage(LS_LEAD_DISTRIBUTE_BIZ_DATA LDB){

            LS_LISTINGMODEL_DATA LMD = new LS_LISTINGMODEL_DATA()
            {
                adid = LDB.adid,
                viewno=LDB.mobileno,
                title=LDB.title,
                titleurl=LDB.titleurl,
                newcityurl=Commonsettings.Usercityurl
                
                
            };
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Detail(LMD));
        }

        string city = string.Empty, statecode = string.Empty, state = string.Empty, country = string.Empty, newcityurl = string.Empty;
        float latitude = 0.00f, longitude = 0.00f; string zipcode="";
        public void cityListselection(LOCATION_DATA ld)
        {
            city = ld.city; statecode = ld.statecode; state = ld.statename; latitude = ld.latitude;
            longitude = ld.longitude; zipcode =Convert.ToString(ld.zipcode); country = ld.country; newcityurl = ld.newcityurl;
            _LeadCity = _Tempcity = ld.city + ", " + ld.statecode;
            OnPropertyChanged(nameof(LeadCity)); OnPropertyChanged(nameof(IsBusy));
            _isBusy = false;

        }

        public async void OnSubmitLeadOtp()
        {
            if (LeadOTP == _Pinno)
            {
                try
                {
                    _Dialog.ShowLoading(""); var adid = "";

                    string devicetypeid = "1";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("android"))
                    {
                        devicetypeid = "2";
                    }
                    var postLead = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                    var data = await postLead.PostLeadForm(Sendllf, "0", Commonsettings.UserDeviceId,devicetypeid,Commonsettings.UserMobileOS,Commonsettings.UserPid);
                    try
                    {
                        if (data != null)
                        {
                            adid = data.adid;
                        }

                        var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_LEAD_Thankyou(adid));
                    }
                    catch (Exception e) { }
                    _Dialog.HideLoading();
                }
                catch (Exception e)
                {
                    _Dialog.Toast("Enter valid OTP");
                    _Dialog.HideLoading();
                }
            }
            else
            {
                _Dialog.Toast("Enter valid OTP");
            }
        }
        public async void OnResendOTP()
        {
            try
            {

                _Dialog.ShowLoading("");
                var resendotp = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.TechjobsAPI);

                var data = await resendotp.OTPResnd(Sendllf.mobileno, Sendllf.countrycode);

                _Pinno = data.pinno;
                _Dialog.Toast("OTP has been sent to your mobile number");
                _Dialog.HideLoading();

            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }

        public async void OnChangeMObile()
        {
            _isBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            _Otpblock = false; OnPropertyChanged(nameof(Otpblock));

        }
        public async void OnSubmitChangeMobile()
        {
            try
            {
                if (NewContactnumber != null && NewContactnumber != "")
                {
                    var resendotp = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);

                    var data = await resendotp.OTPResnd(NewContactnumber, Newselectcontact);
                    Sendllf.mobileno = NewContactnumber;
                    Sendllf.countrycode = Newselectcontact;
                    Sendllf.country = Newselectcontact;
                    _Pinno = data.pinno;
                    _Dialog.Toast("OTP has been sent to your mobile number");
                    _isBusy = false;
                    OnPropertyChanged(nameof(IsBusy));
                    _Otpblock = true; OnPropertyChanged(nameof(Otpblock));
                }
                else
                {
                    _Dialog.Toast("Enter valid mobile number");
                }
            }
            catch (Exception e) { }


        }
        public async void OnClosecommand()
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Servicetype());

        }
        public void Clicktocall(LS_LEAD_DISTRIBUTE_BIZ_DATA lead)
        {
            var phonecall = CrossMessaging.Current.PhoneDialer;
            if (phonecall.CanMakePhoneCall)
                phonecall.MakePhoneCall(lead.mobileno, null);
        }
        public async void GetListing()
        {
            ViewModels.LS_Listing_Search.issearchtext = 0;
            LS_ViewModel.isfiltered = 0;
            // ViewModels.LS_ViewModel.searchtag = Leadlcfptag;
            ViewModels.LS_ViewModel.searchtag = LS_ViewModel.primarytag;
            LS_ViewModel.sortselected = "date";LS_ViewModel.distancefrom = 0;LS_ViewModel.distanceto = 10;
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();            
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing());
        }
    }
}
