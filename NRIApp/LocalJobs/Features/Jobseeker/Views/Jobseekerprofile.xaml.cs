using NRIApp.LocalJobs.Features.Jobseeker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Jobseeker.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Jobseekerprofile : ContentPage
	{
        public Jobseekerprofile (Jobseekers_DATA data)
		{
			InitializeComponent ();
            NRIApp.USHome.Views.HomePage.seekerlogincode = 0;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new Features.Jobseeker.ViewModels.JobseekerprofileVM(data);
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        
    }
}