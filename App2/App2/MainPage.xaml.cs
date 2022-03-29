using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        Thread thread;
        long count;
        bool state = true;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            thread = new Thread(new ThreadStart(spinLogo));
            thread.Start();
            count = 0;
        }

        private void spinLogo()
        {
           while(state)
            {
                Thread.Sleep(100);
                count += 20;
                logoSenati.Rotation = count;

            }
        }
    }
}
