using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Input;
using App2.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App2.ViewModels
{
    class OrderFormViewModel : BaseViewModel
    {
        private CartModel shoppingCart;
        private ProductModel product;

        public OrderFormViewModel(CartModel shoppingCart, ProductModel product)
        {
            this.shoppingCart = shoppingCart;
            this.product = product;
        }

        #region attributes
        private int stock;
        #endregion


        #region properties
        public int cmbstock
        {
            get { return this.stock; }
            set { SetValue(ref this.stock, value); }
        }
        #endregion


        #region commands
        public ICommand AgregarProducto
        {
            get
            {
                return new RelayCommand(verify);
            }
        }
        #endregion

        #region methods
        private void verify()
        {
           try
            {
                if (this.cmbstock == 0)
                {
                    Application.Current.MainPage.DisplayAlert("Whoops!", "Seleccione stock", "OK");
                    return;
                }

                Models.ConnectionSql consql = new Models.ConnectionSql();
                SqlConnection conn = consql.connect();

                string checkStockQuery = "SELECT stock FROM articulo WHERE idarticulo=" + this.product.ProductId;

                consql.show(checkStockQuery);

                DataSet ds = consql.getData();

                int queryStock = int.Parse(ds.Tables["show"].Rows[0].ItemArray[0].ToString());

                if (queryStock <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("Whoops!", "No contamos con stock, seleccione otro producto", "OK");
                    return;
                }

                this.shoppingCart.AddToCart(this.product, this.stock);
                
                Application.Current.MainPage.DisplayAlert("Aviso", "Producto agregado", "OK");
            
            } catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }
        }
        #endregion
    }
}
