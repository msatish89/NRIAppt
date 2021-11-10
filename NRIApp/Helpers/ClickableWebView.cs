using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace USAppHomePage.Audio
{
    public class ClickEventArgs : EventArgs
    {
        public string Element { get; set; }
    }
    public class ClickableWebView : XLabs.Forms.Controls.HybridWebView
    {
        public event EventHandler<ClickEventArgs> Clicked;

        public static readonly BindableProperty ClickCommandProperty =
            BindableProperty.Create("ClickCommand", typeof(ICommand), typeof(ClickableWebView), null);

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public ClickableWebView()
        {
            LoadFinished += (sender, e) =>
                InjectJavaScript(@"
            document.body.addEventListener('click', function(e) {
                e = e || window.event;
                var target = e.target || e.srcElement;
                Native('invokeClick', 'tag='+target.tagName+' id='+target.id+' name='+target.name);
            }, true /* to ensure we capture it first*/);
        ");

            this.RegisterCallback("invokeClick", (string el) =>
            {
                var args = new ClickEventArgs { Element = el };

                Clicked?.Invoke(this, args);
                ClickCommand?.Execute(args);
            });
        }
    }
}
