using Acr.UserDialogs;
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
    public partial class Seekerresponsetq : ContentPage
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public static int resumedtlpagecode = 0;
        public Seekerresponsetq()
        {
            InitializeComponent();
        }

        Posting.Models.Postingdata jdata = new Posting.Models.Postingdata();
        private void gotopostpagecmd(object sender, EventArgs e)
        {
             try
                {
                     Navigation.PushAsync(new LocalJobs.Features.Posting.Views.Jobtype(jdata));
                }
                catch (Exception ex)
                {

                }
        }

        private void gotojobresume(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
        }

        private void PostClose(object sender, EventArgs e)
        {
            try
            {
                dialogs.ShowLoading("", null);
                if (resumedtlpagecode == 1)// && NRIApp.LocalJobs.Features.Posting.Views.JobSuccess.viewadcode == 0
                {
                    Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(page1);
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }
                else
                {
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }

                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }

        protected override bool OnBackButtonPressed()
        {
           
            try
            {
                dialogs.ShowLoading("", null);
                if (resumedtlpagecode == 1)//&& resumedtlpagecode == 1 && NRIApp.LocalJobs.Features.Posting.Views.JobSuccess.viewadcode == 0
                {
                    Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(page1);
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }
                else
                {
                    Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Navigation.RemovePage(page2);
                    Navigation.PopAsync();
                }

                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
            return true;
        }
    }
}