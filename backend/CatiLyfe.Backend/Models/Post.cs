namespace CatiLyfe.Backend.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Post
    {
        public Post(DataLayer.Models.Post post)
        {
            this.Metadata = new PostMetaData(post.MetaData);
            this.Content = post.PostContent.Select(c => new PostContent(c));
        }

        public PostMetaData Metadata { get; }

        public IEnumerable<PostContent> Content { get; }
    }
}
