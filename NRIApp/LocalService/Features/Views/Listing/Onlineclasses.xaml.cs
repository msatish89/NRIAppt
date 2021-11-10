using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalService.Features.ViewModels;
namespace NRIApp.LocalService.Features.Views.Listing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Onlineclasses : ContentPage
    {
        public Onlineclasses()
        {
            InitializeComponent();
            this.BindingContext = new LS_Onlineclasses_VM("");
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           // Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Online_Detail());
        }
    }
}