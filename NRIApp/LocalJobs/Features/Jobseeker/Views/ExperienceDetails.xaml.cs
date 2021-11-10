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
	public partial class ExperienceDetails : ContentPage
	{
		public ExperienceDetails (Jobseekers_DATA Sdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Experience Details";
            BindingContext = new Jobseeker.ViewModels.ExperienceDetailsVM(Sdata);
            
            frommonth.TextColor = Color.FromHex("#212121");
            fromyear.TextColor = Color.FromHex("#212121");
            tomonth.TextColor = Color.FromHex("#212121");
            toyear.TextColor = Color.FromHex("#212121");
        }
	}
}