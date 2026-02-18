namespace Bot.Abstract;

public abstract class PollingServiceBase<TReceiverService>
    : BackgroundService 
    where TReceiverService : IReceiverService
{
    private const int RetryDelay = 5;
    private const int RetryDelayWhenError = 10;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    private protected PollingServiceBase(
        IServiceProvider serviceProvider,
        ILogger logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Starting polling service");

        await DoWork(stoppingToken);
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

                _logger.LogWarning(
                    "ReceiveAsync completed, restarting in {Seconds} seconds...",
                    RetryDelay);

                await Task.Delay(
                    TimeSpan.FromSeconds(RetryDelay),
                    cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation(
                    ex,
                    "Polling service cancelled");

                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Polling failed with exception, retrying in {RetrySeconds} seconds",
                    RetryDelayWhenError);

                await Task.Delay(
                    TimeSpan.FromSeconds(RetryDelayWhenError),
                    cancellationToken);
            }
        }

        _logger.LogInformation(
            "Polling service stopped");
    }
}
