using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace NRIApp.DayCare.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Imagedetail : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;
        public Imagedetail (string photourl)
		{
			InitializeComponent ();
            ImgDisplay(photourl);
        }
        public List<string> photourllist { get; set; }
        public async void ImgDisplay(string photourl)
        {
            try
            {
                dialogs.ShowLoading("", null);
                //var Imagelist = RestService.For<ILisCategory>(Commonsettings.RoommatesAPI);
                //Models.ListOfImages response = await Imagelist.GetListofImages(adid);
                photourllist = new List<string>();
                string ldata = photourl;
                string[] photourlarr = ldata.Split(',');
                foreach (var photolistdta in photourlarr)
                {
                    if (!string.IsNullOrEmpty(photolistdta))
                    {
                        string photourlata = photolistdta;
                        photourllist.Add(photourlata);
                    }
                }
                listofphoto.ItemsSource = photourllist;
                await Task.Delay(2000);
                dialogs.HideLoading();
            }
            catch (Exception exx)
            {

            }
        }
        public void CloseImgelst(object sender, EventArgs e)
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PopModalAsync();
        }
        private Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}