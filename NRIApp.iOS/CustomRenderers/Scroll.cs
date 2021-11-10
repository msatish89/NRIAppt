using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using NRIApp.Helpers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ScrollView), typeof(NRIApp.iOS.CustomRenderers.Scroll))]
namespace NRIApp.iOS.CustomRenderers
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
                this.ShowsVerticalScrollIndicator = false;
                this.ShowsHorizontalScrollIndicator = false;

            }
        }
#pragma warning restore CS0618
    }