using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Net.Http;
using Acr.UserDialogs;
using System.Windows.Input;
using NRIApp.Roommates.Features.Post.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NRIApp.DayCare.Features.Post.Models;
using Refit;
using NRIApp.Helpers;
using NRIApp.DayCare.Features.Post.Interface;
using NRIApp.Roommates.Features.Post.Interface;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class PaymentDC_VM : INotifyPropertyChanged
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
           // new Payment_Params{years=2019,checkimage="RadioButtonUnChecked.png"},
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

        private string _validitydays = "";
        public string validitydays
        {
            get { return _validitydays; }
            set { _validitydays = value; OnPropertyChanged(nameof(validitydays)); }
        }

        private string _selectedpackagename = "";
        public string selectedpackagename
        {
            get { return _selectedpackagename; }
            set { _selectedpackagename = value; OnPropertyChanged(nameof(selectedpackagename)); }
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
        private bool _couponvisible = true;
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
        private string _ordergroup = "";
        public string ordergroup
        {
            get { return _ordergroup; }
            set { _ordergroup = value; OnPropertyChanged(nameof(ordergroup)); }
        }
        //private string _summercamp = "";
        //public string summercamp
        //{
        //    get { return _summercamp; }
        //    set { _summercamp = value; OnPropertyChanged(nameof(summercamp)); }
        //}
        private bool _summercampvisible =false;
        public bool summercampvisible
        {
            get { return _summercampvisible; }
            set { _summercampvisible = value; OnPropertyChanged(nameof(summercampvisible)); }
        }
        private string _summercampImg = "CheckBoxUnChecked.png";
        public string summercampImg
        {
            get { return _summercampImg; }
            set { _summercampImg = value; OnPropertyChanged(nameof(summercampImg)); }
        }
        private string _summercamptxt = "";
        public string summercamptxt
        {
            get { return _summercamptxt; }
            set { _summercamptxt = value; OnPropertyChanged(nameof(summercamptxt)); }
        }
        private int _summercampID = 0;
        public int summercampID
        {
            get { return _summercampID; }
            set { _summercampID = value; OnPropertyChanged(nameof(summercampID)); }
        }
        public Command summercampTap { get; set; }
        public Command makechangesTap { get; set; }
        private static string amnt = "", addid = "", usertype = "1", Sordertype = "", Scouponcode = "";
        public static string packageamount, discountamount, totalamount, banneramount = "",cities = "";
        //public static int citycount = 0;
        Daycareposting post = new Daycareposting();
        public PaymentDC_VM(Daycareposting postdata, string pkgname, string paymentamt)
        {
            post = postdata;
//            supercategoryid == 1
//primarycatid == 3
//seccatid == 8,9,49
            if (postdata.supercategoryid=="1"&&postdata.primarycategoryid=="3"&&(postdata.secondarycategoryid=="8"|| postdata.secondarycategoryid == "9"|| postdata.secondarycategoryid == "49"))//Child Care -> Nanny/ Care Center/ Family Child Care
            {
                summercampvisible = true;
                //summercamptxt = "Are you providing summer camp? We will promote you. Just pay $"+ postdata.addonamount;
                summercamptxt =  "$" + postdata.addonamount;
            }
            else
            {
                summercampvisible = false;
            }
            post.ordertype = pkgname;
            Scouponcode = ""; totalamount = "";
            amnt = paymentamt;
            addid = postdata.businessid;
            totalamount = paymentamt;
            packageamount = paymentamt;
           // citycount = citycnt;
            billingname = postdata.contactName;
            billingemail = postdata.emailid;
            billingmobileno = postdata.ContactNo;
            Sordertype = postdata.ordertype;
            Selectcountry = postdata.countrycode;
            TempLocationName = postdata.city + ", " + postdata.state;
            LocationName = postdata.city + ", " + postdata.state;
            city = postdata.city;
            zipcode = postdata.zipcode;
            country = postdata.country;
            statecode = postdata.state;
            TempLocationAddress = postdata.locationAddress;
            LocationAddress = postdata.locationAddress;
            Expirymonthlistdata = Expirymonthlist;
            Expiryyearlistdata = Expiryyearlistdata;
            //ordersummary
            selectedpackagename = char.ToUpper(postdata.ordertype[0]) + postdata.ordertype.Substring(1);
            validitydays = postdata.numberofdays+" days";
            ordergroup = postdata.ordergroup;
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
            summercampTap = new Command(checksummercamp);
            makechangesTap = new Command(Editoption);
        }

        //NRIApp.DayCare.Features.Post.Models.DC_CategoryList dccatlist = new DayCare.Features.Post.Models.DC_CategoryList();
        public async void Editoption()
        {
            try
            {
                dialog.ShowLoading("", null);
                var currentpage = GetCurrentPage();
                var AdDetailsAPI = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
               // addata.oldclpostid = addata.adid;
                var AdDetails = await AdDetailsAPI.getaddaycareDetails(post.businessid, "1", Commonsettings.UserEmail, "Day Care");
                var optDCapi = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                    var optlist = await optDCapi.getqandalist(post.businessid, "1");
                    foreach (var data in optlist.ROW_DATA)
                    {
                        if (!string.IsNullOrEmpty(data.xml_answeredas))
                        {
                            if (data.questionid == "3" || data.questionid == "9" || data.questionid == "14")
                            {
                                post.Experience = data.xml_answeredas.Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", ""); //<answers><answer>2012</answer></answers>
                            }
                            else if (data.questionid == "8")//certification
                            {
                                //post.
                                post.certifications = data.xml_answeredas.Replace("</answer><answer>", ",").Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");
                            }
                            else if (data.questionid == "12")//experience
                            {
                                //post.
                                post.establishmentyear = data.xml_answeredas.Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");
                            }
                            else if (data.questionid == "42")
                            {
                                //response
                                post.responsetime = data.xml_answeredas.Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");
                            }
                            else if (data.questionid == "43")
                            {
                                //driver
                                post.drive = data.xml_answeredas;
                            }
                            else if (data.questionid == "44")
                            {
                                //vehicle
                                post.vehicle = data.xml_answeredas;
                            }
                            else if (data.questionid == "45")
                            {
                                //license
                                post.licenseno = data.xml_answeredas;
                            }
                            else if (data.questionid == "47")
                            {
                                //activeenrichment
                                post.activityenrichments = data.xml_answeredas.Replace("</answer><answer>", ",").Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");//<answers><answer>Role play</answer><answer>Counting activities</answer><answer>Fancy dress</answer><answer>Clay moulding</answer><answer>Outdoor games</answer><answer>Learning concepts</answer><answer>Dance</answer><answer>Hand Painting</answer></answers>
                            }
                            else if (data.questionid == "48")
                            {
                                //<answers><answer>lunch^~^12:30 am^~^2:00 am|||</answer></answers> dayschedule
                                post.availablitytable = data.xml_answeredas.Replace("</answer><answer>", ",").Replace("</answer>", "").Replace("<answer>", "");//<answers><answer>Role play</answer><answer>Counting activities</answer><answer>Fancy dress</answer><answer>Clay moulding</answer><answer>Outdoor games</answer><answer>Learning concepts</answer><answer>Dance</answer><answer>Hand Painting</answer></answers>
                            }
                            else if (data.questionid == "6")
                            {
                                post.gender = data.xml_answeredas.Replace("</answers>", "").Replace("<answers>", "").Replace("</answer>", "").Replace("<answer>", "");//<answers><answer>Role play</answer><answer>Counting activities</answer><answer>Fancy dress</answer><answer>Clay moulding</answer><answer>Outdoor games</answer><answer>Learning concepts</answer><answer>Dance</answer><answer>Hand Painting</answer></answers>
                                post.Genderid = "::" + data.questionid + "::" + data.questionorder;
                            }
                            else if (data.questionid == "30") //work experience description
                            {
                                post.Worktype = "::" + data.questionid + "::" + data.questionorder;
                                post.workdesc = data.xml_answeredas.Replace("</answers>", "").Replace("<answers>", "").Replace("</answer>", "").Replace("<answer>", "");
                            }
                        }

                    }

                    post.businessid = AdDetails.ROW_DATA[0].Businessid;
                    post.businessname = AdDetails.ROW_DATA[0].Businessname;
                    post.Businesstitleurl = AdDetails.ROW_DATA[0].Businesstitleurl;
                    post.Profiletitle = AdDetails.ROW_DATA[0].Profiletitle;
                    post.contactName = AdDetails.ROW_DATA[0].Contactname;
                    //post.Licenseno = AdDetails.ROW_DATA[0].Licenseno;
                    post.adtype = AdDetails.ROW_DATA[0].Adtype;
                    //post.Businessname = AdDetails.ROW_DATA[0].Businessname1;
                    //post.Businesstitleurl = AdDetails.ROW_DATA[0].Businesstitleurl1;
                    post.Businessaddress = AdDetails.ROW_DATA[0].Businessaddress;
                    post.zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    post.city = AdDetails.ROW_DATA[0].City;
                    post.state = AdDetails.ROW_DATA[0].State;
                    post.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    post.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    post.Campaignstarts = AdDetails.ROW_DATA[0].Campaignstarts;
                    post.Campaignend = AdDetails.ROW_DATA[0].Campaignend;
                    post.country = AdDetails.ROW_DATA[0].Country;
                    //post.pricemode = AdDetails.ROW_DATA[0].Crdate;
                    post.emailid = AdDetails.ROW_DATA[0].Email;
                    post.ContactNo = AdDetails.ROW_DATA[0].Phone;
                    //post.Phone2 = AdDetails.ROW_DATA[0].Phone2;
                    post.Education = AdDetails.ROW_DATA[0].Education;
                    //post.Experience = AdDetails.ROW_DATA[0].Experience;
                    post.Backgroundcheck = AdDetails.ROW_DATA[0].Backgroundcheck;
                    post.Salarydescription = AdDetails.ROW_DATA[0].Salarydescription;
                    post.Perkdescription = AdDetails.ROW_DATA[0].Perkdescription;
                    post.Workingfrom = AdDetails.ROW_DATA[0].Workingfrom;
                    post.Workingto = AdDetails.ROW_DATA[0].Workingto;
                    post.description = AdDetails.ROW_DATA[0].Description;
                    post.Details = AdDetails.ROW_DATA[0].Details;
                    // post.Genderid = AdDetails.ROW_DATA[0].Genderid;
                    post.Preferredworktiming = AdDetails.ROW_DATA[0].Preferredworktiming;
                    post.Staffratio = AdDetails.ROW_DATA[0].Staffratio;
                    post.Showvn = AdDetails.ROW_DATA[0].Showvn;
                    post.Showvs = AdDetails.ROW_DATA[0].Showvs;
                    post.Milestotravel = AdDetails.ROW_DATA[0].Milestotravel;
                    post.Noofkids = AdDetails.ROW_DATA[0].Noofkids;
                    post.Noofadults = AdDetails.ROW_DATA[0].Noofadults;
                    post.Noofspecialneeds = AdDetails.ROW_DATA[0].Noofspecialneeds;
                    post.Salaryrate = AdDetails.ROW_DATA[0].Salaryrate;
                    post.Perkscost = AdDetails.ROW_DATA[0].Perkscost;
                    post.premiumad = AdDetails.ROW_DATA[0].Premiumad;
                    post.availablefrom = AdDetails.ROW_DATA[0].Availablefrom;
                    post.Availableto = AdDetails.ROW_DATA[0].Availableto;
                    post.Neededdays = AdDetails.ROW_DATA[0].Neededdays;
                    post.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    post.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    //post.gender = AdDetails.ROW_DATA[0].Gender;
                    post.userpid = AdDetails.ROW_DATA[0].Userpid;
                    post.Startdate = AdDetails.ROW_DATA[0].Startdate;
                    post.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    post.Verifieddate = AdDetails.ROW_DATA[0].Verifieddate;
                    post.Status = AdDetails.ROW_DATA[0].Status;
                    post.Agegroupid = AdDetails.ROW_DATA[0].Agegroupid;
                    post.agegroups = AdDetails.ROW_DATA[0].Agegroup;
                    post.Supertag = AdDetails.ROW_DATA[0].Supertag;
                    post.supertagid = AdDetails.ROW_DATA[0].Supertagid;
                    post.primarycategoryid = AdDetails.ROW_DATA[0].Primarytagid;
                    post.Primarytag = AdDetails.ROW_DATA[0].Primarytag;
                    post.Cityurl = AdDetails.ROW_DATA[0].Cityurl;
                    post.Languageid = AdDetails.ROW_DATA[0].Languageid;
                    post.languages = AdDetails.ROW_DATA[0].Language;
                    post.Trainingid = AdDetails.ROW_DATA[0].Trainingid;
                    post.Training = AdDetails.ROW_DATA[0].Training;
                    post.Airportcode = AdDetails.ROW_DATA[0].Airportcode;
                    post.Statecode = AdDetails.ROW_DATA[0].Statecode;
                    post.Noofpersons = AdDetails.ROW_DATA[0].Noofpersons;
                    post.Neededtime = AdDetails.ROW_DATA[0].Neededtime;
                    post.Category = AdDetails.ROW_DATA[0].Category;
                    post.Statename = AdDetails.ROW_DATA[0].Statename;
                    post.Metro = AdDetails.ROW_DATA[0].Metro;
                    post.Nooflivefreeads = AdDetails.ROW_DATA[0].Nooflivefreeads;
                    post.Freeadposted = AdDetails.ROW_DATA[0].Freeadposted;
                    post.Freeliveadid = AdDetails.ROW_DATA[0].Freeliveadid;
                    post.Freeliveadtitle = AdDetails.ROW_DATA[0].Freeliveadtitle;
                    post.Freeliveadtitleurl = AdDetails.ROW_DATA[0].Freeliveadtitleurl;
                    post.Freeliveadnewcityurl = AdDetails.ROW_DATA[0].Freeliveadnewcityurl;
                    post.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                    post.Radius = AdDetails.ROW_DATA[0].Radius;
                    post.lat = AdDetails.ROW_DATA[0].Lat;
                    post.longtitude = AdDetails.ROW_DATA[0].Long;
                    post.maincategoryid = AdDetails.ROW_DATA[0].Maincategoryid;
                    post.Photoid = AdDetails.ROW_DATA[0].Photoid;
                    post.photo = AdDetails.ROW_DATA[0].Photos;
                    post.Videoid = AdDetails.ROW_DATA[0].Videoid;
                    post.videourl = AdDetails.ROW_DATA[0].Videourl;
                    post.Externalvideourl = AdDetails.ROW_DATA[0].Externalvideourl;
                    post.Britecode = AdDetails.ROW_DATA[0].Britecode;
                    post.Age = AdDetails.ROW_DATA[0].Age;
                    post.Website = AdDetails.ROW_DATA[0].Website;
                    post.Agentid = AdDetails.ROW_DATA[0].Agentid;
                    post.newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    post.Folderid = AdDetails.ROW_DATA[0].Folderid;
                    post.ordergroup = AdDetails.ROW_DATA[0].Ordergroup;
                    post.ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    post.numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                    post.Upgradeordergroup = AdDetails.ROW_DATA[0].Upgradeordergroup;
                    post.Upgradeordertype = AdDetails.ROW_DATA[0].Upgradeordertype;
                    post.Upgradenumberofdays = AdDetails.ROW_DATA[0].Upgradenumberofdays;
                    post.Alladid = AdDetails.ROW_DATA[0].Alladid;
                    post.Services = AdDetails.ROW_DATA[0].Services;
                    //post.Worktype = AdDetails.ROW_DATA[0].Worktype;
                    post.Carespecialchild = AdDetails.ROW_DATA[0].Carespecialchild;
                    post.Hideemail = AdDetails.ROW_DATA[0].Hideemail;
                    post.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    post.Classtypeid = AdDetails.ROW_DATA[0].Classtypeid;
                    post.Classtype = AdDetails.ROW_DATA[0].Classtype;
                    post.Pettypeid = AdDetails.ROW_DATA[0].Pettypeid;
                    post.Pettype = AdDetails.ROW_DATA[0].Pettype;
                    post.supercategoryid = AdDetails.ROW_DATA[0].Supercategoryid;
                    post.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    post.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid;
                    post.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    post.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                    post.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    post.tertiarycategoryid = AdDetails.ROW_DATA[0].Tertiarycategoryid;
                    post.tertiarycategoryvalue = AdDetails.ROW_DATA[0].Tertiarycategoryvalue;
                    post.countrycode = AdDetails.ROW_DATA[0].Countrycode;
                    post.Ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                    post.Contentid = AdDetails.ROW_DATA[0].Contentid;
                    post.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                    post.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    post.Agentid1 = AdDetails.ROW_DATA[0].Agentid1;
                    post.Agentstatus = AdDetails.ROW_DATA[0].Agentstatus;
                    post.Totalads = AdDetails.ROW_DATA[0].Totalads;
                    post.Adsposted = AdDetails.ROW_DATA[0].Adsposted;
                    post.Noofadslive = AdDetails.ROW_DATA[0].Noofadslive;
                    post.Noofadsremain = AdDetails.ROW_DATA[0].Noofadsremain;
                    post.Licenseno1 = AdDetails.ROW_DATA[0].Licenseno1;
                    post.Agentdisplayphone = AdDetails.ROW_DATA[0].Agentdisplayphone;
                    post.Agentcity = AdDetails.ROW_DATA[0].Agentcity;
                    post.Agentzipcode = AdDetails.ROW_DATA[0].Agentzipcode;
                    post.Agentstatecode = AdDetails.ROW_DATA[0].Agentstatecode;
                    post.Agentcountry = AdDetails.ROW_DATA[0].Agentcountry;
                    post.Agentbusinessname = AdDetails.ROW_DATA[0].Agentbusinessname;
                    post.Agentaddress = AdDetails.ROW_DATA[0].Agentaddress;
                    post.Agentphone = AdDetails.ROW_DATA[0].Agentphone;
                    post.usertype = AdDetails.ROW_DATA[0].Usertype;
                    post.Agentemail = AdDetails.ROW_DATA[0].Agentemail;
                    post.Agentphotourl = AdDetails.ROW_DATA[0].Agentphotourl;
                    post.Agentadordertype = AdDetails.ROW_DATA[0].Agentadordertype;
                    post.Packageexpiredate = AdDetails.ROW_DATA[0].Packageexpiredate;
                    post.Packageexpiredays = AdDetails.ROW_DATA[0].Packageexpiredays;
                    post.Agentmetros = AdDetails.ROW_DATA[0].Agentmetros;
                    //post.Availability = AdDetails.ROW_DATA[0].Availability;
                    post.Amount = AdDetails.ROW_DATA[0].Amount;
                    post.Discountamt = AdDetails.ROW_DATA[0].Discountamt;
                    post.Paymentamt = AdDetails.ROW_DATA[0].Paymentamt;
                    post.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                    post.addonamount = AdDetails.ROW_DATA[0].Addonamount;
                    post.Addontype = AdDetails.ROW_DATA[0].Addontype;
                    post.Categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                    post.Businessorigin = AdDetails.ROW_DATA[0].Businessorigin;
                    post.Adsourceurl = AdDetails.ROW_DATA[0].Adsourceurl;
                    post.Packageid = AdDetails.ROW_DATA[0].Packageid;
                    post.virtualnanny = AdDetails.ROW_DATA[0].Virtualnanny;
                    post.caretypes = AdDetails.ROW_DATA[0].Primarytagmobile;
                    post.categoryurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    post.otherserviceprovider = AdDetails.ROW_DATA[0].categoryvalues;
                    post.ismyneed = "1";
                    post.locationAddress = AdDetails.ROW_DATA[0].Businessaddress;
                    post.pkgid = AdDetails.ROW_DATA[0].Packageid;
                    post.salaryratemax= AdDetails.ROW_DATA[0].salaryratemax.Replace(".0", "").Replace(".00", ""); 
                    if ((post.primarycategoryid == "3" || post.primarycategoryid == "4" || post.primarycategoryid == "5") && (post.secondarycategoryid == "8" || post.secondarycategoryid == "11" || post.secondarycategoryid == "13") && post.supercategoryid == "1")
                    {
                        post.maincategoryid = "1";
                    }
                    else if ((post.primarycategoryid == "4" || post.primarycategoryid == "3") && (post.secondarycategoryid == "12" || post.secondarycategoryid == "9") && post.supercategoryid == "1")
                    {
                        post.maincategoryid = "2";
                    }
                    else if (post.supercategoryid == "2" && post.primarycategoryid == "0" && post.secondarycategoryid == "0" && post.tertiarycategoryid == "0")
                    {
                        post.maincategoryid = "3";
                    }
                    else
                    {
                        post.maincategoryid = "0";
                    }

                    //await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Catfist(post));
                    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostFirst(post));
                //}
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void checksummercamp()
        {
            try
            {
                if (summercampID == 0)
                {
                    summercampImg = "CheckBoxChecked.png";
                    summercampID = 1;
                   // summercamp = "1";
                    var currentpg = GetCurrentPage();
                    var packgamt = currentpg.FindByName<Label>("packamt");
                    packgamt.Text =  packageamount.Replace(".0", ".00");
                    var totalamt = currentpg.FindByName<Label>("totalamt");
                    decimal payamnt;
                    if (!string.IsNullOrEmpty(discountamount))
                    {
                        payamnt = (Convert.ToDecimal(totalamount.Replace("$", "")) - Convert.ToDecimal(discountamount.Replace("$", ""))) + Convert.ToDecimal(post.addonamount.Replace("$", ""));
                    }
                    else
                    {
                        payamnt = Convert.ToDecimal(totalamount.Replace("$", "")) + Convert.ToDecimal(post.addonamount.Replace("$", ""));
                    }
                    // totalamount = payamnt.ToString("N");
                    if (removecouponvisible == true)
                    {
                        applycouponcode();
                    }
                    totalamt.Text = "$ " + payamnt;
                }
                else
                {
                    summercampImg = "CheckBoxUnChecked.png";
                    summercampID = 0;
                    //summercamp = "0";
                    var currentpg = GetCurrentPage();
                    var packgamt = currentpg.FindByName<Label>("packamt");
                    packgamt.Text =  packageamount.Replace(".0", ".00");
                    var totalamt = currentpg.FindByName<Label>("totalamt");
                    decimal payamnt;
                    if (!string.IsNullOrEmpty(discountamount))
                    {
                        payamnt = Convert.ToDecimal(totalamount.Replace("$", "")) - Convert.ToDecimal(discountamount.Replace("$", ""));
                    }
                    else
                    {
                        payamnt = Convert.ToDecimal(totalamount.Replace("$", "")) ;
                    }
                   // totalamount = payamnt.ToString("N");
                    totalamt.Text = "$ " + payamnt;
                    if (removecouponvisible == true)
                    {
                        applycouponcode();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async void applycouponcode()
        {
            try
            {
                if (string.IsNullOrEmpty(couponcodetxt))
                {
                    dialog.Toast("Enter your coupon code");
                }
                else
                {
                    dialog.ShowLoading("");
                    var couponapi = RestService.For<IDCpayment>(Commonsettings.DaycareAPI);
                   
                    //  var details = await couponapi.applycouponcode(addid, couponcodetxt, Sordertype, citycount.ToString());
                   
                    var details = await couponapi.applycouponcode(addid, couponcodetxt, ordergroup,post.numberofdays, summercampID.ToString());
                    if (details != null)
                    {
                        dialog.HideLoading();
                        if (details.resultinformation == "invalid")
                        {
                            dialog.Toast("Enter a valid coupon");
                        }
                        else if(details.resultinformation == "notapplied")
                        {
                            dialog.Toast("Coupon not applied");
                        }
                        else
                        {
                            couponvisible = false;
                            removecouponvisible = true;
                            appliedcoupondtl = true;
                            Scouponcode = couponcodetxt;
                            var currentpg = GetCurrentPage();
                            var packgamt = currentpg.FindByName<Label>("packamt");
                            packgamt.Text = packageamount.Replace(".0", ".00");
                            var totalamt = currentpg.FindByName<Label>("totalamt");
                            decimal payamnt;
                            if (summercampID==1)
                            {
                                payamnt = Convert.ToDecimal(totalamount.Replace("$", "")) - Convert.ToDecimal(details.discountamount) + Convert.ToDecimal(post.addonamount.Replace("$", ""));
                            }
                            else
                            {
                                payamnt = Convert.ToDecimal(totalamount.Replace("$", "")) - Convert.ToDecimal(details.discountamount);
                            }
                           // totalamount = payamnt.ToString("N");
                            totalamt.Text = payamnt.ToString("N");
                            var discntamt = currentpg.FindByName<Label>("discntamt");
                            decimal discamnt = Convert.ToDecimal(details.discountamount);
                            discountamount = discamnt.ToString("N");
                            discntamt.Text = "$ " + discountamount;
                        }
                    }
                    else
                        dialog.HideLoading();
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void removecoupon()
        {
            try
            {
                couponvisible = true;
                removecouponvisible = false;
                appliedcoupondtl = false;
                decimal Totalamt;
                var currentpage = GetCurrentPage();
                var totalamt = currentpage.FindByName<Label>("totalamt");
               
                    Scouponcode = "";
                if(summercampID==1)
                {
                    Totalamt = Convert.ToDecimal(totalamount.Replace("$", "")) + Convert.ToDecimal(post.addonamount.Replace("$", ""));
                }
                else
                {
                    Totalamt = Convert.ToDecimal(totalamount.Replace("$", ""));
                }
                discountamount = "";
                    totalamt.Text = "$ " + Totalamt.ToString("N");
                    // PaymentDC_VM.totalamount = totalamount.Replace("$", "");
                
            }
            catch(Exception ex)
            {

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
                LocationRowData response = await LocationAPI.getlocation(LocationName);
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
            try
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
            catch (Exception ex)
            {

            }
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
            expiryyearID = payment_Params.years;
            contentviewvisible = false;
        }
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        string orderid = "", shopingcartid = "";

        public async void paymentfirst ()
        {
            var currpage = GetCurrentPage();
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

                    NRIApp.LocalJobs.Features.Posting.Models.JobPaymentdetails pmnt = new NRIApp.LocalJobs.Features.Posting.Models.JobPaymentdetails()
                    {
                        adcity = post.city,
                        adcountry = post.country,
                        adlat = post.lat.ToString(),
                        adlong = post.longtitude.ToString(),
                        adstate = post.state,
                        adstatecode = post.state,
                        adzipcode = post.zipcode,
                        ademail = post.emailid,
                        adphone = post.ContactNo,
                        adname = post.contactName,
                        //nationwide = data.isnationwidechk,
                        //bannerstatus = data.isbannerchk,
                        categoryflag = post.categoryflag,
                        //newly added 
                        //banneramnt = topbannerchkamountt.ToString(),
                        //emailblastamount = exclusivemaileramountt.ToString(),
                        //addonamount =post.addonamount,
                        //bonusdays = bonusdayID,
                        //nationwideamnt = nationwideamount,
                        addedcities = cities,
                        // citycnt = citycount,
                        ordertype = Sordertype,
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
                        usertype = post.usertype,
                        postedviaid = sPostedviaid,
                        couponcode = Scouponcode,

                        totalamount = PaymentDC_VM.totalamount.Replace("$", ""),
                        ip = ipaddress,
                        ordergroup = post.ordergroup,
                        noofdays = post.numberofdays,
                        summercamp = summercampID.ToString(),
                        discountamt = discountamount.Replace("$", ""),
                        mode =post.mode,
                        pkgid=post.pkgid
                    };
                    if (!string.IsNullOrEmpty(discountamount))
                    {
                        pmnt.discountamt = Convert.ToDecimal(discountamount.Replace("$", "")).ToString();
                    }
                    if(summercampID==1)
                    {
                        pmnt.addonamount = post.addonamount;
                    }
                 
                    var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    NRIApp.LocalJobs.Features.Posting.Models.JobPayment details = await DCpostAPI.paymentpostsecondsubmit(pmnt);
                  
                   if(details.errormsg== "success")
                    {
                        await currpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.OrderReceipt(pmnt.adid, pmnt.noofdays,post.ordertype));
                    }
                    else
                    {
                        dialog.HideLoading();
                        dialog.Toast(details.errormsg.ToString());
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
