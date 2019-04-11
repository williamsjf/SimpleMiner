using SimpleMiner.Parsing.Html.Result;

namespace SimpleMiner.Parsing.Html
{
    public interface IHtmlParser : IParser
    {
        string Html { get; }

        HtmlNodeCollectionResult FromNodeCollection(ParseBy parseBy);
    }
}
