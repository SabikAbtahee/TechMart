using System;
using System.Collections.Generic;
using System.Text;
using TechMart.Domain.Entities;

namespace TechMart.DataAccess.Modules.Carts.Interfaces
{
    public interface ICartRepository
    {
        CartItem GetCartItem(int id);
        void SaveCartItem(CartItem cartItem);

        void ClearCartItems();
    }
}
