using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWpfExample.WebApi.Data.Model;

namespace WebApiWpfExample.WebApi.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        public DbSet<WeatherObservation> WeatherObservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherObservation>().HasData(
                new WeatherObservation { WeatherObservationId = 1, Date = new DateTime(2020, 11, 25), TemperatureC = -5, Summary = "Freezing" },
                new WeatherObservation { WeatherObservationId = 2, Date = new DateTime(2020, 11, 26), TemperatureC = 7, Summary = "Cool" },
                new WeatherObservation { WeatherObservationId = 3, Date = new DateTime(2020, 11, 27), TemperatureC = 15, Summary = "Warm" },
                new WeatherObservation { WeatherObservationId = 4, Date = new DateTime(2020, 11, 28), TemperatureC = 23, Summary = "Hot" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
