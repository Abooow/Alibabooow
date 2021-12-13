using System.ComponentModel.DataAnnotations;

namespace Alibabooow.Api.DTOs.Requests;

public sealed class CreateProductRequest
{
    public Guid CreatedById { get; set; }

    public int TotalSupply { get; set; }

    public double Price { get; set; }

    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [MaxLength(64)]
    public string Description { get; set; }
}
