using Alibabooow.Api.DataAccessModels;
using Alibabooow.Api.DTOs.Requests;
using Alibabooow.Api.DTOs.Responses;

namespace Alibabooow.Api.Mapper;

public static class ProductMapper
{
    public static IEnumerable<FullProductResponse> MapToFullProductResponses(IEnumerable<ProductRecord> productRecords)
    {
        return productRecords.Select(x => MapToFullProductResponse(x));
    }

    public static FullProductResponse MapToFullProductResponse(ProductRecord productRecord)
    {
        if (productRecord.Owner is null)
            throw new Exception("CreatedBy can not be null.");

        return new FullProductResponse(
            productRecord.Id,
            productRecord.TotalSupply,
            productRecord.Price,
            productRecord.Name,
            productRecord.Description,
            productRecord.CreationDate,
            UserMapper.MapToUserResponse(productRecord.Owner));
    }

    public static ProductResponse MapToProductResponse(ProductRecord productRecord)
    {
        return new ProductResponse(
            productRecord.Id,
            productRecord.TotalSupply,
            productRecord.Price,
            productRecord.Name,
            productRecord.Description,
            productRecord.CreationDate);
    }

    public static ProductRecord MapToProductRecord(CreateProductRequest productResponse)
    {
        return new ProductRecord()
        {
            Id = Guid.NewGuid(),
            OwnerId = productResponse.CreatedById,
            TotalSupply = productResponse.TotalSupply,
            Price = productResponse.Price,
            Name = productResponse.Name,
            Description = productResponse.Description,
            CreationDate = DateTime.UtcNow
        };
    }
}
