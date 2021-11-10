using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using NRIApp.Rentals.Features.Post.Models;
using System.Windows.Input;
using Acr.UserDialogs;
using Refit;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Interface;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;

namespace NRIApp.Rentals.Features.Post.ViewModels
{
    public class Payment_VMRT : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;

        public static string expirymonthID;
        public static int expiryyearID;

        public Command TaponProceed { get; set; }
        public List<Payment_Params_RT> Expirymonthlistdata { get; set; }
        public List<Payment_Params_RT> Expiryyearlistdata { get; set; }
        public ICommand TaponExpirymonth { get; set; }
        public ICommand TaponExpiryyear { get; set; }
        public ICommand selectexpirymonth { get; set; }
        public ICommand selectexpiryyear { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command Taponcvvimg { get; set; }
        public Command<RTLocation> LocationCmd { get; set; }
        public Command Selectgoogleaddress { get; set; }
        public ICommand openbilling { get; set; }
        public ICommand ApplyCouponCode { get; set; }
        public ICommand makechangesTap { get; set; }
        public ICommand removecouponcmd { get; set; }

        public List<Payment_Params_RT> Expirymonthlist = new List<Payment_Params_RT>()
        {
            new Payment_Params_RT {months="01",monthname="01 (JAN)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="02",monthname="02 (FEB)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="03",monthname="03 (MAR)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="04",monthname="04 (APR)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="05",monthname="05 (MAY)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="06",monthname="06 (JUN)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="07",monthname="07 (JUL)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="08",monthname="08 (AUG)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="09",monthname="09 (SEP)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="10",monthname="10 (OCT)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="11",monthname="11 (NOV)",checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT {months="12",monthname="12 (DEC)",checkimage="RadioButtonUnChecked.png"},
        };
        public List<Payment_Params_RT> expiryyearslist = new List<Payment_Params_RT>()
        {
            new Payment_Params_RT{years=2019,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2020,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2021,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2022,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2023,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2024,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2025,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2026,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2027,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2028,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2029,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2030,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2031,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2032,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2033,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2034,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2035,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2036,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2037,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2038,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2039,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2040,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2041,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2042,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2043,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2044,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2045,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2046,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2047,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2048,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2049,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2050,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2051,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2052,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2053,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2054,checkimage="RadioButtonUnChecked.png"},
            new Payment_Params_RT{years=2055,checkimage="RadioButtonUnChecked.png"},
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
            set { _cardnumber = value; OnPropertyChanged(nameof(cardnumber)); }
        }
        private string _cardname;
        public string cardname
        {
            get { return _cardname; }
            set { _cardname = value; OnPropertyChanged(nameof(cardname)); }
        }
        private string _expirymonthvalue = "Month";
        public string expirymonthvalue
        {
            get { return _expirymonthvalue; }
            set { _expirymonthvalue = value; OnPropertyChanged(nameof(expirymonthvalue)); }
        }
        private string _expiryyearvalue = "Year";
        public string expiryyearvalue
        {
            get { return _expiryyearvalue; }
            set { _expiryyearvalue = value; OnPropertyChanged(nameof(expiryyearvalue)); }
        }
        private string _CVVnumber;
        public string CVVnumber
        {
            get { return _CVVnumber; }
            set { _CVVnumber = value; OnPropertyChanged(nameof(CVVnumber)); }
        }
        private bool _contentviewvisible;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _cvvimgvisible;
        public bool cvvimgvisible
        {
            get { return _cvvimgvisible; }
            set { _cvvimgvisible = value; OnPropertyChanged(nameof(cvvimgvisible)); }
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
        public void OnLocationClick(RTLocation obj)
        {
            try
            {
                LocationName = TempLocationName = obj.citystatecode;
                city = obj.city;
                zipcode = obj.zipcode.ToString();
                country = obj.country;
                statecode = obj.statecode;
                IsVisibleList = false;
            }
            catch (Exception e) { }
        }
        private List<RTLocation> _LocationList { get; set; }
        public List<RTLocation> LocationList
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
        private static string amnt = "", addid = "",usertype="", Sordertype="",Scouponcode="";
        public static string packageamount, discountamount, totalamount;
        RTPostFirst myneedpost = new RTPostFirst();
        public Payment_VMRT(string amount,string ordertype, string adid, RTPostFirst pst)
        {
            try
            {
                myneedpost = pst;
                Scouponcode = ""; totalamount = "";
                if (pst.usertype == "1")
                {
                    couponvisible = true;
                }
                else
                {
                    couponvisible = false;
                }
                amnt = amount;
                addid = adid;
                if (string.IsNullOrEmpty(addid) || addid == "0")
                {
                    addid = pst.adid.ToString();
                }
                Sordertype = ordertype;
                totalamount = amount;
                packageamount = amount;
                usertype = pst.usertype;
                billingname = pst.ContactName;
                if(string.IsNullOrEmpty(billingname))
                {
                    billingname = pst.Contributor;
                }
                billingemail = pst.ContactEmail;
                if (string.IsNullOrEmpty(billingemail))
                {
                    billingemail = pst.Email;
                }
                billingmobileno = pst.ContactPhone;
                if (string.IsNullOrEmpty(billingmobileno))
                {
                    billingmobileno = pst.Phone;
                }
                Selectcountry = pst.Countrycode;
                TempLocationName = pst.City + ", " + pst.StateCode;
                LocationName = pst.City + ", " + pst.StateCode;
                city = pst.City;
                zipcode = pst.Zipcode;
                country = pst.Country;
                statecode = pst.StateCode;
                TempLocationAddress = pst.LocationAddress;
                LocationAddress = pst.LocationAddress;
                if (string.IsNullOrEmpty(pst.LocationAddress))
                {
                    TempLocationAddress = pst.Streetname;
                    LocationAddress = pst.Streetname;
                }
                Expirymonthlistdata = Expirymonthlist;
                Expiryyearlistdata = Expiryyearlistdata;

                Taponcvvimg = new Command(showcvvimg);
                PopupContentTap = new Command(showcontentview);
                ContentViewTap = new Command(Closecontentview);

                TaponExpiryyear = new Command(showexpiryyearlist);
                TaponExpirymonth = new Command(showexpirymonthlist);
                selectexpirymonth = new Command<Payment_Params_RT>(selectexpirymonthvalue);
                selectexpiryyear = new Command<Payment_Params_RT>(selectexpiryyearvalue);
                TaponProceed = new Command(paymentfirst);
                LocationCmd = new Command<RTLocation>(OnLocationClick);
                Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
                openbilling = new Command(openbillingdeatils);
                removecouponcmd = new Command(removecoupon);
                ApplyCouponCode = new Command(applycouponcode);
                makechangesTap = new Command(Editoption);
            }
            catch(Exception ex)
            {

            }
        }
        Rentals.Features.Post.Models.RTCategoryList rtcatlist = new Rentals.Features.Post.Models.RTCategoryList();
        public async void Editoption()
        {
            try
            {
                var currentpage = GetCurrentPage();
                rtcatlist.pricemode = myneedpost.pricemode;
                rtcatlist.accomodate = myneedpost.accomodate;
                rtcatlist.stayperiod = myneedpost.stayperiod;
                rtcatlist.Rent = myneedpost.Rent;
                rtcatlist.attachedbaths = myneedpost.attachedbaths;
                rtcatlist.availablefrm = myneedpost.availablefrm;
                rtcatlist.Movedate = myneedpost.Movedate;
                rtcatlist.service = myneedpost.service;
                rtcatlist.adid = myneedpost.adid;
                //myneedpost.Adsid;
                rtcatlist.agentid = myneedpost.agentid;
                rtcatlist.Isagent = myneedpost.Isagent;
                rtcatlist.XmlPhotopath = myneedpost.XmlPhotopath;
                rtcatlist.Imagepath = myneedpost.Imagepath;
                //rtcatlist.Imagepath = myneedpost.HtmlPhotopath;
                rtcatlist.Photoscnt = myneedpost.Photoscnt;
                rtcatlist.Ispaid = myneedpost.Ispaid;
                rtcatlist.Category = rtcatlist.maincategory = myneedpost.Category;
                rtcatlist.Subcategory = myneedpost.Subcategory;
                if (!string.IsNullOrEmpty(myneedpost.Adtype))
                {
                    if (myneedpost.Adtype == "offered")
                    {
                        rtcatlist.adtype = "1";
                    }
                    else
                    {
                        rtcatlist.adtype = "0";
                    }
                }
                rtcatlist.Additionalcity = myneedpost.Additionalcity;
                rtcatlist.Citycount = myneedpost.Citycount;
                rtcatlist.supercategoryid = Convert.ToInt32(myneedpost.supercategoryid);
                rtcatlist.primarycategoryid = myneedpost.primarycategoryid;
                rtcatlist.secondarycategoryid = myneedpost.secondarycategoryid;
                rtcatlist.supercategoryvalue = myneedpost.supercategoryvalue;
                rtcatlist.primarycategoryvalue = myneedpost.primarycategoryvalue;
                rtcatlist.secondarycategoryvalue = myneedpost.secondarycategoryvalue;
                rtcatlist.tertiarycategoryid = Convert.ToInt32(myneedpost.tertiarycategoryid);
                rtcatlist.tertiarycategoryvalue = myneedpost.tertiarycategoryvalue;
                rtcatlist.Tags = myneedpost.Tags;
                rtcatlist.businessname = myneedpost.businessname;
                rtcatlist.licenseno = myneedpost.businessLicenseno;
                if (rtcatlist.licenseno == "new id")
                {
                    rtcatlist.licenseno = "";
                }
                rtcatlist.url = myneedpost.url;
                //rtcatlist.supercategoryid = Convert.ToInt32(myneedpost.supercategoryid);
                rtcatlist.Bestindustry = myneedpost.Bestindustry;
                rtcatlist.Hideaddress = myneedpost.Hideaddress;
                rtcatlist.Trimetros = myneedpost.Trimetros;
                rtcatlist.Nationwide = myneedpost.Nationwide;
                rtcatlist.Salarymode = myneedpost.Salarymode;
                rtcatlist.Salarymodeid = myneedpost.Salarymodeid;
                rtcatlist.Streetname = rtcatlist.Locationname = myneedpost.Streetname;
                rtcatlist.Zipcode = myneedpost.Zipcode;
                rtcatlist.Lat = myneedpost.Lat;
                rtcatlist.Long = myneedpost.Long;
                rtcatlist.Newcityurl = myneedpost.Newcityurl;
                rtcatlist.City = myneedpost.City;
                rtcatlist.State = rtcatlist.StateName = myneedpost.State;
                rtcatlist.Country = myneedpost.Country;
                rtcatlist.title = myneedpost.Title;
                rtcatlist.Titleurl = myneedpost.Titleurl;
                rtcatlist.description = myneedpost.Description;
                rtcatlist.Contributor = rtcatlist.ContactName = myneedpost.Contributor;
                rtcatlist.Status = myneedpost.Status;
                rtcatlist.Displayphone = myneedpost.Displayphone;
                rtcatlist.Mailerstatus = myneedpost.Mailerstatus;
                rtcatlist.Userpid = myneedpost.userpid;
                rtcatlist.Ordertype = myneedpost.Ordertype;
                rtcatlist.Customerid = myneedpost.Customerid;
                rtcatlist.Campaignid = myneedpost.Campaignid;
                rtcatlist.Postedby = myneedpost.Postedby;
                rtcatlist.Enddate = myneedpost.Enddate;
                rtcatlist.Email = rtcatlist.ContactEmail = myneedpost.Email;
                rtcatlist.Phone = rtcatlist.ContactPhone = myneedpost.Phone;
                rtcatlist.Startdate = myneedpost.Startdate;
                rtcatlist.Numberofdays = myneedpost.Numberofdays;
                rtcatlist.Amount = myneedpost.Amount;
                rtcatlist.Folderid = myneedpost.Folderid;
                rtcatlist.Countrycode = myneedpost.Countrycode;
                rtcatlist.Ismobileverified = myneedpost.Ismobileverified;
                rtcatlist.Statecode = myneedpost.StateCode;
                rtcatlist.Metro = myneedpost.Metro;
                rtcatlist.Metrourl = myneedpost.Metrourl;
                rtcatlist.Supercategoryvalueurl = myneedpost.Supercategoryvalueurl;
                rtcatlist.Primarycategoryvalueurl = myneedpost.Primarycategoryvalueurl;
                rtcatlist.Secondarycategoryvalueurl = myneedpost.Secondarycategoryvalueurl;
                rtcatlist.Orderid = myneedpost.Orderid;
                rtcatlist.Expirydate = myneedpost.Expirydate;
                rtcatlist.Noofadcnt = myneedpost.Noofadcnt;
                rtcatlist.Adpostedcnt = myneedpost.Adpostedcnt;
                rtcatlist.Responsemail = myneedpost.Responsemail;
                rtcatlist.Gender = myneedpost.Gender;
                rtcatlist.Disclosedbusiness = myneedpost.Disclosedbusiness;
                rtcatlist.Couponcode = myneedpost.Couponcode;
                rtcatlist.Salespersonemail = myneedpost.Salespersonemail;
                rtcatlist.Companytype = myneedpost.Companytype;
                rtcatlist.Bannerstatus = myneedpost.Bannerstatus;
                rtcatlist.Banneramount = myneedpost.Banneramount;
                rtcatlist.Categoryflag = myneedpost.Categoryflag;
                rtcatlist.Freeadsflag = myneedpost.Freeadsflag;
                rtcatlist.Islisting = myneedpost.Islisting;
                rtcatlist.Addonamount = myneedpost.Addonamount;
                //rtcatlist.Emailblastamount = myneedpost.Emailblastamount;
                rtcatlist.Bonusdays = myneedpost.Bonusdays;
                rtcatlist.Bonusamount = myneedpost.Bonusamount;
                rtcatlist.Pendingpaycouponcode = myneedpost.Pendingpaycouponcode;
                rtcatlist.categoryname = myneedpost.categoryname;
                rtcatlist.Benefits = myneedpost.Benefits;
                rtcatlist.noofbaths = myneedpost.noofbaths;
                rtcatlist.area = myneedpost.area;
                rtcatlist.ismyneed = "1";
                rtcatlist.ismyneedpayment = "1";
                if (string.IsNullOrEmpty(rtcatlist.ContactName) || string.IsNullOrEmpty(rtcatlist.ContactPhone))
                {
                    rtcatlist.ContactName = rtcatlist.Contributor = myneedpost.ContactName;
                    rtcatlist.ContactPhone = rtcatlist.Phone = myneedpost.ContactPhone;
                    rtcatlist.Email = rtcatlist.ContactEmail = myneedpost.ContactEmail;
                }
                if (string.IsNullOrEmpty(rtcatlist.Rent))
                {
                    rtcatlist.Rent = myneedpost.ExpRent;
                }
                if (string.IsNullOrEmpty(rtcatlist.area))
                {
                    rtcatlist.sqrfeet = myneedpost.sqrfeet;
                }
                if (string.IsNullOrEmpty(rtcatlist.Streetname))
                {
                    rtcatlist.Streetname = myneedpost.LocationAddress;
                }
                rtcatlist.title = myneedpost.title;
                rtcatlist.description = myneedpost.description;
                await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtcatlist));
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
                dialog.Toast("Enter coupon code");
            }
            else
            {
                var couponapi = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                var details = await couponapi.applycouponcode(addid, couponcodetxt, Sordertype);
                if (details != null)
                {
                    if (details.resultinformation == "invalid")
                    {
                        dialog.Toast("Enter a valid coupon");
                    }
                    else
                    {
                        Scouponcode = couponcodetxt;
                        couponvisible = false;
                        removecouponvisible = true;
                        appliedcoupondtl = true;
                        var currentpg = GetCurrentPage();
                        var packgamt = currentpg.FindByName<Label>("packamt");
                        packgamt.Text = packageamount = details.packageamount;
                        packgamt.Text = "$"+ details.packageamount;
                        var totalamt = currentpg.FindByName<Label>("totalamt");
                        totalamt.Text = totalamount = details.totalamount;
                        totalamt.Text = "$" + details.totalamount;
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
            if (!string.IsNullOrEmpty(totalamount) && !string.IsNullOrEmpty(discountamount))
            {
                Totalamt = Convert.ToDecimal(totalamount) + Convert.ToDecimal(discountamount);
                totalamt.Text = Totalamt.ToString();
                totalamt.Text = "$"+ Totalamt.ToString();
                Payment_VMRT.totalamount = Totalamt.ToString();
                Scouponcode = "";
            }
            //totalamountt = totalamount + discountamount;
            //OnPropertyChanged(totalamountt);
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

            foreach (var data in Expirymonthlist)
            {
                if (expirymonthvalue == data.months.ToString())
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

            foreach (var data in expiryyearslist)
            {
                if (expiryyearvalue == data.years.ToString())
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

        public void selectexpirymonthvalue(Payment_Params_RT payment_Params)
        {
            var currentpage = GetCurrentPage();
            Label monthtxtclr = currentpage.FindByName<Label>("lblexpdate");
            monthtxtclr.TextColor = Color.FromHex("#212121");
            expirymonthvalue = payment_Params.months.ToString();
            expirymonthID = payment_Params.months;
            contentviewvisible = false;
        }
        public void selectexpiryyearvalue(Payment_Params_RT payment_Params)
        {
            var currentpage = GetCurrentPage();
            Label yeartxtclr = currentpage.FindByName<Label>("lblexpyear");
            yeartxtclr.TextColor = Color.FromHex("#212121");
            expiryyearvalue = payment_Params.years.ToString();
            expiryyearID = payment_Params.years;
            contentviewvisible = false;
        }
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        string orderid = "", shopingcartid = "";
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
                    Shoppingcartdetails_RT pmnt = new Shoppingcartdetails_RT()
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
                        totalamount = Payment_VMRT.totalamount.Replace("$", ""),
                        ip = ipaddress
                    };
                    var baseApi = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                    shoppingcart_RT details = await baseApi.Saveshoppingcart(pmnt);
                    orderid = details.ordernumber.ToString();
                    shopingcartid = details.shopingcartid.ToString();
                    if (details.errormsg.Length > 0)
                    {
                        dialog.HideLoading();
                        dialog.Toast(details.errormsg.ToString());
                    }
                    else
                    {
                       await currpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.postsuccess(pmnt.adid, usertype, ""));
                    }
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                dialog.Toast("Payment failed.");
                string msg = ex.Message.ToString();
            }
        }

      /*  public async void stripepaymentfirst()
        {
            //   CreateToken("4000000000003089", 02, 2020, "123");
            try
            {
                if (paymentvalidation())
                {
                    dialog.ShowLoading("");

                    Shoppingcartdetails_RT pmnt = new Shoppingcartdetails_RT()
                    {
                        name = billingname,
                        email = billingemail,
                        mobileno = billingmobileno,
                        billingcity = city,
                        billingaddress = LocationAddress,
                        billingstatecode = statecode,
                        billingzipcode = zipcode,
                        billingcountry = country,
                        amount = totalamount.Replace("$", ""),
                        userpid = Commonsettings.UserPid,
                        adid = addid,
                        ip = ipaddress,
                    };
                    var baseApi = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                    shoppingcart_RT details = await baseApi.Saveshoppingcart(pmnt);
                    orderid = details.ordernumber.ToString();
                    shopingcartid = details.shopingcartid.ToString();
                    if (details.ordernumber > 0)
                        CreateToken(cardnumber, Convert.ToInt32(expirymonthID), expiryyearID, CVVnumber);
                    else
                    {
                        dialog.HideLoading();
                        dialog.Toast("OrderNumber Generation failed. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                dialog.Toast("Please try again.");
                string msg = ex.Message.ToString();
            }

        }
        public void CreateToken(string cardNumber, int cardExpMonth, int cardExpYear, string cardCVC)
        {
            //StripeConfiguration.SetApiKey("sk_test_4eC39HqLyjWDarjtT1zdp7dc");
            StripeConfiguration.SetApiKey("sk_live_KKWWltg7m7vWwhL5XawZW1rw");

            try
            {
                var tokenOptions = new Stripe.TokenCreateOptions()
                {
                    Card = new Stripe.CreditCardOptions()
                    {
                        Number = cardNumber,
                        ExpMonth = cardExpMonth,
                        ExpYear = cardExpYear,
                        Cvc = cardCVC
                    }
                };

                var tokenService = new Stripe.TokenService();
                Stripe.Token stripeToken = tokenService.Create(tokenOptions);

                if (stripeToken.Id != "" || stripeToken.Id != null)
                    CCStripePayment(stripeToken.Id, 50);
            }
            catch (StripeException exception)
            {
                dialog.HideLoading();
                switch (exception.StripeError.ErrorType)
                {
                    case "card_error":
                        dialog.Toast("Please try another card number");

                        break;

                    default:
                        throw;
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                string msg = ex.Message.ToString();
                dialog.Toast(msg);
            }


        }
        public async void CCStripePayment(string token, int totalamount)
        {
            string sResult = "", Stripe_TransId = "", sPostedviaid="";
            var currpage = GetCurrentPage();
            try
            {
                //StripeConfiguration.SetApiKey("sk_test_4eC39HqLyjWDarjtT1zdp7dc");
                StripeConfiguration.SetApiKey("sk_live_KKWWltg7m7vWwhL5XawZW1rw");
                var options = new Stripe.ChargeCreateOptions
                {
                    Amount = totalamount * 100, // *100 (for cent to dollar)
                    Currency = "USD",
                    Description = "SulekhaStripePayment",
                    SourceId = token,
                };
                var service = new Stripe.ChargeService();
                Stripe.Charge charge = service.Create(options);
                Payment_RT pmnt = new Payment_RT();
                sResult = charge.Status;

                if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                {
                    sPostedviaid = "194";
                }
                else
                {
                    sPostedviaid = "197";
                }

                if (sResult == "succeeded")
                {
                    if (cardnumber == "4242424242424242")
                    {
                        pmnt = new Payment_RT()
                        {
                            name = cardname,
                            email = billingemail,
                            billingcity = city,
                            orderid = orderid,
                            transid = "9999999999",
                            billingstatecode = statecode,
                            billingcountry = country,
                            amount = amnt.Replace("$", ""),
                            adid = addid,
                            shopingcartid = shopingcartid,
                            ip = ipaddress,
                            usertype=usertype,
                            postedviaid = sPostedviaid,
                            couponcode=Scouponcode,
                            totalamount = Payment_VMRT.totalamount.Replace("$", "")
                        };
                    }
                       else
                    {
                        Stripe_TransId = charge.BalanceTransactionId;
                        pmnt = new Payment_RT()
                        {
                            name = cardname,
                            email = billingemail,
                            billingcity = city,
                            orderid = orderid,
                            transid = Stripe_TransId,
                            billingstatecode = statecode,
                            billingcountry = country,
                            amount = amnt.Replace("$", ""),
                            adid = addid,
                            shopingcartid = shopingcartid,
                            ip = ipaddress,
                            usertype = usertype,
                            postedviaid = sPostedviaid,
                            couponcode = Scouponcode,
                            totalamount=Payment_VMRT.totalamount.Replace("$", "")
                        };
                    }
                    var baseApi = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                    Paymentsucess_RT details = await baseApi.Addpayment(pmnt);
                    if (details.resultinformation == "sucess")
                    {
                        dialog.HideLoading();
                        await currpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.postsuccess(pmnt.adid,usertype,""));
                    }
                    else
                    {
                        dialog.HideLoading();
                        dialog.Toast("Your payment was unsucessful.Please try again!");
                    }

                }
            }
            catch (StripeException exception)
            {
                dialog.HideLoading();
                switch (exception.StripeError.ErrorType)
                {
                    case "card_error":
                        dialog.Toast(exception.StripeError.Message + ". Please try another card");
                        break;
                    default:
                        throw;
                }

            }

        }
        public void Changeadstatus()
        {

        } */

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentPage()
        {
            var currentpage = Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
   
}
