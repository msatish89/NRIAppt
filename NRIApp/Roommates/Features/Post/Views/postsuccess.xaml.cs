using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class postsuccess : ContentPage
	{
		public postsuccess (string adid,string usertype,string packagetype)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.PostSuccess_VM(adid,usertype,packagetype);
		}
        protected override bool OnBackButtonPressed()
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PopToRootAsync();
            return true;
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}