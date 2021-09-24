using System.ComponentModel.DataAnnotations;

namespace Alibabooow.Api.DataAccessModels;

public sealed class OrderDetailRecord
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public OrderRecord? Order { get; set; }

    public ProductRecord? Product { get; set; }
}
