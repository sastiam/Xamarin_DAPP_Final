using System.ComponentModel.DataAnnotations;

namespace App2.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ProductModel Product { get; set; }

        public double GetProductImporte()
        {
            return Quantity * Product.ProductPrice;
        }
    }
}
