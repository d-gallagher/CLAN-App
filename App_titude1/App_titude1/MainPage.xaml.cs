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

        private void About_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutPage());
        }

        private void Tutorial_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TutorialPage());
        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PushAsync(new GamePage());
            App.playClicked = true;
        }

        private void Options_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OptionsPage());
        }

    }
}
