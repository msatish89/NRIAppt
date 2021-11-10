using NRIApp.DayCare.Features.Post.Models;
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
	public partial class OTP_DC : ContentPage
	{
		public OTP_DC (Daycareposting post)
		{
			InitializeComponent ();
            NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid = 1;
            var vm = new ViewModels.OTP_VM(post);
            vm.StackVisible = true;
            vm.OTP = post.pinno;
            vm.Respid = post.businessid;
            vm.Mobileno = post.ContactNo;
            vm.Selectcountry = post.countrycode;
            Task.Delay(100);
            this.BindingContext = vm;
           // BindingContext = new NRIApp.DayCare.Features.Post.ViewModels.OTP_VM(post);
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
    }
}