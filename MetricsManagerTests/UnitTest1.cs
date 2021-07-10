using System;
using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Support;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using MetricsAgent.DAL;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerTests
    {
        private Mock<ILogger<CpuMetricsController>> _logger;
        private IMapper _mapper;
        private CpuMetricsController _controller;
        private Mock<ICpuMetricRepository> _mock;

        public CpuMetricsControllerTests()
        {
            _logger = new Mock<ILogger<CpuMetricsController>>();
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mock = new Mock<ICpuMetricRepository>();
            _controller = new CpuMetricsController(_logger.Object, _mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }

        [Fact]
        public void GetMetricsFromCluster_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class DotNetMetricsControllerTests
    {
        private Mock<ILogger<DotnetMetricsController>> _logger;
        private IMapper _mapper;
        private DotnetMetricsController _controller;
        private Mock<IDotnetMetricRepository> _mock;

        public DotNetMetricsControllerTests()
        {
            _logger = new Mock<ILogger<DotnetMetricsController>>();
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mock = new Mock<IDotnetMetricRepository>();
            _controller = new DotnetMetricsController(_logger.Object, _mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }

        [Fact]
        public void GetMetricsFromCluster_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class HDDMetricsManagerTests
    {
        private Mock<ILogger<HDDMetricsController>> _logger;
        private IMapper _mapper;
        private HDDMetricsController _controller;
        private Mock<IHDDMetricRepository> _mock;

        public HDDMetricsManagerTests()
        {
            _logger = new Mock<ILogger<HDDMetricsController>>();
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mock = new Mock<IHDDMetricRepository>();
            _controller = new HDDMetricsController(_logger.Object, _mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }

        [Fact]
        public void GetMetricsFromCluster_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class NetworkMetricsControllerTests
    {
        private Mock<ILogger<NetworkMetricsController>> _logger;
        private IMapper _mapper;
        private NetworkMetricsController _controller;
        private Mock<INetworkMetricRepository> _mock;

        public NetworkMetricsControllerTests()
        {
            _logger = new Mock<ILogger<NetworkMetricsController>>();
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mock = new Mock<INetworkMetricRepository>();
            _controller = new NetworkMetricsController(_logger.Object, _mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }

        [Fact]
        public void GetMetricsFromCluster_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class RAMMetricsControllerTests
    {
        private Mock<ILogger<RAMMetricsController>> _logger;
        private IMapper _mapper;
        private RAMMetricsController _controller;
        private Mock<IRAMMetricRepository> _mock;

        public RAMMetricsControllerTests()
        {
            _logger = new Mock<ILogger<RAMMetricsController>>();
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mock = new Mock<IRAMMetricRepository>();
            _controller = new RAMMetricsController(_logger.Object, _mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetMetricsFromAgent(
                    It.IsAny<int>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }

        [Fact]
        public void GetMetricsFromCluster_ReturnsOk()
        {
            _mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            _mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
}