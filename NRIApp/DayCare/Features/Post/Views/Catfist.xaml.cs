using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.DayCare.Features.Post.ViewModels;

namespace NRIApp.DayCare.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Catfist : ContentPage
	{
		//NRIApp.DayCare.Features.Post.Models.DC_CategoryList dccatlist = new DayCare.Features.Post.Models.DC_CategoryList();
		public Catfist (Models.DC_CategoryList dccatlist)
		{
            
			InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
			if(dccatlist.ismyneed=="1")
            {
				dccatlist.isprefill = "1";
            }
            BindingContext = new VMCategory(dccatlist);
            Title = "Select your need";
        }
	}
}