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
	public partial class PostOTP : ContentPage
	{
		public PostOTP (RTPostFirst pst, string pinno)
		{
			InitializeComponent ();
            lcfsecond.postsuccessbackbtn = 1;
            BindingContext = new VMRTPost(pst, pinno);
           // newcountrycode.SelectedIndex = 0;
        }
        protected override bool OnBackButtonPressed()
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PopToRootAsync();
            return true;
        }
        private Page GetCurrentPage()
        {
            var CurrentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return CurrentPage;
        }
        //protected override void OnAppearing()
        //{
        //    var currentpage = GetCurrentPage();
        //    if (lcfsecond.postsuccessbackbtn == 1)
        //    {
        //        currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        //    }
        //}
    }
}