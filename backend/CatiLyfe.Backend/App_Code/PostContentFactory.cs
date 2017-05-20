namespace CatiLyfe.Backend.App_Code
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CatiLyfe.Backend.Models;

    using HeyRed.MarkdownSharp;

    /// <summary>
    /// The post content factory.
    /// </summary>
    public static class PostContentFactory
    {
        /// <summary>
        /// Builds a new post content object.
        /// </summary>
        /// <param name="contents">The contents of the content.</param>
        /// <returns>The post content.</returns>
        public static PostContent Build(IEnumerable<CatiLyfe.DataLayer.Models.PostContent> contents)
        {
            var postResultContent = new StringBuilder();
            foreach (var content in contents.OrderBy(c => c.Index))
            {
                postResultContent.AppendLine(
                    PostContentFactory.Resolve(content));
            }

            return new PostContent(postResultContent.ToString());
        }

        /// <summary>
        /// Resolve the content type and return the result.
        /// </summary>
        /// <param name="content">The content type.</param>
        /// <returns>The string content</returns>
        private static string Resolve(DataLayer.Models.PostContent content)
        {
            switch (content.ContentType.ToLowerInvariant())
            {
                case "markdown":
                    return PostContentFactory.ParseMarkdown(content);
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Parses the markdown content type.
        /// </summary>
        /// <param name="content">The parsed content type.</param>
        /// <returns>The html string.</returns>
        private static string ParseMarkdown(DataLayer.Models.PostContent content)
        {
            var options = new MarkdownOptions
            {
                AutoHyperlink = true,
                AutoNewLines = true,
                LinkEmails = true,
                QuoteSingleLine = true,
                StrictBoldItalic = true,
            };

            var markdown = new Markdown(options);
            return markdown.Transform(content.Content);
        }
    }
}
