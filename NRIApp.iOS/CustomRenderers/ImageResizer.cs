using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using NRIApp.Helpers;
using NRIApp.iOS.CustomRenderers;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ImageResizer))]
namespace NRIApp.iOS.CustomRenderers
{
   public class ImageResizer : IimageResize
    {
        public ImageResizer()
        {

        }

        public  byte[] ResizeImage(byte[] imageData, int width, int height)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            UIImageOrientation orientation = originalImage.Orientation;

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 width, height, 8,
                                                 4 * width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                RectangleF imageRect = new RectangleF(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }
        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
        private UIImage ImageFromBytes(byte[] bytes, nfloat width, nfloat height)
        {
            try
            {
                NSData data = NSData.FromArray(bytes);
                UIImage image = UIImage.LoadFromData(data);
                CGSize scaleSize = new CGSize(width, height);
                UIGraphics.BeginImageContextWithOptions(scaleSize, false, 0);
                image.Draw(new CGRect(0, 0, scaleSize.Width, scaleSize.Height));
                UIImage resizedImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
                return resizedImage;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}