using Xamarin.Forms;

namespace App2.Views
{
    public class CustomFlex : Element
    {
        private string text;
        private string imageUrl;

        public CustomFlex()
        {

        }

        public string Text { get => text; set => text = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}