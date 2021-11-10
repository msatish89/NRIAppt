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
	public partial class PostOTP : ContentPage
	{
		public PostOTP (Postfirst pst, string pinno)
		{
            try
            {
                InitializeComponent();
                lcfsecond.postscuccesbackbtn = 1;
                BindingContext = new VMPost(pst, pinno);
                newcountrycode.SelectedIndex = 0;
            }
            catch (Exception e) { }

        }
        protected override bool OnBackButtonPressed()
        {
            var curpage = GetCurrentPage();
            curpage.Navigation.PopToRootAsync();
            return true;
          //  Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }
        //
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        //protected override void OnAppearing()
        //{
        //   // var currentpage = GetCurrentPage();
        //    //if (lcfsecond.postscuccesbackbtn == 0)
        //    //{
        //    //    currentpage.Navigation.PopAsync();
        //    //}
        //    //else

        //    //
        //    //if (lcfsecond.postscuccesbackbtn == 1)
        //    //{
        //    //   // currentpage.Navigation.PopAsync();
        //    //    currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        //    //}
        //}
    }
}