using NRIApp.Rentals.Features.List.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Report_RT : ContentPage
	{
		public Report_RT (string adid)
		{
			InitializeComponent ();
            int type = 0;int type1 = 1;
            BindingContext = new VMRT_Detail(adid,type,type1);
        }
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var vm = BindingContext as VMRT_Detail;
            vm?.taponreportentry();
        }
    }
}