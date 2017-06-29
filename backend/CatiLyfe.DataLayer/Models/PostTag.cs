namespace CatiLyfe.DataLayer.Models
{
    /// <summary>
    /// The post tag.
    /// </summary>
    public class PostTag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTag"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="weight">The tag uses.</param>
        public PostTag(string tag, int weight)
        {
            this.Tag = tag;
            this.Weight = weight;
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        public int Weight { get; }
    }
}
