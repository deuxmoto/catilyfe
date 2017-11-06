namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        /// <param name="id">The id of the post to change</param>
        /// <param name="title">The post title.</param>
        /// <param name="whenPublished">The live time.</param>
        /// <param name="whenCreated">When created.</param>
        /// <param name="slug">The post slug.</param>
        /// <param name="description">The post description.</param>
        /// <param name="tags">The tags.</param>
        public AdminMetaData(int? id, string title, DateTimeOffset whenPublished, DateTimeOffset whenCreated, string slug, string description, IEnumerable<string> tags)
        {
            this.Id = id;
            this.Title = title;
            this.WhenPublished = whenPublished;
            this.WhenCreated = whenCreated;
            this.Slug = slug;
            this.Description = description;
            this.Tags = tags;
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
    }
}
