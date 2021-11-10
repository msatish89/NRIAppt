using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using NRIApp.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListviewScrollbar), typeof(NRIApp.iOS.CustomRenderers.ListviewScroll))]
namespace NRIApp.iOS.CustomRenderers
{
    public class ListviewScroll : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.ShowsVerticalScrollIndicator = false;
        }
    }
}