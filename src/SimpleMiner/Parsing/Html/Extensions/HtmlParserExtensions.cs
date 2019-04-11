using HtmlAgilityPack;
using SimpleMiner.Parsing.Html.Result;

namespace SimpleMiner.Parsing.Html.Extensions
{
    public static class HtmlParserExtensions
    {
        public static HtmlNodeCollectionResult ExtractNodeCollection(string html, ParseBy parseBy)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var nodeCollection = htmlDocument.DocumentNode.SelectNodes(parseBy.Value);
            if (nodeCollection == null)
            {
                return new HtmlNodeCollectionResult
                {
                    Success = false,
                    NodeCollection = null
                };
            }
            else
            {
                return new HtmlNodeCollectionResult
                {
                    NodeCollection = nodeCollection
                };
            }
        }
    }
}
