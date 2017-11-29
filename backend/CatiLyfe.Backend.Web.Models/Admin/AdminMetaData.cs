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
            this.IsReserved = meta.IsReserved;
            this.IsPublished = meta.IsPublished;
            this.IsDeleted = meta.IsDeleted;
            this.Tags = meta.Tags;
            this.History = meta.History.Select(h => new AdminAuditHistory(h)).ToList();
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
        /// A value indicating whether the post is reserved.
        /// </summary>
        public bool IsReserved { get; set; }

        /// <summary>
        /// Gets if the post is published.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Gets if the post is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// The tags for the post.
        /// </summary>
        [Required]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// The admin audit history.
        /// </summary>
        public IEnumerable<AdminAuditHistory> History { get; set; }

        /// <summary>
        /// Convert this object to a post meta.
        /// </summary>
        /// <returns>The post meta.</returns>
        public PostMeta ToPostMeta()
        {
            return new PostMeta(
                id: this.Id ?? -1,
                slug: this.Slug,
                title: this.Title,
                description: this.Description,
                whencreated: this.WhenCreated,
                goeslive: this.WhenPublished,
                isReserved: this.IsReserved,
                isPublished: this.IsPublished,
                isDeleted: this.IsDeleted,
                tags: this.Tags ?? Enumerable.Empty<string>(),
                history: this.History?.Select(h => h.ToPostAuditHistory()) ?? Enumerable.Empty<PostAuditHistory>());
        }
    }
}
