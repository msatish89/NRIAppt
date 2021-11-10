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
	public partial class lcfsuccess : ContentPage
	{
		public lcfsuccess (string result, int allrespid, Response res, string cityurl)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMLCFSUC(result, allrespid, res, cityurl);
            
        }
        protected override bool OnBackButtonPressed()
        {
            var currentpage = GetCurrentPage();
            // return true;

            if (lcfsecond.postscuccesbackbtn == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (lcfsecond.postscuccesbackbtn == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
            return true;
        }
        private Page  GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}