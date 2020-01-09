using HtmlAgilityPack;

namespace SimpleMiner.Parsing.Html.Results
{
    public class HtmlNodeCollectionResult : HtmlParseResult
    {
        public HtmlNodeCollection NodeCollection { get; set; }
    }
}
