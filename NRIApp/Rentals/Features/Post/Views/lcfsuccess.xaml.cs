using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.ViewModels;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class lcfsuccess : ContentPage
	{
		public lcfsuccess(string result, int allrespid, RTResponse res, string cityurl)
		{
			InitializeComponent ();
           this.BindingContext = new VMRTLCFSUC(result, allrespid, res, cityurl);
		}
        protected override bool OnBackButtonPressed()
        {
              var currentpage = GetCurrentPage();
            //currentpage.Navigation.PopToRootAsync();
            //return true;

           
            if (lcfsecond.postsuccessbackbtn == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (lcfsecond.postsuccessbackbtn == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
            return true;
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}