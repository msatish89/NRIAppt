using Acr.UserDialogs;
using NRIApp.Events.Features.Listing.Interface;
using NRIApp.Events.Features.Listing.Model;
using NRIApp.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using Xamarin.RangeSlider.Forms;

namespace NRIApp.Events.Features.Listing.ViewModel
{
    public class Eevents_VM : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public InfiniteScrollCollection<EVENTS_LISTING_DATA> Eventslisting { get; set; }
        public List<EVENTS_LISTING_DATA> Eventslisting1 { get; set; }
        public List<EVENTS_BANNER_DATA> Eventsbanner { get; set; }
        public List<HOT_EVENTS_DATA> Hotevents { get; set; }
        public static List<FILTER_DATA> listcategory = new List<FILTER_DATA>();
        public static List<FILTER_DATA> listlang = new List<FILTER_DATA>();
        public static string checkedcategoryvalue = "";
        public static string checkedlanguagevalue = "";
        public static string selecteddatevalue = "";
        public string topeventstag = "";


        private bool _Eventlist = false;
        public bool Eventlist
        {
            get { return _Eventlist; }
            set
            {
                _Eventlist = value;
                OnPropertyChanged(nameof(Eventlist));
            }
        }
        private bool _Eventlistavail = false;
        public bool Eventlistavail
        {
            get { return _Eventlistavail; }
            set
            {
                _Eventlistavail = value;
                OnPropertyChanged(nameof(Eventlistavail));
            }
        }

