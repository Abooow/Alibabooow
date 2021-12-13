using System.ComponentModel.DataAnnotations;

namespace Alibabooow.Api.DataAccessModels;

public sealed class UserRecord
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(32)]
    public string LastName { get; set; }

    public DateTime DateCreated { get; set; }
}
