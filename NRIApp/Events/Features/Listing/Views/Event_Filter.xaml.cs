using Acr.UserDialogs;
using NRIApp.Events.Features.Listing.Interface;
using NRIApp.Events.Features.Listing.Model;
using NRIApp.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Events.Features.Listing.ViewModel;

namespace NRIApp.Events.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Event_Filter : ContentPage
	{
        IUserDialogs _Dialog = UserDialogs.Instance;
        public Event_Filter ()
		{
			InitializeComponent ();
            Title = "Filter Listing";
            BindingContext = new NRIApp.Events.Features.Listing.ViewModel.Eevents_VM("filter","");
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
           
            

        }
        private void clickeddatefilter(object sender, EventArgs e)
        {
            var lbl = sender as Frame;
            var k = Eevents_VM.selecteddatevalue= lbl.ClassId;
            lbl.BackgroundColor = Color.Red;
             
            if (k=="1")
            {               
                day2.BackgroundColor= day3.BackgroundColor=  Color.FromHex("#e6e5e5");
                stackcustomcalender.IsVisible = false;
            }
            else if (k == "2")
            {
                day1.BackgroundColor = day3.BackgroundColor =  Color.FromHex("#e6e5e5");
                stackcustomcalender.IsVisible = false;
            }
            else if (k == "3")
            {
                day1.BackgroundColor = day2.BackgroundColor =  Color.FromHex("#e6e5e5");
                stackcustomcalender.IsVisible = true;
            }
            else if (k == "4")
            {
                day1.BackgroundColor = day2.BackgroundColor = day3.BackgroundColor = Color.FromHex("#e6e5e5");
                stackcustomcalender.IsVisible = true;
            }
           
        }
      
       
        public void Clicknerbyevents(object sender, EventArgs e)
        {
            if (Eevents_VM.Nearbyevents == 1)
            {
                nearbyeventimg.Source = "OffButton.png";
                Eevents_VM.Nearbyevents = 0;
            }
            else
            {
                nearbyeventimg.Source = "OnButton.png";
                Eevents_VM.Nearbyevents = 1;
            }
          
        }
     
     
        }
}