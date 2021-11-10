using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.LeadForm.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OTPform : ContentPage
	{
		public OTPform (string otp, string respid, string mobileno,string code)
		{
			InitializeComponent ();
            var vm = new ViewModels.Leadformviewmodel();
            vm.StackVisible = true;
            vm.OTP = otp;
            vm.Respid = respid;
            vm.Mobileno = mobileno;
            vm.Selectcountry = code;
            this.BindingContext = vm;
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            //Task.Run(async () =>
            //{
           
           // await Navigation.PopToRootAsync();
            //});
            return true;
        }

    }
}