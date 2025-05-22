using System;
namespace TaxApp.Models
{
    public class TaxRecord
    {
        public int Id { get; set; }
        public string? Municipality { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Rate { get; set; }
    }
}