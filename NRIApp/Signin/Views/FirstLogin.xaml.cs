using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Helpers;
using NRIApp.Signin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Signin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstLogin : ContentPage
	{
		public FirstLogin ()
		{
            //try
            //{
                InitializeComponent();
                this.BindingContext = new Signinviewmodel();
           // }
            //catch(Exception e)
            //{
            //    string ex = e.Message.ToString();
            //}
			
           
        }

        protected override bool OnBackButtonPressed()
        {
            Task.Run(async () =>
            {
                await Navigation.PopToRootAsync();
            });
            return true;
        }

        private async void Facebook_Tapped(object sender, EventArgs e)
        {
            string loginviafb = "Facebook";
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Signin.Views.ProviderLoginPage(loginviafb));
        }
        private async void Google_Tapped(object sender, EventArgs e)
        {
            string loginviagmail = "Gmail";
            var currntpage = GetCurrentPage();
            await currntpage.Navigation.PushAsync(new NRIApp.Signin.Views.ProviderLoginPage(loginviagmail));
        }
        private Page GetCurrentPage()
        {
            var currentpage = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
        //protected override void OnAppearing()
        //{
        //    if (Commonsettings.UserPid != "" && Commonsettings.UserPid != null)
        //}
    }
}