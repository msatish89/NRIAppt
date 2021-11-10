using Acr.UserDialogs;
using NRIApp.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using NRIApp.LocalService.Features.Interfaces;
using Xamarin.Forms.Extended;
using NRIApp.LocalService.Features.Models;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Messaging;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Plugin.Connectivity;

namespace NRIApp.LocalService.Features.ViewModels
{
   public class LS_Listing_VM : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public static string Checkedservicevalues;
        public static string Checkedservicetext;
        public static string _Pinno;
        public static string _Adid;
        public InfiniteScrollCollection<LS_LISTINGMODEL_DATA> Primarytaglisting { get; set; }
        public ObservableCollection<LS_LISTINGMODEL_DATA> Primarytaglisting1 { get; set; }
        public ICommand Contactcommand { get; set; }
        public ICommand Contactrespcommand { get; set; }
        public ICommand GetDetailPage { get; set; }
        public ICommand Clicklistingsearch { get; set; }
        public ICommand Filtercommand { get; set; }
        public ICommand Sortcommand { get; set; }
        public ICommand Retrylisting { get; set; }
       
        string  country = string.Empty,sPremiumad=string.Empty;
        private bool _Listblk = false;
        public bool Listblk
        {
            get { return _Listblk; }
            set { _Listblk = value; }
        }
        private bool _Filterblkavail = true;
        public bool Filterblkavail
        {
            get { return _Filterblkavail; }
            set { _Filterblkavail = value; }
        }
        private bool _NoFilterblk = false;
        public bool NoFilterblk
        {
            get { return _NoFilterblk; }
            set { _NoFilterblk = value; }
        }
        

        private bool _Nolist = false;
        public bool Nolist
        {
            get { return _Nolist; }
            set { _Nolist = value; }
        }

        private Color _Timing1color;

        public Color Timing1color
        {
            get { return _Timing1color; }
            set { _Timing1color = value; }
        }
        private Color _Timing2color;

        public Color Timing2color
        {
            get { return _Timing2color; }
            set { _Timing2color = value; }
        }

        #region response 
        //Resp
        public static string Contactnumber;
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";


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
        private string _RespService = "Select Service";

