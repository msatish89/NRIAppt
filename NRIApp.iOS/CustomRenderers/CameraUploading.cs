using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using NRIApp.Helpers;
using Plugin.Media.Abstractions;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ICameraUploading))]
namespace NRIApp.iOS.CustomRenderers
{
    public class CameraUploading :ICameraUploading
    {
     
        public Func<object> GetImage()
        {
            Func<object> func = () =>
            {
                var imageView = new UIImageView(UIImage.FromBundle("face-template.png"));
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

                var screen = UIScreen.MainScreen.Bounds;
                imageView.Frame = screen;

                return imageView;
            };
             return func;
        }


        //Func<object> ICameraUploading.func()
        //{
        //    var imageView = new UIImageView(UIImage.FromBundle("face-template.png"));
        //    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

        //    var screen = UIScreen.MainScreen.Bounds;
        //    imageView.Frame = screen;

        //    return func;
        //}
        //Take Photo, could be in iOS Project, or in shared code where there function is passed up via Dependency Services.
        //var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
        //{
        //    OverlayViewProvider = func
        //});
    }
}