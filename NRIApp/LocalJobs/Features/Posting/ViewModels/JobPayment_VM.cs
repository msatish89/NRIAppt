using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using System.Windows.Input;
using Refit;
using NRIApp.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.Interface;
using Plugin.Connectivity;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    class JobPayment_VM : INotifyPropertyChanged
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
        public ICommand makechangesTap { get; set; }

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
            //new Payment_Params{years=2019,checkimage="RadioButtonUnChecked.png"},
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
      
        private string _validitydays="";
        public string validitydays
        {
            get { return _validitydays; }
            set { _validitydays = value; OnPropertyChanged(nameof(validitydays)); }
        }
        private string _selectedpackagename="";
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
        private decimal _topbannerchkamountt;
        public decimal topbannerchkamountt
        {
            get { return _topbannerchkamountt; }
            set
            {
                _topbannerchkamountt = value;
                OnPropertyChanged(nameof(topbannerchkamountt));
            }
        }
        private decimal _exclusivemaileramountt;
        public decimal exclusivemaileramountt
        {
            get { return _exclusivemaileramountt; }
            set
            {
                _exclusivemaileramountt = value;
                OnPropertyChanged(nameof(exclusivemaileramountt));
            }
        }
        private decimal _jobslotamountt;
        public decimal jobslotamountt
        {
            get { return _jobslotamountt; }
            set
            {
                _jobslotamountt = value;
                OnPropertyChanged(nameof(jobslotamountt));
            }
        }
        private decimal _bonusdayamountt;
        public decimal bonusdayamountt
        {
            get { return _bonusdayamountt; }
            set
            {
                _bonusdayamountt = value;
                OnPropertyChanged(nameof(bonusdayamountt));
            }
        }
        private string _topbannerchkamounttxt;
        public string topbannerchkamounttxt
        {
            get { return _topbannerchkamounttxt; }
            set
            {
                _topbannerchkamounttxt = value;
                OnPropertyChanged(topbannerchkamounttxt);
            }
        }
        private string _exclusivemaileramounttxt;
        public string exclusivemaileramounttxt
        {
            get { return _exclusivemaileramounttxt; }
            set
            {
                _exclusivemaileramounttxt = value;
                OnPropertyChanged(exclusivemaileramounttxt);
            }
        }
        private string _jobslotamounttxt;
        public string jobslotamounttxt
        {
            get { return _jobslotamounttxt; }
            set
            {
                _jobslotamounttxt = value;
                OnPropertyChanged(jobslotamounttxt);
            }
        }
        private string _bonusdayamounttxt;
        public string bonusdayamounttxt
        {
            get { return _bonusdayamounttxt; }
            set
            {
                _bonusdayamounttxt = value;
                OnPropertyChanged(bonusdayamounttxt);
            }
        }
        private string _bonusdayID;
        public string bonusdayID
        {
            get { return _bonusdayID; }
            set
            {
                _bonusdayID = value;
                OnPropertyChanged(bonusdayID);
            }
        }
        private bool _topbannerchkamounttvisible=false;
        public bool topbannerchkamounttvisible
        {
            get { return _topbannerchkamounttvisible; }
            set
            {
                _topbannerchkamounttvisible = value;
                OnPropertyChanged(nameof(topbannerchkamounttvisible));
            }
        }
        private bool _exclusivemaileramounttvisible=false;
        public bool exclusivemaileramounttvisible
        {
            get { return _exclusivemaileramounttvisible; }
            set
            {
                _exclusivemaileramounttvisible = value;
                OnPropertyChanged(nameof(exclusivemaileramounttvisible));
            }
        }
        private bool _jobslotamounttvisible=false;
        public bool jobslotamounttvisible
        {
            get { return _jobslotamounttvisible; }
            set
            {
                _jobslotamounttvisible = value;
                OnPropertyChanged(nameof(jobslotamounttvisible));
            }
        }
        private bool _bonusdayamounttvisible=false;
        public bool bonusdayamounttvisible
        {
            get { return _bonusdayamounttvisible; }
            set
            {
                _bonusdayamounttvisible = value;
                OnPropertyChanged(nameof(bonusdayamounttvisible));
            }
        }
        private bool _bonusdaycntvisible = false;
        public bool bonusdaycntvisible
        {
            get { return _bonusdaycntvisible; }
            set
            {
                _bonusdaycntvisible = value;
                OnPropertyChanged(nameof(bonusdaycntvisible));
            }
        }
        private string _bonusdaycount;
        public string bonusdaycount
        {
            get { return _bonusdaycount; }
            set
            {
                _bonusdaycount = value;
                OnPropertyChanged(bonusdaycount);
            }
        }
        private string _appcouponcodetxt="";
        public string appcouponcodetxt
        {
            get { return _appcouponcodetxt; }
            set
            {
                _appcouponcodetxt = value;
                OnPropertyChanged(appcouponcodetxt);
            }
        }
        private bool _appcouponcodevisible = false;
        public bool appcouponcodevisible
        {
            get { return _appcouponcodevisible; }
            set
            {
                _appcouponcodevisible = value;
                OnPropertyChanged(nameof(appcouponcodevisible));
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
        private static string amnt = "", addid = "", usertype = "1", Sordertype = "", Scouponcode = "";
        public static string packageamount, discountamount, totalamount, banneramount="", nationwideamount="",cities="";
        public static int citycount = 0;
        Postingdata data = new Postingdata();

        public JobPayment_VM(string amount,  string nationwideamnt, int citycnt, string ordertype, string totalamnt,string percityamnt, Postingdata pst, decimal topbannerchk, decimal exclusivemailerchk, decimal jobslotchk, decimal bonusdaychk,string daycnt)
        {
           try
            {
                if (topbannerchk != 0)
                {
                    topbannerchkamounttxt = "$ " + topbannerchk.ToString("N");
                    topbannerchkamounttvisible = true;
                }
                if (exclusivemailerchk != 0)
                {
                    exclusivemaileramounttxt = "$ " + exclusivemailerchk.ToString("N");
                    exclusivemaileramounttvisible = true;
                }
                if (jobslotchk != 0)
                {
                    jobslotamounttxt = "$ " + jobslotchk.ToString("N");
                    jobslotamounttvisible = true;
                }
                if (bonusdaychk != 0)
                {
                    bonusdayamounttxt = "$ " + bonusdaychk.ToString("N");
                    bonusdayamounttvisible = true;
                }
                if(!string.IsNullOrEmpty(daycnt)&&daycnt!="0"&&daycnt.Trim().Length>0)
                {
                    bonusdaycount = daycnt +" days";
                    bonusdaycntvisible = true;
                }
                else
                {
                    bonusdaycntvisible = false;
                }

                topbannerchkamountt = topbannerchk;
                exclusivemaileramountt = exclusivemailerchk;
                jobslotamountt = jobslotchk;
                bonusdayamountt = bonusdaychk;
                bonusdayID = daycnt;

                Scouponcode = ""; totalamount = "";
                data = pst;
                amnt = amount;
                addid = pst.adid;
                totalamount = totalamnt;
                packageamount = amount;
                banneramount = topbannerchkamountt.ToString();
                citycount = citycnt;
                nationwideamount = nationwideamnt;
                billingname = pst.username;
                if(string.IsNullOrEmpty(billingname))
                {
                    billingname = Commonsettings.UserName;
                }
                billingemail = pst.useremail;
                billingmobileno = pst.usermobileno;
                Sordertype = ordertype;
                Selectcountry = pst.contrycode;
                if(!Selectcountry.Contains("+"))
                {
                    Selectcountry ="+"+ pst.contrycode;
                }
                TempLocationName = pst.city + ", " + pst.statecode;
                LocationName = pst.city + ", " + pst.statecode;
                city = pst.city;
                zipcode = pst.zipcode;
                country = pst.country;
                statecode = pst.statecode;
                TempLocationAddress = pst.bizaddress;
                LocationAddress = pst.bizaddress;
                Expirymonthlistdata = Expirymonthlist;
                Expiryyearlistdata = Expiryyearlistdata;
                //ordersummary
                selectedpackagename = char.ToUpper(ordertype[0]) + ordertype.Substring(1);
                validitydays = pst.validitydays;
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
                // getcouponcodetxt();
                if (!string.IsNullOrEmpty(pst.appcouponcode))
                {
                    appcouponcodevisible = true;
                }
                if (citycnt > 0)
                    Bindcities();

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }

        }
        //public async void getcouponcodetxt()
        //{
        //    try
        //    {
        //        var couponapi = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
        //        var details = await couponapi.Getcouponcode();
        //        if(details!=null)
        //        {
        //            appcouponcodevisible = true;
        //            appcouponcodetxt = "Coupon code: "+details.resultinformation;
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}
        public void Editoption()
        {
            try
            {
                var currentpage = GetCurrentPage();
                data.ismyneed = "1";
                data.mode = "makechange";
                currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobtype(data));
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
                    var couponapi = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                    //if (data.isbannerchk != "1")
                    //    banneramount = "0";
                    if (data.isnationwidechk != "1")
                        nationwideamount = "0";
                    var details = await couponapi.applycouponcode(addid, couponcodetxt.Trim(), Sordertype, citycount.ToString(), nationwideamount, topbannerchkamountt,jobslotamountt,exclusivemaileramountt, bonusdayID);
                    if (details != null)
                    {
                        dialog.HideLoading();
                        if (details.resultinformation == "invalid")
                        {
                            Scouponcode = "";
                            couponcodetxt = "";
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
                            packgamt.Text = "$" + packageamount.Replace(".0", ".00");
                            var totalamt = currentpg.FindByName<Label>("totalamt");
                            decimal payamnt = Convert.ToDecimal(totalamount.Replace("$", "")) - Convert.ToDecimal(details.discountamount);
                            totalamount = payamnt.ToString("N");
                            totalamt.Text = "$ " + totalamount;
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
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void removecoupon()
        {
           try
            {
                dialog.ShowLoading("", null);
                couponvisible = true;
                removecouponvisible = false;
                appliedcoupondtl = false;
                decimal Totalamt;
                var currentpage = GetCurrentPage();
                var totalamt = currentpage.FindByName<Label>("totalamt");
                if (!string.IsNullOrEmpty(totalamount) && !string.IsNullOrEmpty(discountamount))
                {
                    Scouponcode = "";
                    couponcodetxt = "";
                    Totalamt = Convert.ToDecimal(totalamount) + Convert.ToDecimal(discountamount);
                    totalamt.Text = "$ " + Totalamt.ToString("N");
                    JobPayment_VM.totalamount = Totalamt.ToString();
                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
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
            catch(Exception ex)
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

        public async void paymentfirst()
        {
            var currpage = GetCurrentPage();
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                if(connected==true)
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
                        JobPaymentdetails pmnt = new JobPaymentdetails()
                        {
                            adcity = data.city,
                            adcountry = data.country,
                            adlat = data.lat,
                            adlong = data.longitude,
                            adstate = data.state,
                            adstatecode = data.statecode,
                            adzipcode = data.zipcode,
                            ademail = data.useremail,
                            adphone = data.usermobileno,
                            adname = data.username,
                            nationwide = data.isnationwidechk,
                            bannerstatus = data.isbannerchk,
                            categoryflag = data.categoryflag,
                            //newly added 
                            banneramnt = topbannerchkamountt.ToString(),
                            emailblastamount = exclusivemaileramountt.ToString(),
                            addonamount = jobslotamountt.ToString(),
                            bonusdays = bonusdayID,
                            nationwideamnt = nationwideamount,
                            addedcities = cities,
                            citycnt = citycount,
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
                            usertype = data.usertype,
                            postedviaid = sPostedviaid,
                            couponcode = Scouponcode,
                            totalamount = JobPayment_VM.totalamount.Replace("$", ""),
                            ip = ipaddress,
                            mode=data.mode
                        };
                       
                        if (data.mode=="renew" || data.mode == "completepayment")
                        {
                            if(string.IsNullOrEmpty(data.username))
                            {
                                data.username = Commonsettings.UserName;
                                data.userpid = Commonsettings.UserPid;
                                data.ip = ipaddress;
                            }
                            var baseApii = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                            postingsbmtdetails dtails = await baseApii.Jobsposting(data);
                            postingsbmtdetails detls = await baseApii.Jobspostingsecondstep(data);
                            //if (detls.blockstatus == "1")
                            //{

                            //    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                            //}
                            //else
                            //{
                            //    if (detls.usertype == "2")
                            //    {
                            //        await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.JobSuccess(data.adid, details.usertype));
                            //        //await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                            //    }
                            //    else
                            //    {
                            //            if (detls.otpsend == "otpsend")
                            //            {
                            //                Selectcountry = detls.countrycode;
                            //                dialog.HideLoading();
                            //                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.OTPform(detls.otpno, data.adid, data.usermobileno, Selectcountry, data));
                            //            }
                            //            else
                            //            {
                            //                //dialog.HideLoading();
                            //                if (detls.ispayment == "1")
                            //                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Packages(data));
                            //                else
                            //                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                            //            }
                                    
                            //    }
                            //}
                        }

                        var baseApi = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                        JobPayment details = await baseApi.Saveshoppingcart(pmnt);
                        orderid = details.ordernumber.ToString();
                        shopingcartid = details.shopingcartid.ToString();
                        if (details.errormsg.Length > 0)
                        {
                            dialog.HideLoading();
                            dialog.Toast(details.errormsg.ToString());
                        }
                        else
                        {
                            await currpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.JobSuccess(pmnt.adid, usertype));
                        }
                    }
                   
                }
                else
                {
                    dialog.Toast("Kindly check your internet connection!");
                }

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                if (connected == false)
                { dialog.Toast("Kindly check your internet connection!"); }
                else
                { dialog.Toast("Payment failed."); }
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

        public void Bindcities()
        {
           try
            {
                foreach (var item in JobPackage_VM.addionalcities)
                {
                    if (cities == "")
                        cities = item.Value;
                    else
                        cities = cities + "," + item.Value;
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
    }


}
