using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.Detail.Models
{
   public class Comment
    {
        public string businessid { get; set; }
        public string desc { get; set; }
        public string userpid { get; set; }
        public string name { get; set; }
        public string reviewid { get; set; }
    }

    public class Resultinfo
    {
        public string resultinformation { get; set; }
    }
}
