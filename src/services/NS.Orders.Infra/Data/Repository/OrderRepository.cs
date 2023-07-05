using Microsoft.EntityFrameworkCore;
using NS.Core.Data;
using NS.Orders.Domain.Orders;
using System.Data.Common;

namespace NS.Orders.Infra.Data.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrdersContext _context;

    public OrderRepository(OrdersContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public DbConnection GetConnection() => _context.Database.GetDbConnection();

    public async Task<Order> GetByIdAsync(Guid id) => await _context.Orders.FindAsync(id);

    public async Task<IEnumerable<Order>> GetListByClientIdAsync(Guid clientId)
    {
        return await _context.Orders
            .Include(p => p.OrderItems)
            .AsNoTracking()
            .Where(p => p.ClientId == clientId)
            .ToListAsync();
    }

    public void Add(Order order) => _context.Orders.Add(order);

    public void Update(Order order) => _context.Orders.Update(order);


    public async Task<OrderItem> GetItemByIdAsync(Guid id) => await _context.OrderItems.FindAsync(id);

    public async Task<OrderItem> GetItemByOrderAsync(Guid orderId, Guid productId)
    {
        return await _context.OrderItems
            .FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);
    }

    public void Dispose() => _context.Dispose();
}