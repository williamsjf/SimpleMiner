using System.Collections.Generic;

namespace SimpleMiner.Parsing.Html.Results
{
    public class HtmlParseResult
    {
        public HtmlParseResult()
        {
            Success = true;
        }

        public bool Success { get; set; }
    }

    public class HtmlModelParser<TModel> : HtmlParseResult
    {
        public TModel Model { get; set; }
    }

    public class HtmlModelParserColletion<TModel> : HtmlParseResult
    {
        public HtmlModelParserColletion()
        {
            Models = new List<TModel>();
        }

        public List<TModel> Models { get; set; }
    }
}
