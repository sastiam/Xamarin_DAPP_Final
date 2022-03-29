using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public string ImageSource { get; set; }

        public int ProductStock { get; set; }
    }
}
