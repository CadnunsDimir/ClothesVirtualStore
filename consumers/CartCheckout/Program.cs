using System.Text;
using System.Text.Json;
using ClothesVirtualStore.CommonsLib.Models;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;
using MySqlX.XDevAPI.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

string queueName = "ClothesVirtualStore.Cart.Checkout";
string appName = "[CartCheckout.MQ.Consumer]";

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
        try
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"{appName} Received {message}");

            Console.WriteLine($"{appName} ... Begin of Processing  ...");

            // parse Order
            var orderFromQueue = JsonSerializer.Deserialize<Order>(message);
            // create customer profile by cpf on DB if not exists
            using var context = new VirtualStoreDbContext();

            context.Database.EnsureCreated();
            var customer = context.Customer.FirstOrDefault(x=> x.Cpf == orderFromQueue.cpf);

            if(customer == null){
                customer = new CustomerEntity(orderFromQueue.cpf, "Não informado");
                context.Customer.Add(customer);
            }            

            // update order with customer id

            var order = new OrderEntity
            {
                Id = orderFromQueue.orderId,
                Customer = customer,
                PurchaseDateTime = DateTime.Now,
                Itens = new List<OrderItemEntity>(),
                Status = OrderStatus.New
            };

            context.Order.Add(order);

            var itens = orderFromQueue.itens.Select(x => new OrderItemEntity
            {
                ProductName = "Cadastrar Produtos",
                ProductPrice = 0,
                ProductAmount = x.Amount,
                TotalValue = 0,
                Order = order
            }).ToList();

            context.OrderItem.AddRange(itens);

            var result = context.SaveChanges();

            Console.WriteLine($"{appName} Pedido salvo. Linhas Alteradas {result}");
            // add order in others queues

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{appName} Exception: {ex.Message}");
        }
        Console.WriteLine($"{appName} ... End of Processing  ...");
    };

    channel.BasicConsume(
        queue: queueName,
        autoAck: true,
        consumer
    );

    Console.WriteLine("Press [enter] to exit.");
    Console.Read();
}

