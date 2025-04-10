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
    public class Object2DControllerTests
    {
        private readonly Mock<IObject2DRepository> _mockObject2DRepository;
        private readonly Mock<ILogger<Object2DController>> _mockLogger;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<IEnvironment2DRepository> _mockEnvironment2DRepository;
        private readonly Object2DController _controller;

        public Object2DControllerTests()
        {
            _mockObject2DRepository = new Mock<IObject2DRepository>();
            _mockLogger = new Mock<ILogger<Object2DController>>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockEnvironment2DRepository = new Mock<IEnvironment2DRepository>();
            _controller = new Object2DController(_mockObject2DRepository.Object, _mockLogger.Object, _mockEnvironment2DRepository.Object);
        }

        [TestMethod]
        public async Task GetByEnvironmentId_ReturnsOk_WithObject2Ds()
        {
            // Arrange
            var environmentId = Guid.NewGuid();
            var object2Ds = new List<Object2D> { new Object2D() };
            _mockObject2DRepository.Setup(x => x.ReadByEnvironmentIdAsync(It.IsAny<Guid>())).ReturnsAsync(object2Ds);

            // Act
            var result = await _controller.GetByEnvironmentId(environmentId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(object2Ds, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ById_ReturnsNotFound_WhenObject2DIsNull()
        {
            // Arrange
            _mockObject2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Object2D?)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Get_ById_ReturnsOk_WithObject2D()
        {
            // Arrange
            var object2D = new Object2D();
            _mockObject2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync(object2D);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(object2D, okResult.Value);
        }


        [TestMethod]
        public async Task Update_ReturnsNotFound_WhenObject2DIsNull()
        {
            // Arrange
            _mockObject2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Object2D?)null);

            // Act
            var result = await _controller.Update(Guid.NewGuid(), new Object2D());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenObject2DIsNull()
        {
            // Arrange
            _mockObject2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Object2D?)null);

            // Act
            var result = await _controller.Delete(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsOk_WhenObject2DIsDeleted()
        {
            // Arrange
            var object2D = new Object2D();
            _mockObject2DRepository.Setup(x => x.ReadByIdAsync(It.IsAny<Guid>())).ReturnsAsync(object2D);
            _mockObject2DRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
