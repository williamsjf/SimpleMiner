using System.Collections.Generic;

namespace SimpleMiner.Comunication.Http
{
    public class RequestOptions
    {
        public RequestOptions()
        {
            Headers = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Headers { get; set; }
    }

    public class RequestOptionsBuilder
    {
        public RequestOptionsBuilder()
        {
            Options = new RequestOptions();
        }

        public RequestOptions Options { get; private set; }

        public RequestOptionsBuilder AddHeader(string key, string value)
        {
            Options.Headers.Add(key, value);
            return this;
        }

        public RequestOptionsBuilder AddHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                Options.Headers.Add(header.Key, header.Value);
            }

            return this;
        }
    }
}
