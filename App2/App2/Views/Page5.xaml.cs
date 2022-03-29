using App2.Models;
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
    public partial class Page5 : ContentPage
    {
        public Page5(CartModel shoppingCart)
        {
            InitializeComponent();

            Label title = new Label
            {
                Text = ":: Mantenimiento PC ::",
                BackgroundColor = Color.Black,
                FontSize = 20,
                TextColor = Color.White,
                FontFamily = "verdana",
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Start,
            };

            Image image = new Image
            {
                Source = "https://www.muycanal.com/wp-content/uploads/2017/02/SoporteTecnico.jpg",
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = new StackLayout
                {
                    Children =
                    {
                        new FlexLayout
                        {
                            AlignItems = FlexAlignItems.Center,
                            Children =
                            {
                                showData()
                            }
                        }
                    }
                }
            };

            Content = new StackLayout
            {
                Margin = new Thickness(5),
                BackgroundColor = Color.MediumAquamarine,
                Children =
                {
                    createBox(),
                    title,
                    image,
                    scrollView
                }
            };

        }

        private View showData()
        {
            return new Frame
            {
                WidthRequest = 300,
                HeightRequest = 480,
                Content = new FlexLayout
                {
                    Direction = FlexDirection.Row,
                    AlignContent = FlexAlignContent.Center,
                    Children =
                    {
                        new StackLayout
                        {
                            Spacing = 20,
                            Children =
                            {
                                customFlex("producto 1", "https://i.ebayimg.com/images/g/v-AAAOSw-7Zgp-~A/s-l300.jpg"),
                                customFlex("producto 2", "https://i.ebayimg.com/images/g/v-AAAOSw-7Zgp-~A/s-l300.jpg"),
                                customFlex("producto 3", "https://i.ebayimg.com/images/g/v-AAAOSw-7Zgp-~A/s-l300.jpg"),
                            }
                        }
                    }
               
                }
            };
        }

        private FlexLayout customFlex(string text, string imageUrl)
        {
            return new FlexLayout
            {
                Direction = FlexDirection.Row,
                Children =
                {
                   new Label
                   {
                       Text = text
                   },
                   new Image
                   {
                       Source = imageUrl,
                       WidthRequest = 180,
                       HeightRequest = 180

                   }
                }
            };
        }

        private BoxView createBox()
        {
            return new BoxView
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Black,
                WidthRequest = 400,
                HeightRequest = 20
            };
        }
    }
}