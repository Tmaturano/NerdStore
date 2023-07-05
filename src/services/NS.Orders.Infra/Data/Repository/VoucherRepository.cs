using Microsoft.EntityFrameworkCore;
using NS.Core.Data;
using NS.Orders.Domain;

namespace NS.Orders.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly OrdersContext _context;

        public VoucherRepository(OrdersContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Voucher> GetVoucherByCodeAsync(string codigo) => await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == codigo);

        public void Update(Voucher voucher) => _context.Vouchers.Update(voucher);

        public void Dispose() => _context.Dispose();
    }
}