        private bool _Eventlistother = false;
        public bool Eventlistother
        {
            get { return _Eventlistother; }
            set
            {
                _Eventlistother = value;
                OnPropertyChanged(nameof(Eventlistother));
            }
        }
        private bool _Nolist = false;
        public bool Nolist
        {
            get { return _Nolist; }
            set
            {
                _Nolist = value;
                OnPropertyChanged(nameof(Nolist));
            }
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
        private bool _Moreloading = false;
        public bool Moreloading
        {
            get { return _Moreloading; }
            set
            {
                _Moreloading = value;
                OnPropertyChanged(nameof(Moreloading));
            }
        }

        private bool _Moreloadingloader = false;
        public bool Moreloadingloader
        {
            get { return _Moreloadingloader; }
            set
            {
                _Moreloadingloader = value;
                OnPropertyChanged(nameof(Moreloadingloader));
            }
        }


        public static int Nearbyevents = 0;
        private string _Nearbyeventimg = "OffButton.png";
        public string Nearbyeventimg
        {
            get { return _Nearbyeventimg; }
            set
            {
                _Nearbyeventimg = value;
                OnPropertyChanged(nameof(Nearbyeventimg));
            }
        }
        private bool _Iscustomavail = false;
        public bool Iscustomavail
        {
            get { return _Iscustomavail; }
            set
            {
                _Iscustomavail = value;
                OnPropertyChanged(nameof(Iscustomavail));
            }
        }
        private bool _Categoryblk = false;
        public bool Categoryblk
        {
            get { return _Categoryblk; }
            set
            {
                _Categoryblk = value;
                OnPropertyChanged(nameof(Categoryblk));
            }
        }
        public int Categoryimgnum = 0;
        public string _Categoryimg = "downarrowGrey.png";
        public string Categoryimg
        {
            get { return _Categoryimg; }
            set
            {
                _Categoryimg = value;
                OnPropertyChanged(nameof(Categoryimg));
            }
        }
        private bool _Languageblk = false;
        public bool Languageblk
        {
            get { return _Languageblk; }
            set
            {
                _Languageblk = value;
                OnPropertyChanged(nameof(Languageblk));
            }
        }
        public int Languageimgnum = 0;
        public string _Languageimg = "downarrowGrey.png";
        public string Languageimg
        {
            get { return _Languageimg; }
            set
            {
                _Languageimg = value;
                OnPropertyChanged(nameof(Languageimg));
            }
        }
        public int Priceimgnum = 0;
        public string _Priceimg = "downarrowGrey.png";
        public string Priceimg
        {
            get { return _Priceimg; }
            set
            {
                _Priceimg = value;
                OnPropertyChanged(nameof(Priceimg));
            }
        }
        private bool _Priceblk = false;
        public bool Priceblk
        {
            get { return _Priceblk; }
            set
            {
                _Priceblk = value;
                OnPropertyChanged(nameof(Priceblk));
            }
        }

        public int Dateimgnum = 0;
        public string _Dateimg = "downarrowGrey.png";
        public string Dateimg
        {
            get { return _Dateimg; }
            set
            {
                _Dateimg = value;
                OnPropertyChanged(nameof(Dateimg));
            }
        }
        private bool _Dateblk = false;
        public bool Dateblk
        {
            get { return _Dateblk; }
            set
            {
                _Dateblk = value;
                OnPropertyChanged(nameof(Dateblk));
            }
        }

        private DateTime _Date = DateTime.Now.Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private DateTime _Todate = DateTime.Now.Date;

        public DateTime Todate
        {
            get { return _Todate; }
            set { _Todate = value; }
        }
        private bool _Ishomebanneravail = false;
        public bool Ishomebanneravail
        {
            get { return _Ishomebanneravail; }
            set
            {
                _Ishomebanneravail = value;
                OnPropertyChanged(nameof(Ishomebanneravail));
            }
        }
        private bool _Islanguageavail = false;
        public bool Islanguageavail
        {
            get { return _Islanguageavail; }
            set
            {
                _Islanguageavail = value;
                OnPropertyChanged(nameof(Islanguageavail));
            }
        }

        private bool _Iscategoryavail = false;
        public bool Iscategoryavail
        {
            get { return _Iscategoryavail; }
            set
            {
                _Iscategoryavail = value;
                OnPropertyChanged(nameof(Iscategoryavail));
            }
        }
        public string _TempEventsearch;
        private string _Eventssearch { get; set; }
        public string Eventssearch
        {
            get { return _Eventssearch; }
            set
            {
                _Eventssearch = value;
                //if (string.IsNullOrEmpty(Eventssearch))
                //{

                //    OnPropertyChanged(nameof(Searchlisting));
                //}
                if (_TempEventsearch != Eventssearch && Eventssearch != "")
                {
                    geteventssearchtext();
                }
            }
        }
        public string _Btntext = "Buy Tickets";
        public string Btntext
        {
            get { return _Btntext; }
            set
            {
                _Btntext = value;
                OnPropertyChanged(nameof(Btntext));
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
        public ICommand Loadmoreeventscommand { get; set; }
        public ICommand Getdetailpage { get; set; }
        public ICommand Getbannerdetailpage { get; set; }
        public ICommand ClickHotevent { get; set; }
        public ICommand Clicktofilter { get; set; }
        public ICommand Clicknerbyevents { get; set; }
        public ICommand Resetcommand { get; set; }
        public ICommand FilterClick { get; set; }
        public ICommand Categoryclick { get; set; }
        public ICommand Languageclick { get; set; }
        public ICommand Priceclick { get; set; }
        public ICommand Dateclick { get; set; }
        public ICommand Clicklistingsearch { get; set; }
        public ICommand Backbtncommand { get; set; }
        public ICommand Eventssearchcommand { get; set; }
        
        public Eevents_VM()
        {
            getHomeBanners();
            Loadmoreeventscommand = new Command(Moreevents);
            Getdetailpage = new Command<EVENTS_LISTING_DATA>(Redirecttodetailpage);
            Getbannerdetailpage = new Command<EVENTS_BANNER_DATA>(Redirecttobannerdetailpage);
            ClickHotevent = new Command(OpenHotevents);
            Clicktofilter = new Command(Openfilters);
            Clicklistingsearch = new Command(Opensearchpage);
            getEventslisting();
            //Eventslisting = new InfiniteScrollCollection<EVENTS_LISTING_DATA>
            //{
            //    OnLoadMore = async () =>
            //    {

            //        var items = new List<EVENTS_LISTING_DATA>();
            //        if (Convert.ToInt32(Eventslisting.First().totalrecs) != Eventslisting.Count)
            //        {


            //            try
            //            {
            //                var page = Eventslisting.Last().pageno + 1;
            //                IsBusy = true;
            //                var primarylisting = RestService.For<IEvents>(Commonsettings.EventsAPI);
            //                var data = await primarylisting.GetEventsListing(Convert.ToString(Commonsettings.UserLat), Convert.ToString(Commonsettings.UserLong), 1);
            //                if (data.ROW_DATA.Count > 0)
            //                {
            //                    Eventlist = true;
            //                    Nolist = false;
            //                    OnPropertyChanged(nameof(Eventlist));
            //                    OnPropertyChanged(nameof(Nolist));
            //                    if (data.ROW_DATA.First().totalrecs > 10)
            //                    {
            //                        Moreloading = true;
            //                    }
            //                    else
            //                    {
            //                        Moreloading = false;
            //                    }
            //                    foreach (var item in data.ROW_DATA)
            //                    {
            //                        string[] eventdate = null;
            //                        item.fulladdress = item.venue + ", " + item.cityname + ", " + item.statecode;
            //                        item.ticketcost = "$" + item.startprice;
            //                        if (item.soldout == "1")
            //                        {
            //                            item.issold = true;
            //                            item.Isbuyavail = false;
            //                        }
            //                        else
            //                        {
            //                            item.issold = false;
            //                            item.Isbuyavail = true;
            //                        }
            //                        if (item.soldout == "2")
            //                        {
            //                            item.soldtype = false;
            //                        }
            //                        else
            //                        {
            //                            item.soldtype = true;
            //                        }

            //                        if (!string.IsNullOrEmpty(item.category))
            //                        {
            //                            item.categoryavail = true;
            //                        }
            //                        else
            //                        {
            //                            item.categoryavail = false;
            //                        }
            //                        eventdate = item.eventdate.Split(' ');
            //                        item.eventmnth = eventdate[1].ToString();
            //                        item.eventday = eventdate[0].ToString();
            //                    }

            //                    OnPropertyChanged(nameof(Eventslisting));

            //                    // OnPropertyChanged(nameof(Eventslisting1));
            //                    Eventslisting1 = data.ROW_DATA;
            //                    var currentpage =GetCurrentPage();
            //                    HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listdata");
            //                    myEntry.ItemsSource = null;
            //                    myEntry.ItemsSource = Eventslisting1;
            //                }
            //                items = data.ROW_DATA;
            //                IsBusy = false;

            //                //var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            //                //var list1 = currentpage.FindByName<ListView>("listdata");
            //                //list1.ItemsSource = null;
            //                //list1.ItemsSource = items;
            //            }
            //            catch (Exception ex) { }
            //        }
            //        return items;
            //    }
            //};


        }
        public Eevents_VM(string data, string tagtype)
        {
            if (data == "filter")
            {

                Clicknerbyevents = new Command(Checknearby);
                Resetcommand = new Command(Clicktoreset);
                FilterClick = new Command(FilterClicked);
                Categoryclick = new Command(Categoryblkclick);
                Languageclick = new Command(Languageblkclick);
                Priceclick = new Command(Priceblkclick);
                Dateclick = new Command(Dateblkclick);
                Backbtncommand = new Command(BackbtncommandClick);
                Getcategory();
                Getlanguages();
            }
            else if (data == "hotevents")
            {
                Gethotevents(tagtype);
                Getdetailpage = new Command<HOT_EVENTS_DATA>(Redirecttodetailpagehotevents);
            }
            else if (data == "search")
            {
                Binddefaultvalues();
                Backbtncommand = new Command(BackbtncommandClick);
                Eventssearchcommand = new Command<EVENTS_LISTING_DATA>(async (ELD) => await Redirectsearchtodetailpage(ELD));
            }

        }

        public async void geteventssearchtext()
        {
            try
            {
                var events = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await events.Eventsearchajax(Eventssearch);
                if (data.ROW_DATA.Count > 0)
                {

                    foreach (var i in data.ROW_DATA)
                    {
                            i.header = "Search Events";
                    }
                    var CurrentPage =GetCurrentPage();
                    var sorted = from srch in data.ROW_DATA group srch by srch.header into searchGroup select new Grouping<string, EVENTS_LISTING_DATA>(searchGroup.Key, searchGroup);
                    var entry = CurrentPage.FindByName<ListView>("searchlistview");
                    entry.ItemsSource = sorted;
                }

            }
            catch(Exception e)
            {

            }
        }
        public async void Binddefaultvalues()
        {
            try
            {
                _Dialog.ShowLoading("");
                var primarylisting = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await primarylisting.GetEventsListing(Convert.ToString(Commonsettings.UserLat), Convert.ToString(Commonsettings.UserLong), 1);
                if (data.ROW_DATA.Count > 0)
                {
                    
                    foreach (var item in data.ROW_DATA)
                    {
                        item.header = "Top Events";
                    }                   
                    var sorted = from srch in data.ROW_DATA group srch by srch.header into searchGroup select new Grouping<string, EVENTS_LISTING_DATA>(searchGroup.Key, searchGroup);
                    var currentpage = GetCurrentPage();
                    ListView myEntry = currentpage.FindByName<ListView>("searchlistview");
                    myEntry.ItemsSource = sorted;
                }
            }
            catch (Exception e)
            {

            }
            await Task.Delay(3000);
            _Dialog.HideLoading();
        }
        public async void BackbtncommandClick()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public async void Opensearchpage()
        {
            var currentpage = GetCurrentPage();
           // currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Listing.Views.Search());
        }
        public async void Getcategory()
        {
            try
            {
                _Dialog.ShowLoading("");
                var category = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await category.Getcategory(Convert.ToString(Commonsettings.Usermetrourl));

                if (data.ROW_DATA.Count > 0 && data.ROW_DATA != null)
                {
                    Iscategoryavail = true;
                    Eevents_VM.listcategory = data.ROW_DATA;
                    var currentpage = GetCurrentPage();
                    StackLayout layoutinnercheckbox = currentpage.FindByName<StackLayout>("layoutinnercheckbox");
                    foreach (var item in Eevents_VM.listcategory)
                    {
                        BoxView boxx = new BoxView();
                        boxx.BackgroundColor = Color.FromHex("e0e0e0");
                        boxx.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                        //chbx.OnImg = "CheckBoxChecked.png";
                        //chbx.OffImg = "CheckBoxUnChecked.png";
                        chbx.Text = item.items;
                        //chbx.ShowLabel = true;
                        chbx.CheckChanged += Chb_CategoryCheckChanged;
                        chbx.ClassId = item.itemscss;
                        chbx.Padding = 5;
                        layoutinnercheckbox.Children.Add(chbx);
                        layoutinnercheckbox.Children.Add(boxx);
                    }


                }
                else
                {
                    Iscategoryavail = false;
                }
                OnPropertyChanged(nameof(Iscategoryavail));
            }
            catch (Exception e)
            {
               
            }
        }
        private void Chb_CategoryCheckChanged(object sender, EventArgs e)
        {
            var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (category.IsChecked==true)
            {
                if (Eevents_VM.checkedcategoryvalue != null && Eevents_VM.checkedcategoryvalue != "")
                {
                    if (!Eevents_VM.checkedcategoryvalue.Contains(category.ClassId))
                    {
                        Eevents_VM.checkedcategoryvalue += category.ClassId + ",";
                    }
                }
                else
                {

                    Eevents_VM.checkedcategoryvalue += category.ClassId + ",";
                }

            }
            else
            {
                string[] indexvalue = Eevents_VM.checkedcategoryvalue.Split(',');
                int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                Eevents_VM.checkedcategoryvalue = string.Join(",", indexvalue);
            }

        }
        public async void Getlanguages()
        {
            try
            {

                var languages = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await languages.Getlanguages(Convert.ToString(Commonsettings.Usermetrourl));

                if (data.ROW_DATA.Count > 0 && data.ROW_DATA != null)
                {
                    Islanguageavail = true;
                    Eevents_VM.listlang = data.ROW_DATA;
                    var currentpage = GetCurrentPage();
                    StackLayout layoutlanguage = currentpage.FindByName<StackLayout>("layoutlanguage");
                    foreach (var item in Eevents_VM.listlang)
                    {
                        BoxView boxx = new BoxView();
                        boxx.BackgroundColor = Color.FromHex("e0e0e0");
                        boxx.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                        //chbx.OnImg = "CheckBoxChecked.png";
                        //chbx.OffImg = "CheckBoxUnChecked.png";
                        chbx.Text = item.items;
                        //chbx.ShowLabel = true;
                        chbx.CheckChanged += Chb_LanguageCheckChanged;
                        chbx.ClassId = item.itemscss;
                        chbx.Padding = 5;
                        layoutlanguage.Children.Add(chbx);
                        layoutlanguage.Children.Add(boxx);
                    }


                }
                else
                {
                    Islanguageavail = false;
                }
                OnPropertyChanged(nameof(Islanguageavail));
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
               
            }
        }

        private void Chb_LanguageCheckChanged(object sender, EventArgs e)
        {
            var lang = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (lang.IsChecked ==true)
            {
                if (Eevents_VM.checkedlanguagevalue != null && Eevents_VM.checkedlanguagevalue != "")
                {
                    if (!Eevents_VM.checkedlanguagevalue.Contains(lang.ClassId))
                    {
                        Eevents_VM.checkedlanguagevalue += lang.ClassId + ",";
                    }
                }
                else
                {

                    Eevents_VM.checkedlanguagevalue += lang.ClassId + ",";
                }

            }
            else
            {
                string[] indexvalue = Eevents_VM.checkedlanguagevalue.Split(',');
                int numIndex = Array.IndexOf(indexvalue, lang.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                Eevents_VM.checkedlanguagevalue = string.Join(",", indexvalue);
            }

        }
        public async void getEventslisting()
        {
            try
            {
                _Dialog.ShowLoading("");
                var primarylisting = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await primarylisting.GetEventsListing(Convert.ToString(Commonsettings.UserLat), Convert.ToString(Commonsettings.UserLong), 1);
                if (data.ROW_DATA.Count > 0)
                {
                    Eventlistavail = true;

                  Events_Filter_VM.eventtoptag = data.ROW_DATA.First().tag;
                    if (string.IsNullOrEmpty(Events_Filter_VM.eventtoptag))
                    {
                        Eventlist = false;
                        Eventlistother = true;

                    }
                    else
                    {
                        Eventlist = true;
                        Eventlistother = false;
                    }
                   
                    Nolist = false;
                    OnPropertyChanged(nameof(Nolist));
                    OnPropertyChanged(nameof(Eventlist));
                    OnPropertyChanged(nameof(Eventlistother));
                    if (data.ROW_DATA.First().totalrecs>10)
                    {
                        Moreloading = true;
                    }
                    else
                    {
                        Moreloading = false;
                    }
                    foreach (var item in data.ROW_DATA)
                    {
                        string[] eventdate = null;
                        if (!string.IsNullOrEmpty(item.cityname) && !string.IsNullOrEmpty(item.statecode))
                        {
                            item.fulladdress = item.venue + ", " + item.cityname + ", " + item.statecode;
                        }else if (!string.IsNullOrEmpty(item.venue))
                        {
                            item.fulladdress = item.venue;
                        }
                        item.ticketcost = "$" + item.startprice;
                        if (item.soldout == "1")
                        {
                            item.issold = true;
                            item.Isbuyavail = false;
                        }
                        else
                        {
                            item.issold = false;
                            item.Isbuyavail = true;
                        }
                        if (item.soldout=="2")
                        {
                            item.soldtype = false;
                        }
                        else
                        {
                            item.soldtype = true;
                        }
                        if (item.startprice == "0" || item.startprice == "0.00")
                        {
                            item.soldtype = false;
                        }

                        if (!string.IsNullOrEmpty(item.category))
                        {
                            item.categoryavail = true;
                        }
                        else
                        {
                            item.categoryavail = false;
                        }
                        eventdate = item.eventdate.Split(' ');
                        item.eventmnth = eventdate[1].ToString();
                        item.eventday = eventdate[0].ToString();
                        if (string.IsNullOrEmpty(item.btntext))
                        {
                            item.btntext = "Buy Tickets";
                        }
                        else
                        {
                            item.btntext = item.btntext;
                        }
                        if (item.venue == "Live Streaming")
                        {
                            item.islivestreamavail = true;
                        }
                        else
                        {
                            item.islivestreamavail = false;
                        }
                        if (item.streaming.ToLower().Contains("facebook"))
                        {
                            item.streamingimg = "facebook.png";
                        }
                        else if (item.streaming.ToLower().Contains("youtube"))
                        {
                            item.streamingimg = "youtube.png";
                        }
                        else if (item.streaming.ToLower().Contains("zoom"))
                        {
                           
                            item.streamingimg = "zoomIcon2.png";
                        }
                        else
                        {
                            item.streaming = "";
                        }

                    }

                    OnPropertyChanged(nameof(Eventslisting));
                   // Eventslisting?.AddRange(data.ROW_DATA);
                    // OnPropertyChanged(nameof(Eventslisting1));
                    Eventslisting1 = data.ROW_DATA;
                    var currentpage =GetCurrentPage();
                    HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listdata");
                    myEntry.ItemsSource = null;
                    myEntry.ItemsSource = Eventslisting1;
                }
                else
                {
                    Eventlistavail = false;
                    Eventlist = false;
                    Nolist = true;
                    OnPropertyChanged(nameof(Eventlist));
                    OnPropertyChanged(nameof(Nolist));
                }

               
               



            }
            catch (Exception e)
            {
               
            }
            await Task.Delay(3000);
            _Dialog.HideLoading();
        }
        int eventlistpageno = 1;
        public async void Moreevents()
        {
            eventlistpageno = eventlistpageno + 1;
            try
            {
                Moreloading = false;
                Moreloadingloader = true;
                OnPropertyChanged(nameof(Moreloadingloader));
                var primarylisting = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await primarylisting.GetEventsListing(Convert.ToString(Commonsettings.UserLat), Convert.ToString(Commonsettings.UserLong), eventlistpageno);
                if (data.ROW_DATA.Count > 0)
                {
                    //foreach (var item in data.ROW_DATA)
                    //{
                    //    string[] eventdate = null;
                    //    item.fulladdress = item.venue + ", " + item.cityname + ", " + item.statecode;
                    //    item.ticketcost = "$" + item.startprice;
                    //    if (item.soldout == "1")
                    //    {
                    //        item.issold = false;
                    //    }
                    //    else
                    //    {
                    //        item.issold = true;
                    //    }
                    //    if (!string.IsNullOrEmpty(item.category))
                    //    {
                    //        item.categoryavail = true;
                    //    }
                    //    else
                    //    {
                    //        item.categoryavail = false;
                    //    }
                    //    eventdate = item.eventdate.Split(' ');
                    //    item.eventmnth = eventdate[1].ToString();
                    //    item.eventday = eventdate[0].ToString();
                    //    Eventslisting1.Add(item);
                    //}
                    foreach (var item in data.ROW_DATA)
                    {
                        string[] eventdate = null;
                        if (!string.IsNullOrEmpty(item.cityname) && !string.IsNullOrEmpty(item.statecode))
                        {
                            item.fulladdress = item.venue + ", " + item.cityname + ", " + item.statecode;
                        }
                        else if (!string.IsNullOrEmpty(item.venue))
                        {
                            item.fulladdress = item.venue;
                        }
                        item.ticketcost = "$" + item.startprice;
                        if (item.soldout == "1")
                        {
                            item.issold = true;
                            item.Isbuyavail = false;
                        }
                        else
                        {
                            item.issold = false;
                            item.Isbuyavail = true;
                        }
                        if (item.soldout == "2")
                        {
                            item.soldtype = false;
                        }
                        else
                        {
                            item.soldtype = true;
                        }

                        if(item.startprice=="0" || item.startprice == "0.00")
                        {
                            item.soldtype = false;
                        }
                        if (!string.IsNullOrEmpty(item.category))
                        {
                            item.categoryavail = true;
                        }
                        else
                        {
                            item.categoryavail = false;
                        }
                        if (item.venue == "Live Streaming")
                        {
                            item.islivestreamavail = true;
                        }
                        else
                        {
                            item.islivestreamavail = false;
                        }
                        if (item.streaming.ToLower().Contains("facebook"))
                        {
                            item.streamingimg = "facebook.png";
                        }
                        else if (item.streaming.ToLower().Contains("youtube"))
                        {
                            item.streamingimg = "youtube.png";
                        }
                        else if (item.streaming.ToLower().Contains("zoom"))
                        {
                            item.streamingimg = "zoomIcon2.png";
                        }
                        else
                        {
                            item.streaming = "";
                        }
                        eventdate = item.eventdate.Split(' ');
                        item.eventmnth = eventdate[1].ToString();
                        item.eventday = eventdate[0].ToString();
                        Eventslisting1.Add(item);
                    }
                    // OnPropertyChanged(nameof(Eventslisting));   Eventslisting1.Add(item);
                    OnPropertyChanged(nameof(Eventslisting1));
                    var currentpage = GetCurrentPage();
                    HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listdata");
                    myEntry.ItemsSource = null;
                    myEntry.ItemsSource = Eventslisting1;
                    Moreloadingloader = false;
                    OnPropertyChanged(nameof(Moreloading));
                    OnPropertyChanged(nameof(Moreloadingloader));
                    if (data.ROW_DATA.First().totalrecs == Eventslisting1.Count)
                    {
                        Moreloading = false;
                    }
                    else
                    {
                        Moreloading = true;
                    }
                    
                }
            }catch(Exception e)
            {

            }
        }

        public async void getHomeBanners()
        {
            try
            {
                _Dialog.ShowLoading("", null);
                var primarylisting = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await primarylisting.GetEventshomebanners(Convert.ToString(Commonsettings.Usermetrourl));
                if (data.ROW_DATA.Count > 0)
                {
                    OnPropertyChanged(nameof(Eventsbanner));
                    Eventsbanner = data.ROW_DATA;
                    Ishomebanneravail = true;
                }
                else
                {
                    Ishomebanneravail = false;
                }
                OnPropertyChanged(nameof(Ishomebanneravail));
               
            }
            catch(Exception e)
            {
               
            }

        }

        public async void Openfilters()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Listing.Views.Event_Filter());
        }

        public void Checknearby()
        {
            if (Nearbyevents==1)
            {
                Nearbyeventimg = "OffButton.png";
                Nearbyevents = 0;
            }
            else
            {
                Nearbyeventimg = "OnButton.png";
                Nearbyevents = 1;
            }
            OnPropertyChanged(nameof(Nearbyeventimg));
        }

        public void Categoryblkclick()
        {
            if (Categoryimgnum==0)
            {
                Categoryimg = "minusGrey.png";
                Categoryimgnum = 1;
                Categoryblk = true;
            }
            else
            {
                Categoryimg = "downarrowGrey.png";
                Categoryimgnum = 0;
                Categoryblk = false;
            }
            OnPropertyChanged(nameof(Categoryimg));
            OnPropertyChanged(nameof(Categoryblk));
        }
        public void Languageblkclick()
        {
            if (Languageimgnum == 0)
            {
                Languageimg = "minusGrey.png";
                Languageimgnum = 1;
                Languageblk = true;
            }
            else
            {
                Languageimg = "downarrowGrey.png";
                Languageimgnum = 0;
                Languageblk = false;
            }
            OnPropertyChanged(nameof(Languageimg));
            OnPropertyChanged(nameof(Languageblk));
        }
        public void Priceblkclick()
        {
            if (Priceimgnum == 0)
            {
                Priceimg = "minusGrey.png";
                Priceimgnum = 1;
                Priceblk = true;
            }
            else
            {
                Priceimg = "downarrowGrey.png";
                Priceimgnum = 0;
                Priceblk = false;
            }
            OnPropertyChanged(nameof(Priceimg));
            OnPropertyChanged(nameof(Priceblk));
        }
        public void Dateblkclick()
        {
            if (Dateimgnum == 0)
            {
                Dateimg = "minusGrey.png";
                Dateimgnum = 1;
                Dateblk = true;
            }
            else
            {
               Dateimg = "downarrowGrey.png";
                Dateimgnum = 0;
                Dateblk = false;
            }
            OnPropertyChanged(nameof(Dateimg));
            OnPropertyChanged(nameof(Dateblk));
        }


        public async void Redirecttodetailpage(EVENTS_LISTING_DATA ELD)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail(ELD.eventid,ELD.title,ELD.ticketingid));
        }
        public async void Redirecttobannerdetailpage(EVENTS_BANNER_DATA ELD)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail(ELD.eventid, ELD.title, ""));
        }
        public async void Redirecttodetailpagehotevents(HOT_EVENTS_DATA ELD)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail(ELD.eventid,ELD.eventtitle,""));
        }
        public async Task Redirectsearchtodetailpage(EVENTS_LISTING_DATA ELD)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail(ELD.contentid, ELD.title,ELD.ticketingid));
        }

        public async void OpenHotevents()
        {
            _Dialog.ShowLoading("");
            var currentpage = GetCurrentPage();
           
            await currentpage.Navigation.PushAsync(new Events.Features.Listing.Views.Hotevents(Events_Filter_VM.eventtoptag));

        }
        public async void Gethotevents(string tagtype)
        {
            try
            {
                _Dialog.ShowLoading("");
                var hotevents = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await hotevents.GetHotEvents(Convert.ToString(Commonsettings.Usermetrourl),Convert.ToString(Commonsettings.Usercityurl),tagtype,"1");
                if (data.ROW_DATA.Count > 0)
                {
                    OnPropertyChanged(nameof(Hotevents));
                    foreach (var item in data.ROW_DATA)
                    {
                        item.eventaddress = item.venuename + ", " + item.city + ", " + item.statecode;
                    }
                    Hotevents = data.ROW_DATA;                  
                }
            }
            catch(Exception e)
            {
               
            }
           await Task.Delay(3000);
           _Dialog.HideLoading();
        }

        public async void FilterClicked()
        {
            
            var currentpage = GetCurrentPage();
            RangeSlider slider = currentpage.FindByName<RangeSlider>("slidervalue");
            string startrprice=Convert.ToString(slider.LowerValue );
            string endprice = Convert.ToString(slider.UpperValue) ;
            string metrourl = "",startdate="",enddate="",category="",language="";
            category = Eevents_VM.checkedcategoryvalue; language = Eevents_VM.checkedlanguagevalue;
            if (startrprice == "0" && endprice == "100000")
            {
                startrprice = endprice = "";
            }
            if (Nearbyevents!=0)
            {
                metrourl = Commonsettings.Usermetrourl;
            }
            if (selecteddatevalue=="1")
            {
                startdate=enddate = DateTime.Now.ToString("yyyy.MM.dd"); 
            }
            else if (selecteddatevalue=="2")
            {
                startdate = enddate = DateTime.Now.AddDays(1).ToString("yyyy.MM.dd");
            }
            else if (selecteddatevalue == "3")
            {
                startdate = Date.ToString("yyyy.MM.dd");
                enddate = Todate.ToString("yyyy.MM.dd");
            }
            else
            {
                startdate = "";
                enddate = "";
            }
            if (string.IsNullOrEmpty(metrourl)&& string.IsNullOrEmpty(category) && string.IsNullOrEmpty(language) && string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate)&&string.IsNullOrEmpty(startrprice)&&string.IsNullOrEmpty(endprice))
            {
                _Dialog.Toast("Please choose at least one");
            }
            else{
                await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Listing.Views.Filter_Listing(metrourl, category, language, startrprice, endprice, startdate, enddate));
            }
           

        }

        public void Clicktoreset()
        {
            checkedcategoryvalue = "";
            checkedlanguagevalue = "";
            Nearbyeventimg = "OffButton.png";
            Nearbyevents = 0;
            OnPropertyChanged(nameof(Nearbyeventimg));
            var currentpage = GetCurrentPage();
            RangeSlider slider = currentpage.FindByName<RangeSlider>("slidervalue");
            slider.LowerValue = 0;
            slider.UpperValue = 100000;
            Frame day1 = currentpage.FindByName<Frame>("day1");
            Frame day2 = currentpage.FindByName<Frame>("day2");
            Frame day3 = currentpage.FindByName<Frame>("day3");
            selecteddatevalue = "";
            day1.BackgroundColor = day2.BackgroundColor = day3.BackgroundColor =  Color.FromHex("#e6e5e5");
            Eevents_VM.checkedcategoryvalue = Eevents_VM.checkedlanguagevalue="";
            StackLayout stackcategory = currentpage.FindByName<StackLayout>("layoutinnercheckbox");
            Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;

            foreach (var item in stackcategory.Children)
            {
                if (item.GetType() == chbx.GetType())
                {
                    var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                    if (cat.IsChecked == true)
                    {
                        cat.IsChecked = false;
                    }
                }

            }
            StackLayout layoutlanguage = currentpage.FindByName<StackLayout>("layoutlanguage");
            foreach (var item in layoutlanguage.Children)
            {
                if (item.GetType() == chbx.GetType())
                {
                    var lan = item as Plugin.InputKit.Shared.Controls.CheckBox;
                    
                    if (lan.IsChecked == true)
                    {
                        lan.IsChecked = false;
                    }
                }

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
