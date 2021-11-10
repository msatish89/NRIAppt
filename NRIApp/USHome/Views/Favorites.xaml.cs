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
	public partial class Favorites : ContentPage
	{
		public Favorites ()
		{
            Title = "Favorites";
			InitializeComponent ();
            BindingContext = new ViewModels.Favorite();
		}
	}
}