using NRIApp.DayCare.Features.Post.Models;
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
	public partial class PostFirst : ContentPage
	{
		public PostFirst (Daycareposting post)
		{
			InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            if(post.ismyneed=="1")
            {
                //paidexperienceyrtxt.Text = post.Experience;
                //establishyeartxtID.Text = post.Experience;
                establishyeartxtID.TextColor= Color.FromHex("#212121");
                //paidexperienceyrtxt.Text = post.Experience;
                paidexperienceyrtxt.TextColor = Color.FromHex("#212121");
                //replyperiodtxt.Text = post.responsetime;
                replyperiodtxt.TextColor = Color.FromHex("#212121");
            }
            BindingContext = new ViewModels.Postfirst_VM(post);
            // Title = "Care Expectations";
            if(post.supercategoryid=="1")
            {
                Title = "Care Experience";
            }
            else
            {
                Title = "Care Expectations";
            }
        }
	}
}