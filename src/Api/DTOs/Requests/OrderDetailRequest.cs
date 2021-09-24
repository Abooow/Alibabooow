namespace Alibabooow.Api.DTOs.Requests;

public sealed class OrderDetailRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}
