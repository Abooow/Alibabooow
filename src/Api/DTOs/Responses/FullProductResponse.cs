namespace Alibabooow.Api.DTOs.Responses;

public sealed record FullProductResponse(
    Guid ProductId,
    int TotalSupply,
    double Price,
    string Name,
    string? Description,
    DateTime DateCreated,
    UserResponse CreatedBy);
