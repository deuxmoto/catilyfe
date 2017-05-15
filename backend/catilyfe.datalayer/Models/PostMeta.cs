using System;
using System.Collections.Generic;
using System.Text;

namespace catilyfe.datalayer.Models
{
    /// <summary>
    /// The post meta.
    /// </summary>
    public sealed class PostMeta
    {
        public PostMeta(int id, string slug, string title, string description, DateTime whencreated, DateTime goeslive)
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

        public DateTime WhenCreated { get; }

        public DateTime GoesLive { get; }
    }
}
