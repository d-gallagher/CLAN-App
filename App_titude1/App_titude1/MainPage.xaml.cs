using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_titude1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            //navigate to bodyfate.xaml page
            Navigation.PushAsync(new GamePage());
            //new NavigationPage(new GamePage());
        }
    }
}
