using System;
using System.Collections.Generic;
using System.Text;

namespace TechMart.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int ProductId { get; set; }

        public int Quantity{ get; set; }

        public int UserId { get; set; }
    }
}
