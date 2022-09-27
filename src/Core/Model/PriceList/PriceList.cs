using System;

namespace WebAPI.Domain.Entities
{
    public class PriceList
    {
        public string PartNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
