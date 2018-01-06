using System;
using System.Collections.Generic;

namespace Git_Gud_At_Math.Controls
{
    public class DiceRoller
    {
        public Random RandomGenerator { get; set; }
        public int DiceSides { get; set; }
        public int DicePerGame { get; set; }
        public int Games { get; set; }

        public DiceRoller(int diceSides, int dicePerGame, int games)
        {
            this.DiceSides = diceSides;
            this.DicePerGame = dicePerGame;
            this.Games = games;

            this.RandomGenerator = new Random();
        }

        public List<List<int>> RollGames()
        {
            List<List<int>> games = new List<List<int>>();

            for (int i = 0; i < this.Games; i++)
            {
                games.Add(this.RollGame());
            }

            return games;
        }

        public List<int> RollGame()
        {
            List<int> results = new List<int>();

            for (int i = 0; i < this.DicePerGame; i++)
            {
                // Roll dice
                int dice = this.RandomGenerator.Next(this.DiceSides);

                results.Add(dice);
            }

            return results;
        }
    }
}
