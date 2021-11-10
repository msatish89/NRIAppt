using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Thankyou_RT : ContentPage
	{
		public Thankyou_RT ()
		{
			InitializeComponent ();
		}
        public void PostClose(object sender,EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            Navigation.PopAsync();
           // Navigation.PopToRootAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            var currentpage = GetCurrentPage();
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            //Navigation.PopAsync();
            currentpage.Navigation.PopToRootAsync();
            return true;
        }
        private Page GetCurrentPage()
        {
            var CurrentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return CurrentPage;
        }
    }
}