using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Foundation;
using Newtonsoft.Json;
using NRIApp.Helpers;
using NRIApp.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NRIApp.Signin.Views.ProviderLoginPage), typeof(LoginRenderer))]
namespace NRIApp.iOS.CustomRenderers
{
    public class LoginRenderer : PageRenderer
    {
        bool showLogin = true;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);


            if (string.IsNullOrEmpty(Commonsettings.UserPid) && USHome.Models.OAuthConfig.User == null)
            {
                showLogin = false;
                var loginPage = Element as NRIApp.Signin.Views.ProviderLoginPage;
                string providername = loginPage.LoginVia;
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
                                    Commonsettings.UserName = emaildata.name;
                                    Commonsettings.UserEmail = emaildata.email;
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
                        }
                    };
                    PresentViewController(auth.GetUI(), true, null);
         
               
            }

        }
    }
}