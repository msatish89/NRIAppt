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
	public partial class Catfirstpost : ContentPage
	{
		public Catfirstpost (int stagid,string type,int param,Roommates.Features.Post.Models.CategoryList needscatlist)
		{
            InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new ViewModels.VMCategory(stagid,type,param,needscatlist);
            Title = "Property Type";
        }
	}
}