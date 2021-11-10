using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Techjobs.Features.Listing.Models;
using NRIApp.Techjobs.Features.Listing.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Sort : ContentPage
	{
        string id = "", mode = "", facilityids="";
       // static string facilityids = "";
        public Sort (string moduleid, string trmode, string facility,string orderby)
		{
			InitializeComponent ();
            id = moduleid;
            mode = trmode;
            facilityids = facility;
            if (orderby == "")
                orderby = "featured";
            this.BindingContext = new FiltersVM(orderby);
        }
        private async void Backbtn()
        {
            await Navigation.PopModalAsync();

        }
        private async void Applysorting()
        {
            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new NRIApp.Techjobs.Features.Listing.Views.Modulelisting(id, "", mode, facilityids, "filter",FiltersVM.orderbyval));
        }
    }
}