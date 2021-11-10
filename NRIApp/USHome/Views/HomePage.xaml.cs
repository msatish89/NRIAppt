using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.USHome.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Models;
using Acr.UserDialogs;
using Plugin.Connectivity;

using Refit;
using NRIApp.USHome.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using NRIApp.USHome.Models;
using Plugin.Messaging;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace NRIApp.USHome.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : MasterDetailPage,INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public List<Userlocdetails> locdetails { get; set; }
        public string LoginVia { get; set; }

        public HomePage()
        {
            InitializeComponent();
            //LoginVia = loginvia;
            // getloc();
            if (Commonsettings.UserMobileOS == "android")
                GetAndroidStoreAppVersion();
            else if (Commonsettings.UserMobileOS == "iphone")
                GetIosStoreAppVersion();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");

            this.BindingContext = new USHomeViewModel();
            string name = Commonsettings.UserName;
            string mail = Commonsettings.UserEmail;
            //var vm = BindingContext as USHomeViewModel;
            //vm?.getloc();
            // GetMobileAds();
        }
     
        public async void GetAndroidStoreAppVersion()
        {
            string version = "";
            string url = "https://play.google.com/store/apps/details?id=com.sulekha.nri&hl=en";
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (var handler = new HttpClientHandler())
                    {
                        using (var client = new HttpClient(handler))
                        {
                            using (var responseMsg = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                            {
                                //if (!responseMsg.IsSuccessStatusCode)
                                //{
                                //    throw new LatestVersionException($"Error connecting to the Play Store. Url={url}.");
                                //}

                                try
                                {
                                    var content = responseMsg.Content == null ? null : await responseMsg.Content.ReadAsStringAsync();

                                    var versionMatch = Regex.Match(content, "<div[^>]*>Current Version</div><span[^>]*><div[^>]*><span[^>]*>(.*?)<").Groups[1];

                                    if (versionMatch.Success)
                                    {
                                        version = versionMatch.Value.Trim();
                                        if (version != Commonsettings.Userappversion)
                                        {
                                            var answer = await dialog.ConfirmAsync("", "New version available", "Update", "");
                                            if (answer)
                                            {
                                                Device.OpenUri(new Uri(url));
                                            }
                                            else
                                                Device.OpenUri(new Uri(url));
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    string msg = e.Message;
                                    //throw new LatestVersionException($"Error parsing content from the Play Store. Url={url}.", e);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            // return version;
        }

        public async void GetIosStoreAppVersion()
        {
            string iOsStoreAppVersion = null;

            string url = "http://itunes.apple.com/lookup?bundleId=id483101010";


            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string jsonString = webClient.DownloadString(string.Format(url));

                    var lookup = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);


                    if (lookup != null
                        && lookup.Count >= 1
                        && lookup["resultCount"] != null
                        && Convert.ToInt32(lookup["resultCount"].ToString()) > 0)
                    {

                        var results = JsonConvert.DeserializeObject<List<object>>(lookup[@"results"].ToString());


                        if (results != null && results.Count > 0)
                        {
                            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(results[0].ToString());
                            iOsStoreAppVersion = values.ContainsKey("version") ? values["version"].ToString() : string.Empty;
                            if (iOsStoreAppVersion != Commonsettings.Userappversion)
                            {
                                var answer = await dialog.ConfirmAsync("", "New version availiable", "Update", "");
                                if (answer)
                                {
                                    Device.OpenUri(new Uri("https://apps.apple.com/us/app/sulekhaus/id483101010?ls=1"));
                                }
                                else
                                    Device.OpenUri(new Uri("https://apps.apple.com/us/app/sulekhaus/id483101010?ls=1"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }


        }

        public async void GetMobileAds()
        {
            try
            {
                var ApiC = RestService.For<IUSHome>(Commonsettings.USHomeAPI);
                MOB_ADS list = await ApiC.Getmobileads(Commonsettings.UserMobileOS);
                if (list.ROW_DATA.Count > 0)
                {
                    DependencyService.Get<IAdInterstitial>().ShowAd(list.ROW_DATA.First().adunitid);
                }

            }
            catch (Exception e)
            {

            }
        }
        //public HomePage()
        //{
        //    InitializeComponent();
        //    if (Device.RuntimePlatform == Device.iOS)
        //        NavigationPage.SetBackButtonTitle(this, "Back");

        //    this.BindingContext = new USHomeViewModel();

        //}
        public void menuTap(object sender,EventArgs e)
        {
            if (IsPresented == false)
                IsPresented = true;
            else
                IsPresented = false;
        }
        public void Refresh()
        {
            Navigation.PushAsync(new NRIApp.USHome.Views.HomePage());
        }
        private void ServicesTap(object sender, EventArgs e)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
             Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Servicetype());
            //Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Listing.Onlineclasses());
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
        private void FavoritesTap(object sender, EventArgs e)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
            {
                if (Commonsettings.UserPid != "" && Commonsettings.UserPid != "0" && !string.IsNullOrEmpty(Commonsettings.UserPid))
                {
                    Application.Current.MainPage.Navigation.PushAsync(new NRIApp.USHome.Views.Favorites());
                }
                else
                    Navigation.PushModalAsync(new Signin.Views.Login());
            }

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

        private void resumesTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
        }


        private void jobsTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Firststep());
        }
        public static int seekerlogincode = 0;
        private  void jobseekerTap(object sender, EventArgs e)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                 Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                seekerlogincode = 1;
            }
            else
            {
                seekerlogincode = 0;
                LocalJobs.Features.Jobseeker.Models.Jobseekers_DATA data = new LocalJobs.Features.Jobseeker.Models.Jobseekers_DATA();
                Navigation.PushAsync(new LocalJobs.Features.Jobseeker.View.Jobseekerprofile(data));
            }

        }
        protected override void OnAppearing()
        {
           try
            {
                base.OnAppearing();
                if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                    cityname.Text = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                else
                    cityname.Text = "New York, NY";

                if (Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    loginstack.IsVisible = false;
                    logoutstack.IsVisible = true;
                    lblusr.Text = Commonsettings.UserName;
                }
                else
                {
                    loginstack.IsVisible = true;
                    logoutstack.IsVisible = false;
                    lblusr.Text = "Guest";
                }
                NRIApp.LocalJobs.Features.Posting.Views.JobSuccess.viewadcode = 0;
                if (NRIApp.USHome.Views.HomePage.seekerlogincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    LocalJobs.Features.Jobseeker.Models.Jobseekers_DATA data = new LocalJobs.Features.Jobseeker.Models.Jobseekers_DATA();
                    Navigation.PushAsync(new LocalJobs.Features.Jobseeker.View.Jobseekerprofile(data));
                }
                else if(myneedslogincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    Navigation.PushAsync(new NRIApp.MyNeeds.Features.Views.MyNeeds());
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public async void getloc()
        {
            try
            {
                dialog.ShowLoading("", null);
                var hasPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (hasPermission != PermissionStatus.Granted)
                {
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

                            Getlocation();
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
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
      
        private void HorizontalTap(object sender, EventArgs e)
        {

            // Navigation.PushAsync(new HorizontalListviewpage());
        }
        CategoryList catlistt = new CategoryList();
        private void RoommatesTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(catlistt));
        }
        Rentals.Features.Post.Models.RTCategoryList rtcatlistt = new Rentals.Features.Post.Models.RTCategoryList();
        private void RentalsTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtcatlistt));
        }
        NRIApp.DayCare.Features.Post.Models.DC_CategoryList dccatlist = new DayCare.Features.Post.Models.DC_CategoryList();
        private void DaycareTap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Catfist(dccatlist));
        }

        private void LoginTap(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
        }

        private void callus()
        {
            var phonecall = CrossMessaging.Current.PhoneDialer;
            if (phonecall.CanMakePhoneCall)
                phonecall.MakePhoneCall("15127885300", null);
        }
        private void OnEmailButtonClicked(object sender, EventArgs e)
        {
            var sendEmail = CrossMessaging.Current.EmailMessenger;

            if (sendEmail.CanSendEmail)
            {
                sendEmail.SendEmail("us.sulekha@sulekha.net", "", "");
            }
        }

        private async void LogoutTap()
        {
            var answer = await dialog.ConfirmAsync("Are you sure?", "Confirm", "Yes", "No");
            if (answer)
            {
                Commonsettings.UserEmail = "";
                Commonsettings.UserMobileno = "";
                Commonsettings.UserName = "";
                Commonsettings.UserPid = "";
                lblusr.Text = "Guest";
                loginstack.IsVisible = true;
                logoutstack.IsVisible = false;

            }
            else
            {
                loginstack.IsVisible = false;
                logoutstack.IsVisible = true;
            }
        }
        public static int myneedslogincode = 0;
        private  void MyNeedsButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    myneedslogincode = 1;
                    Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    myneedslogincode = 0;
                    byte[] encData_byte = new byte[Commonsettings.UserEmail.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(Commonsettings.UserEmail);
                    string encodedData = Convert.ToBase64String(encData_byte);
                    //Device.OpenUri(new Uri("https://techjobs.sulekha.com/mobileapp/autologin.aspx?useremail="+encodedData));
                    Navigation.PushAsync(new MyNeeds.Features.Views.MyNeeds());
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        private void jobresumeTap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (Commonsettings.UserPid != null && Commonsettings.UserPid != "")
        //    {
        //        loginstack.IsVisible = false;
        //        logoutstack.IsVisible = true;
        //        lblusr.Text = Commonsettings.UserName;
        //    }
        //    else
        //    {
        //        loginstack.IsVisible = true;
        //        logoutstack.IsVisible = false;
        //        lblusr.Text = "Guest";
        //    }
        //}
    }
}
