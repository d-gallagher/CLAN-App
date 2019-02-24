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
        //private bool isRight;

        public GamePage ()
		{
			InitializeComponent ();
            //isRight = App.isLeft;
            setIsLeft();
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);

        }

        public void setIsLeft()
        {
            gridGamesLeft.IsVisible = App.isLeft;
            gridGamesRight.IsVisible = !App.isLeft;
        }

        //*********Calculator Button Logic*********
        string calcInput = " ";
        private void calcBtnClick(object sender, EventArgs e)
        {
            var val = ((Button)sender).Text;
            //string val = sender.Text;
            lblArithBox.Text += val;
            calcInput += val;
        }
        //private void Btn0_Clicked(object sender, EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    string val = b.Text;
        //    lblArithBox.Text += val ;
        //    calcInput += val;
        //}
        //private void Btn1_Clicked(object sender, EventArgs e)
        //{
        //    string val = "1";
        //    lblArithBox.Text += val ;
        //    calcInput += val;
        //}
        //private void Btn2_Clicked(object sender, EventArgs e)
        //{
        //    string val = "2";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        //private void Btn3_Clicked(object sender, EventArgs e)
        //{
        //    string val = "3";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        //private void Btn4_Clicked(object sender, EventArgs e)
        //{
        //    string val = "4";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        //private void Btn5_Clicked(object sender, EventArgs e)
        //{
        //    string val = "5";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        //private void Btn6_Clicked(object sender, EventArgs e)
        //{
        //    string val = "6";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        //private void Btn7_Clicked(object sender, EventArgs e)
        //{
        //    string val = "7";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        //private void Btn8_Clicked(object sender, EventArgs e)
        //{
        //    string val = "8";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        //private void Btn9_Clicked(object sender, EventArgs e)
        //{
        //    string val = "9";
        //    lblArithBox.Text += val;
        //    calcInput += val;
        //}
        private void BtnCLR_Clicked(object sender, EventArgs e)
        {

            lblArithBox.Text = lblArithBox.Text.Replace(calcInput, " ");
            calcInput = " ";
        }

        //*********letterGame Logic*********
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

            //put a sum in the label
            populateAritLabel();

            //Console.WriteLine(arr.Length + " " + arr[0] + " " + arr[1] + " " + arr[2] + " " + arr[3] + " " + x);
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
            int a = random.Next(1, 13);
            int b = random.Next(2, 12);
            int answer = a * b;
            lblArithBox.Text = a + " * " + b + ": ";
            return answer;
        }

        public void populateAritLabel()
        {
            Random random = new Random();

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


        }

        //*********colourGame Logic*********
        //colour frames
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

        //moving icons - code from https://github.com/xamarin/xamarin-forms-book-samples 

        static readonly TimeSpan duration = TimeSpan.FromSeconds(1);
        Random random = new Random();
        Point startPoint;
        Point animationVector;
        DateTime startTime;
        Button button = null;
        void OnButtonClicked(object sender, EventArgs args)
        {
            button = (Button)sender;
            View container = (View)button.Parent;

            // The start of the animation is the current Translation properties.
            startPoint = new Point(button.TranslationX, button.TranslationY);

            // The end of the animation is a random point.
            double endX = (random.NextDouble() - 0.5) * (container.Width - button.Width);
            //double endY = (random.NextDouble() - 0.5) * (container.Height - button.Height);

            // Create a vector from start point to end point.
            animationVector = new Point(endX - startPoint.X, 0); // , endY - startPoint.Y);

            // Save the animation start time.
            startTime = DateTime.Now;
          
            // Get the elapsed time from the beginning of the animation.
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Normalize the elapsed time from 0 to 1.
            double t = Math.Max(0, Math.Min(1, elapsedTime.TotalMilliseconds /
                                                    duration.TotalMilliseconds));

            // Calculate the new translation based on the animation vector.
            button.TranslationX = startPoint.X + t * animationVector.X;
            //button.TranslationY = startPoint.Y + t * animationVector.Y;

        }

        bool OnTimerTick()
        {
            // Get the elapsed time from the beginning of the animation.
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Normalize the elapsed time from 0 to 1.
            double t = Math.Max(0, Math.Min(1, elapsedTime.TotalMilliseconds /
                                                    duration.TotalMilliseconds));

            // Calculate the new translation based on the animation vector.
            //button.TranslationX = startPoint.X + t * animationVector.X;
            //button.TranslationY = startPoint.Y + t * animationVector.Y;
            return true;
        }

        //shake button
        async void btnShake_Clicked(object sender, EventArgs e)
        {
            Button shake = (Button)sender;
            uint timeout = 50;

            await shake.TranslateTo(-15, 0, timeout);

            await shake.TranslateTo(15, 0, timeout);

            await shake.TranslateTo(-10, 0, timeout);

            await shake.TranslateTo(10, 0, timeout);

            await shake.TranslateTo(-5, 0, timeout);

            await shake.TranslateTo(5, 0, timeout);

            shake.TranslationX = 0;

        }
        //right/left color game bools
        
    }
}