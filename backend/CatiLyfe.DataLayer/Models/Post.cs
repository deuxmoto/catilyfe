namespace CatiLyfe.DataLayer.Models
{
    using System.Collections.Generic;

    public sealed class Post
    {
        public Post(PostMeta meta, IEnumerable<PostContent> content)
        {
            this.MetaData = meta;
            this.PostContent = content;
        }

        public PostMeta MetaData { get; }

        public IEnumerable<PostContent> PostContent { get; }
    }
}
