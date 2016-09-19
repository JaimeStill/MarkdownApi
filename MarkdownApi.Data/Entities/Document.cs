namespace MarkdownApi.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }

        public Project Project { get; set; }
    }
}
