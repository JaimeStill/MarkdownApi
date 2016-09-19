using System.Collections.Generic;

namespace MarkdownApi.Data.Entities
{
    public class Wiki
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Markdown { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
