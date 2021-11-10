using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Techjobs.Features.Home.ViewModels;

namespace NRIApp.Techjobs.Features.Home.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Modules : ContentPage
	{
		public Modules (string id, string name)
		{
			InitializeComponent ();
            Title = name + " Training";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            //lblmod.Text = name;
            // ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Red;
            BindingContext = new ViewModels.homeviewmodel(id, "module");
         
        }

    }
}