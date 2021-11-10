using Acr.UserDialogs;
using Newtonsoft.Json;
using NRIApp.DayCare.Features.Post.Models;
using NRIApp.DayCare.Features.Post.Views;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class Location_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public string TempLocationName = "";
        private string _LocationName = "";
        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value;
                OnPropertyChanged(nameof(LocationName));
                if (string.IsNullOrEmpty(LocationName))
                {
                    TempLocationName = "";
                }
                if (!(string.IsNullOrEmpty(LocationName)) && (TempLocationName != LocationName))
                {
                    GetLocation();
                }
            }
        }
        public string TempaddresslocationName = "";
        private string _addresslocationname = "";
        public string addresslocationname
        {
            get { return _addresslocationname; }
            set
            {
                _addresslocationname = value;
                OnPropertyChanged(nameof(addresslocationname));
                if (string.IsNullOrEmpty(addresslocationname))
                {
                    TempaddresslocationName = "";
                }
                if (!(string.IsNullOrEmpty(addresslocationname)) && addresslocationname.Trim().Length != 0 && (TempaddresslocationName != addresslocationname))
                {
                    //  GetaddressLocation();
                    GetgoogleAddress();
                }
            }
        }
        private string _cityID = "";
        public string cityID
        {
            get { return _cityID; }
            set { _cityID = value; OnPropertyChanged(nameof(cityID)); }
        }
        private string _addresscityID = "";
        public string addresscityID
        {
            get { return _addresscityID; }
            set { _addresscityID = value; OnPropertyChanged(nameof(addresscityID)); }
        }
        private double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; OnPropertyChanged(nameof(Latitude)); }
        }
        private double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; OnPropertyChanged(nameof(Longitude)); }
        }
        private string _Country = "";
        public string Country
        {
            get { return _Country; }
            set { _Country = value; OnPropertyChanged(nameof(Country)); }
        }
        private string _Statecode = "";
        public string Statecode
        {
            get { return _Statecode; }
            set { _Statecode = value; OnPropertyChanged(nameof(Statecode)); }
        }
        private string _newcityurl = "";
        public string newcityurl
        {
            get { return _newcityurl; }
            set { _newcityurl = value; OnPropertyChanged(nameof(newcityurl)); }
        }
        private string _cityzipcode = "";
        public string cityzipcode
        {
            get { return _cityzipcode; }
            set { _cityzipcode = value; OnPropertyChanged(nameof(cityzipcode)); }
        }
        private string _addresszipcode = "";
        public string addresszipcode
        {
            get { return _addresszipcode; }
            set { _addresszipcode = value; OnPropertyChanged(nameof(addresszipcode)); }
        }
        private bool _citylistbvisble = false;
        public bool citylistbvisble
        {
            get { return _citylistbvisble; }
            set { _citylistbvisble = value; OnPropertyChanged(nameof(citylistbvisble)); }
        }
        private bool _addresslistvisible = false;
        public bool addresslistvisible
        {
            get { return _addresslistvisible; }
            set { _addresslistvisible = value; OnPropertyChanged(nameof(addresslistvisible)); }
        }
        private string _showmapImg = "CheckBoxUnChecked.png";
        public string showmapImg
        {
            get { return _showmapImg; }
            set { _showmapImg = value; OnPropertyChanged(nameof(showmapImg)); }
        }
        private int _showmapID = 0;
        public int showmapID
        {
            get { return _showmapID; }
            set { _showmapID = value; OnPropertyChanged(nameof(showmapID)); }
        }
        private string _zipcode = "";
        public string zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; OnPropertyChanged(nameof(zipcode)); }
        }
        private string _Zipcode = "";
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; OnPropertyChanged(nameof(Zipcode)); }
        }
        private List<NRIApp.Roommates.Features.Post.Models.Location> locationlist;
        public List<NRIApp.Roommates.Features.Post.Models.Location> _locationlist
        {
            get { return _locationlist; }
            set { _locationlist = value; OnPropertyChanged(nameof(locationlist)); }
        }
        public Command showmapcmd { get; set; }
        public Command<Roommates.Features.Post.Models.Location> SelectLocation { get; set; }
        public Command<NRIApp.Roommates.Features.Post.Models.Predictions> SelectAddressLocation { get; set; }
        public Command Locationcmd { get; set; }

        public void selectlocation(Roommates.Features.Post.Models.Location location)
        {
            var city = location.citystatecode;
            LocationName = TempLocationName = city;
            cityID = location.city;
            Latitude = location.latitude;
            Longitude = location.longitude;
            Statecode = location.statecode;
            newcityurl = location.newcityurl;
            if(string.IsNullOrEmpty(location.newcityurl))
            {
                newcityurl = location.cityurl;
            }
            cityzipcode = zipcode = location.zipcode.ToString();
            citylistbvisble = false;
        }

        public void checkshowmapdir()
        {
            if (showmapID == 0)
            {
                showmapImg = "CheckBoxChecked.png";
                showmapID = 1;
            }
            else
            {
                showmapImg = "CheckBoxUnChecked.png";
                showmapID = 0;
            }
        }

        public async void GetLocation()
        {
            try
            {
                cityzipcode = "";
                //Tlocationtxt = "";
                var LocationAPI = RestService.For<Roommates.Features.Post.Interface.IRMCategory>(Commonsettings.Localservices);
                Roommates.Features.Post.Models.LocationRowData response = await LocationAPI.getlocation(LocationName);

                if (response != null && response.ROW_DATA.Count > 0)
                {
                    citylistbvisble = true;
                    locationlist = response.ROW_DATA;
                    var currentpage = Getcurrentpage();
                    var locationdata = currentpage.FindByName<ListviewScrollbar>("locationlistdata");

                    foreach (var item in response.ROW_DATA)
                    {
                        item.citystatecode = item.city + ", " + item.statecode;
                    }
                    locationdata.ItemsSource = response.ROW_DATA;
                }

            }
            catch (Exception e)
            {
            }
        }
        public async void GetaddressLocation()
        {
            try
            {
                addresszipcode = "";
                //Tlocationtxt = "";
                var LocationAPI = RestService.For<Roommates.Features.Post.Interface.IRMCategory>(Commonsettings.Localservices);
                Roommates.Features.Post.Models.LocationRowData response = await LocationAPI.getlocation(addresslocationname);

                if (response != null && response.ROW_DATA.Count > 0)
                {
                    addresslistvisible = true;
                    // locationlist = response.ROW_DATA;
                    var currentpage = Getcurrentpage();
                    var locationdata = currentpage.FindByName<ListviewScrollbar>("addresslistdata");

                    foreach (var item in response.ROW_DATA)
                    {
                        item.citystatecode = item.city + ", " + item.statecode;
                    }
                    locationdata.ItemsSource = response.ROW_DATA;
                }

            }
            catch (Exception e)
            {
            }
        }
        private const string GooglePlacesUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        private List<Predictions> _googleadd;
        public List<Predictions> googleadd
        {
            get { return _googleadd; }
            set { _googleadd = value; }
        }
        private string _placeid = "";
        public string placeid
        {
            get { return _placeid; }
            set { _placeid = value; OnPropertyChanged(nameof(placeid)); }
        }
        public async void GetgoogleAddress()
        {
            try
            {
                placeid = "";
                string content = GooglePlacesUrl + "?input=" + addresslocationname + "&components=country:us&key=" + Commonsettings.GoogleAPIkey;
                var client = new HttpClient();
                var response = await client.PostAsync(content, new StringContent("")).ConfigureAwait(false);
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Googlecity>(data);
                var currentpage = Getcurrentpage();
                //var locationdata = currentpage.FindByName<ListviewScrollbar>("addresslistdata");
                //locationdata.ItemsSource = result.Predictions;

                OnPropertyChanged(nameof(googleadd));
                _googleadd = result.Predictions;
                OnPropertyChanged(nameof(addresslistvisible));
                addresslistvisible = true;
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        public void OnSelectgoogleaddress(Predictions pt)
        {
            try
            {
                placeid = pt.Place_id;
                addresslocationname = TempaddresslocationName = pt.Description;
                addresslistvisible = false;
                OnPropertyChanged(nameof(addresslistvisible));
                addresslistvisible = false;
            }
            catch(Exception ex)
            {

            }
        }
        Daycareposting post = new Daycareposting();
        public Location_VM(Daycareposting postdata)
        {
           try
            {
                post = postdata;
                if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                {
                    LocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    TempLocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    newcityurl = Commonsettings.Usercityurl;
                    Zipcode = cityzipcode = zipcode = Commonsettings.Userzipcode;
                    cityID = Commonsettings.Usercity;
                    Latitude = Commonsettings.UserLat;
                    Longitude = Commonsettings.UserLong;
                    Statecode = Commonsettings.Userstatecode;
                    post.locationAddress = addresslocationname;
                    post.hidemap = showmapID.ToString();
                    newcityurl = Commonsettings.Usercityurl;
                }
                if(postdata.ismyneed=="1")
                {
                    prefilllocation();
                }
                showmapcmd = new Command(checkshowmapdir);
                SelectLocation = new Command<Roommates.Features.Post.Models.Location>(selectlocation);
                SelectAddressLocation = new Command<NRIApp.Roommates.Features.Post.Models.Predictions>(OnSelectgoogleaddress);
                Locationcmd = new Command(gotopostthird);
            }
            catch(Exception ex)
            {

            }
        }
        public void prefilllocation()
        {
            try
            {
                LocationName = post.city + ", " + post.Statecode;// Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                TempLocationName = post.city + ", " + post.Statecode;
                newcityurl = post.Cityurl;
                Zipcode = cityzipcode = zipcode = post.zipcode;
                cityID = post.city;
                Latitude = post.lat;
                Longitude = post.longtitude;
                Statecode = post.Statecode;
                addresslocationname = TempaddresslocationName = post.Businessaddress;
                if(string.IsNullOrEmpty(post.Businessaddress))
                {
                    addresslocationname = post.locationAddress;
                }
                newcityurl = post.newcityurl;
                if(!string.IsNullOrEmpty(post.hidemap))
                {
                    showmapID = Convert.ToInt32(post.hidemap);
                    if(showmapID== 1)
                    {
                        showmapID = 0;
                        checkshowmapdir();
                    }
                }
                
            }
            catch(Exception ex)
            {
            }
        }
        bool validation()
        {
            bool result = true;
            if (string.IsNullOrEmpty(cityzipcode))
            {
                dialog.Toast("Please select your City");
                result = false;
            }
            if (addresslocationname.Trim().Length == 0)
            {
                dialog.Toast("Please select your Address");
                result = false;
                return false;
            }
            if (string.IsNullOrEmpty(zipcode) || zipcode.Trim().Length == 0)
            {
                dialog.Toast("Please enter your Zipcode");
                result = false;
                return false;
            }
            return result;
        }
        public async void gotopostthird()
        {
            if(validation())
            {
                post.city = cityID;
                post.lat = Latitude;
                post.longtitude = Longitude;
                post.lat = Latitude;
                post.state = Statecode;
                post.locationAddress = addresslocationname;
                post.zipcode = zipcode;
                post.hidemap = showmapID.ToString();
                post.newcityurl = newcityurl;

                var currentpage = Getcurrentpage();
                await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostThird(post));
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page Getcurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
