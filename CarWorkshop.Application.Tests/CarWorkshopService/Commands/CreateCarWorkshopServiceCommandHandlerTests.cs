using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;


namespace CarWorkshop.Application.CarWorkshopService.Commands.Tests
{
    public class CreateCarWorkshopServiceCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreateCarWorkshopService_WhenUserIsAuthorized()
        {
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1",
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100",
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopServiceRepositoryMock.Object, userContextMock.Object, carWorkshopRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
        }

        [Fact()]
        public async Task Handle_CreateCarWorkshopService_WhenUserIsModerator()
        {
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1",
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100",
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new[] { "Moderator" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopServiceRepositoryMock.Object, userContextMock.Object, carWorkshopRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
        }

        [Fact()]
        public async Task Handle_DoesntCreateCarWorkshopService_WhenUserIsNotAuthorized()
        {
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1",
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100",
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new[] { "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopServiceRepositoryMock.Object, userContextMock.Object, carWorkshopRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
            carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
        }

        [Fact()]
        public async Task Handle_DoesntCreateCarWorkshopService_WhenUserIsNotAuthenticated()
        {
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1",
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100",
                Description = "test",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns((CurrentUser?)null);

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopServiceRepositoryMock.Object, userContextMock.Object, carWorkshopRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
            carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
        }
    }
}