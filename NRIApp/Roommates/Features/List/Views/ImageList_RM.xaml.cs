using NRIApp.Helpers;
using NRIApp.Roommates.Features.List.Interface;
using NRIApp.Roommates.Features.List.Models;
using NRIApp.Roommates.Features.List.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageList_RM : ContentPage
	{
        //public bool IsLoading { get; set; }
        IUserDialogs dialogs = UserDialogs.Instance;
        public ImageList_RM(string Adid)
		{
			InitializeComponent ();
            ImgDisplay(Adid);
		}
        public async void ImgDisplay(string adid)
        {
            try
            {
                dialogs.ShowLoading("", null);
                var Imagelist = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                Models.ListOfImages response = await Imagelist.GetListofImages(adid);
                listofphoto.ItemsSource = response.ROW_DATA;
                await Task.Delay(2000);
                dialogs.HideLoading();
            }
            catch(Exception exx)
            {

            }
        }
        public void CloseImgelst(object sender,EventArgs e)
        {
                var currentpage = GetCurrentPage();
                currentpage.Navigation.PopModalAsync();
        }
        private Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
        //private void Img_Property(object sender,System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if(e.PropertyName.Equals("IsLoading"))
        //    {
        //        loader.IsRunning= listofphoto
        //    }
        //}

    }
}