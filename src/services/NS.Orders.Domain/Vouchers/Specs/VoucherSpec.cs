using NetDevPack.Specification;
using System.Linq.Expressions;

namespace NS.Orders.Domain.Specs;

public class VoucherDateSpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression() => voucher => voucher.ExpirationDate >= DateTime.Now;
}

public class VoucherQuantitySpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression() => voucher => voucher.Quantity > 0;
}

public class VoucherActiveSpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression() => voucher => voucher.IsActive && !voucher.Used;
}