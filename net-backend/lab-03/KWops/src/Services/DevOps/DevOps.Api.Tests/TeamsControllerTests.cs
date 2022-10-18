using AutoMapper;
using DevOps.Api.Controllers;
using DevOps.Api.Models;
using DevOps.AppLogic;
using DevOps.Domain;
using DevOps.Domain.Tests.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Test;

namespace DevOps.Api.Tests
{
    public class TeamsControllerTests : TestBase
    {
        private Mock<ITeamService> _teamServiceMock;
        private Mock<ITeamRepository> _teamRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private TeamsController _controller;

        [SetUp]
        public void Setup()
        {
            _teamServiceMock = new Mock<ITeamService>();
            _teamRepositoryMock = new Mock<ITeamRepository>();
            _mapperMock = new Mock<IMapper>();

            _controller = new TeamsController(_teamServiceMock.Object,
                _teamRepositoryMock.Object,
                _mapperMock.Object);
        }


        [Test]
        public void GetAll_ShouldUseRepositoryAndMapResult()
        {
            //Arrange
            var allTeams = new List<Team>()
            {
                new TeamBuilder().Build(),
                new TeamBuilder().Build()
            };

            _teamRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(allTeams);

            _mapperMock.Setup(mapper => mapper.Map<TeamDetailModel>(It.IsAny<Team>())).Returns(new TeamDetailModel());


            //Act
            var result = _controller.GetAll().Result as OkObjectResult;

            //Assert
            Assert.That(result, Is.Not.Null);

            _teamRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<TeamDetailModel>(It.IsIn<Team>(allTeams)), Times.Exactly(allTeams.Count));

            var models = result.Value as IList<TeamDetailModel>;
            Assert.That(models, Has.Count.EqualTo(allTeams.Count));
        }

        [Test]
        public void AssembleTeam_TeamExists_ShouldUseService()
        {
            //Arrange
            Team team = new TeamBuilder().Build();
            var inputModel = new TeamAssembleInputModel
            {
                RequiredNumberOfDevelopers = Random.Next(2, 11)
            };

            _teamRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(team);

            //Act
            var result = _controller.AssembleTeam(team.Id, inputModel).Result as OkResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _teamRepositoryMock.Verify(repo => repo.GetByIdAsync(team.Id), Times.Once);
            _teamServiceMock.Verify(service => service.AssembleDevelopersAsyncFor(team, inputModel.RequiredNumberOfDevelopers), Times.Once);
        }

        [Test]
        public void AssembleTeam_TeamDoesNotExist_ShouldReturnNotFound()
        {
            //Arrange
            Guid nonExistingTeamId = Guid.NewGuid();
            var inputModel = new TeamAssembleInputModel
            {
                RequiredNumberOfDevelopers = Random.Next(2, 11)
            };

            _teamRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = _controller.AssembleTeam(nonExistingTeamId, inputModel).Result as NotFoundResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _teamRepositoryMock.Verify(repo => repo.GetByIdAsync(nonExistingTeamId), Times.Once);
            _teamServiceMock.Verify(service => service.AssembleDevelopersAsyncFor(It.IsAny<Team>(), It.IsAny<int>()), Times.Never);
        }
    }
}