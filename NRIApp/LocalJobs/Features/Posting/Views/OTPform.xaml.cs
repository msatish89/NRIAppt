using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using NRIApp.LocalJobs.Features.Posting.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OTPform : ContentPage
	{
		public OTPform(string otp, string respid, string mobileno, string code, Postingdata pst)
        {
			InitializeComponent ();
            pst.respid = respid;
            var vm = new OtpVM(pst);
            vm.StackVisible = true;
            vm.OTP = otp;
            vm.Respid = respid;
            vm.Mobileno = mobileno;
            vm.Selectcountry = code;
            Task.Delay(100);
            this.BindingContext = vm;
        }

        protected override bool OnBackButtonPressed()
        {
            //Task.Run(async () =>
            //{
            //    await Navigation.PopToRootAsync();
            //});
            //return true;
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