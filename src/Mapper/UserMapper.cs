using Alibabooow.Api.DataAccessModels;
using Alibabooow.Api.DTOs.Requests;
using Alibabooow.Api.DTOs.Responses;

namespace Alibabooow.Api.Mapper;

public static class UserMapper
{
    public static IEnumerable<UserResponse> MapToUserResponses(IEnumerable<UserRecord> userRecords)
    {
        return userRecords.Select(x => MapToUserResponse(x));
    }

    public static UserResponse MapToUserResponse(UserRecord userRecord)
    {
        return new UserResponse(
            userRecord.Id,
            $"{userRecord.FirstName} {userRecord.LastName}",
            userRecord.FirstName,
            userRecord.LastName);
    }

    public static UserRecord MapToUserRecord(CreateUserRequest userRequest)
    {
        return new UserRecord()
        {
            Id = Guid.NewGuid(),
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            DateCreated = DateTime.UtcNow
        };
    }
}
