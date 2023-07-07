namespace NS.Basket.API.Model;

public class Voucher
{
    public string Code { get; private set; }
    public decimal? Percentage { get; private set; }
    public decimal? DiscountValue { get; private set; }    
    public VoucherDiscountType DiscountType { get; private set; }
}

public enum VoucherDiscountType
{
    Percentage = 0,
    Value = 1
}