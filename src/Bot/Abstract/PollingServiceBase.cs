namespace Bot.Abstract;

public abstract class PollingServiceBase<TReceiverService>
    : BackgroundService 
    where TReceiverService : IReceiverService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    private protected PollingServiceBase(
        IServiceProvider serviceProvider,
        ILogger logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting polling service");

        await DoWork(cancellationToken);
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider
                    .CreateScope();

                var receiver = scope.ServiceProvider
                    .GetRequiredService<TReceiverService>();

                await receiver.ReceiveAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Polling failed with exception");

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
