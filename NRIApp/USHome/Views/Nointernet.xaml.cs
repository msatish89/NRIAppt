using Acr.UserDialogs;
using NRIApp.Helpers;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.USHome.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Nointernet : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        public Nointernet ()
		{
			InitializeComponent ();
		}
        public async void Gotohome()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
            {
                dialog.ShowLoading("", MaskType.Black);
                if (Commonsettings.UserPid != "" && Commonsettings.UserPid != null)
                {
                    Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
                    ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
                    ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                }
                else
                {
                    await Navigation.PushAsync(new NRIApp.Signin.Views.FirstLogin());
                }
            }
               
        }
	}
}