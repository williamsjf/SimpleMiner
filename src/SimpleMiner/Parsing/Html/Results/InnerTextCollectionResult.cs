using System.Collections.Generic;

namespace SimpleMiner.Parsing.Html.Results
{
    public class InnerTextCollectionResult : HtmlParseResult
    {
        public InnerTextCollectionResult()
        {
            TextCollection = new HashSet<string>();
        }

        public ICollection<string> TextCollection { get; set; }
    }
}
