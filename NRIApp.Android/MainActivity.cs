using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Plugin.Permissions;

using Xamarin.Forms;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Android.Content;
using NRIApp.Helpers;
using Plugin.CurrentActivity;
using System.IO;
using Android.Database;
using Android.Provider;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Graphics;
//using XLabs.Serialization;
//using XLabs.Serialization.JsonNET;
//using XLabs.Ioc;


namespace NRIApp.Droid
{
    [Activity(Label = "Sulekha NRI ", Icon = "@drawable/sulekhalogoh", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static int photocount;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            UserDialogs.Init(this);
            ToastConfig.DefaultPosition = ToastPosition.Top;
            
            //ToastConfig.DefaultBackgroundColor = System.Drawing.Color.LightGray;
            //ToastConfig.DefaultActionTextColor = System.Drawing.Color.White;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            // Presenters Initialization
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);

            // User-Agent tweaks for Embedded WebViews
            global::Xamarin.Auth.WebViewConfiguration.Android.UserAgent = "moljac++";
            //global::Xamarin.Auth.WebViewConfiguration.Android.UserAgent = "test";

            // Xamarin.Auth CustomTabs Initialization/Customisation


            global::Android.Graphics.Color color_xamarin_blue;
            color_xamarin_blue = new global::Android.Graphics.Color(0x34, 0x98, 0xdb);
            global::Xamarin.Auth.CustomTabsConfiguration.ToolbarColor = color_xamarin_blue;


            // ActivityFlags for tweaking closing of CustomTabs
            // please report findings!
            global::Xamarin.Auth.CustomTabsConfiguration.
               ActivityFlags =
                    global::Android.Content.ActivityFlags.NoHistory
                    |
                    global::Android.Content.ActivityFlags.SingleTop
                    |
                    global::Android.Content.ActivityFlags.NewTask
                    ;

            global::Xamarin.Auth.CustomTabsConfiguration.IsWarmUpUsed = true;
            global::Xamarin.Auth.CustomTabsConfiguration.IsPrefetchUsed = true;

            //to enable scrolling when keypad opened
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);
            AndroidBug5497WorkaroundForXamarinAndroid.assistActivity(this);
            #region For screen Height & Width  
            var pixels = Resources.DisplayMetrics.WidthPixels;
            var scale = Resources.DisplayMetrics.Density;
            var dps = (double)((pixels - 0.5f) / scale);
            var ScreenWidth = (int)dps;
            App.screenWidth = ScreenWidth;
            pixels = Resources.DisplayMetrics.HeightPixels;
            dps = (double)((pixels - 0.5f) / scale);
            var ScreenHeight = (int)dps;
            App.screenHeight = ScreenHeight;

