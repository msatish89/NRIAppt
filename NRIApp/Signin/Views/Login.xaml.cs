using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Signin.ViewModels;

namespace NRIApp.Signin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
            this.BindingContext = new Signinviewmodel(); 
		}

        //public void btnclick()
        //{
        //  PopupNavigation.Instance.PushAsync(new NRIApp.Signin.Views.Popuptest());
        //   // PopupNavigation.Instance.PopAsync(true);
        //}

        private async void Facebook_Tapped(object sender, EventArgs e)
        {
            string loginviafb = "Facebook";
            var currntpage = GetCurrentPage();
            await currntpage.Navigation.PushAsync(new ProviderLoginPage(loginviafb));
        }
        private async void Google_Tapped(object sender, EventArgs e)
        {
            string loginviagmail = "Gmail";
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Signin.Views.ProviderLoginPage(loginviagmail));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}