using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using Refit;
using NRIApp.Rentals.Features.List.Models;
using NRIApp.Rentals.Features.List.Interface;
using NRIApp.Rentals.Features.List.Views;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Models;
using System.Windows.Input;
using System.Threading.Tasks;
using NRIApp.Rentals.Features.Post.Views;
using Xamarin.Forms.Extended;
using Plugin.Connectivity;

namespace NRIApp.Rentals.Features.List.ViewModels
{
    public class VMRentals:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public Command<ListRentalCategoryData> DetailCommand { get; set; }
        public InfiniteScrollCollection<ListRentalCategoryData> rentalslst { get; set; }
        public List<ListRentalCategoryData> RentalsList { get; set; }
        public List<ListRentalCategoryData> DetailListData { get; set; }
        public Command<ListRentalCategoryData> Listcontactcmd { get; set; }
        public ICommand sortbycommand { get; set; }
        public ICommand filtercommand { get; set; }
        public Command Retrycmd { get; set; }
        public Command TapSearchPage { get; set; }
        public string Categoryurl = "";
        public string Cityurl = "";
       
        private bool _isbusy=false;
        public bool Isbusy
        {
            get { return _isbusy; }
            set { _isbusy = value; OnPropertyChanged(); }
        }
        private bool _sqftvisible;
        public bool sqftvisible
        {
            get { return _sqftvisible; }
            set { _sqftvisible = value;OnPropertyChanged(); }
        }
        private bool _rentvisible;
        public bool rentvisible
        {
            get { return _rentvisible; }
            set { _rentvisible = value; OnPropertyChanged(); }
        }
        private bool _Nolisting;
        public bool Nolisting
        {
            get { return _Nolisting; }
            set { _Nolisting = value; OnPropertyChanged(nameof(Nolisting)); }
        }
        private bool _lcflistview;
        public bool lcflistview
        {
            get { return _lcflistview; }
            set { _lcflistview = value; OnPropertyChanged(nameof(lcflistview)); }
        }
        bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(); }
        }
        private bool _nointernet;
        public bool nointernet
        {
            get { return _nointernet; }
            set { _nointernet = value; OnPropertyChanged(nameof(nointernet)); }
        }
        private string _Availablelistcount;
        public string Availablelistcount
        {
            get { return _Availablelistcount; }
            set { _Availablelistcount = value;OnPropertyChanged(nameof(Availablelistcount)); }
        }
        public static string Scityurl = "";
        public VMRentals(RTResponse res,string cityurl)
        {
            Scityurl = cityurl;
            rentalslst = new InfiniteScrollCollection<ListRentalCategoryData>
            {
                OnLoadMore = async () =>
                {
                    var RTitems = new List<ListRentalCategoryData>();
                    if (rentalslst.Last().totalrecs != rentalslst.Count)
                    {
                        try
                        {
                            var page = rentalslst.Last().pageno + 1;
                            
                            var currentpage = GetCurrentPage();
                            ActivityIndicator Rentalslistingldr = currentpage.FindByName<ActivityIndicator>("listingloader");
                            Rentalslistingldr.IsRunning = true;
                            Rentalslistingldr.IsVisible = true;
                            IsBusy = true;
                            res.pageno = page;
                            res.nearby = "1";
                            //res.userlat = Commonsettings.UserLat;
                            //res.userlong = Commonsettings.UserLong;
                            var Listofrentaldata = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                            ListRentalCategory responsedata = await Listofrentaldata.GetRentalList(res, cityurl);
                            if(string.IsNullOrEmpty(NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName))
                            {
                                NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                            }
                            // Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " Listings in and near " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                            //if (res.categoryname == "Homes" || res.categoryname == "Houses" || res.categoryname == "Hostels")
                            //{
                            //    if (responsedata.ROW_DATA.First().totalrecs == 1)
                            //    {
                            //        Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " " + res.categoryname.Remove(res.categoryname.Length - 1, 1) + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                            //    }
                            //    else
                            //    {
                            //        Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " " + res.categoryname + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                            //    }
                            //}
                            //else if (res.categoryname == "Single Family Home" || res.categoryname == "Apartment" || res.categoryname == "Condo" || res.categoryname == "Office Space" || res.categoryname == "Town house" || res.categoryname == "Retail Outlet" || res.categoryname == "Basement Apartment" || res.categoryname == "Parking Garage")
                            //{
                            //    if (responsedata.ROW_DATA.First().totalrecs == 1)
                            //    {
                            //        Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " " + res.categoryname + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                            //    }
                            //    else
                            //    {
                            //        Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " " + res.categoryname + "s" + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                            //    }
                            //}
                            //if (res.primarycategoryvalue.Contains(",") || res.categoryname == "Others")
                            //{
                                if (responsedata.ROW_DATA.First().totalrecs == 1)
                                {
                                    Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " Property for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                                }
                                else
                                {
                                    Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " Properties for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                                }
                           // }
                            //OnPropertyChanged(nameof(rentalslst));
                            if (responsedata.ROW_DATA.Count > 0)
                            {
                                Nolisting = false;
                                lcflistview = true;
                                foreach (var results in responsedata.ROW_DATA)
                                {
                                    if (results.Thumbimgurl == null || results.Thumbimgurl == "" || results.Thumbimgurl == "0")
                                    {
                                        //results.Thumbimgurl = "RoommateNoImage.png";
                                        results.thumburlvisible = false;
                                    }
                                    else
                                    {
                                        results.thumburlvisible = true;
                                    }
                                    if (results.Roomtype == "1")
                                        results.Roomtype = "Single Family Home";
                                    if (results.Roomtype == "2")
                                        results.Roomtype = "Apartment";
                                    if (results.Roomtype == "3")
                                        results.Roomtype = "Condo";
                                    if (results.Roomtype == "4")
                                        results.Roomtype = "Office Space";
                                    if (results.Roomtype == "5")
                                        results.Roomtype = "Retail Outlet";
                                    if (results.Roomtype == "6")
                                        results.Roomtype = "Town house";
                                    if (results.Roomtype == "7")
                                        results.Roomtype = "Others";
                                    if (results.Roomtype == "8")
                                        results.Roomtype = "Homes";
                                    if (results.Roomtype == "9")
                                        results.Roomtype = "Houses";
                                    if (results.Roomtype == "10")
                                        results.Roomtype = "Hostels";
                                    if (results.Roomtype == "11")
                                        results.Roomtype = "Hotels";
                                    if (results.Roomtype == "12")
                                        results.Roomtype = "Basement Apartment";
                                    if (results.Roomtype == "13")
                                        results.Roomtype = "Parking Garage";
                                    if (results.noofrooms == "1")
                                        results.noofrooms = "1Bhk";
                                    if (results.noofrooms == "2")
                                        results.noofrooms = "2Bhk";
                                    if (results.noofrooms == "3")
                                        results.noofrooms = "3Bhk";
                                    if (results.noofrooms == "4")
                                        results.noofrooms = "4Bhk+";

                                    
                                    if (string.IsNullOrEmpty(results.areamin) & results.areamin == "0")
                                    {
                                        results.sqfeet = "";
                                        sqftvisible = false;
                                    }

                                    string rentprice = "";
                                    if(results.Roomtype == "Parking Garage" || results.Roomtype== "Retail Outlet" || results.Roomtype == "Office Space" || results.Roomtype == "Others")
                                    {
                                        results.parkingvisible = false;
                                    }
                                    else
                                    {
                                        results.parkingvisible = true;
                                    }

                                    if (!string.IsNullOrEmpty(results.noofrooms) && results.noofrooms != "0")
                                        results.Title = results.noofrooms + " " + results.Roomtype;
                                    else
                                        results.Title = results.Roomtype;

                                    results.City = results.City + " " + "," + results.Statecode;
                                    if (results.Negotiable == "1")
                                        results.Negotiable = "(Negotiable)";
                                    else
                                        results.Negotiable = "";


                                    if (results.hiderent == "0")
                                    {
                                        if (results.Adtype == "1")
                                        {
                                            rentprice = results.price1.ToString();
                                            if (string.IsNullOrEmpty(results.price1.ToString()) && results.price1.ToString() == "0")
                                            {
                                                rentprice = "";
                                            }
                                            else
                                            {
                                                rentprice = "$" + Convert.ToDouble(results.price1) + " " + results.Negotiable;
                                                results.price1 = rentprice;
                                                if (results.Pricemode == "Per Month")
                                                    results.Pricemode = "Month";
                                                results.Pricemode = "/ " + results.Pricemode;
                                            }
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(results.price2.ToString()) && results.price2.ToString() == "0")
                                            {
                                                rentprice = "";
                                            }
                                            else
                                            {
                                                rentprice = "$ " + Convert.ToDouble(results.price2) + " " + results.Negotiable;
                                                results.price1 = rentprice;
                                                if (results.Pricemode == "Per Month")
                                                    results.Pricemode = "Month";
                                                results.Pricemode = "/ " + results.Pricemode;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        results.price1 = "Contact for price";
                                        results.Pricemode = "";
                                    }
                                    //if (res.orderby == "distance" && results.distance != 0)
                                    if (results.distance != 0 && Math.Round(results.distance)!=0)
                                    {
                                        results.distVisible = true;
                                        results.distance = Math.Round(results.distance, 1);
                                        results.distancedata = "Distance :" + " " + results.distance + " " + "Miles";
                                    }
                                    else
                                    {
                                        results.distVisible = false;
                                    }
                                    if (results.hideaddress == "0")
                                    {
                                        results.addressvisible = true;
                                    }
                                    if (results.Adtype == "0")
                                        results.Adtype = "Property Wanted";
                                    else
                                        results.Adtype = "Property Offered";

                                    if (results.Adtype == "0")
                                    {
                                        results.availablefromtext = "Move in from : ";
                                    }
                                    else
                                    {
                                        results.availablefromtext = "Available from : ";
                                    }

                                }
                            }
                            RTitems = responsedata.ROW_DATA;
                            IsBusy = false;
                            Rentalslistingldr.IsRunning = false;
                            Rentalslistingldr.IsVisible = false;
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    return RTitems;
                }

            };



            Filter_RT.checkedcategoryvalue = res.primarycategoryvalue;
            Filter_RT.checkedamenitycategoryvalue = res.amenities;
            Filter_RT.noofbed = res.secondarycategoryvalue;
            Filter_RT.tenant = res.categoryname;
            Filter_RT.availability = res.Movedate;
            //res.Movedate = Filter_RT.availability;
            Categoryurl =res.primarycategoryvalue;
            Cityurl = cityurl;
            GetRentalListData(res,cityurl);
            DetailCommand = new Command<ListRentalCategoryData>(getdetaildata);
            Listcontactcmd = new Command<ListRentalCategoryData>(TapOnListContact);
            filtercommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(() => { dialogs.ShowLoading("", null); });
                Taponfiltercommand(res, cityurl);
           });
            //filtercommand = new Command(async () => await Taponfiltercommand(res, cityurl));
            sortbycommand = new Command(async () => await TapOnsortbycommand(res, cityurl));
            TapSearchPage = new Command(async () => await ClickSearchPage(cityurl));
            Retrycmd = new Command(() => { Taponretry(res, cityurl); });
        }
        public void Taponretry(RTResponse categoryurl, string cityurl)
        {
            GetRentalListData(categoryurl, cityurl);
        }
        public async Task ClickSearchPage(string cityurl)
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new RTSearch(cityurl));
        }
        public async void Taponfiltercommand(RTResponse res, string cityurl)
        {
           // dialogs.ShowLoading();
           
            if (Filter_RT.pagetype == "filter")
            {
                if (!string.IsNullOrEmpty(Filter_RT.filtercityurl))
                {
                    res.City = Filter_RT.filtercity;
                    cityurl = Filter_RT.filtercityurl;
                    res.userlong = Filter_RT.filterlongitude;
                    res.userlat = Filter_RT.filterlatitude;
                }
                if (Filter_RT.checkedcategoryvalue != "" && Filter_RT.checkedcategoryvalue != null)
                    res.primarycategoryvalue = Filter_RT.checkedcategoryvalue;
                else
                    res.primarycategoryvalue = Filter_RT.checkedcategoryvalue;
                if (!string.IsNullOrEmpty(Filter_RT.leaseperiod))
                    res.stayperiod = Filter_RT.leaseperiod;
                if (!string.IsNullOrEmpty(Filter_RT.petpreference))
                    res.petpolicy = Filter_RT.petpreference;
                if (!string.IsNullOrEmpty(Filter_RT.furnish))
                    res.furnishing = Filter_RT.furnish;
                if (!string.IsNullOrEmpty(Filter_RT.vegpreference))
                    res.isveg = Filter_RT.vegpreference;
                //if (!string.IsNullOrEmpty(Filter_RT.checkedamenitycategoryvalue))
                //    res.amenities = Filter_RT.checkedamenitycategoryvalue;
                //else
                    res.amenities = Filter_RT.checkedamenitycategoryvalue;
                if (!string.IsNullOrEmpty(Filter_RT.availability))
                    res.availablefrm = Filter_RT.availability;
                if (!string.IsNullOrEmpty(Filter_RT.availability))
                    res.Movedate = Filter_RT.availability;
                if (!string.IsNullOrEmpty(Filter_RT.priceto))
                    res.ExpRent = Filter_RT.priceto;
                if (!string.IsNullOrEmpty(Filter_RT.pricefrm))
                    res.pricefrom = Filter_RT.pricefrm;
                if (!string.IsNullOrEmpty(Filter_RT.tenant))
                    res.categoryname = Filter_RT.tenant;
                if (!string.IsNullOrEmpty(Filter_RT.leaseperiod))
                    res.stayperiod = Filter_RT.leaseperiod;
                if (!string.IsNullOrEmpty(Filter_RT.noofbed))
                    res.secondarycategoryvalue = Filter_RT.noofbed;
                if (!string.IsNullOrEmpty(Filter_RT.noofaccomodate))
                    res.accommodates = Filter_RT.noofaccomodate;
                if (!string.IsNullOrEmpty(Filter_RT.noofbath))
                    res.noofbaths = Filter_RT.noofbath;
                if (!string.IsNullOrEmpty(Filter_RT.distancefrm))
                    res.distancefrm = Filter_RT.distancefrm;
                else
                    res.distancefrm = "0";
                if (!string.IsNullOrEmpty(Filter_RT.distanceto))
                    res.distanceto = Filter_RT.distanceto;
                else
                    res.distanceto = "75";
                //RangeSlider pricerange = currentpage.FindByName<RangeSlider>("slidervalue");
                //float rent = (float)Convert.ToDouble(res.ExpRent);
                //if (!string.IsNullOrEmpty(res.ExpRent))
                //    pricerange.UpperValue = rent;
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new Filter_RT(res, cityurl));
                dialogs.HideLoading();
            }
            else
            {
                Device.BeginInvokeOnMainThread(() => { dialogs.ShowLoading("", null); });
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new Filter_RT(res, cityurl));
                Filter_RT.pagetype = "filter";
                dialogs.HideLoading();
            }
        }
        public async Task TapOnsortbycommand(RTResponse res, string cityurl)
        {
            //Response response, string cityurl

            //if (Filter_RT.checkedcategoryvalue != "" && Filter_RT.checkedcategoryvalue != null)
            //    res.primarycategoryvalue = Filter_RT.checkedcategoryvalue;
            //else
            if (!string.IsNullOrEmpty(Filter_RT.filtercityurl))
            {
                res.City = Filter_RT.filtercity;
                cityurl = Filter_RT.filtercityurl;
                res.userlong = Filter_RT.filterlongitude;
                res.userlat = Filter_RT.filterlatitude;
            }
            res.primarycategoryvalue = Filter_RT.checkedcategoryvalue;
            if (!string.IsNullOrEmpty(Filter_RT.leaseperiod))
                res.stayperiod = Filter_RT.leaseperiod;
            if (!string.IsNullOrEmpty(Filter_RT.petpreference))
                res.petpolicy = Filter_RT.petpreference;
            if (!string.IsNullOrEmpty(Filter_RT.furnish))
                res.furnishing = Filter_RT.furnish;
            else
                res.furnishing ="999";
            if (!string.IsNullOrEmpty(Filter_RT.vegpreference))
                res.isveg = Filter_RT.vegpreference;

            if (!string.IsNullOrEmpty(Filter_RT.priceto))
                res.ExpRent = Filter_RT.priceto;
            else
                res.ExpRent =res.ExpRent;
            
            if (!string.IsNullOrEmpty(Filter_RT.pricefrm))
                res.pricefrom = Filter_RT.pricefrm;
            //else
            //    res.pricefrom = "0";

            if (Filter_RT.checkedamenitycategoryvalue != "" && Filter_RT.checkedamenitycategoryvalue != null)
                res.amenities = Filter_RT.checkedamenitycategoryvalue;
            else
                res.amenities = Filter_RT.checkedamenitycategoryvalue;

            if (!string.IsNullOrEmpty(Filter_RT.availability))
                res.availablefrm = Filter_RT.availability;
            if (!string.IsNullOrEmpty(Filter_RT.availability))
                res.Movedate = Filter_RT.availability;
            if (!string.IsNullOrEmpty(Filter_RT.distancefrm))
                res.distancefrm = Filter_RT.distancefrm;
            else
                res.distancefrm = "0";
            if (!string.IsNullOrEmpty(Filter_RT.distanceto))
                res.distanceto = Filter_RT.distancefrm;
            else
                res.distanceto = "75";
            if (!string.IsNullOrEmpty(Sortby_RT.orderby))
                res.orderby = Sortby_RT.orderby;
            if (!string.IsNullOrEmpty(Filter_RT.noofbed))
                res.secondarycategoryvalue = Filter_RT.noofbed;
            if (!string.IsNullOrEmpty(Filter_RT.noofaccomodate))
                res.accommodates = Filter_RT.noofaccomodate;
            if (!string.IsNullOrEmpty(Filter_RT.noofbath))
                res.noofbaths = Filter_RT.noofbath;
            else
                res.noofbaths = Filter_RT.noofbath;
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new Views.Sortby_RT(res, cityurl));

        }
        public async void TapOnListContact(ListRentalCategoryData listadid)
        {
            string adid = listadid.Adid.ToString();
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new Contact_RT(adid));
        }
        public static string roomtype = "";
        public async void getdetaildata(ListRentalCategoryData data)
        {
            roomtype = data.Primarycategoryvalue;
            string Adid = data.Adid.ToString();
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.DetailRentalList(Adid,Scityurl));
        }
        public async void GetRentalListData(RTResponse categoryurl,string cityurl)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if(connected==true)
            {
                try
                {
                    dialogs.ShowLoading("");
                    nointernet = false;
                    categoryurl.pageno = 1;
                    if(string.IsNullOrEmpty(categoryurl.nearby))
                    {
                        categoryurl.nearby = "0";
                    }
                    categoryurl.userlat = Commonsettings.UserLat.ToString();
                    categoryurl.userlong = Commonsettings.UserLong.ToString();
                    if (string.IsNullOrEmpty(categoryurl.petpolicy))
                    {
                        categoryurl.petpolicy = "999";
                    }
                    if (string.IsNullOrEmpty(categoryurl.furnishing))
                    {
                        categoryurl.furnishing = "999";
                    }
                    var Listofrentaldata = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                    ListRentalCategory responsedata = await Listofrentaldata.GetRentalList(categoryurl, cityurl);
                    
                    OnPropertyChanged(nameof(RentalsList));
                    if (responsedata.ROW_DATA.Count > 0)
                    {
                        Nolisting = false;
                        lcflistview = true;
                        if (string.IsNullOrEmpty(NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName))
                        {
                            NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        }
                        //Availablelistcount = responsedata.ROW_DATA.First().totalrecs + " Listings in and near " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                        foreach (var results in responsedata.ROW_DATA)
                        {
                            results.orderby = categoryurl.orderby;
                            if (results.Thumbimgurl == null || results.Thumbimgurl == "" || results.Thumbimgurl == "0")
                            {
                                //results.Thumbimgurl = "RoommateNoImage.png";
                                results.thumburlvisible = false;
                            }
                            else
                            {
                                results.thumburlvisible = true;
                            }
                            if (results.Roomtype == "1")
                                results.Roomtype = "Single Family Home";
                            if (results.Roomtype == "2")
                                results.Roomtype = "Apartment";
                            if (results.Roomtype == "3")
                                results.Roomtype = "Condo";
                            if (results.Roomtype == "4")
                                results.Roomtype = "Office Space";
                            if (results.Roomtype == "5")
                                results.Roomtype = "Retail Outlet";
                            if (results.Roomtype == "6")
                                results.Roomtype = "Town house";
                            if (results.Roomtype == "7")
                                results.Roomtype = "Others";
                            if (results.Roomtype == "8")
                                results.Roomtype = "Homes";
                            if (results.Roomtype == "9")
                                results.Roomtype = "Houses";
                            if (results.Roomtype == "10")
                                results.Roomtype = "Hostels";
                            if (results.Roomtype == "11")
                                results.Roomtype = "Hotels";
                            if (results.Roomtype == "12")
                                results.Roomtype = "Basement Apartment";
                            if (results.Roomtype == "13")
                                results.Roomtype = "Parking Garage";
                            if (results.noofrooms == "1")
                                results.noofrooms = "1Bhk";
                            if (results.noofrooms == "2")
                                results.noofrooms = "2Bhk";
                            if (results.noofrooms == "3")
                                results.noofrooms = "3Bhk";
                            if (results.noofrooms == "4")
                                results.noofrooms = "4Bhk+";

                            if (string.IsNullOrEmpty(results.areamin) & results.areamin == "0")
                            {
                                results.sqfeet = "";
                                sqftvisible = false;
                            }
                            string rentprice = "";
                            if (results.Roomtype == "Parking Garage" || results.Roomtype == "Retail Outlet" || results.Roomtype == "Office Space" || results.Roomtype == "Others")
                            {
                                results.parkingvisible = false;
                            }
                            else
                            {
                                results.parkingvisible = true;
                            }

                            if (!string.IsNullOrEmpty(results.noofrooms) && results.noofrooms != "0")
                                results.Title = results.noofrooms + " " + results.Roomtype;
                            else
                                results.Title = results.Roomtype;

                            if(results.hideaddress =="0")
                            {
                                results.addressvisible = true;
                            }
                            results.City = results.City + " " + "," + results.Statecode;
                            if (results.Negotiable == "1")
                                results.Negotiable = "(Negotiable)";
                            else
                                results.Negotiable = "";


                            if (results.hiderent == "0")
                            {
                                if (results.Adtype == "1")
                                {
                                    rentprice = results.price1.ToString();
                                    if (string.IsNullOrEmpty(results.price1.ToString()) && results.price1.ToString() == "0")
                                    {
                                        rentprice = "";
                                    }
                                    else
                                    {
                                        rentprice = "$" + Convert.ToDouble(results.price1) + " " + results.Negotiable;
                                        results.price1 = rentprice;
                                        if (results.Pricemode == "Per Month")
                                            results.Pricemode = "Month";
                                        results.Pricemode = "/ " + results.Pricemode;
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(results.price2.ToString()) && results.price2.ToString() == "0")
                                    {
                                        rentprice = "";
                                    }
                                    else
                                    {
                                        rentprice = "$ " + Convert.ToDouble(results.price2) + " " + results.Negotiable;
                                        results.price1 = rentprice;
                                        if (results.Pricemode == "Per Month")
                                            results.Pricemode = "Month";
                                        results.Pricemode = "/ " + results.Pricemode;
                                    }
                                }
                            }
                            else
                            {
                                results.price1 = "Contact for price";
                                results.Pricemode = "";
                            }
                            if (string.IsNullOrEmpty(results.orderby))
                            {
                                results.orderby = "date";
                            }
                            //if (results.orderby == "distance" && results.distance != 0)
                        
                            if (results.distance != 0 && Math.Round(results.distance, 1) !=0)
                            {
                                results.distVisible = true;
                                results.distance = Math.Round(results.distance, 1);
                                results.distancedata = "Distance :" + " " + results.distance + " " + "Miles";
                            }
                            else
                            {
                                results.distVisible = false;
                            }

                            if (results.Adtype == "0")
                                results.Adtype = "Property Wanted";
                            else
                                results.Adtype = "Property Offered";

                            if (results.Adtype == "0")
                            {
                                results.availablefromtext = "Move in from : ";
                            }
                            else
                            {
                                results.availablefromtext = "Available from : ";
                            }
                        }
                        //var currentpage = GetCurrentPage();
                        //ListView RentalListing = currentpage.FindByName<ListView>("Rental");
                        //StackLayout stack = currentpage.FindByName<StackLayout>("stacknoblk");
                        //RentalListing.ItemsSource = responsedata.ROW_DATA;
                        //RentalListing.IsVisible = true;
                        //stack.IsVisible = false;

                        var currentpage = GetCurrentPage();
                        var Availablelistcounttxt = currentpage.FindByName<Label>("Availablelistcount");
                        Availablelistcounttxt.IsVisible = true;
                        //Availablelistcounttxt.Text = responsedata.ROW_DATA.First().totalrecs + " Listings in and near " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                        //if (categoryurl.categoryname == "Homes" || categoryurl.categoryname == "Houses" || categoryurl.categoryname == "Hostels")
                        //{
                        //    if (responsedata.ROW_DATA.First().totalrecs == 1)
                        //    {
                        //        Availablelistcounttxt.Text = responsedata.ROW_DATA.First().totalrecs + " " + categoryurl.categoryname.Remove(categoryurl.categoryname.Length - 1, 1) + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                        //    }
                        //    else
                        //    {
                        //        Availablelistcounttxt.Text = responsedata.ROW_DATA.First().totalrecs + " " + categoryurl.categoryname + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                        //    }
                        //}
                        //else if (categoryurl.categoryname == "Single Family Home" || categoryurl.categoryname == "Apartment" || categoryurl.categoryname == "Condo" || categoryurl.categoryname == "Office Space" || categoryurl.categoryname == "Town house" || categoryurl.categoryname == "Retail Outlet" || categoryurl.categoryname == "Basement Apartment" || categoryurl.categoryname == "Parking Garage")
                        //{
                        //    if (responsedata.ROW_DATA.First().totalrecs == 1)
                        //    {
                        //        Availablelistcounttxt.Text = responsedata.ROW_DATA.First().totalrecs + " " + categoryurl.categoryname + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                        //    }
                        //    else
                        //    {
                        //        Availablelistcounttxt.Text = responsedata.ROW_DATA.First().totalrecs + " " + categoryurl.categoryname + "s" + " for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                        //    }
                        //}
                        //if(categoryurl.primarycategoryvalue.Contains(",") || categoryurl.categoryname=="Others")
                        //{
                            if (responsedata.ROW_DATA.First().totalrecs == 1)
                            {
                                Availablelistcounttxt.Text = responsedata.ROW_DATA.First().totalrecs + " Property for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                            }
                            else
                            {
                                Availablelistcounttxt.Text = responsedata.ROW_DATA.First().totalrecs + " Properties for rent in " + NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                            }
                       // }
                        ListView RentalListing = currentpage.FindByName<ListView>("Rental");
                        StackLayout stack = currentpage.FindByName<StackLayout>("stacknoblk");
                        
                        rentalslst?.AddRange(responsedata.ROW_DATA);
                        RentalListing.ItemsSource = rentalslst;
                        OnPropertyChanged(nameof(rentalslst));
                        RentalListing.IsVisible = true;
                        stack.IsVisible = false;
                    }
                    //else
                    //{
                    //    var currentpage = GetCurrentPage();
                    //    var Availablelistcounttxt = currentpage.FindByName<Label>("Availablelistcount");

                    //    categoryurl.nearby = "2";
                    //    if (!string.IsNullOrEmpty(NRIApp.Roommates.Features.Post.ViewModels.VMLCF.RM_metrourl))
                    //    {
                    //        cityurl = NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.RT_metrourl;
                    //    }
                    //    else
                    //    {
                    //        cityurl = Commonsettings.Usermetrourl;
                    //    }

                    //    var metroListofrentaldata = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                    //    ListRentalCategory metroresponsedata = await Listofrentaldata.GetRentalList(categoryurl, cityurl);
                    //    if (metroresponsedata.ROW_DATA.Count > 0)
                    //    {
                    //        Availablelistcounttxt.Text = "Sorry,there are no listings matching your criteria...You can find related listings below";
                    //        gotolisting(categoryurl, cityurl);
                    //    }
                        else
                        {
                        var currentpage = GetCurrentPage();
                        var Availablelistcounttxt = currentpage.FindByName<Label>("Availablelistcount");
                        // Availablelistcounttxt.Text = "Sorry,there are no rooms for rent in "+ NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                        Availablelistcounttxt.IsVisible = false;
                             ListView RentalListing = currentpage.FindByName<ListView>("Rental");
                            StackLayout stack = currentpage.FindByName<StackLayout>("stacknoblk");
                            RentalListing.IsVisible = false;
                            stack.IsVisible = true;
                            //RoommatesListing.ItemsSource = response.ROW_DATA;
                            lcflistview = false;
                            Nolisting = true;
                        }
                   // }

                    //RentalsList = responsedata.ROW_DATA;
                    dialogs.HideLoading();
                }
                catch (Exception e)
                {
                    var connect = CrossConnectivity.Current.IsConnected;
                    dialogs.HideLoading();
                    if (connect == false)
                    {
                        nointernet = true;
                    }
                }
            }
            else
            {
                    dialogs.HideLoading();
                    nointernet = true;
                //dialogs.Toast("Kindly check your internet connection");
            }
           
        }
        public async void gotolisting(RTResponse categoryurl, string cityurl)
        {
            GetRentalListData(categoryurl, cityurl);
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
