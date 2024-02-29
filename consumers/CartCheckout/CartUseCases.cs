using ClothesVirtualStore.CommonsLib.Models;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Context;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Repositories;

public class CartUseCases
{
    private IVirtualStoreDbContext _context;
    private IStoreProductsRepository _productsRepository;

    public CartUseCases(IVirtualStoreDbContext dbContext, IStoreProductsRepository productsRepository)
    {
        _context = dbContext;
        _productsRepository = productsRepository;
        _context.PrepareDataBase();
    }

    public CustomerEntity CreateOrSelectCustomer(Order orderFromQueue)
    {
        var customer = _context.Customer.FirstOrDefault(x=> x.Cpf == orderFromQueue.cpf);

            if(customer == null){
                customer = new CustomerEntity(orderFromQueue.cpf, "Não informado");
                _context.Customer.Add(customer);
            }
        return customer;
    }

    internal OrderEntity CreateOrder(Order orderFromQueue, CustomerEntity customer)
    {
        var order = new OrderEntity
            {
                Id = orderFromQueue.orderId,
                Customer = customer,
                PurchaseDateTime = DateTime.Now,
                Itens = new List<OrderItemEntity>(),
                Status = OrderStatus.New
            };

        _context.Order.Add(order);
        return order;
    }

    internal void CreateOrderItens(Order orderFromQueue, OrderEntity order)
    {
        var products = _productsRepository.QueryByIds(orderFromQueue.itens.Select(x=>x.ProductId).ToArray());

        var itens = orderFromQueue.itens.Select(x => {
            var product = products.First(product=>product.Id == x.ProductId);

            return new OrderItemEntity
            {
                ProductName = product.Name,
                ProductPrice = product.Price,
                ProductAmount = x.Amount,
                TotalValue = x.Amount * product.Price,
                Order = order
            };
        }).ToList();

        _context.OrderItem.AddRange(itens);
    }

    internal void PersistAllEntities()
    {
        var result = _context.SaveChanges();
    }
}