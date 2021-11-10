using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Events.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Event_Report : ContentPage
	{
		public Event_Report (string eventid,string title,string eventlink)
		{
			InitializeComponent ();
            BindingContext = new NRIApp.Events.Features.Detail.ViewModel.Detail_VM(eventid, title, eventlink);
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