namespace CatiLyfe.Backend.Web.Core.Code
{
    using HeyRed.MarkdownSharp;

    /// <summary>
    /// The markdown processor.
    /// </summary>
    public class MarkdownProcessor : IContentTransformer
    {
        private readonly Markdown markdown;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownProcessor"/> class.
        /// </summary>
        public MarkdownProcessor()
        {
            this.markdown = new Markdown(new MarkdownOptions
                                             {
                                                 
                                             });
        }

        /// <summary>
        /// The transform markdown.
        /// </summary>
        /// <param name="markdown">The markdown.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string TransformMarkdown(string markdown)
        {
            return this.markdown.Transform(markdown);
        }
    }
}
