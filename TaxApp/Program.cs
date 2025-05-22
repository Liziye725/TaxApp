using System;
using TaxApp.Data;
using TaxApp.Services;
using System.Globalization;

namespace TaxApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            var db = new TaxContext();
            var taxService = new TaxService(db);

            taxService.AddTax("Copenhagen", new DateTime(2025, 1, 1), new DateTime(2025, 12, 31), 0.2m);
            taxService.AddTax("Copenhagen", new DateTime(2025, 5, 1), new DateTime(2025, 5, 31), 0.4m);
            taxService.AddTax("Copenhagen", new DateTime(2025, 1, 1), new DateTime(2025, 1, 1), 0.1m);
            taxService.AddTax("Copenhagen", new DateTime(2025, 12, 25), new DateTime(2025, 12, 25), 0.1m);

            while (true)
            {
                Console.WriteLine("----------Tax Inquriy System----------");
                Console.WriteLine("1. Query tax rate\n2. Add tax rate\n3. Export CSV\n4. Import CSV\n0. Exit");
                Console.Write("Please select an action: ");
                var input = Console.ReadLine();
                if (input == "0") break;

                if (input == "1")
                {
                    Console.Write("City name: ");
                    string? city = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(city))
                    {
                        Console.WriteLine("⚠️ City name can't be empty, please input again.");
                        continue;
                    }
                    Console.Write("Date (YYYY-MM-DD): ");
                    var dateInput = Console.ReadLine();
                    if (DateTime.TryParse(dateInput, out DateTime date))
                    {
                        var rate = taxService.GetTax(city, date);
                        Console.WriteLine($"{city} on {date:yyyy-MM-dd} has tax rate:  {(rate.HasValue ? rate.Value.ToString("0.##", CultureInfo.InvariantCulture) : "No record")}");
                    }
                }
                else if (input == "2")
                {
                    Console.Write("City name: ");
                    string? city = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(city))
                        {
                            Console.WriteLine("⚠️ City name can't be empty, please input again.");
                            continue;
                        }
                    Console.Write("Start date (YYYY-MM-DD): ");
                    var start = Console.ReadLine();
                    Console.Write("End date (YYYY-MM-DD): ");
                    var end = Console.ReadLine();
                    Console.Write("Tax rate: ");
                    var rate = Console.ReadLine();
                    if (DateTime.TryParse(start, out var s) && DateTime.TryParse(end, out var e) && Decimal.TryParse(rate, NumberStyles.Number, CultureInfo.InvariantCulture, out var r))
                    {
                        taxService.AddTax(city, s, e, r);
                        Console.WriteLine("✅ Added successfully!");
                    }
                }
                else if (input == "3")
                {
                    taxService.ExportToCsv("export.csv");
                    Console.WriteLine("✅ Exported to export.csv");
                }
                else if (input == "4")
                {
                    taxService.ImportFromCsv("import.csv");
                    Console.WriteLine("✅ Imported from import.csv");
                }
            }
        }
    }
}