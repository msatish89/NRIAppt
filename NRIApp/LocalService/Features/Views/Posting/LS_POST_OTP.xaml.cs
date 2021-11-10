using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Posting
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_POST_OTP : ContentPage
	{
        
        public LS_POST_OTP(Business llf, string bizid = null, string pinno = null, string mobileno = null)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            InitializeComponent();
            BindingContext = new ViewModels.LS_Posting_VM(llf);
            ViewModels.LS_Posting_VM._Pinno = pinno;
            ViewModels.LS_Posting_VM._Businessid = bizid;
            ViewModels.LS_Posting_VM.Contactnumber = llf.contactNumber;
            ViewModels.LS_Posting_VM.sCountrcode = llf.countrycode;
            LS_ViewModel.isOTPpage = 1;
            Bindshowtimer();


        }
      
        public async void Bindshowtimer()
        {
            await Task.Delay(3000);
            int TotalSec = 30;
            frametimer.IsVisible = true;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // Do something
                if (TotalSec == 0)
                {
                   
                    frametimer.IsVisible = false;
                    stackcallotp.IsVisible = true;
                    return false;
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    TotalSec = TotalSec - 1;
                    TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                   
                    lbltimer.Text = string.Format("{00}", _TimeSpan.TotalSeconds);
                });
                return true;

            });
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            return true;
        }
    }
}