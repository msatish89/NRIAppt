using NRIApp.DayCare.Features.List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewNumberForm : ContentPage
	{
		public ViewNumberForm (DClistings data, string pageid)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "View Phone Number";
            BindingContext = new ViewModels.ResponseForm_VM(data, pageid);
        }

        private void gototc(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
        private void PostClose(object sender, EventArgs e)
        {
            try
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {

            }
        }
        protected override bool OnBackButtonPressed()
        {
            try
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }
}