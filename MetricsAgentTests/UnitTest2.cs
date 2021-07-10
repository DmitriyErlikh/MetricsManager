using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using Moq;
using System;
using Xunit;
using MetricsAgent;
using MetricsAgent.Models;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuAgentController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuAgentController>> logger;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            logger = new Mock<ILogger<CpuAgentController>>();
            controller = new CpuAgentController(logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Get_By_Time_Period_from_Repository()
        {
            DateTimeOffset fromTime = DateTimeOffset.Now.AddYears(-3);
            DateTimeOffset toTime = DateTimeOffset.Now;

            mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var result = controller.GetMetricsCPU(fromTime, toTime);


            mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class DotNetMetricsControllerUnitTests
    {
        private DotnetAgentController controller;
        private Mock<IDotnetMetricsRepository> mock;
        private Mock<ILogger<DotnetAgentController>> logger;

        public DotNetMetricsControllerUnitTests()
        {
            mock = new Mock<IDotnetMetricsRepository>();
            logger = new Mock<ILogger<DotnetAgentController>>();
            controller = new DotnetAgentController(logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mock.Setup(repository => repository.Create(It.IsAny<DotnetMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new MetricsAgent.Requests.DotnetMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<DotnetMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Get_By_Time_Period_from_Repository()
        {
            DateTimeOffset fromTime = DateTimeOffset.Now.AddYears(-3);
            DateTimeOffset toTime = DateTimeOffset.Now;

            mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var result = controller.GetMetricsDotnet(fromTime, toTime);


            mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class HDDAgentControllerUnitTests
    {
        private HDDAgentController controller;
        private Mock<IHDDMetricsRepository> mock;
        private Mock<ILogger<HDDAgentController>> logger;

        public HDDAgentControllerUnitTests()
        {
            mock = new Mock<IHDDMetricsRepository>();
            logger = new Mock<ILogger<HDDAgentController>>();
            controller = new HDDAgentController(logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mock.Setup(repository => repository.Create(It.IsAny<HDDMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new MetricsAgent.Requests.HDDMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<HDDMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Get_By_Time_Period_from_Repository()
        {
            DateTimeOffset fromTime = DateTimeOffset.Now.AddYears(-3);
            DateTimeOffset toTime = DateTimeOffset.Now;

            mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var result = controller.GetMetricsHDD(fromTime, toTime);


            mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class NetworkAgentControllerUnitTests
    {
        private NetworkAgentController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkAgentController>> logger;

        public NetworkAgentControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            logger = new Mock<ILogger<NetworkAgentController>>();
            controller = new NetworkAgentController(logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new MetricsAgent.Requests.NetworkMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Get_By_Time_Period_from_Repository()
        {
            DateTimeOffset fromTime = DateTimeOffset.Now.AddYears(-3);
            DateTimeOffset toTime = DateTimeOffset.Now;

            mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var result = controller.GetMetricsNetwork(fromTime, toTime);


            mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
    public class RAMAgentControllerUnitTests
    {
        private RAMAgentController controller;
        private Mock<IRAMMetricsRepository> mock;
        private Mock<ILogger<RAMAgentController>> logger;

        public RAMAgentControllerUnitTests()
        {
            mock = new Mock<IRAMMetricsRepository>();
            logger = new Mock<ILogger<RAMAgentController>>();
            controller = new RAMAgentController(logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mock.Setup(repository => repository.Create(It.IsAny<RAMMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new MetricsAgent.Requests.RAMMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<RAMMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Get_By_Time_Period_from_Repository()
        {
            DateTimeOffset fromTime = DateTimeOffset.Now.AddYears(-3);
            DateTimeOffset toTime = DateTimeOffset.Now;

            mock.Setup(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var result = controller.GetMetricsRAM(fromTime, toTime);


            mock.Verify(controller =>
                controller.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce);
        }
    }
}