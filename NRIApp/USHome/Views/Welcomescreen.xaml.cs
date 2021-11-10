using NRIApp.Helpers;
using NRIApp.USHome.Interfaces;
using NRIApp.USHome.Models;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions.Abstractions;
using Acr.UserDialogs;
using System.Net;
using System.IO;
using NRIApp.Signin.Interfaces;
using NRIApp.Signin.Models;

namespace NRIApp.USHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Welcomescreen : ContentPage
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public List<Userlocdetails> locdetails { get; set; }
        public Welcomescreen()
        {
            InitializeComponent();
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            else
            {
                StartTimer();
            }
        }

        public async void StartTimer()
        {
            await Task.Delay(5); //60 minutes 
            Uri uri = new Uri("https://techjobs.sulekha.com/adtest.html");
            WebRequest request = HttpWebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string htmls = reader.ReadToEnd();
            if (htmls != "" && htmls != null)
                Commonsettings.isadpage = "1";
            // Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
            if (Commonsettings.UserPid != "" && Commonsettings.UserPid != null)
            {

                if (htmls == "" || htmls == null)
                {
                    var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                    Loginlock details = await nsAPI.Loginlocking(Commonsettings.UserEmail, Commonsettings.UserDeviceId, Commonsettings.UserMobileOS, Commonsettings.Userappversion, "2");
                    Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
                    ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
                    ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                }
                else
                    await Navigation.PushAsync(new NRIApp.USHome.Views.Ads());


            }

            else
                await Navigation.PushAsync(new NRIApp.Signin.Views.FirstLogin());
            // Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
        }



    }
}