using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.Models
{
    public class Post
    {
        public Post(DataLayer.Models.Post post)
        {
            this.Metadata = new PostMetaData(post.MetaData);
            this.Content = post.PostContent.Select(c => new PostContent(c));
        }

        public PostMetaData Metadata { get; set; }

        public IEnumerable<PostContent> Content { get; set; }
    }
}
