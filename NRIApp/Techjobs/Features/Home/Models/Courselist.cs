using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NRIApp.Techjobs.Features.Home.Models
{
   public class Courselist
    {
        public List<Tags> ROW_DATA { get; set; }
    }

    public class Tags
    {
        public int supertagid { get; set; }
        public string supertag { get; set; }
        public string supertagurl { get; set; }
    }
}
