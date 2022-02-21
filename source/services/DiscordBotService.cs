using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class DiscordBotService : CustomServiceBase
{
    private readonly DiscordSocketClient _discordClient;

    public DiscordBotService(DiscordSocketClient client, ILogger<DiscordBotService> logger, IConfiguration config) : base(logger, config)
    {
        _discordClient = client;
    }

    public override void Run()
        => InitializeClientAsync().GetAwaiter().GetResult();

    private async Task InitializeClientAsync()
    {
        // Retreive our token value from our User Environment Variables.
        var token = _config.GetValue<string>("CollustrandaBotToken");

        _discordClient.Log += LogAsync;
        _discordClient.Ready += ReadyAsync;

        await _discordClient.LoginAsync(TokenType.Bot, token);
        await _discordClient.StartAsync();
    }

    private Task LogAsync(LogMessage logMessage)
    {
        _logger.LogInformation(logMessage.ToString());
        return Task.CompletedTask;
    }

    private Task ReadyAsync()
    {
        _logger.LogInformation($"Discord client connected as user {_discordClient.CurrentUser}");
        return Task.CompletedTask;
    }
}

