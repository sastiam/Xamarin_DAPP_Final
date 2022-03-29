using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App2.Models
{
    public class CartModel
    {
        public List<CartItem> CartItems { get; }

        public CartModel()
        {
            CartItems = new List<CartItem>();
        }

        public void AddToCart(ProductModel product, int quantity)
        {
            try
            {
                var cartItem = CartItems.Find(item => item.Product.ProductId == product.ProductId);

                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        Id = CartItems.Count + 1,
                        Product = product,
                        Quantity = quantity

                    };

                    CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }

                
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveOfCart(int cartItemId)
        {
            try
            {
                var cartItem = CartItems.Find(item => item.Id == cartItemId);

                if (cartItem == null)
                {
                    return false;
                }

                return CartItems.Remove(cartItem);

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetProductsId()
        {
            return CartItems.Select(item => item.Product.ProductId).ToList();
        }

        public Double GetSubtotalCost()
        {
            return CartItems.Sum(item => item.Product.ProductPrice * item.Quantity);
        }

    }
}
