using Xunit;
using System;
using Microsoft.EntityFrameworkCore;
using TaxApp.Data;
using TaxApp.Services;

namespace TaxApp.Tests
{
        public class TaxServiceTests
        {
            private TaxService GetService([System.Runtime.CompilerServices.CallerMemberName] string testName = "")
        {
            var options = new DbContextOptionsBuilder<TaxContext>()
                .UseInMemoryDatabase("TestDB_" + testName)
                .Options;

            return new TaxService(new TaxContext(options));
        }

        [Fact]
        public void TaxIsCorrectlyRetrieved()
        {
            var service = GetService();
            service.AddTax("TestCity", new DateTime(2025, 1, 1), new DateTime(2025, 12, 31), 0.2m);
            var result = service.GetTax("TestCity", new DateTime(2025, 6, 1));
            Assert.Equal(0.2m, result);
        }

        [Fact]
        public void GetTax_ReturnsCorrectRate_WhenMultipleRangesExist()
        {
            var service = GetService();
            service.AddTax("CityA", new DateTime(2025, 1, 1), new DateTime(2025, 3, 31), 0.1m);
            service.AddTax("CityA", new DateTime(2025, 4, 1), new DateTime(2025, 6, 30), 0.2m);

            var result = service.GetTax("CityA", new DateTime(2025, 5, 15));

            Assert.Equal(0.2m, result);
        }

        [Fact]
        public void GetTax_ReturnsNull_WhenNoMatchFound()
        {
            var service = GetService();
            var result = service.GetTax("NonExistingCity", new DateTime(2025, 1, 1));
            Assert.Null(result);
        }

        [Fact]
        public void GetTax_IncludesStartAndEndDates()
        {
            var service = GetService();
            service.AddTax("CityA", new DateTime(2025, 1, 1), new DateTime(2025, 12, 31), 0.15m);

            var startDateTax = service.GetTax("CityA", new DateTime(2025, 1, 1));
            var endDateTax = service.GetTax("CityA", new DateTime(2025, 12, 31));

            Assert.Equal(0.15m, startDateTax);
            Assert.Equal(0.15m, endDateTax);
        }

        [Fact]
        public void GetTax_ReturnsCorrectRate_ForDifferentCities()
        {
            var service = GetService();
            service.AddTax("CityA", new DateTime(2025, 1, 1), new DateTime(2025, 12, 31), 0.1m);
            service.AddTax("CityB", new DateTime(2025, 1, 1), new DateTime(2025, 12, 31), 0.2m);

            var result = service.GetTax("CityB", new DateTime(2025, 6, 1));

            Assert.Equal(0.2m, result);
        }



    }
}