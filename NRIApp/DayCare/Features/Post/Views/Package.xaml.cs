using NRIApp.DayCare.Features.Post.Models;
using Plugin.Messaging;
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
	public partial class Package : ContentPage
	{
        public Package (Daycareposting post)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");

            Title = "Choose Package";
            BindingContext = new NRIApp.DayCare.Features.Post.ViewModels.Package_VM(post);
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModels.PaymentDC_VM.discountamount = "";
        }
        protected override bool OnBackButtonPressed()
        {
            var currentpage = GetCurrentPage();
            if (NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
            return true;
           // return base.OnBackButtonPressed();
        }
        private void Backbutton_Tapped(object obj, EventArgs e)
        {
            var currentpage = GetCurrentPage();
            if (NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
        }
        public Page GetCurrentPage()
        {
            var currentpg = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpg;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var phonecall = CrossMessaging.Current.PhoneDialer;
                if (phonecall.CanMakePhoneCall)
                    phonecall.MakePhoneCall("15123600100", null);
            }
            catch(Exception ex)
            {

            }
        }

        private void OnEmailButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var sendEmail = CrossMessaging.Current.EmailMessenger;

                if (sendEmail.CanSendEmail)
                {
                    sendEmail.SendEmail("us.sulekha@sulekha.net", "", "");
                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}