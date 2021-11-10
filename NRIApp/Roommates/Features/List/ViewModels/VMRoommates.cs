using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Refit;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.List.Views;
using NRIApp.Roommates.Features.List.Models;
using NRIApp.Roommates.Features.List.Interface;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Xamarin.RangeSlider.Forms;
using Xamarin.Forms.Extended;
using Plugin.Connectivity;

namespace NRIApp.Roommates.Features.List.ViewModels
{
    public class VMRoommates : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;

       // public int _lastItemAppearedIdx;

        public InfiniteScrollCollection<ListData> roommateslst { get; set; }
        private List<ListData> _roommateslist;
        public List<ListData> RoommatesList
        {
            get { return _roommateslist; }
            set { _roommateslist = value; OnPropertyChanged(nameof(RoommatesList)); }
        }
        public List<RMSaveAdList> AdSaveList { get; set; }
        public List<RMDeleteAdList> AdDeleteList { get; set; }
        public Command<ListData> DetailRMCommand { get; set; }
        public Command<ListData> Listcontactcmd { get; set; }
        public Command TapSearchPage { get; set; }
        public ICommand sortbycommand { get; set; }
        public ICommand filtercommand { get; set; }
        public Command Retrycmd { get; set; }
        public Command Adsavecmd { get; set; }
        public string Categoryurl = " ";
        public string Cityurl = "";
        public string Contentid = "";
        bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(); }
        }
        private bool _Nolisting;
        public bool Nolisting
        {
            get { return _Nolisting; }
            set { _Nolisting = value;OnPropertyChanged(nameof(Nolisting)); }
        }
        private bool _lcflistview;
        public bool lcflistview
        {
            get { return _lcflistview; }
            set { _lcflistview = value; OnPropertyChanged(nameof(lcflistview)); }
        }
        private bool _filter;
        public bool filter
        {
            get { return _filter; }
            set { _filter = value;OnPropertyChanged(nameof(filter)); }
        }
        private bool _nofilter;
        public bool nofilter
        {
            get { return _nofilter; }
            set { _nofilter = value;OnPropertyChanged(nameof(nofilter)); }
        }
        private bool _nointernet;
        public bool nointernet
        {
            get { return _nointernet; }
            set { _nointernet = value;OnPropertyChanged(nameof(nointernet)); }
        }
        public static int listingcnt_RM;
        private string _Availablelistcount;
        public string Availablelistcount
        {
            get { return _Availablelistcount; }
            set { _Availablelistcount = value;OnPropertyChanged(nameof(Availablelistcount)); }
        }


        public static string primarycatval = "";
        public static string gender = "";
        public static string PasContentid ;
        Response response = new Response();
        public static string AdResult;
        public static string cityurl="";
        public VMRoommates(Response scategoryurl, string scityurl)
        {
            cityurl = scityurl;
            roommateslst = new InfiniteScrollCollection<ListData>
            {
                OnLoadMore = async () =>
                  {
                      var RMitems = new List<ListData>();
                      if (roommateslst.Last().totalrecs != roommateslst.Count)
                      {
                          try
                          {
                              var page = roommateslst.Last().pageno + 1;
                              var currentpage = GetCurrentPage();
                              ActivityIndicator RoommatesListingldr = currentpage.FindByName<ActivityIndicator>("listingloader");
                              RoommatesListingldr.IsRunning = true;
                              RoommatesListingldr.IsVisible = true;
                              IsBusy = true;
                              scategoryurl.pageno = page;
                              scategoryurl.nearby = "0";

                              //scategoryurl.userlat = Commonsettings.UserLat;
                              //scategoryurl.userlong = Commonsettings.UserLong;

                              var roomAPI = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                              Models.ListRowData response = await roomAPI.Getroomlist(scategoryurl, scityurl);

                              if (string.IsNullOrEmpty(NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName))
                              {
                                  NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                              }
                              Availablelistcount = response.ROW_DATA.First().totalrecs + " Rooms available in and near " + NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                              var Availablelistcounttxt = currentpage.FindByName<Label>("Availablelistcount");
                              Availablelistcounttxt.Text = response.ROW_DATA.First().totalrecs + " Rooms available in and near " + NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                              listingcnt_RM = response.ROW_DATA.First().totalrecs;
                              if (response.ROW_DATA.Count > 0)
                              {

                                  Nolisting = false;
                                  lcflistview = true;
                                  foreach (var item in response.ROW_DATA)
                                  {
                                      PasContentid = item.Contentid.ToString();
                                      item.orderby = Categoryurl;
                                      if (item.Thumbimgurl == null || item.Thumbimgurl == "")
                                      {
                                          // item.Thumbimgurl = "RoommateNoImage.png";
                                          item.thumburlvisible = false;
                                      }
                                      item.Title = item.Title;
                                      if(item.Hideaddress==0)
                                      {
                                          item.addressvisible = true;
                                          item.City = item.City + " " + "," + item.Statecode;
                                      }
                                      else
                                      {
                                          item.addressvisible = false;
                                      }
                                      if (item.negotiable == "1")
                                      {
                                          item.negotiable = "(negotiable)";
                                      }
                                      else
                                      {
                                          item.negotiable = "";
                                      }
                                      //if (scategoryurl.orderby == "distance" && item.distance != 0)
                                      if (item.distance != 0)
                                      {
                                          item.distVisible = true;
                                          item.distance = Math.Round(item.distance, 1);
                                          item.distancedata = "Distance :" + " " + item.distance + " " + "Miles";
                                      }
                                      else
                                      {
                                          item.distVisible = false;
                                      }
                                      if (!string.IsNullOrEmpty(item.openhousestarttime) && !string.IsNullOrEmpty(item.openhouseendtime) && !string.IsNullOrEmpty(item.openhouseschedule))
                                      {
                                          item.Openhouseschemavisible = true;
                                          item.openhouse = "Open House : " + item.openhouseschedule +" ("+ item.openhousestarttime + " " + "to " + item.openhouseendtime+")";
                                      }
                                      else if (string.IsNullOrEmpty(item.openhousestarttime) || string.IsNullOrEmpty(item.openhouseendtime))
                                      {
                                          if (!string.IsNullOrEmpty(item.openhouseschedule))
                                          {
                                              item.Openhouseschemavisible = true;
                                              item.openhouse = "Open House : " + item.openhouseschedule;
                                          }
                                          else
                                          {
                                              item.Openhouseschemavisible = false;
                                          }
                                      }
                                      if (item.hiderent==0)
                                      {
                                          if (string.IsNullOrEmpty(item.Pricefrom) && item.Pricefrom == "0")
                                          {
                                              item.Pricefrom = "N/A";
                                              item.Pricemode = "";
                                          }
                                          else
                                          {
                                              item.Pricefrom = "$" + item.Pricefrom + " " + item.negotiable;
                                              if (item.Pricemode == "Per Month")
                                                  item.Pricemode = "Month";
                                              item.Pricemode = "/ " + item.Pricemode;
                                          }
                                      }
                                      else
                                      {
                                          item.Pricefrom = "Contact for price";
                                          item.Pricemode = "";
                                      }
                                      

                                      if (item.Gender == "1")
                                          item.Gender = "Male";
                                      if (item.Gender == "2")
                                          item.Gender = "Female";
                                      if (item.Gender == "3")
                                          item.Gender ="Male/Female";
                                      //item.contactname = "Contact : " + item.contactname;
                                      item.contactname = " " + item.contactname;

                                      if (item.adtype == "0")
                                      {
                                          item.adtype = "Room Wanted";
                                          item.availablefromtext = "Move in from";
                                      }
                                      else
                                      {
                                          item.adtype = "Room Offered";
                                          item.availablefromtext = "Available from";
                                      }
                                          

                                      if (item.attachedbaths == "0")
                                          item.attachedbaths = "Separate";
                                      else
                                          item.attachedbaths = "Attached";

                                      //if (item.isadsaved == "0")
                                      //{
                                      //    item.isadsavedimg = "save.png";
                                      //    AdResult = "0";
                                      //}
                                      //else if (item.isadsaved == "1")
                                      //{
                                      //    item.isadsavedimg = "FavoriteActive.png";
                                      //    AdResult = "1";
                                      //}

                                  }
                              }
                              RMitems = response.ROW_DATA;
                              RoommatesListingldr.IsRunning = false;
                              RoommatesListingldr.IsVisible = false;
                              IsBusy = false;
                          }
                          catch (Exception e)
                          {

                          }
                      }
                      return RMitems;
                  }

            };


            Filter_RM.checkedcategoryvalue = scategoryurl.primarycategoryvalue;
            Filter_RM.checkedamenitycategoryvalue = scategoryurl.amenities;
            Filter_RM.bathtype = scategoryurl.attachedbaths;
            Filter_RM.tenant = scategoryurl.categoryname;
            //Filter_RM.availability = scategoryurl.Movedate;
            // scategoryurl.Movedate = Filter_RM.availability;
            Cityurl = scityurl;
            DetailRMCommand = new Command<ListData>(getRMdetailsdata);
            Listcontactcmd = new Command<ListData>(TapOnListContact);
            TapSearchPage = new Command(async () => await ClickSearchPage(scityurl));
            filtercommand = new Command(() =>
           {
               Device.BeginInvokeOnMainThread(() => { dialogs.ShowLoading("", null); });
               Taponfiltercommand(scategoryurl, scityurl);
           });
            //filtercommand = new Command(async () => await Taponfiltercommand(scategoryurl, scityurl));
            sortbycommand = new Command(async () => await TapOnsortbycommand(scategoryurl,scityurl));
            Retrycmd = new Command(() => { Taponretry(scategoryurl, scityurl); });
            Adsavecmd = new Command(() => { TaponAdsave(scategoryurl, scityurl); });
            //Adsavecmd = new Command(TaponAdsave);
            Roomlistpage(scategoryurl, scityurl);
        }
        public async void TaponAdsave(Response categoryurl, string cityurl)
        {
            var roomAPI = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
            Models.ListRowData response = await roomAPI.Getroomlist(categoryurl, cityurl);
            var currentpage = GetCurrentPage();

            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
               
                  
                        if (AdResult == "0")
                        {
                            var savead = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                            Models.RMSaveAD adsaveresponse = await savead.SaveAdData(PasContentid, Commonsettings.UserEmail,Commonsettings.UserPid);
                            AdSaveList = adsaveresponse.ROW_DATA;
                            AdResult = "1";
                    categoryurl.isadsaved = "1";
                        }
                        else
                        {
                            var deletead = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                            Models.RMDeleteAD addeleteresponse = await deletead.DeleteAdData(PasContentid, Commonsettings.UserPid);
                            AdDeleteList = addeleteresponse.ROW_DATA;
                            AdResult = "0";
                    categoryurl.isadsaved = "0";
                }
                Roomlistpage(categoryurl, cityurl);
            }
        }
        public  void Taponretry(Response categoryurl, string cityurl)
        {
            Roomlistpage(categoryurl, cityurl);
        }
        public async Task  ClickSearchPage(string cityurl)
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new Post.Views.RMSearch(cityurl));
        }
        public async void Taponfiltercommand(Response res,string cityurl)
        {
            dialogs.ShowLoading("");
            if(!string.IsNullOrEmpty(Filter_RM.filtercityurl))
            {
                res.City = Filter_RM.filtercity;
                cityurl = Filter_RM.filtercityurl;
                res.userlong = Filter_RM.filterlongitude;
                res.userlat = Filter_RM.filterlatitude;
            }

            if (Filter_RM.pagetype=="filter")
            {

               // Device.BeginInvokeOnMainThread(() => { dialogs.ShowLoading("", null); });
                //if (Filter_RM.checkedcategoryvalue != "" && Filter_RM.checkedcategoryvalue != null)
                //    res.primarycategoryvalue = Filter_RM.checkedcategoryvalue;
                //else 
                    res.primarycategoryvalue = Filter_RM.checkedcategoryvalue;
                if (!string.IsNullOrEmpty(Filter_RM.gender))
                    res.gender = Filter_RM.gender;
               
                if (!string.IsNullOrEmpty(Filter_RM.bathtype))
                    res.attachedbaths = Filter_RM.bathtype;
                if (!string.IsNullOrEmpty(Filter_RM.petpreference))
                    res.petpolicy = Filter_RM.petpreference;
                if (!string.IsNullOrEmpty(Filter_RM.furnish))
                    res.furnishing = Filter_RM.furnish;
                if (!string.IsNullOrEmpty(Filter_RM.vegpreference))
                    res.isveg = Filter_RM.vegpreference;
                
                //if (!string.IsNullOrEmpty(Filter_RM.checkedamenitycategoryvalue))
                //    res.amenities = Filter_RM.checkedamenitycategoryvalue;
                //else
                    res.amenities = Filter_RM.checkedamenitycategoryvalue;
                if (!string.IsNullOrEmpty(Filter_RM.availability))
                    res.availablefrm = Filter_RM.availability;
                if (!string.IsNullOrEmpty(Filter_RM.availability))
                    res.Movedate = Filter_RM.availability;
                if (!string.IsNullOrEmpty(Filter_RM.priceto))
                    res.ExpRent = Filter_RM.priceto;
                if (!string.IsNullOrEmpty(Filter_RM.pricefrm))
                    res.pricefrom = Filter_RM.pricefrm;
                if (!string.IsNullOrEmpty(Filter_RM.tenant))
                    res.categoryname = Filter_RM.tenant;
                if (!string.IsNullOrEmpty(Filter_RM.leaseperiod))
                    res.stayperiod = Filter_RM.leaseperiod;
                if (!string.IsNullOrEmpty(Filter_RM.distancefrm))
                    res.distancefrm = Filter_RM.distancefrm;
                else
                    res.distancefrm = "0";
                if (!string.IsNullOrEmpty(Filter_RM.distanceto))
                    res.distanceto = Filter_RM.distanceto;
                else
                    res.distanceto = "75";

                //RangeSlider pricerange = currentpage.FindByName<RangeSlider>("slidervalue");
                //float rent = (float)Convert.ToDouble(res.ExpRent);
                //if (!string.IsNullOrEmpty(res.ExpRent))
                //    pricerange.UpperValue = rent;
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new Filter_RM(res, cityurl));
                dialogs.HideLoading();
            
            }
            
            else
            {
                Device.BeginInvokeOnMainThread(() => { dialogs.ShowLoading("", null); });
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new Filter_RM(res, cityurl));
                Filter_RM.pagetype = "filter";
                dialogs.HideLoading();
            }
           
           // dialogs.HideLoading();
        }
        public async Task TapOnsortbycommand(Response res, string cityurl)
        {
            //Response response, string cityurl
            var currentpage = GetCurrentPage();
            //if (Filter_RM.checkedcategoryvalue != "" && Filter_RM.checkedcategoryvalue != null)
            //    res.primarycategoryvalue = Filter_RM.checkedcategoryvalue;
            //else

            if (!string.IsNullOrEmpty(Filter_RM.filtercityurl))
            {
                res.City = Filter_RM.filtercity;
                cityurl = Filter_RM.filtercityurl;
                res.userlong = Filter_RM.filterlongitude;
                res.userlat = Filter_RM.filterlatitude;
            }
            res.primarycategoryvalue = Filter_RM.checkedcategoryvalue;
            if (!string.IsNullOrEmpty(Filter_RM.gender))
                res.gender = Filter_RM.gender;
            if (!string.IsNullOrEmpty(Filter_RM.leaseperiod))
                res.stayperiod = Filter_RM.leaseperiod;
            if (!string.IsNullOrEmpty(Filter_RM.bathtype))
                res.attachedbaths = Filter_RM.bathtype;
            if (!string.IsNullOrEmpty(Filter_RM.petpreference))
                res.petpolicy = Filter_RM.petpreference;

            if (!string.IsNullOrEmpty(Filter_RM.furnish))
                res.furnishing = Filter_RM.furnish;
            else
                res.furnishing = "999";
            if (!string.IsNullOrEmpty(Filter_RM.vegpreference))
                res.isveg = Filter_RM.vegpreference;
            if (!string.IsNullOrEmpty(Filter_RM.priceto))
                res.ExpRent = Filter_RM.priceto;
            else
                res.ExpRent = res.ExpRent;
            if (!string.IsNullOrEmpty(Filter_RM.pricefrm))
                res.pricefrom = Filter_RM.pricefrm;
            else
                res.pricefrom = "0";
            //if (Filter_RM.checkedamenitycategoryvalue != "" && Filter_R
            //M.checkedamenitycategoryvalue != null)
            //    res.amenities = Filter_RM.checkedamenitycategoryvalue;
            //else
                res.amenities = Filter_RM.checkedamenitycategoryvalue;

            if (!string.IsNullOrEmpty(Filter_RM.availability))
                res.availablefrm = Filter_RM.availability;

            if (!string.IsNullOrEmpty(Filter_RM.availability))
                res.Movedate = Filter_RM.availability;
            if (!string.IsNullOrEmpty(Filter_RM.distancefrm))
                res.distancefrm = Filter_RM.distancefrm;
            else
                res.distancefrm = "0";
            if (!string.IsNullOrEmpty(Filter_RM.distanceto))
                res.distanceto = Filter_RM.distancefrm;
            else
                res.distanceto = "75";
            if (!string.IsNullOrEmpty(Sortby_RM.orderby))
                res.orderby = Sortby_RM.orderby;
            else
                res.orderby = "ordergroup"; 


             //RangeSlider pricerange = currentpage.FindByName<RangeSlider>("slidervalue");
             //if (!string.IsNullOrEmpty(res.pricefrom))
             //    pricerange.UpperValue = Math.Round(res.pricefrom);
             await currentpage.Navigation.PushModalAsync(new Roommates.Features.List.Views.Sortby_RM(res, cityurl));
        
        }
        public async void TapOnListContact(ListData listadid)
        {
            string adid = listadid.Adid.ToString();
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new Contact_RM(adid));
        }
        public static string roomtype = "";
        public async void getRMdetailsdata(ListData detailrmlist)
        {
            string adid = detailrmlist.Adid.ToString();
            roomtype = detailrmlist.Primarycategoryvalue;
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.DetailRMList(adid, cityurl));
        }
       
        public async void Roomlistpage(Response categoryurl, string cityurl)
        {
           
            var connected = CrossConnectivity.Current.IsConnected;
           if (connected==true)
            {
                try
                {
                    dialogs.ShowLoading("");
                   
                    nointernet = false;
                    if (string.IsNullOrEmpty(categoryurl.furnishing))
                        categoryurl.furnishing = "999";
                    if (string.IsNullOrEmpty(categoryurl.petpolicy))
                        categoryurl.petpolicy = "999";
                   
                    //if (string.IsNullOrEmpty(categoryurl.attachedbaths))
                    //    categoryurl.attachedbaths = "-1";
                    categoryurl.pageno = 1;
                    if(string.IsNullOrEmpty(categoryurl.nearby))
                    {
                        categoryurl.nearby = "0";
                    }
                    if(string.IsNullOrEmpty(categoryurl.userlat))
                    {
                        categoryurl.userlat = Commonsettings.UserLat.ToString();
                    }
                    if (string.IsNullOrEmpty(categoryurl.userlong))
                    {
                        categoryurl.userlong = Commonsettings.UserLong.ToString();
                    }

                    
                    var roomAPI = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                    Models.ListRowData response = await roomAPI.Getroomlist(categoryurl, cityurl);
                    
                    OnPropertyChanged(nameof(Availablelistcount));
                    OnPropertyChanged(nameof(RoommatesList));
                    
                    if (response.ROW_DATA.Count > 0)
                    {
                        Nolisting = false;
                        lcflistview = true;
                        //if (response.ROW_DATA.Count < 2)
                        //{
                        //    filter = false;
                        //    nofilter = true;
                        //}
                        //else
                        //{
                        //    nofilter = false;
                        //    filter = true;
                        //}
                        if (string.IsNullOrEmpty(NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName))
                        {
                            NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        }
                        if(response.ROW_DATA.First().totalrecs==1)
                        {
                            Availablelistcount = response.ROW_DATA.First().totalrecs + " Room for rent in " + NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                        }
                        else
                        {
                            Availablelistcount = response.ROW_DATA.First().totalrecs + " Rooms for rent in  " + NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                        }
                        
                        foreach (var item in response.ROW_DATA)
                        {
                            PasContentid = item.Contentid.ToString();
                            if (item.Thumbimgurl == null || item.Thumbimgurl == "")
                            {
                                // item.Thumbimgurl = "RoommateNoImage.png";
                                item.thumburlvisible = false;
                            }
                            else
                            {
                                item.thumburlvisible = true;
                            }
                            item.Title = item.Title;
                            
                            if (item.Hideaddress == 0)
                            {
                                item.addressvisible = true;
                                item.City = item.City + " " + "," + item.Statecode;
                            }
                            else
                            {
                                item.addressvisible = false;
                            }
                            if (item.negotiable == "1")
                            {
                                item.negotiable = "(negotiable)";
                            }
                            else
                            {
                                item.negotiable = "";
                            }
                           
                            if(string.IsNullOrEmpty(categoryurl.orderby))
                            {
                                categoryurl.orderby = "date";
                            }
                            //if (categoryurl.orderby == "distance" && item.distance!=0 )
                            if (item.distance != 0)
                            {
                                item.distVisible = true;
                                item.distance = Math.Round(item.distance, 1);
                                item.distancedata = "Distance :" + " " + item.distance + " " + "Miles";
                            }
                            else
                            {
                                item.distVisible = false;
                            }
                            if (!string.IsNullOrEmpty(item.openhousestarttime) && !string.IsNullOrEmpty(item.openhouseendtime) && !string.IsNullOrEmpty(item.openhouseschedule))
                            {
                                item.Openhouseschemavisible = true;
                                item.openhouse = "Open House : " + item.openhouseschedule + " (" + item.openhousestarttime + " " + "to " + item.openhouseendtime + ")";
                            }
                            else if (string.IsNullOrEmpty(item.openhousestarttime) || string.IsNullOrEmpty(item.openhouseendtime))
                            {
                                if (!string.IsNullOrEmpty(item.openhouseschedule))
                                {
                                    item.Openhouseschemavisible = true;
                                    item.openhouse = "Open House : " + item.openhouseschedule;
                                }
                                else
                                {
                                    item.Openhouseschemavisible = false;
                                }
                            }
                           
                            if(item.hiderent==0)
                            {
                                if (string.IsNullOrEmpty(item.Pricefrom) && item.Pricefrom == "0")
                                {
                                    item.Pricefrom = "N/A";
                                    item.Pricemode = "";
                                }
                                else
                                {
                                    item.Pricefrom = "$" + item.Pricefrom + " " + item.negotiable;
                                    if (item.Pricemode == "Per Month")
                                        item.Pricemode = "Month";
                                    item.Pricemode = "/ " + item.Pricemode;
                                }
                            }
                            else
                            {
                                item.Pricefrom = "Contact for price";
                                item.Pricemode = "";
                            }
                            if (item.Gender == "1")
                                item.Gender = "Male";
                            if (item.Gender == "2")
                                item.Gender = " " + "Female";
                            if (item.Gender == "3")
                                item.Gender = " " + "Male/Female";
                            //if (item.Gender == "1")
                            //    item.Gender = "Preferred : " + "Male Roommate";
                            //if (item.Gender == "2")
                            //    item.Gender = "Preferred : " + "Female Roommate";
                            //if (item.Gender == "3")
                            //    item.Gender = "Preferred : " + "Male/Female Roommate";

                            //if(string.IsNullOrEmpty(item.contactname) || item.contactname=="")
                            //item.contactname = "Contact : " + item.contactname;
                            item.contactname = " " + item.contactname;


                            if (item.adtype == "0")
                            {
                                item.adtype = "Room Wanted";
                                item.availablefromtext = "Move in from";
                            }
                            else
                            {
                                item.adtype = "Room Offered";
                                item.availablefromtext = "Available from";
                            }

                            if (item.attachedbaths == "0")
                                item.attachedbaths = "Separate";
                            else
                                item.attachedbaths = "Attached";

                            //if (item.isadsaved == "0")
                            //{
                             
                            //    item.isadsavedimg = "save.png";
                            //    AdResult = "0";
                            //}
                            //else if (item.isadsaved == "1")
                            //{
                            //    item.isadsavedimg = "FavoriteActive.png";
                            //    AdResult = "1";
                            //}

                        }


                        var currentpage = GetCurrentPage();
                        var Availablelistcounttxt = currentpage.FindByName<Label>("Availablelistcount");
                        Availablelistcounttxt.IsVisible = true;
                        if (response.ROW_DATA.First().totalrecs == 1)
                        {
                            Availablelistcounttxt.Text = response.ROW_DATA.First().totalrecs + " Room for rent in " + NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                        }
                        else
                        {
                            Availablelistcounttxt.Text = response.ROW_DATA.First().totalrecs + " Rooms for rent in  " + NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                        }
                       // Availablelistcounttxt.Text = response.ROW_DATA.First().totalrecs + " Rooms available in and near " + NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                        ListView RoommatesListing = currentpage.FindByName<ListView>("RoommatesListview");
                        StackLayout stack = currentpage.FindByName<StackLayout>("stacknoblk");
                        stack.IsVisible = false;
                        RoommatesListing.IsVisible = true;
                        RoommatesListing.ItemsSource = roommateslst;
                        OnPropertyChanged(nameof(roommateslst));
                        roommateslst?.AddRange(response.ROW_DATA);



                        //RoommatesListing.ItemAppearing += async (object sender, ItemVisibilityEventArgs e) =>
                        //{
                        //    var currentIdx = roommateslst.IndexOf((ListData)e.Item);

                        //    if (currentIdx > _lastItemAppearedIdx)
                        //    {
                        //        filter = false;
                        //        nofilter = false;
                        //    }
                        //    else
                        //    {
                        //        filter = true;
                        //        nofilter = false;
                        //    }

                        //    _lastItemAppearedIdx = roommateslst.IndexOf((ListData)e.Item);
                        //};
                    }
                    //else
                    //{
                    //    var currentpage = GetCurrentPage();
                    //    var Availablelistcounttxt = currentpage.FindByName<Label>("Availablelistcount");
                        
                    //    categoryurl.nearby = "2";
                    //    if (!string.IsNullOrEmpty(NRIApp.Roommates.Features.Post.ViewModels.VMLCF.RM_metrourl))
                    //    {
                    //        cityurl = NRIApp.Roommates.Features.Post.ViewModels.VMLCF.RM_metrourl;
                    //    }
                    //    else
                    //    {
                    //        cityurl = Commonsettings.Usermetrourl;
                    //    }
                        
                    //    var metroroomAPI = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                    //    Models.ListRowData metroresponse = await roomAPI.Getroomlist(categoryurl, cityurl);
                    //    if (metroresponse.ROW_DATA.Count>0)
                    //    {
                    //        Availablelistcounttxt.Text = "Sorry,there are no listings matching your criteria...You can find related listings below";
                    //        gotolisting(categoryurl, cityurl);
                    //    }
                        else
                        {
                            var currentpage = GetCurrentPage();
                            var Availablelistcounttxt = currentpage.FindByName<Label>("Availablelistcount");
                            Availablelistcounttxt.Text = "Sorry,there are no rooms for rent in"+ NRIApp.Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName;
                        Availablelistcounttxt.IsVisible = false;
                     lcflistview = false;
                            Nolisting = true;
                            ListView RoommatesListing = currentpage.FindByName<ListView>("RoommatesListview");
                            StackLayout stack = currentpage.FindByName<StackLayout>("stacknoblk");
                            RoommatesListing.IsVisible = false;
                            stack.IsVisible = true;
                            RoommatesListing.ItemsSource = response.ROW_DATA;
                        }

                  //  }

                    dialogs.HideLoading();
                }
                catch (Exception e)
                {
                    var connect = CrossConnectivity.Current.IsConnected;

                    if (connect == false)
                    {
                        nointernet = true;
                        dialogs.HideLoading();
                    }
                    else
                    {
                        dialogs.HideLoading();
                        
                    }
                    
                    //dialogs.Toast("Kindly check your internet connection");

                }
            }
           else
            {

                    dialogs.HideLoading();
                    nointernet = true;
            }
          
        }
        public async void gotolisting(Response categoryurl, string cityurl)
        {
            Roomlistpage(categoryurl,cityurl);
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}
