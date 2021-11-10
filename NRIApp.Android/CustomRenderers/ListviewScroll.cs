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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListviewScrollbar), typeof(NRIApp.Droid.CustomRenderers.ListviewScroll))]
namespace NRIApp.Droid.CustomRenderers
{
#pragma warning disable CS0618
    public class ListviewScroll: ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.VerticalScrollBarEnabled = false;
            

        }

    }
#pragma warning restore CS0618
}