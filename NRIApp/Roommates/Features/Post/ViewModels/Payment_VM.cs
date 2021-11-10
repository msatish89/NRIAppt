using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NRIApp.Roommates.Features.Post.Models;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using System.Windows.Input;
using Refit;
using NRIApp.Roommates.Features.Post.Interface;
using NRIApp.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;

namespace NRIApp.Roommates.Features.Post.ViewModels
{
    public class Payment_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;

        public static string expirymonthID;
        public static int expiryyearID;

        public Command TaponProceed { get; set; }
        public List<Payment_Params> Expirymonthlistdata { get; set; }
        public List<Payment_Params> Expiryyearlistdata { get; set; }
        public ICommand TaponExpirymonth { get; set; }
        public ICommand TaponExpiryyear { get; set; }
        public ICommand selectexpirymonth { get; set; }
        public ICommand selectexpiryyear { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command Taponcvvimg { get; set; }
        public Command<Location> LocationCmd { get; set; }
        public Command Selectgoogleaddress { get; set; }
        public ICommand openbilling { get; set; }
        public ICommand ApplyCouponCode { get; set; }
        public ICommand makechangesTap { get; set; }
        public ICommand removecouponcmd { get; set; }

        public List<Payment_Params> Expirymonthlist = new List<Payment_Params>()
        {
            new Payment_Params {months="01",monthname="01 (JAN)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="02",monthname="02 (FEB)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="03",monthname="03 (MAR)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="04",monthname="04 (APR)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="05",monthname="05 (MAY)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="06",monthname="06 (JUN)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="07",monthname="07 (JUL)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="08",monthname="08 (AUG)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="09",monthname="09 (SEP)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="10",monthname="10 (OCT)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="11",monthname="11 (NOV)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params {months="12",monthname="12 (DEC)",checkimage="RadioButtonUnChecked.png"},
        };
        public List<Payment_Params> expiryyearslist = new List<Payment_Params>()
        {
          //  new Payment_Params{years=2019,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2020,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2021,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2022,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2023,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2024,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2025,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2026,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2027,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2028,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2029,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2030,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2031,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2032,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2033,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2034,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2035,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2036,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2037,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2038,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2039,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2040,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2041,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2042,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2043,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2044,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2045,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2046,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2047,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2048,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2049,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2050,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2051,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2052,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2053,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2054,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params{years=2055,checkimage="RadioButtonUnChecked.png"},
        };
        private bool _billingvisible = false;
        public bool billingvisible
        {
            get { return _billingvisible; }
            set { _billingvisible = value; OnPropertyChanged(nameof(billingvisible)); }
        }
        private bool _monthvisible;
        public bool monthvisible
        {
            get { return _monthvisible; }
            set { _monthvisible = value; OnPropertyChanged(nameof(monthvisible)); }
        }
        private bool _yearvisible;
        public bool yearvisible
        {
            get { return _yearvisible; }
            set { _yearvisible = value; OnPropertyChanged(nameof(yearvisible)); }
        }
        private string _cardnumber;
        public string cardnumber
        {
            get { return _cardnumber; }
            set { _cardnumber = value;OnPropertyChanged(nameof(cardnumber)); }
        }
        private string _cardname;
        public string cardname
        {
            get { return _cardname; }
            set { _cardname = value; OnPropertyChanged(nameof(cardname)); }
        }
        private string _expirymonthvalue="Month";
        public string expirymonthvalue
        {
            get { return _expirymonthvalue; }
            set { _expirymonthvalue = value; OnPropertyChanged(nameof(expirymonthvalue)); }
        }
        private string _expiryyearvalue="Year";
        public string expiryyearvalue
        {
            get { return _expiryyearvalue; }
            set { _expiryyearvalue = value; OnPropertyChanged(nameof(expiryyearvalue)); }
        }
        private string _CVVnumber;
        public string CVVnumber
        {
            get { return _CVVnumber; }
            set { _CVVnumber = value;OnPropertyChanged(nameof(CVVnumber)); }
        }
        private bool _contentviewvisible;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value;OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _cvvimgvisible;
        public bool cvvimgvisible
        {
            get { return _cvvimgvisible; }
            set { _cvvimgvisible = value;OnPropertyChanged(nameof(cvvimgvisible)); }
        }


        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z ]+$");
        }

