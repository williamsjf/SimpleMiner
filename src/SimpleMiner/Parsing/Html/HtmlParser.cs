using SimpleMiner.Exceptions;
using SimpleMiner.Parsing.Html.Result;

namespace SimpleMiner.Parsing.Html
{
    public class HtmlParser : IHtmlParser
    {
        public string Html { get; private set; }

        public void LoadContent(object content)
        {
            if (!(content is string))
                throw new UnsupportedContentException(
                    $"The content type '{content.GetType().Name}' is not supported for the HtmlParser.");

            Html = content.ToString();
        }

        public HtmlNodeCollectionResult FromNodeCollection(ParseBy parseBy)
        {
            return null;
        }
    }
}
