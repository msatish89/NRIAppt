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

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace NRIApp.Droid.CustomRenderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        CustomEditor element;
        public CustomEditorRenderer(Context context) : base(context)
        {

        }

        protected async override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            if (e.NewElement == null) return;
            base.OnElementChanged(e);
            element = Element as CustomEditor;
            if (element.HideBorder)
                if (e.OldElement == null) Control.Background = null;
            if (Control != null && element != null)
            {
                SetReturnKeyType(element);
                Control.EditorAction += (object sender, Android.Widget.TextView.EditorActionEventArgs args) => {
                    if (element.ReturnKeyType != ReturnKeyTypesEditor.Next)
                        element.Unfocus();
                    element.InvokeCompleted();
                    if (element.ReturnKeyType == ReturnKeyTypesEditor.Next)
                        element.OnNext();
                    if (element.ReturnKeyType == ReturnKeyTypesEditor.Go)
                    {
                        element.Execute(sender,args);
                    }
                    element.InvokeCompleted();
                    
                };
            }
            
            // UpdateBorders();

        }

       

        void SetReturnKeyType(CustomEditor entry)
        {
            var type = entry.ReturnKeyType;
            switch (type)
            {
                case ReturnKeyTypesEditor.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case ReturnKeyTypesEditor.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case ReturnKeyTypesEditor.Done:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
                case ReturnKeyTypesEditor.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case ReturnKeyTypesEditor.Search:
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