using NRIApp.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace NRIApp.Behaviors
{
    class EmailValidatorBehavior : Behavior<CustomEntry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        protected override void OnAttachedTo(CustomEntry bindable)
        {
            //if (bindable.Text == "" || bindable.Text == null)
            //    bindable.BackgroundColor = Color.Red;

            // Code to show unfocused only

            //bindable.Unfocused += (object sender, FocusEventArgs e) => {
            //    if (!e.IsFocused)
            //    {
            //        if (bindable.Text == "" || bindable.Text == null)
            //            bindable.BackgroundColor = Color.Red;
            //        else
            //            bindable.BackgroundColor = Color.White;
            //        ((CustomEntry)sender).Focus();
            //    }
            //};

            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);

        }

       public  void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
 IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
              //  ((CustomEntry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(CustomEntry bindable)
        {
            bool IsValid = false;

            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
