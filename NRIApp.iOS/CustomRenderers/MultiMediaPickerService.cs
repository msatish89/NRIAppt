using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AVFoundation;
using Foundation;
using GMImagePicker;
using NRIApp.Helpers;
using NRIApp.iOS.CustomRenderers;
using Photos;
using Plugin.Media;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MultiMediaPickerService))]
namespace NRIApp.iOS.CustomRenderers
{
    public class MultiMediaPickerService : IMultiMediaPickerService
    {
        //private static int photocount;
        IUserDialogs dialog = UserDialogs.Instance;
        const string TemporalDirectoryName = "TmpMedia";
        GMImagePickerController currentPicker;
        TaskCompletionSource<IList<MediaFile>> mediaPickTcs;
        public event EventHandler<MediaFile> OnMediaPicked;
        public event EventHandler<IList<MediaFile>> OnMediaPickedCompleted;
        public async Task PickPhotosAsync(string adid)
        {

            await PickMediaAsync("Select Images", PHAssetMediaType.Image);

        }
        //permission need to be added in info.plist
        //NSPhotoLibraryUsageDescription
        private List<String> selectImagePaths;
        async Task<IList<MediaFile>> PickMediaAsync(string title, PHAssetMediaType type)
        {

            mediaPickTcs = new TaskCompletionSource<IList<MediaFile>>();
            currentPicker = new GMImagePickerController()
            {
                Title = title,
                MediaTypes = new[] { type }
            };

            currentPicker.FinishedPickingAssets += FinishedPickingAssets;

            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }
            await vc.PresentViewControllerAsync(currentPicker, true);

            var results = await mediaPickTcs.Task;

            currentPicker.FinishedPickingAssets -= FinishedPickingAssets;
            OnMediaPickedCompleted?.Invoke(this, results);
            return results;
        }

        async void FinishedPickingAssets(object sender, MultiAssetEventArgs args)
        {
            IList<MediaFile> results = new List<MediaFile>();
            TaskCompletionSource<IList<MediaFile>> tcs = new TaskCompletionSource<IList<MediaFile>>();

            var options = new PHImageRequestOptions()
            {
                NetworkAccessAllowed = true
            };

            options.Synchronous = false;
            options.ResizeMode = PHImageRequestOptionsResizeMode.Fast;
            options.DeliveryMode = PHImageRequestOptionsDeliveryMode.HighQualityFormat;
            bool completed = false;
            byte[] media = null;
            var name = "";
            for (var i = 0; i < args.Assets.Length; i++)
            {
                var asset = args.Assets[i];

                string fileName = string.Empty;
                if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
                {
                    fileName = PHAssetResource.GetAssetResources(asset).FirstOrDefault().OriginalFilename;

                }
                PHImageManager.DefaultManager.RequestImageData(asset, options, (data, dataUti, orientation, info) =>
                {

                    string path = FileHelper.GetOutputPath(MediaFileType.Image, TemporalDirectoryName, fileName);

                    if (!File.Exists(path))
                    {
                        // Debug.WriteLine(dataUti);
                        var imageData = data;
                        var image = UIImage.LoadFromData(imageData);
                        //imageData?.Save(path, true);
                        NSData imgdata = image.AsJPEG();
                        MemoryStream ms = new MemoryStream();
                        imgdata.AsStream().CopyTo(ms);
                        media = ms.ToArray();

                    }

                    var meFile = new MediaFile()
                    {
                        Type = MediaFileType.Image,
                        Path = path,
                        PreviewPath = path
                    };

                    results.Add(meFile);
                    OnMediaPicked?.Invoke(this, meFile);
                    if (args.Assets.Length == results.Count && !completed)
                    {
                        completed = true;
                        tcs.TrySetResult(results);
                    }
                    if (Commonsettings.CategoryType == "Roommates" && !string.IsNullOrEmpty(NRIApp.Roommates.Features.Post.ViewModels.VMPost.postadid_RM.ToString()))
                    {
                        name = "roommates" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Roommates.Features.Post.ViewModels.VMPost.postadid_RM + ".jpg";
                        if (media != null)
                        {
                            uploadtoserver(media, name, "roommates");
                        }

                    }
                    else if (Commonsettings.CategoryType == "Rentals" && !string.IsNullOrEmpty(NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.postadid_RT.ToString()))
                    {
                        name = "rentals" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.postadid_RT + ".jpg";
                        if (media != null)
                        {
                            uploadtoserver(media, name, "rentals");
                        }
                    }

                });

            }

            mediaPickTcs?.TrySetResult(await tcs.Task);
        }


        public byte[] CreateMediaFileFromUri(NSUrl fullSizeImageUrl)
        {
            //Stream stream =  ContentResolver.OpenInputStream(fullSizeImageUrl);
            Stream stream = new FileStream(fullSizeImageUrl.Path, FileMode.Open, FileAccess.Read);
            byte[] byteArray;

            using (var memoryStream = new MemoryStream())
            {

                stream.CopyTo(memoryStream);
                byteArray = memoryStream.ToArray();
            }
            return byteArray;
        }
        public async void uploadtoserver(byte[] media, string name, string imgname)
        {
            dialog.ShowLoading("");
            byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(media, 150, 150);
            MemoryStream smallimagestrm = new MemoryStream(smallimage);
            string profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/smallimage", name, smallimagestrm);
            if (imgname == "roommates")
            {
                NRIApp.Roommates.Features.Post.ViewModels.VMPost.Adimgurl(profileimgurl, name);
            }
            else if (imgname == "rentals")
            {
                NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.Adimgurl(profileimgurl, name);
            }
            smallimagestrm.Dispose();
            MemoryStream defaultimg = new MemoryStream(media);
            await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images", name, defaultimg);
            defaultimg.Dispose();

            byte[] large = DependencyService.Get<IimageResize>().ResizeImage(media, 346, 250);
            MemoryStream largeimagestrm = new MemoryStream(large);
            await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/large", name, largeimagestrm);
            largeimagestrm.Dispose();

            byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(media, 106, 80);
            MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
            await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/small", name, verysmallimgstrm);
            verysmallimgstrm.Dispose();

            byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(media, 226, 170);
            MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
            await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/thumbnail", name, thumbnailimagestrm);
            thumbnailimagestrm.Dispose();

            byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(media, 770, 577);
            MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
            await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/thumbnailfull", name, thumbnailfullimagestrm);
            thumbnailfullimagestrm.Dispose();

            byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(media, 400, 260);
            MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
            await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/thumbnailmedium", name, thumbnailmediumimagestrm);
            thumbnailmediumimagestrm.Dispose();

            byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(media, 80, 60);
            MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
            await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/thumbs", name, thumbsimagestrm);
            thumbsimagestrm.Dispose();

            dialog.HideLoading();
        }

    }
}