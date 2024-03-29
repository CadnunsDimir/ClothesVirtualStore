﻿using System.Text;
using System.Text.Json;
using ClothesVirtualStore.Api.Cart.Services;
using RabbitMQ.Client;
using ClothesVirtualStore.CommonsLib.Models;

namespace ClothesVirtualStore.Api.Cart;

public class RabbitMQService : IMQProcessingService
{
    string queueName = "ClothesVirtualStore.Cart.Checkout";

    private readonly IConfiguration _configuration;

    public RabbitMQService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void EnqueueOrder(Order order)
    {
        var factory = new ConnectionFactory { 
            HostName = _configuration.GetConnectionString("RabbitMQHostName") ?? "localhost",
            UserName = "admin",
            Password = "123456"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

        var json = JsonSerializer.Serialize(order);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: string.Empty,
                            routingKey: queueName,
                            basicProperties: null,
                            body: body);
    }
}
