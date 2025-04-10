using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectMap.WebApi.Controllers;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Tests.Controllers
{
    [TestClass]
    public class Environment2DControllerTests
    {
        private readonly Mock<IEnvironment2DRepository> _mockEnvironment2DRepository;
        private readonly Mock<ILogger<Environment2DController>> _mockLogger;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Environment2DController _controller;

        public Environment2DControllerTests()
        {
            _mockEnvironment2DRepository = new Mock<IEnvironment2DRepository>();
            _mockLogger = new Mock<ILogger<Environment2DController>>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _controller = new Environment2DController(_mockEnvironment2DRepository.Object, _mockLogger.Object, _mockAuthenticationService.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            // Arrange
            _mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns((string?)null);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }
        

        [TestMethod]
        public async Task Get_ById_ReturnsNotFound_WhenEnvironment2DIsNull()
        {
            // Arrange
            _mockEnvironment2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Environment2D?)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task Add_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            // Arrange
            _mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns((string?)null);

            // Act
            var result = await _controller.Add(new Environment2D());

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }


        [TestMethod]
        public async Task Update_ReturnsNotFound_WhenEnvironment2DIsNull()
        {
            // Arrange
            _mockEnvironment2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Environment2D?)null);

            // Act
            var result = await _controller.Update(Guid.NewGuid(), new Environment2D());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenEnvironment2DIsNull()
        {
            // Arrange
            _mockEnvironment2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Environment2D?)null);

            // Act
            var result = await _controller.Delete(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsOk_WhenEnvironment2DIsDeleted()
        {
            // Arrange
            var environment2D = new Environment2D();
            _mockEnvironment2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync(environment2D);
            _mockEnvironment2DRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}

