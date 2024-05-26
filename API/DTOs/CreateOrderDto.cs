using API.Entities.OrderAggregate;

namespace API.DTOs;

public class CreateOrderDto
{
    public bool SaveAddress { get; set; }
    public ShippingEmailAddress ShippingEmailAddress { get; set; }
}
