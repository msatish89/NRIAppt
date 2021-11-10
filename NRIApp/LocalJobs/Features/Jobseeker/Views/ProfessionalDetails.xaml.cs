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
    public partial class ProfessionalDetails : ContentPage
    {
        public ProfessionalDetails(Jobseekers_DATA Sdata)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Professional Details";
            BindingContext = new NRIApp.LocalJobs.Features.Jobseeker.ViewModels.ProfessionalDetailsVM(Sdata);
            if(!string.IsNullOrEmpty(Sdata.salarymode))
            {
                perdatatxt.TextColor = Color.FromHex("#212121");
            }
            if(!string.IsNullOrEmpty(Sdata.functionalarea))
            {
                functionalareatxt.TextColor = Color.FromHex("#212121");
            }
            if(!string.IsNullOrEmpty(Sdata.experience.ToString()))
            {
                experienceyrtxt.TextColor = Color.FromHex("#212121");
            }
        }
    }
}