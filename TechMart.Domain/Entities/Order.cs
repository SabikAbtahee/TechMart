using System;
using System.Collections.Generic;
using System.Text;

namespace TechMart.Domain.Entities
{
    public class Order : BaseEntity
    {

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string ShippingAddress { get; set; }

        public string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
