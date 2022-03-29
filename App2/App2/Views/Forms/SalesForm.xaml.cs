using App2.Models;
using App2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesForm : ContentPage
    {
        public SalesForm(CartModel shoppingCart)
        {
            InitializeComponent();
            BindingContext = new FormViewModel(shoppingCart);

            txtdescripcion.Text = "Total productos: " + shoppingCart.CartItems.Count.ToString();

        }
    }
}