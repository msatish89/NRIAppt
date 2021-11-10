using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NRIApp.Helpers
{
    public interface ICommonInterface
    {
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