        bool CheckValidEmail(string inputEmail)
        {
            return Regex.IsMatch(inputEmail, "[\\w\\.-]+@([\\w\\-]+\\.)+[A-Z]{2,4}$", RegexOptions.IgnoreCase);
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

        public bool paymentvalidation()
        {
            if (string.IsNullOrEmpty(cardnumber))
            {
                dialog.Toast("Credit Card Number is required");
                return false;
            }
            if (string.IsNullOrEmpty(cardname))
            {
                dialog.Toast("Name on Card is required");
                return false;
            }
            if (!CheckAplhaOnly(cardname))
            {
                dialog.Toast("Please enter a valid name");
                return false;
            }
            if (string.IsNullOrEmpty(expirymonthID.ToString()))
            {
                dialog.Toast("Expiry Month is required");
                return false;
            }
            if (string.IsNullOrEmpty(expiryyearID.ToString()) || expiryyearID == 0)
            {
                dialog.Toast("Expiry Year is required");
                return false;
            }
            if (string.IsNullOrEmpty(CVVnumber))
            {
                dialog.Toast("CVV Number is required");
                return false;
            }
            if (string.IsNullOrEmpty(billingname))
            {
                dialog.Toast("Billing name is required");
                return false;
            }
            if (!CheckAplhaOnly(billingname))
            {
                dialog.Toast("Please enter a valid name");
                return false;
            }
            if (string.IsNullOrEmpty(billingemail))
            {
                dialog.Toast("Billing email is required");
                return false;
            }
            if (!CheckValidEmail(billingemail))
            {
                dialog.Toast("Enter valid email id");
                return false;
            }
                if (string.IsNullOrEmpty(billingmobileno))
            {
                dialog.Toast("Billing mobileno is required");
                return false;
            }
            if (billingmobileno.Length < 10)
            {
                dialog.Toast("Enter a 10-digit mobile number");
                return false;
            }
            if (!CheckValidPhone(billingmobileno))
            {
                dialog.Toast("Enter valid mobile number");
                return false;
            }
            if (string.IsNullOrEmpty(zipcode))
            {
                dialog.Toast("City is required");
                return false;
            }
            if (string.IsNullOrEmpty(LocationAddress))
            {
                dialog.Toast("Billing address is required");
                return false;
            }
            return true;
        }
        private string _LocationName = "";
        bool _IsVisibleList = false;
        public string TempLocationName = "";
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
        public static string city = "";
        public static string zipcode = "";
        public static string country = "";
        public static string statecode = "";
        public void OnLocationClick(Location obj)
        {
            try
            {
                LocationName = TempLocationName = obj.citystatecode;
                city = obj.city;
                zipcode = obj.zipcode;
                country = obj.country;
                statecode = obj.statecode;
                IsVisibleList = false;
            }
            catch (Exception e) { }
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
        public bool IsVisibleList
        {
            get { return _IsVisibleList; }
            set
            {
                _IsVisibleList = value;
                OnPropertyChanged();
            }
        }
        public string _LocationAddress = "";
        public string TempLocationAddress = "";
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
        private List<Predictions> _googleadd;
        public List<Predictions> googleadd
        {
            get { return _googleadd; }
            set { _googleadd = value; }
        }
        bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }
        private string _Selectcountry = "+1";
        public string Selectcountry
        {
            get { return _Selectcountry; }
            set
            {
                _Selectcountry = value;
                OnPropertyChanged(Selectcountry);
            }
        }
        public string _billingname = "";
        public string billingname
        {
            get { return _billingname; }
            set
            {
                _billingname = value;
                OnPropertyChanged(billingname);
            }
        }
        private string _billingemail = "";
        public string billingemail
        {
            get { return _billingemail; }
            set
            {
                _billingemail = value;
                OnPropertyChanged(billingemail);
            }
        }
        private string _billingmobileno = "";
        public string billingmobileno
        {
            get { return _billingmobileno; }
            set
            {
                _billingmobileno = value;
                OnPropertyChanged(billingmobileno);
            }
        }
        private bool _couponvisible = false;
        public bool couponvisible
        {
            get { return _couponvisible; }
            set
            {
                _couponvisible = value;
                OnPropertyChanged(nameof(couponvisible));
            }
        }
        private bool _removecouponvisible = false;
        public bool removecouponvisible
        {
            get { return _removecouponvisible; }
            set
            {
                _removecouponvisible = value;
                OnPropertyChanged(nameof(removecouponvisible));
            }
        }
        private bool _appliedcoupondtl = false;
        public bool appliedcoupondtl
        {
            get { return _appliedcoupondtl; }
            set
            {
                _appliedcoupondtl = value;
                OnPropertyChanged(nameof(appliedcoupondtl));
            }
        }
        private string _couponcodetxt;
        public string couponcodetxt
        {
            get { return _couponcodetxt; }
            set
            {
                _couponcodetxt = value;
                OnPropertyChanged(couponcodetxt);
            }
        }
      
        private string _discountamountt;
        public string discountamountt
        {
            get { return _discountamountt; }
            set
            {
                _discountamountt = value;
                OnPropertyChanged(discountamountt);
            }
        }
        //private string _totalamountt;
        //public string totalamountt
        //{
        //    get { return _totalamountt; }
        //    set
        //    {
        //        _totalamountt = value;
        //        OnPropertyChanged(totalamountt);
        //    }
        //}
        //private string _packageamountt;
        //public string packageamountt
        //{
        //    get { return _packageamountt; }
        //    set
        //    {
        //        _packageamountt = value;
        //        OnPropertyChanged(packageamountt);
        //    }
        //}



        public async void GetgoogleAddress()
        {
            try
            {
                string content = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=" + LocationAddress + "&components=country:us&key=" + Commonsettings.GoogleAPIkey;
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
        private static string amnt = "", addid="",usertype="", Sordertype="",Scouponcode="";
        public static string packageamount, discountamount, totalamount;
        Postfirst myneedpost = new Postfirst();
        public Payment_VM(string amount,string ordertype,string adid, Postfirst pst)
        {
            myneedpost = pst;
            Scouponcode = ""; totalamount = "";
            if (pst.usertype=="1")
            {
                couponvisible = true;
            }
            else
            {
                couponvisible = false;
            }
            amnt = amount;
            addid = adid;
            totalamount = amount;
            packageamount = amount;
            //var currentpg = GetCurrentPage();
            //var packgamt = currentpg.FindByName<Label>("packgamt");
            //packgamt.Text = amount;
            //var totalamt = currentpg.FindByName<Label>("totalamt");
            //packgamt.Text = amount;
            //totalamountt = amount;
            //packageamountt = amount;
            usertype = pst.usertype;
            billingname = pst.ContactName;
            billingemail = pst.ContactEmail;
            billingmobileno = pst.ContactPhone;
            if (string.IsNullOrEmpty(billingname))
            {
                billingname = pst.Contributor;
            }
            if (string.IsNullOrEmpty(billingemail))
            {
                billingemail = pst.Email;
            }
            if (string.IsNullOrEmpty(billingmobileno))
            {
                billingmobileno = pst.Phone;
            }
            Sordertype = ordertype;
            Selectcountry = pst.Countrycode;
            TempLocationName = pst.City +", "+pst.StateCode;
            LocationName = pst.City + ", " + pst.StateCode;
            city = pst.City;
            zipcode = pst.Zipcode;
            country = pst.Country;
            statecode = pst.StateCode;
            TempLocationAddress = pst.LocationAddress;
            LocationAddress = pst.LocationAddress;
            if(string.IsNullOrEmpty(pst.LocationAddress))
            {
                TempLocationAddress  = pst.Streetname;
                LocationAddress = pst.Streetname;
            }
            Expirymonthlistdata = Expirymonthlist;
            Expiryyearlistdata = Expiryyearlistdata;

            Taponcvvimg = new Command(showcvvimg);
            PopupContentTap = new Command(showcontentview);
            ContentViewTap = new Command(Closecontentview);

            TaponExpiryyear = new Command(showexpiryyearlist);
            TaponExpirymonth = new Command(showexpirymonthlist);
            selectexpirymonth = new Command<Payment_Params>(selectexpirymonthvalue);
            selectexpiryyear = new Command<Payment_Params>(selectexpiryyearvalue);
            TaponProceed = new Command(paymentfirst);
            LocationCmd = new Command<Location>(OnLocationClick);
            Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
            openbilling = new Command(openbillingdeatils);
            removecouponcmd = new Command(removecoupon);
            ApplyCouponCode = new Command(applycouponcode);
            makechangesTap = new Command(Editoption);
        }
        Roommates.Features.Post.Models.CategoryList catlist = new Roommates.Features.Post.Models.CategoryList();
        //public static int Paymentedit = 0;
        public async void Editoption()
        {
            try
            {
                var currentpage = GetCurrentPage();

                //catlist.pricemode = myneedpost.pricemode;
                //catlist.accomodate = myneedpost.accomodate;
                //catlist.stayperiod = myneedpost.stayperiod;
                //catlist.Rent = myneedpost.Rent;
                //catlist.attachedbaths = myneedpost.attachedbaths;
                //catlist.availablefrm = myneedpost.availablefrm;
                //catlist.service = myneedpost.Service;
                //catlist.adid = myneedpost.Adid;
                ////myneedpost.Adsid;
                //catlist.agentid = myneedpost.Agentid;
                //catlist.Isagent = myneedpost.Isagent;
                //catlist.XmlPhotopath = myneedpost.XmlPhotopath;
                //catlist.Imagepath = myneedpost.Imagepath;
                ////catlist.Imagepath = myneedpost.HtmlPhotopath;
                //catlist.Photoscnt = myneedpost.Photoscnt;
                //catlist.Ispaid = myneedpost.Ispaid;
                //catlist.Category = myneedpost.Category;
                //catlist.Subcategory = myneedpost.Subcategory;
                //if (!string.IsNullOrEmpty(myneedpost.Adtype))
                //{
                //    if (myneedpost.Adtype == "offered")
                //    {
                //        catlist.adtype = "1";
                //    }
                //    else
                //    {
                //        catlist.adtype = "0";
                //    }
                //}
                ////catlist.adtype = myneedpost.Adtype;

                //catlist.Additionalcity = myneedpost.Additionalcity;
                //catlist.Citycount = myneedpost.Citycount;
                ////pst.me myneedpost.Metrototalamount;
                //catlist.Citytotalamount = myneedpost.Citytotalamount;
                //catlist.supercategoryid = myneedpost.Supercategoryid;
                //catlist.primarycategoryid = myneedpost.Primarycategoryid.ToString();
                //catlist.secondarycategoryid = myneedpost.Secondarycategoryid;
                //catlist.supercategoryvalue = myneedpost.Supercategoryvalue;
                //catlist.primarycategoryvalue = myneedpost.Primarycategoryvalue;
                //catlist.secondarycategoryvalue = myneedpost.Secondarycategoryvalue;
                //catlist.Tags = myneedpost.Tags;
                //catlist.businessname = myneedpost.Businessname;
                //catlist.licenseno = myneedpost.licenseno;
                //if (catlist.licenseno == "new id")
                //{
                //    catlist.licenseno = "";
                //}
                //catlist.url = myneedpost.Url;
                //catlist.supercategoryid = Convert.ToInt32(myneedpost.Supertagid);
                //catlist.Bestindustry = myneedpost.Bestindustry;
                //catlist.Hideaddress = myneedpost.Hideaddress;
                //catlist.Linkedinurl = myneedpost.Linkedinurl;
                //catlist.Trimetros = myneedpost.Trimetros;
                //catlist.Nationwide = myneedpost.Nationwide;
                //catlist.Salarymode = myneedpost.Salarymode;
                //catlist.Salarymodeid = myneedpost.Salarymodeid;
                //catlist.Streetname = catlist.Locationname = myneedpost.Streetname;
                //catlist.Zipcode = myneedpost.Zipcode;
                //catlist.Lat = myneedpost.Lat;
                //catlist.Long = myneedpost.Long;
                //catlist.Newcityurl = myneedpost.Newcityurl;
                //catlist.City = myneedpost.City;
                //catlist.State = catlist.StateName = myneedpost.State;
                //catlist.Country = myneedpost.Country;
                //catlist.title = myneedpost.Title;
                //catlist.Titleurl = myneedpost.Titleurl;
                //catlist.description = myneedpost.Description;
                //catlist.Contributor = catlist.ContactName = myneedpost.Contributor;
                //catlist.Status = myneedpost.Status;
                //catlist.Displayphone = myneedpost.Displayphone;
                //catlist.Mailerstatus = myneedpost.Mailerstatus;
                //catlist.Userpid = myneedpost.Userpid;
                //catlist.Ordertype = myneedpost.Ordertype;
                //catlist.Customerid = myneedpost.Customerid;
                //catlist.Campaignid = myneedpost.Campaignid;
                //catlist.Postedby = myneedpost.Postedby;
                //catlist.Enddate = myneedpost.Enddate;
                //catlist.Email = catlist.ContactEmail = myneedpost.Email;
                //catlist.Phone = catlist.ContactPhone = myneedpost.Phone;
                //catlist.Startdate = myneedpost.Startdate;
                //catlist.Numberofdays = myneedpost.Numberofdays;
                //catlist.Amount = myneedpost.Amount;
                //catlist.Folderid = myneedpost.Folderid;
                //catlist.Countrycode = myneedpost.Countrycode;
                //catlist.Ismobileverified = myneedpost.Ismobileverified;
                //catlist.Statecode = myneedpost.Statecode;
                //catlist.Metro = myneedpost.Metro;
                //catlist.Metrourl = myneedpost.Metrourl;
                //catlist.Supercategoryvalueurl = myneedpost.Supercategoryvalueurl;
                //catlist.Primarycategoryvalueurl = myneedpost.Primarycategoryvalueurl;
                //catlist.Secondarycategoryvalueurl = myneedpost.Secondarycategoryvalueurl;
                //catlist.Orderid = myneedpost.Orderid;
                //catlist.Expirydate = myneedpost.Expirydate;
                //catlist.Noofadcnt = myneedpost.Noofadcnt;
                //catlist.Adpostedcnt = myneedpost.Adpostedcnt;
                //catlist.Responsemail = myneedpost.Responsemail;
                //catlist.Gender = myneedpost.Gender;
                //catlist.Disclosedbusiness = myneedpost.Disclosedbusiness;
                //catlist.Couponcode = myneedpost.Couponcode;
                //catlist.Salespersonemail = myneedpost.Salespersonemail;
                //catlist.Companytype = myneedpost.Companytype;
                //catlist.Bannerstatus = myneedpost.Bannerstatus;
                //catlist.Banneramount = myneedpost.Banneramount;
                //catlist.Categoryflag = myneedpost.Categoryflag;
                //catlist.Freeadsflag = myneedpost.Freeadsflag;
                //catlist.Islisting = myneedpost.Islisting;
                //catlist.Addonamount = myneedpost.Addonamount;
                //catlist.Emailblastamount = myneedpost.Emailblastamount;
                //catlist.Bonusdays = myneedpost.Bonusdays;
                //catlist.Bonusamount = myneedpost.Bonusamount;
                //catlist.Pendingpaycouponcode = myneedpost.Pendingpaycouponcode;

                //catlist.Benefits = myneedpost.Benefits;
                //catlist.ismyneed = "1";

                catlist.pricemode = myneedpost.pricemode;
                catlist.accomodate = myneedpost.accomodate;
                catlist.stayperiod = myneedpost.stayperiod;
                catlist.Rent = myneedpost.rent;
                if(string.IsNullOrEmpty(catlist.Rent))
                {
                    catlist.Rent = myneedpost.ExpRent;
                }
                catlist.attachedbaths = myneedpost.attachedbaths;
                if (string.IsNullOrEmpty(catlist.attachedbaths))
                {
                    catlist.attachedbaths = myneedpost.attachedbathtype;
                }
                catlist.availablefrm = myneedpost.availablefrm;
                if (string.IsNullOrEmpty(catlist.availablefrm))
                {
                    catlist.availablefrm = myneedpost.Movedate;
                }
                catlist.service = myneedpost.service;
                catlist.adid = myneedpost.adid;
                //myneedpost.Adsid;
                catlist.agentid = myneedpost.agentid;
                catlist.Isagent = myneedpost.Isagent;
                catlist.XmlPhotopath = myneedpost.XmlPhotopath;
                catlist.Imagepath = myneedpost.Imagepath;
                //catlist.Imagepath = myneedpost.HtmlPhotopath;
                catlist.Photoscnt = myneedpost.Photoscnt;
                catlist.Ispaid = myneedpost.Ispaid;
                catlist.Category = myneedpost.Category;
                if (string.IsNullOrEmpty(catlist.Category))
                {
                    catlist.Category = myneedpost.RMcategory;
                }
                catlist.Subcategory = myneedpost.Subcategory;
                if (!string.IsNullOrEmpty(myneedpost.Adtype))
                {
                    if (myneedpost.Adtype == "offered")
                    {
                        catlist.adtype = "1";
                    }
                    else
                    {
                        catlist.adtype = "0";
                    }
                }
                else
                {
                    catlist.adtype = myneedpost.adtype;
                }
                //catlist.adtype = myneedpost.Adtype;

                catlist.Additionalcity = myneedpost.Additionalcity;
                catlist.Citycount = myneedpost.Citycount;
                //pst.me myneedpost.Metrototalamount;
                catlist.Citytotalamount = myneedpost.Citytotalamount;
                catlist.supercategoryid = myneedpost.supercategoryid;
                catlist.primarycategoryid = myneedpost.primarycategoryid.ToString();
                catlist.secondarycategoryid = myneedpost.secondarycategoryid;
                catlist.supercategoryvalue = myneedpost.supercategoryvalue;
                catlist.primarycategoryvalue = myneedpost.primarycategoryvalue;
                catlist.secondarycategoryvalue = myneedpost.secondarycategoryvalue;
                catlist.Tags = myneedpost.Tags;
                catlist.businessname = myneedpost.businessname;
                catlist.licenseno = myneedpost.businessLicenseno;
                if (catlist.licenseno == "new id")
                {
                    catlist.licenseno = "";
                }
                catlist.url = myneedpost.url;
                //catlist.supercategoryid = myneedpost.supertagid;
                catlist.Bestindustry = myneedpost.Bestindustry;
                catlist.Hideaddress = myneedpost.Hideaddress;
                catlist.Linkedinurl = myneedpost.Linkedinurl;
                catlist.Trimetros = myneedpost.Trimetros;
                catlist.Nationwide = myneedpost.Nationwide;
                catlist.Salarymode = myneedpost.Salarymode;
                catlist.Salarymodeid = myneedpost.Salarymodeid;
                if (string.IsNullOrEmpty(catlist.Category))
                {
                    catlist.Category = myneedpost.RMcategory;
                }
                catlist.Streetname = catlist.Locationname = myneedpost.Streetname;
                if (string.IsNullOrEmpty(catlist.Streetname))
                {
                    catlist.Streetname = myneedpost.LocationAddress;
                }
                catlist.Zipcode = myneedpost.Zipcode;
                catlist.userlat = myneedpost.Lat;
                catlist.userlong = myneedpost.Long;
                catlist.Newcityurl = myneedpost.Newcityurl;
                catlist.City = myneedpost.City;
                catlist.State = catlist.StateName = myneedpost.State;
                catlist.Country = myneedpost.Country;
                catlist.title = myneedpost.title;
                catlist.Titleurl = myneedpost.Titleurl;
                catlist.description = myneedpost.description;
                catlist.Contributor = catlist.ContactName = myneedpost.Contributor;
                catlist.Status = myneedpost.Status;
                catlist.Displayphone = myneedpost.Displayphone;
                catlist.Mailerstatus = myneedpost.Mailerstatus;
                catlist.Userpid = myneedpost.userpid;
                catlist.Ordertype = myneedpost.Ordertype;
                catlist.Customerid = myneedpost.Customerid;
                catlist.Campaignid = myneedpost.Campaignid;
                catlist.Postedby = myneedpost.Postedby;
                catlist.Enddate = myneedpost.Enddate;
                catlist.Email = catlist.ContactEmail = myneedpost.Email;
                catlist.Phone = catlist.ContactPhone = myneedpost.Phone;
                catlist.Startdate = myneedpost.Startdate;
                catlist.Numberofdays = myneedpost.Numberofdays;
                catlist.Amount = myneedpost.Amount;
                catlist.Folderid = myneedpost.Folderid;
                catlist.Countrycode = myneedpost.Countrycode;
                catlist.Ismobileverified = myneedpost.Ismobileverified;
                catlist.Statecode = myneedpost.StateCode;
                catlist.Metro = myneedpost.Metro;
                catlist.Metrourl = myneedpost.Metrourl;
                catlist.Supercategoryvalueurl = myneedpost.Supercategoryvalueurl;
                catlist.Primarycategoryvalueurl = myneedpost.Primarycategoryvalueurl;
                catlist.Secondarycategoryvalueurl = myneedpost.Secondarycategoryvalueurl;
                catlist.Orderid = myneedpost.Orderid;
                catlist.Expirydate = myneedpost.Expirydate;
                catlist.Noofadcnt = myneedpost.Noofadcnt;
                catlist.Adpostedcnt = myneedpost.Adpostedcnt;
                catlist.Responsemail = myneedpost.Responsemail;
                catlist.Gender = myneedpost.Gender;
                catlist.Disclosedbusiness = myneedpost.Disclosedbusiness;
                catlist.Couponcode = myneedpost.Couponcode;
                catlist.Salespersonemail = myneedpost.Salespersonemail;
                catlist.Companytype = myneedpost.Companytype;
                catlist.Bannerstatus = myneedpost.Bannerstatus;
                catlist.Banneramount = myneedpost.Banneramount;
                catlist.Categoryflag = myneedpost.Categoryflag;
                catlist.Freeadsflag = myneedpost.Freeadsflag;
                catlist.Islisting = myneedpost.Islisting;
                catlist.Addonamount = myneedpost.Addonamount;
                catlist.Emailblastamount = myneedpost.Emailblastamount;
                catlist.Bonusdays = myneedpost.Bonusdays;
                catlist.Bonusamount = myneedpost.Bonusamount;
                catlist.Pendingpaycouponcode = myneedpost.Pendingpaycouponcode;
                catlist.categoryname = myneedpost.categoryname;
                catlist.Benefits = myneedpost.Benefits;
                catlist.userlat = myneedpost.userlat;
                catlist.userlong = myneedpost.userlat;
                catlist.HideRent = myneedpost.HideRent;
                catlist.ismyneed = "1";
                catlist.ismyneedpayment = "1";
                //Paymentedit = 1;
                if(string.IsNullOrEmpty(catlist.ContactName) || string.IsNullOrEmpty(catlist.ContactPhone))
                {
                    catlist.ContactName = catlist.Contributor = myneedpost.ContactName;
                    catlist.ContactPhone = catlist.Phone = myneedpost.ContactPhone;
                    catlist.Email = catlist.ContactEmail = myneedpost.ContactEmail;
                }
                await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(catlist));
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void applycouponcode()
        {
            if(string.IsNullOrEmpty(couponcodetxt))
            {
                dialog.Toast("Enter your coupon code");
            }
            else
            {
                var couponapi = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                var details = await couponapi.applycouponcode(addid, couponcodetxt, Sordertype);
                if (details != null)
                {
                    if (details.resultinformation == "invalid")
                    {
                        dialog.Toast("Enter a valid coupon");
                    }
                    else
                    {
                        couponvisible = false;
                        removecouponvisible = true;
                        appliedcoupondtl = true;
                        Scouponcode = couponcodetxt;

                        var currentpg = GetCurrentPage();
                        var packgamt = currentpg.FindByName<Label>("packamt");
                        packgamt.Text = packageamount = details.packageamount;
                        packgamt.Text = "$"+ details.packageamount;
                        var totalamt = currentpg.FindByName<Label>("totalamt");
                        totalamt.Text = totalamount = details.totalamount;
                        totalamt.Text = "$"+ details.totalamount;
                        var discntamt = currentpg.FindByName<Label>("discntamt");
                        discntamt.Text = discountamount = details.discountamount;
                        discntamt.Text = "$"+ details.discountamount;
                    }
                }
            }
        }
        public void removecoupon()
        {
            couponvisible = true;
            removecouponvisible = false;
            appliedcoupondtl = false;
            decimal Totalamt;
            var currentpage = GetCurrentPage();
            var totalamt = currentpage.FindByName<Label>("totalamt");
            if(!string.IsNullOrEmpty(totalamount) && !string.IsNullOrEmpty(discountamount))
            {
                Scouponcode = "";
                Totalamt = Convert.ToDecimal(totalamount) + Convert.ToDecimal(discountamount);
                totalamt.Text = "$"+Totalamt.ToString();
                Payment_VM.totalamount = Totalamt.ToString();
            }
        }
        public void openbillingdeatils()
        {
            if (billingvisible == true)
                billingvisible = false;
            else
                billingvisible = true;
        }
        public async void GetLocationname()
        {
            try
            {
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
        public void showcvvimg()
        {
            contentviewvisible = true;
            monthvisible = false;
            yearvisible = false;
            cvvimgvisible = true;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showexpirymonthlist()
        {
            var curentpage = GetCurrentPage();
            var expirymonth = curentpage.FindByName<ListView>("ExpiryMonth");
           

            foreach(var data in Expirymonthlist)
            {
                if(expirymonthvalue == data.months.ToString())
                {
                    data.checkimage = "RadioButtonChecked.png";
                    expirymonthvalue = data.months.ToString();
                    expirymonthID = data.months;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }
            
            expirymonth.ItemsSource = null;
            expirymonth.ItemsSource = Expirymonthlist;
            contentviewvisible = true;
            monthvisible = true;
            yearvisible = false;
            cvvimgvisible = false;
        }
        public void showexpiryyearlist()
        {
            var curentpage = GetCurrentPage();
            var expiryyear = curentpage.FindByName<ListView>("ExpiryYear");

            foreach(var data in expiryyearslist)
            {
                if(expiryyearvalue==data.years.ToString())
                {
                    data.checkimage = "RadioButtonChecked.png";
                    expiryyearvalue = data.years.ToString();
                    expiryyearID = data.years;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }

            expiryyear.ItemsSource = null;
            expiryyear.ItemsSource = expiryyearslist;
            contentviewvisible = true;
            monthvisible = false;
            cvvimgvisible = false;
            yearvisible = true;
            
        }
       
        public void selectexpirymonthvalue(Payment_Params payment_Params)
        {
            var currentpage = GetCurrentPage();
            Label monthtxtclr = currentpage.FindByName<Label>("lblexpdate");
            monthtxtclr.TextColor = Color.FromHex("#212121");
            expirymonthvalue = payment_Params.months.ToString();
            expirymonthID = payment_Params.months;
            contentviewvisible = false;
        }
        public void selectexpiryyearvalue(Payment_Params payment_Params)
        {
            var currentpage = GetCurrentPage();
            Label yeartxtclr = currentpage.FindByName<Label>("lblexpyear");
            yeartxtclr.TextColor = Color.FromHex("#212121");
            expiryyearvalue = payment_Params.years.ToString();
            expiryyearID= payment_Params.years;
            contentviewvisible = false;
        }
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        string orderid = "",shopingcartid="";
        public async void paymentfirst()
        {
            var currpage = GetCurrentPage();
            //   CreateToken("4000000000003089", 02, 2020, "123");
            try
            {
                if (paymentvalidation())
                {
                    dialog.ShowLoading("");
                    string sPostedviaid = "";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                    {
                        sPostedviaid = "194";
                    }
                    else
                    {
                        sPostedviaid = "197";
                    }
                    Shoppingcartdetails pmnt = new Shoppingcartdetails()
                    {
                        name = billingname,
                        email = billingemail,
                        mobileno = billingmobileno,
                        billingcity = city,
                        billingaddress = LocationAddress,
                        billingstatecode = statecode,
                        billingzipcode = zipcode,
                        billingcountry = country,
                        amount = packageamount.Replace("$", ""),
                        userpid = Commonsettings.UserPid,
                        adid = addid,
                        cardnumber = cardnumber,
                        expmnth = expirymonthID.ToString(),
                        expyear = expiryyearID.ToString(),
                        cvv = CVVnumber,
                        usertype = usertype,
                        postedviaid = sPostedviaid,
                        couponcode = Scouponcode,
                        totalamount = Payment_VM.totalamount.Replace("$", ""),
                        ip = ipaddress
                    };
                    var baseApi = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                    shoppingcart details = await baseApi.Saveshoppingcart(pmnt);
                    orderid = details.ordernumber.ToString();
                    shopingcartid = details.shopingcartid.ToString();
                    if (details.errormsg.Length > 0)
                    {
                        dialog.HideLoading();
                        dialog.Toast(details.errormsg.ToString());
                    }
     
                    else
                    {
                        await currpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.postsuccess(pmnt.adid, usertype, ""));
                    }
                }
            }
           catch(Exception ex)
            {
                dialog.HideLoading();
                dialog.Toast("Payment failed.");
                string msg = ex.Message.ToString();
            }
            
        }
        //public void CreateToken(string cardNumber, int cardExpMonth, int cardExpYear, string cardCVC)
        //{
        //    //StripeConfiguration.SetApiKey("sk_test_4eC39HqLyjWDarjtT1zdp7dc");
        //    StripeConfiguration.SetApiKey("sk_live_KKWWltg7m7vWwhL5XawZW1rw");

        //    try
        //    {
        //        var tokenOptions = new Stripe.TokenCreateOptions()
        //        {
        //            Card = new Stripe.CreditCardOptions()
        //            {
        //                Number = cardNumber,
        //                ExpMonth = cardExpMonth,
        //                ExpYear = cardExpYear,
        //                Cvc = cardCVC
        //            }
        //        };

        //        var tokenService = new Stripe.TokenService();
        //        Stripe.Token stripeToken = tokenService.Create(tokenOptions);

        //        if (stripeToken.Id != "" || stripeToken.Id != null)
        //            CCStripePayment(stripeToken.Id, 50);
        //    }
        //    catch (StripeException exception)
        //    {
        //        dialog.HideLoading();
        //        switch (exception.StripeError.ErrorType)
        //        {
        //            case "card_error":
        //                dialog.Toast("Please try another card number");

        //                break;

        //            default:
        //                throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dialog.HideLoading();
        //        string msg = ex.Message.ToString();
        //        dialog.Toast(msg);
        //    }

        //}
        //public async void CCStripePayment(string token, int totalamount)
        //{
        //    string sResult = "", Stripe_TransId = "", sPostedviaid;
        //    var currpage = GetCurrentPage();
        //    try
        //    {
        //       // StripeConfiguration.SetApiKey("sk_test_4eC39HqLyjWDarjtT1zdp7dc");
        //        StripeConfiguration.SetApiKey("sk_live_KKWWltg7m7vWwhL5XawZW1rw");
        //        var options = new Stripe.ChargeCreateOptions
        //        {
        //            Amount = totalamount * 100, // *100 (for cent to dollar)
        //            Currency = "USD",
        //            Description = "SulekhaStripePayment",
        //            SourceId = token,
        //        };
        //        var service = new Stripe.ChargeService();
        //        Stripe.Charge charge = service.Create(options);
        //        sResult = charge.Status;

        //        if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
        //        {
        //            sPostedviaid = "194";
        //        }
        //        else
        //        {
        //            sPostedviaid = "197";
        //        }

        //        Payment pmnt = new Payment();
        //        if (sResult == "succeeded")
        //        {
        //            if (cardnumber == "4242424242424242")
        //            {
        //                pmnt = new Payment()
        //                {
        //                    name = cardname,
        //                    email = billingemail,
        //                    billingcity = city,
        //                    orderid = orderid,
        //                    transid = "9999999999",
        //                    billingstatecode = statecode,
        //                    billingcountry = country,
        //                    amount = amnt.Replace("$", ""),
        //                    adid = addid,
        //                    shopingcartid = shopingcartid,
        //                    ip = ipaddress,
        //                    usertype = usertype,
        //                    postedviaid = sPostedviaid,
        //                    couponcode=Scouponcode,
        //                    totalamount = Payment_VM.totalamount.Replace("$", "")
        //                };
        //            }
        //            else
        //            {
        //                Stripe_TransId = charge.BalanceTransactionId;
        //                pmnt = new Payment()
        //                {
        //                    name = cardname,
        //                    email = billingemail,
        //                    billingcity = city,
        //                    orderid = orderid,
        //                    transid = Stripe_TransId,
        //                    billingstatecode = statecode,
        //                    billingcountry = country,
        //                    amount = amnt.Replace("$", ""),
        //                    adid = addid,
        //                    shopingcartid = shopingcartid,
        //                    ip = ipaddress,
        //                    usertype = usertype,
        //                    postedviaid = sPostedviaid,
        //                    couponcode = Scouponcode,
        //                    totalamount=Payment_VM.totalamount.Replace("$", "")
        //                };
        //            }
                   
        //            var baseApi = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
        //            Paymentsucess details = await baseApi.Addpayment(pmnt);
        //            if (details.resultinformation == "sucess")
        //            {
        //                dialog.HideLoading();
        //                await currpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.postsuccess(pmnt.adid, usertype, ""));
        //            }
        //            else
        //            {
        //                dialog.HideLoading();
        //                dialog.Toast("Your payment was unsucessful.Please try again!");
        //            }

        //        }
        //    }
        //    catch (StripeException exception)
        //    {
        //        dialog.HideLoading();
        //        switch (exception.StripeError.ErrorType)
        //        {
        //            case "card_error":
        //                dialog.Toast(exception.StripeError.Message + ". Please try another card");
        //                break;
        //            default:
        //                throw;
        //        }
                
        //    }
          
        //}
        //public void Changeadstatus()
        //{

        //}

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentPage()
        {
            var currentpage = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }

        //public async void stripepaymentfirst()
        //{
           
        //    try
        //    {
        //        if (paymentvalidation())
        //        {
        //            dialog.ShowLoading("");

        //            Shoppingcartdetails pmnt = new Shoppingcartdetails()
        //            {
        //                name = billingname,
        //                email = billingemail,
        //                mobileno = billingmobileno,
        //                billingcity = city,
        //                billingaddress = LocationAddress,
        //                billingstatecode = statecode,
        //                billingzipcode = zipcode,
        //                billingcountry = country,
        //                amount = totalamount.Replace("$", ""),
        //                userpid = Commonsettings.UserPid,
        //                adid = addid,
        //                ip = ipaddress
        //            };
        //            var baseApi = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
        //            shoppingcart details = await baseApi.Saveshoppingcart(pmnt);
        //            orderid = details.ordernumber.ToString();
        //            shopingcartid = details.shopingcartid.ToString();
        //            if (details.ordernumber > 0)
        //                CreateToken(cardnumber, Convert.ToInt32(expirymonthID), expiryyearID, CVVnumber);
        //            else
        //            {
        //                dialog.HideLoading();
        //                dialog.Toast("OrderNumber Generation failed. Please try again.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dialog.HideLoading();
        //        dialog.Toast("Payment failed.");
        //        string msg = ex.Message.ToString();
        //    }

        //}

    }
}
