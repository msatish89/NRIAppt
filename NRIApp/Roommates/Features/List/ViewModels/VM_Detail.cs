using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.List.Interface;
using NRIApp.Roommates.Features.List.Models;
using NRIApp.Roommates.Features.List.Views;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.Views;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using Plugin.Connectivity;
using Plugin.MapsPlugin;
using Plugin.Share;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.Roommates.Features.List.ViewModels
{
    public class VM_Detail:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public Command RMTapOnThumbimgurl { get; set; }
        public Command TaptoShare { get; set; }
        public Command TapOnAdsave { get; set; }
        public Command TapOnMap { get; set; }
        public Command Taponstreetview { get; set; }
        public Command Tapontransportation { get; set; }
        public Command Taponhospital { get; set; }
        public Command TapOnReportFlag { get; set; }
        public Command TapOnReportEntry { get; set; }
        public ICommand selectreport { protected set; get; }
        public ICommand Reportsubmitcmd { protected set; get; }
        public Command CloseFlagpage { get; set; }
        public Command Contactcmd { get; set; }
        public Command ContactSubmit { get; set; }
        public Command CloseImgelst { get; set; }
        public Command TCcheckcmd { get; set; }

        public List<RMSaveAdList> AdSaveList { get; set; }
        public List<RMDeleteAdList> AdDeleteList { get; set; }
        public List<RMThumbimgurl> PhotourlList { get; set; }
        public List<RM_ReportflagData> ReportAd { get; set; }
        private List<RMamenity> _listofamenity;
        public List<RMamenity> Listofamenity
        {
            get { return _listofamenity; }
            set { _listofamenity = value; OnPropertyChanged(); }
        }
        private List<RMutility> _listofutilities;
        public List<RMutility> Listofutilities
        {
            get { return _listofutilities; }
            set { _listofutilities = value; OnPropertyChanged(); }
        }
        private List<ReportFlag> _listofreports;
        public List<ReportFlag> Listofreports
        {
            get { return _listofreports; }
            set { _listofreports = value; OnPropertyChanged(); }
        }
        string userpid = Commonsettings.UserPid;
        string photoadid = string.Empty;
        string Categoryurl = string.Empty;
        string Cityurl = string.Empty;
        string amenity = string.Empty;
        string utility = string.Empty;
        string PassContentid = string.Empty;
        string Reportid = string.Empty;
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        double citylat;
        double citylong;
        string mapstreetname = string.Empty;

        private bool _isvisible = false;
        public bool Isvisible
        {
            get { return _isvisible; }
            set { _isvisible = value;OnPropertyChanged(); }
        }
        private bool _openhouseschemavisible = false;
        public bool Openhouseschemavisible
        {
            get { return _openhouseschemavisible; }
            set { _openhouseschemavisible = value; OnPropertyChanged(); }
        }
        private string _openhousestart;
        public string Openhousestart
        {
            get { return _openhousestart; }
            set { _openhousestart = value; OnPropertyChanged(); }
        }
        private string _openhouseend;
        public string Openhouseend
        {
            get { return _openhouseend; }
            set { _openhouseend = value; OnPropertyChanged(); }
        }
        private string _adid;
        public string Adid
        {
            get { return _adid; }
            set { _adid = value; OnPropertyChanged(); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        private string _shortdesc;
        public string Shortdesc
        {
            get { return _shortdesc; }
            set { _shortdesc = value; OnPropertyChanged(); }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(); }
        }
        
        private string _pricefrom;
        public string Pricefrom
        {
            get { return _pricefrom; }
            set { _pricefrom = value; OnPropertyChanged(); }
        }
       
        private string _priceto;
        public string Priceto
        {
            get { return _priceto; }
            set { _priceto = value; OnPropertyChanged(); }
        }
        private string _availablefrm;
        public string Availablefrm
        {
            get { return _availablefrm; }
            set { _availablefrm = value; OnPropertyChanged(); }
        }
        private bool _availablefrmvisible;
        public bool Availablefrmvisible
        {
            get { return _availablefrmvisible; }
            set { _availablefrmvisible = value;OnPropertyChanged(); }
        }
        private string _detailimage;
        public string DetailImage
        {
            get { return _detailimage; }
            set { _detailimage = value;OnPropertyChanged(); }
        }
        private string _streetname;
        public string Streetname
        {
            get { return _streetname; }
            set { _streetname = value;OnPropertyChanged(); }
        }
        private string _smokepolicy;
        public string Smokepolicy
        {
            get { return _smokepolicy; }
            set { _smokepolicy = value;OnPropertyChanged(); }
        }
        private string _openhouseschema;
        public string Openhouseschema
        {
            get { return _openhouseschema; }
            set { _openhouseschema = value; OnPropertyChanged(); }
        }
        private string _attachedbaths;
        public string Attachedbaths
        {
            get { return _attachedbaths; }
            set { _attachedbaths = value; OnPropertyChanged(); }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); }
        }
        private string _accomodate;
        public string Accomodate
        {
            get { return _accomodate; }
            set { _accomodate = value; OnPropertyChanged(); }
        }
        private string _stayperiodfulltext;
        public string Stayperiodfulltext
        {
            get { return _stayperiodfulltext; }
            set { _stayperiodfulltext = value;OnPropertyChanged(); }
        }
        private string _primarycatval;
        public string Primarycatval
        {
            get { return _primarycatval; }
            set { _primarycatval = value; OnPropertyChanged(); }
        }
        private string _pricemode;
        public string Pricemode
        {
            get { return _pricemode; }
            set { _pricemode = value;OnPropertyChanged(); }
        }
        private string _amenities;
        public string Amenities
        {
            get { return _amenities; }
            set { _amenities = value;OnPropertyChanged(); }
        }
        private string _deposit;
        public string Deposit
        {
            get { return _deposit; }
            set { _deposit = value;OnPropertyChanged(); }
        }
        private string _utilities;
        public string Utilities
        {
            get { return _utilities; }
            set { _utilities = value;OnPropertyChanged(); }
        }
        private string _petpolicy;
        public string Petpolicy
        {
            get { return _petpolicy; }
            set { _petpolicy = value;OnPropertyChanged(); }
        }
        private int _totalrecs;
        public int Totalrecs
        {
            get { return _totalrecs; }
            set { _totalrecs = value;OnPropertyChanged(); }
        }
        
        private string _isfurnished;
        public string Isfurnished
        {
            get { return _isfurnished; }
            set { _isfurnished = value;OnPropertyChanged(); }
        }
        private string _isveg;
        public string IsVeg
        {
            get { return _isveg; }
            set { _isveg = value;OnPropertyChanged(); }
        }
        private bool _isvisibleamenity = false;
        public bool Isvisibleamenity
        {
            get { return _isvisibleamenity; }
            set { _isvisibleamenity = value;OnPropertyChanged(); }
        }
        private bool _isvisiblepetpolicy = false;
        public bool Isvisiblepetpolicy
        {
            get { return _isvisiblepetpolicy; }
            set { _isvisiblepetpolicy = value; OnPropertyChanged(); }
        }
        private bool _isvisiblefurnished = false;
        public bool Isvisiblefurnished
        {
            get { return _isvisiblefurnished; }
            set { _isvisiblefurnished = value; OnPropertyChanged(); }
        }
        private bool _isvisiblesmokepolicy = false;
        public bool Isvisiblesmokepolicy
        {
            get { return _isvisiblesmokepolicy; }
            set { _isvisiblesmokepolicy = value; OnPropertyChanged(); }
        }
        private bool _isvisibleutilities = false;
        public bool Isvisibleutilities
        {
            get { return _isvisibleutilities; }
            set { _isvisibleutilities = value; OnPropertyChanged(); }
        }
        private bool _isvisiblevegpreference = false;
        public bool Isvisiblevegpreference
        {
            get { return _isvisiblevegpreference; }
            set { _isvisiblevegpreference = value; OnPropertyChanged(); }
        }
        private bool _isvisibledeposit = false;
        public bool Isvisibledeposit
        {
            get { return _isvisibledeposit; }
            set { _isvisibledeposit = value; OnPropertyChanged(); }
        }
        private bool _ismobileverified = false;
        public bool Ismobileverified
        {
            get { return _ismobileverified; }
            set { _ismobileverified = value; OnPropertyChanged(); }
        }
        private string _adsaveimg ="Favorite.png";
        public string AdSaveimg
        {
            get { return _adsaveimg; }
            set { _adsaveimg = value; OnPropertyChanged(); }
        }
        private string _reportlist;
        public string Reportlist
        {
            get { return _reportlist; }
            set { _reportlist = value;OnPropertyChanged(); }
        }
        private bool _isvisiblereportlist = false;
        public bool IsVisibleReportList
        {
            get { return _isvisiblereportlist; }
            set { _isvisiblereportlist = value;OnPropertyChanged(); }
        }
        private string _contactEmail;
        public string ContactEmail
        { get { return _contactEmail; }
          set { _contactEmail = value;OnPropertyChanged(); }
        }
        private string _phoneno;
        public string Phoneno
        {
            get { return _phoneno; }
            set { _phoneno = value; OnPropertyChanged(); }
        }
        private string _contactname;
        public string contactname
        {
            get { return _contactname; }
            set { _contactname = value;OnPropertyChanged(); }
        }
        string Namepattern = "^[0-9A-Za-z ]+$";
        private string _selectcountry = "+1";
        public string Selectcountry
        {
            get { return _selectcountry; }
            set { _selectcountry = value; OnPropertyChanged(); }
        }
        private string _Countrycode="+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set
            {
                _Countrycode = value;
            }
        }

        private DateTime _FrDate = DateTime.Today;
        public DateTime FrDate
        {
            get { return _FrDate; }
            set { _FrDate = value; }
        }
        private string _movedate;
        public string Movedate
        {
            get { return _movedate; }
            set { _movedate = value;OnPropertyChanged(); }
        }
        private string _txtrent;
        public string txtrent
        {
            get { return _txtrent; }
            set { _txtrent = value;OnPropertyChanged(); }
        }
        private string _contactdetail = "";
        public string ContactDetail
        {
            get { return _contactdetail; }
            set { _contactdetail = value;OnPropertyChanged(); }
        }
        private string _flagreason = "";
        public string FlagReason
        {
            get { return _flagreason; }
            set { _flagreason = value;OnPropertyChanged(); }
        }
        private string _newtitleurl;
        public string newtitleurl
        {
            get { return _newtitleurl; }
            set { _newtitleurl = value; OnPropertyChanged(); }
        }
        private string _newcityurl;
        public string newcityurl
        {
            get { return _newcityurl; }
            set { _newcityurl = value; OnPropertyChanged(); }
        }
        private string _reportemail;
        public string ReportEmail
        {
            get { return _reportemail; }
            set { _reportemail = value; OnPropertyChanged(); }
        }
        private string _adtypetext;
        public string Adtypetext
        {
            get { return _adtypetext; }
            set { _adtypetext = value;OnPropertyChanged(); }
        }
        private bool _negotiablevisible = false;
        public bool negotiablevisible
        {
            get { return _negotiablevisible; }
            set { _negotiablevisible = value;OnPropertyChanged(nameof(negotiablevisible)); }
        }
        
        private bool _addressvisible = false;
        public bool addressvisible
        {
            get { return _addressvisible; }
            set { _addressvisible = value; OnPropertyChanged(nameof(addressvisible)); }
        }

        private string _adidDtl;
        public string adidDtl
        {
            get { return _adidDtl; }
            set { _adidDtl = value; OnPropertyChanged(nameof(adidDtl)); }
        }
        public string adresult;
        public static int allowpermissionID = 0;
        public static string cityurl = "";
        public VM_Detail(string Contentid, string Scityurl)
        {
            try
            {
                cityurl = Scityurl;
                adidDtl = PassContentid = Contentid;
                TapOnMap = new Command(TapOnMapView);
                Taponstreetview = new Command(TapOnStreetView);
                Tapontransportation = new Command(TapOnTranportation);
                Taponhospital = new Command(TapOnHospital);
                GetDetaildataList(Contentid);
                RMTapOnThumbimgurl = new Command(TapOnThumimgurl);
                TaptoShare = new Command(TapOnShare);
                TapOnAdsave = new Command(TapOnAdtoSave);
                TapOnReportFlag = new Command(Taponreportflag);
                Contactcmd = new Command(TaponContact);
            }
            catch(Exception ex)
            {

            }
        }
        public async void TaponContact()
        {
            dialogs.ShowLoading("");
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new Contact_RM(PassContentid));
            dialogs.HideLoading();
        }
       
        public async void GetDetaildataList(string Contentid)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
            {

                try
                {
                    dialogs.ShowLoading("");
                    string devieid = Commonsettings.UserDeviceId;
                    string pid = Commonsettings.UserPid;
                    if (pid == null)
                        pid = "";
                    var DetailData = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                    Models.DetailList response = await DetailData.GetDetailListData(Contentid, pid);
                    Shortdesc = response.RowData.Last().Shortdesc.Replace("<br/>", "\n");
                    City = response.RowData.Last().City;
                    Title = response.RowData.Last().Title;
                    
                    Totalrecs = response.RowData.Last().Totalrecs;
                    Gender = response.RowData.Last().Gendertext;
                    Cityurl = response.RowData.Last().City;
                    photoadid = response.RowData.Last().Adid;
                    PassContentid = Contentid;
                    Pricefrom = response.RowData.Last().Pricefrom.ToString();
                    Primarycatval = response.RowData.Last().Primarycategoryvalue;
                    citylat = response.RowData.Last().Citylat;
                    citylong = response.RowData.Last().Citylong;
                    mapstreetname = response.RowData.Last().Streetname;
                    Adtypetext ="Room"+" "+ response.RowData.Last().adtypetext;
                    newcityurl = response.RowData.Last().newcityurl;
                    newtitleurl = response.RowData.Last().newtitleurl;
                    Openhousestart = response.RowData.Last().openhousestarttime;
                    Openhouseend = response.RowData.Last().openhouseendtime;
                    if (response.RowData.Last().Isadsaved == 0)
                    {
                        AdSaveimg = "Favorite.png";
                        adresult = "0";
                    }
                    if (response.RowData.Last().Isadsaved == 1)
                    {
                        AdSaveimg = "FavoriteActive.png";
                        adresult = "1";
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Smokepolicy))
                    {
                        Isvisiblesmokepolicy = true;
                        if (response.RowData.Last().Smokepolicy == "0")
                        {
                            Smokepolicy = "No Smoking";
                        }
                        else if (response.RowData.Last().Smokepolicy == "1")
                        {
                            Smokepolicy = "Smoking is Ok";
                        }
                        else
                        {
                            Smokepolicy = "Smoking outside only";
                        }
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Petpolicy))
                    {
                        Isvisiblepetpolicy = true;
                        if (response.RowData.Last().Petpolicy == "0")
                        {
                            Petpolicy = "No Pets";
                        }
                        else if (response.RowData.Last().Petpolicy == "1")
                        {
                            Petpolicy = "Only Dogs";
                        }
                        else if (response.RowData.Last().Petpolicy == "2")
                        {
                            Petpolicy = "Only Cats";
                        }
                        else
                        {
                            Petpolicy = "Any Pet is Ok";
                        }
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Isfurnished))
                    {
                        Isvisiblefurnished = true;
                        if (response.RowData.Last().Isfurnished == "0")
                        {
                            Isfurnished = "Unfurnished";
                        }
                        else if (response.RowData.Last().Isfurnished == "1")
                        {
                            Isfurnished = "Furnished with Bed";
                        }
                        else if (response.RowData.Last().Isfurnished == "2")
                        {
                            Isfurnished = "Semi Furnished";
                        }
                        else if (response.RowData.Last().Isfurnished == "3")
                        {
                            Isfurnished = "Fully Furnished";
                        }
                        else
                        {
                            Isfurnished = "Not mentioned";
                        }
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Isveg))
                    {
                        Isvisiblevegpreference = true;
                        if (response.RowData.Last().Isveg == "1")
                        {
                            IsVeg = "Yes,Vegetarian mandatory";
                        }
                        if (response.RowData.Last().Isveg == "2")
                        {
                            IsVeg = "No, Non-veg is ok";
                        }
                        if (response.RowData.Last().Isveg == "3")
                        {
                            IsVeg = "Both";
                        }
                    }
                    if(response.RowData.Last().hiderent==0)
                    {
                        if (!string.IsNullOrEmpty(response.RowData.Last().Pricemode))
                        {
                            Isvisible = true;
                            Pricemode = "/ " + response.RowData.Last().Pricemode;
                        }
                        if (!string.IsNullOrEmpty(response.RowData.Last().Pricefrom.ToString()))
                        {
                            Isvisible = true;
                            Pricefrom = "$" + response.RowData.Last().Pricefrom;
                        }
                    }
                    else
                    {
                        Isvisible = true;
                        Pricefrom = "Contact for price";
                        Pricemode = "";
                    }
                   

                    if (response.RowData.Last().Deposit != 0 || !string.IsNullOrEmpty(response.RowData.Last().Deposit.ToString()))
                    {
                        string depositval = "$" + Math.Round((response.RowData[0].Deposit), 2);
                        if (depositval != "$0")
                        {
                            Isvisibledeposit = true;
                            Deposit = "Deposit: $" + Math.Round((response.RowData[0].Deposit), 2);
                        }

                    }
                    if (string.IsNullOrEmpty(response.RowData.Last().thumbimgurl) || response.RowData.Last().thumbimgurl == "0")
                    {
                        DetailImage = "RoommateNoImage.png";
                    }
                    else
                    {
                        DetailImage = response.RowData.Last().thumbimgurl;
                    }

                    if (!string.IsNullOrEmpty(response.RowData.Last().Availablefrm))
                    {
                        //adtype 1 -offered
                        Availablefrmvisible = true;
                        if (response.RowData.Last().adtype==1)
                        {
                            Availablefrm = "Available from : " + response.RowData.Last().Availablefrm;
                        }
                        else
                        {
                            Availablefrm = "Move in from : " + response.RowData.Last().Availablefrm;
                        }
                    }
                    if (response.RowData[0].Stayperiod == 1)
                    {
                        Stayperiodfulltext = "Stay : " + "Long term";
                    }
                    if (response.RowData[0].Stayperiod == 2)
                    {
                        Stayperiodfulltext = "Stay : " + "Short term";
                    }
                    if (response.RowData[0].Stayperiod == 0 || response.RowData[0].Stayperiod == 3)
                    {
                        Stayperiodfulltext = "Stay : " + "Long/Short Term";
                    }
                    if (!string.IsNullOrEmpty(response.RowData.Last().Accomodate.ToString()))
                    {
                        Isvisible = true;
                        Accomodate = "Accomodates : " + response.RowData.Last().Accomodate;
                    }
                    if (response.RowData[0].Attachedbaths == 0)
                    {
                        Attachedbaths = "Attached bath : " + "No";
                    }
                    else
                    {
                        Attachedbaths = "Attached bath : " + "Yes";
                    }
                    if(!string.IsNullOrEmpty(Openhousestart) && !string.IsNullOrEmpty(Openhouseend) && !string.IsNullOrEmpty(response.RowData.Last().openhouseschedule))
                    {
                        Openhouseschemavisible = true;
                        Openhouseschema = "Open House : " + response.RowData.Last().openhouseschedule+" ("+ response.RowData[0].openhousestarttime + " " + "to " + response.RowData[0].openhouseendtime +")";
                    }
                    else if (string.IsNullOrEmpty(Openhousestart) || string.IsNullOrEmpty(Openhouseend))
                    {
                        if (!string.IsNullOrEmpty(response.RowData.Last().openhouseschedule))
                        {
                            Openhouseschemavisible = true;
                            Openhouseschema = "Open House : " + response.RowData.Last().openhouseschedule;
                        }
                        else
                        {
                            Openhouseschemavisible = false;
                        }
                    }
                  
                    if (response.RowData.Last().Ismobileverified == 1)
                    {
                        Ismobileverified = true;
                    }
                    else { Ismobileverified = false; }

                    var lstofutilities = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                    Models.Listofutility utilitylst = await lstofutilities.GetutilityListofData(Contentid);
                    if (utilitylst.ROW_DATA.Count > 0)
                    {
                        Isvisibleutilities = true;
                        Listofutilities = utilitylst.ROW_DATA;
                    }
                    var lstofamenities = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                    Models.Listofamenity amenitylst = await lstofamenities.GetamenityListofData(Contentid);
                    if (amenitylst.ROW_DATA.Count > 0)
                    {
                        Isvisibleamenity = true;

                        //double nn = amenitylst.ROW_DATA[0].category.Count();
                        //int count = (int)Math.Round((nn / 2), MidpointRounding.AwayFromZero);
                        //var listone = amenitylst.ROW_DATA.Skip(count);
                        //amenitylst.ROW_DATA = listone.ToList();
                        Listofamenity = amenitylst.ROW_DATA;

                    }
                    if(response.RowData.Last().hideaddress==0)
                    {
                        addressvisible = true;
                        Streetname = response.RowData.Last().Streetname;
                    }
                    if (response.RowData.Last().Negotiable == "1" || response.RowData.Last().Negotiable == "1.0")
                    {
                        negotiablevisible = true;
                    }
                    dialogs.HideLoading();
                }
                catch (Exception ex)
                {
                    dialogs.HideLoading();
                    var connect = CrossConnectivity.Current.IsConnected;
                    if(connect==false)
                    dialogs.Toast("Kindly check your internet connection");
                }
            }
            else
            {
                dialogs.Toast("Kindly check your internet connection");
            }
        }
        public async void TapOnMapView()
        {
            await CrossMapsPlugin.Current.PinTo(mapstreetname, citylat, citylong, 8);
           // Device.OpenUri(new Uri("http://maps.google.com/?saddr=" + Commonsettings.UserLat + "," + Commonsettings.UserLong + "&daddr=" + citylat + "," + citylong));
            //ios http://maps.apple.com/?sll=50.894967,4.341626&z=10&t=s
          
        }
        public void TapOnStreetView()
        {
            if(Device.RuntimePlatform==Device.Android)
            {
                //Device.OpenUri(new Uri("google.streetview:cbll=" + citylat + "," + citylong +"&cbp=1,90,,0,1.0&mz=20"));
                Device.OpenUri(new Uri("http://maps.google.com/maps?q=&layer=c&cbll="+citylat+","+citylong+ "&cbp=1,90,0,0,1.0&mz=20"));
            }
            else if(Device.RuntimePlatform==Device.iOS)
            {

                Device.OpenUri(new Uri("google.streetview:cbll=" + citylat + "," + citylong + "&cbp=1,90,,0,1.0&mz=20"));
                
                
                // Device.OpenUri(new Uri("http://maps.apple.com/?q="+Streetname+"&sll="+citylat+","+citylong+"&z=10&t=h"));

                //Device.OpenUri(new Uri("comgooglemaps://?center="+citylat+","+ citylong +"&mapmode=streetview"));
                ////"https://?center="+citylat+","+ citylong +"&mapmode=streetview"
                ////https://?center=42.585444,13.007813,6z&mapmode=streetview
                ////maps://?q=dallas" data-rel="external"
                // Device.OpenUri("http://maps.apple.com/maps?q={0}&sll={1}", name.Replace(' ', '+'), loc)//apple doesnt like spaces in url


                //if (UIApplication.SharedApplication.CanOpenUrl(new Foundation.NSUrl("comgooglemaps://")))
                //    UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl("comgooglemaps://?center=" + citylat + "," + citylong + "&mapmode=streetview"));
                //else
                //{
                //   // Log.e("dasd", "Google maps not supported");
                //    UIAlertView _error = new UIAlertView("Opps", "Please install google maps to access this feature.", null, "Ok", null);
                //    _error.Show();
                //}

                //if (UIApplication.SharedApplication.CanOpenUrl(new Foundation.NSUrl("comgooglemaps://")))
                //{
                //    //Device.OpenUri(new Uri("comgooglemaps://?center=" + citylat + "," + citylong + "&mapmode=streetview"));
                //    UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl("comgooglemaps://?center=" + citylat + "," + citylong + "&mapmode=streetview"));
                //}
                //else
                //{
                //    UIAlertView alert = new UIAlertView()
                //    {
                //        Title = "oops",
                //        Message = "Please install google maps to access this feature."
                //    };
                //    alert.AddButton("OK");
                //    alert.Show();
                //}
            }
        }
        public void TapOnHospital()
        {
            string business = "hospitals";
            string city = Streetname;
            if (Device.RuntimePlatform == Device.Android)
            {
                // Device.OpenUri(new Uri("geo:0,0?q=" + business+" " + "near" + city));

                // Device.OpenUri(new Uri("http://maps.google.com/maps?&q=" + business + "near" + city + "&sll=" + citylat + "," + citylong + "&sspn=0.2,0.1&nav=1"));
                // Device.OpenUri(new Uri("http://maps.google.com/maps?&q="+business+"near"+city+"&sll="+citylat+","+citylong+"&sspn=0.2,0.1&nav=1"));
                // Device.OpenUri(new Uri("http://maps.google.com/?q=" + business + "near" + city));


                // Device.OpenUri(new Uri("http://maps.apple.com/?q=" + Streetname + "&sll=" + citylat + "," + citylong + "&z=10&t=h"));
                // Device.OpenUri(new Uri("http://maps.apple.com/?q=" + business + "near" + city + "&sll=" + citylat + "," + citylong + "&z=10&t=s"));

                //Device.OpenUri(new Uri(" http://maps.google.com/maps?&q=hospitals" + "near" + city + "&sll" + citylat + "," + citylong + "&sspn=0.2,0.1&nav= 1"));

                // Device.OpenUri(new Uri("http://maps.google.com/maps?q=hospitals" + "near" + city + "&directionsmode=transit"));
                //string url = "https://www.google.com/maps/search/?api=1&query="+ business+ " near " + city.Replace(" ", "+");
                //  url = url.Replace(" ", "%20");
                //  url = url.Replace(" ", "+");
                string url = "http://maps.apple.com/?q=" + business + " near " + city.Replace(" ", "+");
                Device.OpenUri(new Uri(url));

                //Device.OpenUri(new Uri("https://www.google.com/maps/search/?api=1&query=hospitals" + "near" + city.Replace(" ", "&nbsp")));
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                Device.OpenUri(new Uri("geo:0,0?q=" + business + " " + "near" + city));
                //http://maps.apple.com/?q=Mexican+Restaurant&sll=50.894967,4.341626&z=10&t=s
                //Device.OpenUri(new Uri("http://maps.apple.com/?q="+business+"&sll="+citylong+","+citylat+"&z=10&t=s"));
                //Device.OpenUri(new Uri("http://maps.apple.com/?q="+business+city.Replace(" ", "&#160")));
                //&#160; &nbsp; 
                // Device.OpenUri(new Uri("comgooglemaps://?q=Hospital" + "near" + "2052 John F.Kennedy Blvd" + "&center=37.759748,-122.427135"));
            }
        }
        public void TapOnTranportation()
        {
            string business = "Transportation";
            string city = Streetname;
            if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("http://maps.google.com/?q=" + business + "near" + city));
                // Device.OpenUri(new Uri("geo:0,0?q=" + business + "near" + city));
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                Device.OpenUri(new Uri("http://maps.apple.com/?q="+business+ city.Replace(" ", "&nbsp")));
                // Device.OpenUri(new Uri("http://maps.apple.com/?q=" + business + "near"+city+"&sll="+ citylat +","+citylong+"&z=10&t=s"));
                
            //http://maps.apple.com/?daddr=San+Francisco&dirflg=d&t=h
            }
        }
      

        public void TapOnShare()
        {
            //string text = "Hi, I found this Roommates listing in Sulekha.com and would like to share it with you , have a look at it and provide me your feedback about this property.";
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                //Text = text,
                Url = "http://indianroommates.sulekha.com/" + newtitleurl + "_" + newcityurl + "_" + PassContentid
            });
        }

        public async void TapOnAdtoSave()
        {
            string email = Commonsettings.UserEmail;
            var currentpage = GetCurrentPage();
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
               if(adresult =="0")
                    {
                        AdSaveimg = "FavoriteActive.png";
                        var savead = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                        Models.RMSaveAD adsaveresponse = await savead.SaveAdData(PassContentid, email, userpid);
                        AdSaveList = adsaveresponse.ROW_DATA;
                        adresult = "1";
                    dialogs.Toast("Ad saved successfully!");
                    }
               else
                {
                        AdSaveimg = "Favorite.png";
                        var deletead = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                        Models.RMDeleteAD addeleteresponse = await deletead.DeleteAdData(PassContentid, userpid);
                        AdDeleteList = addeleteresponse.ROW_DATA;
                        adresult = "0";
                    }
             }
        }
        public void Taponreportflag()
        {
            var currentpage = GetCurrentPage();
            Device.BeginInvokeOnMainThread(async () => await currentpage.Navigation.PushModalAsync(new Report_RM(PassContentid)));
        }
        public async void TapOnThumimgurl()
        {
            try
            {
                if (Totalrecs>0)
                {
                    //dialogs.ShowLoading();
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new ImageList_RM(PassContentid));
                    //dialogs.HideLoading();
                }
            }
            catch(Exception ex)
            {

            }

        }
        public VM_Detail(string adid,int type,int type2 )
        {
           try
            {
                PassContentid = adid;
                ContactEmail = Commonsettings.UserEmail;
                if (string.IsNullOrEmpty(ReportEmail))
                {
                    ReportEmail = Commonsettings.UserEmail;
                }
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    Phoneno = Commonsettings.UserMobileno;
                selectreport = new Command<ReportFlag>(Taponreportflaglist);
                TapOnReportEntry = new Command(taponreportentry);
                Reportsubmitcmd = new Command(Taponreportsubmitcmd);
                CloseFlagpage = new Command(TaponClose);
                List<ReportFlag> listofreports = new List<ReportFlag>();
                listofreports.Add(new ReportFlag { reportlist = "Spam/Fraud", reportid = 1 });
                listofreports.Add(new ReportFlag { reportlist = "Withdrawn Property lease", reportid = 4 });
                listofreports.Add(new ReportFlag { reportlist = "Unrevised price", reportid = 2 });
                listofreports.Add(new ReportFlag { reportlist = "Invalid Contact details", reportid = 6 });
                listofreports.Add(new ReportFlag { reportlist = "Unresponsive link", reportid = 7 });
                listofreports.Add(new ReportFlag { reportlist = "Incorrect photo", reportid = 8 });
                listofreports.Add(new ReportFlag { reportlist = "Invalid property details", reportid = 9 });
                listofreports.Add(new ReportFlag { reportlist = "Miscategorized", reportid = 3 });
                Listofreports = listofreports;
                Reportlist = listofreports.First().reportlist;
               
            }
            catch(Exception e)
            {

            }
        }
        public void taponreportentry()
        {
            IsVisibleReportList = true;
        }
        public void Taponreportflaglist(ReportFlag reportFlag)
        {
            Reportlist = reportFlag.reportlist;
            Reportid = reportFlag.reportid.ToString();
            IsVisibleReportList = false;
        }
        public async void Taponreportsubmitcmd()
        {
            if(validate())
            {
               try
                {
                    string Siteid = "32";
                    if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                        ContactEmail = Commonsettings.UserEmail;
                    if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                        contactname = Commonsettings.UserName;
                    if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                        Phoneno = Commonsettings.UserMobileno;
                    string reportid = Reportid;
                    if(string.IsNullOrEmpty(reportid))
                    {
                        reportid = "1";
                    }
                    string adid = PassContentid;
                    Phoneno = Commonsettings.UserMobileno;
                    string adurl = "http://indianroommates.sulekha.com/"+newtitleurl+"_"+newcityurl+"_"+adid;
                    string email = ContactEmail;
                    string sitename = "indianroommates.sulekha.com";
                    if (Title == null)
                        Title = "";
                    if (string.IsNullOrEmpty(Phoneno))
                        Phoneno = "";
                    if (string.IsNullOrEmpty(userpid))
                        userpid = "";
                    var report = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                    Models.RMReportflag reportsubmit = await report.GetReport(adid,userpid,Title,sitename,Phoneno,reportid,adurl,Siteid,FlagReason,ReportEmail);
                    ReportAd = reportsubmit.ROW_DATA;
                    //if (ReportAd[0].resultinfo == "inserted success")
                    //{
                    //    dialogs.Toast("Successfully reported.");
                    //}
                    //else
                    //{
                    //    dialogs.Toast("You are already reported!");
                    //}
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PopModalAsync();
                    // Device.BeginInvokeOnMainThread(async () => await currentpage.Navigation.PopModalAsync());
                }
                catch (Exception e)
                {

                }
            
            }
            
        }
        public void TaponClose()
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PopModalAsync();
        }
        public async void getdetailpage()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PopAsync();
        }
        //public bool Reportvalidation()
        //{
        //    bool result = false;
        //    if (string.IsNullOrEmpty(selectedreportid))
        //    {
        //        dialogs.Toast("Please select the reson");
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Reportdesc))
        //    {
        //        dialogs.Toast("Please wirte a description");
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Reportmail))
        //    {
        //        dialogs.Toast("Please wirte a email");
        //        return false;
        //    }
        //    if (!Regex.IsMatch(Reportmail, Emailpattern))
        //    {
        //        dialogs.Toast("Enter Proper Email");
        //        return false;
        //    }

        //    return result;
        //}
        public bool validate()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(Reportlist))
            {
                Entry myEntry = currentpage.FindByName<Entry>("Reportlist");
                //myEntry.Focus();
                myEntry.Placeholder = "Select anyone";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(ReportEmail))
            {
                //Entry myentry = currentpage.FindByName<Entry>("ReportEmail");
                NRIApp.Controls.CustomEntry myentry = currentpage.FindByName<NRIApp.Controls.CustomEntry>("ReportEmail");
                //ReportEmail = "";
                //myentry.Text = "";
                //myentry.Focus();
               
                //myentry.Placeholder = "Please enter your email id";
                //myentry.PlaceholderColor = Color.Red;
                dialogs.Toast("Please enter your email id");
                return false;
            }
           
            if (!Regex.IsMatch(ReportEmail, Emailpattern))
            {
                Entry myentry = currentpage.FindByName<Entry>("ReportEmail");
               // myentry.Focus();
                //myentry.Text = "";
                //myentry.Placeholder = "Please enter your email id";
                //myentry.PlaceholderColor = Color.Red;
                dialogs.Toast("Please enter a valid email id");
                return false;
            }
            if (string.IsNullOrEmpty(FlagReason))
            {
                Entry myentry = currentpage.FindByName<Entry>("reasondesc");
                //myentry.Focus();
                dialogs.Toast("Please write a description");
                // dialogs.Alert("Please write a description");
                return false;
            }
            if (string.IsNullOrEmpty(Phoneno))
            {
                NRIApp.Controls.CustomEntry myEntry = currentpage.FindByName<NRIApp.Controls.CustomEntry>("txtcontactphone");
                //myEntry.Focus();
                //myEntry.Text = "";
                //myEntry.Placeholder = "Please enter your mobile number";
                //myEntry.PlaceholderColor = Color.Red;
                dialogs.Toast("Please enter your mobile number");
                return false;
            }
            if (Phoneno.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                //myEntry.Focus();
                //myEntry.Text = "";
                //myEntry.Placeholder = "Minimum 10 Digits required";
                //myEntry.PlaceholderColor = Color.Red;
                dialogs.Toast("Minimum 10 Digits required");
                return false;
            }
            if (!CheckValidPhone(Phoneno))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                //myEntry.Focus();
                //myEntry.Text = "";
                //myEntry.Placeholder = "Please enter valid mobile number";
                //myEntry.PlaceholderColor = Color.Red;
                dialogs.Toast("Please enter valid mobile number");
                return false;
            }
            return result;
        }
       
        public bool Validation()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(contactname) || !Regex.IsMatch(contactname, Namepattern))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactname");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your name";
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
            if (string.IsNullOrEmpty(Phoneno))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your mobile number";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (Phoneno.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Minimum 10 Digits required";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (!CheckValidPhone(Phoneno))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter valid mobile number";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(ContactDetail) && ContactDetail=="")
            {
                Editor myEntry = currentpage.FindByName<Editor>("reasondesc");
                myEntry.Focus();
                //dialogs.Alert("Details field is required");
                dialogs.Toast("Details field is required");
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
        public VM_Detail(string adid,int type,int type1,int type2)
        {
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                ContactEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                contactname = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Phoneno = Commonsettings.UserMobileno;

            OnPropertyChanged(nameof(Countrycode));
            TCcheckcmd  = new Command(contactallowpermission);
            ContactSubmit = new Command(async () => await ContactPropertySubmit(adid,contactname,ContactEmail,Phoneno,ContactDetail));
        }
        public void contactallowpermission()
        {
            var currentpg = GetCurrentPage();
            if(allowpermissionID == 0)
            {
                var checkbximg = currentpg.FindByName<Image>("checkbximg");
                checkbximg.Source = "CheckBoxChecked.png";
                allowpermissionID = 1;
            }
            else
            {
                var checkbximg = currentpg.FindByName<Image>("checkbximg");
                checkbximg.Source = "CheckBoxUnChecked.png";
                allowpermissionID = 0;
            }
        }
        Response res = new Response();
        public async Task ContactPropertySubmit(string adid,string name, string email, string phoneno,string contactdetail)
        {
            var DetailData = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
            string userpid=Commonsettings.UserPid,postedvia="";
            Models.DetailList response = await DetailData.GetDetailListData(adid,userpid);
            dialogs.ShowLoading("");
            var connectivity = CrossConnectivity.Current.IsConnected;
            if(connectivity==true)
            {
                try
                {
                    string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                    if (Validation())
                    {
                        //if (!string.IsNullOrEmpty(Commonsettings.UserMobileOS))
                        //{
                        //    if (Commonsettings.UserMobileOS == "iphone")
                        //    {
                        //        postedvia = "194";
                        //    }
                        //    else
                        //    {
                        //        postedvia = "197";
                        //    }
                        //}
                        res.ExpRent = txtrent;
                        res.adid = adid;
                        res.ContactName = contactname;
                        res.ContactEmail = ContactEmail;
                        res.Title = response.RowData.Last().Title;
                        Countrycode = Countrycode.Replace("+", "");
                        res.CountryCode = Countrycode;
                        res.ContactPhone =Phoneno;
                        res.Shortdesc = ContactDetail;
                        res.ipaddress = ipaddress;
                        res.detailpostid = "2";
                        res.ismobileverified = "1";
                        res.roomtype = VMRoommates.roomtype;
                        if(string.IsNullOrEmpty(Post.ViewModels.VMLCF.SelectedCityName))
                        {
                            res.Locationname = Commonsettings.Usercity;
                        }
                        else
                        {
                            res.Locationname = Post.ViewModels.VMLCF.SelectedCityName;
                        }
                        res.deviceid = Commonsettings.UserDeviceId;
                        res.devicename = Commonsettings.UserMobileOS;
                        res.ismobileverified = "1";
                        res.userpid = Commonsettings.UserPid;
                        res.postedvia = Commonsettings.UserMobileOS;
                        res.allowpermission = allowpermissionID.ToString();
                        res.cityurl = cityurl;

                        //sendleadtobusinessparams
                        res.RMcategory = Commonsettings.CRMCategory;
                        
                        
                        if (Commonsettings.UserMobileOS == "iphone")
                        {
                            res.devicetypeid = "1";
                        }
                        else
                            res.devicetypeid = "2";
                        var OTPLCFSubmit = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                        var data = await OTPLCFSubmit.Responsesubmit(res,res.ismobileverified);
                        var curpage = GetCurrentPage();
                        if (data != null)
                        {
                            if(data.Result== "exist")
                            {
                                dialogs.Alert("You have already responded to this Ad");
                            }
                           else
                            {
                                if (data.type == "0")
                                {
                                    res.isResponseotp = 1;
                                    await curpage.Navigation.PushAsync(new lcfOTP(res, data.pinno));
                                }
                                else
                                {
                                    await curpage.Navigation.PushAsync(new Thankyou_RM());
                                }
                            }
                        }
                    }
                   
                }
                catch (Exception e)
                {
                    dialogs.HideLoading();
                    dialogs.Toast("Kindly check your internet connection");
                }
                dialogs.HideLoading();
            }
            else
            {
                dialogs.Toast("Kindly check your internet connection");
            }
               
        }
        //public VM_Detail()
        //{
        //    CloseImgelst = new Command(Taponimglst);
        //}
        //public async void Taponimglst()
        //{
        //    var currentpage = GetCurrentPage();
        //    await currentpage.Navigation.PopModalAsync();
        //}
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
