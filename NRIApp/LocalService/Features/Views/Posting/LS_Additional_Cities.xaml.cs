using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.ViewModels;
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
    public partial class LS_Additional_Cities : ContentPage
    {
        public LS_Additional_Cities(int citycount, SELECTED_PACKAGE sp)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Expand to more cities";
            //   LS_ViewModel.valueadded = 0;
            //   LS_ViewModel.Valueaddedcontentid = "";
           // BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_Package_VM(citycount, sp);
        }

        private void Citytext_Focused(object sender, FocusEventArgs e)
        {
            frameaddtionalamtblk.IsVisible = false;
        }

        private void Citytext_Unfocused(object sender, FocusEventArgs e)
        {
            frameaddtionalamtblk.IsVisible = true;
        }
    }
}