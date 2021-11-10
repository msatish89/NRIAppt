using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using NRIApp.Helpers;

namespace NRIApp.LocalJobs.Features.Detail.ViewModels
{
    public class ResponsetqVM:INotifyPropertyChanged
    {
        public Command jobprofile { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ResponsetqVM()
        {
            jobprofile = new Command(gotojobprofile);
        }
        public void gotojobprofile()
        {
            Jobseeker.Models.Jobseekers_DATA data = new Jobseeker.Models.Jobseekers_DATA();
            var currentpage = Getcurrentpage();
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                NRIApp.USHome.Views.HomePage.seekerlogincode = 1;
            }
            else
            {
                NRIApp.USHome.Views.HomePage.seekerlogincode = 0;
                currentpage.Navigation.PushAsync(new LocalJobs.Features.Jobseeker.View.Jobseekerprofile(data));
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page Getcurrentpage()
        {
            var currentpage = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
