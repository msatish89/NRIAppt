
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.USHome.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Homemaster : MasterDetailPage
    {
        public Homemaster ()
		{
            InitializeComponent ();
          
            this.Master = new HomeMenuPage();
            this.Detail = new NavigationPage(new HomeScreenPage())
            {
                BarBackgroundColor = Color.FromHex("#e30045"),
                BarTextColor = Color.White     
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsPresented = false;
        }

    }
}