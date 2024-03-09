
using System.Text;
using System.Text.Json;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.MQ;

public class RabbitMQService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CartCheckoutWorker> _logger;

    public RabbitMQService(IConfiguration configuration, ILogger<CartCheckoutWorker> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public async Task ConsumeCheckoutQueueAsync(CancellationToken stoppingToken, EventHandler<BasicDeliverEventArgs> handler)
    {
        string queueName = "ClothesVirtualStore.Cart.Checkout";

        var factory = new ConnectionFactory
        {
            HostName = _configuration.GetConnectionString(AppConstants.RabbitMQHostNameKey),
            UserName = "admin",
            Password = "123456"
        };

        _logger.LogInformation($"MQ.hostname: {factory.HostName}");

        using var connection = factory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += handler;
        
            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer
            );

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    public T ParseBody<T>(BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        _logger.LogInformation($"{AppConstants.appName} json: {message}");
        var result =  JsonSerializer.Deserialize<T>(message);
        if (result == null)
        {
            throw new JsonException("MessageQueueBody deserialize response is null");
        }
        _logger.LogInformation($"{AppConstants.appName} parsed: {result}");
        return result;
    }

    public void PublishOnPaymentQueue(OrderEntity order)
    {
        _logger.LogInformation($"{AppConstants.appName} ... PublishOnPaymentQueue routine not implemented  ...");
    }
}

