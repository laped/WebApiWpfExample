using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWpfExample.WebApi.Data.Model;

namespace WebApiWpfExample.WebApi.Data.Interfaces
{
    public interface IWeatherRepository : IRepository<WeatherObservation>
    {
        Task<IEnumerable<WeatherObservation>> GetAll();
        Task Add(WeatherObservation weatherObservation);
        Task<WeatherObservation> GetById(int id);
    }
}
