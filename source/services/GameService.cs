using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

public class GameService : CustomServiceBase
{
    public GameService(ILogger<GameService> logger, IConfiguration config)
        : base(logger, config)
    {
    }

    public override void Run()
        => MainAsync().GetAwaiter().GetResult();

    private async Task MainAsync()
    {
        // Block this task until the program is closed. 
        await Task.Delay(Timeout.Infinite);
    }
}
