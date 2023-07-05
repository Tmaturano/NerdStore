using NS.Core.Data;
using System.Data.Common;

namespace NS.Orders.Domain.Orders;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetListByClientIdAsync(Guid clientId);
    void Add(Order pedido);
    void Update(Order pedido);

    DbConnection GetConnection();

            
    Task<OrderItem> GetItemByIdAsync(Guid id);
    Task<OrderItem> GetItemByOrderAsync(Guid orderId, Guid productId);
}