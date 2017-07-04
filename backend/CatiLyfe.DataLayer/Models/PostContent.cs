namespace CatiLyfe.DataLayer.Models
{
    public sealed class PostContent
    {
        public PostContent(int index, int postId, string contentType, string content)
        {
            this.Index = index;
            this.PostId = postId;
            this.ContentType = contentType;
            this.Content = content;
        }

        public int Index { get; }

        public string ContentType { get; }

        public string Content { get; }

        internal int PostId { get; }
    }
}
