namespace CatiLyfe.Backend.Models
{
    public class PostContent
    {
        public PostContent(DataLayer.Models.PostContent content)
        {
            this.Index = content.Index;
            this.ContentType = content.ContentType;
            this.Content = content.Content;
        }

        public int Index { get; }

        public string Content { get; }

        public string ContentType { get; }
    }
}
