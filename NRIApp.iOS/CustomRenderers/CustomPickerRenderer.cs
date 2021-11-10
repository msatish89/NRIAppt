using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using NRIApp.iOS.CustomRenderers;
using CoreGraphics;
using NRIApp.Controls;
using System.Drawing;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace NRIApp.iOS.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            var element = (CustomPicker)this.Element;
            if (Control != null)
            {
                //var downarrow = UIImage.FromBundle(element.Image);
                //Control.RightViewMode = UITextFieldViewMode.Always;
                //Control.RightView = new UIImageView(downarrow);
                var _picker = this.Control;
                _picker.RightViewMode = UIKit.UITextFieldViewMode.Always;
                _picker.RightView = new UIImageView(UIImage.FromBundle("ic_arrow_drop_down.png"));
            }
        }
    }
}