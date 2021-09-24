using Alibabooow.Api.Data;
using Alibabooow.Api.DTOs.Requests;
using Alibabooow.Api.DTOs.Responses;
using Alibabooow.Api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alibabooow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductsController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<FullProductResponse>> GetAllProducts()
    {
        var products = await _applicationDbContext.Products
            .Include(x => x.Owner)
            .ToArrayAsync();

        return ProductMapper.MapToFullProductResponses(products);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var product = await _applicationDbContext.Products
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == productId);

        return product is null
            ? BadRequest("The product was not found.")
            : Ok(ProductMapper.MapToFullProductResponse(product));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest createProductRequest)
    {
        if (createProductRequest.TotalSupply <= 0)
            return BadRequest("The total supply has to be at least 1.");

        if (createProductRequest.Price <= 0)
            return BadRequest("The price has to be more than 0.");

        var user = await _applicationDbContext.Users.FindAsync(createProductRequest.CreatedById);
        if (user is null)
            return BadRequest("The user was not found.");

        var productRecord = ProductMapper.MapToProductRecord(createProductRequest);
        _applicationDbContext.Products.Add(productRecord);
        await _applicationDbContext.SaveChangesAsync();

        return Ok(ProductMapper.MapToFullProductResponse(productRecord));
    }
}
