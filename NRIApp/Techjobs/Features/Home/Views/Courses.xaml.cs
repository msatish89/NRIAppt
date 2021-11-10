using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Techjobs.Features.Home.ViewModels;
using NRIApp.Techjobs.Features.Home.Interfaces;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Connectivity;
using Acr.UserDialogs;
using NRIApp.Helpers;

namespace NRIApp.Techjobs.Features.Home.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Courses : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        public Courses ()
		{
            var connected = CrossConnectivity.Current.IsConnected;
            InitializeComponent();
            if (connected == true)
            {
               
                Title = "Trending Courses";
                if (Device.RuntimePlatform == Device.iOS)
                    NavigationPage.SetBackButtonTitle(this, "Back");
                BindingContext = new ViewModels.homeviewmodel("", "course");
            }

        }
        
    }
}