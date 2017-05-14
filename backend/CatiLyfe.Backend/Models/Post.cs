namespace CatiLyfe.Backend.Models
{
    using System;

    public class Post
    {
        public string Slug { get; set; }

        public string Title { get; set; }

        public DateTime LiveTime { get; set; }

        public string Content { get; set; }
    }
}