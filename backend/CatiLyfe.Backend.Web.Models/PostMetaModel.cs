namespace CatiLyfe.Backend.Web.Models
{
    using System;
    using System.Collections.Generic;

    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// The post meta model.
    /// </summary>
    public class PostMetaModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostMetaModel"/> class.
        /// </summary>
        /// <param name="meta">The meta.</param>
        public PostMetaModel(PostMeta meta)
        {
            this.Id = meta.Id;
            this.Slug = meta.Slug;
            this.Title = meta.Title;
            this.Description = meta.Description;
            this.WhenPublished = meta.GoesLive;
            this.Tags = meta.Tags;
        }

        public int Id { get; }
        public string Slug { get; }

        public string Title { get; }

        public DateTimeOffset WhenPublished { get; }

        public string Description { get; }

        public IEnumerable<string> Tags { get; }
    }
}