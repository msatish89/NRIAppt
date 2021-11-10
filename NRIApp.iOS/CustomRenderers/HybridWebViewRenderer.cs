// ***********************************************************************
// Assembly       : XLabs.Forms.iOS
// Author           : XLabs Team
// Created          : 01-06-2016
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-06-2016
// ***********************************************************************
// <copyright file="HybridWebViewRenderer.cs" company="XLabs Team">
//        Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//        This project is licensed under the Apache 2.0 license
//        https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
// 
//        XLabs is a open source project that aims to provide a powerfull and cross
//        platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Foundation;
using NRIApp.Behaviors;
using NRIApp.iOS.CustomRenderers;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
//using XLabs.Forms.Controls;
using XLabs.Platform.Services.IO;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]

namespace NRIApp.iOS.CustomRenderers
{
    /// <summary>
    /// The hybrid web view renderer.
    /// </summary>
    /// 
    public class ClickEventArgs : EventArgs
    {
        public string Element { get; set; }
    }
    public partial class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler
    {
        private UISwipeGestureRecognizer _leftSwipeGestureRecognizer;
        private UISwipeGestureRecognizer _rightSwipeGestureRecognizer;
        private WKUserContentController _userController;

        /// <summary>
        /// Gets the desired size of the view.
        /// </summary>
        /// <returns>The desired size.</returns>
        /// <param name="widthConstraint">Width constraint.</param>
        /// <param name="heightConstraint">Height constraint.</param>
        /// <remarks>
        /// We need to override this method and set the request to 0. Otherwise on view refresh
        /// we will get incorrect view height and might lose the ability to scroll the webview
        /// completely.
        /// </remarks>
        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(Size.Zero, Size.Zero);
        }

        #region Navigation delegates

