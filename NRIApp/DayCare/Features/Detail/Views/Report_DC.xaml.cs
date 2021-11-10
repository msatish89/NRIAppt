using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Report_DC : ContentPage
	{
		public Report_DC (string businessid,string adurl,string title)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.Report_DC_VM(businessid, adurl,title);
		}
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var vm = BindingContext as ViewModels.Report_DC_VM;
            vm?.taponreportentry();
        }
    }
}