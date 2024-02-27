using Xunit;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CarWorkshop.Domain.Entities.Tests
{
    public class CarWorkshopTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            //arange
            var carWorkshop = new CarWorkshop();
            carWorkshop.Name = "Test Workshop";

            //act
            carWorkshop.EncodeName();

            //assert
            carWorkshop.EncodedName.Should().Be("test-workshop");
        }

        [Fact()]
        public void EncodedName_ShouldThrowException_WhenNameIsNull()
        {
            var carWorkshop = new CarWorkshop();

            Action action = () => carWorkshop.EncodeName();

            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}