using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Thankyou_DC : ContentPage
	{
		public Thankyou_DC ()
		{
			InitializeComponent ();
		}

        private void PostClose(object sender, EventArgs e)
        {
           try
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Navigation.PopAsync();
            }
            catch(Exception ex)
            {

            }
        }
        protected override bool OnBackButtonPressed()
        {
            //var currentpage = GetCurrentPage();
            //if (NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid == 0)
            //{
            //    currentpage.Navigation.PopAsync();
            //}
            //else if (NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid == 1)
            //{
            //    currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            //}
          try
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Navigation.PopAsync();

              
            }
            catch(Exception ex)
            {

            }
            return true;
        }
        public Page GetCurrentPage()
        {
            var currentpg = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpg;
        }
    }
}