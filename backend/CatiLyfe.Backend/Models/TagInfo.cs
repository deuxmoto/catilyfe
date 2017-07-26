namespace CatiLyfe.Backend.Models
{
    /// <summary>
    /// The tag info.
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagInfo"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="weight">The weight.</param>
        public TagInfo(string name, int weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        /// <summary>
        /// The name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of references.
        /// </summary>
        public int Weight { get; set; }
    }
}
