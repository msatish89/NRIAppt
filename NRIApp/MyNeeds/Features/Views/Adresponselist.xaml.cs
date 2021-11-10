using NRIApp.MyNeeds.Features.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.MyNeeds.Features.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adresponselist : ContentPage
    {
        public Adresponselist(MyNeedData addata)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Responses";
            BindingContext = new NRIApp.MyNeeds.Features.ViewModel.Adresponselist_VM(addata);
        }
    }
}