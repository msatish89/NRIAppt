using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using NRIApp.Controls;

namespace NRIApp.Behaviors
{
    public class NumericInputValidator : Behavior<CustomEntry>
    {
        protected override void OnAttachedTo(CustomEntry bindable)
        {
            bindable.TextChanged += Handle_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CustomEntry bindable)
        {
            bindable.TextChanged -= Handle_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && e.NewTextValue.Length >= 1)
            {
                bool isValid = e.NewTextValue.ToCharArray().All(CheckIsDigit);
                ((CustomEntry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private bool CheckIsDigit(Char c)
        {
            return c >= 48 && c <= 57;
        }
    }
}
