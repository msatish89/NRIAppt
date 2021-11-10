using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using NRIApp.Rentals.Features.List.Models;
using Refit;
using NRIApp.Helpers;
using Acr.UserDialogs;
using NRIApp.Rentals.Features.List.Interface;
using NRIApp.Rentals.Features.List.Views;
using Plugin.MapsPlugin;
using Plugin.Share;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.Rentals.Features.Post.Views;
using Plugin.Connectivity;

namespace NRIApp.Rentals.Features.List.ViewModels
{
    public class VMRT_Detail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;

        public Command RTTapOnThumbimgurl { get; set; }
        public Command TapOnShare { get; set; }
        public Command TapOnFavourite { get; set; }
        public Command TapOnFlag { get; set; }
        public Command TapOnMap { get; set; }
        public Command Taponstreetview { get; set; }
        public Command Tapontransportation { get; set; }
        public Command Taponhospital { get; set; }
        public Command TapOnReportEntry { get; set; }
        public Command Reportsubmitcmd { get; set; }
        public ICommand selectreport { protected set; get; }
        public Command Contactcmd { get; set; }
        public Command CloseFlagpage { get; set;}
        public Command ContactSubmit { get; set; }

        public Command TCcheckcmd { get; set; }

        public Command CloseImgelst { get; set; }
        public List<RT_ReportflagData> ReportAd { get; set; }
        public List<RTSaveAdList> AdSaveList { get; set; }
        public List<RTDeleteAdList> AdDeleteList { get; set; }
        private List<RTDetailCategoryData> _amenitiesList;
        public List<RTDetailCategoryData> AmenitiesList
        {
            get { return _amenitiesList; }
            set { _amenitiesList = value; OnPropertyChanged(); }
        }
        private List<RTamenity> _listofamenity;
        public List<RTamenity> Listofamenity
        {
            get { return _listofamenity; }
            set { _listofamenity = value; OnPropertyChanged(); }
        }
        private List<RTutility> _listofutilities;
        public List<RTutility> Listofutilities
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
        public List<RTThumbimgurl> PhotourlList { get; set; }
        string userpid = Commonsettings.UserPid;
        string photoadid = string.Empty;
        string Categoryurl = string.Empty;
        string Cityurl = string.Empty;
        string amenity = string.Empty;
        string PassContentid = string.Empty;
        double citylat;
        double citylong;
        string streetmapname = string.Empty;
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Reportid = string.Empty;

