using System.ComponentModel.DataAnnotations;

namespace Alibabooow.Api.DTOs.Requests;

public sealed class CreateUserRequest
{
    [Required]
    [MaxLength(32)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(32)]
    public string LastName { get; set; }
}
