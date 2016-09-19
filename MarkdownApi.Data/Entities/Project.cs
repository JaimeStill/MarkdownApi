using System.Collections.Generic;

namespace MarkdownApi.Data.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
