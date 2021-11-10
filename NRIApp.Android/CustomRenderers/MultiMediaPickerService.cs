using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NRIApp.Droid.CustomRenderers;
using NRIApp.Helpers;
using Plugin.CurrentActivity;
using Plugin.Media;
using Xamarin.Forms;

[assembly: Dependency(typeof(MultiMediaPickerService))]
namespace NRIApp.Droid.CustomRenderers
{
    public class MultiMediaPickerService : IMultiMediaPickerService
    {
        IUserDialogs dialog = UserDialogs.Instance;
        int MultiPickerResultCode = 9793;
        TaskCompletionSource<IList<MediaFile>> mediaPickedTcs;

        public async Task PickPhotosAsync(string adid)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                dialog.Toast("This is not support on your device.");
                return;
            }
            else
            {
                await PickMediaAsync("image/*", "Select Images", MultiPickerResultCode, adid);
            }
        }
        async Task<IList<MediaFile>> PickMediaAsync(string type, string title, int resultCode, string adid)
        {
            mediaPickedTcs = new TaskCompletionSource<IList<MediaFile>>();
            var imageIntent= new Intent(Intent.ActionPick);
            imageIntent.SetType(type);
            imageIntent.SetAction(Intent.ActionGetContent); //select from recent images (also from apps not from gallery) (will not open chooser popview)
            imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            CrossCurrentActivity.Current.Activity.StartActivityForResult(Intent.CreateChooser(imageIntent, title), resultCode);
            Intent data = imageIntent;
            return await mediaPickedTcs.Task;
        }
    }
}