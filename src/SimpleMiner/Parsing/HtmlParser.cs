using SimpleMiner.Parsing.Html;
using SimpleMiner.Parsing.Html.Results;

namespace SimpleMiner.Parsing
{
    public class HtmlParser : IParser
    {
        public string Html { get; private set; }

        public void Load(object content)
        {
            if (!(content is string))
            {
                //throw new UnsupportedContentException(
                //    $"The content type '{content.GetType().Name}' is not supported for the HtmlParser.");
            }

            Html = content.ToString();
        }

        public HtmlNodeCollectionResult FromNodeCollection(ParseBy parseBy)
        {
            return HtmlResultExtensions.ExtractNodeCollection(Html, parseBy);
        }

        public HtmlNodeCollectionResult FromNodeCollection(string xpath)
        {
            return HtmlResultExtensions.ExtractNodeCollection(Html, new ParseBy(HtmlParseByType.Xpath, xpath));
        }

        public HtmlSingleNodeResult FromSingleNode(ParseBy parseBy)
        {
            return HtmlResultExtensions.ExtractSingleNode(Html, parseBy);
        }

        public HtmlSingleNodeResult FromSingleNode(string xpath)
        {
            return HtmlResultExtensions.ExtractSingleNode(Html, new ParseBy(HtmlParseByType.Xpath, xpath));
        }
    }
}
