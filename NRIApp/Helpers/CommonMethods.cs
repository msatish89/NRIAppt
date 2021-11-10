using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.Helpers
{
   public static class CommonMethods
    {
        public static async Task<string> Uploadimagestoblob(string cdn, string name, Stream stream)
        {
            string imageurl = string.Empty;
            try
            {
                string blobkey = Commonsettings.AzureBlobStorgekey;
                var account = CloudStorageAccount.Parse(blobkey);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(cdn);
                var blockBlob = container.GetBlockBlobReference($"{name}");
                await blockBlob.UploadFromStreamAsync(stream);
                imageurl = blockBlob.Uri.OriginalString;
                if (name.Contains("daycare"))
                {
                    string cdnDirectUrl = "http://cdnnrisulekhalive.blob.core.windows.net:80/";
                    string cdnConfiguredUrl = "http://usimg.sulekhalive.com/";
                    if (imageurl.Contains(cdnDirectUrl))
                    {
                        imageurl = imageurl.Replace(cdnDirectUrl, cdnConfiguredUrl);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return imageurl;
        }
    }
}
