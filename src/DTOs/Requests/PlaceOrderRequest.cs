using System.ComponentModel.DataAnnotations;

namespace Alibabooow.Api.DTOs.Requests;

public sealed class PlaceOrderRequest
{
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(64)]
    public string ShippingCountry { get; set; }

    [Required]
    [MaxLength(64)]
    public string ShippingCity { get; set; }

    [Required]
    [MaxLength(64)]
    public string ShippingAddress { get; set; }

    [Required]
    public IEnumerable<OrderDetailRequest> OrderDetails { get; set; }
}
