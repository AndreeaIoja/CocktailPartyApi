using Meilisearch;

namespace CocktailParty.Extensions
{
    public static class MeiliSearchExtension
    {
        public static IServiceCollection AddMeilisearch(this IServiceCollection services, IConfiguration config)
        {
            var host = config["Meili:Host"];
            var apiKey = config["Meili:MeiliSearchPassword"];

            services.AddSingleton(sp => new MeilisearchClient(host, apiKey));

            return services;
        }
    }
}
