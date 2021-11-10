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
	public partial class ResumeDetail : ContentPage
	{
        public string profileID = "";
        public string emailID = "";
		public ResumeDetail (string profileid,string emailid)
		{
			InitializeComponent ();
            profileID = profileid;
            emailID = emailid;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            NRIApp.LocalJobs.Features.Jobresume.Views.Seekerresponsetq.resumedtlpagecode = 0;
            if (LocalJobs.Features.Jobresume.ViewModels.ResumeDetail_VM.isdownloadresumeprofilecnt != 1)
            {
                BindingContext = new LocalJobs.Features.Jobresume.ViewModels.ResumeDetail_VM(profileid, emailid);
            }
        }

        protected override void OnAppearing()
        {
           if (LocalJobs.Features.Jobresume.ViewModels.ResumeDetail_VM.isdownloadresumeprofilecnt == 1)
            {
                BindingContext = new LocalJobs.Features.Jobresume.ViewModels.ResumeDetail_VM(profileID, emailID);
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}