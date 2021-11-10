using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;
using NRIApp.LocalJobs.Features.Jobseeker.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using System.Text.RegularExpressions;

namespace NRIApp.LocalJobs.Features.Jobseeker.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileDescription : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;
        Jobseekers_DATA data = new Jobseekers_DATA();

        public ProfileDescription (Jobseekers_DATA Sdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            data = Sdata;
            Title = "Profile Description";
            profiledesc.Text = Sdata.profiledesc;

        }
        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z ]+$");
        }
        private async void save_description(object sender, EventArgs e)
        {
           try
            {
                dialogs.ShowLoading("", null);
               // string titlepattern = "^[A-Za-z ]+$";
                if (string.IsNullOrEmpty(profiledesc.Text) || profiledesc.Text.Trim().Length==0)
                {
                    dialogs.Toast("Please enter the description");
                }
                //else if (!Regex.IsMatch(profiledesc.Text, titlepattern))
                //{
                //    dialogs.Toast("Title should not contain any special characters or integers...");
                //}
                else if(profiledesc.Text.Contains("@"))
                {
                    dialogs.Toast("Title should not contain any special characters or integers...");
                }
                else
                {
                    if(CheckAplhaOnly(profiledesc.Text))
                    {
                        data.profiledesc = profiledesc.Text;
                        data.block = "description";
                        var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                        var profiledatas = await profileupdata.profilepagecreate(data);
                        if (profiledatas != null)
                        {
                            Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                            Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                            Navigation.RemovePage(page1);
                            Navigation.RemovePage(page2);
                            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.)
                            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                            await Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                        }
                        else
                        {
                            dialogs.Toast("Try again later...");
                        }
                    }
                    else
                    {
                        dialogs.Toast("Description should have some letters...");
                    }

                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {

            }
        }
        protected override bool OnBackButtonPressed()
        {

            return base.OnBackButtonPressed();
        }
        private void cancel_descrption(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}