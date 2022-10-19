using DevOps.AppLogic.Events;
using DevOps.AppLogic.Tests.Builders;
using DevOps.Domain;
using DevOps.Domain.Tests.Builders;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Test;

namespace DevOps.AppLogic.Tests
{
    public class EmployeeHiredEventHandlerTests : TestBase
    {
        private Mock<IDeveloperRepository> _developerRepositoryMock;
        private EmployeeHiredEventHandler _handler;

        [SetUp]
        public void Setup()
        {
            _developerRepositoryMock = new Mock<IDeveloperRepository>();
            var logger = new Mock<ILogger<EmployeeHiredEventHandler>>();

            _handler = new EmployeeHiredEventHandler(_developerRepositoryMock.Object, logger.Object);
        }

        [Test]
        public void Handle_DeveloperWithSameIdAlreadyExists_ShouldDoNothing()
        {
            //Arrange
            var existingDeveloper = new DeveloperBuilder().Build();
            _developerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(existingDeveloper);
            EmployeeHiredIntegrationEvent @event = new EmployeeHiredIntegrationEventBuilder().Build();

            //Act
            _handler.Handle(@event).Wait();

            //Assert
            _developerRepositoryMock.Verify(repo => repo.GetByIdAsync(@event.Number), Times.Once);
            _developerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Developer>()), Times.Never);
        }

        [Test]
        public void Handle_DeveloperDoesNotExistYet_ShouldAddDeveloper()
        {
            //Arrange
            _developerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            EmployeeHiredIntegrationEvent @event = new EmployeeHiredIntegrationEventBuilder().Build();

            //Act
            _handler.Handle(@event).Wait();

            //Assert
            _developerRepositoryMock.Verify(repo => repo.GetByIdAsync(@event.Number), Times.Once);
            _developerRepositoryMock.Verify(
                repo => repo.AddAsync(It.Is<Developer>(d =>
                    d.Id == @event.Number &&
                    d.FirstName == @event.FirstName &&
                    d.LastName == @event.LastName &&
                    d.Rating == 0.0)), Times.Once);
        }
    }
}