        public string RespService {
            get
            {
                return _RespService;
            }
            set {
              
                _RespService = value.Trim();
               
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
        private string _Userpid = "";
        public string Userpid
        {
            get { return _Userpid; }
            set
            {

                _Userpid = value.Trim();
                OnPropertyChanged(Userpid);

            }
        }
        private string _Userloginname = "";
        public string Userloginname
        {
            get { return _Userloginname; }
            set
            {

                _Userloginname = value.Trim();
                OnPropertyChanged(Userloginname);

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
        //public string Leadlcfleaftags
        //{
        //    get { return _Leadlcfleaftags; }
        //    set
        //    {
        //        _Leadlcfleaftags = value;

        //        OnPropertyChanged(Leadlcfleaftags);
        //    }
        //}
        private bool _Isselectedservice = false;
        public bool Isselectedservice
        {
            get { return _Isselectedservice; }
            set
            {
                _Isselectedservice = value;
              
                OnPropertyChanged(nameof(Isselectedservice));
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
        public ICommand SubmitRespCommand { protected set; get; }
        public ICommand Showservice { protected set; get; }
        //Resp
        #endregion
        LS_RESP_FORM Sendlrf = new LS_RESP_FORM();

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public LS_Listing_VM(string ptagid,string cityurl)
        {
            Primarytaglisting = new InfiniteScrollCollection<LS_LISTINGMODEL_DATA>
            {
                OnLoadMore = async () =>
                {
                   
                    var items = new List<LS_LISTINGMODEL_DATA>();
                    if (Convert.ToInt32(Primarytaglisting.First().totalrecs) != Primarytaglisting.Count)
                    {


                        try
                        {
                            var page = Primarytaglisting.Last().pageno + 1;
                            IsBusy = true;
                            var primarytags = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                            var list = await primarytags.GetPrimaryListing(ptagid,cityurl,Convert.ToString(page), LS_ViewModel.distancefrom, LS_ViewModel.distanceto, LS_ViewModel.sortselected, LS_ViewModel.openorclose,Convert.ToString(Commonsettings.UserLat), Convert.ToString(Commonsettings.UserLong));
                            if (list.ROW_DATA.Count > 0)
                            {
                                foreach (var item in list.ROW_DATA)
                                {
                                    string[] servicescomma; string[] servicescolon;
                                    item.citystatecode = item.city + ", " + item.statecode + "";
                                    item.totalmiles = Math.Round(item.miles, 2) + " miles from "+Commonsettings.Usercity+", "+Commonsettings.Userstatecode;
                                    if (ViewModels.LS_ViewModel.sortselected == "distance" &&item.miles > 0)
                                    {
                                        item.ismilesavail = true;
                                    }
                                    else
                                    {
                                        item.ismilesavail = false;
                                    }

                                    if (string.IsNullOrEmpty(item.thumbimgurl))
                                    {
                                        item.NoimgVisible = true;
                                        item.ImgVisible = false;
                                    }
                                    else
                                    {
                                        item.NoimgVisible = false;
                                        item.ImgVisible = true;
                                    }
                                    if (string.IsNullOrEmpty(item.ratingavg))
                                    {
                                        item.rateavil = false;
                                        item.starimg = "Star0.png";
                                    }
                                    else
                                    {
                                        item.rateavil = true;
                                        item.starimg = "Star" + item.ratingavg + ".png";
                                    }
                                    if (item.calltotwilio == "1" && !string.IsNullOrEmpty(item.twilionumber) && !string.IsNullOrEmpty(item.twiliopin))
                                    {
                                        item.Novirtualno = false;
                                        item.Isvirtualno = true;
                                    }
                                   else if (string.IsNullOrEmpty(item.virtualno))
                                    {
                                        item.Novirtualno = true;
                                        item.Isvirtualno = false;
                                    }
                                    else
                                    {
                                        item.Novirtualno = false;
                                        item.Isvirtualno = true;
                                    }
                                    if (item.reviewcount == "0" || string.IsNullOrEmpty(item.reviewcount))
                                    {
                                        item.isreviewcount = false;
                                        item.reviews = "";
                                    }
                                    else
                                    {
                                        item.isreviewcount = true;
                                        if (item.reviewcount == "1")
                                        {
                                            item.reviews = item.reviewcount + " review";
                                        }
                                        else
                                        {
                                            item.reviews = item.reviewcount + " reviews";
                                        }
                                    }
                                    if (string.IsNullOrEmpty(item.offertitle))
                                    {
                                        item.isofferavail = false;
                                    }
                                    else
                                    {
                                        item.isofferavail = true;
                                    }
                                    if (string.IsNullOrEmpty(item.ratingavg))
                                    {
                                        item.isratingavail = false;
                                    }
                                    else
                                    {
                                        item.isratingavail = true;
                                    }
                                    if (item.adavailability == "1")
                                    {
                                        item.timing1 = "Open now - ";
                                        item.timing1avail = true;
                                        item.timing2 = "Today " + item.timing;
                                        item.timing2avail = true;
                                        item.timingavail = true;
                                        item.timingavailother = false;
                                    }
                                    else if (item.adavailability == "0")
                                    {
                                        item.timing2 = "Today " + item.timing;
                                        item.timing2avail = true;
                                        item.timing3 = "Closed now";
                                        item.timing3avail = true;
                                        item.timingavail = true;
                                        item.timingavailother = false;
                                    }
                                    else if (!string.IsNullOrEmpty(item.adavailability) && item.adavailability != "1" && item.adavailability != "0")
                                    {
                                        item.timing1 = item.adavailability;
                                        item.timing1avail = true;
                                        item.timingavail = true;
                                        item.timingavailother = false;
                                    }

                                    if (string.IsNullOrEmpty(item.adavailability))
                                    {
                                        item.timingavail = false;
                                        item.timingavailother = true;
                                        if (string.IsNullOrEmpty(item.timingstatus))
                                        {
                                            item.timingstatus = "By appointment";
                                        }
                                        item.timingother = item.timingstatus;
                                    }
                                    item.rating = item.ratingavg + ".0";
                                    
                                    servicescomma = item.servicewithid.Split(',');
                                    servicescolon = servicescomma.First().Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (!string.IsNullOrEmpty(servicescolon[0].ToString()))
                                    {
                                        item.levels = servicescolon[0].ToString() + " + "+ (servicescolon.Count()-1) + " more";
                                    }
                                    if (item.locationtype == "0")
                                    {
                                        item.streetname = item.citiescnttxt;
                                    }
                                    if (string.IsNullOrEmpty(item.streetname)) { item.streetname = item.city + " ," + item.statecode; }

                                    item.nobizimg = item.title.Substring(0, 1).ToUpper();
                                }
                            }
                            items = list.ROW_DATA;
                            IsBusy = false;

                            //var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                            //var list1 = currentpage.FindByName<ListView>("listdata");
                            //list1.ItemsSource = null;
                            //list1.ItemsSource = items;
                        }
                        catch (Exception ex) { }
                    }
                    return items;
                }
            };           
            Contactcommand = new Command<LS_LISTINGMODEL_DATA>(Getcall);
            Contactrespcommand = new Command<LS_LISTINGMODEL_DATA>(Getrespform);
            GetDetailPage = new Command<LS_LISTINGMODEL_DATA>(Redirecttodetailpage);
            Clicklistingsearch = new Command(Redirecttolistingserach);
            Filtercommand = new Command(Redirectofilterpage);
            Sortcommand = new Command(Redirectosortingpage);
            Retrylisting = new Command(Reloadlistingblock);
            getPrimarytagListing(ptagid, cityurl);
        }

        public void Reloadlistingblock()
        {
            string ptagid = "";

            if (ViewModels.LS_Listing_Search.issearchtext == 1)
            {
               
                if (!string.IsNullOrEmpty(ViewModels.LS_ViewModel.filterprimarytagid))
                {
                    ptagid = ViewModels.LS_ViewModel.filterprimarytagid;
                }
                else
                {
                    ptagid = ViewModels.LS_ViewModel.searchtagid;
                }

            }
            else if (!string.IsNullOrEmpty(ViewModels.LS_ViewModel.filterprimarytagid))
            {
                ptagid = ViewModels.LS_ViewModel.filterprimarytagid;
            }
            else
            {
                ptagid = ViewModels.LS_ViewModel.primarytagid;
            }
            getPrimarytagListing(ptagid, Commonsettings.Usercityurl);
        }

        public LS_Listing_VM(string ptagid, string premiumad,string adid,string type)
        {
            sPremiumad = premiumad;
            if (!string.IsNullOrEmpty(Commonsettings.UserEmail))
                Email = Commonsettings.UserEmail;
            if (!string.IsNullOrEmpty(Commonsettings.UserName ))
                Name = Commonsettings.UserName;
            if (!string.IsNullOrEmpty(Commonsettings.UserMobileno ))
                Contactno = Commonsettings.UserMobileno;
            if (!string.IsNullOrEmpty(Commonsettings.UserPid))
                Userpid = Commonsettings.UserPid;
            if (!string.IsNullOrEmpty(Commonsettings.Usercity))
                LeadCity = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            _Adid = adid;
            SubmitRespCommand = new Command(OnRespSubmit);
             Showservice = new Command(OnShowservice);
        }
    
        public LS_Listing_VM(LS_RESP_FORM lrf,string pinno)
        {
            Sendlrf.adid = lrf.adid;
            Sendlrf.email = lrf.email;
            Sendlrf.name = lrf.name;
            Sendlrf.description = lrf.description;
            Sendlrf.countrycode = lrf.countrycode;
            Sendlrf.country = lrf.countrycode;
            Sendlrf.city = lrf.city+" ,"+lrf.statecode;
            Sendlrf.mobileno = lrf.mobileno;
            Sendlrf.pagetype = lrf.pagetype;
            Sendlrf.premiumid = lrf.premiumid;
            Sendlrf.Selecteddate = lrf.Selecteddate;
            Sendlrf.selectedservicetypeid = lrf.selectedservicetypeid;
            Sendlrf.selectedservicetypetext = lrf.selectedservicetypetext;
            if (!string.IsNullOrEmpty(Commonsettings.UserName))
            {
                Sendlrf.loginusername = Commonsettings.UserName;
            }
            else
            {
                Sendlrf.loginusername = lrf.name;
            }

            if (!string.IsNullOrEmpty(Commonsettings.UserPid))
            {
                Sendlrf.userpid = Commonsettings.UserPid;
            }
            else
            {
                Sendlrf.userpid = "0";
            }
            _Pinno = pinno;
            SubmitLeadotp = new Command(OnSubmitRespOtp);
            ResendOTP = new Command(OnResendOTP);
            ChangemobileNumber = new Command(OnChangeMObile);
             SubmitChangeMobile = new Command(OnSubmitChangeMobile);

        }
        public async void OnSubmitRespOtp()
        {
            if (LeadOTP == _Pinno)
            {
                try
                {
                    string devicetypeid = "1";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("android"))
                    {
                        devicetypeid = "2";
                    }
                    _Dialog.ShowLoading(""); var adid = "";                   
                    var respform = RestService.For<ILS_Listing>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                    var data = await respform.PostRespForm(Sendlrf, "0",Commonsettings.UserDeviceId,devicetypeid,Commonsettings.UserMobileOS);
                    try
                    {
                        if (data != null)
                        {
                            adid = data.adid;
                        }

                        var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_LEAD_Thankyou());
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

                var data = await resendotp.OTPResnd(Sendlrf.mobileno, Sendlrf.countrycode);

                _Pinno = data.pinno;
                _Dialog.Toast("OTP has been sent to your mobile number");
                _Dialog.HideLoading();

            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }
        public  void OnChangeMObile()
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
                    Sendlrf.mobileno = NewContactnumber;
                    Sendlrf.countrycode = Newselectcontact;
                    Sendlrf.country = Newselectcontact;
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
        public void OnShowservice()
        {
            if (Isselectedservice)
            {
                Isselectedservice = false;
            }
            else { Isselectedservice = true; }
            OnPropertyChanged(nameof(Isselectedservice));
        }
        private async void getPrimarytagListing(string ptagid, string cityurl)
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;  
                if (connected==true)
                {
                    _Dialog.ShowLoading("");
                    if (string.IsNullOrEmpty(cityurl))
                    {
                        cityurl = Commonsettings.Usercityurl;
                    }
                    var primarylisting = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                    string userlat = Convert.ToString(Commonsettings.UserLat);
                    string userlong = Convert.ToString(Commonsettings.UserLong);
                    var data = await primarylisting.GetPrimaryListing(ptagid, cityurl, "1", LS_ViewModel.distancefrom, LS_ViewModel.distanceto, LS_ViewModel.sortselected, LS_ViewModel.openorclose,userlat,userlong );
                    var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var stackLayout = currentpage.FindByName<StackLayout>("nointernet");
                    var listblk = currentpage.FindByName<ListView>("listdata");
                    var nolistblk = currentpage.FindByName<StackLayout>("Nolistblk");
                    stackLayout.IsVisible = false;
                    if (data.ROW_DATA.Count>0)
                    {
                        listblk.IsVisible = true;
                        nolistblk.IsVisible = false;
                        if (data.ROW_DATA.Count < 2 &&LS_ViewModel.page != "filter")
                        {
                            Filterblkavail = false;
                            NoFilterblk = true;
                        }

                        OnPropertyChanged(nameof(Listblk)); OnPropertyChanged(nameof(Nolist));
                    OnPropertyChanged(nameof(Filterblkavail));
                    foreach (var item in data.ROW_DATA)
                    {
                        string[] servicescomma;string[] servicescolon;
                        item.citystatecode = item.city + ", " + item.statecode + "";
                        item.totalmiles =Math.Round(item.miles,2) + " miles from " + Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        if (ViewModels.LS_ViewModel.sortselected == "distance" && item.miles > 0)
                        {
                            item.ismilesavail = true;
                        }
                        else
                        {
                            item.ismilesavail = false;
                        }

                        if (string.IsNullOrEmpty(item.thumbimgurl))
                        {
                            item.NoimgVisible = true;
                            item.ImgVisible = false;
                        }
                        
                        else
                        {
                            item.NoimgVisible = false;
                            item.ImgVisible = true;
                        }
                        if (string.IsNullOrEmpty(item.ratingavg))
                        {
                            item.rateavil = false;
                            item.starimg = "Star0.png";
                        }
                        else
                        {
                            item.rateavil = true;
                            item.starimg = "Star" + item.ratingavg + ".png";
                        }
                            if (item.calltotwilio == "1" && !string.IsNullOrEmpty(item.twilionumber) && !string.IsNullOrEmpty(item.twiliopin))
                            {
                                item.Novirtualno = false;
                                item.Isvirtualno = true;
                            }
                        else if (string.IsNullOrEmpty(item.virtualno))
                        {
                            item.Novirtualno = true;
                            item.Isvirtualno = false;
                        }
                        else
                        {
                            item.Novirtualno = false;
                            item.Isvirtualno = true;
                        }
                        if (item.reviewcount == "0" || string.IsNullOrEmpty(item.reviewcount))
                        {
                            item.isreviewcount = false;
                            item.reviews = "";
                        }
                        else
                        {
                            item.isreviewcount = true;
                            if (item.reviewcount == "1")
                            {
                                item.reviews = item.reviewcount + " review";
                            }
                            else
                            {
                                item.reviews = item.reviewcount + " reviews";
                            }
                        }
                        if (string.IsNullOrEmpty(item.offertitle))
                        {
                            item.isofferavail = false;
                        }
                        else
                        {
                            item.isofferavail = true;
                        }
                        if (string.IsNullOrEmpty(item.ratingavg))
                        {
                            item.isratingavail = false;
                        }
                        else
                        {
                            item.isratingavail = true;
                        }
                        if (item.adavailability == "1")
                        {
                            item.timing1 = "Open now - ";
                            item.timing1avail = true;
                            item.timing2 = "Today " + item.timing;
                            item.timing2avail = true;
                            item.timingavail = true;
                            item.timingavailother = false;
                        }
                        else if (item.adavailability == "0")
                        {
                            item.timing2 = "Today " + item.timing;
                            item.timing2avail = true;
                            item.timing3 = "Closed now";
                            item.timing3avail = true;
                            item.timingavail = true;
                            item.timingavailother = false;
                        }
                        else if (!string.IsNullOrEmpty(item.adavailability) && item.adavailability != "1" && item.adavailability != "0")
                        {
                            item.timing1 = item.adavailability;
                            item.timing1avail = true;
                            item.timingavail = true;
                            item.timingavailother = false;
                        }

                        if (string.IsNullOrEmpty(item.adavailability))
                        {
                            item.timingavail = false;
                            item.timingavailother = true;
                            if (string.IsNullOrEmpty(item.timingstatus))
                            {
                                item.timingstatus = "By appointment";
                            }
                            item.timingother = item.timingstatus;
                        }
                        item.rating = item.ratingavg + ".0";
                        servicescomma = item.servicewithid.Split(',');
                        servicescolon = servicescomma.First().Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                        if (!string.IsNullOrEmpty(servicescolon[0].ToString()))
                        {
                            item.levels = servicescolon[0].ToString() + " + " + (servicescolon.Count() - 1) + " more";
                        }

                            if (item.locationtype=="0")
                            {
                                item.streetname = item.citiescnttxt;
                            }
                           if (string.IsNullOrEmpty(item.streetname))
                            {
                                item.streetname = item.city + " ," + item.statecode;
                            }

                        item.nobizimg = item.title.Substring(0, 1).ToUpper();
                    }
                    OnPropertyChanged(nameof(Primarytaglisting));

                    Primarytaglisting?.AddRange(data.ROW_DATA);
                    }
                else
                {

                        listblk.IsVisible = false;
                        nolistblk.IsVisible = true;
                    Filterblkavail = false;
                    OnPropertyChanged(nameof(Filterblkavail));
                   
                    }
                }
                else
                {
                    var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var stackLayout = currentpage.FindByName<StackLayout>("nointernet");
                    var listblk = currentpage.FindByName<ListView>("listdata");
                    var nolistblk = currentpage.FindByName<StackLayout>("Nolistblk");
                    listblk.IsVisible = false;
                    nolistblk.IsVisible = false;
                    Filterblkavail = false;
                    OnPropertyChanged(nameof(Listblk)); OnPropertyChanged(nameof(Nolist));
                    OnPropertyChanged(nameof(Filterblkavail));
                    stackLayout.IsVisible = true;

                }


                _Dialog.HideLoading();

            }
            catch (Exception e)
            {
                var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                var stackLayout = currentpage.FindByName<StackLayout>("nointernet");
                var listblk = currentpage.FindByName<ListView>("listdata");
                var nolistblk = currentpage.FindByName<StackLayout>("Nolistblk");
                _Dialog.HideLoading();
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == false)
                {
                    listblk.IsVisible = false;
                    nolistblk.IsVisible = false;
                    Filterblkavail = false;
                    OnPropertyChanged(nameof(Listblk)); OnPropertyChanged(nameof(Nolist));
                    OnPropertyChanged(nameof(Filterblkavail));
                    stackLayout.IsVisible = true;
                }
            }
           
        }

        public async void Getcall(LS_LISTINGMODEL_DATA LMD)
        {


            try
            {
                if (LMD.calltotwilio=="1")
                {
                    _Dialog.ShowLoading("");
                   
                    string dynamiccallpin =LMD.twiliopin;
                    string countrycode = LMD.countrycode;
                    _Dialog.HideLoading();
                    var answer = await _Dialog.ConfirmAsync("To connect, please enter this pin in dial pad after dialing user phone number.", dynamiccallpin, "OK", "");
                    if (answer)
                    {
                        var phonecall = CrossMessaging.Current.PhoneDialer;
                        if (phonecall.CanMakePhoneCall)
                            phonecall.MakePhoneCall(LMD.twilionumber, null);
                      
                    }
                }
                else if (LMD.calltotwilio == "2")
                {
                    var phonecall = CrossMessaging.Current.PhoneDialer;
                    if (phonecall.CanMakePhoneCall)
                        phonecall.MakePhoneCall(LMD.virtualno, null);
                }
                else
                {
                    var phonecall = CrossMessaging.Current.PhoneDialer;
                    if (phonecall.CanMakePhoneCall)
                        phonecall.MakePhoneCall(LMD.virtualno, null);
                }
            }
            catch(Exception e)
            {

            }
          
        }

        public async void Getrespform(LS_LISTINGMODEL_DATA LMD)
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            //await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Leadform.LS_RespForm(LMD.adid,LMD.premiumad));
           await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Leadform.LS_Prime_detail(LMD.adid, LMD.premiumad,LMD.titleurl,LMD.cityurl));
        }
       
        //Resp
        public bool Validations()
        {
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();

            bool result = true;
            if (string.IsNullOrEmpty(Name) || Name.Trim().Length == 0)
            {
                _Dialog.Toast("Enter Name");
                var entry = CurrentPage.FindByName<Entry>("uname");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Email) || Email.Trim().Length == 0)
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
            if (string.IsNullOrEmpty(Contactno) || Contactno.Trim().Length == 0)
            {
                _Dialog.Toast("Enter Contact Number");
                var entry = CurrentPage.FindByName<Entry>("umobileno");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(ViewModels.LS_Listing_VM.Checkedservicevalues))
            {
                _Dialog.Toast("Select Service");
                result = false;
                return false;
               
            }
            if (Date == null)
            {
                _Dialog.Toast("Choose Date");
                return false;
            }
            if (string.IsNullOrEmpty(Description))
            {
                _Dialog.Toast("Enter Description");
                return false;
            }
            return result;
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

        public async void OnRespSubmit()
        {

            string email = Email, ph = Contactno, name = Name, desc = Description, postedvia = "";//,
              //  ptagid = Leadlcfptagid, leaftags = Leadlcfleaftagid, postedvia = "";
            try
            {
                if (Validations())
                {



                    _Dialog.ShowLoading("");
                    if (!string.IsNullOrEmpty(Commonsettings.UserMobileOS))
                    {
                        if (Commonsettings.UserMobileOS == "iphone")
                        {
                            postedvia = "194";
                        }
                        else
                        {
                            postedvia = "197";
                        }

                    }

                    LS_RESP_FORM lrf = new LS_RESP_FORM()
                    {
                        adid = _Adid,
                        email = Email,
                        mobileno = Contactno,
                        name = Name,
                        description = Description,
                        selectedservicetypeid = ViewModels.LS_Listing_VM.Checkedservicevalues,
                        selectedservicetypetext = ViewModels.LS_Listing_VM.Checkedservicetext,
                        premiumid = sPremiumad,
                        pagetype = "Mobile Resp",
                        city = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode,
                        countrycode = Selectcontact.Replace("+", ""),
                        Selecteddate = Date.ToString("MM/dd/yyyy"),
                        loginusername = Name,
                        userpid = Userpid
                        
                    };
                    string devicetypeid = "1";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("android"))
                    {
                        devicetypeid = "2";
                    }
                    var respform = RestService.For<ILS_Listing>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                    var data = await respform.PostRespForm(lrf, "1",Commonsettings.UserDeviceId,devicetypeid,Commonsettings.UserMobileOS);
                    if (data != null)
                    {
                        var result = data.type;
                        var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                        if (result == "0")
                        {
                            await curpage.Navigation.PushAsync(new Views.Leadform.LS_LEAD_OTP(lrf, data.pinno));
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
            try { 
            await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ee)
            {

            }
        }
        //Resp
        public async void Redirecttodetailpage(LS_LISTINGMODEL_DATA LMD)
        {
            _Dialog.ShowLoading("");
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Detail(LMD));
        }
        public async void Redirecttolistingserach()
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing_Search(ViewModels.LS_ViewModel.searchtagid,Commonsettings.Usercityurl));
        }
        public async void Redirectofilterpage()
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing_Filter());
        }
        public async void Redirectosortingpage()
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing_Sorting());
        }
    }
}
