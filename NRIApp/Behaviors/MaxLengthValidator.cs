using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using NRIApp.Controls;

namespace NRIApp.Behaviors
{
    public class MaxLengthValidator : Behavior<CustomEntry>
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
            if (e.NewTextValue.Length >= MaxLength)
            {
               
                ((CustomEntry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
                bool IsValid = true;
                ((CustomEntry)sender).TextColor = IsValid ? Color.Default : Color.Gray;
            }
            else
            {
                bool IsValid = false;
               // ((CustomEntry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            }
                

        }
    }
}
