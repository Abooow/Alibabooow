namespace Alibabooow.Api.DTOs.Responses;

public sealed record ProductResponse(
    Guid ProductId,
    int TotalSupply,
    double Price,
    string Name,
    string? Description,
    DateTime DateCreated);
