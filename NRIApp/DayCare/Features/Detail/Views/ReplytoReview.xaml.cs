using NRIApp.LocalJobs.Features.Detail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReplytoReview : ContentPage
	{
		public ReplytoReview (Rating_Details reviewdtl)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new Features.Detail.ViewModels.ReplytoReview_VM(reviewdtl);
        }
	}
}