using App2.Models;
using App2.Views.Forms;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        public Page3(CartModel shoppingCart)
        {
            InitializeComponent();

            TableView tableArticles = new TableView();
            tableArticles.BackgroundColor = Color.Yellow;

            TableRoot tableRoot = new TableRoot("Teclados");

            tableArticles.Root = tableRoot;
            tableArticles.HasUnevenRows = true;
            tableArticles.Intent = TableIntent.Data;

            DataTable dataTable = Models.TecladosData.data.Tables["show"];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TableSection section = new TableSection();
                for (int y = 1; y <= 1; y++)
                {
                    ImageCell imageArticle = new ImageCell();
                    imageArticle.ImageSource = dataTable.Rows[i].ItemArray[5].ToString();
                    imageArticle.Detail = dataTable.Rows[i].ItemArray[0].ToString();
                    imageArticle.Text = dataTable.Rows[i].ItemArray[5].ToString();
                    imageArticle.TextColor = Color.Yellow;
                    string ArticleName = dataTable.Rows[i].ItemArray[2].ToString();
                    imageArticle.Tapped += (Object sender, EventArgs e) =>
                    {
                        PopupNavigation.Instance.PushAsync(new PopupWindow(imageArticle.Text, ArticleName));
                    };


                    section.Add(imageArticle);

                    // -- name
                    TextCell nameArticle = new TextCell();
                    nameArticle.Text = dataTable.Rows[i].ItemArray[2].ToString();

                    section.Add(nameArticle);

                    // -- price
                    TextCell priceArticle = new TextCell()
                    {
                        Text = "S/." + dataTable.Rows[i].ItemArray[3].ToString(),
                        TextColor = Color.Black,
                    };

                    section.Add(priceArticle);


                    // -- stock
                    TextCell stockArticle = new TextCell()
                    {
                        TextColor = Color.Black,
                    };
                    stockArticle.Text = dataTable.Rows[i].ItemArray[4].ToString();

                    section.Add(stockArticle);

                    // -- cart item
                    ProductModel product = new ProductModel
                    {
                        ProductId = int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        ProductName = dataTable.Rows[i].ItemArray[2].ToString(),
                        ProductPrice = Double.Parse(dataTable.Rows[i].ItemArray[3].ToString()),
                        ProductStock = int.Parse(dataTable.Rows[i].ItemArray[4].ToString()),
                        ImageSource = dataTable.Rows[i].ItemArray[5].ToString()
                    };

                    ImageCell buttonBuy = new ImageCell()
                    {
                        ImageSource = "https://www.freeiconspng.com/uploads/buy-now-button-png-5.png",
                    };
                    buttonBuy.Tapped += (Object sender, EventArgs e) =>
                    {
                        int stock = int.Parse(stockArticle.Text);
                        double price = double.Parse(stockArticle.Text);
                        string name = nameArticle.Text;

                        Models.GlobalVariables.codarticulo = int.Parse(imageArticle.Detail);
                        Models.GlobalVariables.precio = price;

                        Navigation.PushAsync(new OrderForm(product, shoppingCart));
                    };

                    section.Add(buttonBuy);

                }

                tableRoot.Add(section);

            }

            Label title = new Label
            {
                Text = ":: Teclados ::",
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