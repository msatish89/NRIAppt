using Newtonsoft.Json;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Detail.Interfaces;
using NRIApp.LocalJobs.Features.Detail.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
namespace NRIApp.LocalJobs.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Fulldescription : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;
        public Fulldescription(string folderid,string adid)
		{
			InitializeComponent ();
            method(folderid,adid);
            Title = "Description";
        }
        public async void method(string foldrid,string adid)
        {
            try
            {
                dialogs.ShowLoading("",null);
                await Task.Delay(50);
                string url = "http://localjobs.sulekha.com/classifiedshtmls/"+foldrid+"/"+adid+".html";
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
                htmlCode = Regex.Replace(htmlCode, "<style>(.|\n)*?</style>", string.Empty).Replace("]]>","");
                var htm = new HtmlWebViewSource
                {
                    Html = htmlCode
                };

               // htmlCode = Regex.Replace(htmlCode, "<style>(.|\n)*?</style>", string.Empty);
                fulldesc.Source = htm;
                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }
    }
}