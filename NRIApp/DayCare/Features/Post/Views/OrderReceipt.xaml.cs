using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderReceipt : ContentPage
	{
		public OrderReceipt (string businessid,string noofdays,string pkgname)
		{
			InitializeComponent ();
            BindingContext = new NRIApp.DayCare.Features.Post.ViewModels.OrderReceipt_VM(businessid, noofdays, pkgname);

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