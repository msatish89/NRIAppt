using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Helpers
{
    public interface IDeviceInfo
    {
        string GetDeviceId();
        string GetDeviceName();
        string GetDeviceModel();
        string GetDeviceOS();
        string GetDeviceOSVersion();
      //  string GetDeviceMobileno();
        string GetVersionNumber();
        string GetBuildNumber();
       

    }
    public interface IAdInterstitial
    {
        void ShowAd(string unitid);
    }

    public interface IimageResize
    {
        byte[] ResizeImage(byte[] imagedata,int width,int height);
    }

    

}
