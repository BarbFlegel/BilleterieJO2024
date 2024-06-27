using API.Entities;
using Xunit;

namespace Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_SetProperties()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "This is a test product",
                Price = 100,
                PictureUrl = "http://example.com/product.jpg",
                Type = "TestType",
                Brand = "TestBrand",
                QuantityInStock = 10,
                PublicId = "abcd1234"
            };

            // Act
            var id = product.Id;
            var name = product.Name;
            var description = product.Description;
            var price = product.Price;
            var pictureUrl = product.PictureUrl;
            var type = product.Type;
            var brand = product.Brand;
            var quantityInStock = product.QuantityInStock;
            var publicId = product.PublicId;

            // Assert
            Assert.Equal(1, id);
            Assert.Equal("Test Product", name);
            Assert.Equal("This is a test product", description);
            Assert.Equal(100, price);
            Assert.Equal("http://example.com/product.jpg", pictureUrl);
            Assert.Equal("TestType", type);
            Assert.Equal("TestBrand", brand);
            Assert.Equal(10, quantityInStock);
            Assert.Equal("abcd1234", publicId);
        }
    }
}
