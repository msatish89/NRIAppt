using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using NRIApp.Helpers;
using NRIApp.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace NRIApp.iOS.CustomRenderers
{
   public class ExtendedWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            Delegate = new ExtendedUIWebViewDelegate(this);
        }
    }
    public class ExtendedUIWebViewDelegate : UIWebViewDelegate
    {
        ExtendedWebViewRenderer webViewRenderer;

        public ExtendedUIWebViewDelegate(ExtendedWebViewRenderer _webViewRenderer = null)
        {
            webViewRenderer = _webViewRenderer ?? new ExtendedWebViewRenderer();
        }

        public override async void LoadingFinished(UIWebView webView)
        {
            var wv = webViewRenderer.Element as ExtendedWebView;
            if (wv != null)
            {
                await System.Threading.Tasks.Task.Delay(100); // wait here till content is rendered
                wv.HeightRequest = (double)webView.ScrollView.ContentSize.Height;
            }
        }
    }
}