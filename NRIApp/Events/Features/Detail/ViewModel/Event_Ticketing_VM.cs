using Acr.UserDialogs;
using Android.Bluetooth.LE;
using Android.Views;
using Java.Util;
using NRIApp.Controls;
using NRIApp.Events.Features.Detail.Interface;
using NRIApp.Events.Features.Detail.Model;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Models;
using Plugin.Messaging;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.Events.Features.Detail.ViewModel
{
    public class Event_Ticketing_VM : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _contentviewvisible;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }

        public string _Eventtotalamount = "";
        public string Eventtotalamount
        {
            get { return _Eventtotalamount; }
            set
            {
                _Eventtotalamount = value;
                OnPropertyChanged(nameof(Eventtotalamount));
            }
        }

        public string _totaltickets = "";
        public string totaltickets
        {
            get { return _totaltickets; }
            set
            {
                _totaltickets = value;
                OnPropertyChanged(nameof(totaltickets));
            }
        }

        public string _payamount = "";
        public string payamount
        {
            get { return _payamount; }
            set
            {
                _payamount = value;
                OnPropertyChanged(nameof(payamount));
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

        private bool _cvvimgvisible = false;
        public bool cvvimgvisible
        {
            get { return _cvvimgvisible; }
            set { _cvvimgvisible = value; OnPropertyChanged(nameof(cvvimgvisible)); }
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

        //sanng
        public string _sangfirstname = "";
        public string sangfirstname
        {
            get { return _sangfirstname; }
            set
            {
                _sangfirstname = value;
                OnPropertyChanged(nameof(sangfirstname));
            }
        }
        public string _sanglastname = "";
        public string sanglastname
        {
            get { return _sanglastname; }
            set
            {
                _sanglastname = value;
                OnPropertyChanged(nameof(sanglastname));
            }
        }
        public string _sangateofbirth = DateTime.Now.ToString("MM/dd/yyyy");
        public string sangateofbirth
        {
            get { return _sangateofbirth; }
            set
            {
                _sangateofbirth = value;
                OnPropertyChanged(nameof(sangateofbirth));
            }
        }
        public string _sangselectgender = "";
        public string sangselectgender
        {
            get { return _sangselectgender; }
            set
            {
                _sangselectgender = value;
                OnPropertyChanged(nameof(sangselectgender));
            }
        }
        public string _sangparentname = "";
        public string sangparentname
        {
            get { return _sangparentname; }
            set
            {
                _sangparentname = value;
                OnPropertyChanged(nameof(sangparentname));
            }
        }
        public string _sangparentmobileno = "";
        public string sangparentmobileno
        {
            get { return _sangparentmobileno; }
            set
            {
                _sangparentmobileno = value;
                OnPropertyChanged(nameof(sangparentmobileno));
            }
        }
        public string _sangparentaltmobileno = "";
        public string sangparentaltmobileno
        {
            get { return _sangparentaltmobileno; }
            set
            {
                _sangparentaltmobileno = value;
                OnPropertyChanged(nameof(sangparentaltmobileno));
            }
        }

        public string _sangpemail = "";
        public string sangpemail
        {
            get { return _sangpemail; }
            set
            {
                _sangpemail = value;
                OnPropertyChanged(nameof(sangpemail));
            }
        }

        public string _sangselectcountry = "";
        public string sangselectcountry
        {
            get { return _sangselectcountry; }
            set
            {
                _sangselectcountry = value;
                OnPropertyChanged(nameof(sangselectcountry));
            }
        }
        public string _sangstatename = "";
        public string sangstatename
        {
            get { return _sangstatename; }
            set
            {
                _sangstatename = value;
                OnPropertyChanged(nameof(sangstatename));
            }
        }
        public string _sangcityname = "";
        public string sangcityname
        {
            get { return _sangcityname; }
            set
            {
                _sangcityname = value;
                OnPropertyChanged(nameof(sangcityname));
            }
        }
        public string _sangzipcode = "";
        public string sangzipcode
        {
            get { return _sangzipcode; }
            set
            {
                _sangzipcode = value;
                OnPropertyChanged(nameof(sangzipcode));
            }
        }
        public string _sangareyoutrained = "";
        public string sangareyoutrained
        {
            get { return _sangareyoutrained; }
            set
            {
                _sangareyoutrained = value;
                OnPropertyChanged(nameof(sangareyoutrained));
            }
        }
        public string _sanghowtoknow = "";
        public string sanghowtoknow
        {
            get { return _sanghowtoknow; }
            set
            {
                _sanghowtoknow = value;
                OnPropertyChanged(nameof(sanghowtoknow));
            }
        }

        public string _sanghowtoknowothers = "";
        public string sanghowtoknowothers
        {
            get { return _sanghowtoknowothers; }
            set
            {
                _sanghowtoknowothers = value;
                OnPropertyChanged(nameof(sanghowtoknowothers));
            }
        }



        public bool _issangblockavail = false;
        public bool issangblockavail
        {
            get { return _issangblockavail; }
            set
            {
                _issangblockavail = value;
                OnPropertyChanged(nameof(issangblockavail));
            }
        }

        public bool _isnonsangblockavail = false;
        public bool isnonsangblockavail
        {
            get { return _isnonsangblockavail; }
            set
            {
                _isnonsangblockavail = value;
                OnPropertyChanged(nameof(isnonsangblockavail));
            }
        }
        //sang
        public List<Ticketing_Data> Eventstickets { get; set; }

        public List<Ticketing_Data> Eventsticketstemp { get; set; }

        public List<LS_Payment> Selectdropdownvalues;

        public List<Ticketing_Data> Eventordersummary;

        public List<LS_Payment> Expirymonthlistdata { get; set; }
        public List<LS_Payment> Expiryyearlistdata { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }

        public Command selecttoshowpopup { get; set; }

        public Command selectrowwisevalue { get; set; }

        public ICommand TaponExpirymonth { get; set; }
        public ICommand TaponExpiryyear { get; set; }
        public ICommand selectexpirymonth { get; set; }
        public ICommand selectexpiryyear { get; set; }
        public Command Taponcvvimg { get; set; }

        public ICommand Paynonsangorder { get; set; }
        public ICommand Paysangorder { get; set; }

        public ICommand Eventcouponapply { get; set; }

        public ICommand Removeeventcoupon { get; set; }

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
        public List<LS_Payment> expiryyearslist = new List<LS_Payment>();
        Sendpaymentchoosedetails spcd = new Sendpaymentchoosedetails();

        string Scheckoutid = "", Sticketingid = "", Sticketingtypeid = "", sEventtype = "", sPostedtype = "", sOrderform = "", sAdtitle = "",
               sTablerows = "", sTablerowseats = "", sEvendate = "";
        public Event_Ticketing_VM(string checkoutid, List<Ticketing_Data> td, string totalamount, List<Ticket_Calculation_Data> tcd, string eventtype, string postedtype, string orderform, Sendpaymentchoosedetails sdf, string ticketingid, string adtitle, string tablerowseats, string tablerows, string eventdate)
        {
            Scheckoutid = checkoutid; sAdtitle = adtitle;
            sEventtype = eventtype; sPostedtype = postedtype; sOrderform = orderform; sEvendate = eventdate;
            spcd = sdf; Sticketingid = ticketingid; Sticketingtypeid = sdf.objectid;
            sTablerows = tablerows; sTablerowseats = tablerowseats;
            Bindordersummery(checkoutid, td, totalamount, tcd);
            int year = DateTime.Now.Year;
            int yearlength = year + 50;
            for (int i = year; i < yearlength; i++)
            {
                expiryyearslist.Add(new LS_Payment { years = i, checkimage = "RadioButtonUnChecked.png" });
            }
            TaponExpiryyear = new Command(showexpiryyearlist);
            TaponExpirymonth = new Command(showexpirymonthlist);
            Expirymonthlistdata = Expirymonthlist;
            Expiryyearlistdata = expiryyearslist;
            Taponcvvimg = new Command(showcvvimg);
            PopupContentTap = new Command(showcontentview);
            ContentViewTap = new Command(Closecontentview);
            selectexpirymonth = new Command<LS_Payment>(selectexpirymonthvalue);
            selectexpiryyear = new Command<LS_Payment>(selectexpiryyearvalue);
            Paysangorder = new Command(ClickPaysangorder);
            Paynonsangorder = new Command(ClickPaynonsangorder);
            Eventcouponapply = new Command(Submitcoupon);
            Removeeventcoupon = new Command(Removecoupon);
            if (sPostedtype == "1" && sEventtype == "2")
            {

            }
            else if(sPostedtype=="0" && sEventtype == "0")
            {
                isnonsangblockavail = true;
                OnPropertyChanged(nameof(isnonsangblockavail));
            }
            else if (sEventtype == "2")
            {

            }
            else
            {
                if (sPostedtype == "1" && sEventtype == "0")
                {
                    if (sOrderform == "2")
                    {
                        issangblockavail = true;
                        OnPropertyChanged(nameof(issangblockavail));
                    }
                    else
                    {
                        isnonsangblockavail = true;
                        OnPropertyChanged(nameof(isnonsangblockavail));
                    }
                }
            }
        }

        List<Ticketing_Data> td = new List<Ticketing_Data>();

        List<Order_Summary> os = new List<Order_Summary>();
        public bool paymentvalidation()
        {
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

            if (string.IsNullOrEmpty(billingmobileno))
            {
                _Dialog.Toast("Billing mobile number is required");
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

            return true;
        }

        public async void Removecoupon()
        {
            try{
                _Dialog.ShowLoading("");
                spcd.couponcode = "";
                double totalamount = 0;
                var eventetails = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                var edata = await eventetails.GetCaluculation(spcd);
                if(edata !=null && edata.ROW_TICKETLINEITEM.Count > 0)
                {
                    


                    foreach (var lineitem in edata.ROW_TICKETLINEITEM)
                    {
                        foreach (var item in os)
                        {
                            if (lineitem.lineitemtitle == item.title)
                            {
                                item.couponavail = false;
                                item.couondiscount = "$" + lineitem.couponoffertotalamount;
                                item.totalamounttxt = "$" + lineitem.lineitemtotalamount;
                                item.totalservicecharge = "$" + Convert.ToString(lineitem.lineitemservicecharge);
                            }
                        }
                    }

                    var curpage = GetCurrentPage();

                    var list = curpage.FindByName<HVScrollGridView>("listorder");
                    list.ItemsSource = null;
                    list.ItemsSource = os;


                    foreach (var item in os)
                    {


                        totalamount = totalamount + Convert.ToDouble(item.totalamounttxt.Replace("$", ""));

                    }

                    Eventtotalamount = "$" + Convert.ToString(totalamount);
                    payamount = "Pay $" + totalamount;
                 
                    var lblcouponamount = curpage.FindByName<Label>("lblcouponamt");
                    lblcouponamount.Text = "";

                    var framecoupnblk = curpage.FindByName<Frame>("frmaecouponblk");
                    framecoupnblk.IsVisible = true;

                    var coupon = curpage.FindByName<CustomEntry>("entrycoupon");
                    coupon.Text = "";

                    var stackcoupnblk = curpage.FindByName<StackLayout>("stackcoupon");
                    stackcoupnblk.IsVisible = false;

                    var btncouponapply = curpage.FindByName<Button>("Btnapplycoupon");
                    var btncouponcancel = curpage.FindByName<Button>("Btnremovecoupon");

                    btncouponapply.IsVisible = true;
                    btncouponcancel.IsVisible = false;
                }
                _Dialog.HideLoading();

            }catch(Exception e)
            {

            }
        }

        public async void Submitcoupon()
        {
            try
            {
                bool couponstatus = false, couponapplied = false;
                var curpage = GetCurrentPage();

                var coupon = curpage.FindByName<CustomEntry>("entrycoupon");

                if (coupon.Text != "")
                {
                    _Dialog.ShowLoading("");
                    spcd.couponcode = coupon.Text;

                    var eventetails = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                    var edata = await eventetails.GetCaluculation(spcd);

                    if (edata != null)
                    {
                        var couponcheck = edata.ROW_TICKETLINEITEM[0].couponstatus;
                        var coupontype = edata.ROW_TICKETLINEITEM[0].coupontype;
                        var ticketcoupon = edata.ROW_TICKETLINEITEM[0].ticketcoupon;
                        if (!string.IsNullOrEmpty(couponcheck))
                        {

                            if (couponcheck == "coupon-live" || couponcheck == "coupon-notexists"
                        || couponcheck == "coupon-code" || couponcheck == "coupon-codeemail"
                        || couponcheck == "coupon-valid")
                            {

                                couponstatus = true;
                                var coupondiscounttype = edata.ROW_TICKETLINEITEM[0].coupondiscounttype;
                                var coupondiscountamount = edata.ROW_TICKETLINEITEM[0].coupondiscountamount;
                                var coupondiscountpercentage = edata.ROW_TICKETLINEITEM[0].coupondiscountpercentage;
                                //if (data.ROW_TICKETLINEITEM[i].couponoffertotalamount != null && data.ROW_TICKETLINEITEM[i].totalamount != null) {
                                var couponamount = edata.ROW_TICKETLINEITEM[0].couponoffertotalamount;
                                if (!couponapplied == true && couponamount != "0")
                                    couponapplied = true;
                                string msg = "";

                                var lblcouponamount = curpage.FindByName<Label>("lblcouponamt");

                                var lblcouponamounttxt = curpage.FindByName<Label>("coupontxt");
                                if (coupondiscounttype == "1") 
                                {
                                    msg = "(Discount of $" + RoundToTwoDecimals(coupondiscountamount) + ")";

                                    lblcouponamount.Text = "$"+ RoundToTwoDecimals(coupondiscountamount);

                                    lblcouponamounttxt.Text = msg;
                                   
                                }
                                  
                                else if (coupondiscounttype == "2")
                                {
                                    msg = "(Discount of " + RoundToTwoDecimals(coupondiscountpercentage) + "%)";

                                    lblcouponamount.Text = "$" + RoundToTwoDecimals(coupondiscountpercentage);

                                    lblcouponamounttxt.Text = msg;
                                    
                                }
                                    
                                //console.log(key + "  " + coupontype + "  " + ticketcoupon + "  "+ msg);
                                if (coupontype == "ticketcoupon" && ticketcoupon == "ticketcoupon")
                                {
                                    _Dialog.Toast(msg);
                                }
                                else if (coupontype == "eventcoupon")
                                {
                                    _Dialog.Toast(msg);
                                }
                                else if (couponcheck == "coupon-invalid")
                                {
                                    _Dialog.Toast("Invalid Promo Code.");

                                }
                                else if (couponcheck == "coupon-purchased")
                                {

                                    _Dialog.Toast("Promo code already purchased for this email, please try another one.");

                                }
                                else if (couponcheck == "coupon-maxireached")
                                {
                                    _Dialog.Toast("Promo code discount has been closed.");

                                }
                                else if (couponcheck == "coupon-expired")
                                {
                                    _Dialog.Toast("Promo code discount has been expired.");

                                }
                                else if (couponcheck == "coupon-emailnotfound")
                                {

                                }

                                foreach (var lineitem in edata.ROW_TICKETLINEITEM)
                                {
                                    foreach (var item in os)
                                    {
                                        if (lineitem.lineitemtitle == item.title)
                                        {
                                            item.couponavail = true;
                                            item.couondiscount ="$"+ lineitem.couponoffertotalamount;
                                            item.totalamounttxt = "$" + lineitem.lineitemtotalamount;
                                            item.totalservicecharge = "$" + Convert.ToString(lineitem.lineitemservicecharge);
                                        }
                                    }
                                }

                                var list = curpage.FindByName<HVScrollGridView>("listorder");
                                list.ItemsSource = null;
                                list.ItemsSource = os;



                            }
                        }
                        if (couponstatus)
                        {
                            var btncouponapply = curpage.FindByName<Button>("Btnapplycoupon");
                            var btncouponcancel = curpage.FindByName<Button>("Btnremovecoupon");
                            if (couponapplied)
                            {
                                _Dialog.Toast("Coupon activated");
                                btncouponapply.IsVisible = false;
                                btncouponcancel.IsVisible = true;

                            }
                            else
                            {
                                _Dialog.Toast("Coupon code not applicable to selected level");
                                btncouponapply.IsVisible = true;
                                btncouponcancel.IsVisible = false;
                            }
                        }

                        double totalamount = 0;

                        foreach (var item in os)
                        {
                            
                                
                                totalamount = totalamount + Convert.ToDouble(item.totalamounttxt.Replace("$",""));
                           
                        }

                        

                        Eventtotalamount = "$" + Convert.ToString(totalamount);
                        payamount = "Pay $" + totalamount;
                    }
                }
                else
                {
                    _Dialog.Toast("Please enter coupon code");
                }
                _Dialog.HideLoading();
            }

            catch (Exception e)
            {

            }
        }
        public double RoundDecimals(double dblValue, int intDecimals)
        {
            return Math.Round(dblValue * Math.Pow(10, intDecimals)) / Math.Pow(10, intDecimals);
        }
        public string RoundToTwoDecimals(string strValue)
        {
            strValue = Convert.ToString(RoundDecimals(Convert.ToDouble(strValue), 2));
            var strTemp = "";
            strTemp = strValue.ToString();
            string[] strTemp1 = strTemp.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (strTemp1.Length > 1)
            {
                if (strTemp1[1].Length >= 2)
                {
                    return strValue;
                }
            }
            if (strValue.ToString().IndexOf(".") == -1)
            {
                strValue = strValue + ".00";
            }
            else
            {
                strValue = strValue + "0";
            }
            return strValue;
        }
        public bool paymentsangvalidation()
        {
            if (string.IsNullOrEmpty(sangfirstname))
            {
                _Dialog.Toast("First name is required");
                return false;
            }

            if (string.IsNullOrEmpty(sanglastname))
            {
                _Dialog.Toast("Last name is required");
                return false;
            }
            if (string.IsNullOrEmpty(sangselectgender))
            {
                _Dialog.Toast("Gender is required");
                return false;
            }
            if (string.IsNullOrEmpty(sangparentname))
            {
                _Dialog.Toast("Parent name is required");
                return false;
            }

            if (string.IsNullOrEmpty(sangparentmobileno))
            {
                _Dialog.Toast("Parent mobile number is required");
                return false;
            }


            if (sangparentmobileno.Length < 10)
            {
                _Dialog.Toast("Enter a 10-digit mobile number");
                return false;
            }
            if (!CheckValidPhone(sangparentmobileno))
            {
                _Dialog.Toast("Enter valid mobile number");
                return false;
            }


            if (string.IsNullOrEmpty(sangparentaltmobileno))
            {
                _Dialog.Toast("Parent alternate mobile number is required");
                return false;
            }


            if (sangparentaltmobileno.Length < 10)
            {
                _Dialog.Toast("Enter a 10-digit mobile number");
                return false;
            }
            if (!CheckValidPhone(sangparentaltmobileno))
            {
                _Dialog.Toast("Enter valid mobile number");
                return false;
            }
            if (string.IsNullOrEmpty(sangpemail))
            {
                _Dialog.Toast("Email is required");
                return false;
            }

            if (string.IsNullOrEmpty(sangselectcountry))
            {
                _Dialog.Toast("Country is required");
                return false;
            }
            if (string.IsNullOrEmpty(sangstatename))
            {
                _Dialog.Toast("State is required");
                return false;
            }

            if (string.IsNullOrEmpty(sangcityname))
            {
                _Dialog.Toast("State is required");
                return false;
            }

            if (string.IsNullOrEmpty(sangzipcode))
            {
                _Dialog.Toast("State is required");
                return false;
            }
            if (string.IsNullOrEmpty(sangareyoutrained))
            {
                _Dialog.Toast("Are you trained is required");
                return false;
            }

            if (string.IsNullOrEmpty(sanghowtoknow))
            {
                _Dialog.Toast("How to know is required");
                return false;
            }
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

        public async void ClickPaysangorder()
        {
            try
            {
                if (paymentsangvalidation())
                {
                    _Dialog.ShowLoading("");
                    var eventetails = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                    var edata = await eventetails.GetCaluculation(spcd);

                    if (edata != null)
                    {
                        var objectid = Sticketingid;
                        var firstcardval = cardnumber;
                        var firstcarddigit = firstcardval.Substring(0, 1);
                        var tickettitle = sAdtitle;

                        Ticketing_final_submit_paraams tfs = new Ticketing_final_submit_paraams()
                        {
                            firstname_reg = sangfirstname,
                            lastname_reg = sanglastname,
                            dateofbirth = sangateofbirth,
                            gender = sangselectgender,
                            firstname = sangparentname,
                            email = sangpemail,
                            countryode = "+1",
                            homephone = sangparentmobileno,
                            workphone = sangparentaltmobileno,
                            city = sangcityname,
                            state = sangstatename,
                            statecode = "",
                            country = Commonsettings.Usercountry,
                            zipcode = sangzipcode,
                            sagcountry = sangselectcountry,
                            cardnumber = cardnumber,
                            cardname = cardname,
                            expirymonth = expirymonthID,
                            expiryyear = Convert.ToString(expiryyearID),
                            cvv = CVVnumber,
                            tablerows = sTablerows,
                            tablerowsseats = sTablerowseats,
                            ticketingid = Sticketingid,
                            checkoutid = Scheckoutid,
                            ticketingtypeid = Sticketingtypeid,
                            registration = "2",
                            areyoutrained = sangareyoutrained,
                            howtoyouknow = sanghowtoknow,
                            others = sanghowtoknowothers
                        };
                        var Detail = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                        var data = await Detail.Submitfinalorder(tfs);





                        if (data.value == "success")
                        {
                            var id = data.orderguid;
                            var ordernumber = data.ordernumber;
                            var curpage = GetCurrentPage();
                            await curpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Event_Payment_Thankyou(ordernumber, sAdtitle, sEvendate, billingemail));
                        }
                        else
                        {
                            _Dialog.Toast(data.errormsg);
                        }

                    }
                    _Dialog.HideLoading();
                }

            }
            catch (Exception e)
            {

            }
        }

        public async void ClickPaynonsangorder()
        {
            try
            {
                if (paymentvalidation())
                {
                    _Dialog.ShowLoading("");
                    spcd.email = billingemail;
                    var eventetails = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                    var edata = await eventetails.GetCaluculation(spcd);

                    if (edata != null)
                    {
                        var objectid = Sticketingid;
                        var firstcardval = cardnumber;
                        var firstcarddigit = firstcardval.Substring(0, 1);
                        var tickettitle = sAdtitle;

                        Ticketing_final_submit_paraams tfs = new Ticketing_final_submit_paraams()
                        {
                            firstname = billingname,
                            email = billingemail,
                            countryode = Selectcountry,
                            homephone = billingmobileno,
                            workphone = billingmobileno,
                            city = Commonsettings.Usercity,
                            state = Commonsettings.Userstate,
                            statecode = Commonsettings.Userstatecode,
                            country = Commonsettings.Usercountry,
                            zipcode = Commonsettings.Userzipcode,
                            cardnumber = cardnumber,
                            cardname = cardname,
                            expirymonth = expirymonthID,
                            expiryyear = Convert.ToString(expiryyearID),
                            cvv = CVVnumber,
                            tablerows = sTablerows,
                            tablerowsseats = sTablerowseats,
                            ticketingid = Sticketingid,
                            checkoutid = Scheckoutid,
                            ticketingtypeid = Sticketingtypeid,
                            registration = "0",
                            couponcode=spcd.couponcode

                        };
                        var Detail = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                        var data = await Detail.Submitfinalorder(tfs);




                        if (data.value == "success")
                        {
                            var id = data.orderguid;
                            var ordernumber = data.ordernumber;
                            var curpage = GetCurrentPage();
                            await curpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Event_Payment_Thankyou(ordernumber, sAdtitle, sEvendate, billingemail));
                        }
                        else
                        {
                            _Dialog.Toast(data.errormsg);
                        }

                    }
                    _Dialog.HideLoading();
                }

            }
            catch (Exception e)
            {

            }
        }
        string sCouponcode = "";
        public async void Bindordersummery(string checkoutid, List<Ticketing_Data> td, string totalamount, List<Ticket_Calculation_Data> tcd)
        {
            try
            {
                _Dialog.ShowLoading("");
                //   await Task.Delay(3000);
                var eventartist = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
                var data = await eventartist.Checkoutdetails(checkoutid);
                if (data.ROW_DATA.Count > 0)
                {
                    var coupon = data.ROW_DATA[0].coupon;
                    sCouponcode = coupon;
                    if (data.ROW_DATA[0].ticketdealstatus != "exist")
                    {
                        var curpage1 = GetCurrentPage();
                        var list1 = curpage1.FindByName<Frame>("frmaecouponblk");
                        if (coupon == "coupon-live" || coupon == "coupon-code" || coupon == "coupon-codeemail")
                        {

                            list1.IsVisible = true;
                        }
                        else
                        {
                            list1.IsVisible = false;
                        }
                    }
                }
                foreach (var item in tcd)
                {
                    if (!string.IsNullOrEmpty(item.lineitemqty))
                    {
                        string Squanity = item.lineitemqty + " Tickets";
                        string Stotalamount = "$" + item.lineitemtotalamount;
                        string ticketfee = "$" + item.lineitempurchasetotal;
                        string totalservchg = "$" + RoundToTwoDecimals(Convert.ToString(item.lineitemservicecharge));
                        
                        os.Add(new Order_Summary { title = item.lineitemtitle, quantity = item.lineitemqty, quantitytxt = Squanity,totalamount= item.lineitemtotalamount, ticketfee = ticketfee, totalamounttxt = Stotalamount, totalservicecharge = totalservchg, couponavail=false });
                    }

                }
                var curpage = GetCurrentPage();
                var list = curpage.FindByName<HVScrollGridView>("listorder");

                list.ItemsSource = os;

                
                Eventordersummary = td;

                int Stotaltickets = 0; double finaltotalamount = 0;

                foreach (var item in os)
                {
                    Stotaltickets = Stotaltickets + Convert.ToInt32(item.quantity);
                    finaltotalamount = finaltotalamount + Convert.ToDouble(item.totalamount);
                }

                totaltickets = Stotaltickets + " Tickets";

                string roundfinalamount= RoundToTwoDecimals(Convert.ToString(finaltotalamount));

                payamount = "Pay $" + roundfinalamount;

                Eventtotalamount = "$" + roundfinalamount;

                _Dialog.HideLoading();
            }
            catch (Exception e)
            {

            }

        }
        public static string expirymonthID = "";
        public static int expiryyearID = 0;
        public void selectexpirymonthvalue(LS_Payment payment_Params)
        {
            var currentpage = GetCurrentPage();
            Label monthtxtclr = currentpage.FindByName<Label>("lblexpdate");
            monthtxtclr.TextColor = Color.FromHex("#212121");
            expirymonthvalue = payment_Params.months.ToString();
            expirymonthID = payment_Params.months;
            contentviewvisible = false;
        }
        public void selectexpiryyearvalue(LS_Payment payment_Params)
        {
            var currentpage = GetCurrentPage();
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
            var curentpage = GetCurrentPage();
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
                var curentpage = GetCurrentPage();
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
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return currentPage;
        }
    }
}
