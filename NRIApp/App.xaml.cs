using System;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Models;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;


namespace NRIApp
{
	public partial class App : Application
	{
        Postfirst pst = new Postfirst();
        public static int screenHeight, screenWidth;
        public App ()
		{
           
            Commonsettings.UserLat = 0;
            Commonsettings.UserLong = 0;
            // getloc();
            InitializeComponent();
           
            if (Device.RuntimePlatform == Device.iOS)
            {
                Commonsettings.UserMobileOS = "iphone";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Commonsettings.UserMobileOS = "android";
            }
            //if (string.IsNullOrEmpty(Commonsettings.UserDeviceId))
                DeviceInfo.SetDeviceDetails();
            Commonsettings.UserDeviceHeight = Convert.ToString(screenHeight - 180);
            Commonsettings.UserDeviceWidth = Convert.ToString(screenWidth - 150);
            MainPage = new NavigationPage(new NRIApp.USHome.Views.Welcomescreen());
        }


        protected override void OnStart()
        {
            // Handle when your app starts
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                string token = p.Token;


                //System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                IUserDialogs _Dialog = UserDialogs.Instance;
                // notificationid = 0;

                //var currntpage = GetCurrentPage();
                //if (p.Data.Count > 1)
                //{
                //    foreach (var dt in p.Data)
                //    {
                //        if (dt.Key == "adid")
                //        {
                //            adid = dt.Value.ToString();
                //        }
                //        if (dt.Key == "resptype")
                //        {
                //            resptype = dt.Value.ToString();
                //        }
                //        if (dt.Key == "respid")
                //        {
                //            respid = dt.Value.ToString();
                //        }
                //    }
                //    notificationid = 1;
                //    if (resptype == "0")
                //        currntpage.Navigation.PushAsync(new SulekhaNRIBusiness.Localservices.Views.Jobdetail(adid, respid));
                //    else if (resptype == "1")
                //        currntpage.Navigation.PushAsync(new SulekhaNRIBusiness.Localservices.Views.ResponseDetail(adid, respid));
                //    else
                //        currntpage.Navigation.PushAsync(new SulekhaNRIBusiness.Localservices.Views.Jobs());

                //}
                //else
                //{
                //    currntpage.Navigation.PopToRootAsync();
                //}
            };
        }
        public static Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return currentPage;
        }
        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            // Handle when your app resumes
           
        }
        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Type currentPage = this.MainPage.GetType();
            if (e.IsConnected && currentPage != typeof(MainPage))
                this.MainPage = new MainPage();
            else if (!e.IsConnected && currentPage != typeof(MainPage))
                this.MainPage = new MainPage();
        }
    }
}
