using NRIApp.Helpers;
using NRIApp.Signin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Signin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Signup : ContentPage
	{
		public Signup ()
		{
			InitializeComponent ();
            this.BindingContext = new Signinviewmodel();
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }

        //public void getnumber()
        //{
        //    string mobile = DependencyService.Get<IDeviceInfo>().GetDeviceMobileno();
        //}

        //private void Lblph_Focused(object sender, FocusEventArgs e)
        //{
        //    string mobile = DependencyService.Get<IDeviceInfo>().GetDeviceMobileno();
        //    DisplayAlert("",mobile,"OK");
        //}
    }
}