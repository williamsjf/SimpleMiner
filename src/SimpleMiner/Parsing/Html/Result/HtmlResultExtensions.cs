using HtmlAgilityPack;
using SimpleMiner.Parsing.Html;
using SimpleMiner.Parsing.Html.Result;

namespace SimpleMiner
{
    public static class HtmlResultExtensions
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

        public static HtmlNodeSingleCollectionResult ForEachSingleNode(this HtmlNodeCollectionResult htmlNodeCollectionResult, ParseBy parseBy, bool ensureOnlyNotNull = true)
        {
            var singleNodeCollection = new HtmlNodeSingleCollectionResult();
            foreach (var node in htmlNodeCollectionResult.NodeCollection)
            {
                var partialNode = node.SelectSingleNode(parseBy.Value);
                if (partialNode == null && ensureOnlyNotNull)
                {
                    singleNodeCollection.Success = false;
                }

                singleNodeCollection.HtmlNodes.Add(partialNode);
            }

            return singleNodeCollection;
        }

        public static InnerTextCollectionResult ParseInnerTextCollection(this HtmlNodeSingleCollectionResult singleNodeCollection, bool ensureOnlyNotNullOrEmpty = true)
        {
            var innerTextCollection = new InnerTextCollectionResult();

            foreach (var htmlNode in singleNodeCollection.HtmlNodes)
            {
                string result = htmlNode.GetValueFromNode();
                if (string.IsNullOrEmpty(result) && ensureOnlyNotNullOrEmpty)
                {
                    innerTextCollection.Success = false;
                }
                else
                {
                    innerTextCollection.TextCollection.Add(result);
                }
            }

            return innerTextCollection;
        }
    }
}
