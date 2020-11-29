using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWpfExample.WebApi.Data.Interfaces;
using WebApiWpfExample.WebApi.Data.Model;

namespace WebApiWpfExample.WebApi.Data.Implementations
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext _dbContext;

        public WeatherRepository(WeatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(WeatherObservation weatherObservation)
        {
            _dbContext.WeatherObservations.Add(weatherObservation);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeatherObservation>> GetAll()
        {
            return await _dbContext.WeatherObservations.ToListAsync();
        }

        public Task<WeatherObservation> GetById(int id)
        {
            return _dbContext.WeatherObservations.FirstOrDefaultAsync(w => w.WeatherObservationId == id);
        }
    }
}
