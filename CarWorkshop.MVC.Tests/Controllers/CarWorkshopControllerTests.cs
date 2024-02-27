using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using CarWorkshop.Application.CarWorkhsop;
using Moq;
using MediatR;
using CarWorkshop.Application.CarWorkhsop.Queries.GetAllCarWorkshops;
using Microsoft.AspNetCore.TestHost;
using FluentAssertions;

namespace CarWorkshop.MVC.Controllers.Tests
{
    public class CarWorkshopControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public CarWorkshopControllerTests(WebApplicationFactory<Program> factory)
        { 
            _factory = factory;
        }

        [Fact()]
        public async Task Index_ReurnViewWithExpectedData()
        {
            //arrange
            var carWorkshops = new List<CarWorkshopDto>()
            {
                new CarWorkshopDto()
                {
                    Name = "Workshop 1"
                },
                new CarWorkshopDto()
                {
                    Name = "Workshop 2"
                },
                new CarWorkshopDto()
                {
                    Name = "Workshop 3"
                },
            };

            var mediatormock = new Mock<IMediator>();
            mediatormock.Setup(m => m.Send(It.IsAny<GetAllCarWorkshopsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carWorkshops);

            var client = _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddScoped(_ => mediatormock.Object)))
                .CreateClient();

            //act
            var response = await client.GetAsync("/CarWorkshop/Index");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>Car Workshops</h1>")
                .And.Contain("Workshop 1")
                .And.Contain("Workshop 2")
                .And.Contain("Workshop 3");
        }
    }
}