using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Helpers
{
    public interface ICameraUploading
    {
       // Func<object> func();
        Func<object> GetImage();
    }
}
