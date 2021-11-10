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
	public partial class SkillDetail : ContentPage
	{
		public SkillDetail (Jobseekers_DATA Sdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Skill Details";
            BindingContext = new Jobseeker.ViewModels.SkillDetailVM(Sdata);
            if(!string.IsNullOrEmpty(Sdata.skillyearsofexperience))
            {
                experienceyrtxt.TextColor = Color.FromHex("#212121");
            }
        }
	}
}