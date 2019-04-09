using Microsoft.Extensions.DependencyInjection;
using Polly;
using SimpleMiner.Navigation.Http;
using SimpleMiner.Service;
using System;
using System.Net.Http;

namespace SimpleMiner.Configuration
{
    public static class MinerConfigurationExtensions
    {
        public static void UseSimpleMiner(this IServiceCollection services)
        {
            services.AddScoped<IMinerService, MinerService>();

            services.AddHttpClient<IHttpNavigator, HttpNavigator>(client =>
            {
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            })
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
            .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));
        }

        public static void ConfigureHttpNavigator()
        {

        }
    }
}
