using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Helpers
{
    public enum MediaFileType
    {
        Image
    }

    public class MediaFile
    {
        public string PreviewPath { get; set; }
        public string Path { get; set; }
        public MediaFileType Type { get; set; }
    }
}
