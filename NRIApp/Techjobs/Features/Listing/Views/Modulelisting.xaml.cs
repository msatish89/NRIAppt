using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Techjobs.Features.Listing.ViewModels;
using Xamarin.Forms.Internals;

namespace NRIApp.Techjobs.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class Modulelisting : ContentPage
	{
        //private int _lastItemAppearedIdx;
        public Modulelisting (string id, string name, string mode, string facilities,string page,string sort)
		{
			InitializeComponent ();
            Title = name + " Training";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            this.BindingContext = new ModulelistVM(id,mode,facilities,page,sort);
            

        }
        //private void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        //{
        //    var data = ModulelistVM.modlistings;
        //    var currentIdx = data.IndexOf(e.Item);

        //    if (currentIdx > _lastItemAppearedIdx)
        //        bottomnav.IsVisible = false;
        //    else
        //        bottomnav.IsVisible = true;

        //    _lastItemAppearedIdx = data.IndexOf(e.Item);

        //}
    }
}