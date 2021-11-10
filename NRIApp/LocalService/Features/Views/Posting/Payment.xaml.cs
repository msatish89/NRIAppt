using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Stripe;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Posting
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Payment : ContentPage
	{
		public Payment ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            //CreateToken("4000000000003089", 02, 2020, "123");
        }

        //public void CreateToken(string cardNumber, int cardExpMonth, int cardExpYear, string cardCVC)
        //{
        //    StripeConfiguration.SetApiKey("pk_test_FyPZYPyqf8jU6IdG2DONgudS");


        //    try
        //    {
        //        var tokenOptions = new Stripe.TokenCreateOptions()
        //        {
        //            Card = new Stripe.CreditCardOptions()
        //            {
        //                Number = cardNumber,
        //                ExpMonth = cardExpMonth,
        //                ExpYear = cardExpYear,
        //                Cvc = cardCVC
        //            }
        //        };

        //        var tokenService = new Stripe.TokenService();
        //        Stripe.Token stripeToken = tokenService.Create(tokenOptions);

        //        if (stripeToken.Id != "" || stripeToken.Id != null)
        //            CCStripePayment(stripeToken.Id, 50);
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message.ToString();
        //    }


        //}

        //public string CCStripePayment(string token, int totalamount)
        //{
        //    string sResult = "", Stripe_TransId = "";
        //    try
        //    {
        //        StripeConfiguration.SetApiKey("sk_test_4eC39HqLyjWDarjtT1zdp7dc");
        //        var options = new Stripe.ChargeCreateOptions
        //        {
        //            Amount = totalamount * 100, // *100 (for cent to dollar)
        //            Currency = "USD",
        //            Description = "SulekhaStripePayment",
        //            SourceId = token,
        //        };
        //        var service = new Stripe.ChargeService();
        //        Stripe.Charge charge = service.Create(options);
        //        sResult = charge.Status;
        //        if (sResult == "succeeded")
        //        {
        //            Stripe_TransId = charge.BalanceTransactionId;
        //        }
        //    }
        //    catch (Exception exx)
        //    {
        //        sResult = exx.Message.ToString();
        //    }
        //    return sResult;
        //}
    }
}