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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NRIApp.Droid;
using Android.Graphics.Drawables;
using NRIApp.Droid.CustomRenderers;
using NRIApp.Controls;
using Android.Views.InputMethods;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace NRIApp.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        CustomEntry element;
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected async override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (e.NewElement == null) return;
            base.OnElementChanged(e);
            element = Element as CustomEntry;
            if (element.HideBorder)
                if (e.OldElement == null) Control.Background = null;
            if (Control != null && element != null)
            {
                SetReturnKeyType(element);
                Control.EditorAction += (object sender, Android.Widget.TextView.EditorActionEventArgs args) => {
                    if (element.ReturnKeyType != ReturnKeyTypes.Next)
                        element.Unfocus();
                    element.InvokeCompleted();
                    if (element.ReturnKeyType == ReturnKeyTypes.Next)
                        element.OnNext();
                    if (element.ReturnKeyType == ReturnKeyTypes.Go)
                    {
                        element.Execute(sender,args);
                    }
                    element.InvokeCompleted();
                    
                };
            }
            
            // UpdateBorders();

        }

       

        void SetReturnKeyType(CustomEntry entry)
        {
            var type = entry.ReturnKeyType;
            switch (type)
            {
                case ReturnKeyTypes.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case ReturnKeyTypes.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case ReturnKeyTypes.Done:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
                case ReturnKeyTypes.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case ReturnKeyTypes.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
            }
        }

    }
}