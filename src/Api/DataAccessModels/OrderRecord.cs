using System.ComponentModel.DataAnnotations;

namespace Alibabooow.Api.DataAccessModels;

public sealed class OrderRecord
{
    [Key]
    public Guid Id { get; set; }

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

    public double TotalPrice { get; set; }

    public bool Fulfilled { get; set; }

    public DateTime OrderDate { get; set; }

    public UserRecord? User { get; set; }

    public ICollection<OrderDetailRecord>? OrderDetails { get; set; }
}
