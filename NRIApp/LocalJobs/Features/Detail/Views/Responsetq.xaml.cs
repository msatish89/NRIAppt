using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using NRIApp.Helpers;

namespace NRIApp.LocalJobs.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Responsetq : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;
        public static int dtlpagecode=0;
		public Responsetq ()
		{
			InitializeComponent ();
          //  dtlpagecode = 0;
            BindingContext = new LocalJobs.Features.Detail.ViewModels.ResponsetqVM();
		}

        protected override void OnAppearing()
        {
          try
            {
                //base.OnAppearing();
                if (NRIApp.USHome.Views.HomePage.seekerlogincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    LocalJobs.Features.Jobseeker.Models.Jobseekers_DATA data = new LocalJobs.Features.Jobseeker.Models.Jobseekers_DATA();
                    Navigation.PushAsync(new LocalJobs.Features.Jobseeker.View.Jobseekerprofile(data));
                }
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        private void PostClose(object sender, EventArgs e)
        {
           try
            {
                dialogs.ShowLoading("", null);
                if(dtlpagecode==1 && NRIApp.LocalJobs.Features.Posting.Views.JobSuccess.viewadcode == 0)
                {
                    Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(page1);
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }
                else
                {
                   // Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 0];
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    //Navigation.RemovePage(page1);
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }

                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            //Navigation.PopAsync();
            //Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            //Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            //Navigation.RemovePage(page1);
            //Navigation.RemovePage(page2);
            try
            {
                dialogs.ShowLoading("", null);
                if (dtlpagecode == 1&&dtlpagecode == 1 && NRIApp.LocalJobs.Features.Posting.Views.JobSuccess.viewadcode == 0)
                {
                    Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(page1);
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }
                else
                {
                   // Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 0];
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    //Navigation.RemovePage(page1);
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }

                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
            // return base.OnBackButtonPressed();
            return true;
        }
    }
}