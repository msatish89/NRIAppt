using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.USHome.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using Refit;
using NRIApp.USHome.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using NRIApp.USHome.Models;

namespace NRIApp.USHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeScreenPage : ContentPage
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public List<Userlocdetails> locdetails { get; set; }
        public HomeScreenPage()
        {
             //getloc();
            InitializeComponent();
           
            if (Device.RuntimePlatform == Device.iOS)
                    NavigationPage.SetBackButtonTitle(this, "Back");

            this.BindingContext = new USHomeViewModel();
        }
        public void Refresh()
        {
            Navigation.PushAsync(new NRIApp.USHome.Views.Homemaster());
        }
        private void ServicesTap(object sender, EventArgs e)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
                Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Servicetype());
            else
                dialog.Toast("Kindly check your internet connection");
        }
        private void EventsTap(object sender, EventArgs e)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
                Application.Current.MainPage.Navigation.PushAsync(new NRIApp.Events.Features.Listing.Views.Listing());
            else
                dialog.Toast("Kindly check your internet connection");
        }

        

        private void TechjobsTap(object sender, EventArgs e)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
                Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Courses());
            else
                dialog.Toast("Kindly check your internet connection");
        }
      

        private void jobsTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Firststep());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                Title = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    Task.Run(async () =>
        //    {
        //        await Navigation.PopToRootAsync();
        //    });
        //    return true;
        //}

        public async void getloc()
        {
            try
            {
                dialog.ShowLoading("", null);
                var hasPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                if (hasPermission != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    hasPermission = results[Permission.Location];
                }
                if (hasPermission == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    if (locator.IsGeolocationEnabled)
                    {
                        Position position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5));
                        if (position != null)
                        {
                            Commonsettings.UserLat = position.Latitude;
                            Commonsettings.UserLong = position.Longitude;

                          //  Getlocation();
                        }
                    }
                    else
                    {
                        Commonsettings.Usercity = "New York";
                        Commonsettings.Userzipcode = "10026";
                        Commonsettings.Usercountry = "US";
                        Commonsettings.UserLat = Convert.ToDouble("40.80405");
                        Commonsettings.UserLong = Convert.ToDouble("-73.952833");
                        Commonsettings.Userstatecode = "NY";
                        Commonsettings.Usercityurl = "new-york-ny";
                        Commonsettings.Usermetrourl = "new-york-metro-area";
                    }
                }
                else
                {
                    Commonsettings.Usercity = "New York";
                    Commonsettings.Userzipcode = "10026";
                    Commonsettings.Usercountry = "US";
                    Commonsettings.UserLat = Convert.ToDouble("40.80405");
                    Commonsettings.UserLong = Convert.ToDouble("-73.952833");
                    Commonsettings.Userstatecode = "NY";
                    Commonsettings.Usercityurl = "new-york-ny";
                    Commonsettings.Usermetrourl = "new-york-metro-area";
                }


            }
            catch (Exception e)
            {
                string Error = e.Message + e.StackTrace;
                dialog.HideLoading();
            }
            dialog.HideLoading();
        }

        public async void Getlocation()
        {

            var ApiC = RestService.For<IUSHome>(Commonsettings.TechjobsAPI);
            Userloc loc = await ApiC.Getuserloc(Commonsettings.UserLat, Commonsettings.UserLong);
            OnPropertyChanged(nameof(locdetails));
            locdetails = loc.ROW_DATA;
            if (locdetails != null)
            {
                Commonsettings.Usercity = locdetails.First().city;
                Commonsettings.Userzipcode = locdetails.First().zipcode;
                Commonsettings.Usercountry = locdetails.First().countrycode;
                Commonsettings.UserLat = Convert.ToDouble(locdetails.First().latitude);
                Commonsettings.UserLong = Convert.ToDouble(locdetails.First().longitude);
                Commonsettings.Userstatecode = locdetails.First().statecode;
                Commonsettings.Usercityurl = locdetails.First().cityurl;
                Commonsettings.Usermetrourl = locdetails.First().metrourl;
                Title = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                dialog.HideLoading();
            }

        }
    }
}