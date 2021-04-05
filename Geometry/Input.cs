using System;
using System.Collections.Generic;
using static Geometry.Validator;

namespace Geometry
{
    public static class Input
    {
        public static int RequestGameMode()
        {
            int modeSelector;
            Console.WriteLine("Do you want to play against another player or the computer?\n1. Vs Player\n2. Vs Computer");
            while (true)
            {
                modeSelector = ParseNumber();
                if (modeSelector == 1 || modeSelector == 2)
                {
                    break;
                }
            }
            return modeSelector;
        }

        public static void RequestBoardSize(out int cols, out int rows)
        {
            Console.WriteLine("Please set the size of the board. (mininum is: 20x30)");
            cols = InputValidation("columns", 20, 38);
            rows = InputValidation("rows", 30, 99);
        }

        public static int RequestNumberOfTurns()
        {
            return InputValidation("moves for the players ", 20, 3762);
        }

        public static string RequestName(string defaultName)
        {
            string name;
            Console.WriteLine($"{defaultName}, enter your name or leave the field blank:");
            name = Console.ReadLine();
            if (name.Length == 0)
            {
                return defaultName;
            }
            return name;
        }

        public static string RequestFiller(string defaultFiller, List<Player> players)
        {
            string filler;
            Console.WriteLine($"Your default filler is {defaultFiller}. Enter a new one or leave the field blank:");
            filler = Console.ReadLine();
            if (filler.Length == 0)
            {
                return defaultFiller;
            }
            else if (filler.Length != defaultFiller.Length)
            {
                Console.WriteLine("The filler must have 4 characters. Enter again:");
                return RequestFiller(defaultFiller, players);
            }
            else if (filler.Equals("    "))
            {
                Console.WriteLine("The filler cannot be the same as the board filler. Enter again:");
                return RequestFiller(defaultFiller, players);
            }
            else if (players.Count != 0)
            {
                foreach (var player in players)
                {
                    if (filler.Equals(player.Filler))
                    {
                        Console.WriteLine("This template is already taken. Enter again:");
                        return RequestFiller(defaultFiller, players);
                    }
                }
            }
            return filler;
        }

        public static Point RequestСoordinates(int cols, int rows)
        {
            return new(InputValidation("coordinate X", 1, cols), InputValidation("coordinate Y", 1, rows));
        }
    }
}