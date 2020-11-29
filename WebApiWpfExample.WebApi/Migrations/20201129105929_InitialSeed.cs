using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiWpfExample.WebApi.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeatherObservations",
                columns: new[] { "WeatherObservationId", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Freezing", -5 },
                    { 2, new DateTime(2020, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cool", 7 },
                    { 3, new DateTime(2020, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warm", 15 },
                    { 4, new DateTime(2020, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hot", 23 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "WeatherObservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "WeatherObservationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "WeatherObservationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "WeatherObservationId",
                keyValue: 4);
        }
    }
}
