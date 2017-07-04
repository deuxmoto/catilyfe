namespace CatiLyfe.DataLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The post meta.
    /// </summary>
    public sealed class PostMeta
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostMeta"/> class.
        /// </summary>
        /// <param name="id">The id of the post.</param>
        /// <param name="slug">The slug of the post.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="description">the posts description</param>
        /// <param name="whencreated">When the post was created.</param>
        /// <param name="goeslive">When the post goes live.</param>
        /// <param name="tags">The tags.</param>
        public PostMeta(int id, string slug, string title, string description, DateTimeOffset whencreated, DateTimeOffset goeslive, IEnumerable<string> tags = null)
        {
            this.Id = id;
            this.Slug = slug;
            this.Title = title;
            this.Description = description;
            this.WhenCreated = whencreated;
            this.GoesLive = goeslive;
            this.Tags = tags ?? Enumerable.Empty<string>();
        }

        /// <summary>
        /// The posts ID.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The posts slug.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The description of the post.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// When the post was created.
        /// </summary>
        public DateTimeOffset WhenCreated { get; }

        /// <summary>
        /// When the post goes live.
        /// </summary>
        public DateTimeOffset GoesLive { get; }

        /// <summary>
        /// The tags associated with the post.
        /// </summary>
        public IEnumerable<string> Tags { get; internal set; }
    }
}
