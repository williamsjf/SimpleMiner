using Microsoft.Extensions.DependencyInjection;
using SimpleMiner;
using SimpleMiner.Navigation.Http;
using SimpleMiner.Service;
using System;

public static class MinerConfigurationExtensions
{
    public static void UseSimpleMiner(this IServiceCollection services)
    {
        services.AddScoped<IMinerService, MinerService>();
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