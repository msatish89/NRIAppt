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

namespace NRIApp.Events.Features.Listing.ViewModel
{
   public class Events_Filter_VM : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public static string eventtoptag = "";
        private bool _Islistavail = false;

        public bool Islistavail
        {
            get { return _Islistavail; }
            set { _Islistavail = value; }
        }
        private bool _Nolist = false;

        public bool Nolist
        {
            get { return _Nolist; }
            set { _Nolist = value; }
        }
        private bool _Moreloadingfilter = false;
        public bool Moreloadingfilter
        {
            get { return _Moreloadingfilter; }
            set
            {
                _Moreloadingfilter = value;
                OnPropertyChanged(nameof(Moreloadingfilter));
            }
        }
        private bool _Moreloadingloaderfilter = false;
        public bool Moreloadingloaderfilter
        {
            get { return _Moreloadingloaderfilter; }
            set
            {
                _Moreloadingloaderfilter = value;
                OnPropertyChanged(nameof(Moreloadingloaderfilter));
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
        public List<FILTER_LISTING_DATA> Filterevents { get; set; }

        string sMetrourl = "", sCategory = "", sLanguage = "", sStartrprice = "", sEndprice = "", sStartdate="", sEnddate="";

        public ICommand Getdetailpage { get; set; }
        public ICommand Loadmorefiltereventscommand { get; set; }
        public Events_Filter_VM(string metrourl, string category, string language, string startrprice, string endprice, string startdate, string enddate)
        {
            sMetrourl = metrourl;sCategory = category;sLanguage = language;
            sStartrprice = startrprice;sEndprice = endprice;sStartdate = startdate;sEnddate = enddate;
            Getfilterdata(metrourl, category, language, startrprice, endprice, startdate, enddate);
            Getdetailpage = new Command<FILTER_LISTING_DATA>(Redirecttodetailpage);
            Loadmorefiltereventscommand = new Command(Morefilterdata);
        }
        public async void Getfilterdata(string metrourl, string category, string language, string startrprice, string endprice, string startdate, string enddate)
        {
            try
            {
                _Dialog.ShowLoading("");
                 var filterlist = RestService.For<IEvents>(Commonsettings.EventsAPI);
                var data = await filterlist.GetFilterlisting(metrourl, category, language, startrprice, endprice, startdate, enddate,1);
                if (data.ROW_DATA.Count > 0)
                {
                    Islistavail = true;
                    Nolist = false;
                    if (data.ROW_DATA.First().totalrecs > 10)
                    {
                        Moreloadingfilter = true;
                    }
                    else
                    {
                        Moreloadingfilter = false;
                    }
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
                        item.ticketcost = "$ " + item.startprice;
                        eventdate = item.eventdate.Split(' ');
                        item.eventmnth = eventdate[1].ToString();
                        item.eventday = eventdate[0].ToString();
                    }
                    OnPropertyChanged(nameof(Filterevents));
                    Filterevents = data.ROW_DATA;

                    var currentpage = GetCurrentPage();
                    HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listdata");
                    myEntry.ItemsSource = null;
                    myEntry.ItemsSource = Filterevents;

                }
                else
                {
                    Islistavail = false;
                    Nolist = true;
                }
               
                await Task.Delay(2000);
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
                Islistavail = false;
                Nolist = true;
            }
            OnPropertyChanged(nameof(Islistavail));
            OnPropertyChanged(nameof(Nolist));
        }
        int eventfilterlistpageno = 1;
        public async void Morefilterdata()
        {
            Moreloadingfilter = false;
            Moreloadingloaderfilter = true;
            OnPropertyChanged(nameof(Moreloadingloaderfilter));
            OnPropertyChanged(nameof(Moreloadingfilter));
            eventfilterlistpageno = eventfilterlistpageno + 1;

            var filterlist = RestService.For<IEvents>(Commonsettings.EventsAPI);
            var data = await filterlist.GetFilterlisting(sMetrourl, sCategory, sLanguage, sStartrprice, sEndprice, sStartdate, sEnddate, eventfilterlistpageno);
            if (data.ROW_DATA.Count > 0)
            {
               
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
                    else 
                    {
                        item.streamingimg = "zoomIcon2.png";
                    }
                    item.ticketcost = "$ " + item.startprice;
                    eventdate = item.eventdate.Split(' ');
                    item.eventmnth = eventdate[1].ToString();
                    item.eventday = eventdate[0].ToString();
                    Filterevents.Add(item);
                }
              

                var currentpage = GetCurrentPage();
                HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listdata");
                myEntry.ItemsSource = null;
                myEntry.ItemsSource = Filterevents;
                Moreloadingloaderfilter = false;
                if (data.ROW_DATA.First().totalrecs == Filterevents.Count)
                {
                    Moreloadingfilter = false;
                }
                else
                {
                    Moreloadingfilter = true;
                }
                OnPropertyChanged(nameof(Moreloadingfilter));
                OnPropertyChanged(nameof(Moreloadingloaderfilter));

            }
        }

        public async void Redirecttodetailpage(FILTER_LISTING_DATA ELD)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail(ELD.eventid, ELD.title,ELD.ticketingid));
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
