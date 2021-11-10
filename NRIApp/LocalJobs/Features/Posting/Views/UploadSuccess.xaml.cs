using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UploadSuccess : ContentPage
	{
        public string jobroletagID = "";
        public string jobroletxt = "";
        public UploadSuccess (string jobroletagid,string SelJobrole)
		{
			InitializeComponent ();
            jobroletagID = jobroletagid;
            jobroletxt = SelJobrole;
		}

        private void close_Tap(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private  void Jobprofile_Tap(object sender, EventArgs e)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                NRIApp.USHome.Views.HomePage.seekerlogincode = 1;
            }
            else
            {
                NRIApp.USHome.Views.HomePage.seekerlogincode = 0;
                Jobseeker.Models.Jobseekers_DATA data = new Jobseeker.Models.Jobseekers_DATA();
                Navigation.PushAsync(new Features.Jobseeker.View.Jobseekerprofile(data));
            }
        }
        protected override void OnAppearing()
        {
           try
            {
                if (NRIApp.USHome.Views.HomePage.seekerlogincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    Jobseeker.Models.Jobseekers_DATA data = new Jobseeker.Models.Jobseekers_DATA();
                    Navigation.PushAsync(new Features.Jobseeker.View.Jobseekerprofile(data));
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void Joblisting_Tap(object sender, EventArgs e)
        {
            Listing.Models.LocalJobSenddata listdatas = new Listing.Models.LocalJobSenddata();
            listdatas.jobtype = "-1";
            if(!string.IsNullOrEmpty(jobroletagID))
            {
                listdatas.roleid = jobroletagID;
                listdatas.roletxt = jobroletxt;
            }
            Navigation.PushAsync(new LocalJobs.Features.Listing.Views.JobList(listdatas));
        }
    }
}