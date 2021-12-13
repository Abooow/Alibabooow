namespace Alibabooow.Api.DTOs.Responses;

public sealed record OrderResponse(
    Guid OrderId,
    DateTime DateOrdered,
    UserResponse User,
    IEnumerable<OrderDetailResponse> OrderDetails);
