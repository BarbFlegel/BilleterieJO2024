using API.DTOs;
using API.Entities.OrderAggregate;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class OrderExtensionsTests
    {
        [Fact]
        public void ProjectOrderToOrderDto_ReturnsCorrectDto()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    BuyerId = "buyer1",
                    OrderDate = DateTime.UtcNow,
                    ShippingEmailAddress = new ShippingEmailAddress
                    {
                        FullName = "John Doe",
                        EmailAddress = "john.doe@example.com"
                    },
                    ServiceFee = 10,
                    Subtotal = 100,
                    OrderStatus = OrderStatus.PaymentReceived,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ItemOrdered = new ProductItemOrdered
                            {
                                ProductId = 101,
                                Name = "Product A",
                                PictureUrl = "url_to_picture"
                            },
                            Price = 50,
                            Quantity = 2
                        },
                        new OrderItem
                        {
                            ItemOrdered = new ProductItemOrdered
                            {
                                ProductId = 102,
                                Name = "Product B",
                                PictureUrl = "url_to_picture"
                            },
                            Price = 30,
                            Quantity = 1
                        }
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Order>>();
            mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
            mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
            mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Order>()).Returns(mockSet.Object);

            // Act
            var result = mockContext.Object.Set<Order>().ProjectOrderToOrderDto().ToList();

            // Assert
            Assert.Single(result);  // We expect exactly one order in the result
            var orderDto = result.First();

            Assert.Equal(1, orderDto.Id);
            Assert.Equal("buyer1", orderDto.BuyerId);
            Assert.Equal("John Doe", orderDto.ShippingEmailAddress.FullName);
            Assert.Equal("john.doe@example.com", orderDto.ShippingEmailAddress.EmailAddress);
            Assert.Equal(2, orderDto.OrderItems.Count);
            Assert.Equal("Product A", orderDto.OrderItems[0].Name);
            Assert.Equal("Product B", orderDto.OrderItems[1].Name);
            Assert.Equal(80, orderDto.Subtotal);  // 50 * 2 + 30 * 1
            Assert.Equal(10, orderDto.ServiceFee);
            Assert.Equal("PaymentReceived", orderDto.OrderStatus);
            Assert.Equal(90, orderDto.Total);  // Subtotal + ServiceFee
        }
    }
}
