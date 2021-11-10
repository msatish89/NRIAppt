using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Roommates.Features.Post.ViewModels;
using NRIApp.Roommates.Features.Post.Models;
namespace NRIApp.Roommates.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class lcfOTP : ContentPage
	{
        public static int lcfotpbackbtn;
		public lcfOTP (Response res, string pinno)
		{
			InitializeComponent ();
            lcfsecond.postscuccesbackbtn = 1;
            if (res.isResponseotp==1)
            {
                BindingContext = new VMLCF(res.isResponseotp,res,pinno);
            }
            else
            {
                BindingContext = new VMLCF(res, pinno);
            }
            //newcountrycode.SelectedIndex = 0;
        }
        protected  override bool OnBackButtonPressed()
        {
            var curpage = GetCurrentPage();
            curpage.Navigation.PopToRootAsync();
            return true;
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}