            #endregion
          //  var resolverContainer = new XLabs.Ioc.SimpleContainer();
           // resolverContainer.Register<IJsonSerializer, JsonSerializer>();
           // Resolver.SetResolver(resolverContainer.GetResolver());
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override async void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                var currentpg = GetCurrentPage();
                await currentpg.Navigation.PopAllPopupAsync();
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
                // var currentpg = GetCurrentPage();
                //await currentpg.Navigation.PopToRootAsync();
            }
        }
        //IUserDialogs dialog = UserDialogs.Instance;
        //Plugin.Media.Abstractions.MediaFile _mediafile;
        public class AndroidBug5497WorkaroundForXamarinAndroid
        {
            public static void assistActivity(Activity activity)
            {
                new AndroidBug5497WorkaroundForXamarinAndroid(activity);
            }
            private Android.Views.View mChildOfContent;
            private int usableHeightPrevious;
            private FrameLayout.LayoutParams frameLayoutParams;

            private AndroidBug5497WorkaroundForXamarinAndroid(Activity activity)
            {
                FrameLayout content = (FrameLayout)activity.FindViewById(Android.Resource.Id.Content);
                mChildOfContent = content.GetChildAt(0);
                ViewTreeObserver vto = mChildOfContent.ViewTreeObserver;
                vto.GlobalLayout += (object sender, EventArgs e) => {
                    possiblyResizeChildOfContent();
                };
                frameLayoutParams = (FrameLayout.LayoutParams)mChildOfContent.LayoutParameters;
            }
            private void possiblyResizeChildOfContent()
            {
                int usableHeightNow = computeUsableHeight();
                if (usableHeightNow != usableHeightPrevious)
                {
                    int usableHeightSansKeyboard = mChildOfContent.RootView.Height;
                    int heightDifference = usableHeightSansKeyboard - usableHeightNow;

                    frameLayoutParams.Height = usableHeightSansKeyboard - heightDifference;

                    mChildOfContent.RequestLayout();
                    usableHeightPrevious = usableHeightNow;
                }
            }
            public static int bottomheight;
            private int computeUsableHeight()
            {
                Rect r = new Rect();
                mChildOfContent.GetWindowVisibleDisplayFrame(r);
                bottomheight = r.Bottom - r.Top;
                return (r.Bottom - r.Top);
            }
        }
        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            Android.App.ProgressDialog progressDialog;
           
            progressDialog = new Android.App.ProgressDialog(this);
            progressDialog.Show();
            progressDialog.SetMessage("Uploading!");
            try
            {
               
                base.OnActivityResult(requestCode, resultCode, data);
                if (resultCode == Android.App.Result.Ok)
                {
                    byte[] media = null;
                    if (data != null)
                    {
                        ClipData clipData = data.ClipData;
                    
                        if (clipData != null)
                        {
                            for (int i = 0; i < clipData.ItemCount; i++)
                            {
                                var name = "";
                                ClipData.Item item = clipData.GetItemAt(i);
                                Android.Net.Uri uri = item.Uri;

                                media = CreateMediaFileFromUri(uri);
                               // Commonsettings.CRMCategory = "Roommates";
                                //Roommates.Features.Post.ViewModels.VMPost.postadid_RM = 9923650;
                                if (Commonsettings.CategoryType == "Roommates" && !string.IsNullOrEmpty(NRIApp.Roommates.Features.Post.ViewModels.VMPost.postadid_RM.ToString()))
                                {
                                    name = "roommates" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Roommates.Features.Post.ViewModels.VMPost.postadid_RM + ".jpeg";
                                    if (media != null)
                                    {
                                        uploadtoserver(media, name, "roommates");
                                    }

                                }
                                else if (Commonsettings.CategoryType == "Rentals" && !string.IsNullOrEmpty(NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.postadid_RT.ToString()))
                                {
                                    name = "rentals" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.postadid_RT + ".jpeg";
                                    if (media != null)
                                    {
                                        uploadtoserver(media, name, "rentals");
                                    }
                                }
                                else if (Commonsettings.CategoryType == "Daycare" && !string.IsNullOrEmpty(NRIApp.DayCare.Features.Post.ViewModels.PostSecond_VM.Adid_DC))
                                {
                                    name = "daycare" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.DayCare.Features.Post.ViewModels.PostSecond_VM.Adid_DC + ".jpeg";
                                    if (media != null)
                                    {
                                        uploadtoserver(media, name, "daycare");
                                    }
                                }
                            }
                        }
                        else
                        {
                            var name = "";
                            Android.Net.Uri newuri = Android.Net.Uri.Parse(data.DataString);
                            media = CreateMediaFileFromUri(newuri);
                            
                            if (Commonsettings.CategoryType == "Roommates" && !string.IsNullOrEmpty(NRIApp.Roommates.Features.Post.ViewModels.VMPost.postadid_RM.ToString()))
                            {
                                name = "roommates" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Roommates.Features.Post.ViewModels.VMPost.postadid_RM + ".jpeg";
                                if (media != null)
                                {
                                    uploadtoserver(media, name, "roommates");

                                }
                            }
                            else if (Commonsettings.CategoryType == "Rentals" && !string.IsNullOrEmpty(NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.postadid_RT.ToString()))
                            {
                                name = "rentals" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.postadid_RT + ".jpeg";
                                if (media != null)
                                {
                                    uploadtoserver(media, name, "rentals");
                                }
                            }
                            else if (Commonsettings.CategoryType == "Daycare" && !string.IsNullOrEmpty(NRIApp.DayCare.Features.Post.ViewModels.PostSecond_VM.Adid_DC))
                            {
                                name = "daycare" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + NRIApp.DayCare.Features.Post.ViewModels.PostSecond_VM.Adid_DC + ".jpeg";
                                if (media != null)
                                {
                                    uploadtoserver(media, name, "daycare");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                progressDialog.Hide();
                Toast.MakeText(CrossCurrentActivity.Current.Activity, "Upload failed...try again", ToastLength.Long).Show();
            }
            progressDialog.Hide();
           
        }
        public static string profileimgurl;
        public async void uploadtoserver(byte[] media,string name,string imgname)
        {
             Android.App.ProgressDialog progressDialog;
            progressDialog = new Android.App.ProgressDialog(this);
            try
            {
               
                
                progressDialog.Show();
                progressDialog.SetMessage("Uploading!");
                //smallimage image
                byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(media, 150, 150);
                MemoryStream smallimagestrm = new MemoryStream(smallimage);
                smallimagestrm.Position = 0;
                byte[] thumbnailimaged = DependencyService.Get<IimageResize>().ResizeImage(media, 226, 170);
                MemoryStream thumbnailimagestrmd = new MemoryStream(thumbnailimaged);
                thumbnailimagestrmd.Position = 0;
                if (imgname== "daycare")
                {
                   
                    profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/thumbnail", name, thumbnailimagestrmd);
                }
                else
                {
                   
                    profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/smallimage", name, smallimagestrm);
                }
                
                if(imgname=="roommates")
                {
                    NRIApp.Roommates.Features.Post.ViewModels.VMPost.Adimgurl(profileimgurl,name);
                }
                else if(imgname=="rentals")
                {
                    NRIApp.Rentals.Features.Post.ViewModels.VMRTPost.Adimgurl(profileimgurl,name);
                }
                else if (imgname == "daycare")
                {
                    NRIApp.DayCare.Features.Post.ViewModels.PostSecond_VM.Adimgurl(profileimgurl, name);
                    thumbnailimagestrmd.Dispose();
                }
                if(imgname!="daycare")
                {
                    smallimagestrm.Dispose();
                }
              
                
                //originalimages
                MemoryStream originalsizestrm = new MemoryStream(media);
                originalsizestrm.Position = 0;
                await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images", name, originalsizestrm);
                originalsizestrm.Dispose();

                //large image
                byte[] large = DependencyService.Get<IimageResize>().ResizeImage(media, 346, 250);
                MemoryStream largeimagestrm = new MemoryStream(large);
                largeimagestrm.Position = 0;
                await CommonMethods.Uploadimagestoblob("cdn/"+imgname+"/images/large", name, largeimagestrm);
                largeimagestrm.Dispose();

                //small image
                byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(media, 106, 80);
                MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                verysmallimgstrm.Position = 0;
                
                await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/small", name, verysmallimgstrm);
                verysmallimgstrm.Dispose();

                //thumbnail image
                byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(media, 226, 170);
                MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                thumbnailimagestrm.Position = 0;
                if (imgname == "daycare")
                {
                    await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/smallimage", name, smallimagestrm);
                    smallimagestrm.Dispose();
                }
                else
                {
                    await CommonMethods.Uploadimagestoblob("cdn/" + imgname + "/images/thumbnail", name, thumbnailimagestrm);
                    thumbnailimagestrm.Dispose();
                }
              //   await CommonMethods.Uploadimagestoblob("cdn/"+imgname+"/images/thumbnail", name, thumbnailimagestrm);
                

                //thumbnailfull image
                byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(media, 770, 577);
                MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                thumbnailfullimagestrm.Position = 0;
                await CommonMethods.Uploadimagestoblob("cdn/"+imgname+"/images/thumbnailfull", name, thumbnailfullimagestrm);
                thumbnailfullimagestrm.Dispose();

                //thumbnailmedium image
                byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(media, 400, 260);
                MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                thumbnailmediumimagestrm.Position = 0;
                await CommonMethods.Uploadimagestoblob("cdn/"+imgname+"/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                thumbnailmediumimagestrm.Dispose();

                //thumbs image
                byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(media, 80, 60);
                MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                thumbsimagestrm.Position = 0;
                await CommonMethods.Uploadimagestoblob("cdn/"+imgname+"/images/thumbs", name, thumbsimagestrm);
                thumbsimagestrm.Dispose();
              
                progressDialog.Hide();
               // NRIApp.Roommates.Features.Post.ViewModels.VMPost.AdmultiImage.Add(profileimgurl);
                
            }
            catch (Exception ex)
            {
                progressDialog.Hide();
                Toast.MakeText(CrossCurrentActivity.Current.Activity,"Upload failed...try again", ToastLength.Long).Show();
            }
        }
      
        public byte[] CreateMediaFileFromUri(Android.Net.Uri uri)
        {
           
                Stream stream = ContentResolver.OpenInputStream(uri);
                byte[] byteArray;

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    byteArray = memoryStream.ToArray();
                }
                return byteArray;
        
        }
      
        public static string GetRealPathFromURI(Android.Net.Uri contentURI)
        {
            ICursor cursor = null;
            try
            {
                string mediaPath = string.Empty;
                cursor = CrossCurrentActivity.Current.Activity.ContentResolver.Query(contentURI, null, null, null, null);
                cursor.MoveToFirst();
                int idx = cursor.GetColumnIndex(MediaStore.MediaColumns.Data);

                if (idx != -1)
                {
                    var type = CrossCurrentActivity.Current.Activity.ContentResolver.GetType(contentURI);
                    int pIdx = cursor.GetColumnIndex(MediaStore.MediaColumns.Id);
                    var mData = cursor.GetString(idx);
                    mediaPath = mData;
                }
                else
                {
                    var docID = DocumentsContract.GetDocumentId(contentURI);
                    var doc = docID.Split(':');
                    var id = doc[1];
                    var whereSelect = MediaStore.Images.ImageColumns.Id + "=?";
                    var dataConst = MediaStore.Images.ImageColumns.Data;
                    var projections = new string[] { dataConst };
                    var internalUri = MediaStore.Images.Media.InternalContentUri;
                    var externalUri = MediaStore.Images.Media.ExternalContentUri;
                    switch (doc[0])
                    {
                        case "image":
                            whereSelect = MediaStore.Video.VideoColumns.Id + "=?";
                            projections = new string[] { MediaStore.Video.VideoColumns.Data };
                            break;
                    }

                    projections = new string[] { dataConst };
                    cursor = CrossCurrentActivity.Current.Activity.ContentResolver.Query(internalUri, projections, whereSelect, new string[] { id }, null);
                    if (cursor.Count == 0)
                    {
                        cursor = CrossCurrentActivity.Current.Activity.ContentResolver.Query(externalUri, projections, whereSelect, new string[] { id }, null);
                    }
                    var colDatax = cursor.GetColumnIndexOrThrow(dataConst);
                    cursor.MoveToFirst();

                    mediaPath = cursor.GetString(colDatax);
                }
                return mediaPath;
            }
            catch (Exception)
            {
                Toast.MakeText(CrossCurrentActivity.Current.Activity, "Unable to get path", ToastLength.Long).Show();
            }
            finally
            {
                if (cursor != null)
                {
                    cursor.Close();
                    cursor.Dispose();
                }
            }
            return null;
        }
        private Page GetCurrentPage()
        {
            var currentpage = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}

