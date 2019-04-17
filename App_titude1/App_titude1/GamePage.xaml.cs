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
        private static int round;
        //Timers
        Timer roundTimer = new Timer();
        Timer iconAnimationTimer = new Timer();
        Timer letterButtonTimer = new Timer();
        Timer letterPromptTimer = new Timer();
        //True if icons are moving
        bool iconsMoving = false;
        //array of lettergame strings
        string[] lettersArray;

        public GamePage ()
		{
            InitializeComponent ();
            //Set Left / Right Game View
            SetIsLeft();
            //array of lettergame strings
            lettersArray = PopulateLettersArray();

        }

        //Set Game as Left/Right-Handed
        public void SetIsLeft()
        {
            gridGamesLeft.IsVisible = App.isLeft;
            gridGamesRight.IsVisible = !App.isLeft;
        }

        #region == Calculator Button Logic ==
        //Get Input from Calculator
        string calcInput = " ";
        private void calcBtnClick(object sender, EventArgs e)
        {
            var val = ((Button)sender).Text;
            //string val = sender.Text;
            lblArithBox.Text += val;
            calcInput += val;
        }
        
        //Clear input from Calculator
        private void BtnCLR_Clicked(object sender, EventArgs e)
        {
            lblArithBox.Text = lblArithBox.Text.Replace(calcInput, " ");
            calcInput = " ";
        }

        //Handle scoring and re-populate the sum label
        private void BtnGO_Clicked(object sender, EventArgs e)
        {
            //put a sum in the label
            PopulateAritLabel();

        }
        #endregion

        #region == LetterGame Logic ==
        //generate random char between a - z inclusive
        private static char GenerateChar(Random rng)
        {
            // 'Z' + 1 because the range is exclusive
            return (char)(rng.Next('A', 'Z' + 1));
        }

        //generate 'length' string of letters 
        private static string GenerateLetters(Random rng, int length)
        {
            char[] letters = new char[length];
            for (int i = 0; i < length; i++)
            {
                letters[i] = GenerateChar(rng);
            }
            return new string(letters);
        }

        //add 4 random letter strings to list, and return array
        private static string[] PopulateLettersArray()
        {
            Random rng = new Random();
            List<string> letterSet = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                letterSet.Add(GenerateLetters(rng, 3));
            }
            return letterSet.ToArray();
        }

        //Set letter buttons
        private void PopulateLetterButtons()
        {
            bntColourTL.Text = lettersArray[0];
            bntColourTR.Text = lettersArray[1];
            bntColourBL.Text = lettersArray[2];
            bntColourBR.Text = lettersArray[3];
        }
        //Set Prompt label
        private void PopulateLetterPrompt()
        {
            Random random = new Random();
            lblLetterPrompt.Text = lettersArray[random.Next(0, 4)];
        }
        #endregion

        #region == NumberGame Logic ==
        private int GenAddPromlem()
        {
            Random random = new Random();
            int a = random.Next(1, 100);
            int b = random.Next(1, 100);
            int answer = a + b;

            lblArithBox.Text = a + " + " + b + ": ";

            return answer;
        }
        private int GenSubPromlem()
        {
            Random random = new Random();
            int a = random.Next(1, 100);
            int b = random.Next(1, a);
            int answer = a - b;
            lblArithBox.Text = a + " - " + b + ": ";
            return answer;
        }

        private int GenMultPromlem()
        {
            Random random = new Random();
            int a = random.Next(1, 13);
            int b = random.Next(2, 12);
            int answer = a * b;
            lblArithBox.Text = a + " * " + b + ": ";
            return answer;
        }

        public void PopulateAritLabel()
        {
            Random random = new Random();

            int x = random.Next(0, 3);
            switch (x)
            {
                case 0:
                    x = GenAddPromlem();
                    break;
                case 1:
                    x = GenSubPromlem();
                    break;
                case 2:
                    x = GenMultPromlem();
                    break;
                default: break;
            }
        }
        #endregion

        #region == ColourGame Logic ==

        #region == colour frames tapped ==
        private void RED_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            ShakeFrameWhenTapped(frame);
            Button b;
            //set button depending on left/right orientation
            if (!App.isLeft)
            {
                b = gridParent.FindByName<Button>("btnRIcon_R");
            }
            else
            {
                b = gridParent.FindByName<Button>("btnRIcon_L");
            }

            bool overlap = DoesIconOverlapFrame(frame, b);
            ShakeButtonWhenTapped(b);

            if (overlap)
            {
                lblLetterPrompt.Text = "Red Overlaps!";
                Console.WriteLine("Got Collision");
            }

            Console.WriteLine("Tapped");
            //else lblLetterPrompt.Text = "RedTapped!";
        }

        private void YELLOW_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            ShakeFrameWhenTapped(frame);
            Button b;
            //set button depending on left/right orientation
            if (!App.isLeft)
            {
                b = gridParent.FindByName<Button>("btnYIcon_R");
            }
            else
            {
                b = gridParent.FindByName<Button>("btnYIcon_L");
            }

            bool overlap = DoesIconOverlapFrame(frame, b);

            if (overlap)
            {
                lblLetterPrompt.Text = "Yellow Overlaps!";
                Console.WriteLine("Got Collision");
            }
            //else lblLetterPrompt.Text = "YellowTapped!";
            Console.WriteLine("Tapped");
        }

        private void GREEN_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            ShakeFrameWhenTapped(frame);
            
            Button b;
            //set button depending on left/right orientation
            if (!App.isLeft)
            {
                b = gridParent.FindByName<Button>("btnGIcon_R");
            }
            else
            {
                b = gridParent.FindByName<Button>("btnGIcon_L");
            }

            bool overlap = DoesIconOverlapFrame(frame, b);

            if (overlap)
            {
                lblLetterPrompt.Text = "Green Overlaps!";
                Console.WriteLine("Got Collision");
            }
            Console.WriteLine("Tapped");
            //else lblLetterPrompt.Text = "GreenTapped!";
        }

        //return true if icon overlaps a frame when tapped
        public bool DoesIconOverlapFrame(Frame frame, Button button)
        {
            Point btn;            

            //set bounds of buttons
            btn = new Point(button.TranslationX, button.TranslationY);

            //check if overlapping w/frame when frame is tapped
            bool overlaps = frame.Bounds.Contains(btn);
            
            return overlaps;
        }

        //shake frame (vertical)
        async void ShakeFrameWhenTapped(object sender)
        {
            Frame shake = (Frame)sender;
            uint timeout = 50;
            double startPosition = shake.TranslationY;

            await shake.TranslateTo(0, -15, timeout);

            await shake.TranslateTo(0, 15, timeout);

            await shake.TranslateTo(0, -10, timeout);

            await shake.TranslateTo(0, 10, timeout);

            await shake.TranslateTo(0, -5, timeout);

            await shake.TranslateTo(0, 5, timeout);

            shake.TranslationY = startPosition;

        }
        #endregion

        #region == Icon Move Logic ==
        // Handle Round beginning and set animations and timers in motion
        private void BtnBegin_Clicked(object sender, EventArgs e)
        {
            //Set round time
            round = 60;
            //roundTimer. //end, close, dispose timer to fix bug? no joy

            //populate the first sum
            PopulateAritLabel();

            //Set timer label if left/right
            if (!App.isLeft) lblGameTimer_R.Text = "Time: " + round;
            else lblGameTimer_L.Text = "Time: " + round;

            //Update round timer label
            roundTimer.Start();
            roundTimer.Interval = 1000;
            roundTimer.Elapsed += RoundTimer_Elapsed;

            //Hide begin button while round in progress
            btnBegin.IsEnabled = false;
            btnBegin.IsVisible = false;

            //Start Icons moving and start timer to manage call upon each interval
            SetIconsMovingState(true);
            StartButtonAnimation();
            iconAnimationTimer.Start();
            iconAnimationTimer.Interval = 10000;
            iconAnimationTimer.Elapsed += IconAnimationTimer_Elapsed;

            //*Default while working on timer for letter labels*
            PopulateLetterPrompt();
            PopulateLetterButtons();

            //Timer to manage letter prompt
            letterPromptTimer.Start();
            letterPromptTimer.Interval = 5000;
            //letterPromptTimer.Elapsed += LetterPromptTimer_Elapsed;  
            if (round < 55)
            {
                letterPromptTimer.Stop();
            }

            //Timer to manage letter buttons
            if (round < 40 && round > 35)
            {
                letterButtonTimer.Start();
                letterButtonTimer.Interval = 5000;
                //letterButtonTimer.Elapsed += LetterButtonTimer_Elapsed;
            }
            //letterButtonTimer.Stop();
        }

        //On off switch for moving Icons
        private void SetIconsMovingState(bool startAnimation)
        {
            iconsMoving = startAnimation;
        }

        //Start icons moving 
        async void StartButtonAnimation()
        {
            //get buttons - to do: set bool to get left/right buttons
            Button r, y, g;
            if (!App.isLeft)
            {
                r = gridParent.FindByName<Button>("btnRIcon_R");
                y = gridParent.FindByName<Button>("btnYIcon_R");
                g = gridParent.FindByName<Button>("btnGIcon_R");
            }
            else
            {
                r = gridParent.FindByName<Button>("btnRIcon_L");
                y = gridParent.FindByName<Button>("btnYIcon_L");
                g = gridParent.FindByName<Button>("btnGIcon_L");
            }

            //get button parent grid
            View containerR = (View)r.Parent;
            View containerY = (View)y.Parent;
            View containerG = (View)g.Parent;

            //start from design time button location  
            Point startPointR = new Point(r.TranslationX, r.TranslationY);
            Point startPointY = new Point(y.TranslationX, y.TranslationY);
            Point startPointG = new Point(g.TranslationX, g.TranslationY);

            //end location of button 
            double endXRed = -containerR.Width + r.Width;
            double endXYel = -containerY.Width + y.Width;
            double endXGre = -containerG.Width + g.Width;

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
            }     
        }

        // == Timer Elapsed Methods == //
        private void LetterButtonTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PopulateLetterButtons();
            });
        }
        //Populate LetterPrompt at interval
        private void LetterPromptTimer_Elapsed(object sender, ElapsedEventArgs e)
        {            
            Device.BeginInvokeOnMainThread(() =>
            {
                if (round > 55)
                {
                    PopulateLetterPrompt();
                }
               
            });
        }
        //Animate Icons at interval
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
                round--;

                if (round <= 0)
                {
                    roundTimer.Stop();
                    btnBegin.IsEnabled = true;
                    btnBegin.IsVisible = true;
                    btnBegin.Text = "Replay";
                }
                
                if(!App.isLeft) lblGameTimer_R.Text = "Time: " + round;
                else lblGameTimer_L.Text = "Time: " + round;
            });
           
        }
        #endregion

        #endregion

        private void BtnRIcon_R_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            ShakeButtonWhenTapped(b);

        }
        async void ShakeButtonWhenTapped(object sender)
        {
            Button shake = (Button)sender;
            uint timeout = 50;
            double startPosition = shake.TranslationY;

            await shake.TranslateTo(0, -15, timeout);

            await shake.TranslateTo(0, 15, timeout);

            await shake.TranslateTo(0, -10, timeout);

            await shake.TranslateTo(0, 10, timeout);

            await shake.TranslateTo(0, -5, timeout);

            await shake.TranslateTo(0, 5, timeout);

            shake.TranslationY = startPosition;

        }
    }
}