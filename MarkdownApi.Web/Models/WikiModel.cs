using System.Collections.Generic;

namespace MarkdownApi.Web.Models
{
    public class WikiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string markdown { get; set; }
        public string html { get; set; }
        public CategoryModel category { get; set; }
        public SidebarModel sidebar { get; set; }
        public IEnumerable<DocumentModel> documents { get; set; }
    }
}