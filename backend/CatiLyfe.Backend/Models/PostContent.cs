namespace CatiLyfe.Backend.Models
{
    using HeyRed.MarkdownSharp;

    public class PostContent
    {
        public PostContent(DataLayer.Models.PostContent content)
        {
            this.Index = content.Index;
            this.ContentType = content.ContentType;
            this.Content = content.Content;

            if (this.ContentType == "markdown")
            {
                var options = new MarkdownOptions
                                  {
                                      AutoHyperlink = true,
                                      AutoNewLines = true,
                                      LinkEmails = true,
                                      QuoteSingleLine = true,
                                      StrictBoldItalic = true
                                  };

                var markdown = new Markdown(options);
                this.Content = markdown.Transform(this.Content);
            }
            else
            {
                // do nothing
            }
        }

        public int Index { get; }

        public string Content { get; }

        public string ContentType { get; }
    }
}
