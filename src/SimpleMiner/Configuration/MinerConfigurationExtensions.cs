using Microsoft.Extensions.DependencyInjection;
using Polly;
using SimpleMiner;
using SimpleMiner.Navigation;
using SimpleMiner.Navigation.Http;
using SimpleMiner.Parsing;
using SimpleMiner.Parsing.Html;
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
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            client.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Origin", "https://www3.tjrj.jus.br");
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
            client.BaseAddress = new Uri("https://www3.tjrj.jus.br");
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            AllowAutoRedirect = true,
        })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, (sleep) => TimeSpan.FromSeconds(5)));
    }

    public static IHttpClientBuilder UseSimpleMinerHttpNavigator<THttpNavigator>(
        this IServiceCollection serviceCollection,
        Action<HttpNavigatorSettingsBuilder> action)
        where THttpNavigator : class, IHttpNavigator
    {
        HttpNavigatorSettingsBuilder builder = default(HttpNavigatorSettingsBuilder);
        action(builder);

       return serviceCollection.AddHttpClient<IHttpNavigator, THttpNavigator>();
    }
}