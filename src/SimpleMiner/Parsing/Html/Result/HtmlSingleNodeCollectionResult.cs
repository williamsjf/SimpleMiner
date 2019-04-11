using HtmlAgilityPack;
using System.Collections.Generic;

namespace SimpleMiner.Parsing.Html.Result
{
    public class HtmlNodeSingleCollectionResult : HtmlResult
    {
        public HtmlNodeSingleCollectionResult()
        {
            HtmlNodes = new HashSet<HtmlNode>();
        }

        public ICollection<HtmlNode> HtmlNodes { get; set; }
    }
}
