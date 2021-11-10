using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalJobs.Features.Posting.Models;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Jobdetails : ContentPage
	{
        public Jobdetails(Postingdata pd)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Enter Job Details";
            this.BindingContext = new JobdetailsVM(pd);
        }
    }
}