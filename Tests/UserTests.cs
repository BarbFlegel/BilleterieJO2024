using Xunit;
using API.Entities;

namespace UserTests
{
    public class UserTests
    {
        [Fact]
        public void User_CanSetAndGetProperties()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                Email = "testuser@example.com"
            };

            var address = new UserAddress
            {
                Id = 1,
                FullName = "John Doe",
                EmailAddress = "john.doe@example.com"
            };

            // Act
            user.Address = address;

            // Assert
            Assert.Equal("testuser", user.UserName);
            Assert.Equal("testuser@example.com", user.Email);
            Assert.Equal(1, user.Address.Id);
            Assert.Equal("John Doe", user.Address.FullName);
            Assert.Equal("john.doe@example.com", user.Address.EmailAddress);
        }

        [Fact]
        public void User_DefaultAddressIsNull()
        {
            // Arrange
            var user = new User();

            // Act
            var address = user.Address;

            // Assert
            Assert.Null(address);
        }

        [Fact]
        public void User_CanUpdateAddress()
        {
            // Arrange
            var user = new User();
            var address = new UserAddress
            {
                Id = 1,
                FullName = "John Doe",
                EmailAddress = "john.doe@example.com"
            };

            // Act
            user.Address = address;

            var newAddress = new UserAddress
            {
                Id = 2,
                FullName = "Jane Smith",
                EmailAddress = "jane.smith@example.com"
            };

            user.Address = newAddress;

            // Assert
            Assert.Equal(2, user.Address.Id);
            Assert.Equal("Jane Smith", user.Address.FullName);
            Assert.Equal("jane.smith@example.com", user.Address.EmailAddress);
        }
    }
}
