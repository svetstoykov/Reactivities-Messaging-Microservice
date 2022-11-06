using System.Reflection;
using EasyNetQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    private const string DefaultConnection = nameof(DefaultConnection);
    private const string RabbitMQBus = nameof(RabbitMQBus);
    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddDbContext(config)
            .AddRabbitMqMessageBus(config)
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

    private static IServiceCollection AddRabbitMqMessageBus(this IServiceCollection services, IConfiguration config)
        => services.AddSingleton(RabbitHutch.CreateBus(config.GetConnectionString(RabbitMQBus)));
}