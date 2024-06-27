using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Extensions;
using Xunit;

namespace Tests
{
    public class ProductExtensionsTests
    {
        [Fact]
        public void Sort_OrderByPrice_ReturnsOrderedQuery()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Price = 100 },
                new Product { Id = 2, Name = "Product B", Price = 50 },
                new Product { Id = 3, Name = "Product C", Price = 75 }
            }.AsQueryable();

            // Act
            var result = products.Sort("price").ToList();

            // Assert
            Assert.Equal(2, result[0].Id);  // Product B should be first (lowest price)
            Assert.Equal(3, result[1].Id);  // Product C should be second
            Assert.Equal(1, result[2].Id);  // Product A should be last (highest price)
        }

        [Fact]
        public void Sort_OrderByPriceDesc_ReturnsOrderedQuery()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Price = 100 },
                new Product { Id = 2, Name = "Product B", Price = 50 },
                new Product { Id = 3, Name = "Product C", Price = 75 }
            }.AsQueryable();

            // Act
            var result = products.Sort("priceDesc").ToList();

            // Assert
            Assert.Equal(1, result[0].Id);  // Product A should be first (highest price)
            Assert.Equal(3, result[1].Id);  // Product C should be second
            Assert.Equal(2, result[2].Id);  // Product B should be last (lowest price)
        }

        [Fact]
        public void Sort_NullOrEmptyOrderBy_ReturnsOrderedByName()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A" },
                new Product { Id = 2, Name = "Product B" },
                new Product { Id = 3, Name = "Product C" }
            }.AsQueryable();

            // Act
            var result = products.Sort(null).ToList();

            // Assert
            Assert.Equal(1, result[0].Id);  // Product A should be first
            Assert.Equal(2, result[1].Id);  // Product B should be second
            Assert.Equal(3, result[2].Id);  // Product C should be last
        }

        [Fact]
        public void Search_MatchingSearchTerm_ReturnsFilteredQuery()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Apple iPhone 12" },
                new Product { Id = 2, Name = "Samsung Galaxy S21" },
                new Product { Id = 3, Name = "Google Pixel 5" }
            }.AsQueryable();

            // Act
            var result = products.Search("iphone").ToList();

            // Assert
            Assert.Single(result);  // Only Apple iPhone 12 should match
            Assert.Equal("Apple iPhone 12", result[0].Name);
        }

        [Fact]
        public void Search_NonMatchingSearchTerm_ReturnsOriginalQuery()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Apple iPhone 12" },
                new Product { Id = 2, Name = "Samsung Galaxy S21" },
                new Product { Id = 3, Name = "Google Pixel 5" }
            }.AsQueryable();

            // Act
            var result = products.Search("xyz").ToList();

            // Assert
            Assert.Equal(3, result.Count);  // No matches, so all products should be returned
        }

        [Fact]
        public void Filter_ByBrand_ReturnsFilteredQuery()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Brand = "Brand1" },
                new Product { Id = 2, Name = "Product B", Brand = "Brand2" },
                new Product { Id = 3, Name = "Product C", Brand = "Brand1" }
            }.AsQueryable();

            // Act
            var result = products.Filter("Brand1", null).ToList();

            // Assert
            Assert.Equal(2, result.Count);  // Only products with Brand1 should be returned
            Assert.Equal("Brand1", result[0].Brand);
            Assert.Equal("Brand1", result[1].Brand);
        }

        [Fact]
        public void Filter_ByType_ReturnsFilteredQuery()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Type = "Type1" },
                new Product { Id = 2, Name = "Product B", Type = "Type2" },
                new Product { Id = 3, Name = "Product C", Type = "Type1" }
            }.AsQueryable();

            // Act
            var result = products.Filter(null, "Type1").ToList();

            // Assert
            Assert.Equal(2, result.Count);  // Only products with Type1 should be returned
            Assert.Equal("Type1", result[0].Type);
            Assert.Equal("Type1", result[1].Type);
        }

        [Fact]
        public void Filter_ByBrandAndType_ReturnsFilteredQuery()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Brand = "Brand1", Type = "Type1" },
                new Product { Id = 2, Name = "Product B", Brand = "Brand2", Type = "Type2" },
                new Product { Id = 3, Name = "Product C", Brand = "Brand1", Type = "Type2" }
            }.AsQueryable();

            // Act
            var result = products.Filter("Brand1", "Type1").ToList();

            // Assert
            Assert.Single(result);  // Only one product matches Brand1 and Type1
            Assert.Equal("Brand1", result[0].Brand);
            Assert.Equal("Type1", result[0].Type);
        }
    }
}
