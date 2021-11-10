using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.LocalService.Features.ViewModels
{
   public class LS_Flex_Payment_VM : INotifyPropertyChanged
    {

        public static string expirymonthID="";
        public static int expiryyearID=0;
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;
        public Command TaponProceed { get; set; }
        public List<LS_Payment> Expirymonthlistdata { get; set; }
        public List<LS_Payment> Expiryyearlistdata { get; set; }
        public ICommand TaponExpirymonth { get; set; }
        public ICommand TaponExpiryyear { get; set; }
        public ICommand selectexpirymonth { get; set; }
        public ICommand selectexpiryyear { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command Taponcvvimg { get; set; }
        public Command BillingblkClick { get; set; }
        public Command Publishadclick { get; set; }
        public Command Viewadclick { get; set; }
        public ICommand cityListcommand { protected set; get; }
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

        private string _billingname;
        public string billingname
        {
            get { return _billingname; }
            set { _billingname = value; OnPropertyChanged(nameof(billingname)); }
        }
        private string _billingemail;
        public string billingemail
        {
            get { return _billingemail; }
            set { _billingemail = value; OnPropertyChanged(nameof(billingemail)); }
        }
        private string _billingcontact;
        public string billingcontact
        {
            get { return _billingcontact; }
            set { _billingcontact = value; OnPropertyChanged(nameof(billingcontact)); }
        }
        public string _tempBusinessAddress = "";
        private string _billingaddress;
        public string billingaddress
        {
            get { return _billingaddress; }
            set { _billingaddress = value.Trim(); OnPropertyChanged(nameof(billingaddress));
                
            }
        }
        public string _Tempbillingcity = "";
        private string _billingcity;
        public string billingcity
        {
            get { return _billingcity; }
            set { _billingcity = value.Trim(); ; OnPropertyChanged(nameof(billingcity));
                if (string.IsNullOrEmpty(billingcity))
                {
                    _Tempbillingcity = "";
                }
                if (!string.IsNullOrEmpty(billingcity) && _Tempbillingcity != billingcity)
                {
                    cityajax();
                }
            }
        }
        private string _Selectcontact = "+1";
        public string Selectcontact
        {
            get { return _Selectcontact; }
            set { _Selectcontact = value; }
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
        private string _billingcountry;
        public string billingcountry
        {
            get { return _billingcountry; }
            set { _billingcountry = value; OnPropertyChanged(nameof(billingcountry)); }
        }
        private string _billingzip;
        public string billingzip
        {
            get { return _billingzip; }
            set { _billingzip = value; OnPropertyChanged(nameof(billingzip)); }
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
        private bool _BillingIsvisible = false;
        public bool BillingIsvisible
        {
            get { return _BillingIsvisible; }
            set
            {
                _BillingIsvisible = value;
                OnPropertyChanged(nameof(BillingIsvisible));
            }
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
            set { _Adtext = value;OnPropertyChanged(nameof(Adtext)); }
        }



        //Receipt

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
            if (string.IsNullOrEmpty(billingcontact))
            {
                _Dialog.Toast("Billing Contact is required");
                return false;
            }
            if (string.IsNullOrEmpty(billingaddress))
            {
                _Dialog.Toast("Billing Address is required");
                return false;
            }
            if (string.IsNullOrEmpty(billingcity))
            {
                _Dialog.Toast("Billing City is required");
                return false;
            }

            return true;
        }
       // string sOldclpostid = "", sPrimeid = "";
        public LS_Flex_Payment_VM(string oldclpostid,string primeid)
        {
            if (LS_ViewModel.islseditable != 1)
            {


                if (primeid != "")
                {
                    Bindflexorderrecipet(primeid);
                }
                else
                {
                    Withoutpayment = true;
                }
                Publishadclick = new Command(Publishad);
                Viewadclick = new Command(Viewbusinesslink);
            }
            else
            {
                Withoutpayment = true;
                Bindeditthankyou();
            }
            sOldclpostid = oldclpostid;
            sPrimeid = primeid;
            

        }
        public string sAdid = "", sBizname = "",sPrimeid="",sOldclpostid="", sCouponcode="", sPrimeguid=""; decimal totalamount = 0;
        public LS_Flex_Payment_VM(LS_PRIME_BY_GUID_DATA lpbg, SELECTED_PACKAGE sp)
        {
           // sAdid = adid;
            totalamount = sp.amount;
            sBizname = LS_Posting_VM.sBusinessname;
            sPrimeid = lpbg.primeid;
            sCouponcode = lpbg.couponcode;
            sOldclpostid = lpbg.oldclpostid;
            sPrimeguid = lpbg.guid;
            Expirymonthlistdata = Expirymonthlist;
            Expiryyearlistdata = Expiryyearlistdata;
            BindBillingdetails(lpbg);
            Taponcvvimg = new Command(showcvvimg);
            PopupContentTap = new Command(showcontentview);
            ContentViewTap = new Command(Closecontentview);
            cityListcommand = new Command<LOCATION_DATA>(cityListselection);
            TaponExpiryyear = new Command(showexpiryyearlist);
            TaponExpirymonth = new Command(showexpirymonthlist);
            selectexpirymonth = new Command<LS_Payment>(selectexpirymonthvalue);
            selectexpiryyear = new Command<LS_Payment>(selectexpiryyearvalue);
            // TaponProceed = new Command(paymentfirst);
            TaponProceed = new Command(paymentfirstFlex);
             BillingblkClick = new Command(ClicktoBillingblk);
        }

        public async void Bindeditthankyou()
        {
            try
            {
                await System.Threading.Tasks.Task.Delay(2000);
                var curpage = LS_ViewModel.GetCurrentPage();
                var lbl1 = curpage.FindByName<Label>("lbltext1");
                lbl1.Text = "You have successfully updated your business profile in sulekha.com";
                var lbl2 = curpage.FindByName<Label>("lbltext2");

                lbl2.IsVisible = false;
                var orederrecipt = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var data = await orederrecipt.Publishflexad(sOldclpostid, sPrimeid);

            }
            catch(Exception e)
            {

            }
        }
        public async void Publishad()
        {
            try
            {
                _Dialog.ShowLoading("");
                var orederrecipt = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var data = await orederrecipt.Publishflexad(sOldclpostid,sPrimeid);
                if(data != null)
                {
                    if (data.result == "success")
                    {
                        Adurl = data.linkurl;
                        sAdid = data.alladid;
                        var curpage = LS_ViewModel.GetCurrentPage();
                        var publisadlink = curpage.FindByName<Button>("lblpublishlink");
                        var viewadlink = curpage.FindByName<Button>("lblviewlink");

                        publisadlink.IsVisible = false;
                        viewadlink.IsVisible = true;
                    }
                }
                _Dialog.HideLoading();
            }
            catch(Exception e)
            {

            }
        }

        public async void Viewbusinesslink()
        {
            
            try
            {
                if (!string.IsNullOrEmpty(sAdid))
                {
                    var addetails = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                    var data = await addetails.Getaddetailsbyadid(sAdid);
                    if (data.ROW_DATA.Count > 0)
                    {
                        var curpage = LS_ViewModel.GetCurrentPage();
                        LS_LISTINGMODEL_DATA LMD = new LS_LISTINGMODEL_DATA
                        {
                            titleurl = data.ROW_DATA[0].titleurl,
                            maincityurl = data.ROW_DATA[0].maincityurl,
                            adid = data.ROW_DATA[0].adid,
                            viewno = data.ROW_DATA[0].viewno,
                            premiumad=data.ROW_DATA[0].premiumad,
                            title=data.ROW_DATA[0].title,
                            newcityurl=data.ROW_DATA[0].newcityurl

                        };
                        
                        await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Detail(LMD));

                    }
                    else
                    {
                        Uri uri = new Uri(Adurl);
                        Device.OpenUri(uri);
                    }
                }
                else
                {
                    Uri uri = new Uri(Adurl);
                    Device.OpenUri(uri);
                }
            }
            catch(Exception e)
            {
               
            }
        }

        public async void Bindorderrecipet(string adid)
        {
            try
            {
              
                var orederrecipt = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var data = await orederrecipt.sendorderrecipet(adid);
                if (data !=null &&data.ROW_EMAIL_RECEPT.Count>0 && data.ROW_EMAIL_RECEPT !=null)
                {
                    Withpayment = true;
                    OrderID = data.ROW_EMAIL_RECEPT[0].orderid;
                    AdStartdate = data.ROW_EMAIL_RECEPT[0].startdate;
                    AdEnddate = data.ROW_EMAIL_RECEPT[0].enddate;
                    AdPaidamount ="$"+data.ROW_EMAIL_RECEPT[0].amount;
                    Adurl = data.ROW_EMAIL_RECEPT[0].linkurl;
                    Adtext = "A copy of the receipt has been mailed to your email id "+data.ROW_EMAIL_RECEPT[0].email+". You may see your Ad by clicking";
                    _Dialog.HideLoading();
                }
                else
                {
                    Withoutpayment = true;
                    _Dialog.HideLoading();
                }
            }
            catch(Exception ex) {
                _Dialog.HideLoading();
            }
        }
        public async void Bindflexorderrecipet(string primeid)
        {
            try
            {

                var orederrecipt = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var data = await orederrecipt.Getflexorderrecipet(primeid);
                if (data != null && data.ROW_DATA.Count > 0 && data.ROW_DATA != null)
                {
                    Withpayment = true;
                    OrderID = data.ROW_DATA[0].transid;
                   // AdStartdate = data.ROW_DATA[0].startdate;
                   // AdEnddate = data.ROW_DATA[0].enddate;
                    AdPaidamount = "$" + data.ROW_DATA[0].amount;
                    Addays = data.ROW_DATA[0].days;
                    Adtext = "A copy of the receipt has been mailed to your email id " + data.ROW_DATA[0].email + ". You may see your Ad by clicking";
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
        public void ClicktoBillingblk()
        {
            if (BillingIsvisible)
            {
                BillingIsvisible = false;
            }
            else
            {
                BillingIsvisible = true;
            }
            OnPropertyChanged(nameof(BillingIsvisible));
        }

        public  void BindBillingdetails(LS_PRIME_BY_GUID_DATA lpbg)
        {
            try
            {
               // var addetails = RestService.For<IServiceAPI>(Commonsettings.Localservices);
               // var data = await addetails.GetAdDetails(sAdid);
                if (lpbg != null)
                {
                    billingemail = lpbg.email;
                    billingcontact = lpbg.mobileno;
                    billingaddress = lpbg.streetname;
                    billingname = lpbg.contactname;
                    billingcity = lpbg.city+", "+ lpbg.statecode;
                    billingcountry = lpbg.country;
                    billingzip = lpbg.zipcode;
                    OnPropertyChanged(nameof(billingemail)); OnPropertyChanged(nameof(billingcontact));
                    OnPropertyChanged(nameof(billingaddress)); OnPropertyChanged(nameof(billingname));
                    OnPropertyChanged(nameof(billingcity)); OnPropertyChanged(nameof(billingcountry));
                    OnPropertyChanged(nameof(billingzip));

                }

            }
            catch(Exception e)
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
            var curentpage = LS_ViewModel.GetCurrentPage();
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
            var curentpage = LS_ViewModel.GetCurrentPage();
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
        public void selectexpirymonthvalue(LS_Payment payment_Params)
        {
            var currentpage =LS_ViewModel.GetCurrentPage();
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

        public async void paymentfirstFlex()
        {
            //   CreateToken("4000000000003089", 02, 2020, "123");
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

                LS_FLEX_PAYMENT_DETAILS LPD = new LS_FLEX_PAYMENT_DETAILS()
                {
                    userpid = Commonsettings.UserPid,
                    email = Commonsettings.UserEmail,
                    adid = sAdid,
                    bizmode = "",
                    contactname = cardname,
                    address = billingaddress,
                    contactno = billingcontact,
                    mobilecountry = Selectcontact,
                    city = billingcity,
                    zipcode = billingzip,
                    country = billingcountry,
                    contactemail = billingemail,
                    selectremitindia = "",
                    cardnumber = cardnumber,
                    expmonth = expirymonthID.ToString(),
                    expyear = expiryyearID.ToString(),
                    cvv = CVVnumber,
                    postedviaid = sPostedviaid,
                    postedvia = Commonsettings.UserMobileOS.ToLower(),
                    userid = Commonsettings.UserId,
                    Businessname=sBizname,
                    Primeid = sPrimeid,
                    Oldclpostid=sOldclpostid,
                    Couponcode=sCouponcode,
                    Primeguid =sPrimeguid
                };

                //    var postpayment = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);

                //    var paymentresult = await postpayment.Postpaymentdetails(LPD);
                var shopingcart = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                PAYMENT_RESULT_DATA list = await shopingcart.getFlexpayment(LPD);
                if (list.result == "success")
                {
                    var curpage = LS_ViewModel.GetCurrentPage();
                    _Dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_POST_Thankyou(LPD.Oldclpostid,LPD.Primeid));
                    // CreateToken(cardnumber, Convert.ToInt32(expirymonthID), expiryyearID, CVVnumber,list);
                }
                else
                {
                    _Dialog.HideLoading();
                    _Dialog.Toast(list.result);

                }

            }

        }

        public async void paymentfirst()
        {
            //   CreateToken("4000000000003089", 02, 2020, "123");
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

                LS_PAYMENT_DETAILS LPD = new LS_PAYMENT_DETAILS()
                {
                    userpid = Commonsettings.UserPid,
                    email = Commonsettings.UserEmail,
                    adid = sAdid,
                    bizmode = "",
                    contactname = cardname,
                    address = billingaddress,
                    contactno = billingcontact,
                    mobilecountry=Selectcontact,
                    city = billingcity,
                    zipcode = billingzip,
                    country = billingcountry,
                    contactemail = billingemail,
                    selectremitindia = "",
                    cardnumber = cardnumber,
                    expmonth = expirymonthID.ToString(),
                    expyear = expiryyearID.ToString(),
                    cvv = CVVnumber,
                    postedviaid = sPostedviaid,
                    postedvia = Commonsettings.UserMobileOS.ToLower(),
                    userid = Commonsettings.UserId
                };

                //    var postpayment = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);

                //    var paymentresult = await postpayment.Postpaymentdetails(LPD);
                 var shopingcart = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                 SHOPPING_CART_DATA list = await shopingcart.getShopingcartNumber(LPD);
                if (list.Result=="success")
                {
                    var curpage = LS_ViewModel.GetCurrentPage();
                    _Dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_POST_Thankyou(sAdid));
                    // CreateToken(cardnumber, Convert.ToInt32(expirymonthID), expiryyearID, CVVnumber,list);
                }
                else
                {
                    _Dialog.HideLoading();
                    _Dialog.Toast(list.Result);
                    
                }
                
            }

        }
        public async void cityajax()
        {
            try
            {
                var location = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.Localservices);
                var data = await location.GetLocationData(billingcity);
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
        public void cityListselection(LOCATION_DATA ld)
        {
            //  city = ld.city; statecode = ld.statecode; state = ld.statename; latitude = ld.latitude;
            //   longitude = ld.longitude; zipcode = Convert.ToString(ld.zipcode); country = ld.country; newcityurl = ld.newcityurl;
            billingcity = _Tempbillingcity = ld.city + ", " + ld.statecode;
            billingzip= Convert.ToString(ld.zipcode);billingcountry = ld.country;

            OnPropertyChanged(nameof(billingcity)); OnPropertyChanged(nameof(IsBusy));
            _isBusy = false;

        }
       /* public void CreateToken(string cardNumber, int cardExpMonth, int cardExpYear, string cardCVC ,SHOPPING_CART_DATA list)
        {
           // StripeConfiguration.SetApiKey("sk_test_4eC39HqLyjWDarjtT1zdp7dc");
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
                {
                    CCStripePayment(stripeToken.Id, totalamount, list);
                }
               
                  
                
                    
            }
            catch (StripeException exception)
            {
                _Dialog.HideLoading();
                switch (exception.StripeError.ErrorType)
                {
                    case "card_error":
                        _Dialog.Toast("Please try another card number");

                        break;
                   
                    default:
                        throw;
                }
            }
            catch (Exception ex)
            {
                _Dialog.HideLoading();
                string msg = ex.Message.ToString();
            }


        }
        public async  void CCStripePayment(string token, decimal totalamount,SHOPPING_CART_DATA list)
        {
            string sResult = "", Stripe_TransId = "", sPostedviaid="", sPostedvia="", authcode="";
            try
            {
               // StripeConfiguration.SetApiKey("sk_test_4eC39HqLyjWDarjtT1zdp7dc");
                StripeConfiguration.SetApiKey("sk_live_KKWWltg7m7vWwhL5XawZW1rw");
                var options = new Stripe.ChargeCreateOptions
                {
                    Amount =Convert.ToInt64(totalamount) * 100, // *100 (for cent to dollar)
                    Currency = "USD",
                    Description = "SulekhaStripePayment",
                    SourceId = token,
                };
                var service = new Stripe.ChargeService();
                Stripe.Charge charge = service.Create(options);
                sResult = charge.Status;
                if (sResult == "succeeded")
                {
                    if (cardnumber=="4242424242424242")
                    {
                        Stripe_TransId = "9999999999";
                         authcode = "99999";
                    }
                    else
                    {
                        Stripe_TransId = charge.BalanceTransactionId;
                         authcode = charge.AuthorizationCode;
                    }
                  

                    var addetails = RestService.For<IServiceAPI>(Commonsettings.Localservices);

                    var data = await addetails.GetAdDetails(sAdid);

                    if (data.ROW_DATA != null)
                    {
                        if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                        {
                            sPostedviaid = "194";
                        }
                        else
                        {
                            sPostedviaid = "197";
                        }


                        LS_PAYMENT_DETAILS LPD = new LS_PAYMENT_DETAILS()
                        {
                            userpid = Commonsettings.UserPid,
                            email = Commonsettings.UserEmail,
                            adid = sAdid,
                            bizmode = "",
                            contactname = cardname,
                            address = billingaddress,
                            contactno = billingcontact,
                            mobilecountry = Selectcontact,
                            city = billingcity,
                            zipcode = billingzip,
                            country = billingcountry,
                            contactemail = billingemail,
                            selectremitindia = "",
                            cardnumber = cardnumber,
                            expmonth = expirymonthID.ToString(),
                            expyear = expiryyearID.ToString(),
                            cvv = CVVnumber,
                            AuthCode = authcode,
                            TransactionId = Stripe_TransId,
                            postedviaid= sPostedviaid,
                            postedvia= Commonsettings.UserMobileOS.ToLower(),
                            userid=Commonsettings.UserId,
                            Ordernumber=list.Ordernumber,
                            Authorize_AuthCode=list.Authorize_AuthCode,
                            Authorize_TransId=list.Authorize_TransId,
                            cartid=list.cartid,
                            StrObjecttitle=list.StrObjecttitle
                        };

                        var postpayment = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);

                        var paymentresult = await postpayment.Postpaymentdetails(LPD);

                        if (paymentresult != null)
                        {
                            if (paymentresult.result == "success")
                            {
                                var curpage = LS_ViewModel.GetCurrentPage();
                                await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_POST_Thankyou(paymentresult.adid));
                            }
                            else
                            {
                                _Dialog.Toast("Please try again!");
                            }
                        }
                        else
                        {
                            _Dialog.Toast("Please try again!");
                        }
                    }

                }
                else
                {
                    _Dialog.Toast("Your payment was unsucessful.Please try again!");
                }
                _Dialog.HideLoading();
            }
            catch (StripeException exception)
            {
                _Dialog.HideLoading();
                switch (exception.StripeError.ErrorType)
                {
                    case "card_error":
                        _Dialog.Toast(exception.StripeError.Message+". Please try another card");
                        break;
                    default:
                        throw;
                }
            }
            catch (Exception exx)
            {
                _Dialog.HideLoading();
                sResult = exx.Message.ToString();
                _Dialog.Toast("Your payment was unsucessful.Please try again!");
            }
           // return sResult;
        } */
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
