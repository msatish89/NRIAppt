using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class offerwant : ContentPage
	{
		public offerwant (Roommates.Features.Post.Models.CategoryList catlist)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new ViewModels.VMCategory(catlist,catlist.ismyneed);
            Title = "What is your need?";
		}
	}
}