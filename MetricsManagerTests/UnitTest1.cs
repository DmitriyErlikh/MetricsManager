using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private Mock<ILogger<CpuMetricsController>> logger;
        private CpuMetricsController controller;

        public CpuMetricsControllerUnitTests()
        {
            logger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            var result2 = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            _ = Assert.IsAssignableFrom<IActionResult>(result2);
        }
        
    }
    public class DotnetMetricsControllerUnitTests
    {
        private Mock<ILogger<DotnetMetricsController>> logger;
        private DotnetMetricsController controller;

        public DotnetMetricsControllerUnitTests()
        {
            logger = new Mock<ILogger<DotnetMetricsController>>();
            controller = new DotnetMetricsController(logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            var result2 = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            _ = Assert.IsAssignableFrom<IActionResult>(result2);
        }
    }
    public class HDDMetricsControllerUnitTests
    {
        private Mock<ILogger<HDDMetricsController>> logger;
        private HDDMetricsController controller;

        public HDDMetricsControllerUnitTests()
        {
            logger = new Mock<ILogger<HDDMetricsController>>();
            controller = new HDDMetricsController(logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            var result2 = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            _ = Assert.IsAssignableFrom<IActionResult>(result2);
        }
    }
    public class NetworkMetricsControllerUnitTests
    {
        private Mock<ILogger<NetworkMetricsController>> logger;
        private NetworkMetricsController controller;

        public NetworkMetricsControllerUnitTests()
        {
            logger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            var result2 = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            _ = Assert.IsAssignableFrom<IActionResult>(result2);
        }
    }
    public class RAMMetricsControllerUnitTests
    {
        private Mock<ILogger<RAMMetricsController>> logger;
        private RAMMetricsController controller;

        public RAMMetricsControllerUnitTests()
        {
            logger = new Mock<ILogger<RAMMetricsController>>();
            controller = new RAMMetricsController(logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            var result2 = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            _ = Assert.IsAssignableFrom<IActionResult>(result2);
        }
    }
}
