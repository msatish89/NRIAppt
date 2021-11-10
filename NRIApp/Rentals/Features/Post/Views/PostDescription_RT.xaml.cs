using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostDescription_RT : ContentPage
	{
		public PostDescription_RT (RTPostFirst obj, int adid, int agentid, string ordertype)
		{
			InitializeComponent ();
            ordertype = "postdesc";
            Title = "Room Description";
            BindingContext = new VMRTPost(obj, adid, agentid, ordertype,"");
        }
        protected override void OnAppearing()
        {
            lcfsecond.postsuccessbackbtn = 0;
        }
    }
}