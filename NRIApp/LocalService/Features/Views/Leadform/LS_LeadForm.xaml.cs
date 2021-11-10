using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalService.Features.Models;
using Plugin.Connectivity;

namespace NRIApp.LocalService.Features.Views.Leadform
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_LeadForm : ContentPage
    {
        public LS_LeadForm(int supertagid, string supertag, string ptag, string ptagid, string checkvalue, string checktext)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            InitializeComponent();
            Title = "Enter Contact Details";
            try
            {
                // lblformtitle.Text = ptag;
                if (Device.RuntimePlatform == Device.iOS)
                    NavigationPage.SetBackButtonTitle(this, "Back");

                BindingContext = new ViewModels.LS_Leadform_VM(supertagid, supertag, ptagid, ptag, checkvalue, checktext);

                //leadcountrycode.SelectedIndex = 0;
            }
            catch (Exception e)
            {

            }
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }

    }
}