using Core.Extensions;
using Infrastructure.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
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
            .AddApplicationServices()
            .AddInfrastructureServices(config);
    })
    .Build();

await host.RunAsync();