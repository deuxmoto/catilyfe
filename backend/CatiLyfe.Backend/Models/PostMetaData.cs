namespace CatiLyfe.Backend.Models
{
    using System;
    using System.Collections.Generic;

    using CatiLyfe.DataLayer.Models;

    public class PostMetaData
    {
        public PostMetaData(PostMeta meta)
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