using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class Dice
    {
        private Random random = new Random();

        public int Roll()
        {
            return this.random.Next(1, 7);
        }

        public void DisplayRollMessage(int rolledScore)
        {
            Console.WriteLine("You rolled {0}", rolledScore);
        }
    }
}
