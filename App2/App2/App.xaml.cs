using App2.Models;
using System;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    public partial class App : Application
    {

        private static SqlConnection connection;

        public App()
        {
            InitializeComponent();

            ConnectionSql connectionSql = new ConnectionSql();

            connection = connectionSql.connect();

            ComputadorasData computadorasData = new ComputadorasData(connectionSql.show("select * from articulo where idcategoria=1 and stock > 2"));
            ImpresorasData impresorasData = new ImpresorasData(connectionSql.show("select * from articulo where idcategoria=2 and stock > 2"));
            TecladosData tecladosData = new TecladosData(connectionSql.show("select * from articulo where idcategoria=3 and stock > 2"));
            AudifonosData audifonosData = new AudifonosData(connectionSql.show("select * from articulo where idcategoria=4 and stock > 2"));

            CartModel shoppingCart = new CartModel();

            CarouselPage carousel = new CarouselPage();
            carousel.Children.Add(new MainPage());
            carousel.Children.Add(new Views.Page1(shoppingCart));
            carousel.Children.Add(new Views.Page2(shoppingCart));
            carousel.Children.Add(new Views.Page3(shoppingCart));
            carousel.Children.Add(new Views.Page4(shoppingCart));
            carousel.Children.Add(new Views.Page5(shoppingCart));

            MainPage = new NavigationPage(carousel);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


    }
}
