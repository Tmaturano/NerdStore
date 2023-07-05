using NS.Orders.API.Application.DTO;
using NS.Orders.Domain;

namespace NS.Orders.API.Application.Queries;

public interface IVoucherQueries
{
    Task<VoucherDTO> GetVoucherByCodeAsync(string code);
}

public class VoucherQueries : IVoucherQueries
{
    private readonly IVoucherRepository _voucherRepository;

    public VoucherQueries(IVoucherRepository voucherRepository) => _voucherRepository = voucherRepository;

    public async Task<VoucherDTO> GetVoucherByCodeAsync(string code)
    {
        var voucher = await _voucherRepository.GetVoucherByCodeAsync(code);

        if (voucher == null) return null;

        if (!voucher.IsValidForUse()) return null;

        return new VoucherDTO
        {
            Code = voucher.Code,
            DiscountType = (int)voucher.DiscountType,
            Percentage = voucher.Percentage,
            DiscountValue = voucher.DiscountValue
        };
    }
}