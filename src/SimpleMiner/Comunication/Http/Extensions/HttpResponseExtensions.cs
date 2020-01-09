using SimpleMiner.Comunication.Http;
using SimpleMiner.Parsing;

namespace SimpleMiner
{
    public static class HttpResponseExtensions
    {
        public static HtmlParser Parse(this HttpResponse<string> httpResponse) 
        {
            var parser = new HtmlParser();
            parser.Load(httpResponse.Content);

            return parser;
        }
    }
}
