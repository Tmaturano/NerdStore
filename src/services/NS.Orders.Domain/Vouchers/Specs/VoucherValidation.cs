using NetDevPack.Specification;

namespace NS.Orders.Domain.Specs;

public class VoucherValidation : SpecValidator<Voucher>
{
    public VoucherValidation()
    {
        var dateSpec = new VoucherDateSpecification();
        var quantitySpec = new VoucherQuantitySpecification();
        var activeSpec = new VoucherActiveSpecification();

        Add("dateSpec", new Rule<Voucher>(dateSpec, "This voucher is already expired"));
        Add("quantitySpec", new Rule<Voucher>(quantitySpec, "This voucher is already used"));
        Add("activeSpec", new Rule<Voucher>(activeSpec, "This voucher is no longer active"));
    }
}