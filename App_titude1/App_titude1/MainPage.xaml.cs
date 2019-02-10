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

        //*********Calculator Logic*********
        private void Button0_Clicked(object sender, EventArgs e)
        {
            string val = "0";
            lblArithBox.Text = val + "Selected";
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
            for(int i = 0; i < 4; i++)
            {
                letterSet.Add(generateLetters(rng, 3));
            }
            return letterSet.ToArray(); 
        }

        //*TEMP* when go clicked - set color buttons text, and label prompt - working
        //*TEMP* when go clicked set random add/sub/mult to arith label - working
        private void ButtonGO_Clicked(object sender, EventArgs e)
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
                case 0: x = genAddPromlem();
                    break;
                case 1: x = genSubPromlem();
                    break;
                case 2: x = genMultPromlem();
                    break;
                default: break;
            }

            Console.WriteLine(arr.Length + " " + arr[0] + " " + arr[1] + " " + arr[2] + " " + arr[3] + " " + x);
        }

        //*********numberGame Logic*********
        private int genAddPromlem()
        {
            Random random = new Random();
            int a = random.Next(0, 100);
            int b = random.Next(0, 100);
            int answer = a+b;

            lblArithBox.Text = a + " + " + b + " ?";

            return answer;
        }
        private int genSubPromlem()
        {
            Random random = new Random();
            int a = random.Next(0, 100);
            int b = random.Next(0, a);
            int answer = a - b;
            lblArithBox.Text = a + " - " + b + " ?";
            return answer;
        }
        private int genMultPromlem()
        {
            Random random = new Random();
            int a = random.Next(0, 100);
            int b = random.Next(2, 12);
            int answer = a * b;
            lblArithBox.Text = a + " * " + b + " ?";
            return answer;
        }
    }
}
