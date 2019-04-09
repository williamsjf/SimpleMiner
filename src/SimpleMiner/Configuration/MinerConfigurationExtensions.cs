using Microsoft.Extensions.DependencyInjection;
using Polly;
using SimpleMiner.Navigation;
using SimpleMiner.Navigation.Http;
using SimpleMiner.Service;
using System;
using System.Net.Http;

public static class MinerConfigurationExtensions
{
    public static void UseSimpleMiner(this IServiceCollection services)
    {
        services.AddScoped<IMinerService, MinerService>();

        services.AddHttpClient<INavigator, HttpNavigator>(client =>
        {
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            client.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");
        })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, (sleep) => TimeSpan.FromSeconds(5)));
    }

    public static void ConfigureHttpNavigator()
    {

    }
}
