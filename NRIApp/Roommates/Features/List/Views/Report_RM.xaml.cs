using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.List.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Report_RM : ContentPage
	{
		public Report_RM (string passcontentid)
		{
            int type=0; int type1=0;
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VM_Detail(passcontentid,type,type1);
		}
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var vm = BindingContext as VM_Detail;
            vm?.taponreportentry();
        }
    }
}