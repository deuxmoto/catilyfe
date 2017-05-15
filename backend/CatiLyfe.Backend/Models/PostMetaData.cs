namespace CatiLyfe.Backend.Models
{
    using System;

    public class PostMetaData
    {
        public int Id { get; set; }
        public string Slug { get; set; }

        public string Title { get; set; }

        public DateTime WhenPublished { get; set; }

        public string Description { get; set; }
    }
}