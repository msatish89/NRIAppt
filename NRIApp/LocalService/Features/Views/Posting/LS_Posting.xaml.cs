using NRIApp.Helpers;
using NRIApp.LocalService.Features.Models;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Posting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_Posting : ContentPage
    {
        public LS_Posting(string stagid, string stag, string ptag, string ptagid, string checkvalue, string checktext,string oldclpostid="")
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            InitializeComponent();
            Title = "Enter Business Details";
            ldprimarytag.Text = ptag;
            BindingContext = new ViewModels.LS_Posting_VM(stagid, stag, ptag, ptagid, checkvalue, checktext, oldclpostid);




        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }

        private void Uexperince_Focused(object sender, FocusEventArgs e)
        {
            popupexperience.IsVisible = true;
        }
        private void ContentView_Tapped(object sender, EventArgs e)
        {
            popupexperience.IsVisible = false;
        }
        private void Frame_tapped(object sender, EventArgs e)
        {
            popupexperience.IsVisible = true;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            popupexperience.IsVisible = false;
        }
    }
}