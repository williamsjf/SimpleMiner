using System.Net;
using System.Net.Http;
using System.Security.Authentication;

namespace SimpleMiner.Comunication.Http
{
    public class HttpNavigatorBuilder
    {
        public HttpNavigatorBuilder()
        {
            HttpClientHandler = new HttpClientHandler();
            HttpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            HttpClientHandler.ClientCertificateOptions = ClientCertificateOption.Automatic;
            HttpClientHandler.SslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12;
            HttpClientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        protected HttpClientHandler HttpClientHandler { get; set; }

        public HttpNavigatorBuilder IgnoreDecompression()
        {
            HttpClientHandler.AutomaticDecompression = DecompressionMethods.None;
            return this;
        }

        public HttpClientHandler GetHttpHandler()
        {
            return HttpClientHandler;
        }
    }
}
