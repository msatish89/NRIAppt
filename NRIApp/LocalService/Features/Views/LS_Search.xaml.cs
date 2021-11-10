using NRIApp.LocalService.Features.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_Search : ContentPage
    {
        public LS_Search()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new LS_ViewModel("search");

        }
        protected override void OnAppearing()
        {
           // txtsearch.Focus();
        }
    }
}