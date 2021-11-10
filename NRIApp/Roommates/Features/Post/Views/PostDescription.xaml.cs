using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.ViewModels;
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
	public partial class PostDescription : ContentPage
	{
        public static int pagetype = 0;
        public PostDescription (NRIApp.Roommates.Features.Post.Models.Postfirst obj,int adid,int agentid,string ordertype)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            ordertype = "postdesc";
            Title = "Room Description";
            BindingContext = new VMPost(obj, adid, agentid, ordertype,"");
            // BindingContext = new ViewModels.VMPost(category,adid,agentid,ordertype,postdesc);
        }
        protected override void OnAppearing()
        {
            lcfsecond.postscuccesbackbtn = 0;
        }
    }
}