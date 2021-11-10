using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Jobresume.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Savedresumes : ContentPage
	{
		public Savedresumes ()
		{
			InitializeComponent ();
            NRIApp.LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM.logincode = 0;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new NRIApp.LocalJobs.Features.Jobresume.ViewModels.SavedresumeVM();
            Title = "Saved Resumes";
        }
	}
}