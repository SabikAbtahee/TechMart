using System;
using System.Collections.Generic;
using System.Text;

namespace TechMart.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity{ get; set; }

        public int UserId { get; set; }
    }
}
