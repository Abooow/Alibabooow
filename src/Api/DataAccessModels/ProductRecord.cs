using System.ComponentModel.DataAnnotations;

namespace Alibabooow.Api.DataAccessModels;

public sealed class ProductRecord
{
    [Key]
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [MaxLength(64)]
    public string? Description { get; set; }

    public int TotalSupply { get; set; }

    public double Price { get; set; }

    public DateTime CreationDate { get; set; }

    public UserRecord? Owner { get; set; }
}
