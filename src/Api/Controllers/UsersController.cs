using Alibabooow.Api.Data;
using Alibabooow.Api.DTOs.Requests;
using Alibabooow.Api.DTOs.Responses;
using Alibabooow.Api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alibabooow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UsersController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<UserResponse>> GetAllUsers()
    {
        var users = await _applicationDbContext.Users.ToArrayAsync();

        return UserMapper.MapToUserResponses(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _applicationDbContext.Users.FindAsync(userId);

        return user is null
            ? BadRequest("User was not found.")
            : Ok(UserMapper.MapToUserResponse(user));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
    {
        var userRecord = UserMapper.MapToUserRecord(createUserRequest);
        _applicationDbContext.Users.Add(userRecord);
        await _applicationDbContext.SaveChangesAsync();

        return Ok(UserMapper.MapToUserResponse(userRecord));
    }
}
