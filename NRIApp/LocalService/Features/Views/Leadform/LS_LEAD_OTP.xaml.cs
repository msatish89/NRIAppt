using NRIApp.LocalService.Features.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Leadform
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_LEAD_OTP : ContentPage
    {
        public LS_LEAD_OTP(LS_LEAD_FORM llf, string bizid = null, string pinno = null, string mobileno = null)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            InitializeComponent();
            BindingContext = new ViewModels.LS_Leadform_VM(llf, pinno);
            ViewModels.LS_Leadform_VM._Businessid = bizid;
            ViewModels.LS_Leadform_VM.Contactnumber = mobileno;

        }
        public LS_LEAD_OTP(LS_RESP_FORM lrf, string bizid = null, string pinno = null, string mobileno = null)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            InitializeComponent();
            BindingContext = new ViewModels.LS_Listing_VM(lrf, pinno);
            ViewModels.LS_Listing_VM._Adid = bizid;
            ViewModels.LS_Listing_VM.Contactnumber = mobileno;
        }
        protected override bool OnBackButtonPressed()
        {
            var curpage =ViewModels.LS_ViewModel.GetCurrentPage();
            try { 
            curpage.Navigation.PopToRootAsync();
            }
            catch (Exception ee)
            {

            }
            return true;
        }
    }
}