using NRIApp.DayCare.Features.Post.Models;
using NRIApp.DayCare.Features.Post.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeadForm : ContentPage
	{
		public LeadForm ()
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            this.BindingContext = new Leadform_VM();
		}
        public void TCtap()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
    }
}