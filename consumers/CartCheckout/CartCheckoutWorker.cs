
using ClothesVirtualStore.CommonsLib.Models;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.MQ;

public class CartCheckoutWorker: BackgroundService
{
    private readonly ILogger<CartCheckoutWorker> _logger;
    private readonly RabbitMQService _queueService;
    private readonly CartUseCases _useCases;

    public CartCheckoutWorker(
        ILogger<CartCheckoutWorker> logger, 
        RabbitMQService queueService,
        CartUseCases useCases)
    {
        _logger = logger;
        _queueService = queueService;
        _useCases = useCases;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _queueService.ConsumeCheckoutQueueAsync(stoppingToken, (model, ea) =>
        {
            try
            {
                LogInfo($"{AppConstants.appName} ... Begin of Processing  ...");

                var orderFromQueue = _queueService.ParseBody<Order>(ea) ?? throw new ArgumentNullException("orderFromQueue");
                var customer = _useCases.CreateOrSelectCustomer(orderFromQueue);
                var order = _useCases.CreateOrder(orderFromQueue, customer);
                _useCases.CreateOrderItens(orderFromQueue, order);
                _useCases.PersistAllEntities();

                LogInfo($"{AppConstants.appName} Pedido id {order.Id} salvo.");

                _queueService.PublishOnPaymentQueue(order);
            }
            catch (Exception ex)
            {
                LogInfo($"{AppConstants.appName} Exception: {ex.Message}");
            }
            LogInfo($"{AppConstants.appName} ... End of Processing  ...");
        });

        LogInfo($"{AppConstants.appName} Consumer was started successfully!");
    }

    private void LogInfo(string message)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(message);
        }
    }
}

public class AppConstants
{
    public static string appName = "[CartCheckout.MQ.Consumer]";
}