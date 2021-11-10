using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Helpers
{
  public class Bindabletext : ICommonInterface
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string datatext = (string)value;
            var data = new HtmlWebViewSource
            {
                Html = (string)value
            };
            return datatext;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
