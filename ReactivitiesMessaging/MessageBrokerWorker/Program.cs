using EasyNetQ;
using Infrastructure;
using MessageBrokerService;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, configBuilder) =>
    {
        configBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true,
                reloadOnChange: true)
            .Build();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;

        services
            .AddHostedService<Worker>()
            .AddSingleton(RabbitHutch.CreateBus(config.GetConnectionString("RabbitMQBus")))
            .AddDbContext<ReactivitiesContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
    })
    .Build();

await host.RunAsync();