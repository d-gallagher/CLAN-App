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

        }
    }
}