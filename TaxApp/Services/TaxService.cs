using System;
using System.IO;
using System.Linq;
using System.Globalization;
using TaxApp.Data;
using TaxApp.Models;

namespace TaxApp.Services
{
    public class TaxService
    {
        private readonly TaxContext _context;

        public TaxService(TaxContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void AddTax(string municipality, DateTime start, DateTime end, decimal rate)
        {
            var record = new TaxRecord { Municipality = municipality, StartDate = start, EndDate = end, Rate = rate };
            _context.TaxRecords.Add(record);
            _context.SaveChanges();
        }

        public decimal? GetTax(string municipality, DateTime date)
        {
            return _context.TaxRecords
                .Where(t => t.Municipality == municipality && date >= t.StartDate && date <= t.EndDate)
                .AsEnumerable()
                .OrderBy(t => (t.EndDate - t.StartDate).Days)
                .Select(t => (decimal?)t.Rate)
                .FirstOrDefault();
        }

        public void ExportToCsv(string filename)
        {
            using var writer = new StreamWriter(filename);
            writer.WriteLine("Municipality,StartDate,EndDate,Rate");
            foreach (var r in _context.TaxRecords)
                writer.WriteLine($"{r.Municipality},{r.StartDate:yyyy-MM-dd},{r.EndDate:yyyy-MM-dd},{r.Rate}");
        }

        public void ImportFromCsv(string filename)
        {
            if (!File.Exists(filename)) return;
            using var reader = new StreamReader(filename);
            reader.ReadLine();
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length == 4 &&
                    DateTime.TryParse(parts[1], out var s) &&
                    DateTime.TryParse(parts[2], out var e) &&
                    Decimal.TryParse(parts[3], out var r))
                {
                    AddTax(parts[0], s, e, r);
                }
            }
        }
    }
}