using Acr.UserDialogs;
//using Android.Text;
using Newtonsoft.Json;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using NRIApp.Roommates.Features.Post.Interface;
using NRIApp.Roommates.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;
using Googlecity = NRIApp.LocalService.Features.Models.Googlecity;
using Predictions = NRIApp.LocalService.Features.Models.Predictions;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Online_Class_Payment_VM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;
        private bool _couponvisible = true;
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";
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
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _cvvimgvisible = false;
        public bool cvvimgvisible
        {
            get { return _cvvimgvisible; }
            set { _cvvimgvisible = value; OnPropertyChanged(nameof(cvvimgvisible)); }
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
        private bool _Withoutpayment = false;
        public bool Withoutpayment
        {
            get { return _Withoutpayment; }
            set
            {
                _Withoutpayment = value;
                OnPropertyChanged(nameof(Withoutpayment));
            }
        }
        private bool _Withpayment = false;
        public bool Withpayment
        {
            get { return _Withpayment; }
            set
            {
                _Withpayment = value;
                OnPropertyChanged(nameof(Withpayment));
            }
        }
        //Receipt
        private string _OrderID;
        public string OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; OnPropertyChanged(nameof(OrderID)); }
        }

        private string _AdStartdate;
        public string AdStartdate
        {
            get { return _AdStartdate; }
            set { _AdStartdate = value; OnPropertyChanged(nameof(AdStartdate)); }
        }
        private string _AdEnddate;
        public string AdEnddate
        {
            get { return _AdEnddate; }
            set { _AdEnddate = value; OnPropertyChanged(nameof(AdEnddate)); }
        }
        private string _Adinstructor;
        public string Adinstructor
        {
            get { return _Adinstructor; }
            set { _Adinstructor = value; OnPropertyChanged(nameof(Adinstructor)); }
        }
        private string _AdPaidamount;
        public string AdPaidamount
        {
            get { return _AdPaidamount; }
            set { _AdPaidamount = value; OnPropertyChanged(nameof(AdPaidamount)); }
        }
        private string _Adurl;
        public string Adurl
        {
            get { return _Adurl; }
            set { _Adurl = value; OnPropertyChanged(nameof(Adurl)); }
        }
        private string _Addays;
        public string Addays
        {
            get { return _Addays; }
            set { _Addays = value; OnPropertyChanged(nameof(Addays)); }
        }
        private string _Adtext;
        public string Adtext
        {
            get { return _Adtext; }
            set { _Adtext = value; OnPropertyChanged(nameof(Adtext)); }
        }



        //Receipt
        public ICommand ApplyCouponCode { get; set; }

        public ICommand removecouponcmd { get; set; }
        public Command CloseImgelst { get; set; }
        public ICommand TaponExpirymonth { get; set; }
        public ICommand TaponExpiryyear { get; set; }
        public ICommand selectexpirymonth { get; set; }
        public ICommand selectexpiryyear { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command Taponcvvimg { get; set; }
        public ICommand openbilling { get; set; }
        public Command TaponProceed { get; set; }
        public Command<Location> LocationCmd { get; set; }
        public Command Selectgoogleaddress { get; set; }

        public List<LS_Payment> Expirymonthlist = new List<LS_Payment>()
        {
            new LS_Payment {months="01",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="02",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="03",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="04",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="05",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="06",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="07",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="08",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="09",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="10",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="11",checkimage="RadioButtonUnChecked.png"},
            new LS_Payment {months="12",checkimage="RadioButtonUnChecked.png"},
        };
        public List<LS_Payment> expiryyearslist = new List<LS_Payment>()
        {

            new LS_Payment{years=2020,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2021,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2022,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2023,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2024,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2025,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2026,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2027,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2028,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2029,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2030,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2031,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2032,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2033,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2034,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2035,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2036,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2037,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2038,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2039,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2040,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2041,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2042,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2043,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2044,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2045,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2046,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2047,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2048,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2049,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2050,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2051,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2052,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2053,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2054,checkimage="RadioButtonUnChecked.png"},
            new LS_Payment{years=2055,checkimage="RadioButtonUnChecked.png"},
        };


        //Receipt
        public List<LS_Payment> Expirymonthlistdata { get; set; }
        public List<LS_Payment> Expiryyearlistdata { get; set; }

        // string sOldclpostid = "", sPrimeid = "";
        public static string city = "";
        public static string zipcode = "";
        public static string country = "";
        public static string statecode = "";
        public string sGuserid = "", sConverionid = "", sBizname = "", sPrimeid = "", sOldclpostid = "", sCouponcode = "", sPrimeguid = "";
        decimal totalamount = 0, discountamount = 0, finalamount = 0;
        public static string expirymonthID = "";
        public static int expiryyearID = 0;
        public LS_Online_Class_Payment_VM(string guserid, string classdetailid)
        {
            // sAdid = adid;

            sGuserid = guserid;
            sConverionid = classdetailid;
            Bindonlineclasspackagedetails(guserid);
            ApplyCouponCode = new Command(ApplyCouponCodeonline);
            removecouponcmd = new Command(Removecouponcode);
            TaponExpiryyear = new Command(showexpiryyearlist);
            TaponExpirymonth = new Command(showexpirymonthlist);
            Expirymonthlistdata = Expirymonthlist;
            Expiryyearlistdata = expiryyearslist;
            Taponcvvimg = new Command(showcvvimg);
            PopupContentTap = new Command(showcontentview);
            ContentViewTap = new Command(Closecontentview);
            selectexpirymonth = new Command<LS_Payment>(selectexpirymonthvalue);
            selectexpiryyear = new Command<LS_Payment>(selectexpiryyearvalue);
            LocationCmd = new Command<Location>(OnLocationClick);
            Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
            TaponProceed = new Command(paymentonlineclass);

        }
        string paymentsucessguid = "";
        public LS_Online_Class_Payment_VM(string paymentgudid)
        {
            paymentsucessguid = paymentgudid;
            if (paymentgudid != "")
            {
                Bindonlineorderrecipet(paymentgudid);
            }
            else
            {
                Withoutpayment = true;
            }
        }
        public async void Bindonlineorderrecipet(string paymentgudid)
        {
            try
            {
                _Dialog.ShowLoading("");
                var orederrecipt = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                var data = await orederrecipt.Getonlineclassorderrecipet(paymentgudid);
                if (data != null && data.ROW_DATA.Count > 0 && data.ROW_DATA != null)
                {
                    Withpayment = true;
                    OrderID = data.ROW_DATA[0].OrderID;
                    AdStartdate = data.ROW_DATA[0].upddate;
                    // AdEnddate = data.ROW_DATA[0].enddate;
                    Adinstructor = data.ROW_DATA[0].insname;
                    AdPaidamount = HttpUtility.HtmlDecode(data.ROW_DATA[0].htmlcode) + data.ROW_DATA[0].paid_amount;
                    Adtext =data.ROW_DATA[0].ptag;
                    _Dialog.HideLoading();
                }
                else
                {
                    Withoutpayment = true;
                    _Dialog.HideLoading();
                }
            }
            catch (Exception ex)
            {
                _Dialog.HideLoading();
            }
        }
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
        public void OnSelectgoogleaddress(Predictions pt)
        {
            LocationAddress = TempLocationAddress = pt.Description;
            OnPropertyChanged(nameof(IsBusy));
            IsBusy = false;
        }
        public bool paymentvalidation()
        {
            if (string.IsNullOrEmpty(cardnumber))
            {
                _Dialog.Toast("Credit Card Number is required");
                return false;
            }
            if (string.IsNullOrEmpty(cardname))
            {
                _Dialog.Toast("Name on Card is required");
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
                _Dialog.Toast("Billing Name is required");
                return false;
            }
            if (string.IsNullOrEmpty(billingemail))
            {
                _Dialog.Toast("Billing Email is required");
                return false;
            }
            if (!Regex.IsMatch(billingemail, Emailpattern))
            {
                _Dialog.Toast("Enter Proper Email");
                return false;
            }
            if (string.IsNullOrEmpty(billingmobileno))
            {
                _Dialog.Toast("Billing mobile number is required");
                return false;
            }
            if (string.IsNullOrEmpty(zipcode) || string.IsNullOrEmpty(LocationName))
            {
                _Dialog.Toast("City is required");
                return false;
            }
            if (string.IsNullOrEmpty(LocationAddress))
            {
                _Dialog.Toast("Billing address is required");
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
            return true;
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
        public async void paymentonlineclass()
        {
            //   CreateToken("4000000000003089", 02, 2020, "123");
            try
            {
                if (paymentvalidation())
                {
                    _Dialog.ShowLoading("");
                    string sPostedviaid = "", sPostedvia = "";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                    {
                        sPostedviaid = "194";
                    }
                    else
                    {
                        sPostedviaid = "197";
                    }

                    sPostedvia = Commonsettings.UserMobileOS.ToLower();
                    LS_ONLINE_PAYMENT_BILLING_DETAILS bill = new LS_ONLINE_PAYMENT_BILLING_DETAILS()
                    {
                        contactname = billingname,
                        contactemail = billingemail,
                        country = country,
                        contactno = billingmobileno,
                        streetname = LocationAddress,
                        city = city,
                        cityurl = null,
                        zipcode = zipcode,
                        statecode = statecode,
                        conamount = Convert.ToString(totalamount),
                        postedviaid = sPostedviaid,
                        guid = sGuserid,
                        postedvia = sPostedvia
                    };
                    var billing = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                    var data = await billing.getbillingdetails(bill);
                    if (data != null && data.result == "success")
                    {
                        // getbillingdetails
                        LS_ONLINE_PAYMENT_SUBMIT LPD = new LS_ONLINE_PAYMENT_SUBMIT()
                        {
                            userpid = Commonsettings.UserPid,
                            email = Commonsettings.UserEmail,
                            conversionprocessid=data.conversionid,
                            cardnumber = cardnumber,
                            expmonth = expirymonthID.ToString(),
                            expyear = expiryyearID.ToString(),
                            cvvno = CVVnumber,
                            postedviaid = sPostedviaid,
                            postedvia = Commonsettings.UserMobileOS.ToLower(),
                            userid = Commonsettings.UserId,
                            guid=sGuserid,
                            couponcodevalue=couponcodetxt

                        };
                        var payment = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                        var paymentdata = await payment.payonlineclassamount(LPD);
                        if (paymentdata != null && paymentdata.result == "success")
                        {
                            var curpage = LS_ViewModel.GetCurrentPage();
                            await curpage.Navigation.PushAsync(new LocalService.Features.Views.Detail.Online_Thankyou(paymentdata.guid));
                        }
                        else
                        {
                            _Dialog.Toast(paymentdata.result);
                        }
                        _Dialog.HideLoading();
                    }
                    else
                    {
                        _Dialog.HideLoading();
                    }
                    // _Dialog.Toast("success");


                }
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }

        }
        public void selectexpirymonthvalue(LS_Payment payment_Params)
        {
            var currentpage = LS_ViewModel.GetCurrentPage();
            Label monthtxtclr = currentpage.FindByName<Label>("lblexpdate");
            monthtxtclr.TextColor = Color.FromHex("#212121");
            expirymonthvalue = payment_Params.months.ToString();
            expirymonthID = payment_Params.months;
            contentviewvisible = false;
        }
        public void selectexpiryyearvalue(LS_Payment payment_Params)
        {
            var currentpage = LS_ViewModel.GetCurrentPage();
            Label yeartxtclr = currentpage.FindByName<Label>("lblexpyear");
            yeartxtclr.TextColor = Color.FromHex("#212121");
            expiryyearvalue = payment_Params.years.ToString();
            expiryyearID = payment_Params.years;
            contentviewvisible = false;
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
            OnPropertyChanged(nameof(contentviewvisible));
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
            OnPropertyChanged(nameof(contentviewvisible));
        }
        public void showexpirymonthlist()
        {
            var curentpage = LS_ViewModel.GetCurrentPage();
            var expirymonth = curentpage.FindByName<ListView>("ExpiryMonth");


            foreach (var data in Expirymonthlist)
            {
                if (expirymonthvalue == data.months.ToString())
                {
                    data.checkimage = "RadioButtonChecked.png";
                    expirymonthvalue = data.months.ToString();
                    // expirymonthID = data.months;
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
            OnPropertyChanged(nameof(contentviewvisible));
            OnPropertyChanged(nameof(monthvisible));
            OnPropertyChanged(nameof(yearvisible)); OnPropertyChanged(nameof(cvvimgvisible));
        }
        public void showexpiryyearlist()
        {
            try
            {
                var curentpage = LS_ViewModel.GetCurrentPage();
                var expiryyear = curentpage.FindByName<ListView>("ExpiryYear");

                foreach (var data in expiryyearslist)
                {
                    if (expiryyearvalue == data.years.ToString())
                    {
                        data.checkimage = "RadioButtonChecked.png";
                        expiryyearvalue = data.years.ToString();
                        // expiryyearID = data.years;
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
                OnPropertyChanged(nameof(contentviewvisible));
                OnPropertyChanged(nameof(monthvisible));
                OnPropertyChanged(nameof(yearvisible)); OnPropertyChanged(nameof(cvvimgvisible));
            }
            catch (Exception ex)
            {

            }
        }
        public async void ApplyCouponCodeonline()
        {
            try
            {
                if (!string.IsNullOrEmpty(couponcodetxt))
                {
                    _Dialog.ShowLoading("");
                    var coupon = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                    var data = await coupon.Apllyonlineclasscouponcode(sGuserid, couponcodetxt);
                    if (data.ROW_DATA.Count > 0 && data.ROW_DATA[0].couponstatus.ToLower() == "valid" && data.ROW_DATA[0].couponstatus != "")
                    {
                       
                        var currentpage = LS_ViewModel.GetCurrentPage();
                        var disamnttxt = currentpage.FindByName<Label>("discntamt");
                        disamnttxt.Text = "$" + data.ROW_DATA[0].discountamount;
                        removecouponvisible = true;
                        var totalamnttxt = currentpage.FindByName<Label>("totalamt");
                        totalamnttxt.Text = "$" + data.ROW_DATA[0].countcurrencycurrent;
                        couponvisible = false;
                        appliedcoupondtl = true;
                        discountamount = Convert.ToDecimal(data.ROW_DATA[0].discountamount);
                        finalamount = Convert.ToDecimal(data.ROW_DATA[0].countcurrencycurrent);
                        OnPropertyChanged(nameof(removecouponvisible)); OnPropertyChanged(nameof(couponvisible));
                        OnPropertyChanged(nameof(appliedcoupondtl));
                    }
                    else
                    {
                        _Dialog.Toast("Invalid");
                    }
                    _Dialog.HideLoading();
                }
                else
                {
                    _Dialog.Toast("Please enter couponcode.");
                }

            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }
        public void Removecouponcode()
        {
            try
            {
                couponcodetxt = "";
                var currentpage = LS_ViewModel.GetCurrentPage();
                finalamount = totalamount;
                var totalamnttxt = currentpage.FindByName<Label>("totalamt");
                totalamnttxt.Text = "$" + finalamount;
                couponvisible = true;
                appliedcoupondtl = false;
                removecouponvisible = false;
                OnPropertyChanged(nameof(removecouponvisible)); OnPropertyChanged(nameof(couponvisible));
                OnPropertyChanged(nameof(appliedcoupondtl));
            }
            catch (Exception e)
            {

            }
        }


        public async void Bindonlineclasspackagedetails(string guserid)
        {
            try
            {
                var DistributeBiz = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                var data = await DistributeBiz.Getonlineclasspackagedetails(guserid);

                if (data.ROW_DATA.Count > 0)
                {
                    var currentpage = LS_ViewModel.GetCurrentPage();
                    var amnttxt = currentpage.FindByName<Label>("packamt");
                    amnttxt.Text = "$" + data.ROW_DATA[0].amount;
                    var totalamnttxt = currentpage.FindByName<Label>("totalamt");
                    totalamnttxt.Text = "$" + data.ROW_DATA[0].amount;
                    appliedcoupondtl = false;
                    removecouponvisible = false;
                    totalamount = finalamount = Convert.ToDecimal(data.ROW_DATA[0].amount);
                }
            }
            catch (Exception e)
            {

            }
        }













        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
