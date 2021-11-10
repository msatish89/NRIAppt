// ***********************************************************************
// Assembly         : XLabs.Forms.Droid
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="HybridWebViewRenderer.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using System.Text;
using System.Text.RegularExpressions;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Java.Interop;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;
//using XLabs.Forms.Controls;
using Object = Java.Lang.Object;
using WebView = Android.Webkit.WebView;
using NRIApp.Behaviors;
using NRIApp.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]

namespace NRIApp.Droid.CustomRenderers
{
    /// <summary>
    /// Class HybridWebViewRenderer.
    /// </summary>
    public partial class HybridWebViewRenderer : ViewRenderer<HybridWebView, HybridWebViewRenderer.NativeWebView>
    {

        public HybridWebViewRenderer(Context context) : base(context)
        {
        }
        /// <summary>
        /// Allows one to override the Webview Client class without a custom renderer.
        /// </summary>
        public static Func<HybridWebViewRenderer,Client> GetWebViewClientDelegate;

        /// <summary>
        /// Allows one to override the Chrome Client class without a custom renderer.
        /// </summary>
        public static Func<HybridWebViewRenderer, ChromeClient> GetWebChromeClientDelegate;

        /// <summary>
        /// Gets the desired size of the view.
        /// </summary>
        /// <param name="widthConstraint">Width constraint.</param>
        /// <param name="heightConstraint">Height constraint.</param>
        /// <returns>The desired size.</returns>
        /// <remarks>We need to override this method and set the request height to 0. Otherwise on view refresh
        /// we will get incorrect view height and might lose the ability to scroll the webview
        /// completely.</remarks>
        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            var sizeRequest = base.GetDesiredSize(widthConstraint, heightConstraint);
            sizeRequest.Request = new Size(sizeRequest.Request.Width, 0);
            return sizeRequest;

            //return new SizeRequest(Size.Zero, Size.Zero);
        }

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged (e);

            if (this.Control == null && e.NewElement != null)
            {
                var webView = new NativeWebView(this,e.NewElement.AndroidAdditionalTouchCallback);

                webView.Settings.JavaScriptEnabled = true;
                webView.Settings.DomStorageEnabled = true;

                //Turn off hardware rendering
                webView.SetLayerType(e.NewElement.AndroidHardwareRendering? LayerType.Hardware : LayerType.Software, null);

                //Set the background color to transparent to fix an issue where the
                //the picture would fail to draw
                webView.SetBackgroundColor(Color.Transparent.ToAndroid());

                webView.SetWebViewClient(this.GetWebViewClient());
                webView.SetWebChromeClient(this.GetWebChromeClient());

                webView.AddJavascriptInterface(new Xamarin(this), "Xamarin");

                this.SetNativeControl(webView);

                webView.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            }
            
            this.Unbind(e.OldElement);

