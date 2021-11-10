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
using NRIApp.Droid.CustomRenderers;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms.Platform.Android;
using System.IO;
using System.Net;
using NRIApp.Helpers;

[assembly: ExportRenderer(typeof(NRIApp.Signin.Views.ProviderLoginPage), typeof(LoginRenderer))]
namespace NRIApp.Droid.CustomRenderers
{
#pragma warning disable CS0618
    public class LoginRenderer : PageRenderer
    {

        //bool showLogin = true;
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            //HomePage
            var loginPage = Element as NRIApp.Signin.Views.ProviderLoginPage;
            string providername =loginPage.LoginVia;
            var activity = this.Context as Activity;

            if (string.IsNullOrEmpty(Commonsettings.UserPid) && USHome.Models.OAuthConfig.User == null)
            {
                // showLogin = false;
                OAuthProviderSetting oauth = new OAuthProviderSetting();


                var auth = oauth.LoginWithProvider(providername);


                auth.Completed += async (sender, eventArgs) =>
                {

                    if (eventArgs.IsAuthenticated)
                    {
                        string dd = eventArgs.Account.Username;
                        var values = eventArgs.Account.Properties;
                        var access_token = values["access_token"];

                        try
                        {
                            if (providername == "Gmail")
                            {
                                var httpclient = new HttpClient();
                                var json = await httpclient.GetStringAsync(@"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + access_token + "&format=json");
                                var emaildata = JsonConvert.DeserializeObject<NRIApp.USHome.Models.LoginUserDetails>(json);
                                string phonenumber = emaildata.phonenumbers;
                                Commonsettings.UserEmail = emaildata.email;
                                Commonsettings.UserName = emaildata.name;
                            }

                            if (providername == "Facebook")
                            {
                                var httpclient = new HttpClient();
                                var requestUrl = "https://graph.facebook.com/v2.7/me/?fields=name,email,first_name,last_name,gender,is_verified,languages&access_token=" + access_token;
                                Uri uri = new Uri(requestUrl);
                                WebRequest request = HttpWebRequest.Create(uri);
                                WebResponse response = request.GetResponse();
                                StreamReader reader1 = new StreamReader(response.GetResponseStream());
                                var result = reader1.ReadToEnd();
                                var Fbdata = JsonConvert.DeserializeObject<NRIApp.USHome.Models.LoginUserDetails>(result);
                                var em = Fbdata.email;
                                string id = Fbdata.id;
                                Commonsettings.UserName = Fbdata.name;
                                Commonsettings.UserEmail = em;
                            }
                            
                        }
                        catch (Exception exx)
                        {
                            System.Console.WriteLine(exx.ToString());
                        }
                    }
                    else
                    {
                        // The user cancelled 
                        NRIApp.USHome.Models.OAuthConfig.SuccessfulLoginAction.Invoke();

                    }
                };
                activity.StartActivity(auth.GetUI(activity));
            }
            else
            {
              //  NRIApp.USHome.Models.OAuthConfig.SuccessfulLoginAction.Invoke();
            }
        }
    }
}