using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.USHome.ViewModels;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.USHome.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeMenuPage : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        public HomeMenuPage ()
		{
			InitializeComponent ();
           // this.BindingContext = new USHomeViewModel();
        }

        private void TechjobsTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Courses());
        }
        private void ServicesTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Servicetype());
        }
        private void HorizontalTap(object sender, EventArgs e)
        {

            // Navigation.PushAsync(new HorizontalListviewpage());
        }
        NRIApp.Roommates.Features.Post.Models.CategoryList catlistt = new NRIApp.Roommates.Features.Post.Models.CategoryList();
        private void RoommatesTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(catlistt));
        }
        Rentals.Features.Post.Models.RTCategoryList rtcatlistt = new Rentals.Features.Post.Models.RTCategoryList();
        private void RentalsTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtcatlistt));
        }
        NRIApp.DayCare.Features.Post.Models.DC_CategoryList dccatlist = new DayCare.Features.Post.Models.DC_CategoryList();
        private void DaycareTap(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Catfist(dccatlist));
        }
        private void EventsTap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NRIApp.Events.Features.Listing.Views.Listing());
        }


        private void LoginTap(object sender, EventArgs e)
        {
           Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
        }

        private void callus()
        {
            var phonecall = CrossMessaging.Current.PhoneDialer;
            if (phonecall.CanMakePhoneCall)
                phonecall.MakePhoneCall("15127885300", null);
        }

        private async void LogoutTap()
        {
            var answer = await dialog.ConfirmAsync("Aru you sure?", "Confirm", "Yes", "No");
            if (answer)
            {
                Commonsettings.UserEmail = "";
                Commonsettings.UserMobileno = "";
                Commonsettings.UserName = "";
                Commonsettings.UserPid = "";
                lblusr.Text = "Guest";
                loginstack.IsVisible = true;
                logoutstack.IsVisible = false;

            }
            else
            {
                loginstack.IsVisible = false;
                logoutstack.IsVisible = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Commonsettings.UserPid != null && Commonsettings.UserPid != "")
            {
                loginstack.IsVisible = false;
                logoutstack.IsVisible = true;
                lblusr.Text = Commonsettings.UserName;
            }
            else
            {
                loginstack.IsVisible = true;
                logoutstack.IsVisible = false;
                lblusr.Text = "Guest";
            }
        }
    }
}