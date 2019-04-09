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
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
        })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, (sleep) => TimeSpan.FromSeconds(5)));
    }

    public static void ConfigureHttpNavigator()
    {

    }
}
