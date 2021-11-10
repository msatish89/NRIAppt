using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.ViewModels;
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
	public partial class LcfotpRT : ContentPage
	{
		public LcfotpRT (RTResponse res, string pinno)
		{
            string diff = "1";
			InitializeComponent ();
            lcfsecond.postsuccessbackbtn = 1;
            if (res.isResponseotp==1)
            {
                BindingContext = new VMRTLCF(res.isResponseotp,res, pinno);
            }
            else
            {
                BindingContext = new VMRTLCF(res, pinno, diff);
            }
            
        }
        protected override bool OnBackButtonPressed()
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