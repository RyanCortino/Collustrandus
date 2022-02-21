using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public abstract class CustomServiceBase : ICustomService
{
    protected readonly ILogger<ICustomService> _logger;
    protected readonly IConfiguration _config;

    public CustomServiceBase(ILogger<ICustomService> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    abstract public void Run();
}
