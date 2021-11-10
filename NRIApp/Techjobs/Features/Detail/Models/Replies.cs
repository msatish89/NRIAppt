using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.Detail.Models
{
   public class Replies
    {
        public string contributor { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class GetReplies
    {
        public List<Replies> ROW_DATA { get; set; }
    }
}
