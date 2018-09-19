using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class DiceGame
    {
        private const int goal = 13;
        private const int maxTries = 3;

        private Dice dice = new Dice();

        private bool active = false;
        private bool gameOver = false;
        private bool playerStatsShowing = false;
        private int tries = 0;
        private int score = 0;

        public DiceGame()
        {
            Console.Title = "Dice Game";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        public void Run()
        {
            this.DisplaySplashScreen();

            while(true)
            {
                this.ListenForUserInput();
                this.ListenForGameOver();
            }
        }

        private void ListenForUserInput()
        {
            bool whileSplashScreen = !this.active;
            bool whilePlayerStatsScreen = this.playerStatsShowing;
            bool whilePlayingScreen = this.active && !this.gameOver;
            bool whileGameOverScreen = this.active && this.gameOver;
            ConsoleKey key = Console.ReadKey(true).Key;

            if (whilePlayingScreen)
            {
                if (key.Equals(ConsoleKey.Escape))
                {
                    this.Quit();
                }
                if (key.Equals(ConsoleKey.Spacebar))
                {
                    this.Play();
                }
                return;
            }
            if (whileGameOverScreen)
            {
                if (key.Equals(ConsoleKey.Escape))
                {
                    this.Quit();
                }
                if (key.Equals(ConsoleKey.Enter))
                {
                    Console.Clear();
                    this.Start();
                }
                return;
            }
            if (whilePlayerStatsScreen)
            {
                if (key.Equals(ConsoleKey.Escape))
                {
                    this.playerStatsShowing = false;
                    this.DisplaySplashScreen();
                }
                return;
            }
            if (whileSplashScreen)
            {
                if (key.Equals(ConsoleKey.Enter))
                {
                    Console.Clear();
                    this.Start();
                }
                if (key.Equals(ConsoleKey.Escape))
                {
                    Environment.Exit(0);
                }
                if (key.Equals(ConsoleKey.S))
                {
                    this.ShowPlayerStats();
                }
                return;
            }
        }

        private void ListenForGameOver()
        {
            if (this.score >= goal)
            {
                this.Win();
            }
            if (this.tries == maxTries)
            {
                this.Lose();
            }
        }

        private void Start()
        {
            this.DisplayControls();

            this.active = true;
            this.gameOver = false;
        }

        private void Quit()
        {
            this.DisplaySplashScreen();
            this.active = false;
            this.gameOver = false;
            this.Reset();
        }

        private void Play()
        {
            int rolledScore = dice.Roll();

            dice.DisplayRollMessage(rolledScore);

            this.score += rolledScore;
            this.tries++;
        }

        private void ShowPlayerStats()
        {
            this.playerStatsShowing = true;
            this.DisplayPlayerStats();
        }

        private void Lose()
        {
            this.gameOver = true;
            PlayerStatsSingleton.GetInstance().Losses++;
            this.DisplayLosingMessage();
            this.Reset();
        }

        private void Win()
        {
            this.gameOver = true;
            PlayerStatsSingleton.GetInstance().Wins++;
            this.DisplayWinningMessage();
            this.Reset();
        }

        private void Reset()
        {
            this.tries = 0;
            this.score = 0;
        }
         
        private void DisplaySplashScreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("████████▄   ▄█   ▄████████    ▄████████         ▄██████▄     ▄████████   ▄▄▄▄███▄▄▄▄      ▄████████");
            Console.WriteLine("███   ▀███ ███  ███    ███   ███    ███        ███    ███   ███    ███ ▄██▀▀▀███▀▀▀██▄   ███    ███");
            Console.WriteLine("███    ███ ███▌ ███    █▀    ███    █▀         ███    █▀    ███    ███ ███   ███   ███   ███    █▀");
            Console.WriteLine("███    ███ ███▌ ███         ▄███▄▄▄           ▄███          ███    ███ ███   ███   ███  ▄███▄▄▄");
            Console.WriteLine("███    ███ ███▌ ███        ▀▀███▀▀▀          ▀▀███ ████▄  ▀███████████ ███   ███   ███ ▀▀███▀▀▀");
            Console.WriteLine("███    ███ ███  ███    █▄    ███    █▄         ███    ███   ███    ███ ███   ███   ███   ███    █▄");
            Console.WriteLine("███   ▄███ ███  ███    ███   ███    ███        ███    ███   ███    ███ ███   ███   ███   ███    ███");
            Console.WriteLine("████████▀  █▀   ████████▀    ██████████        ████████▀    ███    █▀   ▀█   ███   █▀    ██████████");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("Objective:");
            Console.WriteLine("Reach a score of 13 by rolling the dice 3 times");
            Console.WriteLine("");
            Console.WriteLine("Controls:");
            Console.WriteLine("Press the spacebar to roll the dice");
            Console.WriteLine("Press escape to quit the game");
            Console.WriteLine("");
            Console.WriteLine("Press enter to start");
            Console.WriteLine("Press S to show player statistics");
        }

        private void DisplayControls()
        {
            Console.WriteLine("Spacebar: roll dice | Escape: back to home");
            Console.WriteLine("");
        }

        private void DisplayLosingMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Oh no! You lose!");

            this.DisplayGameOverMessage();
        }

        private void DisplayWinningMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Congratulations! You win!");
            
            this.DisplayGameOverMessage();
        }

        private void DisplayGameOverMessage()
        {
            this.DisplayWinsAndLosses();
            Console.WriteLine("Press enter to start a new game");
        }

        private void DisplayPlayerStats()
        {
            Console.Clear();
            Console.WriteLine("Player statistics");
            this.DisplayWinsAndLosses();
            Console.WriteLine("Press escape to go back to home screen");
        }

        private void DisplayWinsAndLosses()
        {
            Console.WriteLine("");
            Console.WriteLine("Total games won: " + PlayerStatsSingleton.GetInstance().Wins);
            Console.WriteLine("Total games lost: " + PlayerStatsSingleton.GetInstance().Losses);
            Console.WriteLine("");
        }
    }
}
