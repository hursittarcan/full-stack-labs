using DevOps.AppLogic;
using DevOps.Domain;
using DevOps.Domain.Tests.Builders;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace DevOps.Logic.Tests
{
    public class TeamServiceTests : TestBase
    {
        private Mock<IDeveloperRepository> _developerRepositoryMock;
        private ITeamService _service;

        [SetUp]
        public void Setup()
        {
            _developerRepositoryMock = new Mock<IDeveloperRepository>();

            _service = new TeamService(_developerRepositoryMock.Object);
        }

        [Test]
        public void AssembleDevelopersAsyncFor_EnoughDevelopersAvailable_ShouldRandomlyAddRequiredNumberOfDevelopers()
        {
            //Arrange
            int requiredNumberOfDevelopers = 2;
            Team team = new TeamBuilder().Build();

            var freeDevelopers = new List<Developer>();
            for (int i = 0; i < requiredNumberOfDevelopers + 2; i++)
            {
                freeDevelopers.Add(new DeveloperBuilder().WithoutTeam().Build());
            }
            _developerRepositoryMock.Setup(repo => repo.FindDevelopersWithoutATeamAsync()).ReturnsAsync(freeDevelopers);

            //Act
            _service.AssembleDevelopersAsyncFor(team, requiredNumberOfDevelopers).Wait();

            //Assert
            _developerRepositoryMock.Verify(repo => repo.FindDevelopersWithoutATeamAsync(), Times.Once);
            Assert.That(team.Developers, Has.Count.EqualTo(requiredNumberOfDevelopers));
            Assert.That(team.Developers.All(d => freeDevelopers.Contains(d)));
            _developerRepositoryMock.Verify(repo => repo.CommitTrackedChangesAsync(), Times.Once);
        }

        [Test]
        public void AssembleDevelopersAsyncFor_NotEnoughDevelopersAvailable_ShouldAddAllAvailableDevelopers()
        {
            //Arrange
            int requiredNumberOfDevelopers = 5;
            Team team = new TeamBuilder().Build();

            int numberOfAvailableDevelopers = requiredNumberOfDevelopers - 2;
            var freeDevelopers = new List<Developer>();
            for (int i = 0; i < numberOfAvailableDevelopers; i++)
            {
                freeDevelopers.Add(new DeveloperBuilder().WithoutTeam().Build());
            }
            _developerRepositoryMock.Setup(repo => repo.FindDevelopersWithoutATeamAsync()).ReturnsAsync(freeDevelopers);

            //Act
            _service.AssembleDevelopersAsyncFor(team, requiredNumberOfDevelopers).Wait();

            //Assert
            _developerRepositoryMock.Verify(repo => repo.FindDevelopersWithoutATeamAsync(), Times.Once);
            Assert.That(team.Developers, Has.Count.EqualTo(numberOfAvailableDevelopers));
            Assert.That(team.Developers.All(d => freeDevelopers.Contains(d)));
            _developerRepositoryMock.Verify(repo => repo.CommitTrackedChangesAsync(), Times.Once);
        }
    }
}
