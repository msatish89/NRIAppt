using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Events.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Register_Thankyou : ContentPage
	{
		public Register_Thankyou (string guid,string ordernumber,string email)
		{
			InitializeComponent();
            lbltext.Text = "You have successfully registered for the event! Live streaming details has been sent to your mail id "+ email;
            lblorderid.Text = ordernumber;
		}
        private void Postsuccessclose(object sender, EventArgs e)
        {

            Navigation.PopToRootAsync();
           
        }
    }
}