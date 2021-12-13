namespace Alibabooow.Api.DTOs.Responses;

public sealed record OrderDetailResponse(
    Guid OrderDetailId,
    Guid OrderId,
    Guid ProductId,
    Guid UserId,
    double Price,
    int Quantity);
