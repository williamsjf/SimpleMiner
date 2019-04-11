using System.Collections.Generic;

namespace SimpleMiner.Parsing.Html.Result
{
    public class InnerTextCollectionResult : HtmlResult
    {
        public InnerTextCollectionResult()
        {
            TextCollection = new HashSet<string>();
        }

        public ICollection<string> TextCollection { get; set; }
    }
}
