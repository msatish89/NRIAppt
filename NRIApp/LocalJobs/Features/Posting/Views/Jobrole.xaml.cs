using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Jobrole : ContentPage
	{
		public Jobrole (int funid,int jobtype, string functionalarea, Models.Postingdata jobdata)
		{
			InitializeComponent ();
            Title = "Job Role";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            this.BindingContext = new JobroleVM(funid, jobtype,functionalarea,jobdata);
		}
        protected override void OnAppearing()
        {
           try
            {
                base.OnAppearing();
                SkilssVM.Checkedvalues = "";
                SkilssVM.Checkedtext = "";
                SkilssVM.newskilltextvalues = new List<string>();
            }
            catch(Exception ex)
            {

            }
        }
    }
}