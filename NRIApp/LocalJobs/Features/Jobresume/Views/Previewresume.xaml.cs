using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobresume.Interfaces;
using NRIApp.LocalJobs.Features.Jobresume.Models;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Jobresume.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Previewresume : ContentPage
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        string contentID = "";
        public Previewresume(string resumepath)
        {
            InitializeComponent();
            contentID = resumepath;
            Title = "Profile Preview";
            previewresume(resumepath);
        }

        //protected override void OnAppearing()
        //{
        //    try
        //    {

        //        if (resumepreviewlogincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
        //        {
        //            previewresume(contentID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public static int resumepreviewlogincode = 0;
        public async void previewresume(string resumepath)
        {
            string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
            try
            {
                //resumepreviewlogincode = 0;
                //dialogs.ShowLoading("", null);
                ////var resumedownloadapi = RestService.For<IJobresume>("http://localjobs.sulekha.com/mobileappresume");
                //var resumedownloadapi = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                //var downloaddata = await resumedownloadapi.previewresume(Commonsettings.UserPid, Commonsettings.UserEmail, id, Commonsettings.UserName);
                //if (downloaddata!=null)
                //{
                //    if (downloaddata.availiableresumes == "fail")
                //    {
                //        var alert = await Application.Current.MainPage.DisplayAlert("To preview the resume, Kindly subscribe to our ", "Resume Package Services.", "Yes", "Back");
                //        if (alert)
                //        {
                //            await Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
                //        }
                //        else
                //        {
                //        if(Commonsettings.UserDeviceOSVersion == "android")
                //        {
                //            await Navigation.PopAsync();
                //        }
                //        else
                //        {
                //            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                //        }
                //        }
                //    }
                //    else
                //    {
                //       // var alert = await Application.Current.MainPage.DisplayAlert("Available Resume count " + downloaddata.availiableresumes, "", "Ok", " ");

                if (!string.IsNullOrEmpty(resumepath))
                {
                    string url = resumepath;//"http://localjobs.sulekha.com/classifiedshtmls/toolresumes/9786010259.html";
                    Uri uri = new Uri(url);
                    WebRequest request = HttpWebRequest.Create(uri);
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    var htmlSource = new HtmlWebViewSource();
                    string htmlCode;
                    using (WebClient client = new WebClient())
                    {
                        htmlCode = client.DownloadString(url);
                    }
                    htmlCode = Regex.Replace(htmlCode, "<style>(.|\n)*?</style>", string.Empty).Replace("]]>", "");
                    var htm = new HtmlWebViewSource
                    {
                        Html = htmlCode
                    };

                    // htmlCode = Regex.Replace(htmlCode, "<style>(.|\n)*?</style>", string.Empty);
                    resume.Source = htm;
                }


                dialogs.HideLoading();
                //}
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }
    }
}