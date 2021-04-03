using System;

namespace Geometry
{
    public static class Program
    {
        public static void Main()
        {
            Game newGame = new();
            newGame.StartGame();
            Console.ReadLine();
        }
    }
}