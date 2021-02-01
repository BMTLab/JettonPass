using System;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace JettonPass.App.Services.Configuration.Extensions
{
    public static class ConfigurationOptionsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddSectionOptions<T>
            (this IServiceCollection services, [NotNull] IConfiguration configuration) where T : class, new()
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            return services.Configure<T>(configuration.GetSection(typeof(T).Name));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetFrom<T>([NotNull] this IConfiguration configuration) where T : class, new()
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            return configuration.GetSection(typeof(T).Name).Get<T>();
        }
    }
}