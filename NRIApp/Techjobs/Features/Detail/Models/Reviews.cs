using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.Detail.Models
{
   public class Reviews
    {
        public string testimonialid { get; set; }
        public string name { get; set; }
        public string aboutlocation { get; set; }
        public string rating { get; set; }
        public string userreview { get; set; }
        public string profilepicurl { get; set; }
        public string profilename { get; set; }
        public bool Profileimg { get; set; }
        public bool Noimg { get; set; }
    }

    public class Getreviews
    {
        public List<Reviews> ROW_DATA { get; set; }
    }
}
