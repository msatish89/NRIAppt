using Acr.UserDialogs;
using NRIApp.Events.Features.Detail.Interface;
using NRIApp.Events.Features.Detail.Model;
using NRIApp.Helpers;
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
using System.Web;
using NRIApp.LocalService.Features.Models;

namespace NRIApp.Events.Features.Detail.ViewModel
{
    public class Detail_VM : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        public static HtmlWebViewSource eventdesc;

        private string _Eventbanner;
        public string Eventbanner
        {
            get { return _Eventbanner; }
            set
            {
                _Eventbanner = value;
                OnPropertyChanged(nameof(Eventbanner));
            }
        }
        private string _Category;
        public string Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private bool _Categoryavail = false;
        public bool Categoryavail
        {
            get { return _Categoryavail; }
            set
            {
                _Categoryavail = value;
                OnPropertyChanged(nameof(Categoryavail));
            }
        }
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        private string _Date;
        public string Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        private string _Time;
        public string Time
        {
            get { return _Time; }
            set
            {
                _Time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        private string _Organisername;
        public string Organisername
        {
            get { return _Organisername; }
            set
            {
                _Organisername = value;
                OnPropertyChanged(nameof(Organisername));
            }
        }
        private string _Organisercontact;
        public string Organisercontact
        {
            get { return _Organisercontact; }
            set
            {
                _Organisercontact = value;
                OnPropertyChanged(nameof(Organisercontact));
            }
        }
        //public HtmlWebViewSource WebViewSource
        //{
        //    get
        //    {
        //        return new HtmlWebViewSource { Html = _Eventdesc };
        //    }
        //}

        private HtmlWebViewSource _Htmlwebviewsource;

        public HtmlWebViewSource Htmlwebviewsource

        {

            get {
                return _Htmlwebviewsource;
            }

            set {

                _Htmlwebviewsource = value;
               
            }

        }
        private string _Eventdesc;
        public string Eventdesc
        {
            get {
                return _Eventdesc;
            }
            set
            {
                _Eventdesc = value;
                OnPropertyChanged(nameof(Eventdesc));
               
            }
        }
        public string Ticketrangetemp { get; set; }
        private string _Ticketrange;
        public string Ticketrange
        {
            get
            {
                return _Ticketrange;
            }
            set
            {
                _Ticketrange = value;
                OnPropertyChanged(nameof(Ticketrange));

            }
        }

        private string _Ticketrangetxt;
        public string Ticketrangetxt
        {
            get
            {
                return _Ticketrangetxt;
            }
            set
            {
                _Ticketrangetxt = value;
                OnPropertyChanged(nameof(Ticketrangetxt));

            }
        }
        private List<EVENT_TERMS> _GetTerms;
        public List<EVENT_TERMS> GetTerms
        {
            get { return _GetTerms; }
            set
            {
                _GetTerms = value;
                OnPropertyChanged(nameof(GetTerms));
            }
        }
        private List<EVENT_FAQ> _Getfaq;
        public List<EVENT_FAQ> Getfaq
        {
            get { return _Getfaq; }
            set
            {
                _Getfaq = value;
                OnPropertyChanged(nameof(Getfaq));
            }
        }
        public string _Isfavoriteimg;
        public string Isfavoriteimg
        {
            get { return _Isfavoriteimg; }
            set { _Isfavoriteimg = value; }
        }
        private string _Reportlist = "";
        public string Reportlist
        {
            get { return _Reportlist; }
            set
            {
                _Reportlist = value;


            }
        }
        private string _Reportdesc = "";
        public string Reportdesc
        {
            get { return _Reportdesc; }
            set
            {
                _Reportdesc = value;


            }
        }
        private bool _Isavailorganisecontact = false;
        public bool Isavailorganisecontact
        {
            get { return _Isavailorganisecontact; }
            set
            {
                _Isavailorganisecontact = value;
                OnPropertyChanged(nameof(Isavailorganisecontact));
            }
        }
        private bool _Ispayingavail = false;
        public bool Ispayingavail
        {
            get { return _Ispayingavail; }
            set
            {
                _Ispayingavail = value;
                OnPropertyChanged(nameof(Ispayingavail));
            }
        }
        private bool _Isbuying = false;
        public bool Isbuying
        {
            get { return _Isbuying; }
            set
            {
                _Isbuying = value;
                OnPropertyChanged(nameof(Isbuying));
            }
        }
        private bool _Issoldout = false;
        public bool Issoldout
        {
            get { return _Issoldout; }
            set
            {
                _Issoldout = value;
                OnPropertyChanged(nameof(Issoldout));
            }
        }
        private bool _Issoldtype = false;
        public bool Issoldtype
        {
            get { return _Issoldtype; }
            set
            {
                _Issoldtype = value;
                OnPropertyChanged(nameof(Issoldtype));
            }
        }
        private bool _Isartistavail = false;
        public bool Isartistavail
        {
            get { return _Isartistavail; }
            set
            {
                _Isartistavail = value;
                OnPropertyChanged(nameof(Isartistavail));
            }
        }
        private bool _Iseventtimezoneavail = false;
        public bool Iseventtimezoneavail
        {
            get { return _Iseventtimezoneavail; }
            set
            {
                _Iseventtimezoneavail = value;
                OnPropertyChanged(nameof(Iseventtimezoneavail));
            }
        }
        private bool _Isvisiblecost = false;
        public bool Isvisiblecost
        {
            get { return _Isvisiblecost; }
            set
            {
                _Isvisiblecost = value;
                OnPropertyChanged(nameof(Isvisiblecost));
            }
        }
        private bool _Ispaymentvisible = false;
        public bool Ispaymentvisible
        {
            get { return _Ispaymentvisible; }
            set
            {
                _Ispaymentvisible = value;
                OnPropertyChanged(nameof(Ispaymentvisible));
            }
        }

        public string _Eventwatchon = "";
        public string Eventwatchon
        {
            get { return _Eventwatchon; }
            set
            {
                _Eventwatchon = value;
                OnPropertyChanged(nameof(Eventwatchon));
            }
        }
        public string _Eventwatchtxt = "";
        public string Eventwatchtxt
        {
            get { return _Eventwatchtxt; }
            set
            {
                _Eventwatchtxt = value;
                OnPropertyChanged(nameof(Eventwatchtxt));
            }
        }
        private bool _contentviewvisible;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }

        private bool _Iseventtimeavail = false;
        public bool Iseventtimeavail
        {
            get { return _Iseventtimeavail; }
            set
            {
                _Iseventtimeavail = value;
                OnPropertyChanged(nameof(Iseventtimeavail));
            }
        }
        private bool _detailregistractionblk = false;
        public bool detailregistractionblk
        {
            get { return _detailregistractionblk; }
            set
            {
                _detailregistractionblk = value;
                OnPropertyChanged(nameof(detailregistractionblk));
            }
        }

        
        string adtitle = "", addlink = "",Isfavorite="",Seventid="",Sticketingid="",sticketingtypeid="",eventlat="",eventlong="",ticketingid="";
        public List<string> Categories { get;set; }
        public List<EVENT_REPORT_LIST> listofreports { get; set; }
        public List<EVENT_ARTIST_DATA> Eventartist { get; set; }

        public List<EVENT_TIMEZONE_DATA> Eventtimezone { get; set; }

        public List<Ticketing_Data> Eventstickets { get; set; }

        public List<Ticketing_Data> Eventsticketstemp { get; set; }

        public List<LS_Payment> Selectdropdownvalues;

        public List<Event_Times> Eventmultitimes;
        public ICommand ShareLinkcommand { get; set; }
        public ICommand Favoritecommand { get; set; }
        public ICommand ReportFlagcommand { get; set; }
        public ICommand selectreport { get; set; }
        public ICommand Clicksubmitreport { get; set; }
        public ICommand Clickmapview { get; set; }
        public ICommand Opendetailscommand { get; set; }
        public ICommand Buyingtickets { get; set; }

        public ICommand Redirecttoweb { get; set; }
        public ICommand Getregister { get; set; }
        public Command PopupContentTap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command selecttoshowpopup { get; set; }

        public Command selectrowwisevalue { get; set; }
        ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;



            IsBusy = true;
            // Items.Clear();



            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {



                //for (int i = 0; i < 100; i++)
                //    Items.Add(DateTime.Now.AddMinutes(i).ToString("F"));



                IsBusy = false;



                // page.DisplayAlert("Refreshed", "You just refreshed the page! Nice job! Pull to refresh is now disabled", "OK");
                //this.CanRefresh = false;
                Bindeventartis(Seventid);
                Bindeventdetails(Seventid);
               // Bindeventtimezone(Seventid);

                //ShareLinkcommand = new Command(Sharelink);
                //Favoritecommand = new Command(ClickFavoritecommand);
                //ReportFlagcommand = new Command(ClickReportFlagcommand);
                //Clickmapview = new Command(Clicktoopenmap);
                //Opendetailscommand = new Command<EVENT_FAQ>(Opendetails);
                //Buyingtickets = new Command(ClickBuyingtickets);
                //Getregister = new Command(Clickedregister);
                //selecttoshowpopup = new Command<Ticketing_Data>(Selectrowtickets);
                //selectrowwisevalue = new Command<LS_Payment>(selectedrowwisevalue);
                //PopupContentTap = new Command(showcontentview);
                //ContentViewTap = new Command(Closecontentview);
                Bindeventticketdata(Sticketingid);
                return false;
            });
        }

        string tSoldout = "";
        public Detail_VM(string eventid,string ticketingid)
        {
            Seventid = eventid;
            Sticketingid = ticketingid;
          //  Bindeventartis(eventid);
            Bindeventdetails(eventid);
           // Bindeventtimezone(eventid);

            ShareLinkcommand = new Command(Sharelink);
            Favoritecommand = new Command(ClickFavoritecommand);
            ReportFlagcommand = new Command(ClickReportFlagcommand);
            Clickmapview = new Command(Clicktoopenmap);
            Opendetailscommand = new Command<EVENT_FAQ>(Opendetails);
            Buyingtickets = new Command(ClickBuyingtickets);
            Redirecttoweb = new Command(ClickRedirecttoweb);
            Getregister = new Command(Clickedregister);
            selecttoshowpopup = new Command<Ticketing_Data>(Selectrowtickets);
            selectrowwisevalue = new Command<LS_Payment>(selectedrowwisevalue);
            PopupContentTap = new Command(showcontentview);
            ContentViewTap = new Command(Closecontentview);
            Bindeventticketdata(Sticketingid);
        }
        public Detail_VM(string eventid, string stitle, string seventlink)
        {
            Seventid = eventid; adtitle = stitle; addlink = seventlink;
            List<EVENT_REPORT_LIST> listofreportsl = new List<EVENT_REPORT_LIST>();
            listofreportsl.Add(new EVENT_REPORT_LIST { reportlist = "Event has been cancelled", reportid = 1 });
            listofreportsl.Add(new EVENT_REPORT_LIST { reportlist = "Event has been postponed", reportid = 2 });
            listofreportsl.Add(new EVENT_REPORT_LIST { reportlist = "This is an Adult event", reportid = 3 });
            listofreportsl.Add(new EVENT_REPORT_LIST { reportlist = "Event is fake/Inappropriate/Duplicate", reportid = 4});
            listofreportsl.Add(new EVENT_REPORT_LIST { reportlist = "This event is a spam", reportid =5 });
            listofreportsl.Add(new EVENT_REPORT_LIST { reportlist = "Please remove this event", reportid = 6 });
            listofreportsl.Add(new EVENT_REPORT_LIST { reportlist = "This event is private", reportid = 7 });
            listofreports = listofreportsl;
            OnPropertyChanged(nameof(listofreports));
            selectreport = new Command<EVENT_REPORT_LIST>(Clicktoselectreport);
            Clicksubmitreport = new Command(Submitreport);


        }
        string rowwisevalue = ""; double totalamount = 0;

        List<Ticket_Calculation_Data> tcd = new List<Ticket_Calculation_Data>();


        Sendpaymentchoosedetails sendingpaydetails = new Sendpaymentchoosedetails();
        public async void selectedrowwisevalue(LS_Payment lsp)
        {

            _Dialog.ShowLoading(""); totalamount = 0;
             rowwisevalue = lsp.months.ToString();

            foreach (var item in Eventstickets)
            {
                if (trd.tickettypeid == item.tickettypeid)
                {
                    item.rowwiseselecttickets = trd.rowwiseselecttickets = Convert.ToInt32(rowwisevalue);

                }

            }
            contentviewvisible = false;
            Sendpaymentchoosedetails paydetails = new Sendpaymentchoosedetails();

            string parametrs = "";
            foreach (var item in Eventstickets)
            {
                parametrs += item.rowwiseselecttickets + "|:sepcol:|" + item.tickettypeid + "|:sepcol:|" + item.displayorder + "|:seprow:|";

                paydetails.objectid = item.tickettypeid;
            }

            int Stotaltickets = 0; 

            foreach (var item in Eventstickets)
            {
                Stotaltickets = Stotaltickets + Convert.ToInt32(item.rowwiseselecttickets);
            }

           Detail_ticket_VM.tablerows= paydetails.parameters = parametrs;
            try
            {

                var currentpage = GetCurrentPage();
                var curbuytickets = currentpage.FindByName<Button>("btnbuytickets");
                var curregistertickets = currentpage.FindByName<Button>("btnregister");
                

                sendingpaydetails = paydetails;
                var eventetails = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                var edata = await eventetails.GetCaluculation(paydetails);
               
                if (edata != null )
                {
                    tcd = edata.ROW_TICKETLINEITEM;
                    foreach (var item in edata.ROW_TICKETLINEITEM)
                    {
                        //if (item.totalamount != null)
                        //{
                        //    Ticketrange ="$"+ item.totalamount;
                        //}
                        if (item.lineitempurchasetotal != null)
                        {
                            //Ticketrange = Ticketrangetemp;
                            Ticketrange = "$" + item.totalamount;
                            totalamount = totalamount + Convert.ToDouble(item.totalamount);
                            Ticketrangetxt = "Total :";
                        }

                        if (Convert.ToString(totalamount) == "0" || Convert.ToString(totalamount) == "0.00")
                        {
                            Ticketrange = Ticketrangetemp;
                            Ticketrangetxt = "Tickets range :";

                        }
                    }
                    OnPropertyChanged(nameof(Ticketrangetxt));
                    if (Stotaltickets == 0)
                    {
                        curbuytickets.IsVisible = false;
                        curregistertickets.IsVisible = false;
                    }
                    else
                    {
                        curbuytickets.IsVisible = true;
                        curregistertickets.IsVisible = false;
                    }
                    if (Stotaltickets > 0 && (totalamount.ToString() == "0" || totalamount.ToString() == "0.00"))
                    {
                        curbuytickets.IsVisible = false;
                        curregistertickets.IsVisible = true;
                    }
                    

                    OnPropertyChanged(nameof(Ticketrange));
                }

            }
            catch (Exception e)
            {

            }

            var curpage = GetCurrentPage();
            var data = curpage.FindByName<HVScrollGridView>("eventticketlist");
            Eventsticketstemp = Eventstickets;
            data.ItemsSource = null;
            Eventstickets = Eventsticketstemp;
            data.ItemsSource = Eventstickets;
            _Dialog.HideLoading();
           
        }
        Ticketing_Data trd = new Ticketing_Data();
        public void Selectrowtickets(Ticketing_Data td)
        {
            trd = null;
            trd = td;
            int maxperorder = td.maxperorder;
            int minperorder = td.minperorder;
            Selectdropdownvalues = null;
            Selectdropdownvalues = new List<LS_Payment>();
            Selectdropdownvalues.Add(new LS_Payment { months = "0", checkimage = "RadioButtonUnChecked.png" });
            for (int i = minperorder; i <= maxperorder; i++)
            {
                Selectdropdownvalues.Add(new LS_Payment { months = Convert.ToString(i), checkimage = "RadioButtonUnChecked.png" });
            }
            OnPropertyChanged(nameof(Selectdropdownvalues));

            var curpage = GetCurrentPage();

            var d = curpage.FindByName<ListviewScrollbar>("rowdropdown");

            d.ItemsSource = Selectdropdownvalues;
            contentviewvisible = true;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public async void Bindeventticketdata(string ticketingid)
        {
            try
            {
             
                await Task.Delay(2000);
                var eventtimezone = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
                var data = await eventtimezone.GetEventtickiting(ticketingid);
                if (data.ROW_DATA.Count > 0)
                {
                    if (data.ROW_DATA[0].result == "Y" && tSoldout != "1")
                    {
                        if (Seventsetup == "1")
                        {
                            detailregistractionblk = true;
                            OnPropertyChanged(nameof(detailregistractionblk));
                            foreach (var item in data.ROW_DATA)
                            {
                                item.originalpricetxt = HttpUtility.HtmlDecode(item.currency) + item.originalprice;
                                item.servicechargetxt = "+" + Convert.ToString(item.servicecharge + item.chargeperticket);

                                if (item.tickettrend == "Sold Out")
                                {
                                    item.isticketavail = false;
                                }
                                else if (item.tickettrend == "Almost Sold")
                                {
                                    item.isticketavail = true;
                                    item.ticketavailtxt = "Almost Sold";
                                    item.ticketavailtxtclr = "#FF0000";
                                }
                                else if (item.tickettrend == "Selling Fast")
                                {
                                    item.isticketavail = true;
                                    item.ticketavailtxt = "Selling Fast";
                                    item.ticketavailtxtclr = "#FF0000";
                                }
                                else
                                {
                                    item.isticketavail = true;
                                    item.ticketavailtxt = "Available";
                                    item.ticketavailtxtclr = "#08a182";
                                }

                                if (item.hideticketnotsale == "0" && (item.salesstatus == "salesended" || item.salesstatus == "salesnotstarted"))
                                {
                                    item.isticketcloseavail = true;
                                    item.isdropdownavail = false;
                                    item.ticketclosetxt = HttpUtility.HtmlDecode(item.sellingdate.Replace("<span>", "").Replace("</span> ", " ").Replace("<b>", "").Replace("</b>", "").Replace(" </span> ", " "));//<span>Ticket close on </span><b>Oct 30</b>
                                    item.ticketclosetxt = HttpUtility.HtmlDecode(item.ticketclosetxt.Replace("</span>", ""));
                                }
                                else
                                {
                                    item.isticketcloseavail = false;
                                    item.isdropdownavail = true;
                                }

                                item.rowwiseselecttickets = 0;
                            }


                            Eventstickets = data.ROW_DATA;
                            OnPropertyChanged(nameof(Eventstickets));

                        }
                        else
                        {
                            detailregistractionblk = false;
                            OnPropertyChanged(nameof(detailregistractionblk));
                        }

                    }
                    else
                    {
                        var curpage = GetCurrentPage();

                        var frame = curpage.FindByName<Frame>("frmaenoticket");
                        var frmae1 = curpage.FindByName<Frame>("frmaenoticket1");

                        var framaeresult = curpage.FindByName<Label>("isNoticketresult");
                        frame.IsVisible = true;
                        frmae1.IsVisible = true;

                        if (!string.IsNullOrEmpty(data.ROW_DATA[0].soldoutdesc))
                        {
                            framaeresult.Text = data.ROW_DATA[0].soldoutdesc;
                        }
                        else
                        {
                            framaeresult.Text = "Tickets may still be available at the venue. Please contact the event organizer for further details..";
                        }

                            detailregistractionblk = false;
                        OnPropertyChanged(nameof(detailregistractionblk));
                    }
                    _Dialog.HideLoading();
                }
                else
                {
                    var currentpage = GetCurrentPage();
                    var btn = currentpage.FindByName<Button>("btndetail");
                    btn.IsVisible = true;
                    
                }


            }
            catch (Exception e)
            {

            }
        }
        public async void Clickedregister()
        {
            string deviceos = "android";

            if (!Commonsettings.UserMobileOS.ToLower().Contains("android"))
            {
                deviceos = "ios";
            }
            var Detail = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
            var data = await Detail.Trackbuyingtickets(Seventid, Commonsettings.UserPid, deviceos);
            var curpage= GetCurrentPage();
            await curpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail_Register(Seventid,Sticketingid, sticketingtypeid));
        }

        public async void ClickRedirecttoweb()
        {
            try
            {
                Uri weblink = new Uri("http://events.sulekha.com/" + addlink);
                Device.OpenUri(weblink);
            }catch(Exception e)
            {

            }
        }
        public async void ClickBuyingtickets()
        {
            _Dialog.ShowLoading("");
            string deviceos = "android", sPostedviaid="194";
            try
            {
                //if (!Commonsettings.UserMobileOS.ToLower().Contains("android"))
                //{
                //    deviceos = "ios";
                //}
                //09-07-2020
                //var Detail = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
                //var data = await Detail.Trackbuyingtickets(Seventid, Commonsettings.UserPid, deviceos);
                //Uri uri = new Uri("http://events.sulekha.com/" + addlink);
                //Device.OpenUri(uri);
                //09-07-2020   918001979487


                if (!Commonsettings.UserMobileOS.ToLower().Contains("android"))
                {
                    deviceos = "ios";
                    sPostedviaid = "197";
                }


                Event_Checkout_Parameters ecp = new Event_Checkout_Parameters();

                ecp.Ticketingid = Sticketingid;
                string param = "";
                foreach (var item in Eventstickets)
                {
                    param += item.tickettypeid + "|:sepcol:|" + item.rowwiseselecttickets + "|:sepcol:||:seprow:|";


                }
                Detail_ticket_VM.tablerowsseats = ecp.Parameters = param;
                ecp.postedvia = deviceos;
                ecp.loginpid = Commonsettings.UserPid;
                ecp.devicemodel = Commonsettings.UserDeviceModel;

                var Detail = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                var data = await Detail.Addshopingcartorder(ecp);
                if (data != null)
                {
                    var curpage = GetCurrentPage();
                    await curpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.EventPayment(data.id, Eventstickets, Convert.ToString(totalamount), tcd, sEventtype, sPostedtype, sOrderform, sendingpaydetails, Sticketingid, adtitle, Detail_ticket_VM.tablerowsseats, Detail_ticket_VM.tablerows, sEventdate));
                }
                else
                {

                    _Dialog.Toast("A Ordered - Failure " + data.errorinfo);
                }
                _Dialog.HideLoading();

            }catch(Exception e)
            {

            }
            
        }

        string sEventtype = "", sPostedtype = "", sOrderform = "",Seventsetup="",sEventdate="";
        public async void Bindeventdetails(string eventid)
        {
            try
            {
               // _Dialog.ShowLoading("");
                
                string userpid = Commonsettings.UserPid;
                if (string.IsNullOrEmpty(userpid))
                {
                    userpid = "0";
                }
                var Detail = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
                
                
                var data = await Detail.GetEventsDetails(eventid,userpid);
                if (data.ROW_DATA.Count > 0)
                {
                    Eventbanner = data.ROW_DATA[0].imgurl;
                    string[] catgorysplit = data.ROW_DATA[0].category.Split('|');
                    string[] langarr = catgorysplit[0].Split(',');
                    string[] categoryarr = catgorysplit[1].Split(',');


                    //if (langarr.Length>0&& !string.IsNullOrEmpty(langarr[0].ToString()))
                    //{
                    //    Category = langarr[0];
                    //    Categoryavail = true;
                    //}
                    //else
                    Ticketrangetxt = "Ticket range:";
                    OnPropertyChanged(nameof(Ticketrangetxt));
                    var CurrentPage = GetCurrentPage();
                    var addressmap = CurrentPage.FindByName<Image>("detailmap");
                    string venueaddress = data.ROW_DATA[0].address;
                    if(string.IsNullOrEmpty(venueaddress)|| venueaddress.ToLower() == "live streaming")
                    {
                        addressmap.IsVisible = false;
                    }
                    else
                    {
                        addressmap.IsVisible = true;
                    }
                    Eventwatchtxt = data.ROW_DATA[0].streaming;

                    if (Eventwatchtxt.ToLower().Contains("facebook")){
                        Eventwatchon = "facebook.png";
                    }else if (Eventwatchtxt.ToLower().Contains("youtube")){
                        Eventwatchon = "youtube.png";
                    }
                    else if (Eventwatchtxt.ToLower().Contains("zoom")){
                   
                        Eventwatchon = "zoomIcon2.png";
                    }
                    else
                    {
                        Eventwatchtxt = "";
                    }

                    if (categoryarr.Length>0 && !string.IsNullOrEmpty(categoryarr[0].ToString()))
                    {
                        Category = categoryarr[0];
                        Categoryavail = true;
                    }
                    else
                    {
                        Categoryavail = false;
                    }
                    Categories = new List<string>();
                    OnPropertyChanged(nameof(Categories));

                    sticketingtypeid = data.ROW_DATA[0].tickettypeid;

                    foreach (var l in langarr)
                    {
                        if (l != Category && !string.IsNullOrEmpty(l))
                        {
                            Categories.Add(l);
                        }

                    }

                    foreach (var c in categoryarr)
                    {
                        if (c !=Category&& !string.IsNullOrEmpty(c))
                        {
                            Categories.Add(c);
                        }
                       
                    }
                    Ispayingavail = true;
                    if (data.ROW_DATA[0].soldout=="1")
                    {
                        Issoldout = true;
                        Isbuying = false;
                       
                    }
                    else
                    {
                        Issoldout = false;
                        Isbuying = true;
                    }

                    if (data.ROW_DATA[0].soldout == "2")
                    {
                        Issoldtype = false;
                    }
                    else
                    {
                        Issoldtype = true;
                    }
                    OnPropertyChanged(nameof(Ispayingavail));
                    OnPropertyChanged(nameof(Issoldout));
                    OnPropertyChanged(nameof(Isbuying));
                    OnPropertyChanged(nameof(Issoldtype));
                    ticketingid = data.ROW_DATA[0].ticketingid;
                    adtitle= Title = data.ROW_DATA[0].title;
                    addlink = data.ROW_DATA[0].newtitleurl + "_event-in_" + Commonsettings.Usercityurl + "_" + eventid;
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].city) && !string.IsNullOrEmpty(data.ROW_DATA[0].address) && !string.IsNullOrEmpty(data.ROW_DATA[0].state) & !string.IsNullOrEmpty(data.ROW_DATA[0].zip))
                    {
                        Address = data.ROW_DATA[0].Venue + ", " + data.ROW_DATA[0].address + ", " + data.ROW_DATA[0].city + ", " + data.ROW_DATA[0].state + ", " + data.ROW_DATA[0].zip;
                    }
                    else if (!string.IsNullOrEmpty(data.ROW_DATA[0].Venue))
                    {
                        Address = data.ROW_DATA[0].Venue;
                    }
                    Date = data.ROW_DATA[0].month + " " + data.ROW_DATA[0].day + ", " + data.ROW_DATA[0].year;
                    Time = data.ROW_DATA[0].time+" ("+data.ROW_DATA[0].timezone+")";
                    Organisername = data.ROW_DATA[0].organisername;
                    Organisercontact = data.ROW_DATA[0].organisermobilenumber;
                    Ticketrangetemp= Ticketrange = data.ROW_DATA[0].minmax;
                    OnPropertyChanged(nameof(Ticketrange));

                    sEventdate = Date +" " +Time;
                    if (!string.IsNullOrEmpty(Organisercontact))
                    {
                        Isavailorganisecontact = true;
                    }
                    else
                    {
                        Isavailorganisecontact = false;
                    }
                    OnPropertyChanged(nameof(Isavailorganisecontact));
                    Isfavorite = data.ROW_DATA[0].shortlist;
                    eventlat = data.ROW_DATA[0].latitude;
                    eventlong = data.ROW_DATA[0].longitude;
                    if (Isfavorite == "0")
                    {
                        Isfavoriteimg = "Favorite.png";
                    }
                    else
                    {
                        Isfavoriteimg = "FavoriteActive.png";
                    }
                    OnPropertyChanged(nameof(Eventdesc));
                    OnPropertyChanged(nameof(Isfavoriteimg));

                    if (data.ROW_DATA[0].ispayment=="0")
                    {
                        Ispaymentvisible = false;
                        Isvisiblecost = true;
                    }
                    else
                    {
                        Ispaymentvisible = true;
                        Isvisiblecost = false;
                    }
                    tSoldout = data.ROW_DATA[0].soldout;
                    var currentpage = GetCurrentPage();

                    string eventdesc = data.ROW_DATA[0].eventdescription;
                    string regex = "<iframe*>(.*?)</iframe>";
                    string regex1 = "<iframe>(.|\n)*?</iframe>";
                   



                    eventdesc = Regex.Replace(eventdesc, regex, "");
                    eventdesc = Regex.Replace(eventdesc, regex1, "");
                    eventdesc= Regex.Replace(eventdesc, @"<iframe.*?/iframe>", "");


                    var customCss = @"<style>img{max-width:100%;height: auto;}p,div{font-size:14px} p{font-family:Montserrat-SemiBold}</style> ";
                    eventdesc = "<html><head>" + customCss+"</head><body>" + eventdesc + "</body></html>";
                    //var htm = new HtmlWebViewSource
                    //{
                    //    Html = customCss + eventdesc
                    //};
                    eventdesc = eventdesc.Replace("http://usimg.sulekhalive.com/", "https://az827626.vo.msecnd.net/");
                    var htm = new HtmlWebViewSource
                    {
                        Html =  eventdesc
                    };

                    
                    Htmlwebviewsource = htm;

                    OnPropertyChanged(nameof(Htmlwebviewsource));
                    
                    
                    OnPropertyChanged(nameof(Eventbanner)); OnPropertyChanged(nameof(Category));
                    OnPropertyChanged(nameof(Categoryavail)); OnPropertyChanged(nameof(Title));
                    OnPropertyChanged(nameof(Address)); OnPropertyChanged(nameof(Date));
                    OnPropertyChanged(nameof(Time)); OnPropertyChanged(nameof(Organisername));
                    OnPropertyChanged(nameof(Organisercontact)); 
                    
                    List<EVENT_TERMS> terms = new List<EVENT_TERMS>();

                    terms.Add(new EVENT_TERMS { term= "All Sales are FINAL\n" });
                    terms.Add(new EVENT_TERMS { term= "All tickets are NON-REFUNDABLE and NON-TRANSFERABLE.\n" });
                    terms.Add(new EVENT_TERMS { term = "The person in whose name the ticket is issued must also be present at the door with valid photo ID.\n" });
                    terms.Add(new EVENT_TERMS { term = "A printout of the order receipt issued by sulekha.com should be produced at the venue.\n" });
                    terms.Add(new EVENT_TERMS { term = "If the event attendee is different from the credit card holder, a copy of the credit card used for ticket purchase must be produced along with the order.\n" });
                    terms.Add(new EVENT_TERMS { term = "Any failure in providing the aforementioned documents may result in denial of admission to the event with no refund.\n" });
                    terms.Add(new EVENT_TERMS { term = "In case of event being cancelled/postponed Sulekha will refund only the face value of the ticket and NOT the service fee.\n" });
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].termcondition))
                    {
                        string moreterm = data.ROW_DATA[0].faq;
                        string[] moretermarr = moreterm.Split(',');

                        foreach (var item in moretermarr)
                        {
                            terms.Add(new EVENT_TERMS { term = item});
                        }

                    }
                    OnPropertyChanged(nameof(GetTerms));
                    GetTerms = terms;

                    List<EVENT_FAQ> faq = new List<EVENT_FAQ>();
                    faq.Add(new EVENT_FAQ { detailblkvisible = false, image = "plus.png", Heading = "How do I purchase tickets?", Content = "There are several options when buying tickets:\n\nYou can purchase any ticket to any events listed on our website. To buy, select the # of tickets you want to purchase in the dropdown of a specific event and follow the instructions.\n\nEmail. Not sure where to go or how to buy tickets? Email us at us.sulekha@sulekha.net.\n\nPhone. You can purchase tickets over the phone 1-512-788-5300" });
                    faq.Add(new EVENT_FAQ { detailblkvisible = false, image = "plus.png", Heading = "What is Sulekha's Refund Policy?", Content = "All sales are final.\n\nNo Refunds will be issued under any circumstance unless an event is canceled or rescheduled.\n\nIf your event is canceled, in most cases you won't need to do a thing; we will inform you of the cancellation and refund the amount you paid for your tickets, on the same payment method used for purchase.\n\nBy purchasing tickets you are accepting these terms.\n\nIf you have any questions, please email us at us.sulekha@sulekha.net or call 1-512-788-5300" });

                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].faq))
                    {
                        string morefaq = data.ROW_DATA[0].faq;
                        string[] morefaqarr = morefaq.Split(new[] { "|qus|" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in morefaqarr)
                        {
                            string[] finalfaqarr = item.Split(new[] { "|ans|" }, StringSplitOptions.RemoveEmptyEntries);
                            faq.Add(new EVENT_FAQ { detailblkvisible = false, image = "plus.png", Heading = finalfaqarr[0].ToString(), Content = finalfaqarr[1].ToString() });
                        }
                    }
                    OnPropertyChanged(nameof(Getfaq));
                    Getfaq = faq;


                    var curbuytickets = currentpage.FindByName<Button>("btnbuytickets");
                    var curregister = currentpage.FindByName<Button>("btnregister");

                    //09-07-2020
                    //if (data.ROW_DATA[0].eventsetup == "1")
                    //{
                    //    curbuytickets.IsVisible = true;
                    //    curregister.IsVisible = false;
                    //}
                    //else
                    //{
                    //    curbuytickets.IsVisible = false;
                    //    curregister.IsVisible = true;
                    //}


                    if (data.ROW_DATA[0].eventsetup == "2")
                    {
                        curbuytickets.IsVisible = false;
                        curregister.IsVisible = true;
                    }
                    else
                    {
                        curbuytickets.IsVisible = false;
                        curregister.IsVisible = false;
                    }
                    //09-07-2020

                    Seventsetup = data.ROW_DATA[0].eventsetup;
                   // curbuytickets.IsVisible = false;
                    // curbuytickets.IsVisible = true;
                    //  curregister.IsVisible = false;


                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].mobilemultidated))
                    {
                        List<Event_Times> evt = new List<Event_Times>();
                        Iseventtimeavail = true;
                        string[] arr1 = data.ROW_DATA[0].mobilemultidated.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in arr1)
                        {
                            string[] k = item.Split(',');
                            string eventdate = k[0].ToString() + " " + k[1].ToString();
                            string eventtime = k[2].ToString() + " (" + k[3].ToString() + ")";
                            evt.Add(new Event_Times { date=eventdate, time=eventtime });
                        }
                         Eventmultitimes = evt;
                        HVScrollGridView eventt = currentpage.FindByName<HVScrollGridView>("eventmultitimelist");
                        eventt.ItemsSource = evt;
                        OnPropertyChanged(nameof(Eventmultitimes));
                    }
                    else
                    {
                        Iseventtimeavail = false;
                    }
                    OnPropertyChanged(nameof(Iseventtimeavail));

                    //Eventmultitimes

                    HVScrollGridView faqview = currentpage.FindByName<HVScrollGridView>("faqdata");
                    faqview.ItemsSource = Getfaq;


                    sEventtype = data.ROW_DATA[0].eventtype;
                    sPostedtype = data.ROW_DATA[0].postedtype;
                    sOrderform = data.ROW_DATA[0].orderform;


                    await Task.Delay(1000);
                   
                }
                }catch(Exception e)
            {
                _Dialog.HideLoading();
            }
        }
     
        public async void Bindeventartis(string eventid)
        {
            try
            {
                _Dialog.ShowLoading("");
                var eventartist = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
                var data = await eventartist.GetEventsartist(eventid);
                if (data.ROW_DATA.Count>0)
                {
                    Isartistavail = true;

                    Eventartist = data.ROW_DATA;
                    OnPropertyChanged(nameof(Eventartist));
                }
                else
                {
                    Isartistavail = false;
                }

                OnPropertyChanged(nameof(Isartistavail));

            }
            catch(Exception e)
            {

            }
        }

        public async void Bindeventtimezone(string eventid)
        {
            try
            {
                _Dialog.ShowLoading("");
                var eventtimezone = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
                var data = await eventtimezone.GetEventtimezones(eventid);
                if (data.ROW_DATA.Count > 0)
                {
                    Iseventtimezoneavail = true;

                    foreach (var item in data.ROW_DATA)
                    {
                        item.datetime = item.day + "," + item.datetime + " " + item.time+"("+item.timezone+")";
                        if (!string.IsNullOrEmpty(item.max))
                        {
                            item.currency = HttpUtility.HtmlDecode(item.htmlcode) + item.min + " - " + HttpUtility.HtmlDecode(item.htmlcode) + item.max;
                        }
                        else if(!string.IsNullOrEmpty(item.min))
                        {
                            item.currency = HttpUtility.HtmlDecode(item.htmlcode) + item.min;
                        }
                       

                    }

                    Eventtimezone = data.ROW_DATA;
                    OnPropertyChanged(nameof(Eventtimezone));
                }
                else
                {
                    Iseventtimezoneavail = false;
                }

                OnPropertyChanged(nameof(Iseventtimezoneavail));

            }
            catch (Exception e)
            {

            }
        }


        public void Sharelink()
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = "",
                Title = adtitle,
                Url = "http://events.sulekha.com/"+addlink
            });
        }
        public async void ClickFavoritecommand()
        {
            try
            {
                var CurrentPage = GetCurrentPage();
                if (Commonsettings.UserPid != "" && Commonsettings.UserPid != "0" && !string.IsNullOrEmpty(Commonsettings.UserPid))
                {
                    _Dialog.ShowLoading("");
                    var Issaved = RestService.For<IEvent_Detail>(Commonsettings.EventsAPI);
                    var data = await Issaved.SaveandremoveBizfavorite(Seventid, Commonsettings.UserPid,  "0", ticketingid);
                    if (data.ROW_DATA.Count > 0)
                    {
                        if (data.ROW_DATA[0].result == "inserted" || data.ROW_DATA[0].result == "shortlisted")
                        {
                            Isfavoriteimg = "FavoriteActive.png";
                            Isfavorite = "1";
                        }
                        else
                        {
                            Isfavoriteimg = "Favorite.png";
                            Isfavorite = "0";
                        }
                    }
                }
                else
                {
                    await CurrentPage.Navigation.PushAsync(new Signin.Views.Login());
                }
                OnPropertyChanged(nameof(Isfavoriteimg));
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }
        public async void ClickReportFlagcommand()
        {
            var CurrentPage = GetCurrentPage();
            if (Commonsettings.UserPid != "" && Commonsettings.UserPid != "0" && !string.IsNullOrEmpty(Commonsettings.UserPid))
            {

                await CurrentPage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Event_Report(Seventid, adtitle, addlink));
            }
            else
            {
                await CurrentPage.Navigation.PushAsync(new Signin.Views.Login());
            }
        }
        public void Clicktoopenmap()
        {
            string uri = "http://maps.google.com/?saddr=" + Commonsettings.UserLat + "," + Commonsettings.UserLong + "&daddr=" + eventlat + "," + eventlong;
            string uri1 = "http://maps.google.com/maps?z=12&q=loc:"+eventlat+"+"+eventlong;
            Device.OpenUri(new Uri(uri1));
        }
        public void Opendetails(EVENT_FAQ list)
        {

            var currentpage = GetCurrentPage();
            HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("faqdata");
            foreach (var p in Getfaq)
            {
                if (p.Heading != list.Heading)
                {
                    p.image = "plus.png";
                    p.detailblkvisible = false;
                }
                else
                {
                    p.image = "minus.png";
                    p.detailblkvisible = true;
                }

            }

           
            myEntry.ItemsSource = null;
            myEntry.ItemsSource = Getfaq;
        }
        string selectedreportid = "";
        public void Clicktoselectreport(EVENT_REPORT_LIST LRT)
        {
            Reportlist = LRT.reportlist;
            selectedreportid = Convert.ToString(LRT.reportid);
            OnPropertyChanged(nameof(Reportlist));
            var CurrentPage =GetCurrentPage();
            var grid = CurrentPage.FindByName<HVScrollGridView>("Reportdata");
            grid.IsVisible = false;
        }
        public bool Reportvalidation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(selectedreportid))
            {
                _Dialog.Toast("Please select the reson");
                return false;
            }
            if (string.IsNullOrEmpty(Reportdesc))
            {
                _Dialog.Toast("Please wirte a description");
                return false;
            }
            

            return result;
        }
        public async void Submitreport()
        {
            if (Reportvalidation())
            {
                try
                {
                    _Dialog.ShowLoading("");
                    var report = RestService.For<IEvent_Detail>(Commonsettings.TechjobsAPI);
                    string pid = Commonsettings.UserPid;
                    if (string.IsNullOrEmpty(pid))
                    {
                        pid = "0";
                    }
                    var data = await report.Sendreport(selectedreportid, "", Reportdesc, Seventid, adtitle, addlink, pid, Commonsettings.UserEmail, "");
                    if (data.ROW_DATA.Count > 0)
                    {
                        if (data.ROW_DATA[0].resultinfo == "inserted success")
                        {
                            _Dialog.Toast("Successfully reported.");
                        }
                        else
                        {
                            _Dialog.Toast("You are already reported!");
                        }
                    }
                    _Dialog.HideLoading();
                }
                catch (Exception e) { _Dialog.HideLoading(); }
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
