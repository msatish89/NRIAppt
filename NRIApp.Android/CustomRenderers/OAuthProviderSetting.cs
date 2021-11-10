using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NRIApp.Droid.CustomRenderers
{
    public class OAuthProviderSetting
    {
        public Xamarin.Auth.OAuth2Authenticator LoginWithProvider(string Provider)
        {
            Xamarin.Auth.OAuth2Authenticator auth = null;
            
            if (Provider == "Gmail")
            {
                auth = new Xamarin.Auth.OAuth2Authenticator(
                clientId: "69626923092-mqnrecc8of3efjejpmg80ald8o0fll6r.apps.googleusercontent.com",
                clientSecret: "1MEwhCUQdQxVa_hMMtAwYn0K",
                scope: "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile",
                authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
                redirectUrl: new Uri("http://us.sulekha.com/"),
                accessTokenUrl: new Uri("https://accounts.google.com/o/oauth2/token"));
                auth.Title = "Gmail";
           
            }
            if (Provider == "Facebook")
            {
                auth = new Xamarin.Auth.OAuth2Authenticator(
                clientId: "512578702521102",
                scope: "email public_profile",
                authorizeUrl: new Uri("https://www.facebook.com/v2.8/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));
                auth.Title = "Facebook";
            }
            return auth;
        }
    }
}