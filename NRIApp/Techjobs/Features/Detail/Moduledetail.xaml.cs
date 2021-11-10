using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NRIApp.Techjobs.Features.Detail.Models;
using NRIApp.Techjobs.Features.Detail.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.Detail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class Moduledetail : ContentPage
	{
        public ObservableCollection<Bizmoduleslist> bizmodlist { get; set; }
        string modid = "", bisid = "";
        IUserDialogs dialog = UserDialogs.Instance;
        public Moduledetail (string module, string moduleid, string businessid,string businessname)
		{
			InitializeComponent ();
            dialog.ShowLoading("", null);
            Title = module + " Training in "+businessname;
            lbltitle.Text = Title;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
           
            var vm = new ModuledetailVM(moduleid,businessid);
            this.BindingContext = vm;
            modid = moduleid;
            bisid = businessid;
            //bizmodlist = vm.bizmodlist2;
            // Display(moduleid);

        }

        public void Display(string moduleid)
        {
            string url = "https://az827626.vo.msecnd.net/cdn/techjobshtml/module/165225.html";
            // htmlweb.Source = GetHtmlSource(url);
        }
        public HtmlWebViewSource GetHtmlSource(string url)
        {
            var htmlSource = new HtmlWebViewSource();
            string htmlCode;
            string da = "";
            WebRequest request = HttpWebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    da = reader.ReadToEnd();
                }
            }


            // instruct the server to return headers only
            //request.Method = "HEAD";

            //// make the connection
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //// get the status code
            //HttpStatusCode status = response.StatusCode;
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead(url);
                if (stream != null)
                     da = "kk";
                    htmlCode = client.DownloadString(url);
            }
            var customCss = @"
              <body>
              <div>
        
        <style>
       .mywrapper, .wrapper, .mycss {
        width: 100% !important;
        height:100% !important;
        zoom:45%;
        padding:0,0,0,0;
        overflow-y: scroll;
    } 
    </div>
    </body>
        </style> ";
            htmlCode = htmlCode.Replace("<body>", customCss);

            htmlSource.Html = htmlCode;
            return htmlSource;
        }
        string text = "start";
        private void EditTextOnFocusChanged()
        {
            if(text=="start")
            {
                txtdesc.Text = "";
                txtdesc.TextColor = Color.Black;
                text = "end";
            }
        }

        public void getdetails(object sender,EventArgs e)
        {
            var details = sender as StackLayout;
            var dd = details.BindingContext as Bizmoduleslist;
            //dd.image = "minus.png";
            //var vm = new ModuledetailVM("", "");
            //vm.Opendetailscommand.Execute(dd);

            //bizmodlist = vm.bizmodlist2;
            // listbizdata.ItemsSource = bizmodlist;
        }
    }
}