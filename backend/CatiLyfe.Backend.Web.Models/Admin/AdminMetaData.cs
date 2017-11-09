namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// The admin meta data.
    /// </summary>
    public class AdminMetaData
    {
        public AdminMetaData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminMetaData"/> class.
        /// </summary>
        /// <param name="meta">The meta.</param>
        public AdminMetaData(PostMeta meta)
        {
            this.Id = meta.Id;
            this.Title = meta.Title;
            this.WhenCreated = meta.WhenCreated;
            this.WhenPublished = meta.GoesLive;
            this.Slug = meta.Slug;
            this.Description = meta.Description;
            this.Tags = meta.Tags;
        }

        /// <summary>
        /// The posts id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// The post title.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// When the post is going live.
        /// </summary>
        [Required]
        public DateTimeOffset WhenPublished { get; set; }


        /// <summary>
        /// When the post was created.
        /// </summary>
        public DateTimeOffset WhenCreated { get; set; }

        /// <summary>
        /// The posts slug.
        /// </summary>
        [Required]
        [MaxLength(128, ErrorMessage = "Slug must not be longer than 128.")]
        public string Slug { get; set; }

        /// <summary>
        /// The post description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// The tags for the post.
        /// </summary>
        [Required]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Convert this object to a post meta.
        /// </summary>
        /// <returns>The post meta.</returns>
        public PostMeta ToPostMeta()
        {
            return new PostMeta(
                this.Id ?? -1,
                this.Slug,
                this.Title,
                this.Description,
                this.WhenCreated,
                this.WhenPublished,
                this.Tags ?? Enumerable.Empty<string>());
        }
    }
}
