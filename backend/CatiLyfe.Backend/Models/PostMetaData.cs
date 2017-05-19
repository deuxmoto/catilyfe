namespace CatiLyfe.Backend.Models
{
    using System;

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
        }

        public int Id { get; }
        public string Slug { get; }

        public string Title { get; }

        public DateTime WhenPublished { get; }

        public string Description { get; }
    }
}