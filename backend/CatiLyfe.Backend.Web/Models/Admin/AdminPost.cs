namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The admin post.
    /// </summary>
    public class AdminPost
    {
        public AdminPost()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminPost"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="postContent">The content.</param>
        public AdminPost(AdminMetaData metadata, AdminPostContent postContent)
        {
            this.MetaData = metadata;
            this.PostContent = postContent;
        }

        /// <summary>
        /// The metadata.
        /// </summary>
        [Required]
        public AdminMetaData MetaData { get; set; }

        /// <summary>
        /// Gets the post content.
        /// </summary>
        [Required]
        public AdminPostContent PostContent { get; set; }
    }
}
