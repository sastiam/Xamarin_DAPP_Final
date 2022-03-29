using Rg.Plugins.Popup.Pages;
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
    public partial class PopupWindow : PopupPage

    {
        public PopupWindow(string imageSource, string text)
        {
            InitializeComponent();
            img.Source = imageSource;
            title.Text = text;
        }
    }
}