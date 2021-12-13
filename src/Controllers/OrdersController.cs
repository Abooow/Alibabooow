using Alibabooow.Api.Data;
using Alibabooow.Api.DataAccessModels;
using Alibabooow.Api.DTOs.Requests;
using Alibabooow.Api.DTOs.Responses;
using Alibabooow.Api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alibabooow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public OrdersController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderResponse>> GetAllOrders()
    {
        var orders = await _applicationDbContext.Orders
            .Include(x => x.User)
            .Include(x => x.OrderDetails)
            .OrderByDescending(x => x.OrderDate)
            .ToArrayAsync();

        return OrderMapper.MapToOrderResponses(orders);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllUserOrders(Guid userId)
    {
        var user = await _applicationDbContext.Users.FindAsync(userId);
        if (user is null)
            return BadRequest("User was not found.");

        var orders = await _applicationDbContext.Orders
            .Include(x => x.User)
            .Include(x => x.OrderDetails)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.OrderDate)
            .ToArrayAsync();

        return Ok(OrderMapper.MapToOrderResponses(orders));
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(Guid orderId)
    {
        var order = await _applicationDbContext.Orders
            .Include(x => x.User)
            .Include(x => x.OrderDetails)
            .FirstOrDefaultAsync(x => x.Id == orderId);

        return order is null
            ? BadRequest("The order was not found.")
            : Ok(OrderMapper.MapToOrderResponse(order));
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderRequest placeOrderRequest)
    {
        if (placeOrderRequest.OrderDetails.Count() == 0)
            return BadRequest("At least 1 order detail has to be placed.");

        var user = await _applicationDbContext.Users.FindAsync(placeOrderRequest.UserId);
        if (user is null)
            return BadRequest($"User was not found.");

        OrderRecord orderRecord = OrderMapper.MapToOrderRecord(placeOrderRequest);

        foreach (var orderDetail in orderRecord.OrderDetails!)
        {
            if (orderDetail.Quantity <= 0)
                return BadRequest($"The quantity has to be more than 0.");

            var product = await _applicationDbContext.Products.FindAsync(orderDetail.ProductId);
            if (product is null)
                return BadRequest($"Product with Id: {orderDetail.ProductId} was not found.");

            if (orderDetail.Quantity > product.TotalSupply)
            {
                return BadRequest($"Can not buy more than the total supply for product {product.Name}. " +
                    $"Total supply is {product.TotalSupply} but tried to buy {orderDetail.Quantity}.");
            }

            product.TotalSupply -= orderDetail.Quantity;
            _applicationDbContext.Products.Update(product);

            orderDetail.Price = product.Price * orderDetail.Quantity;
            _applicationDbContext.OrderDetails.Add(orderDetail);
        }

        _applicationDbContext.Orders.Add(orderRecord);
        await _applicationDbContext.SaveChangesAsync();

        return Ok(OrderMapper.MapToOrderResponse(orderRecord));
    }
}
