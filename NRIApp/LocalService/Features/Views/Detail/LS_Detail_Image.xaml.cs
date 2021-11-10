using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Detail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Detail_Image : ContentPage
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public LS_Detail_Image (string adid)
		{
			InitializeComponent ();
            ImgDisplay(adid);

        }

        public async void ImgDisplay(string adid)
        {
            _Dialog.ShowLoading("");
            var adimages = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
            var data = await adimages.GetPhotos(adid);
            listofphoto.ItemsSource = data.ROW_DATA;
            _Dialog.HideLoading();
        }
        private async void CloseImgBlock(object sender, EventArgs e)
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            try { 
            await currentpage.Navigation.PopModalAsync();
            }
            catch (Exception ee)
            {

            }
        }
    }
}