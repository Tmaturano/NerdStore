using NS.Core.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NS.BFF.Shopping.Models;

public class OrderDTO
{
    #region Order    
   
    public int Code { get; set; }

    // Authorized = 1,
    // Paid = 2,
    // Refused = 3,
    // Delivered = 4,
    // Canceled = 5
    public int Status { get; set; }
    public DateTime Data { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public string VoucherCode { get; set; }
    public bool VoucherAlreadyUsed { get; set; }

    public List<BasketItemDTO> BasketItems { get; set; }


    #endregion

    #region Endereco

    public AddressDTO Address { get; set; }

    #endregion

    #region Cartão

    [Required]
    [DisplayName("Card Number")]
    public string CardNumber { get; set; }

    [Required]
    [DisplayName("Nome do Portador")]
    public string NomeCartao { get; set; }

    [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "The expiration date should be in the format MM/AA")]
    [ExpirationCard(ErrorMessage = "Card Expired")]
    [Required]
    [DisplayName("Expire Date MM/AA")]
    public string ExpiracaoCartao { get; set; }

    [Required]
    [DisplayName("Security Code")]
    public string CvvCartao { get; set; }

    #endregion
}