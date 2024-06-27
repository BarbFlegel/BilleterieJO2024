using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BasketTests
{
    public class BasketTests
    {
        [Fact]
        public void Basket_Should_Set_Properties_Correctly()
        {
            // Arrange
            var basket = new Basket
            {
                Id = 1,
                BuyerId = "buyer123",
                Items = new List<BasketItem>
                {
                    new BasketItem { Id = 1, ProductId = 1, Quantity = 2 },
                    new BasketItem { Id = 2, ProductId = 2, Quantity = 3 }
                },
                PaymentIntentId = "payment123",
                ClientSecret = "secret123"
            };

            // Act & Assert
            Assert.Equal(1, basket.Id);
            Assert.Equal("buyer123", basket.BuyerId);
            Assert.Equal(2, basket.Items.Count);
            Assert.Equal("payment123", basket.PaymentIntentId);
            Assert.Equal("secret123", basket.ClientSecret);
        }

        [Fact]
        public void Basket_Should_Initialize_Default_Values()
        {
            // Arrange
            var basket = new Basket();

            // Act & Assert
            Assert.Equal(0, basket.Id);
            Assert.Null(basket.BuyerId);
            Assert.Empty(basket.Items);
            Assert.Null(basket.PaymentIntentId);
            Assert.Null(basket.ClientSecret);
        }

        [Fact]
        public void AddItem_Should_Add_New_Product()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            var basket = new Basket();

            // Act
            basket.AddItem(product, 2);

            // Assert
            Assert.Single(basket.Items);
            var addedItem = basket.Items.First();
            Assert.Equal(2, addedItem.Quantity);
            Assert.Equal(product, addedItem.Product);
            Assert.Equal(product.Id, addedItem.ProductId); // Ensure ProductId is set correctly
        }

        [Fact]
        public void AddItem_Should_Increase_Quantity_Of_Existing_Product()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            var basket = new Basket();
            basket.AddItem(product, 2);

            // Act
            basket.AddItem(product, 3);

            // Assert
            Assert.Single(basket.Items);
            var existingItem = basket.Items.First();
            Assert.Equal(5, existingItem.Quantity);
            Assert.Equal(product.Id, existingItem.ProductId);
        }

        [Fact]
        public void RemoveItem_Should_Decrease_Quantity_Of_Existing_Product()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            var basket = new Basket();
            basket.AddItem(product, 5);

            // Act
            basket.RemoveItem(product.Id, 2);

            // Assert
            Assert.Single(basket.Items);
            var item = basket.Items.First();
            Assert.Equal(3, item.Quantity);
            Assert.Equal(product.Id, item.ProductId);
        }

        [Fact]
        public void RemoveItem_Should_Remove_Product_If_Quantity_Is_Zero()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            var basket = new Basket();
            basket.AddItem(product, 2);

            // Act
            basket.RemoveItem(product.Id, 2);

            // Assert
            Assert.Empty(basket.Items);
        }

        [Fact]
        public void RemoveItem_Should_Not_Change_If_Product_Does_Not_Exist()
        {
            // Arrange
            var basket = new Basket();

            // Act
            basket.RemoveItem(1, 2);

            // Assert
            Assert.Empty(basket.Items);
        }

        [Fact]
        public void MapBasketToDto_Should_Map_Basket_To_BasketDto()
        {
            // Arrange
            var basket = new Basket
            {
                Id = 1,
                BuyerId = "buyer123",
                PaymentIntentId = "payment123",
                ClientSecret = "secret123",
                Items = new List<BasketItem>
                {
                    new BasketItem { Id = 1, ProductId = 1, Quantity = 2 },
                    new BasketItem { Id = 2, ProductId = 2, Quantity = 3 }
                }
            };

            // Act
            var basketDto = basket.MapBasketToDto();

            // Assert
            Assert.Equal(1, basketDto.Id);
            Assert.Equal("buyer123", basketDto.BuyerId);
            Assert.Equal("payment123", basketDto.PaymentIntentId);
            Assert.Equal("secret123", basketDto.ClientSecret);
            Assert.Equal(2, basketDto.Items.Count);
            Assert.Equal(1, basketDto.Items[0].ProductId);
            Assert.Equal(2, basketDto.Items[1].ProductId);
        }

        [Fact]
        public void RetrieveBasketWithItems_Should_Retrieve_Basket_With_Items()
        {
            // Arrange
            var buyerId = "buyer123";
            var baskets = new List<Basket>
            {
                new Basket
                {
                    Id = 1,
                    BuyerId = buyerId,
                    PaymentIntentId = "payment123",
                    ClientSecret = "secret123",
                    Items = new List<BasketItem>
                    {
                        new BasketItem { Id = 1, ProductId = 1, Quantity = 2 },
                        new BasketItem { Id = 2, ProductId = 2, Quantity = 3 }
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Basket>>();
            mockSet.As<IQueryable<Basket>>().Setup(m => m.Provider).Returns(baskets.Provider);
            mockSet.As<IQueryable<Basket>>().Setup(m => m.Expression).Returns(baskets.Expression);
            mockSet.As<IQueryable<Basket>>().Setup(m => m.ElementType).Returns(baskets.ElementType);
            mockSet.As<IQueryable<Basket>>().Setup(m => m.GetEnumerator()).Returns(baskets.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Basket>()).Returns(mockSet.Object);

            // Act
            var retrievedBasket = mockContext.Object.Set<Basket>().RetrieveBasketWithItems(buyerId).SingleOrDefault();

            // Assert
            Assert.NotNull(retrievedBasket);
            Assert.Equal(1, retrievedBasket.Id);
            Assert.Equal("buyer123", retrievedBasket.BuyerId);
            Assert.Equal("payment123", retrievedBasket.PaymentIntentId);
            Assert.Equal("secret123", retrievedBasket.ClientSecret);
            Assert.Equal(2, retrievedBasket.Items.Count);
            Assert.Equal(1, retrievedBasket.Items[0].ProductId);
            Assert.Equal(2, retrievedBasket.Items[1].ProductId);
        }
    }
}
