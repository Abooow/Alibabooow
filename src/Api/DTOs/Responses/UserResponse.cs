namespace Alibabooow.Api.DTOs.Responses;

public sealed record UserResponse(
    Guid UserId,
    string FullName,
    string FirstName,
    string LastName);
