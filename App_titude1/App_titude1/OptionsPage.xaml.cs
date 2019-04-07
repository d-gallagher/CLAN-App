using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_titude1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OptionsPage : ContentPage
	{

        public OptionsPage ()
		{
			InitializeComponent ();
            swRight_Left.IsToggled = App.isLeft;
		}

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Switch option = (Switch)sender;

            App.isLeft = option.IsToggled;

            if (App.isLeft == true)
            {
                lblOption.Text = "Left-Handed GamePlay Enabled";
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                    case Device.Android:
                        imgOption.Source = ImageSource.FromFile("gameView_left.png");
                        break;
                    case Device.UWP:
                        imgOption.Source = ImageSource.FromFile("Images/gameView_left.png");
                        break;
                    default: break;
                }
            }
            else
            {
                lblOption.Text = "Right-Handed GamePlay Enabled";
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                    case Device.Android:
                        imgOption.Source = ImageSource.FromFile("gameView.png");
                        break;
                    case Device.UWP:
                        imgOption.Source = ImageSource.FromFile("Images/gameView.png");
                        break;
                    default: break;
                }
            }
        }
    }
}