        /// <summary>
        /// Handles <see cref="WKWebView"/> load finished event.
        /// </summary>
        /// <param name="webView">Web view who has finished loading.</param>
        /// <param name="navigation">Navigation object.</param>
        [Export("webView:didFinishNavigation:")]
        public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            Element.OnLoadFinished(webView, EventArgs.Empty);
        }

        /// <summary>
        /// Handles <see cref="WKWebView"/> load start event.
        /// </summary>
        /// <param name="webView">Web view who has started loading.</param>
        /// <param name="navigation">Navigation object.</param>
        [Export("webView:didStartProvisionalNavigation:")]
        public void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
        {
            Element.OnNavigating(webView.Url);
        }

        #endregion

        /// <summary>
        /// Layouts the subviews.
        /// This is a hack to because the base wasn't working 
        /// when within a stacklayout
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            Control.ScrollView.Frame = Control.Bounds;
        }

        /// <summary>
        /// Implements a method from interface <see cref="IWKScriptMessageHandler"/>.
        /// </summary>
        /// <param name="userContentController">User controller sending the message.</param>
        /// <param name="message">The message being sent.</param>
        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            Element.MessageReceived(message.Body.ToString());
        }

        /// <summary>
        /// The on element changed callback.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null && e.NewElement != null)
            {
                _userController = new WKUserContentController();
                var config = new WKWebViewConfiguration()
                {
                    UserContentController = _userController
                };

                var script = new WKUserScript(new NSString(NativeFunction + GetFuncScript()), WKUserScriptInjectionTime.AtDocumentEnd, false);

                _userController.AddUserScript(script);

                _userController.AddScriptMessageHandler(this, "native");

                var webView = new WKWebView(Frame, config) { WeakNavigationDelegate = this };

                SetNativeControl(webView);

                //webView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                //webView.ScalesPageToFit = true;

                _leftSwipeGestureRecognizer = new UISwipeGestureRecognizer(() => Element.OnLeftSwipe(this, EventArgs.Empty))
                {
                    Direction = UISwipeGestureRecognizerDirection.Left
                };

                _rightSwipeGestureRecognizer = new UISwipeGestureRecognizer(() => Element.OnRightSwipe(this, EventArgs.Empty))
                {
                    Direction = UISwipeGestureRecognizerDirection.Right
                };

                webView.AddGestureRecognizer(_leftSwipeGestureRecognizer);
                webView.AddGestureRecognizer(_rightSwipeGestureRecognizer);
            }

            if (e.NewElement == null && Control != null)
            {
                Control.RemoveGestureRecognizer(_leftSwipeGestureRecognizer);
                Control.RemoveGestureRecognizer(_rightSwipeGestureRecognizer);
            }

            Unbind(e.OldElement);
            Bind();
        }

        partial void HandleCleanup()
        {
            if (Control == null) return;
            Control.RemoveGestureRecognizer(_leftSwipeGestureRecognizer);
            Control.RemoveGestureRecognizer(_rightSwipeGestureRecognizer);
        }

        partial void Inject(string script)
        {
            InvokeOnMainThread(() => Control.EvaluateJavaScript(new NSString(script), (r, e) =>
            {
                if (e != null) Debug.WriteLine(e);
            }));
        }

        partial void Load(Uri uri)
        {
            if (uri != null)
            {
                Control.LoadRequest(new NSUrlRequest(new NSUrl(uri.AbsoluteUri)));
            }
        }

        partial void LoadFromContent(object sender, HybridWebView.LoadContentEventArgs contentArgs)
        {
            var baseUri = contentArgs.BaseUri ?? GetTempDirectory();
            Element.Uri = new Uri(baseUri + "/" + contentArgs.Content);
            //Element.Uri = new Uri(NSBundle.MainBundle.BundlePath + "/" + contentFullName);
            //Control.LoadHtmlString(new NSString(contentFullName), new NSUrl(NSBundle.MainBundle.BundlePath, true));
        }

        partial void LoadContent(object sender, HybridWebView.LoadContentEventArgs contentArgs)
        {
            var baseUri = contentArgs.BaseUri ?? GetTempDirectory();
            Control.LoadHtmlString(new NSString(contentArgs.Content), new NSUrl(baseUri, true));
        }

        partial void LoadFromString(string html)
        {
            LoadContent(null, new HybridWebView.LoadContentEventArgs(html, null));
        }

        /// <summary>
        /// Copies bundle directory to temp directory.
        /// </summary>
        /// <param name="path">Directory to copy.</param>
        public static void CopyBundleDirectory(string path)
        {
            var source = Path.Combine(NSBundle.MainBundle.BundlePath, path);
            var dest = Path.Combine(GetTempDirectory(), path);

            FileManager.CopyDirectory(new DirectoryInfo(source), new DirectoryInfo(dest));
        }

        private static string GetTempDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace("Documents", "tmp");
        }
    }

    public partial class HybridWebViewRenderer
    {

#if WINDOWS_PHONE || NETFX_CORE
        private const string NativeFuncCall = "window.external.notify";
        private const string NativeFunction = "function Native(action, data){window.external.notify(JSON.stringify({ a: action, d: data }));}";
#elif __IOS__
        private const string NativeFuncCall = "window.webkit.messageHandlers.native.postMessage";
        private const string NativeFunction = "function Native(action, data){window.webkit.messageHandlers.native.postMessage(JSON.stringify({ a: action, d: data }));}";
#elif __ANDROID__
        private const string NativeFuncCall = "Xamarin.call";
        private const string NativeFunction = "function Native(action, data){Xamarin.call(JSON.stringify({ a: action, d: data }));}";
#endif

        //private const string Format = "^(file|http|https)://(local|LOCAL)/Action(=|%3D)(?<Action>[\\w]+)/";
        private const string FuncFormat = "^(file|http|https)://(local|LOCAL)/Func(=|%3D)(?<CallbackIdx>[\\d]+)(&|%26)(?<FuncName>[\\w]+)/";
        //private static readonly Regex Expression = new Regex(Format);
        private static readonly Regex FuncExpression = new Regex(FuncFormat);

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Xamarin will changed the renderer attached to a view so it is possible that
            // an old renderer gets a property updated.  In this case the Element will be null.
            // In that case, try to clear the property event handler and exit.
            if (Element == null)
            {
                HybridWebView wv = sender as HybridWebView;
                if (wv != null)
                {
                    wv.PropertyChanged -= this.OnElementPropertyChanged;
                }

                return;
            }

            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == HybridWebView.UriProperty.PropertyName)
            {
                this.Load(this.Element.Uri);
            }
            else if (e.PropertyName == HybridWebView.SourceProperty.PropertyName)
            {
                LoadSource();
            }
            else if (e.PropertyName == HybridWebView.CleanupProperty.PropertyName)
            {
                HandleCleanup();
            }
        }

        private string GetFuncScript()
        {
            var builder = new StringBuilder();
            builder.Append("NativeFuncs = [];");
            builder.Append("function NativeFunc(action, data, callback){");

            builder.Append("  var callbackIdx = NativeFuncs.push(callback) - 1;");
            builder.Append(NativeFuncCall);
            builder.Append("(JSON.stringify({ a: action, d: data, c: callbackIdx }));}");
            builder.Append(" if (typeof(window.NativeFuncsReady) !== 'undefined') { ");
            builder.Append("   window.NativeFuncsReady(); ");
            builder.Append(" } ");

            return builder.ToString();
        }

        private void Bind()
        {
            if (Element != null)
            {
                if (this.Element.Uri != null)
                {
                    this.Load(this.Element.Uri);
                }
                else
                {
                    LoadSource();
                }

                // There should only be one renderer and thus only one event handler registered.
                // Otherwise, when Xamarin creates a new renderer, the old one stays attached
                // and crashes when called!
                this.Element.JavaScriptLoadRequested = OnInjectRequest;
                this.Element.LoadFromContentRequested = LoadFromContent;
                this.Element.LoadContentRequested = LoadContent;
            }
        }

        private void LoadSource()
        {
            var htmlSource = this.Element.Source as HtmlWebViewSource;
            if (htmlSource != null)
            {
                this.LoadFromString(htmlSource.Html);
                return;
            }

            var webViewSource = this.Element.Source as UrlWebViewSource;

            if (webViewSource != null)
            {
                this.Load(new Uri(webViewSource.Url));
            }
        }

        private void Unbind(HybridWebView oldElement)
        {
            if (oldElement != null)
            {
                oldElement.JavaScriptLoadRequested -= OnInjectRequest;
                oldElement.LoadFromContentRequested -= LoadFromContent;
                oldElement.LoadContentRequested -= LoadContent;
                oldElement.PropertyChanged -= this.OnElementPropertyChanged;
            }
        }

        private void OnInjectRequest(object sender, string script)
        {
            this.Inject(script);
        }

        partial void Inject(string script);

        partial void Load(Uri uri);

        partial void LoadFromContent(object sender, HybridWebView.LoadContentEventArgs contentArgs);

        partial void LoadContent(object sender, HybridWebView.LoadContentEventArgs contentArgs);

        partial void LoadFromString(string html);

        partial void HandleCleanup();



        //private bool CheckRequest(string request)
        //{
        //    var m = Expression.Match(request);

        //    if (m.Success)
        //    {
        //        Action<string> action;
        //        var name = m.Groups["Action"].Value;

        //        if (this.Element.TryGetAction (name, out action))
        //        {
        //            var data = Uri.UnescapeDataString (request.Remove (m.Index, m.Length));
        //            action.Invoke (data);
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine(string.Format("Unhandled callback {0} was called from JavaScript", name));
        //        }
        //    }

        //    var mFunc = FuncExpression.Match(request);

        //    if (mFunc.Success)
        //    {
        //        Func<string, object[]> func;
        //        var name = mFunc.Groups["FuncName"].Value;
        //        var callBackIdx = mFunc.Groups["CallbackIdx"].Value;

        //        if (this.Element.TryGetFunc (name, out func))
        //        {
        //            var data = Uri.UnescapeDataString (request.Remove (mFunc.Index, mFunc.Length));
        //            ThreadPool.QueueUserWorkItem(o =>
        //                {
        //                    var result = func.Invoke (data);
        //                    Element.CallJsFunction(string.Format("NativeFuncs[{0}]", callBackIdx), result);
        //                });
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine ("Unhandled callback {0} was called from JavaScript", name);
        //        }
        //    }

        //    return m.Success || mFunc.Success;
        //}

        private void TryInvoke(string function, string data)
        {
            Action<string> action;

            if (this.Element != null && this.Element.TryGetAction(function, out action))
            {
                action.Invoke(data);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Unhandled callback {0} was called from JavaScript", function);
            }
        }
    }
}
