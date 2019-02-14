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
	public partial class GamePage : ContentPage
	{
        private ContentPage contentPage;

        public GamePage ()
		{
			InitializeComponent ();
		}

        //*********Calculator Button Logic*********
        private void Btn0_Clicked(object sender, EventArgs e)
        {
            string val = "0";
            lblArithBox.Text += val ;
        }
        private void Btn1_Clicked(object sender, EventArgs e)
        {
            string val = "1";
            lblArithBox.Text += val ;
        }
        private void Btn2_Clicked(object sender, EventArgs e)
        {
            string val = "2";
            lblArithBox.Text += val;
        }
        private void Btn3_Clicked(object sender, EventArgs e)
        {
            string val = "3";
            lblArithBox.Text += val;
        }
        private void Btn4_Clicked(object sender, EventArgs e)
        {
            string val = "4";
            lblArithBox.Text += val;
        }
        private void Btn5_Clicked(object sender, EventArgs e)
        {
            string val = "5";
            lblArithBox.Text += val;
        }
        private void Btn6_Clicked(object sender, EventArgs e)
        {
            string val = "6";
            lblArithBox.Text += val;
        }
        private void Btn7_Clicked(object sender, EventArgs e)
        {
            string val = "7";
            lblArithBox.Text += val;

        }
        private void Btn8_Clicked(object sender, EventArgs e)
        {
            string val = "8";
            lblArithBox.Text += val;
        }
        private void Btn9_Clicked(object sender, EventArgs e)
        {
            string val = "9";
            lblArithBox.Text += val;
        }
        private void BtnCLR_Clicked(object sender, EventArgs e)
        {
            lblArithBox.Text = " ";
        }

        //*********colorGame Logic*********
        //generate random char between a - z inclusive
        private char GenerateChar(Random rng)
        {
            // 'Z' + 1 because the range is exclusive
            return (char)(rng.Next('A', 'Z' + 1));
        }

        //gen random string of letters
        private string generateLetters(Random rng, int length)
        {
            char[] letters = new char[length];
            for (int i = 0; i < length; i++)
            {
                letters[i] = GenerateChar(rng);
            }
            return new string(letters);
        }

        //add 4 random letter strings to list, array
        private string[] populateLetterGame()
        {
            Random rng = new Random();
            List<string> letterSet = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                letterSet.Add(generateLetters(rng, 3));
            }
            return letterSet.ToArray();
        }

        //*TEMP* when go clicked - set color buttons text, and label prompt - working
        //*TEMP* when go clicked set random add/sub/mult to arith label - working
        private void BtnGO_Clicked(object sender, EventArgs e)
        {
            string[] arr = populateLetterGame();
            bntColourTL.Text = arr[0];
            bntColourTR.Text = arr[1];
            bntColourBL.Text = arr[2];
            bntColourBR.Text = arr[3];
            Random random = new Random();
            lblLetterPrompt.Text = arr[random.Next(0, 4)];

            int x = random.Next(0, 3);
            switch (x)
            {
                case 0:
                    x = genAddPromlem();
                    break;
                case 1:
                    x = genSubPromlem();
                    break;
                case 2:
                    x = genMultPromlem();
                    break;
                default: break;
            }

            Console.WriteLine(arr.Length + " " + arr[0] + " " + arr[1] + " " + arr[2] + " " + arr[3] + " " + x);
        }

        //*********numberGame Logic*********
        private int genAddPromlem()
        {
            Random random = new Random();
            int a = random.Next(1, 100);
            int b = random.Next(1, 100);
            int answer = a + b;

            lblArithBox.Text = a + " + " + b + ": ";

            return answer;
        }
        private int genSubPromlem()
        {
            Random random = new Random();
            int a = random.Next(1, 100);
            int b = random.Next(1, a);
            int answer = a - b;
            lblArithBox.Text = a + " - " + b + ": ";
            return answer;
        }
        private int genMultPromlem()
        {
            Random random = new Random();
            int a = random.Next(1, 100);
            int b = random.Next(2, 12);
            int answer = a * b;
            lblArithBox.Text = a + " * " + b + ": ";
            return answer;
        }

        private void RED_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lblLetterPrompt.Text = "RedTapped!";
        }

        private void YELLOW_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lblLetterPrompt.Text = "YellowTapped!";
        }

        private void GREEN_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lblLetterPrompt.Text = "GreenTapped!";
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{

        //}
    }
}