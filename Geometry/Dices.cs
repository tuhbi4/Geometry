using System;
using static Geometry.Board;

namespace Geometry
{
    public class Dices
    {
        public int FirstDice { get; private set; }
        public int SecondDice { get; private set; }
        static readonly Random diceRandomValue = new();
        public Dices(Player player)
        {
            RollingDices(player);
        }
        public void RollingDices(Player player)
        {
            FirstDice = diceRandomValue.Next(1, 6);
            SecondDice = diceRandomValue.Next(1, 6);
            Console.WriteLine("Throwing the dices ...");
            Console.WriteLine("+-------+       +-------+");
            Console.WriteLine("|\\       \\     /       /|");
            Console.WriteLine($"| +-------+   +-------+ |\n| |       |   |       | | \n+ |   {FirstDice}   |   |   {SecondDice}   | +");
            Console.WriteLine(" \\|       |   |       |/ ");
            Console.WriteLine("  +-------+   +-------+  ");
            if (!IsPlacementFieldOnBooardPossible(FirstDice, SecondDice))
            {
                Console.WriteLine("Oops, it seems like you can't draw a rectangle with dices like this.");
                if (player.AttemptsToRoll == 0)
                {
                    Console.WriteLine("Unfortunately, you're out of luck again. You have no more attempts to throw the dices...");
                    player.GameOver = true;
                    Console.WriteLine($"Don't worry {player.Name}, next time you will be more fortunate!");
                }
                else
                {
                    Console.WriteLine($"You have {player.AttemptsToRoll} more attempts...");
                    player.DecrementCountOfAttemptsToRoll();
                    RollingDices(player);
                }
            }
        }
    }
}