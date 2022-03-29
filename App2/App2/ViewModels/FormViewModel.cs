using App2.Models;
using App2.Utils;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

using System.Windows.Input;
using Xamarin.Forms;

namespace App2.ViewModels
{
    class FormViewModel : BaseViewModel
    {
        private RegexValidation regexValidation;
        private CartModel cart;

        public FormViewModel(CartModel cart)
        {
            this.regexValidation = new RegexValidation();
            this.cart = cart;
        }

        #region attributes

        private string name;
        private string dni;
        private string address;
        private string phone;
        private string email;
        private DateTime date;

        #endregion

        #region properties

        public string txtnomcli
        {
            get { return this.name; }

            set { SetValue(ref this.name, value); }
        }

        public string txtdnicli
        {
            get { return this.dni; }
            set { SetValue(ref this.dni, value); }
        }

        public string txtdircli
        {
            get { return this.address; }
            set { SetValue(ref this.address, value); }
        }

        public string txttelcli
        {
            get { return this.phone; }
            set { SetValue(ref this.phone, value); }
        }

        public string txtcorreo
        {
            get { return this.email; }
            set
            {
                SetValue(ref this.email, value);
            }
        }

        public DateTime dpfecha
        {
            get { return this.date; }
            set
            {
                SetValue(ref this.date, value);
            }
        }


        #endregion

        #region commands
        
        public ICommand EnviarDatos
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
                bool value = this.regexValidation.IsValid(this.name, RegexValidation.NAME_PERSON);
                if (!this.regexValidation.IsValid(this.name, RegexValidation.NAME_PERSON))
                {
                    Application.Current.MainPage.DisplayAlert("Whoops!", "Nombre incorrecto y/o vacio", "OK");
                    return;
                }

                if (!this.regexValidation.IsValid(this.dni, RegexValidation.DNI))
                {
                    Application.Current.MainPage.DisplayAlert("Whoops!", "DNI incorrecto y/o vacio", "OK");
                    return;
                }

                if (!this.regexValidation.IsValid(this.address, RegexValidation.ADDRESS))
                {
                    Application.Current.MainPage.DisplayAlert("Whoops!", "Direccion incorrecto y/o vacio", "OK");
                    return;
                }

                if (!this.regexValidation.IsValid(this.phone, RegexValidation.MOBILE_NUMBER))
                {
                    Application.Current.MainPage.DisplayAlert("Whoops!", "Telefono incorrecto y/o vacio", "OK");
                    return;
                }

                if (!this.regexValidation.IsValidEmail(this.txtcorreo))
                {
                    Application.Current.MainPage.DisplayAlert("Whoops!", "Correo incorrecto y/o vacio", "OK");
                    return;
                }

                Models.ConnectionSql consql = new Models.ConnectionSql();
                SqlConnection conn = consql.connect();
                

                string querySql = "INSERT INTO cliente(nombre, dni, direccion, telefono, email) " +
                                "values(@nom,@dni,@dir,@tel,@email)";

                SqlCommand command = new SqlCommand(querySql, conn);

                command.Parameters.Clear();

                command.Parameters.Add("nom", SqlDbType.VarChar, 100).Value = this.txtnomcli;
                command.Parameters.Add("dni", SqlDbType.VarChar, 100).Value = this.txtdnicli;
                command.Parameters.Add("dir", SqlDbType.VarChar, 100).Value = this.txtdircli;
                command.Parameters.Add("tel", SqlDbType.VarChar, 100).Value = this.txttelcli;
                command.Parameters.Add("email", SqlDbType.VarChar, 100).Value = this.txtcorreo;

                int rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    Application.Current.MainPage.DisplayAlert("Aviso", "Cliente grabado OK", "OK");
                }

                querySql = "SELECT * FROM cliente WHERE email='" + this.email.ToString() + "'" + "AND dni='" + this.dni + "'";

                consql.show(querySql);

                DataSet ds = consql.getData();

                int codcli = int.Parse(ds.Tables["show"].Rows[0].ItemArray[0].ToString());

                // -- venta

                Double subtotal = this.cart.GetSubtotalCost();
                Double igv = 0.18 * subtotal;
                Double total = igv + subtotal;

                querySql = "INSERT INTO venta(idcliente, fechahora, subtotal, igv, total, estado) " +
                            "VALUES(@id, @fecha, @subtotal, @igv, @total, @estado)";

                command = new SqlCommand(querySql, conn);

                command.Parameters.Clear();

                command.Parameters.Add("id", SqlDbType.VarChar, 100).Value = codcli;
                command.Parameters.Add("fecha", SqlDbType.DateTime, 10).Value = this.date;
                command.Parameters.Add("subtotal", SqlDbType.Decimal, 10).Value = subtotal;
                command.Parameters.Add("igv", SqlDbType.Decimal, 10).Value = igv;
                command.Parameters.Add("total", SqlDbType.Decimal, 10).Value = total;
                command.Parameters.Add("estado", SqlDbType.VarChar, 100).Value = "Activo";

                rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    Application.Current.MainPage.DisplayAlert("Aviso", "Venta grabado OK", "OK");
                }

                querySql = "select * from venta where idcliente=" + codcli + "";

                consql.show(querySql);

                ds = Models.ConnectionSql.data;

                int codventa = int.Parse(ds.Tables["show"].Rows[0].ItemArray[0].ToString());

                Application.Current.MainPage.DisplayAlert("aviso", "codigo venta: " + codventa, "OK");

                int rowsDetail = 0;

                foreach (CartItem cartItem in this.cart.CartItems)
                {
                    
                    double importe = cartItem.GetProductImporte();

                    querySql = "INSERT INTO detalleventa(idventa, idarticulo, cantidad, importe) " +
                                "VALUES(@idventa, @idarticulo, @cantidad, @importe)";

                    command = new SqlCommand(querySql, conn);

                    command.Parameters.Clear();

                    command.Parameters.Add("idventa", SqlDbType.VarChar, 100).Value = codventa;
                    command.Parameters.Add("idarticulo", SqlDbType.Int).Value = cartItem.Product.ProductId;
                    command.Parameters.Add("cantidad", SqlDbType.Int).Value = cartItem.Quantity;
                    command.Parameters.Add("importe", SqlDbType.Decimal, 10).Value = importe;

                    rows = command.ExecuteNonQuery();

                    //--- decrementar stock

                    querySql = "update articulo " +
                                "set stock = stock - " + cartItem.Quantity + " where idarticulo = " + cartItem.Product.ProductId + "";

                    command = new SqlCommand(querySql, conn);

                    command.Parameters.Clear();

                    rowsDetail = command.ExecuteNonQuery();
                }

                if(rowsDetail > 0)
                {
                    this.cart.CartItems.Clear();
                    Application.Current.MainPage.DisplayAlert("Aviso", "venta realizada", "OK");

                    GlobalVariables.CodigoVenta = codventa;
                }

            } catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Whoops", ex.Message, "OK");
            }
        }


        #endregion

        
    }
}
