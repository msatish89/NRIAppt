using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Roommates.Features.Post.ViewModels;

namespace NRIApp.Roommates.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Catfirst : ContentPage
	{
		public Catfirst (int stagid, string stype, Roommates.Features.Post.Models.CategoryList needscatlist)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            
            BindingContext = new VMCategory(stagid, stype,needscatlist);
            Title = "Property Type";
        }
	}
}