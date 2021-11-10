using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Signin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProviderLoginPage : ContentPage
    {
        public string LoginVia { get; set; }
        public ProviderLoginPage(string loginvia)
        {
            LoginVia = loginvia;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Commonsettings.UserEmail != "" && Commonsettings.UserEmail != null)
            {
                Navigation.PushAsync(new USHome.Views.HomePage());
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            }
        }
        //protected override bool OnBackButtonPressed()
        //{
        //    var currentpage = GetCurrentPage();
        //    currentpage.Navigation.PopToRootAsync();
        //    return true;
        //}
        //private Page GetCurrentPage()
        //{
        //    var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
        //    return currentPage;
        //}
    }
}