using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Detail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Report : ContentPage
	{
		public LS_Report (string adid,string adtitle,string addlink)
		{
			InitializeComponent ();
            BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_Detail_VM(adid,adtitle, addlink);     
		}
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            if (Reportdata.IsVisible)
            {
                Reportdata.IsVisible = false;

            }
            else
            {
                Reportdata.IsVisible = true;
            }
           
        }
    }
}