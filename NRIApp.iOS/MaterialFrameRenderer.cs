using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using NRIApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NRIApp.USHome.Views.HomePage), typeof(MaterialFrameRenderer))]
namespace NRIApp.iOS
{
    public class MaterialFrameRenderer : FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            Layer.ShadowRadius = 2.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }
    
        //protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        //{
        //    base.OnElementChanged(e);
        //    base.OnElementChanged(e);
        //    // Update shadow to match better material design standards of elevation
        //    Layer.ShadowRadius = 2.0f;
        //    Layer.ShadowColor = UIColor.LightGray.CGColor;
        //    Layer.ShadowOffset = new CGSize(2, 2);
        //    Layer.ShadowOpacity = 0.80f;
        //    Layer.MasksToBounds = false;
        //}
    }
}