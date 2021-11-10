using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.List.Interface;
using NRIApp.Rentals.Features.List.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageList_RT : ContentPage
	{
        IUserDialogs userDialogs = UserDialogs.Instance;
		public ImageList_RT (string adid)
		{ 
			InitializeComponent ();
            ImgDisplay(adid);
		}
        public async void ImgDisplay(string adid)
        {
            try
            {
                userDialogs.ShowLoading("", null);
                var Imagelist = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                Models.ListOfRTImages response = await Imagelist.GetRTListofImages(adid);
                listofphoto.ItemsSource = response.ROW_DATA;
                await Task.Delay(2000);
                userDialogs.HideLoading();
            }
            catch(Exception e)
            {

            }
        }
        public void CloseImgelst(object sender,EventArgs e)
        {
            var currentpage = GetCurrentPage();
            Navigation.PopModalAsync();
        }
        private Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}