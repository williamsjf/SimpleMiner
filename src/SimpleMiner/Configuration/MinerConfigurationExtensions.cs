using Microsoft.Extensions.DependencyInjection;
using Polly;
using SimpleMiner.Navigation;
using SimpleMiner.Navigation.Http;
using SimpleMiner.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public static class MinerConfigurationExtensions
{
    public static void UseSimpleMiner(this IServiceCollection services)
    {
        services.AddScoped<IMinerService, MinerService>();

        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = false
        };

        services.AddHttpClient<INavigator, HttpNavigator>(client =>
        {
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            client.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Origin", "https://www3.tjrj.jus.br");
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
             AllowAutoRedirect = true,
        })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, (sleep) => TimeSpan.FromSeconds(5)));
    }

    public static void ConfigureHttpNavigator()
    {

    }
}