using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace NRIApp.Behaviors
{
    public class MobileNumberValidator : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty
        = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaxLengthValidator), default(int));

        public int MaxLength
        {
            get
            {
                return (int)GetValue(MaxLengthProperty);
            }
            set
            {
                SetValue(MaxLengthProperty, value);
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Handle_NumberFormatter;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Handle_NumberFormatter;
            base.OnDetachingFrom(bindable);
        }

        private void Handle_NumberFormatter(object sender, TextChangedEventArgs e)
        {
            string val = e.NewTextValue;
            Regex regObj = new Regex(@"[^\d]");
            val = regObj.Replace(val, string.Empty);
            if (MaxLength == 0) MaxLength = 10;
            if (val.Length > 0)
            {
                val = val.Length > MaxLength ? val.Substring(0, MaxLength) : val;
                if (!val.ToCharArray().All(CheckIsDigit))
                    val = val.Remove(val.Length - 1);
            }
            ((Entry)sender).Text = val;
        }

        private bool CheckIsDigit(Char c)
        {
            return c >= 48 && c <= 57;
        }
    }
}
