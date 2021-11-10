using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalService.Features.ViewModels;

namespace NRIApp.LocalService.Features.Views.Detail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Online_Detail : ContentPage
    {
		public LS_Online_Detail (string contentid)
		{
			InitializeComponent ();
            this.BindingContext = new ViewModels.LS_OnlineDetail_VM(contentid);
		}
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try { 
            Navigation.PopAsync();
            }
            catch (Exception ee)
            {

            }
        }
    }
}