namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    /// <summary>
    /// The markdown preview result object.
    /// </summary>
    [DataContract]
    public class MarkdownPreview
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownPreview"/> object.
        /// </summary>
        /// <param name="content"></param>
        public MarkdownPreview(string content)
        {
            this.Content = content;
        }

        /// <summary>
        /// The html content.
        /// </summary>
        [AllowHtml]
        [DataMember]
        public string Content { get; private set; }
    }
}