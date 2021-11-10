using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Events.Features.Detail.ViewModel;
using NRIApp.Events.Features.Detail.Model;
using NRIApp.Controls;

namespace NRIApp.Events.Features.Detail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventPayment : ContentPage
    {
        public EventPayment(string checkoutid, List<Ticketing_Data> td,string totalamount,List<Ticket_Calculation_Data> tcd,string eventtype,string postedtype,string orderform,Sendpaymentchoosedetails sdf,string Sticketingid,string adtitle,string tablerowsseats,string tablerows,string eventdate)
        {
            InitializeComponent();
            Title = "Contact details";
            BindingContext = new ViewModel.Event_Ticketing_VM(checkoutid,td, totalamount,tcd,eventtype,postedtype,orderform,sdf, Sticketingid, adtitle,tablerowsseats,tablerows, eventdate);
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
        private void changehowtoknowpicker(object sender, EventArgs e)
        {
            var category = sender as CustomPicker;

            if (category.SelectedItem.ToString() == "Others")
            {
                stackothers.IsVisible = true;
            }
            else
            {
                stackothers.IsVisible = false;
            }
        }
    }
}