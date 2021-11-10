using NRIApp.DayCare.Features.Post.Models;
using NRIApp.LocalService.Features.Views;
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
	public partial class WorkType : ContentPage
	{
		public WorkType (string Categorylevel, DC_CategoryList dctypes)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            this.BindingContext=new NRIApp.DayCare.Features.Post.ViewModels.VMCategory(Categorylevel, dctypes, 4);
            Title = "Work Type";
        }
        public void bind()
        {
           // CheckBox chb = new CheckBox();
        }
	}
}