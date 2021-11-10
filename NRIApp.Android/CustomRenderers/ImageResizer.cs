using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using NRIApp.Droid.CustomRenderers;
using NRIApp.Helpers;
using Android.Gms.Ads;
using System.Threading;
using Android.Graphics;
using System.IO;

[assembly: Dependency(typeof(ImageResizer))]
namespace NRIApp.Droid.CustomRenderers
{
    public class ImageResizer  : IimageResize
    {
        public ImageResizer()
        {

        }
        public byte[] ResizeImage(byte[] imageData, int width, int height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, width, height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}