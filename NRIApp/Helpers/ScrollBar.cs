using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Helpers
{
    public class ScrollBar: HVScrollGridView
    {
        public static readonly BindableProperty IsHorizontalScrollbarEnabledProperty =
        BindableProperty.Create(
            nameof(IsHorizontalScrollbarEnabledProperty),
            typeof(bool),
            typeof(ScrollBar),
            false,
           BindingMode.Default
            );

        public bool IsHorizontalScrollbarEnabled
        {
            get { return (bool)GetValue(IsHorizontalScrollbarEnabledProperty); }
            set { SetValue(IsHorizontalScrollbarEnabledProperty, value); }
        }

        public static readonly BindableProperty IsVerticalScrollbarEnabledProperty =
            BindableProperty.Create(
                nameof(IsVerticalScrollbarEnabledProperty),
                typeof(bool),
                typeof(ScrollBar),
                false,
                BindingMode.Default);

        public bool IsVerticalalScrollbarEnabled
        {
            get { return (bool)GetValue(IsVerticalScrollbarEnabledProperty); }
            set { SetValue(IsVerticalScrollbarEnabledProperty, value); }
        }
    }
}
