using NRIApp.Signin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Signin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Forgotpassword : ContentPage
	{
		public Forgotpassword ()
		{
			InitializeComponent ();
            this.BindingContext = new Signinviewmodel();
        }
	}
}