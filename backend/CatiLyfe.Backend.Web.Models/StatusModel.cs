namespace CatiLyfe.Backend.Web.Models
{
    using System;

    /// <summary>
    /// The test model.
    /// </summary>
    public class StatusModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusModel"/> class.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="description">The description.</param>
        public StatusModel(DateTime timestamp, string description)
        {
            this.Timestamp = timestamp;
            this.Description = description;
        }

        /// <summary>
        /// Gets the current server time in UTC.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Yea, IDK. Just a string whatever.
        /// </summary>
        public string Description { get; }
    }
}
