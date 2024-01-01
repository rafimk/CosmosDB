using Microsoft.EntityFrameworkCore;

namespace CosmosDB.Data;

public static class Extensions
{
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("CosmoDb");
        var dbOptions = section.BindOptions<CosmoDbOptions>();

        services.AddDbContext<DemoContext>(options =>
            options.UseCosmos(dbOptions.Endpoint, 
                dbOptions.Key, nameof(DemoContext)));

 
        return services;
    }

    public static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => BindOptions<T>(configuration.GetSection(sectionName));

    public static T BindOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }
}