using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.Models
{
    public class Post
    {
        public string Slug { get; set; }

        public string Title { get; set; }

        public DateTime LiveTime { get; set; }

        public string Content { get; set; }
    }
}
