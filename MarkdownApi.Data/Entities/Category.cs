using System.Collections.Generic;

namespace MarkdownApi.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Wiki> Wikis { get; set; }
    }
}
