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
using NRIApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(CustomListViewRenderer))]
namespace NRIApp.Droid.CustomRenderers
{

    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer(Context context) : base(context)
        {
            
           
        }
        protected  override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {

                // allows ListView to be scrollable inside a of a ScrollView;
                // this behavior is default in iOS so only custom renderer is needed in Android
                var listView = Control as Android.Widget.ListView;
                listView.NestedScrollingEnabled = true;
            }
        }
    }
}