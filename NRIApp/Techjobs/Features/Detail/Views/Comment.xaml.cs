using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Comment : ContentPage
	{
		public Comment (string reviewid, string businessid)
		{
			InitializeComponent ();
            Title = "Add Comment";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            this.BindingContext = new NRIApp.Techjobs.Features.Detail.ViewModels.CommentVM(reviewid, businessid);
		}
	}
}