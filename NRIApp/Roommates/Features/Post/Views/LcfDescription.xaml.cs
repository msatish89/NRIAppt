using NRIApp.Roommates.Features.Post.Models;
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
	public partial class LcfDescription : ContentPage
	{
       
		public LcfDescription (Response res,DateTime Frdate)
		{
            //Response clist, DateTime frdate, string gender, string textrent,string countflag
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Ad Description";
            BindingContext = new ViewModels.VMLCF(res, Frdate,res.gender, res.ExpRent, res.countflag);
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lcfsecond.postscuccesbackbtn = 0;
        }
    }
}