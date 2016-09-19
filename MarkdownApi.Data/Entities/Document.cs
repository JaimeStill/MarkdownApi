namespace MarkdownApi.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public int WikiId { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }

        public Wiki Wiki { get; set; }
    }
}
