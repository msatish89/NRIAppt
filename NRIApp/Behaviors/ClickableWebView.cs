using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.Behaviors
{
    
    public class ClickEventArgs : EventArgs
    {
        public string Element { get; set; }
    }

    public class ClickableWebView : NRIApp.Behaviors.HybridWebView
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
                var hdncityurl=document.getElementById('hdnlstscitystatecodeurl').value;
                document.getElementById('linktermsconditions').removeAttribute('href');
                Native('invokeClick','id='+target.id+' cityurl='+hdncityurl);
            }, true /* to ensure we capture it first*/);
        ");

            this.RegisterCallback("invokeClick", (string el) => {
                var args = new ClickEventArgs { Element = el };

                Clicked?.Invoke(this, args);
                ClickCommand?.Execute(args);
            });
        }
    }
}
