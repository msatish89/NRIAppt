using NRIApp.LocalJobs.Features.Posting.Models;
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
    public partial class Jobtype : ContentPage
    {
        Postingdata jobdata = new Postingdata();
        public Jobtype(Postingdata data)
        {
            InitializeComponent();
            jobdata = data;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            MyneedsNxtVisible.IsVisible = false;
            if (jobdata.ismyneed == "1")
            {
                MyneedsNxtVisible.IsVisible = true;
                if (jobdata.Primarycategoryid == 3)
                {
                    ittext.TextColor = Color.FromHex("#fbaa19");
                }
                else if (jobdata.Primarycategoryid == 4)
                {
                    nonittext.TextColor = Color.FromHex("#fbaa19");
                }
            }
            Title = "Job Type";
        }

        public void ittapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Functionalarea(3, jobdata));
        }
        public void nonittapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Functionalarea(4, jobdata));
        }

        private void myneed_tap(object sender, EventArgs e)
        {
            if (jobdata.Primarycategoryid == 3)
            {
                Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Functionalarea(3, jobdata));
            }
            else if (jobdata.Primarycategoryid == 4)
            {
                Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Functionalarea(4, jobdata));
            }
        }
    }
}