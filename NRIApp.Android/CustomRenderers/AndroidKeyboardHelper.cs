using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NRIApp.Helpers;
using NRIApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Android.Views.InputMethods;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidKeyboardHelper))]
namespace NRIApp.Droid.CustomRenderers
{
   public class AndroidKeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            var context = Android.App.Application.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }

        public void Gotosettings()
        {

            Android.App.Application.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocationSourceSettings));
        }
    }
}