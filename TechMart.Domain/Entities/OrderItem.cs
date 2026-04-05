using System;
using System.Collections.Generic;
using System.Text;

namespace TechMart.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
