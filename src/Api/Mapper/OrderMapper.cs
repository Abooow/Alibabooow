using Alibabooow.Api.DataAccessModels;
using Alibabooow.Api.DTOs.Requests;
using Alibabooow.Api.DTOs.Responses;

namespace Alibabooow.Api.Mapper;

public static class OrderMapper
{
    public static IEnumerable<OrderResponse> MapToOrderResponses(IEnumerable<OrderRecord> orderRecords)
    {
        return orderRecords.Select(x => MapToOrderResponse(x));
    }

    public static OrderResponse MapToOrderResponse(OrderRecord orderRecord)
    {
        if (orderRecord.User is null)
            throw new Exception("User can not be null.");

        if (orderRecord.OrderDetails is null)
            throw new Exception("OrderDetails can not be null.");

        return new OrderResponse(
            orderRecord.Id,
            orderRecord.OrderDate,
            UserMapper.MapToUserResponse(orderRecord.User),
            orderRecord.OrderDetails.Select(x => new OrderDetailResponse(
                x.Id,
                x.OrderId,
                x.ProductId,
                orderRecord.UserId,
                x.Price,
                x.Quantity)));
    }

    public static OrderRecord MapToOrderRecord(PlaceOrderRequest orderRequest)
    {
        Guid orderId = Guid.NewGuid();

        return new OrderRecord()
        {
            Id = orderId,
            UserId = orderRequest.UserId,
            ShippingCountry = orderRequest.ShippingCountry,
            ShippingCity = orderRequest.ShippingCity,
            ShippingAddress = orderRequest.ShippingAddress,
            OrderDetails = orderRequest.OrderDetails.Select(x => new OrderDetailRecord()
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToArray(),
            OrderDate = DateTime.UtcNow
        };
    }
}