        private bool _isvisible = false;
        public bool Isvisible
        {
            get { return _isvisible; }
            set { _isvisible = value; OnPropertyChanged(); }
        }
        private bool _openhouseschemavisible = false;
        public bool Openhouseschemavisible
        {
            get { return _openhouseschemavisible; }
            set { _openhouseschemavisible = value; OnPropertyChanged(); }
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
        private string _thumbimgurl;
        public string Thumbimgurl
        {
            get { return _thumbimgurl; }
            set { _thumbimgurl = value; OnPropertyChanged(); }
        }
        private string _streetname;
        public string Streetname
        {
            get { return _streetname; }
            set { _streetname = value; OnPropertyChanged(); }
        }
        private string _smokepolicy;
        public string Smokepolicy
        {
            get { return _smokepolicy; }
            set { _smokepolicy = value; OnPropertyChanged(); }
        }
        private string _openhouseschema;
        public string Openhouseschema
        {
            get { return _openhouseschema; }
            set { _openhouseschema = value; OnPropertyChanged(); }
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
        private string _attachedbaths;
        public string Attachedbaths
        {
            get { return _attachedbaths; }
            set { _attachedbaths = value; OnPropertyChanged(); }
        }
        private string _attachedimg;
        public string Attachedimg
        {
            get { return _attachedimg; }
            set { _attachedimg = value; OnPropertyChanged(nameof(Attachedimg)); }
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
        private bool _accomodtesvisible = false;
        public bool accomodtesvisible
        {
            get { return _accomodtesvisible; }
            set { _accomodtesvisible = value; OnPropertyChanged(nameof(accomodtesvisible)); }
        }
        private bool _noofvehiclesvisible = false;
        public bool noofvehiclesvisible
        {
            get { return _noofvehiclesvisible; }
            set { _noofvehiclesvisible = value; OnPropertyChanged(nameof(noofvehiclesvisible)); }
        }
        
        private string _stayperiodfulltext;
        public string Stayperiodfulltext
        {
            get { return _stayperiodfulltext; }
            set { _stayperiodfulltext = value; OnPropertyChanged(); }
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
            set { _pricemode = value; OnPropertyChanged(); }
        }
        private string _amenities;
        public string Amenities
        {
            get { return _amenities; }
            set { _amenities = value; OnPropertyChanged(); }
        }
        private string _deposit;
        public string Deposit
        {
            get { return _deposit; }
            set { _deposit = value; OnPropertyChanged(); }
        }
        private string _utilities;
        public string Utilities
        {
            get { return _utilities; }
            set { _utilities = value; OnPropertyChanged(); }
        }
        private string _petpolicy;
        public string Petpolicy
        {
            get { return _petpolicy; }
            set { _petpolicy = value; OnPropertyChanged(); }
        }
        private int _totalrecs;
        public int Totalrecs
        {
            get { return _totalrecs; }
            set { _totalrecs = value; OnPropertyChanged(); }
        }

        private string _isfurnished;
        public string Isfurnished
        {
            get { return _isfurnished; }
            set { _isfurnished = value; OnPropertyChanged(); }
        }
        private string _isveg;
        public string IsVeg
        {
            get { return _isveg; }
            set { _isveg = value; OnPropertyChanged(); }
        }
        private bool _isvisibleamenity = false;
        public bool Isvisibleamenity
        {
            get { return _isvisibleamenity; }
            set { _isvisibleamenity = value; OnPropertyChanged(); }
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
        private bool _isvisibleparkingtype = false;
        public bool Isvisibleparkingtype
        {
            get { return _isvisibleparkingtype; }
            set { _isvisibleparkingtype = value; OnPropertyChanged(nameof(Isvisibleparkingtype)); }
        }
        private bool _Isvisiblenoofrooms = false;
        public bool Isvisiblenoofrooms
        {
            get { return _Isvisiblenoofrooms; }
            set { _Isvisiblenoofrooms = value; OnPropertyChanged(nameof(Isvisiblenoofrooms)); }
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
        private string _adsaveimg = "Favorite.png";
        public string AdSaveimg
        {
            get { return _adsaveimg; }
            set { _adsaveimg = value; OnPropertyChanged(); }
        }
        private string _reportlist;
        public string Reportlist
        {
            get { return _reportlist; }
            set { _reportlist = value; OnPropertyChanged(); }
        }
        private bool _isvisiblereportlist = false;
        public bool IsVisibleReportList
        {
            get { return _isvisiblereportlist; }
            set { _isvisiblereportlist = value; OnPropertyChanged(); }
        }
        private string _contactEmail;
        public string ContactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; OnPropertyChanged(); }
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
            set { _contactname = value; OnPropertyChanged(); }
        }
        string Namepattern = "^[0-9A-Za-z ]+$";

        private string _selectcountry = "+1";
        public string Selectcountry
        {
            get { return _selectcountry; }
            set { _selectcountry = value; OnPropertyChanged(); }
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
            set { _movedate = value; OnPropertyChanged(); }
        }
        private string _txtrent;
        public string txtrent
        {
            get { return _txtrent; }
            set { _txtrent = value; OnPropertyChanged(); }
        }
        private string _contactdetail = "";
        public string ContactDetail
        {
            get { return _contactdetail; }
            set { _contactdetail = value; OnPropertyChanged(); }
        }
        private string _flagreason = "";
        public string FlagReason
        {
            get { return _flagreason; }
            set { _flagreason = value; OnPropertyChanged(); }
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
            set { _reportemail = value;OnPropertyChanged(); }
        }
        private string _adtypetext;
        public string Adtypetext
        {
            get { return _adtypetext; }
            set { _adtypetext = value;OnPropertyChanged(); }
        }
        private string _sqfeet="";
        public string sqfeet
        {
            get { return _sqfeet; }
            set { _sqfeet = value; OnPropertyChanged(nameof(sqfeet)); }
        }
        
        private string _parkingtype="";
        public string parkingtype
        {
            get { return _parkingtype; }
            set { _parkingtype = value; OnPropertyChanged(nameof(parkingtype)); }
        }
        
        private bool _negotiablevisible = false;
        public bool negotiablevisible
        {
            get { return _negotiablevisible; }
            set { _negotiablevisible = value; OnPropertyChanged(nameof(negotiablevisible)); }
        }
        private bool _addressvisible = false;
        public bool addressvisible
        {
            get { return _addressvisible; }
            set { _addressvisible = value; OnPropertyChanged(nameof(addressvisible)); }
        }
        private bool _availablefrmvisible=false;
        public bool Availablefrmvisible
        {
            get { return _availablefrmvisible; }
            set { _availablefrmvisible = value; OnPropertyChanged(nameof(Availablefrmvisible)); }
        }
        private string _noofrooms = "";
        public string noofrooms
        {
            get { return _noofrooms; }
            set { _noofrooms = value; OnPropertyChanged(nameof(noofrooms)); }
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
        public VMRT_Detail(string Adid,string Scityurl)
        {
            try
            {
                cityurl = Scityurl;
                GetRentalDetailData(Adid);
                adidDtl = PassContentid = Adid;
                RTTapOnThumbimgurl = new Command(TapOnThumimgurl);
                TapOnShare = new Command(sharedetail);
                TapOnFavourite = new Command(TapOnAdtoSave);
                TapOnFlag = new Command(reportflag);
                TapOnMap = new Command(TapOnMapView);
                Taponstreetview = new Command(TapOnStreetView);
                Tapontransportation = new Command(TapOnTranportation);
                Taponhospital = new Command(TapOnHospital);
                Contactcmd = new Command(TapOnContact);
            }
            catch (Exception ec)
            {
                
            }
        }
       public async void TapOnContact()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new Contact_RT(PassContentid));
        }
        
        public async void GetRentalDetailData(string Adid)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
            {
                try
                {
                    dialogs.ShowLoading("");
                    if (userpid == null)
                        userpid = "";
                    else
                        userpid = Commonsettings.UserPid;
                    var DetailData = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                    RTDetailCategory response = await DetailData.GetRTDetailListData(Adid, userpid);
                    Title = response.RowData[0].Title;
                   
                    Totalrecs = response.RowData[0].Totalrecs;
                    Gender = response.RowData[0].Gendertext;
                    Cityurl = response.RowData[0].City;
                    citylat = response.RowData[0].Citylat;
                    citylong = response.RowData[0].Citylong;
                    streetmapname = response.RowData[0].Streetname;
                    Shortdesc = response.RowData[0].Shortdesc.Replace("<br/>", "\n");
                    photoadid = response.RowData[0].Adid;
                    PassContentid = Adid;
                    Primarycatval = response.RowData[0].Primarycategoryvalue;
                    if(string.IsNullOrEmpty(Primarycatval))
                    {
                        Primarycatval = response.RowData[0].roomtypetext;
                    }
                    newcityurl = response.RowData.Last().newcityurl;
                    newtitleurl = response.RowData.Last().titleurl;
                    Openhousestart = response.RowData.Last().openhousestarttime;
                    Openhouseend = response.RowData.Last().openhouseendtime;
                    Adtypetext = "Property "+ response.RowData.Last().adtypetext;
                    sqfeet = "Sqfeet : " +response.RowData.Last().areamin.ToString();
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
                    if (response.RowData.Last().Ismobileverified == 1)
                    {
                        Ismobileverified = true;
                    }
                    else { Ismobileverified = false; }
                    if (!string.IsNullOrEmpty(response.RowData[0].Smokepolicy))
                    {
                        Isvisiblesmokepolicy = true;
                        if (response.RowData[0].Smokepolicy == "0")
                        {
                            Smokepolicy = "No Smoking";
                        }
                        else if (response.RowData[0].Smokepolicy == "1")
                        {
                            Smokepolicy = "Smoking is Ok";
                        }
                        else
                        {
                            Smokepolicy = "Smoking outside only";
                        }
                    }
                    if (!string.IsNullOrEmpty(response.RowData[0].Petpolicy))
                    {
                        Isvisiblepetpolicy = true;
                        if (response.RowData[0].Petpolicy == "0")
                        {
                            Petpolicy = "No Pets";
                        }
                        else if (response.RowData[0].Petpolicy == "1")
                        {
                            Petpolicy = "Only Dogs";
                        }
                        else if (response.RowData[0].Petpolicy == "2")
                        {
                            Petpolicy = "Only Cats";
                        }
                        else
                        {
                            Petpolicy = "Any Pet is Ok";
                        }
                    }
                    if (!string.IsNullOrEmpty(response.RowData[0].Utilities))
                    {
                        Isvisibleutilities = true;
                        var lstofutilities = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                        Models.RTListofutility utilitylst = await lstofutilities.GetutilityList(PassContentid);
                        Listofutilities = utilitylst.ROW_DATA;
                    }

                    if (!string.IsNullOrEmpty(response.RowData[0].Amenities))
                    {
                        Isvisibleamenity = true;
                        var lstofamenities = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                        Models.RTListofamenity amenitylst = await lstofamenities.GetamenityList(PassContentid);
                        Listofamenity = amenitylst.ROW_DATA;
                    }
                    if (!string.IsNullOrEmpty(response.RowData[0].Isfurnished))
                    {
                        Isvisiblefurnished = true;
                        if (response.RowData[0].Isfurnished == "0")
                        {
                            Isfurnished = "Unfurnished";
                        }
                        else if (response.RowData[0].Isfurnished == "1")
                        {
                            Isfurnished = "Furnished with Bed";
                        }
                        else if (response.RowData[0].Isfurnished == "2")
                        {
                            Isfurnished = "Semi Furnished";
                        }
                        else if (response.RowData[0].Isfurnished == "3")
                        {
                            Isfurnished = "Fully Furnished";
                        }
                        else
                        {
                            Isfurnished = "Not mentioned";
                        }
                    }
                    if (!string.IsNullOrEmpty(response.RowData[0].Isveg))
                    {
                        Isvisiblevegpreference = true;
                        if (response.RowData[0].Isveg == "1")
                        {
                            IsVeg = "Yes, Vegetarian mandatory";
                        }
                        if (response.RowData[0].Isveg == "2")
                        {
                            IsVeg = "No, Non-veg is ok";
                        }
                        if (response.RowData[0].Isveg == "3")
                        {
                            IsVeg = "Both";
                        }
                    }
                    
                    if (response.RowData[0].hiderent==1)
                    {
                        Isvisible = true;
                        Pricefrom = "Contact for price";
                        Pricemode = "";
                    }
                    else
                    {
                        if (response.RowData[0].adtype == "0")
                        {
                            if (!string.IsNullOrEmpty(response.RowData[0].price2.ToString()))
                            {
                                Isvisible = true;
                                if (!string.IsNullOrEmpty(response.RowData[0].Pricemode))
                                {
                                    Pricemode = "/ " + response.RowData.Last().Pricemode;
                                    Pricefrom = "$" + response.RowData[0].price2;
                                }
                                else
                                {
                                    Pricefrom = "$" + response.RowData[0].price2;
                                    Pricemode = "";
                                }
                                
                            }
                        }
                        else if (response.RowData[0].adtype == "1")
                        {
                            if (!string.IsNullOrEmpty(response.RowData[0].Price1.ToString()))
                            {
                                Isvisible = true;
                                if (!string.IsNullOrEmpty(response.RowData[0].Pricemode))
                                {
                                    Pricemode = "/ " + response.RowData.Last().Pricemode;
                                    Pricefrom = "$" + response.RowData[0].Price1;
                                }
                                else
                                {
                                    Pricefrom = "$" + response.RowData[0].Price1;
                                    Pricemode = "";
                                }
                            }
                        }
                    }
                   
                    if (response.RowData[0].Deposit != 0 || !string.IsNullOrEmpty(response.RowData[0].Deposit.ToString()))
                    {
                        string depositval = "$" + Math.Round((response.RowData[0].Deposit), 2);
                        if (depositval != "$0")
                        {
                            Isvisibledeposit = true;
                            Deposit = "Deposit: $" + Math.Round((response.RowData[0].Deposit), 2);
                        }

                    }
                    if (string.IsNullOrEmpty(response.RowData.Last().Thumbimgurl) || response.RowData.Last().Thumbimgurl == "0")
                    {
                        Thumbimgurl = "RoommateNoImage.png";
                    }
                    else
                    {
                        Thumbimgurl = response.RowData.Last().Thumbimgurl;
                    }
                    if (!string.IsNullOrEmpty(response.RowData[0].Availablefrm))
                    {
                        Availablefrmvisible = true;
                        if(response.RowData.Last().adtype=="1")
                        {
                            Availablefrm = "Available from : " + response.RowData[0].Availablefrm;
                        }
                        else
                        {
                            Availablefrm = "Move in from : " + response.RowData[0].Availablefrm;
                        }
                    }
                    if (!string.IsNullOrEmpty(Openhousestart) && !string.IsNullOrEmpty(Openhouseend) && !string.IsNullOrEmpty(response.RowData.Last().openhouseschedule.ToString()))
                    {
                        Openhouseschemavisible = true;
                        //Openhouseschema = "Open House : " + response.RowData.Last().openhouseschedule.ToString("dd MMM yyyy") + " " + "," + response.RowData[0].openhousestarttime + " " + "to" + response.RowData[0].openhouseendtime;
                        Openhouseschema = "Open House : " + response.RowData.Last().openhouseschedule +" (" + response.RowData[0].openhousestarttime + " " + "to " + response.RowData[0].openhouseendtime+")";
                    }
                    else if (string.IsNullOrEmpty(Openhousestart) || string.IsNullOrEmpty(Openhouseend))
                    {
                        if (!string.IsNullOrEmpty(response.RowData.Last().openhouseschedule.ToString()))
                        {
                            Openhouseschemavisible = true;
                            Openhouseschema = "Open House : " + response.RowData.Last().openhouseschedule;
                        }
                        else
                        {
                            Openhouseschemavisible = false;
                        }
                        //Openhouseschema = "Open House: " + response.RowData.Last().Openhouseschema.ToString("dd MMMM yyyy hh: mm: ss tt");
                    }
                    if(response.RowData[0].roomtype ==13)
                    {
                        if (response.RowData[0].Stayperiod == 1)
                        {
                            Stayperiodfulltext = "Parking Frequency : " + "One time";
                        }
                        if (response.RowData[0].Stayperiod == 2)
                        {
                            Stayperiodfulltext = "Parking Frequency : " + "Daily";
                        }
                        if (response.RowData[0].Stayperiod == 3 || response.RowData[0].Stayperiod == 0)
                        {
                            Stayperiodfulltext = "Parking Frequency : " + "Weekly";
                        }
                    }
                    else
                    {
                        if (response.RowData[0].Stayperiod == 1)
                        {
                            Stayperiodfulltext = "Stay : " + "Long term";
                        }
                        if (response.RowData[0].Stayperiod == 2)
                        {
                            Stayperiodfulltext = "Stay : " + "Short term";
                        }
                        if (response.RowData[0].Stayperiod == 3 || response.RowData[0].Stayperiod == 0)
                        {
                            Stayperiodfulltext = "Stay : " + "Long/Short Term";
                        }
                    }

                    if (response.RowData[0].roomtype == 13 || response.RowData[0].roomtype == 5 || response.RowData[0].roomtype == 4 || response.RowData[0].roomtype == 7)
                    {
                        accomodtesvisible = false;
                       // Accomodate = "Accomodates : " + response.RowData[0].Accomodate;
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(response.RowData[0].Accomodate.ToString()))
                        {
                            accomodtesvisible = true;
                            Accomodate = "Accomodates : " + response.RowData[0].Accomodate;
                        }
                    }
                    //noofvehicles
                    if (response.RowData[0].roomtypetext== "Parking Garage")
                    {
                        noofvehiclesvisible = true;
                        if (!string.IsNullOrEmpty(response.RowData[0].secondarycategoryvalue))
                        {
                            Attachedimg = "vehicle.png";
                            Attachedbaths =  response.RowData[0].secondarycategoryvalue;
                        }
                        else
                        {
                            Attachedimg = "vehicle.png";
                            Attachedbaths = "No of vehicles : " + "-";
                        }
                    }
                    else if(response.RowData[0].roomtype == 5 || response.RowData[0].roomtype == 4 || response.RowData[0].roomtype == 7)
                    {
                        noofvehiclesvisible = false;
                        
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(response.RowData[0].noofbaths))
                        {
                            noofvehiclesvisible = true;
                            if (response.RowData[0].noofbaths=="5")
                            {
                                Attachedimg = "Bathtub.png";
                                Attachedbaths = "Bathrooms : 4+ Baths";
                            }
                            else
                            {
                                Attachedimg = "Bathtub.png";
                                Attachedbaths = "Bathrooms : " + response.RowData[0].noofbaths + " Baths";
                            }
                            
                        }
                        else
                        {
                            noofvehiclesvisible = false;
                        }
                    }
                    if(response.RowData[0].hideaddress==0)
                    {
                        addressvisible = true;
                        Streetname = response.RowData[0].Streetname;
                    }
                    if(response.RowData[0].negotiable == "1.0" || response.RowData[0].negotiable == "1")
                    {
                        negotiablevisible = true;
                    }

                    if(response.RowData[0].roomtype==13  )
                    {
                        Isvisibleamenity = false;
                        Isvisiblefurnished = false;
                        Isvisiblepetpolicy = false;
                        Isvisiblesmokepolicy = false;
                        Isvisibleutilities = false;
                        Isvisibleparkingtype = true;
                        Isvisiblenoofrooms = false;
                        Isvisiblevegpreference = false;
                        parkingtype = response.RowData[0].accomodationtype;
                    }
                    else
                    {
                        Isvisiblenoofrooms = true;
                        if(response.RowData[0].noofrooms=="4")
                        {
                            noofrooms = "Bedrooms : " + response.RowData[0].noofrooms+"+";
                        }
                        else
                        {
                            noofrooms = "Bedrooms : " + response.RowData[0].noofrooms;
                        }
                        
                    }
                    if(response.RowData[0].roomtype == 5 || response.RowData[0].roomtype == 4 || response.RowData[0].roomtype == 7)
                    {
                        Isvisiblefurnished = false;
                        Isvisiblepetpolicy = false;
                        Isvisibleutilities = false;
                        Isvisiblesmokepolicy = false;
                        Isvisibleparkingtype = false;
                        Isvisiblenoofrooms = false;
                    }
                    dialogs.HideLoading();
                }
                catch (Exception ex)
                {
                    var connect=CrossConnectivity.Current.IsConnected;
                    if(connect==false)
                        dialogs.Toast("Kindly check your internet connection");
                    else
                        dialogs.HideLoading();

                }
            }
            else
            {
                dialogs.Toast("Kindly check your internet connection");
            }
        }

        public  void sharedetail()
        {
            //string text = "Hi, I found this Rentals listing in Sulekha.com and would like to share it with you , have a look at it and provide me your feedback about this property.";
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
               // Text = text,
                Url = "http://indianroommates.sulekha.com/" + newtitleurl + "_"+ "rentals"+"_" + newcityurl + "_" + PassContentid
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
                if(adresult=="0")
                {
                    AdSaveimg = "FavoriteActive.png";
                    var savead = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                    Models.RTSaveAD response = await savead.SaveAdData(PassContentid, email, userpid);
                    AdSaveList = response.ROW_DATA;
                    adresult = "1";
                }
                else
                {
                    AdSaveimg = "Favorite.png";
                    var deletead = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                    Models.RTDeleteAD response = await deletead.DeleteAdData(PassContentid, userpid);
                    AdDeleteList = response.ROW_DATA;
                    adresult = "0";
                }
            }
        }
        public  void reportflag()
        {
            string adid = PassContentid;
            var currentpage = GetCurrentPage();
            Device.BeginInvokeOnMainThread(async () => await currentpage.Navigation.PushModalAsync(new Report_RT(adid)));
        }
        public async void TapOnMapView()
        {
            await CrossMapsPlugin.Current.PinTo(streetmapname, citylat,citylong, 8);
        }
        public void TapOnStreetView()
        {
            if (Device.RuntimePlatform==Device.Android)
            {
                Device.OpenUri(new Uri("google.streetview:cbll=" + citylat + "," + citylong + "&cbp=1,90,,0,1.0&mz=20"));
            }
            else if(Device.RuntimePlatform==Device.iOS)
            {
                Device.OpenUri(new Uri("http://maps.google.com/maps?q=&layer=c&cbll=" + citylat + "," + citylong + "&cbp=1,90,0,0,1.0&mz=20"));
            }
        }
        public void TapOnHospital()
        {
            try
            {
                string business = "Hospital";
                string city = Streetname;
                if (Device.RuntimePlatform == Device.Android)
                {
                    Device.OpenUri(new Uri("geo:0,0?q=" + business+" " + "near" + city));
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    string url = "http://maps.google.com/maps?&q=" + business + " near " + city + "&sll=" + citylat + "," + citylong + "&sspn=0.2,0.1&nav=1";
                    url = url.Replace(" ", "%20");
                    Device.OpenUri(new Uri(url));
                }
            }
            catch(Exception e)
            {

            }
        }
        public void TapOnTranportation()
        {
           try
            {
                string business = "Transportation";
                string city = Streetname;
                if (Device.RuntimePlatform == Device.Android)
                {
                    Device.OpenUri(new Uri("geo:0,0?q=" + business + "near" + city));
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    string url = "http://maps.google.com/maps?&q=" + business + " near " + city + "&sll=" + citylat + "," + citylong + "&sspn=0.2,0.1&nav=1";
                    url = url.Replace(" ", "%20");
                    Device.OpenUri(new Uri(url));
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async void TapOnThumimgurl()
        {
            try
            {
                if (Totalrecs > 0)
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new ImageList_RT(PassContentid));
                }
            }
            catch (Exception ex)
            {

            }

        }
        public VMRT_Detail(string adid,int type,int type1)
        {
            try
            {
                PassContentid = adid;
                ContactEmail = Commonsettings.UserEmail;
                selectreport = new Command<ReportFlag>(Taponreportflaglist);
                TapOnReportEntry = new Command(taponreportentry);
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    Phoneno = Commonsettings.UserMobileno;
                if (string.IsNullOrEmpty(ReportEmail))
                {
                    ReportEmail = Commonsettings.UserEmail;
                }
                Reportsubmitcmd = new Command(Taponreportsubmitcmd);
                CloseFlagpage = new Command(TapOnClose);
                List<ReportFlag> listofreports = new List<ReportFlag>();
                listofreports.Add(new ReportFlag { reportlist = "Spam/Fraud", reportid = 1 });
                listofreports.Add(new ReportFlag { reportlist = "Withdrawn Property lease", reportid = 4 });
                listofreports.Add(new ReportFlag { reportlist = "Unrevised price", reportid = 2 });
                listofreports.Add(new ReportFlag { reportlist = "InValid Contact details", reportid = 6 });
                listofreports.Add(new ReportFlag { reportlist = "Unresponsive link", reportid = 7 });
                listofreports.Add(new ReportFlag { reportlist = "Incorrect photo", reportid = 8 });
                listofreports.Add(new ReportFlag { reportlist = "Invalid property details", reportid = 9 });
                listofreports.Add(new ReportFlag { reportlist = "MisCategorized", reportid = 3 });
                Listofreports = listofreports;
                Reportlist = listofreports.First().reportlist;
            }
            catch (Exception e)
            {

            }
        }
        public void TapOnClose()
        {
            var currentpage = GetCurrentPage();
            Device.BeginInvokeOnMainThread(async () => await currentpage.Navigation.PopModalAsync());
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
        //public VMRT_Detail()
        //{
        //    CloseImgelst = new Command(Taponimglst);
        //}
        //public async void Taponimglst()
        //{
        //    var currentpage = GetCurrentPage();
        //    await currentpage.Navigation.PopModalAsync();
        //}
        public async void Taponreportsubmitcmd()
        {
            dialogs.ShowLoading();
            if (validate())
            {
                try
                {
                    string Siteid = "32";
                    string reportid = Reportid;
                    if (string.IsNullOrEmpty(reportid))
                    {
                        reportid = "1";
                    }
                    string adid = PassContentid;
                    string adurl = "http://indianroommates.sulekha.com/"+ "_" + newcityurl + "_" + adid;
                    string sitename = "indianroommates.sulekha.com";
                    
                    if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                        ContactEmail = Commonsettings.UserEmail;
                    if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                        contactname = Commonsettings.UserName;
                    if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                        Phoneno = Commonsettings.UserMobileno;

                    if (Title == null)
                        Title = "";
                    if (string.IsNullOrEmpty(Phoneno))
                        Phoneno = "";
                    if (string.IsNullOrEmpty(userpid))
                        userpid = "";

                    var report = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                    Models.RTReportflag reportsubmit = await report.GetReport(adid,userpid,Title,sitename,Phoneno,reportid,adurl,Siteid,FlagReason,ReportEmail);
                    ReportAd = reportsubmit.ROW_DATA;
                    dialogs.HideLoading();
                    var currentpage = GetCurrentPage();
                    // Device.BeginInvokeOnMainThread(async () => await currentpage.Navigation.PopModalAsync());
                    await currentpage.Navigation.PopModalAsync();
                }
                catch (Exception e)
                {
                    dialogs.HideLoading();
                }
            }
        }
        public bool validate()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(Reportlist))
            {
                Entry myEntry = currentpage.FindByName<Entry>("Reportlist");
                myEntry.Focus();
                dialogs.Toast("select anyone");
                return false;
            }
            if (string.IsNullOrEmpty(ReportEmail))
            {
                Entry myEntry = currentpage.FindByName<Entry>("ReportEmail");
                dialogs.Toast("Please enter your email id");
                return false;
            }
            if (!Regex.IsMatch(ReportEmail, Emailpattern))
            {
                Entry myEntry = currentpage.FindByName<Entry>("ReportEmail");
                //myEntry.Focus();
                //myEntry.Text = "";
                //myEntry.Placeholder = "Please enter your email id";
                //myEntry.PlaceholderColor = Color.Red;
                dialogs.Toast("Enter Proper Email");
                return false;
            }
            if (string.IsNullOrEmpty(FlagReason))
            {
                Editor myEntry = currentpage.FindByName<Editor>("reasondesc");
                dialogs.Toast("Details field is required");
                return false;
            }
            if (string.IsNullOrEmpty(Phoneno))
            {
                NRIApp.Controls.CustomEntry myEntry = currentpage.FindByName<NRIApp.Controls.CustomEntry>("txtcontactphone");
                dialogs.Toast("Please enter your mobile number");
                return false;
            }
            if (Phoneno.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                dialogs.Toast("Minimum 10 Digits required");
                return false;
            }
            if (!CheckValidPhone(Phoneno))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                dialogs.Toast("Please enter valid mobile number");
                return false;
            }
            return result;
        }
        public bool Validation()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(contactname))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactname");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your name";
                myEntry.PlaceholderColor = Color.Red;
                return false;
            }
            if (!Regex.IsMatch(contactname, Namepattern))
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
            if (string.IsNullOrEmpty(ContactDetail))
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

        public VMRT_Detail(string adid, int type, int type1, int type2)
        {
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                ContactEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                contactname = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Phoneno = Commonsettings.UserMobileno;
            OnPropertyChanged(nameof(Countrycode));
            TCcheckcmd = new Command(contactallowpermission);
            ContactSubmit = new Command(async () => await ContactPropertySubmit(adid, contactname, ContactEmail, Phoneno, ContactDetail));
        }

        public void contactallowpermission()
        {
            var currentpg = GetCurrentPage();
            if (allowpermissionID == 0)
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


        RTResponse res = new RTResponse();
        public async Task ContactPropertySubmit(string adid, string name, string email, string phoneno, string contactdetail)
        {
            dialogs.ShowLoading("");
            var DetailData = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
            RTDetailCategory response = await DetailData.GetRTDetailListData(adid, Commonsettings.UserPid);
            var connectivity = CrossConnectivity.Current.IsConnected;
            if(connectivity==true)
            {
                try
                {
                    string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                    if (Validation())
                    {
                        res.ExpRent = txtrent;
                        res.adid = adid;
                        res.ContactName = contactname;
                        res.ContactEmail = ContactEmail;
                        res.ContactPhone = Phoneno;
                        Countrycode = Countrycode.Replace("+", "");
                        res.CountryCode = Countrycode;
                        res.mobileverify = "1";
                        res.ipaddress = ipaddress;
                        res.title= response.RowData[0].Title;
                        res.detailpostid = "2";
                        res.Shortdesc = contactdetail;
                        res.deviceid = Commonsettings.UserDeviceId;
                        res.userpid = Commonsettings.UserPid;
                        res.Sroomtype = VMRentals.roomtype;
                        if(string.IsNullOrEmpty(Post.ViewModels.VMRTLCF.SelectedCityName))
                        {
                            res.Locationname = Commonsettings.Usercity;
                        }
                        else
                        {
                            res.Locationname = Post.ViewModels.VMRTLCF.SelectedCityName;
                        }
                        res.deviceid = Commonsettings.UserDeviceId;
                        res.devicename = Commonsettings.UserMobileOS;
                        res.postedvia = Commonsettings.UserMobileOS;
                        res.cityurl = cityurl;



                        res.allowpermission = allowpermissionID.ToString();

                        if (Commonsettings.UserMobileOS == "iphone")
                        {
                            res.devicetypeid = "1";
                        }
                        else
                            res.devicetypeid = "2";

                        var OTPLCFSubmit = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                        var data = await OTPLCFSubmit.Responsesubmit(res, res.mobileverify);
                        if (data != null)
                        {
                            var curpage = GetCurrentPage();
                            if (data.Result == "exist")
                            {
                                dialogs.Alert("You have already responded to this Ad");
                            }
                            else
                            {
                                if (data.type == "0")
                                {
                                    res.isResponseotp = 1;
                                    await curpage.Navigation.PushAsync(new LcfotpRT(res, data.pinno));
                                }
                                else
                                {
                                    //await curpage.Navigation.PushAsync(new postsuccess());
                                    await curpage.Navigation.PushAsync(new Thankyou_RT());
                                }
                            }
                                
                        }

                    }
                }
                catch (Exception e)
                {
                    dialogs.HideLoading();
                }
                dialogs.HideLoading();
            }
            else
            {
                dialogs.HideLoading();
                dialogs.Toast("Kindly check your internet connection!");
            }
          
            
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page GetCurrentPage()
        {
            var CurrentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return CurrentPage;
        }
    }
}
