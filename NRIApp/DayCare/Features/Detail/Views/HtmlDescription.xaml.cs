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
using Acr.UserDialogs;

namespace NRIApp.DayCare.Features.Detail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HtmlDescription : ContentPage
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public HtmlDescription(string Folderid, string Businessid)
        {
            InitializeComponent();
            htmldescription(Folderid, Businessid);
        }
        public void htmldescription(string Folderid,string Businessid)
        {
            try
            {
                dialogs.ShowLoading("", null);
                string url = "http://daycare.sulekha.com/classifiedshtmls/" + Folderid + "/" + Businessid + ".html";
                Uri uri = new Uri(url);
                WebRequest request = HttpWebRequest.Create(uri);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                //var htmlSource = new HtmlWebViewSource();
                //string htmlCode;
                //using (WebClient client = new WebClient())
                //{
                //    htmlCode = client.DownloadString(url);
                //}
                result = Regex.Replace(result, "<style>(.|\n)*?</style>", string.Empty).Replace("]]>", "");
                //string regex = "<iframe*>(.*?)</iframe>";
                //string regex1 = "<iframe>(.|\n)*?</iframe>";

                //result = Regex.Replace(result, regex, "");
                //result = Regex.Replace(result, regex1, "");
                //result = Regex.Replace(result, @"<iframe.*?/iframe>", "");


                //var customCss = @"<style>img{max-width:100%;height: auto;}p,div{font-size:14px}</style> ";
                //result = "<html><head>" + customCss + "</head><body>" + result + "</body></html>";
                var htm = new HtmlWebViewSource
                {
                    Html = result
                };

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