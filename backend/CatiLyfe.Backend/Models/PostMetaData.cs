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

        public int Id { get; set; }
        public string Slug { get; set; }

        public string Title { get; set; }

        public DateTime WhenPublished { get; set; }

        public string Description { get; set; }
    }
}