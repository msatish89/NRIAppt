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
using NRIApp.LocalService.Features.Interfaces;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace NRIApp.iOS.CustomRenderers
{
   public class CustomEntryRenderer : EntryRenderer
    {
        private EventHandler _editingDidBeginEventHandler;

        CustomEntry element;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (e == null) return;
            base.OnElementChanged(e);
            element = Element as CustomEntry;
            if (element != null && element.HideBorder)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
            if (element.ReturnKeyType == ReturnKeyTypes.Next)
            {
                Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;
                Control.ShouldReturn += (textField) =>
                {
                    element.OnNext();
                    return false;
                };
            }

            if (element != null && Control != null)
            {
                SetReturnKeyType(element);

                
    
                Control.ShouldReturn += (textField) => {
                    element?.InvokeCompleted();
                    if (element.ReturnKeyType != ReturnKeyTypes.Next)
                        element.Unfocus();
                    element.InvokeCompleted();
                    if (element.ReturnKeyType == ReturnKeyTypes.Next)
                        element.OnNext();
                    if (element.ReturnKeyType == ReturnKeyTypes.Go)
                        _editingDidBeginEventHandler = (sender, a) =>
                        {
                            next(sender, a);
                        };
                    return true;
                };

                if (element.ShowToolbar)
                {
                    var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));
                    toolbar.Items = new[]
                    {
                        new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                        new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                        {
                            Control.ShouldReturn += (textField) => {
                            element?.InvokeCompleted();
                             return true;
                            };
                            Control.ResignFirstResponder();
                        })
                    };
                    this.Control.InputAccessoryView = toolbar;
                }
            }
        }
        public async void next(object sender,EventArgs eventArgs)
        {
            await element.Execute(sender, eventArgs);
        }
        void SetReturnKeyType(CustomEntry entry)
        {
            var type = entry.ReturnKeyType;
            switch (type)
            {
                case ReturnKeyTypes.Go:
                    Control.ReturnKeyType = UIKit.UIReturnKeyType.Go;
                    break;
                case ReturnKeyTypes.Next:
                    Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;
                    break;
                case ReturnKeyTypes.Done:
                    Control.ReturnKeyType = UIKit.UIReturnKeyType.Done;
                    break;
                case ReturnKeyTypes.Send:
                    Control.ReturnKeyType = UIKit.UIReturnKeyType.Send;
                    break;
                case ReturnKeyTypes.Search:
                    Control.ReturnKeyType = UIKit.UIReturnKeyType.Search;
                    break;
                default:
                    Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;
                    break;
            }
        }
    }

    
}