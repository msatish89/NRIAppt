using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Primarytags : ContentPage
	{
        public static string _Oldclpostid="";
		public LS_Primarytags (int id=0,string oldclpostid="")
		{
           
            InitializeComponent();
            Title = "Services";
                if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");

            _Oldclpostid = oldclpostid;
        BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_ViewModel(oldclpostid,"");
            ViewModels.LS_ViewModel.Selectedservicetype = id;
            var currentpage = NRIApp.LocalService.Features.ViewModels.LS_ViewModel.GetCurrentPage();
        }

    }
}