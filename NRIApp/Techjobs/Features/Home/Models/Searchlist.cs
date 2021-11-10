using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.Home.Models
{
    public class Searchlist
    {
        public List<SearchTags> ROW_DS { get; set; }
    }

    public class SearchTags
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string resulttype { get; set; }
        public string header { get; set; }
        public string tags { get; set; }
        public string title { get; set; }
    }
}
