using App2.Models;
using App2.Views.Forms;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartListPage : ContentPage
    {
        public CartListPage(CartModel shoppingCart)
        {
            InitializeComponent();

            TableView tableArticles = new TableView();
            tableArticles.BackgroundColor = Color.Yellow;

            TableRoot tableRoot = new TableRoot("Carrito de compras");

            tableArticles.Root = tableRoot;
            tableArticles.HasUnevenRows = true;
            tableArticles.Intent = TableIntent.Data;

            List<CartItem> cart = shoppingCart.CartItems;

            for (int i = 0; i < cart.Count; i++)
            {
                TableSection section = new TableSection();
                for (int y = 1; y <= 1; y++)
                {
                    ProductModel prod = cart[i].Product;

                    ImageCell imageArticle = new ImageCell();
                    imageArticle.ImageSource = prod.ImageSource;
                    imageArticle.Detail = prod.ProductId.ToString();
                    imageArticle.Text = prod.ImageSource;
                    imageArticle.TextColor = Color.Yellow;
                    string ArticleName = prod.ProductName;
                    imageArticle.Tapped += (Object sender, EventArgs e) =>
                    {
                        PopupNavigation.Instance.PushAsync(new PopupWindow(imageArticle.Text, ArticleName));
                    };


                    section.Add(imageArticle);

                    // -- name
                    TextCell nameArticle = new TextCell();
                    nameArticle.Text = prod.ProductName;

                    section.Add(nameArticle);

                    // -- price
                    TextCell priceArticle = new TextCell()
                    {
                        Text = "Costo total S/." + cart[i].GetProductImporte(),
                        TextColor = Color.Black,
                    };

                    section.Add(priceArticle);


                    // -- stock
                    TextCell stockArticle = new TextCell()
                    {
                        TextColor = Color.Black,
                        Text = "Cantidad: "
                    };
                    stockArticle.Text += cart[i].Quantity.ToString();

                    section.Add(stockArticle);

                }

                tableRoot.Add(section);

            }

            Label title = new Label
            {
                Text = ":: Carrito de compras ::",
                BackgroundColor = Color.Black,
                FontSize = 20,
                TextColor = Color.White,
                FontFamily = "verdana",
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            };


            Image buyNowImg = new Image
            {
                Source = "https://cdn-icons-png.flaticon.com/512/825/825575.png",
                VerticalOptions= LayoutOptions.Center,
                WidthRequest = 70,
                HeightRequest = 70
            };

            TapGestureRecognizer tapCart = new TapGestureRecognizer();
            tapCart.Tapped += (object sender, EventArgs e) =>
            {
                if(cart.Count > 0)
                {
                    Navigation.PushAsync(new SalesForm(shoppingCart));
                }

               
            };

            buyNowImg.GestureRecognizers.Add(tapCart);

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
                    buyNowImg,
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