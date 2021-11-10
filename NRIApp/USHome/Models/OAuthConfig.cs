using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.USHome.Models
{
    public class OAuthConfig
    {
        public static USHome.Views.HomePage _HomePage;
        //static NavigationPage _NavigationPage;
        public static LoginUserDetails User;
        public OAuthConfig()
        {

        }
        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() =>
                {
                    if (string.IsNullOrEmpty(Commonsettings.UserEmail))
                    {
                        App.Current.MainPage.Navigation.PopAsync();
                        //App.Current.MainPage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                        //App.Current.MainPage.Navigation.PushAsync(new NRIApp.Signin.Views.Login());
                    }
                    else
                    {
                        // App.Current.MainPage.Navigation.PushAsync(new USHome.Views.HomePage());
                        App.Current.MainPage.Navigation.PopModalAsync();
                    }
                });
            }
        }
        public static Action AfterloginSuccess
        {
            get
            {
                return new Action(() =>
                {
                    App.Current.MainPage.Navigation.PopToRootAsync();
                });

            }
        }
    }
}
