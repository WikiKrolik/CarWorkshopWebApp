using Xunit;
using CarWorkshop.Application.CarWorkshopService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace CarWorkshop.Application.CarWorkshopService.Commands.Tests
{
    public class CreateCarWorkshopServiceCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            var validator = new CreateCarWorkshopServiceCommandValidator();
            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100",
                Description = "Desc",
                CarWorkshopEncodedName = "workshop1"
            };

            var result = validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInValidCommand_ShouldHaveValidationErrors()
        {
            var validator = new CreateCarWorkshopServiceCommandValidator();
            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "",
                Description = "",
                CarWorkshopEncodedName = null
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Cost);
        }
    }
}