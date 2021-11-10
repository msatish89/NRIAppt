using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.LeadForm.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Thankyou : ContentPage
	{
		public Thankyou (string respid)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.ThankyouViewModel(respid);
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            //Task.Run(async () =>
            //{
            //    await Navigation.PopToRootAsync();
            //});
            return true;
        }

    }
   
}