using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.Post.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class lcfthankyou : ContentPage
    {
        public lcfthankyou()
        {
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PopToRootAsync();
            return true;
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}