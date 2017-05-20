namespace CatiLyfe.DataLayer.Models
{
    using System;

    /// <summary>
    /// The post meta.
    /// </summary>
    public sealed class PostMeta
    {
        public PostMeta(int id, string slug, string title, string description, DateTimeOffset whencreated, DateTimeOffset goeslive)
        {
            this.Id = id;
            this.Slug = slug;
            this.Title = title;
            this.Description = description;
            this.WhenCreated = whencreated;
            this.GoesLive = goeslive;
        }

        public int Id { get; }

        public string Slug { get; }

        public string Title { get; }

        public string Description { get; }

        public DateTimeOffset WhenCreated { get; }

        public DateTimeOffset GoesLive { get; }
    }
}
