using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Roommates.Features.List.ViewModels;
using NRIApp.Roommates.Features.Post.Models;

namespace NRIApp.Roommates.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Category : ContentPage
	{
        public Category(Response catgoryurl, string cityurl)
		{
			InitializeComponent();

            //Sortby_RM.orderby = "";
            //catgoryurl.orderby = "date";
            //Filter_RM.priceto = "";

            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
             BindingContext = new VMRoommates(catgoryurl, cityurl);
             Title = "Roommates";
        }

        private void Gotohome(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        //private void RoommatesListview_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        //{
        //    var currentIdx = Items.IndexOf((string)e.Item);

        //    if (currentIdx > _lastItemAppearedIdx)
        //        Direction.Text = "Up";
        //    else
        //        Direction.Text = "Down";

        //    _lastItemAppearedIdx = Items.IndexOf((string)e.Item);

        //}
    }
}