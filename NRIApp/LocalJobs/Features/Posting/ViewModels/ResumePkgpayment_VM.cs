using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.Post.Models;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using NRIApp.Roommates.Features.Post.Interface;
using System.Runtime.CompilerServices;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    public class ResumePkgpayment_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;
        public static string expirymonthID;
        public static int expiryyearID;
        public Command TaponProceed { get; set; }
        public List<Payment_Params> Expirymonthlistdata { get; set; }
        public List<Payment_Params> Expiryyearlistdata { get; set; }
        public Command TaponExpirymonth { get; set; }
        public Command TaponExpiryyear { get; set; }
        public Command selectexpirymonth { get; set; }
        public Command selectexpiryyear { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command Taponcvvimg { get; set; }
        public Command openbilling { get; set; }
        public Command ApplyCouponCode { get; set; }
        public Command removecouponcmd { get; set; }
        public Command<Location> LocationCmd { get; set; }
        public Command Selectgoogleaddress { get; set; }
        public Command<string> TapOnStarterPackage { get; set; }
        public Command<string> TapOnbasicPackage { get; set; }
        public Command TapCustomPackage { get; set; }
        public Command<string> TapOnStandardPackage { get; set; }

        public string _standardpkgdays = "180";
        public string _standardpkgamnt = "$ 1500";
        public string _standardpkgads = "150";
        public string _standardpkgresumecnt = "1500";
        public bool _standardpkgbtn = true;
        public bool _selstandardpkgbtn = false;
        public bool _standardpkg = false;

        public string _basicpkgdays = "90";
        public string _basicpkgamnt = "$ 800";
        public string _basicpkgads = "65";
        public string _basicpkgresumecnt = "750";
        public bool _basicbtn = true;
        public bool _selbasicbtn = false;
        public bool _basicpkg = false;

        public string _starterpkgdays = "60";
        public string _starterpkgamnt = "$ 425";
        public string _starterpkgads = "30";
        public string _starterpkgresumecnt = "400";
        public bool _starterbtn = true;
        public bool _selstarterbtn = false;
        public bool _starterpkg = false;

        public string _custompkgdays = "0 Days";
        public string _custompkgamnt = "$ 0";
        public string _custompkgads = "";
        public string _custompkgresumecnt = "";
        public bool _custombtn = true;
        public bool _selcustombtn = false;
        Resumepkgdetails data = new Resumepkgdetails();
        public static string discountamount = "", totalamount = "", Scouponcode = "";
        public ResumePkgpayment_VM(Resumepkgdetails pkgdata, string page)
        {
            data = pkgdata;
            TapOnStarterPackage = new Command<string>(SelectStarterpkg);
            TapOnbasicPackage = new Command<string>(Selectbasicpkg);
            TapCustomPackage = new Command(Selectcustompkg);
            TapOnStandardPackage = new Command<string>(SelectStandpkg);
            if (page == "package")
                GetJobpackages();
            else
            {
                totalamount = data.totalamount;
                billingname = pkgdata.name;
                billingemail = pkgdata.email;
                billingmobileno = pkgdata.phoneno;
                Selectcountry = pkgdata.countrycode;
                TempLocationName = pkgdata.city + ", " + pkgdata.statecode;
                LocationName = pkgdata.city + ", " + pkgdata.statecode;
                city = pkgdata.city;
                zipcode = pkgdata.zipcode;
                country = pkgdata.country;
                statecode = pkgdata.statecode;
                TempLocationAddress = pkgdata.address;
                LocationAddress = pkgdata.address;
                Expirymonthlistdata = Expirymonthlist;
                Expiryyearlistdata = Expiryyearlistdata;

                Taponcvvimg = new Command(showcvvimg);
                PopupContentTap = new Command(showcontentview);
                ContentViewTap = new Command(Closecontentview);

                TaponExpiryyear = new Command(showexpiryyearlist);
                TaponExpirymonth = new Command(showexpirymonthlist);
                selectexpirymonth = new Command<Payment_Params>(selectexpirymonthvalue);
                selectexpiryyear = new Command<Payment_Params>(selectexpiryyearvalue);
                LocationCmd = new Command<Location>(OnLocationClick);
                Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
                openbilling = new Command(openbillingdeatils);
                removecouponcmd = new Command(removecoupon);
                ApplyCouponCode = new Command(applycouponcode);
                TaponProceed = new Command(Addpayment);
                getcouponcode();
            }
        }
        private string _couponcodetext = "";
        public string couponcodetext
        {
            get { return _couponcodetext; }
            set { _couponcodetext = value;OnPropertyChanged(nameof(couponcodetext)); }
        }

        private bool _couponcodetextvisible = false;
        public bool couponcodetextvisible
        {
            get { return _couponcodetextvisible; }
            set { _couponcodetextvisible = value;OnPropertyChanged(nameof(couponcodetextvisible)); }
        }
        public async void getcouponcode()
        {
            try
            {
                _Dialog.ShowLoading();
                var getcode=RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                Jobseeker.Models.Resultinformation list = await getcode.Getcouponcode();
                if(!string.IsNullOrEmpty(list.resultinformation))
                {
                    couponcodetextvisible = true;
                    couponcodetext = "Coupon code : " + list.resultinformation;//Coupon code: NEWSUL10
                }
                _Dialog.HideLoading();
            }
            catch(Exception ex)
            {
                _Dialog.HideLoading();
            }
        }
        public async void GetJobpackages()
        {
           try
            {
                _Dialog.ShowLoading("");
                var api = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                ResumePackagelist list = await api.Getresumepackages();
                Pack = list.ROW_DATA;
                if (list.ROW_DATA.Count() > 0)
                {
                    foreach (var i in Pack)
                    {
                        if (i.packagename == "Standard")
                        {
                            standardpkgdays = i.noofdays + " days";
                            standardpkgamnt = "$" + i.amount.Replace(".0", "");
                            standardpkgresumecnt = i.ordertype;
                            standardpkgads = i.numberofliveads;
                            standardpkgvisible = true;
                        }
                        else if (i.packagename == "Basic")
                        {
                            basicpkgamnt = "$" + i.amount.Replace(".0", "");
                            basicpkgdays = i.noofdays + " days";
                            basicpkgresumecnt = i.ordertype;
                            basicpkgads = i.numberofliveads;
                            basicpkgvisible = true;
                        }
                        else if (i.packagename == "Starter")
                        {
                            starterpkgamnt = "$" + i.amount.Replace(".0", "");
                            starterpkgdays = i.noofdays + " days";
                            starterpkgresumecnt = i.ordertype;
                            starterpkgads = i.numberofliveads;
                            starterpkgvisible = true; 
                        }
                    }
                    _Dialog.HideLoading();
                }
                else
                    _Dialog.HideLoading();
            }
            catch(Exception ex)
            {
                _Dialog.HideLoading();
            }
        }

        public List<ResumePackages> _pack { get; set; }
        public List<ResumePackages> Pack
        {
            get
            {
                return _pack;
            }
            set
            {
                _pack = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pack)));
            }
        }
        public bool standardpkgvisible
        {
            get { return _standardpkg; }
            set
            {
                _standardpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgvisible)));

            }
        }

        public bool basicpkgvisible
        {
            get { return _basicpkg; }
            set
            {
                _basicpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgvisible)));

            }
        }
        public bool starterpkgvisible
        {
            get { return _starterpkg; }
            set
            {
                _starterpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgvisible)));

            }
        }

        public bool custompkgbtn
        {
            get { return _custombtn; }
            set
            {
                _custombtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(custompkgbtn)));

            }
        }
        public bool Selcustombtn
        {
            get { return _selcustombtn; }
            set
            {
                _selcustombtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selcustombtn)));

            }
        }

        public bool standardpkgbtn
        {
            get { return _standardpkgbtn; }
            set
            {
                _standardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgbtn)));

            }
        }
        public bool Selstandardpkgbtn
        {
            get { return _selstandardpkgbtn; }
            set
            {
                _selstandardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selstandardpkgbtn)));

            }
        }
        public bool basicbtn
        {
            get { return _basicbtn; }
            set
            {
                _basicbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicbtn)));

            }
        }
        public bool Selbasicbtn
        {
            get { return _selbasicbtn; }
            set
            {
                _selbasicbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selbasicbtn)));

            }
        }
        public bool starterbtn
        {
            get { return _starterbtn; }
            set
            {
                _starterbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterbtn)));

            }
        }
        public bool Selstarterbtn
        {
            get { return _selstarterbtn; }
            set
            {
                _selstarterbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selstarterbtn)));

            }
        }

        public string Custompkgdays
        {
            get { return _custompkgdays; }
            set
            {
                _custompkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Custompkgdays)));

            }
        }
        public string custompkgamnt
        {
            get { return _custompkgamnt; }
            set
            {
                _custompkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(custompkgamnt)));

            }
        }
        
        
        public string standardpkgdays
        {
            get { return _standardpkgdays; }
            set
            {
                _standardpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgdays)));

            }
        }
        public string standardpkgamnt
        {
            get { return _standardpkgamnt; }
            set
            {
                _standardpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgamnt)));

            }
        }

        public string basicpkgdays
        {
            get { return _basicpkgdays; }
            set
            {
                _basicpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgdays)));

            }
        }
        public string basicpkgamnt
        {
            get { return _basicpkgamnt; }
            set
            {
                _basicpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgamnt)));

            }
        }
        public string starterpkgdays
        {
            get { return _starterpkgdays; }
            set
            {
                _starterpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgdays)));

            }
        }
        public string starterpkgamnt
        {
            get { return _starterpkgamnt; }
            set
            {
                _starterpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgamnt)));

            }
        }

        public string Custompkgads
        {
            get { return _custompkgads; }
            set
            {
                _custompkgads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Custompkgads)));

            }
        }

        public string starterpkgads
        {
            get { return _starterpkgads; }
            set
            {
                _starterpkgads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgads)));

            }
        }
        public string standardpkgads
        {
            get { return _standardpkgads; }
            set
            {
                _standardpkgads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgads)));

            }
        }
        public string basicpkgads
        {
            get { return _basicpkgads; }
            set
            {
                _basicpkgads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgads)));

            }
        }
        public string Custompkgresumecnt
        {
            get { return _custompkgresumecnt; }
            set
            {
                _custompkgresumecnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Custompkgresumecnt)));

            }
        }
        public string basicpkgresumecnt
        {
            get { return _basicpkgresumecnt; }
            set
            {
                _basicpkgresumecnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgresumecnt)));

            }
        }
        public string standardpkgresumecnt
        {
            get { return _standardpkgresumecnt; }
            set
            {
                _standardpkgresumecnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgresumecnt)));

            }
        }
        public string starterpkgresumecnt
        {
            get { return _starterpkgresumecnt; }
            set
            {
                _starterpkgresumecnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgresumecnt)));

            }
        }

        public string _standardpkgname = "";
        public string standardpkgname
        {
            get { return _standardpkgname; }
            set
            {
                _standardpkgname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgname)));

            }
        }
        public string _starterdpkgname = "";
        public string starterdpkgname
        {
            get { return _starterdpkgname; }
            set
            {
                _starterdpkgname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterdpkgname)));
            }
        }

        public string _basicpkgname = "";
        public string basicpkgname
        {
            get { return _basicpkgname; }
            set
            {
                _basicpkgname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgname)));
            }
        }

        public async void Selectbasicpkg(string amnnt)
        {
            var curpage = GetCurrentPage();
            _Dialog.ShowLoading("");
            basicbtn = false;
            Selbasicbtn = true;
            custompkgbtn = true;
            Selcustombtn = false;
            starterbtn = true;
            Selstarterbtn = false;
            standardpkgbtn = true;
            Selstandardpkgbtn = false;
            data.totaladscount = basicpkgads;
            data.iscustompackage = "0";
            data.totalresume = basicpkgresumecnt;
            data.totalamount = basicpkgamnt;
            data.pkgname = "Basic";
            var api = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
            JobResult list = await api.Insertresumepkgdetails(data);
            if (list.agentid != "")
            {
                data.agentid = list.agentid;
                _Dialog.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepayment(data));
            }
            else
            {
                _Dialog.HideLoading();
            }


        }
        public async void SelectStandpkg(string amnnt)
        {
            var curpage = GetCurrentPage();
            _Dialog.ShowLoading("");
            basicbtn = true;
            Selbasicbtn = false;
            custompkgbtn = true;
            Selcustombtn = false;
            starterbtn = true;
            Selstarterbtn = false;
            standardpkgbtn = false;
            Selstandardpkgbtn = true;
            data.totaladscount = standardpkgads;
            data.totalresume = standardpkgresumecnt;
            data.totalamount = standardpkgamnt;
            data.iscustompackage = "0";
            data.pkgname = "Standard";
            var api = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
            JobResult list = await api.Insertresumepkgdetails(data);
            if (list.agentid != "")
            {
                data.agentid = list.agentid;
                _Dialog.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepayment(data));
            }
            else
            {
                _Dialog.HideLoading();
            }
        }
        public async void SelectStarterpkg(string amnnt)
        {
            var curpage = GetCurrentPage();

            _Dialog.ShowLoading("");

            basicbtn = true;
            Selbasicbtn = false;
            custompkgbtn = true;
            Selcustombtn = false;
            starterbtn = false;
            Selstarterbtn = true;
            standardpkgbtn = true;
            Selstandardpkgbtn = false;
            data.totaladscount = starterpkgads;
            data.totalresume = starterpkgresumecnt;
            data.totalamount = starterpkgamnt;
            data.iscustompackage = "0";
            data.pkgname = "Starter";
            var api = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
            JobResult list = await api.Insertresumepkgdetails(data);
            if (list.agentid != "")
            {
                data.agentid = list.agentid;
                _Dialog.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepayment(data));
            }
            else
            {
                _Dialog.HideLoading();

            }
        }
        //private string _custompkgamt = "";
        //public string custompkgamt
        //{
        //    get { return Custompkgdays}
        //}
        public async void Selectcustompkg()
        {
            try
            {
                var curpage = GetCurrentPage();
                if (Custompkgvalidation())
                {
                    data.iscustompackage = "1";
                    _Dialog.ShowLoading("");
                    basicbtn = true;
                    Selbasicbtn = false;
                    custompkgbtn = false;
                    Selcustombtn = true;
                    starterbtn = true;
                    Selstarterbtn = false;
                    standardpkgbtn = true;
                    Selstandardpkgbtn = false;
                    data.totaladscount = Custompkgads;
                    data.totalresume = Custompkgresumecnt;
                    data.totalamount = "";
                    data.pkgname = "";
                    var api = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                    JobResult list = await api.Insertresumepkgdetails(data);
                    if (list.agentid != "")
                    {
                        data.agentid = list.agentid;
                        _Dialog.HideLoading();
                        data.totalamount = list.pkgamount;
                        custompkgamnt = "$" + list.pkgamount.Replace(".00", "");
                        Custompkgdays = list.noofdays + " Days";
                        await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepayment(data));
                    }
                    else
                    {
                        _Dialog.HideLoading();

                    }
                }
            }
            catch(Exception ex)
            {
                _Dialog.HideLoading();
            }
        }

        public bool Custompkgvalidation()
        {
            bool result = true;
            if (string.IsNullOrEmpty(Custompkgads) || Custompkgads.Trim().Length==0)
            {
                _Dialog.Toast("Please enter Ads Count");
                return false;
            }
            if (!string.IsNullOrEmpty(Custompkgads))
            {
                if (Custompkgads.Contains(".") || Custompkgads.Contains("-"))
                {
                    _Dialog.Toast("Ads Count should not contains any special characters");
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(Custompkgads))
            {
                if (Convert.ToInt32(Custompkgads) < 25)
                {
                    _Dialog.Toast("Ads Count Should be more than 25");
                    return false;
                }
                else if (Convert.ToInt32(Custompkgads) >500)
                {
                    _Dialog.Toast("Ads Count Should be less than 500");
                    return false;
                }
            }

            
            if (string.IsNullOrEmpty(Custompkgresumecnt) || Custompkgresumecnt.Trim().Length == 0)
            {
                _Dialog.Toast("Please enter Resumes Count");
                return false;
            }
            if (!string.IsNullOrEmpty(Custompkgresumecnt))
            {
                if (Custompkgresumecnt.Contains(".") || Custompkgresumecnt.Contains("-"))
                {
                    _Dialog.Toast("Resumes Count should not contains any special characters");
                    return false;
                }

            }
            if (!string.IsNullOrEmpty(Custompkgresumecnt))
            {
                if (Convert.ToInt32(Custompkgresumecnt) < 150)
                {
                    _Dialog.Toast("Resumes Count Should be more than 150");
                    return false;
                }
                else if (Convert.ToInt32(Custompkgresumecnt) > 2500)
                {
                    _Dialog.Toast("Resumes Count Should be less than 2500");
                    return false;
                }
            }

            return result;

        }
        public static Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
            if (string.IsNullOrEmpty(cardnumber) || cardnumber.Trim().Length == 0)
            {
                _Dialog.Toast("Credit Card Number is required");
                return false;
            }
            if (string.IsNullOrEmpty(cardname) || cardname.Trim().Length==0)
            {
                _Dialog.Toast("Name on Card is required");
                return false;
            }
            if (!CheckAplhaOnly(cardname))
            {
                _Dialog.Toast("Please enter a valid name");
                return false;
            }
            if (string.IsNullOrEmpty(expirymonthID.ToString()))
            {
                _Dialog.Toast("Expiry Month is required");
                return false;
            }
            if (string.IsNullOrEmpty(expiryyearID.ToString()) || expiryyearID == 0)
            {
                _Dialog.Toast("Expiry Year is required");
                return false;
            }
            if (string.IsNullOrEmpty(CVVnumber))
            {
                _Dialog.Toast("CVV Number is required");
                return false;
            }
            if (string.IsNullOrEmpty(billingname))
            {
                _Dialog.Toast("Billing name is required");
                return false;
            }
            if (!CheckAplhaOnly(billingname))
            {
                _Dialog.Toast("Please enter a valid name");
                return false;
            }
            if (string.IsNullOrEmpty(billingemail))
            {
                _Dialog.Toast("Billing email is required");
                return false;
            }
            if (!CheckValidEmail(billingemail))
            {
                _Dialog.Toast("Enter valid email id");
                return false;
            }
            if (string.IsNullOrEmpty(billingmobileno))
            {
                _Dialog.Toast("Billing mobileno is required");
                return false;
            }
            if (billingmobileno.Length < 10)
            {
                _Dialog.Toast("Enter a 10-digit mobile number");
                return false;
            }
            if (!CheckValidPhone(billingmobileno))
            {
                _Dialog.Toast("Enter valid mobile number");
                return false;
            }
            if (string.IsNullOrEmpty(zipcode))
            {
                _Dialog.Toast("City is required");
                return false;
            }
            if (string.IsNullOrEmpty(LocationAddress))
            {
                _Dialog.Toast("Billing address is required");
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
        public async void applycouponcode()
        {
           try
            {
                if (string.IsNullOrEmpty(couponcodetxt))
                {
                    _Dialog.Toast("Enter your coupon code");
                }
                else
                {
                    _Dialog.ShowLoading("");
                    var couponapi = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                    var details = await couponapi.applyresumecoupon(data.totaladscount, couponcodetxt.Trim(), data.totalresume, data.iscustompackage);
                    if (details != null)
                    {
                        _Dialog.HideLoading();
                        if (details.resultinformation == "invalid")
                        {
                            couponcodetxt = "";
                            Scouponcode = "";
                            _Dialog.Toast("Enter a valid coupon");
                        }
                        else
                        {
                            couponvisible = false;
                            removecouponvisible = true;
                            appliedcoupondtl = true;
                            Scouponcode = couponcodetxt.Trim();
                            var currentpg = GetCurrentPage();
                            var totalamt = currentpg.FindByName<Label>("totalamt");
                            totalamount = totalamount.Replace("$", "");
                            decimal payamnt = Convert.ToDecimal(totalamount) - Convert.ToDecimal(details.discountamount);
                            totalamount = payamnt.ToString("N");
                            totalamt.Text = "$ " + totalamount;
                            var discntamt = currentpg.FindByName<Label>("discntamt");
                            decimal discamnt = Convert.ToDecimal(details.discountamount);
                            discountamount = discamnt.ToString("N");
                            discntamt.Text = "$ " + discountamount;
                        }
                    }
                    else
                        _Dialog.HideLoading();
                }
            }
            catch(Exception ex)
            {

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
                if (!string.IsNullOrEmpty(totalamount) && !string.IsNullOrEmpty(discountamount))
                {
                    Scouponcode = "";
                    couponcodetxt = "";
                    Totalamt = Convert.ToDecimal(totalamount) + Convert.ToDecimal(discountamount);
                    totalamt.Text = "$ " + Totalamt.ToString("N");
                    totalamount = Totalamt.ToString();
                }
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
            catch (Exception e) 
            { 
            }
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

        public async void Addpayment()
        {
            var currpage = GetCurrentPage();
            try
            {
                if (paymentvalidation())
                {
                    _Dialog.ShowLoading("");
                    string sPostedviaid = "";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                    {
                        sPostedviaid = "194";
                    }
                    else
                    {
                        sPostedviaid = "197";
                    }
                    Resumepaymentdetails pymnt = new Resumepaymentdetails()
                    {
                        agentid = data.agentid,
                        name = billingname,
                        email = billingemail,
                        mobileno = billingmobileno,
                        billingcity = city,
                        billingaddress = LocationAddress,
                        billingstatecode = statecode,
                        billingzipcode = zipcode,
                        billingcountry = country,
                        userpid = Commonsettings.UserPid,
                        cardnumber = cardnumber,
                        expmnth = expirymonthID.ToString(),
                        expyear = expiryyearID.ToString(),
                        cvv = CVVnumber,
                        usertype = data.usertypeid,
                        postedviaid = sPostedviaid,
                        couponcode = Scouponcode,
                        totalresume = data.totalresume,
                        totaladscount = data.totaladscount,
                        iscustompackage = data.iscustompackage,
                        ip = data.ip
                    };
                    var baseApi = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                    ResumePayment details = await baseApi.Resumepayment(pymnt);
                    if (details.errormsg.Length > 0)
                    {
                        _Dialog.HideLoading();
                        _Dialog.Toast(details.errormsg.ToString());
                    }

                    else
                    {
                        await currpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumesuccess(data.agentid));
                    }
                }
            }
            catch (Exception ex)
            {
                _Dialog.HideLoading();
                _Dialog.Toast("Payment failed.");
                string msg = ex.Message.ToString();
            }
        }
    }
}
