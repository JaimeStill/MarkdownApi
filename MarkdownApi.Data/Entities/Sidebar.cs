using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownApi.Data.Entities
{
    public class Sidebar
    {
        public int Id { get; set; }
        public int WikiId { get; set; }
        public string Markdown { get; set; }

        public Wiki Wiki { get; set; }
    }
}