            this.Bind();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Element != null)
            {
                if (this.Control != null)
                {
                    this.Control.StopLoading();
                }

                Unbind(this.Element);
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets <see cref="Client"/> for the web view.
        /// </summary>
        /// <returns><see cref="Client"/></returns>
        protected virtual Client GetWebViewClient()
        {
            var d = GetWebViewClientDelegate;

            return d != null ? d(this) : new Client(this);
        }

        /// <summary>
        /// Gets <see cref="ChromeClient"/> for the web view.
        /// </summary>
        /// <returns><see cref="ChromeClient"/></returns>
        protected virtual ChromeClient GetWebChromeClient()
        {
            var d = GetWebChromeClientDelegate;

            return d != null ? d(this) : new ChromeClient();
        }

        partial void HandleCleanup() 
        {
            if (Control == null) return;

            Control.SetWebViewClient (null);
            Control.SetWebChromeClient (null);
            Control.RemoveJavascriptInterface ("Xamarin");
        }

        protected void OnPageFinished()
        {
            if (this.Element == null) return;
            this.Inject(NativeFunction);
            this.Inject(GetFuncScript());
            this.Element.OnLoadFinished(this, EventArgs.Empty);
        }

        /// <summary>
        /// Injects the specified script.
        /// </summary>
        /// <param name="script">The script.</param>
        partial void Inject(string script)
        {
            if (Control != null) 
            {
                this.Control.LoadUrl(string.Format("javascript: {0}", script));
            }
        }

        /// <summary>
        /// Loads the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        partial void Load(Uri uri)
        {
            if (uri != null && Control != null)
            {
                this.Control.LoadUrl(uri.AbsoluteUri);
            }
        }

        /// <summary>
        /// Loads from content.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="contentFullName">Full name of the content.</param>
        partial void LoadFromContent(object sender, HybridWebView.LoadContentEventArgs contentArgs)
        {
            var baseUri = new Uri(contentArgs.BaseUri ?? "file:///android_asset/");
            this.Element.Uri = new Uri(baseUri, contentArgs.Content);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="content">Full name of the content.</param>
        partial void LoadContent(object sender, HybridWebView.LoadContentEventArgs contentArgs)
        {
            if (Control != null) 
            {
                var baseUri = contentArgs.BaseUri ?? "file:///android_asset/";
                this.Control.LoadDataWithBaseURL(baseUri, contentArgs.Content, "text/html", "UTF-8", null);
            }
        }

        

        /// <summary>
        /// Loads from string.
        /// </summary>
        /// <param name="html">The HTML.</param>
        partial void LoadFromString(string html)
        {
            if (Control != null) 
            {
                this.Control.LoadData(html, "text/html", "UTF-8");
            }
        }

        

        /// <summary>
        /// Class Client.
        /// </summary>
        public class Client : WebViewClient
        {
            /// <summary>
            /// The web hybrid
            /// </summary>
            protected readonly WeakReference<HybridWebViewRenderer> WebHybrid;

            /// <summary>
            /// Initializes a new instance of the <see cref="Client"/> class.
            /// </summary>
            /// <param name="webHybrid">The web hybrid.</param>
            public Client(HybridWebViewRenderer webHybrid)
            {
                this.WebHybrid = new WeakReference<HybridWebViewRenderer>(webHybrid);
            }

            /// <summary>
            /// Notify the host application that a page has finished loading.
            /// </summary>
            /// <param name="view">The WebView that is initiating the callback.</param>
            /// <param name="url">The url of the page.</param>
            /// <since version="Added in API level 1" />
            /// <remarks><para tool="javadoc-to-mdoc">Notify the host application that a page has finished loading. This method
            /// is called only for main frame. When onPageFinished() is called, the
            /// rendering picture may not be updated yet. To get the notification for the
            /// new Picture, use <c><see cref="M:Android.Webkit.WebView.IPictureListener.OnNewPicture(Android.Webkit.WebView, Android.Graphics.Picture)" /></c>.</para>
            /// <para tool="javadoc-to-mdoc">
            ///   <format type="text/html">
            ///     <a href="http://developer.android.com/reference/android/webkit/WebViewClient.html#onPageFinished(android.webkit.WebView, java.lang.String)" target="_blank">[Android Documentation]</a>
            ///   </format>
            /// </para></remarks>
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);

                HybridWebViewRenderer hybrid;
                if (this.WebHybrid != null && this.WebHybrid.TryGetTarget(out hybrid))
                {
                    hybrid.OnPageFinished();
                }
            }
        }

        /// <summary>
        /// Java callback class for JavaScript.
        /// </summary>
        public class Xamarin : Object
        {
            /// <summary>
            /// The web hybrid.
            /// </summary>
            private readonly WeakReference<HybridWebViewRenderer> webHybrid;

            /// <summary>
            /// Initializes a new instance of the <see cref="Xamarin"/> class.
            /// </summary>
            /// <param name="webHybrid">The web hybrid.</param>
            public Xamarin(HybridWebViewRenderer webHybrid)
            {
                this.webHybrid = new WeakReference<HybridWebViewRenderer>(webHybrid);
            }

            /// <summary>
            /// Calls the specified function.
            /// </summary>
            /// <param name="message">The message.</param>
            [JavascriptInterface]
            [Export("call")]
            public void Call(string message)
            {
                HybridWebViewRenderer hybrid;
                HybridWebView webView;
                if (this.webHybrid != null && this.webHybrid.TryGetTarget(out hybrid) && ((webView = hybrid.Element) != null))
                {
                    webView.MessageReceived(message);
                }
            }
        }

        /// <summary>
        /// Class ChromeClient.
        /// </summary>
        public class ChromeClient : WebChromeClient 
        {
            /// <summary>
            /// Overrides the geolocation prompt and accepts the permission.
            /// </summary>
            /// <param name="origin">Origin of the prompt.</param>
            /// <param name="callback">Permission callback.</param>
            /// <remarks>Always grant permission since the app itself requires location
            /// permission and the user has therefore already granted it.</remarks>
            public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
            {
                callback.Invoke(origin, true, false);
            }
        }

        /// <summary>
        /// Class NativeWebView.
        /// </summary>
        public class NativeWebView : WebView
        {
            /// <summary>
            /// The detector
            /// </summary>
            private readonly GestureDetector detector;

            /// <summary>
            /// Detector switch
            /// </summary>
            private readonly bool enableDetector;


            /// <summary>
            /// Initializes a new instance of the <see cref="NativeWebView"/> class.
            /// </summary>
            /// <param name="renderer">The renderer.</param>
            public NativeWebView(HybridWebViewRenderer renderer, bool enableAdditionalTouchDetector) : base(renderer.Context)
            {
                enableDetector = enableAdditionalTouchDetector;

                if (enableDetector)
                {
                    var listener = new MyGestureListener(renderer);
                    this.detector = new GestureDetector(this.Context, listener);
                }
            }

            /// <summary>
            /// This is an Android specific constructor that sometimes needs to be called by the underlying
            /// Xamarin ACW environment.
            /// </summary>
            /// <param name="ptr"></param>
            /// <param name="handle"></param>
            public NativeWebView(IntPtr ptr, JniHandleOwnership handle) : base(ptr, handle)
            {

            }

            /// <summary>
            /// Implement this method to handle touch screen motion events.
            /// </summary>
            /// <param name="e">The motion event.</param>
            /// <returns>To be added.</returns>
            /// <since version="Added in API level 1" />
            /// <remarks><para tool="javadoc-to-mdoc">Implement this method to handle touch screen motion events.</para>
            /// <para tool="javadoc-to-mdoc">
            ///   <format type="text/html">
            ///     <a href="http://developer.android.com/reference/android/view/View.html#onTouchEvent(android.view.MotionEvent)" target="_blank">[Android Documentation]</a>
            ///   </format>
            /// </para></remarks>
            public override bool OnTouchEvent(MotionEvent e)
            {
                if (enableDetector)
                {
                    this.detector.OnTouchEvent(e);
                }
                return base.OnTouchEvent(e);
            }

            /// <summary>
            /// Class MyGestureListener.
            /// </summary>
            private class MyGestureListener : GestureDetector.SimpleOnGestureListener
            {
                /// <summary>
                /// The swip e_ mi n_ distance
                /// </summary>
                private const int SWIPE_MIN_DISTANCE = 120;
                /// <summary>
                /// The swip e_ ma x_ of f_ path
                /// </summary>
                private const int SWIPE_MAX_OFF_PATH = 200;
                /// <summary>
                /// The swip e_ threshol d_ velocity
                /// </summary>
                private const int SWIPE_THRESHOLD_VELOCITY = 200;

                /// <summary>
                /// The web hybrid
                /// </summary>
                private readonly WeakReference<HybridWebViewRenderer> webHybrid;

                /// <summary>
                /// Initializes a new instance of the <see cref="MyGestureListener"/> class.
                /// </summary>
                /// <param name="renderer">The renderer.</param>
                public MyGestureListener(HybridWebViewRenderer renderer)
                {
                    this.webHybrid = new WeakReference<HybridWebViewRenderer>(renderer);
                }

//                public override void OnLongPress(MotionEvent e)
//                {
//                    Console.WriteLine("OnLongPress");
//                    base.OnLongPress(e);
//                }
//
//                public override bool OnDoubleTap(MotionEvent e)
//                {
//                    Console.WriteLine("OnDoubleTap");
//                    return base.OnDoubleTap(e);
//                }
//
//                public override bool OnDoubleTapEvent(MotionEvent e)
//                {
//                    Console.WriteLine("OnDoubleTapEvent");
//                    return base.OnDoubleTapEvent(e);
//                }
//
//                public override bool OnSingleTapUp(MotionEvent e)
//                {
//                    Console.WriteLine("OnSingleTapUp");
//                    return base.OnSingleTapUp(e);
//                }
//
//                public override bool OnDown(MotionEvent e)
//                {
//                    Console.WriteLine("OnDown");
//                    return base.OnDown(e);
//                }

                /// <summary>
                /// Called when [fling].
                /// </summary>
                /// <param name="e1">The e1.</param>
                /// <param name="e2">The e2.</param>
                /// <param name="velocityX">The velocity x.</param>
                /// <param name="velocityY">The velocity y.</param>
                /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
                public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
                {
                    HybridWebViewRenderer hybrid;

                    if (this.webHybrid.TryGetTarget(out hybrid) && Math.Abs(velocityX) > SWIPE_THRESHOLD_VELOCITY)
                    {
                        if(e1.GetX() - e2.GetX() > SWIPE_MIN_DISTANCE) 
                        {
                            hybrid.Element.OnLeftSwipe(this, EventArgs.Empty);
                        }  
                        else if (e2.GetX() - e1.GetX() > SWIPE_MIN_DISTANCE) 
                        {
                            hybrid.Element.OnRightSwipe(this, EventArgs.Empty);
                        }
                    }

                    return base.OnFling(e1, e2, velocityX, velocityY);
                }

//                public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
//                {
//                    Console.WriteLine("OnScroll");
//                    return base.OnScroll(e1, e2, distanceX, distanceY);
//                }
//
//                public override void OnShowPress(MotionEvent e)
//                {
//                    Console.WriteLine("OnShowPress");
//                    base.OnShowPress(e);
//                }
//
//                public override bool OnSingleTapConfirmed(MotionEvent e)
//                {
//                    Console.WriteLine("OnSingleTapConfirmed");
//                    return base.OnSingleTapConfirmed(e);
//                }

            }
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

