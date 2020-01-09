using HtmlAgilityPack;
using System.Collections.Generic;

namespace SimpleMiner.Parsing.Html.Results
{
    public class HtmlNodeSingleCollectionResult : HtmlParseResult
    {
        public HtmlNodeSingleCollectionResult()
        {
            HtmlNodes = new HashSet<HtmlNode>();
        }

        public ICollection<HtmlNode> HtmlNodes { get; set; }
    }
}
