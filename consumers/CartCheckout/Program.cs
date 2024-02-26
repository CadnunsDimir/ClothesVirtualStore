
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

string queueName = "ClothesVirtualStore.Cart.Checkout";

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "admin",
    Password = "123456"
};

using var connection = factory.CreateConnection();
using (var channel = connection.CreateModel()){
    channel.QueueDeclare(
        queue: queueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
    );

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) => {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"[x] Received {message}");

        Console.WriteLine($"[x] ... Begining Processing  ...");

        
    };

    channel.BasicConsume(
        queue: queueName,
        autoAck: true,
        consumer
    );

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadKey();
}

