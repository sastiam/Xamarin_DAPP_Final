using App2.Models;
using App2.ViewModels;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderForm : ContentPage
    {
        public OrderForm(ProductModel product, CartModel shoppingCart)
        {
            InitializeComponent();
            BindingContext = new OrderFormViewModel(shoppingCart, product);

            imgarticulo.Source = product.ImageSource;
            cmbstock.ItemsSource = Enumerable.Range(1, product.ProductStock).ToList();

            txtdescripcion.Text = "Articulo: " + product.ProductName + " Precio: " + product.ProductPrice.ToString();
            txttotalproductos.Text = "Total productos: " + shoppingCart.CartItems.Count.ToString();
        }
    }
}