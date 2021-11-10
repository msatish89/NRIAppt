using NRIApp.LocalJobs.Features.Jobseeker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using Refit;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;
using NRIApp.Helpers;
using System.Text.RegularExpressions;

namespace NRIApp.LocalJobs.Features.Jobseeker.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResumeTitle : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;
        Jobseekers_DATA data = new Jobseekers_DATA();
		public ResumeTitle (Jobseekers_DATA Sdata)
		{
            data = Sdata;
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Resume Title";
            resumetitle.Text = Sdata.resumetitle;
        }
        bool validation()
        {
            bool result = true;
            //string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            //string titlepattern = "^[0-9A-Za-z ]+$";
            string titlepattern = "^[A-Za-z ]+$";
            if (string.IsNullOrEmpty(resumetitle.Text) || resumetitle.Text.Trim().Length==0)
            {
                dialogs.Toast("Enter an apt title for your job profile");
                return false;
            }
            //if (!Regex.IsMatch(resumetitle.Text, specialChar))
            //{
            //    dialogs.Toast("Title should not contain any special characters...");
            //    return false;
            //}
            if (!Regex.IsMatch(resumetitle.Text, titlepattern))
            {
                dialogs.Toast("Title should not contain any special characters or integers...");
                return false;
            }
            return result;
        }
        private async void Add_resume_title(object sender, EventArgs e)
        {
            try
            {
                dialogs.ShowLoading("", null);
                if(validation())
                {

                    data.resumetitle = resumetitle.Text;
                    data.block = "title";
                    var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var profiledatas = await profileupdata.profilepagecreate(data);
                    if (profiledatas != null)
                    {
                        //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                        //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                        Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                        Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(page1);
                        Navigation.RemovePage(page2);
                        await Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                    }
                    else
                    {
                        dialogs.Toast("Try again later...");
                    }

                }


                dialogs.HideLoading();
            }
            catch(Exception ex)
            {

            }
        }

        private void Cancel_title(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}