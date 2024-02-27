using Xunit;
using CarWorkshop.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarWorkshop.Application.ApplicationUser;
using AutoMapper;
using CarWorkshop.Application.CarWorkhsop;
using FluentAssertions;

namespace CarWorkshop.Application.Mappings.Tests
{
    public class CarWorkshopMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
        {
            var userContextMock = new Mock<IUserContext>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dto = new CarWorkshopDto
            {
                City = "city",
                PhoneNumber = "1234567890",
                PostalCode = "12345",
                Street = "street"
            };
            var result = mapper.Map<Domain.Entities.CarWorkshop>(dto);

            result.Should().NotBeNull();
            result.ContactDetails.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);

        }

        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopToCarWorkshopDto()
        {
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(u => u.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Moderator" }));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var carWorkshop = new Domain.Entities.CarWorkshop
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new Domain.Entities.CarWorkshopContactDetails
                {
                    City = "city",
                    PhoneNumber = "1234567890",
                    PostalCode = "12345",
                    Street = "street"
                }

            };
            var result = mapper.Map<CarWorkshopDto>(carWorkshop);

            result.Should().NotBeNull();
            result.Street.Should().Be(carWorkshop.ContactDetails.Street);
            result.City.Should().Be(carWorkshop.ContactDetails.City);
            result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
            result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
            result.IsEditable.Should().BeTrue();


        }
    }

    

}