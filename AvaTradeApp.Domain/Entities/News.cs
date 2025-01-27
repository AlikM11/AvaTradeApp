namespace AvaTradeApp.Domain.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public string ExternalId { get; set; }
        public string ArticleUrl { get; set; }
        public string Description { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime Published { get; set; }
        public List<string> Keywords { get; set; }
    }
}
