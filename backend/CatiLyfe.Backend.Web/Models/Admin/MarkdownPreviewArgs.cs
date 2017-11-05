namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The markdown preview arguments.
    /// </summary>
    [DataContract]
    public class MarkdownPreviewArgs
    {
        /// <summary>
        /// The markdown stuff that you would like to be transformed into some snazzy HTML5!?!?!!
        /// </summary>
        [DataMember]
        public string MarkDOWN { get; set; }
    }
}