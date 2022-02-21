using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    /// <summary>
    /// This method is our primary entry point. Here we build up our config, logger, and dependency injection systems. 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // Setting up configuartion for Serilog
        ConfigurationBuilder builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        // Setting up Dependency Injections, Configuration, and Logging
        IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Services go here
                services.AddSingleton<GameService>();
                services.AddSingleton<DiscordBotService>();
                services.AddSingleton<DiscordSocketClient>();
            })
            .UseSerilog()
            .Build();

        ICustomService discordBotSvc = host.Services.GetRequiredService<DiscordBotService>();
        ICustomService gameSvc = host.Services.GetRequiredService<GameService>();

        discordBotSvc.Run();
        gameSvc.Run();
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Productions"}.json", optional: true)
            .AddEnvironmentVariables();
    }
}