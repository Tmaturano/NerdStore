using NS.Core.Data;
using NS.Orders.Domain;

namespace NS.Orders.Domain;

public interface IVoucherRepository : IRepository<Voucher>
{
    Task<Voucher> GetVoucherByCodeAsync(string codigo);
    void Update(Voucher voucher);
}