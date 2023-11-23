using System.Reflection;
using Infrastructure.Consumers;
using Infrastructure.Settings;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    private const string DefaultConnection = nameof(DefaultConnection);

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddDbContext(config)
            .AddRabbitMq(config)
            .AddMediatR(Assembly.GetExecutingAssembly())
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
        => services.AddDbContext<ReactivitiesContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString(DefaultConnection));
        });

    private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration config)
        => services.AddMassTransit(x =>
        {
            x.AddConsumer<SendMessageConsumer>();
            x.AddConsumer<GetConversationConsumer>();

            var mqConfig = config
                .GetSection(nameof(RabbitMqConfiguration))
                .Get<RabbitMqConfiguration>();

            x.UsingRabbitMq((cont, cfg) =>
            {
                cfg.Host(mqConfig.ConnectionString);

                cfg.ReceiveEndpoint(mqConfig.SendMessageQueueName, e =>
                {
                    e.Bind(mqConfig.MessagingExchangeName);
                    e.UseRawJsonDeserializer(isDefault: true);
                    e.Consumer<SendMessageConsumer>(cont);
                });
                
                cfg.ReceiveEndpoint(mqConfig.GetConversationQueueName, e =>
                {
                    e.Bind(mqConfig.GetConversationQueueName);
                    e.UseRawJsonDeserializer(isDefault: true);
                    e.Consumer<GetConversationConsumer>(cont);
                });
            }); 
        });
}