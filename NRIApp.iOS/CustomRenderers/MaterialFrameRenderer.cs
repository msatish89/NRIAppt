using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using NRIApp.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static NRIApp.USHome.Views.HomePage;

[assembly: ExportRenderer(typeof(Frame), typeof(MaterialFrameRenderer))]
namespace NRIApp.iOS.CustomRenderers
{
    public class MaterialFrameRenderer : FrameRenderer
    {
        Frame element;
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            if (e == null) return;
            base.OnElementChanged(e);
            element = Element as Frame;
            if (element != null && element.HasShadow)
            {
               
                // Update shadow to match better material design standards of elevation
                Layer.ShadowRadius = 2.0f;
                Layer.ShadowColor = UIColor.LightGray.CGColor;
                Layer.ShadowOffset = new CGSize(2, 2);
                Layer.ShadowOpacity = 0.80f;
                Layer.MasksToBounds = false;
            }
        }
    }
}
