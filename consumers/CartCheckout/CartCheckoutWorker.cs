
using ClothesVirtualStore.CommonsLib.Models;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Context;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Repositories;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.MQ;

public class CartCheckoutWorker
{
    internal static void Run()
    {
        //TODO: implement dependency injection
        var queueService = new RabbitMQService();
        var context = new VirtualStoreDbContext() as IVirtualStoreDbContext;
        var productsRepository = new object() as IStoreProductsRepository;

        queueService.ConsumeCheckoutQueue((model, ea) =>
        {
            try
            {
                var useCases = new CartUseCases(context, productsRepository);
                Console.WriteLine($"{AppConstants.appName} ... Begin of Processing  ...");

                var orderFromQueue = queueService.ParseBody<Order>(ea) ?? throw new ArgumentNullException("orderFromQueue");
                var customer = useCases.CreateOrSelectCustomer(orderFromQueue);
                var order = useCases.CreateOrder(orderFromQueue, customer);
                useCases.CreateOrderItens(orderFromQueue, order);
                useCases.PersistAllEntities();

                Console.WriteLine($"{AppConstants.appName} Pedido id {order.Id} salvo.");

                queueService.PublishOnPaymentQueue(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{AppConstants.appName} Exception: {ex.Message}");
            }
            Console.WriteLine($"{AppConstants.appName} ... End of Processing  ...");
        });

        Console.WriteLine("Press [enter] to exit.");
        Console.Read();
    }
}

public class AppConstants
{
    public static string appName = "[CartCheckout.MQ.Consumer]";
}