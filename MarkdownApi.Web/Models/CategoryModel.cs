using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkdownApi.Web.Models
{
    public class CategoryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public IEnumerable<WikiModel> wikis { get; set; }
    }
}