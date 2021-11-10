using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Helpers
{
    public class ImageSourceConverter : IValueConverter
    {
        static readonly WebClient Client = new WebClient();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null || value == "")
                    return null;
                if (!value.ToString().Contains("/"))
                    return value;

                var byteArray = Client.DownloadData(value.ToString());
                value= ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
            catch(Exception ex)
            {

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
