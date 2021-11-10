using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.ViewModels;

namespace NRIApp.Roommates.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class lcfsecond : ContentPage
	{
        public static int postscuccesbackbtn = 0;
		public lcfsecond (Response Catl, DateTime FrDate, string Gender, string  txtrent,string countflag)
		{
			InitializeComponent ();
            postscuccesbackbtn = 0;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMLCF(Catl, FrDate, Gender, txtrent,countflag);
            countrycode.SelectedIndex = 0;
            Title = "Enter Contact Details";
            if(Catl.ismyneed=="1")
            {
                txtcontactemail.IsEnabled = false;
            }
            //txtcontactname.NextView = txtcontactemail;
            //txtcontactname.Completed += Txtcontactname_Completed;
            //txtcontactname.Completed += (s, e) => txtcontactemail.Focus();
            //txtcontactemail.Completed += (s, e) => txtcontactphone.Focus();
            txtcontactphone.Completed += (s, e) => submitbtn.Command.Execute(submitbtn);
        }
        
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }

    
    }
}