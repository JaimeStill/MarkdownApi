using System.Collections.Generic;

namespace MarkdownApi.Web.Models
{
    public class ProjectModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public IEnumerable<DocumentModel> documents { get; set; }
    }
}