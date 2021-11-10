using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Controls
{
    public class CustomPicker : Picker
    {
      //  public new event EventHandler Completed;

        public static readonly BindableProperty ReturnKeyTypeProperty =
            BindableProperty.Create(nameof(CustomPicker), typeof(ReturnKeyTypes), typeof(CustomPicker), ReturnKeyTypes.Next);

        public static readonly BindableProperty HideBorderProperty =
            BindableProperty.Create(nameof(CustomPicker), typeof(bool), typeof(CustomPicker), default(bool));

        public bool HideBorder
        {
            get
            {
                return (bool)GetValue(HideBorderProperty);
            }
            set
            {
                SetValue(HideBorderProperty, value);
            }
        }

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }




    }
}
