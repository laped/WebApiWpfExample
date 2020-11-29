using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiWpfExample.WebApi.Data.Interfaces;
using WebApiWpfExample.WebApi.Data.Model;
using WebApiWpfExample.WebApi.Dto;

namespace WebApiWpfExample.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherObservationDto>> Get()
        {
            var observations = await _weatherRepository.GetAll();
            return observations.Select(o => new WeatherObservationDto
            {
                Date = o.Date,
                TemperatureC = o.TemperatureC,
                TemperatureF = o.TemperatureF,
                Summary = o.Summary
            });
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<WeatherObservationDto>> GetById(int id)
        {
            var weatherObservation = await _weatherRepository.GetById(id);
            if (weatherObservation == null)
                return NotFound(id);

            return new WeatherObservationDto
            {
                Date = weatherObservation.Date,
                TemperatureC = weatherObservation.TemperatureC,
                TemperatureF = weatherObservation.TemperatureF,
                Summary = weatherObservation.Summary
            };
        }

        [HttpPost]
        public async Task<ActionResult> Post(WeatherObservationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var weatherObservation = new WeatherObservation
            {
                Date = dto.Date,
                TemperatureC = dto.TemperatureC,
                Summary = dto.Summary
            };
            await _weatherRepository.Add(weatherObservation);

            return Ok();
        }
    }
}
