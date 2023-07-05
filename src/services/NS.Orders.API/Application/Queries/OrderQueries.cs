using Dapper;
using NS.Orders.API.Application.DTO;
using NS.Orders.Domain.Orders;

namespace NS.Orders.API.Application.Queries;

public interface IOrderQueries
{
    Task<OrderDTO> GetLastOrderAsync(Guid clientId);
    Task<IEnumerable<OrderDTO>> GetListByClientIdAsync(Guid clientId);
}

public class OrderQueries : IOrderQueries
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueries(IOrderRepository orderRepository) => _orderRepository = orderRepository;

    public async Task<OrderDTO> GetLastOrderAsync(Guid clientId)
    {
        const string sql = @"SELECT
                                P.Id AS 'ProductId', P.Code, P.VoucherAlreadyUsed, P.Discount, P.TotalPrice, P.OrderStatus,
                                P.Street, P.Number, P.Neighborhood, P.ZipCode, P.Complement, P.City, P.State,
                                PIT.ID AS 'ProductItemId',PIT.ProductName, PIT.Quantity, PIT.ProductImage, PIT.UnitaryValue 
                                FROM ORDERS P 
                                INNER JOIN ORDERITEMS PIT ON P.Id = PIT.OrderId
                                WHERE P.ClientId = @clientId 
                                AND P.RegisterDate between DATEADD(minute, -3,  GETDATE()) and DATEADD(minute, 0,  GETDATE())
                                AND P.OrderStatus = 1 
                                ORDER BY P.RegisterDate DESC";

        var pedido = await _orderRepository.GetConnection()
            .QueryAsync<dynamic>(sql, new { clientId });

        return MapOrder(pedido);
    }

    public async Task<IEnumerable<OrderDTO>> GetListByClientIdAsync(Guid clientId)
    {
        var pedidos = await _orderRepository.GetListByClientIdAsync(clientId);

        return pedidos.Select(OrderDTO.ToOrderDTO);
    }

    private OrderDTO MapOrder(dynamic result)
    {
        var order = new OrderDTO
        {
            Code = result[0].Code,
            Status = result[0].OrderStatus,
            TotalPrice = result[0].TotalPrice,
            Discount = result[0].Discount,
            VoucherAlreadyUsed = result[0].VoucherAlreadyUsed,

            OrderItems = new List<OrderItemDTO>(),
            Address = new AddressDTO
            {
                Street = result[0].Street,
                Neighborhood = result[0].Neighborhood,
                ZipCode = result[0].ZipCode,
                City = result[0].City,
                Complement = result[0].Complement,
                State = result[0].State,
                Number = result[0].Number
            }
        };

        foreach (var item in result)
        {
            var orderItem = new OrderItemDTO
            {
                Name = item.ProductName,
                Price = item.UnitaryValue,
                Quantity = item.Quantity,
                Image = item.ProductImage
            };

            order.OrderItems.Add(orderItem);
        }

        return order;
    }
}