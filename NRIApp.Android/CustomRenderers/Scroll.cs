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
using NRIApp.Droid;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using NRIApp.Helpers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ScrollView), typeof(NRIApp.Droid.CustomRenderers.Scroll))]
namespace NRIApp.Droid.CustomRenderers
{
#pragma warning disable CS0618
    public class Scroll : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
            {
                return;
            }

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropetyChanged;
            }

            e.NewElement.PropertyChanged += OnElementPropetyChanged;

        }

        private void OnElementPropetyChanged(object sender, PropertyChangedEventArgs e)
        {
            VerticalScrollBarEnabled = false;
           // HorizontalScrollBarEnabled = false;
            if (ChildCount > 0)
            {
                GetChildAt(0).HorizontalScrollBarEnabled = false;
             //   GetChildAt(0).VerticalScrollBarEnabled = false;
            }
        }
    }
#pragma warning restore CS0618
}