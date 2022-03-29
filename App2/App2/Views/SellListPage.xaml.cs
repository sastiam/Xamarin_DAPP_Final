using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellListPage : ContentPage
    {
        public SellListPage()
        {
            InitializeComponent();

            Models.ConnectionSql consql = new Models.ConnectionSql();
            SqlConnection conn = consql.connect();

            string querySql = "SELECT nombre, imagen, importe, cantidad, total FROM detalleventa AS dv INNER JOIN venta AS v ON v.idventa=dv.idventa INNER JOIN articulo AS art ON art.idarticulo=dv.idarticulo WHERE dv.idventa=5";

            consql.show(querySql);

            TableView tableArticles = new TableView();
            tableArticles.BackgroundColor = Color.Yellow;

            TableRoot tableRoot = new TableRoot("Ventas");

            tableArticles.Root = tableRoot;
            tableArticles.HasUnevenRows = true;
            tableArticles.Intent = TableIntent.Data;

            DataTable dataTable = Models.ConnectionSql.data.Tables["show"];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TableSection section = new TableSection();
                for (int y = 1; y <= 1; y++)
                {
                    ImageCell imageArticle = new ImageCell();
                    imageArticle.ImageSource = dataTable.Rows[i].ItemArray[1].ToString();
                    imageArticle.Detail = dataTable.Rows[i].ItemArray[0].ToString();
                    imageArticle.Text = dataTable.Rows[i].ItemArray[0].ToString();
                    imageArticle.TextColor = Color.Yellow;
                    string ArticleName = dataTable.Rows[i].ItemArray[0].ToString();
                    imageArticle.Tapped += (Object sender, EventArgs e) =>
                    {
                        PopupNavigation.Instance.PushAsync(new PopupWindow(imageArticle.Text, ArticleName));
                    };


                    section.Add(imageArticle);

                    // -- name
                    TextCell nameArticle = new TextCell();
                    nameArticle.Text = dataTable.Rows[i].ItemArray[0].ToString();

                    section.Add(nameArticle);

                    // -- price
                    TextCell priceArticle = new TextCell()
                    {
                        Text = "S/." + dataTable.Rows[i].ItemArray[2].ToString(),
                        TextColor = Color.Black,
                    };

                    section.Add(priceArticle);


                    // -- stock
                    TextCell stockArticle = new TextCell()
                    {
                        TextColor = Color.Black,
                    };
                    stockArticle.Text = dataTable.Rows[i].ItemArray[3].ToString();

                    section.Add(stockArticle);

                }

                tableRoot.Add(section);

            }

            Label title = new Label
            {
                Text = ":: Ultima compra ::",
                BackgroundColor = Color.Black,
                FontSize = 20,
                TextColor = Color.White,
                FontFamily = "verdana",
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            };

            Image img = new Image
            {
                Source = "https://pcexpressgt.com/wp-content/uploads/2021/04/5830760.png",
                VerticalOptions = LayoutOptions.Center
            };

            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = new StackLayout
                {
                    Children =
                    {
                        new Frame
                        {
                            BorderColor = Color.Black,
                            Padding = new Thickness(5),
                            CornerRadius = 10,
                            Content= new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Spacing = 15,
                                Children =
                                {
                                    tableArticles
                                }
                            }
                        }
                    }
                }
            };

            Content = new StackLayout
            {
                Margin = new Thickness(5),
                BackgroundColor = Color.Orange,
                Children =
                {
                    createBox(),
                    title,
                    img,
                    scrollView

                }
            };
        }

        private BoxView createBox()
        {
            var box = new BoxView
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Black,
                WidthRequest = 400,
                HeightRequest = 20
            };

            return box;
        }
    }
}