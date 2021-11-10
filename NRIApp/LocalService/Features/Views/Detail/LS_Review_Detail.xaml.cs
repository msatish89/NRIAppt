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
	public partial class LS_Review_Detail : ContentPage
	{
		public LS_Review_Detail (string reviewid,string adid)
		{
			InitializeComponent ();
            BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_Detail_VM(reviewid,adid);
		}
	}
}