using System;
using System.Collections.Generic;
using System.Text;

namespace TechMart.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        /// <summary>Anonymous cart key stored in session (GUID).</summary>
        public string CartSessionId { get; set; } = string.Empty;
    }
}
