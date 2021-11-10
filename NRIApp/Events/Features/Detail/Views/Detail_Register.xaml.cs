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
	public partial class Detail_Register : ContentPage
	{
		public Detail_Register (string eventid,string ticketingid,string tickettypeid)
		{
			InitializeComponent ();
            Title = "Register Event";
            BindingContext = new ViewModel.Detail_ticket_VM(eventid,ticketingid, tickettypeid);
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
    }
}