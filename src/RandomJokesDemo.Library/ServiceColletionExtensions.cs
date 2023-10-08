using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RandomJokesDemo.Library.Clients;
using RandomJokesDemo.Library.Services;
using RandomJokesDemo.Library.StorageProvider;

[assembly: InternalsVisibleTo("RandomJokesDemo.UnitTests")]

namespace RandomJokesDemo.Library;

public static class ServiceColletionExtensions
{
    public static IServiceCollection AddApplication(IServiceCollection services)
    {
        services.AddHttpClients();
        services.AddServices();
        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IRandomJokesClient, RandomJokesClient>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IStorageProvider, BlobTableStorageProvider>();
        services.AddTransient<IJokeService, JokeService>();

        services.AddTransient<ISentementService, SentementService>();

        services.AddOptions<OpenAiOptions>()
            .Configure<IConfiguration>((o, c) =>
        {
            c.Bind(nameof(OpenAiOptions), o);
        });

        return services;
    }
}