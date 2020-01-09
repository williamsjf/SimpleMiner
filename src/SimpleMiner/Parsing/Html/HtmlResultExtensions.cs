using SimpleMiner.Parsing.Html;
using SimpleMiner.Parsing.Html.Results;
using HtmlAgilityPack;

namespace SimpleMiner
{
    public static class HtmlResultExtensions
    {
        public static HtmlNodeCollectionResult ExtractNodeCollection(string html, ParseBy parseBy)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var nodeCollection = htmlDocument.DocumentNode.SelectNodes(parseBy.BuildXPath());
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

        public static HtmlSingleNodeResult ExtractSingleNode(string html, ParseBy parseBy)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var singleNode = htmlDocument.DocumentNode.SelectSingleNode(parseBy.BuildXPath());
            if (singleNode == null)
            {
                return new HtmlSingleNodeResult
                {
                    Success = false,
                    HtmlNode = null
                };
            }
            else
            {
                return new HtmlSingleNodeResult
                {
                    HtmlNode = singleNode
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

        public static InnerTextCollectionResult ParseInnerTextCollection(this HtmlNodeCollectionResult htmlNodeCollection, bool ensureOnlyNotNullOrEmpty = true)
        {
            var innerTextCollection = new InnerTextCollectionResult();

            if (htmlNodeCollection.NodeCollection == null)
            {
                innerTextCollection.Success = false;
                return innerTextCollection;
            }

            foreach (var htmlNode in htmlNodeCollection.NodeCollection)
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

        public static InnerTextResult ParseInnerText(this HtmlSingleNodeResult singleNode, bool ensureOnlyNotNullOrEmpty = true)
        {
            var innerTextResult = new InnerTextResult();

            string result = singleNode.HtmlNode.GetValueFromNode();
            if (string.IsNullOrEmpty(result) && ensureOnlyNotNullOrEmpty)
            {
                innerTextResult.Success = false;
                innerTextResult.Text = string.Empty;
            }
            else
            {
                innerTextResult.Text = result;
            }

            return innerTextResult;
        }
    }
}
