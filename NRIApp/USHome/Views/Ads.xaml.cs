using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.USHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ads : ContentPage
    {
        private HtmlWebViewSource _Htmlwebviewsource;

        public HtmlWebViewSource Htmlwebviewsource

        {

            get
            {
                return _Htmlwebviewsource;
            }

            set
            {

                _Htmlwebviewsource = value;

            }

        }
        public Ads()
        {
            InitializeComponent();
            Uri uri = new Uri("https://techjobs.sulekha.com/adtest.html");
            WebRequest request = HttpWebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string htmls = reader.ReadToEnd();
            var htm = new HtmlWebViewSource
            {
                Html = htmls
            };

            htmlweb.Source = htm;
          
           
           
           
        }
        private void Skipadspage(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
           
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
    }
}