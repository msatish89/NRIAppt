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
	public partial class Catsecond : ContentPage
	{
		public Catsecond (string Categorylevel, Models.DC_CategoryList dctypes)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
			if (dctypes.ismyneed == "1")
			{
				dctypes.isprefill = "0";
			}
			this.BindingContext=new NRIApp.DayCare.Features.Post.ViewModels.VMCategory(Categorylevel,  dctypes, 2);
            Title = "Care Type";
        }
	}
}