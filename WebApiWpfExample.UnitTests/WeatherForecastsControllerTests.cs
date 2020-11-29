using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebApiWpfExample.WebApi.Data.Interfaces;
using WebApiWpfExample.WebApi.Controllers;
using WebApiWpfExample.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApiWpfExample.WebApi.Data.Model;

namespace WebApiWpfExample.UnitTests
{
    public class WeatherForecastsControllerTests
    {
        [Fact]
        public async Task ForGetWeatherById_ReturnsNotFoundObjectResultForNonexistent()
        {
            // Arrange
            var mockRepo = new Mock<IWeatherRepository>();
            var controller = new WeatherForecastController(mockRepo.Object);
            var nonExistentSessionId = 999;

            //// Act
            var result = await controller.GetById(nonExistentSessionId);

            //// Assert
            var actionResult = Assert.IsType<ActionResult<WeatherObservationDto>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task ForGetWeatherById_ReturnsWeatherObject()
        {
            // Arrange
            int weatherId = 2;
            var mockRepo = new Mock<IWeatherRepository>();
            mockRepo.Setup(repo => repo.GetById(weatherId))
                .ReturnsAsync(GetTestWeather());
            var controller = new WeatherForecastController(mockRepo.Object);

            // Act
            var result = await controller.GetById(weatherId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WeatherObservationDto>>(result);
            var returnValue = Assert.IsType<WeatherObservationDto>(actionResult.Value);
            Assert.Equal("Warm", returnValue.Summary);
            Assert.Equal(21, returnValue.TemperatureC);
        }

        [Fact]
        public async Task ForGetAllWeather_ReturnsWeatherObjects()
        {
            // Arrange
            var mockRepo = new Mock<IWeatherRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .ReturnsAsync(GetTestWeatherList());
            var controller = new WeatherForecastController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WeatherObservationDto>>(result);
            var firstVal = returnValue.First();
            Assert.Equal(3, returnValue.Count());
            Assert.Equal("Hot", firstVal.Summary);
            Assert.Equal(34, firstVal.TemperatureC);
        }

        [Fact]
        public async Task AddWeather_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IWeatherRepository>();
            var controller = new WeatherForecastController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Post(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddWeather_ReturnsSuccessfulResponse()
        {
            // Arrange & Act
            var testWeather = GetTestWeather();
            var mockRepo = new Mock<IWeatherRepository>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<WeatherObservation>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new WeatherForecastController(mockRepo.Object);

            var newWeather = new WeatherObservationDto
            {
                Date = testWeather.Date,
                Summary = testWeather.Summary,
                TemperatureC = testWeather.TemperatureC
            };            

            // Act
            var result = await controller.Post(newWeather);

            // Assert
            Assert.IsType<OkResult>(result);
            mockRepo.Verify();
        }

        private WeatherObservation GetTestWeather()
        {
            return new WeatherObservation
            {
                Date = DateTime.Now,
                Summary = "Warm",
                TemperatureC = 21
            };
        }

        private IEnumerable<WeatherObservation> GetTestWeatherList()
        {
            yield return new WeatherObservation
            {
                Date = DateTime.Now,
                Summary = "Hot",
                TemperatureC = 34
            };
            yield return new WeatherObservation
            {
                Date = DateTime.Now,
                Summary = "Warm",
                TemperatureC = 21
            };
            yield return new WeatherObservation
            {
                Date = DateTime.Now,
                Summary = "Cold",
                TemperatureC = 3
            };
        }
    }
}
