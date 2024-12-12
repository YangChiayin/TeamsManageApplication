using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teams.Entities;

namespace Teams.Test
{
    public class EntitiesUnitTest
    {
        [Fact]
        public void Player_WithValidData_PassesValidation()
        {
            // Arrange
            var player = new Player
            {
                LeagueRegistrationNumber = "AB-12345678",
                FirstName = "John",
                LastName = "Doe",
                Number = 99
            };

            var context = new ValidationContext(player);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(player, context, results, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(results);
        }
    }
}
