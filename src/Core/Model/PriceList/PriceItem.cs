using Dapper.Contrib.Extensions;
using System;

namespace WebAPI.Domain.Entities
{
    [Table("PriceItem")]
    public class PriceItem
    {
        [ExplicitKey]
        public int Id { get; set; }
        [Key]
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public string CurrencyCode { get; set; }
        public string ServiceDiscountCategory { get; set; }
        public string ListPriceViewable { get; set; }
        public DateTime InsertionDate { get; set; }
    }
}
