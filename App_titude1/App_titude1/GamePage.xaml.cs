using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App_titude1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
    {
        //round Length
        private int round;
        Timer roundTimer = new Timer();
        Timer iconAnimationTimer = new Timer();
        //Timer iconAnimationTimer = new Timer();
        bool iconsMoving = false;

        public GamePage ()
		{
            InitializeComponent ();
            //Set Left / Right Game View
            SetIsLeft();
            
        }

        //Set Game as Left/Right-Handed
        public void SetIsLeft()
        {
            gridGamesLeft.IsVisible = App.isLeft;
            gridGamesRight.IsVisible = !App.isLeft;
        }

        #region *********Calculator Button Logic*********
        string calcInput = " ";
        private void calcBtnClick(object sender, EventArgs e)
        {
            var val = ((Button)sender).Text;
            //string val = sender.Text;
            lblArithBox.Text += val;
            calcInput += val;
        }
      
        private void BtnCLR_Clicked(object sender, EventArgs e)
        {

            lblArithBox.Text = lblArithBox.Text.Replace(calcInput, " ");
            calcInput = " ";
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

        }
        #endregion

        #region *********letterGame Logic*********
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
        #endregion

        #region *********numberGame Logic*********
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
        #endregion

        #region *********colourGame Logic*********

        #region == colour frames tapped ==
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
        #endregion

        #region == OLD moving button logic ==
        //moving icons - code from https://github.com/xamarin/xamarin-forms-book-samples 
       
        static readonly TimeSpan duration = TimeSpan.FromSeconds(1);        

        Random random = new Random();
        Point startPoint;
        Point animationVector;
        DateTime startTime;
        Button button = null;

        private void OnButtonClicked(object sender, EventArgs args)
        {
            button = (Button)sender;
            View container = (View)button.Parent;

            // The start of the animation is the current Translation properties.
            startPoint = new Point(button.TranslationX, button.TranslationY);
            //startPoint1 = new Point(container.AnchorX, container.AnchorY);
            //startPoint2 = new Point(container.AnchorX, container.AnchorY);
            //startPoint3 = new Point(container.AnchorX, container.AnchorY);

            // The end of the animation is a random point.
            //double endX = (random.NextDouble() - 0.5) * (container.Width - button.Width);
            double endX = -container.Width + button.Width;

            // Create a vector from start point to end point.
            animationVector = new Point(endX - startPoint.X, 0); // , endY - startPoint.Y);

            // Save the animation start time.
            startTime = DateTime.Now;
          
            //// Get the elapsed time from the beginning of the animation.
            //TimeSpan elapsedTime = DateTime.Now - startTime;

            //// Normalize the elapsed time from 0 to 1.
            //double t = Math.Max(0, Math.Min(1, 3));

            //// Calculate the new translation based on the animation vector.
            //button.TranslationX = startPoint.X + t * animationVector.X;

        }

        public bool OnTimerTick()
        {
            // Get the elapsed time from the beginning of the animation.
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Normalize the elapsed time from 0 to 1.
            double t = .05;
            //Math.Max(0, Math.Min(1, elapsedTime.TotalMilliseconds / duration.TotalMilliseconds));

            // Calculate the new translation based on the animation vector.
            if (App.isLeft)
            {
                btnIconLEFT.TranslationX = startPoint.X + t * animationVector.X;
            }
            else
            {
                btnIconRIGHT.TranslationX = startPoint.X + t * animationVector.X;
            }
            
            return true;
        }
        #endregion

        #region == NEW button move logic
        private void SetIconsMovingState(bool startAnimation)//, bool cancelButtonState
        {
            iconsMoving = startAnimation;
        }


        async void StartButtonAnimation()
        {
            //get buttons
            Button r = gridParent.FindByName<Button>("btnIconRIGHT");
            Button g = gridParent.FindByName<Button>("btnShakeRIGHT");
            Button y = gridParent.FindByName<Button>("btnShakeRIGHT1");

            //get button parent grid
            View containerR = (View)r.Parent;
            View containerG = (View)g.Parent;
            View containerY = (View)y.Parent;

            //start from design time button location  
            Point startPointR = new Point(r.TranslationX, r.TranslationY);
            Point startPointG = new Point(g.TranslationX, g.TranslationY);
            Point startPointY = new Point(y.TranslationX, y.TranslationY);

            //end location of button - reset here?
            double endXRed = -containerR.Width + r.Width;
            double endXGre = -containerG.Width + g.Width;
            double endXYel = -containerY.Width + y.Width;

            if (iconsMoving)
            {
                bool isCancelled = await r.TranslateTo(endXRed, 0, 3000);
                if (!isCancelled)
                {
                    isCancelled = await r.TranslateTo(startPointR.X, 0, 100);
                }
                if (!isCancelled)
                {
                    isCancelled = await g.TranslateTo(endXGre, 0, 3000);
                }
                if (!isCancelled)
                {
                    isCancelled = await g.TranslateTo(startPointG.X, 0, 100);
                }
                if (!isCancelled)
                {
                    isCancelled = await y.TranslateTo(endXYel, 0, 3000);
                }
                if (!isCancelled)
                {
                    isCancelled = await y.TranslateTo(startPointY.X, 0, 100);
                }
            }//while true     
        }

        // set off color buttons
        // (re)Set round time, decrement counter
        // disable button
        private void BtnBegin_Clicked(object sender, EventArgs e)
        {
            round = 60;
            lblGameTimer.Text = "Time: " + round;

            roundTimer.Start();
            roundTimer.Interval = 1000;
            roundTimer.Elapsed += RoundTimer_Elapsed;
            

            btnBegin.IsEnabled = false;
            btnBegin.IsVisible = false;

            SetIconsMovingState(true);
            StartButtonAnimation();
            iconAnimationTimer.Start();
            iconAnimationTimer.Interval = 10000;
            iconAnimationTimer.Elapsed += IconAnimationTimer_Elapsed;
            

        }

        private void IconAnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //while the round is ongoing, continue moving icons
                //switching on and off at each interval

                if (round > 0)
                {
                    SetIconsMovingState(true);
                    StartButtonAnimation();
                }
                else SetIconsMovingState(false);

                

            });
        }

        //reduce round by 1 each second then re-enable button
        private void RoundTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //allows animation on main thread
            Device.BeginInvokeOnMainThread(() =>
            {
                 --round;
                if (round == 0)
                {
                    roundTimer.Stop();
                    btnBegin.IsEnabled = true;
                    btnBegin.IsVisible = true;
                    btnBegin.Text = "Replay";
                }
                
                lblGameTimer.Text = "Time: " + round;
            });
           
        }
        #endregion
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

        #endregion

        #region *********Game Timer Logic*********
        private bool ShowTime()
        {
            lblGameTimer.Text = "Time: " + DateTime.Now.ToString("mm:ss");
            //lblGameTimer.Text = "10";

            //timer1.Interval = 1000;
            //timer1.Elapsed += new ElapsedEventHandler(OnShowTimerElapsed);

            //timer1.Start();
            return true;
        }

        private void OnShowTimerElapsed(object sender, EventArgs e)
        {
            int count = int.Parse(lblGameTimer.Text);
            //lblGameTimer.Text = (count - 1).ToString(); //lowering the value in the label by 1
            if (count > 0)
            {
                lblGameTimer.Text = (count - 1).ToString();
            }
            
        }
        #endregion

    }
}