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
	public partial class JobSuccess : ContentPage
	{
        public static int viewadcode = 1;
		public JobSuccess (string adid, string usertype)
		{
			InitializeComponent ();
            this.BindingContext = new ViewModels.Jobsuccess_VM(adid,usertype);
		}

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }
       
    }
}